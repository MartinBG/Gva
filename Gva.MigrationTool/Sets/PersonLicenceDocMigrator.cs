using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading;
using Autofac.Features.OwnedInstances;
using Common.Api.UserContext;
using Common.Data;
using Common.Json;
using Common.Tests;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.PrintRepository;
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
        private Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, IPrintRepository, ILotEventDispatcher, UserContext>>> dependencyFactory;
        private OracleConnection oracleConn;
        private ILotRepository lotRepository;
        private IUnitOfWork unitOfWork;
        private UserContext userContext;
        private ILotEventDispatcher lotEventDispatcher;

        public PersonLicenceDocMigrator(
            OracleConnection oracleConn,
            Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, IPrintRepository, ILotEventDispatcher, UserContext>>> dependencyFactory)
        {
            this.dependencyFactory = dependencyFactory;
            this.oracleConn = oracleConn;
        }

        public void MigrateLicenceDocuments(
            ConcurrentQueue<int> personIds,
            Dictionary<int, int> personIdToLotId,
            Dictionary<int, IEnumerable<JObject>> personsLicences,
            CookieCollection cookies,
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
                this.unitOfWork = dependencies.Value.Item1;
                this.lotRepository = dependencies.Value.Item2;
                var printRepository = dependencies.Value.Item3;
                this.lotEventDispatcher = dependencies.Value.Item4;
                this.userContext = dependencies.Value.Item5;

                string connectionString = ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString;

                int personId = -1;
                try
                {
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
                                request.CookieContainer.Add(cookies);

                                using (HttpWebResponse httpResponse = (HttpWebResponse)request.GetResponse())
                                using (var responseStream = httpResponse.GetResponseStream())
                                using (var stream = printRepository.ConvertWordStreamToPdfStream(responseStream))
                                {
                                    var licenceEditionDocBlobKey = printRepository.SaveStreamToBlob(stream, connectionString);
                                    this.UpdateLicenceEdition(licenceEditionDocBlobKey, editionPartIndex, lot);
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

        public void UpdateLicenceEdition(Guid licenceEditionDocBlobKey, int editionPartIndex, Lot lot)
        {
            PartVersion<PersonLicenceEditionDO> licenceEditionPartVersion = lot.Index.GetPart<PersonLicenceEditionDO>(string.Format("licenceEditions/{0}", editionPartIndex));
            
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                licenceEditionPartVersion.Content.PrintedDocumentBlobKey = licenceEditionDocBlobKey;

                GvaFile printedLicenceFile = new GvaFile()
                {
                    Filename = "migratedLicence",
                    FileContentId = licenceEditionDocBlobKey,
                    MimeType = "application/pdf"
                };

                this.unitOfWork.DbContext.Set<GvaFile>().Add(printedLicenceFile);

                this.unitOfWork.Save();

                licenceEditionPartVersion.Content.PrintedFileId = printedLicenceFile.GvaFileId;

                lot.UpdatePart<PersonLicenceEditionDO>(string.Format("licenceEditions/{0}", licenceEditionPartVersion.Part.Index), licenceEditionPartVersion.Content, this.userContext);

                lot.Commit(this.userContext, this.lotEventDispatcher);
                this.lotRepository.ExecSpSetLotPartTokens(licenceEditionPartVersion.PartId);

                this.unitOfWork.Save();

                transaction.Commit();
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
