using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Autofac.Features.OwnedInstances;
using Common.Api.Models;
using Common.Api.UserContext;
using Common.Blob;
using Common.Data;
using Common.Json;
using Common.Tests;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.MigrationTool.Nomenclatures;
using Gva.MigrationTool.Sets.Common;
using Newtonsoft.Json.Linq;
using Oracle.DataAccess.Client;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.MigrationTool.Sets
{
    public class PersonLicenceDocMigrator : IDisposable
    {
        private bool disposed = false;
        private Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, IFileRepository, IApplicationRepository, ILotEventDispatcher, UserContext>>> dependencyFactory;
        private OracleConnection oracleConn;

        public PersonLicenceDocMigrator(
            OracleConnection oracleConn,
            Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, IFileRepository, IApplicationRepository, ILotEventDispatcher, UserContext>>> dependencyFactory)
        {
            this.dependencyFactory = dependencyFactory;
            this.oracleConn = oracleConn;
        }

        public void MigrateLicenceDocuments(
            Dictionary<int, int> personIdToLotId,
            //cancellation
            CancellationTokenSource cts,
            CancellationToken ct)
        {
            try
            {
                this.oracleConn.Open();
            }
            catch (Exception)
            {
                cts.Cancel();
                throw;
            }

            ct.ThrowIfCancellationRequested();
            using (var dependencies = this.dependencyFactory())
            {
                var unitOfWork = dependencies.Value.Item1;
                var lotRepository = dependencies.Value.Item2;
                var fileRepository = dependencies.Value.Item3;
                var applicationRepository = dependencies.Value.Item4;
                var lotEventDispatcher = dependencies.Value.Item5;
                var context = dependencies.Value.Item6;
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                int personId = -1;
                try
                {
                    var personsLicences = this.getPersonsLicenceDocuments();
                    ConcurrentQueue<int> personIds = new ConcurrentQueue<int>(personsLicences.Keys.ToArray());

                    string loginUri = "http://192.168.0.19:7777/pls/apex/f?p=223:1000:652650075342818::NO";
                    System.Net.CredentialCache credentialCache = new System.Net.CredentialCache();
                    credentialCache.Add(
                        new System.Uri(loginUri),
                        "Basic",
                        new System.Net.NetworkCredential("ri", "caa123065")
                    );
                    HttpWebRequest loginRequest = (HttpWebRequest)WebRequest.Create(loginUri);
                    HttpWebResponse loginResponse = (HttpWebResponse)loginRequest.GetResponse();

                    while (personIds.TryDequeue(out personId))
                    {
                        if (personIdToLotId.ContainsKey(personId))
                        {
                            var lot = lotRepository.GetLotIndex(personIdToLotId[personId], fullAccess: true);
                            JObject licence = null;
                            ConcurrentQueue<JObject> personLicences = new ConcurrentQueue<JObject>(personsLicences[personId]);

                            while (personLicences.TryDequeue(out licence))
                            {
                                string queryString = licence.Get<string>("query_string");
                                int oldId = licence.Get<int>("old_id");

                                int editionPartIndex = lot.Index.GetParts<dynamic>("licenceEditions")
                                    .Where(e => e.Content.__oldId == oldId)
                                    .Single().Part.Index;

                                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(queryString);
                                request.CookieContainer = new CookieContainer();
                                request.CookieContainer.Add(loginResponse.Cookies);

                                using (HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse())
                                {
                                    using (StreamReader readStream = new StreamReader(httpResponse.GetResponseStream(), encode))
                                    {
                                        this.SaveStreamToBlob(readStream.BaseStream, editionPartIndex, lot, unitOfWork, lotRepository, context, lotEventDispatcher);
                                    }
                                }
                            }
                            Console.WriteLine("Migrated printed licences of personId: {0}", personId);
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in migration of printed licences of personId: {0}", personId);
                    cts.Cancel();
                    throw;
                }
            }
        }

        private IDictionary<int, IEnumerable<JObject>> getPersonsLicenceDocuments()
        {
            return this.oracleConn.CreateStoreCommand(
                @"select 
                'http://192.168.0.19:7777/pls/apex/CAA_DOC.printable_documents_routines.print_document_in_msword?p_xml_generator='
                || (select xml_generator from   caa_doc.prt_printable_documents pr_doc
                       where pr_doc.id = lt.prt_printable_document_id*DECODE('LL', 'RN', -1, 1))
                ||'#p_number1='||ll.id||'.#p_printable_documents_id='
                ||lt.prt_printable_document_id*DECODE('LL', 'RN', -1, 1)
                ||'#p_uid='|| 'ri'
                as for_print,
                ll.ID,
                p.ID as PERSON_ID
                 from   caa_doc.CAA CAA
                       , caa_doc.NM_LICENCE_TYPE lt
                       , caa_doc.LICENCE l
                       , caa_doc.licence_log ll
                       , caa_doc.nm_licence_action la
                       , caa_doc.person p
                 where   l.LICENCE_TYPE_ID=lt.ID
                   and   l.PUBLISHER_CAA_ID=CAA.ID
                   and   l.id = ll.licence_id
                   and   l.person_id = p.id
                   and   ll.licence_action_id = la.id")
                .Materialize(r => new 
                    {
                        query_string = r.Field<string>("FOR_PRINT").Replace("#p", "&p"),
                        old_id = r.Field<int>("ID"),
                        person_id = r.Field<int>("PERSON_ID")
                    })
                    .ToList()
                    .GroupBy(d => d.person_id)
                    .ToDictionary(g => g.Key,
                    g => g.Select(r => 
                        new JObject(
                            Utils.ToJObject(
                            new {
                                r.query_string,
                                r.old_id,
                                r.person_id
                            }))));
        }

        public void SaveStreamToBlob(Stream responseStream, int editionPartIndex, Lot lot,
            IUnitOfWork unitOfWork, ILotRepository lotRepository, UserContext userContext, ILotEventDispatcher lotEventDispatcher)
        {
            using (var transaction = unitOfWork.BeginTransaction())
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    responseStream.CopyTo(memoryStream);

                    Guid licenceEditionDocBlobKey = Guid.Empty;

                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
                    {
                        connection.Open();
                        using (var blobWriter = new BlobWriter(connection))
                        using (var blobStream = blobWriter.OpenStream())
                        {
                            blobStream.Write(memoryStream.ToArray(), 0, (int)memoryStream.Length);
                            licenceEditionDocBlobKey = blobWriter.GetBlobKey();
                        }
                    }

                    PartVersion<PersonLicenceEditionDO> licenceEditionPartVersion = lot.Index.GetPart<PersonLicenceEditionDO>(string.Format("licenceEditions/{0}", editionPartIndex));
                    licenceEditionPartVersion.Content.PrintedDocumentBlobKey = licenceEditionDocBlobKey;

                    lot.UpdatePart<PersonLicenceEditionDO>(string.Format("licenceEditions/{0}", licenceEditionPartVersion.Part.Index), licenceEditionPartVersion.Content, userContext);

                    lot.Commit(userContext, lotEventDispatcher);

                    unitOfWork.Save();

                    transaction.Commit();
                }
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            try
            {
                if (disposing && !disposed)
                {
                    this.oracleConn.Dispose();
                }
            }
            finally
            {
                disposed = true;
            }
        }
    }
}
