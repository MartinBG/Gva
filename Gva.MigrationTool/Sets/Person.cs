using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using Common.Api.Models;
using Newtonsoft.Json.Linq;
using Oracle.DataAccess.Client;

namespace Gva.MigrationTool.Sets
{
    public class Person
    {
        private OracleConnection oracleConn;
        private Func<Owned<PersonLotCreator>> personLotCreatorFactory;
        private Func<Owned<PersonLotMigrator>> personLotMigratorFactory;
        private Func<Owned<PersonLicenceDocMigrator>> personLicenceDocMigratorFactory;

        public Person(OracleConnection oracleConn,
            Func<Owned<PersonLotCreator>> personLotCreatorFactory,
            Func<Owned<PersonLotMigrator>> personLotMigratorFactory,
            Func<Owned<PersonLicenceDocMigrator>> personLicenceDocMigratorFactory)
        {
            this.oracleConn = oracleConn;
            this.personLotCreatorFactory = personLotCreatorFactory;
            this.personLotMigratorFactory = personLotMigratorFactory;
            this.personLicenceDocMigratorFactory = personLicenceDocMigratorFactory;
        }

        public Tuple<Dictionary<int, int>, Dictionary<string, int>, Dictionary<int, JObject>> createPersonsLots(Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;

            ConcurrentQueue<int> personIds = new ConcurrentQueue<int>(this.getPersonIds());

            ConcurrentDictionary<int, int> personIdToLotId = new ConcurrentDictionary<int, int>();
            ConcurrentDictionary<string, int> personEgnToLotId = new ConcurrentDictionary<string, int>();
            ConcurrentDictionary<int, JObject> personLotIdToPersonNom = new ConcurrentDictionary<int, JObject>();

            Utils.RunParallel("ParallelMigrations", ct,
                () => this.personLotCreatorFactory().Value,
                (personLotCreator) =>
                {
                    using (personLotCreator)
                    {
                        personLotCreator.StartCreating(noms, personIds, personIdToLotId, personEgnToLotId, personLotIdToPersonNom, cts, ct);
                    }
                })
                .Wait();

            timer.Stop();
            Console.WriteLine("Person lot creation time - {0}", timer.Elapsed.TotalMinutes);

            return Tuple.Create(
                personIdToLotId.ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                personEgnToLotId.ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                personLotIdToPersonNom.ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
        }

        public void migratePersons(
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, int> personIdToLotId,
            Func<int?, JObject> getAircraftByApexId,
            Func<int?, JObject> getPersonByApexId,
            Func<int?, JObject> getOrgByApexId,
            Dictionary<int, string> blobIdsToFileKeys)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;

            ConcurrentQueue<int> personIds = new ConcurrentQueue<int>(this.getPersonIds());

            Utils.RunParallel("ParallelMigrations", ct,
                () => this.personLotMigratorFactory().Value,
                (personLotMigrator) =>
                {
                    using (personLotMigrator)
                    {
                        personLotMigrator.StartMigrating(noms, personIdToLotId, getAircraftByApexId, getPersonByApexId, getOrgByApexId, blobIdsToFileKeys, personIds, cts, ct);
                    }
                })
                .Wait();

            timer.Stop();
            Console.WriteLine("Person migration time - {0}", timer.Elapsed.TotalMinutes);
        }

        private IList<int> getPersonIds()
        {
            var ids = this.oracleConn.CreateStoreCommand("SELECT ID FROM CAA_DOC.PERSON")
                .Materialize(r => r.Field<int>("ID"))
                .OrderByDescending(r => r)
                .AsEnumerable();

            if (Migration.IsPartialMigration)
            {
                ids = ids
                    //.Take(1000)
                    //.Where(r => r == 6730) //РАДОСТИНА
                    .Where(id => new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 13, 14, 16, 19, 21, 24, 38, 41, 42, 61, 101, 102, 104, 122, 123, 124, 125, 127, 128, 129, 130, 131, 132, 133, 134, 135, 142, 150, 162, 177, 182, 189, 259, 265, 288, 442, 453, 462, 469, 518, 522, 536, 563, 682, 704, 762, 782, 784, 803, 822, 849, 862, 882, 924, 942, 962, 969, 988, 989, 991, 1006, 1013, 1015, 1021, 1026, 1030, 1063, 1070, 1071, 1087, 1104, 1114, 1116, 1117, 1149, 1150, 1153, 1222, 1268, 1302, 1341, 1344, 1405, 1500, 1544, 1546, 1581, 1604, 1605, 1608, 1619, 1678, 1738, 1800, 1828, 1834, 1938, 2007, 2103, 2112, 2139, 2143, 2193, 2219, 2286, 2318, 2373, 2396, 2414, 2417, 2425, 2590, 2606, 2631, 2644, 2654, 2659, 2666, 2683, 2708, 2712, 2713, 2715, 2748, 2783, 2786, 2889, 2894, 2953, 2955, 2961, 3059, 3095, 3100, 3285, 3294, 3372, 3373, 3394, 3458, 3491, 3521, 3569, 3646, 3679, 3816, 3897, 3913, 3933, 3959, 3999, 4015, 4028, 4039, 4100, 4124, 4128, 4162, 4213, 4324, 4535, 5443, 5463, 5464, 5465, 5466, 5467, 5468, 5483, 5603, 5823, 6023, 6203, 6730, 7328, 8086, 9184, 9200 }.Contains(id));
            }

            return ids.ToList();
        }

        public void migrateLicenceDocuments(Dictionary<int, int> personIdToLotId)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;

            Dictionary<int, IEnumerable<JObject>> personLicences = this.getPersonsLicenceDocuments();
            ConcurrentQueue<int> personIds = new ConcurrentQueue<int>(personLicences.Keys.ToArray());

            string loginUri = ConfigurationManager.AppSettings["ServerLoginUri"];
            string userName = ConfigurationManager.AppSettings["ServerUserName"];
            string userPassword = ConfigurationManager.AppSettings["ServerUserPassword"];

            System.Net.CredentialCache credentialCache = new System.Net.CredentialCache();
            credentialCache.Add(
                new System.Uri(loginUri),
                "Basic",
                new System.Net.NetworkCredential(userName, userPassword)
            );
            HttpWebRequest loginRequest = (HttpWebRequest)WebRequest.Create(loginUri);
            HttpWebResponse loginResponse = (HttpWebResponse)loginRequest.GetResponse();
            CookieCollection cookies = loginResponse.Cookies;
            loginResponse.Close();

            Utils.RunParallel("ParallelMigrations", ct,
                () => this.personLicenceDocMigratorFactory().Value,
                (personLicenceDocMigrator) =>
                {
                    using (personLicenceDocMigrator)
                    {
                        personLicenceDocMigrator.MigrateLicenceDocuments(personIds, personIdToLotId, personLicences, cookies, cts, ct);
                    }
                })
                .Wait();
        }

        private Dictionary<int, IEnumerable<JObject>> getPersonsLicenceDocuments()
        {
            string printDocumentUri = ConfigurationManager.AppSettings["ServerPrintDocumentUri"];

            return this.oracleConn.CreateStoreCommand(
                @"select '{0}'
                || (select xml_generator from caa_doc.prt_printable_documents pr_doc
                       where pr_doc.id = lt.prt_printable_document_id*DECODE('LL', 'RN', -1, 1))
                ||'#p_number1='||ll.id||'.#p_printable_documents_id='
                ||lt.prt_printable_document_id*DECODE('LL', 'RN', -1, 1)
                ||'#p_uid='|| 'ri'
                as for_print,
                ll.ID,
                p.ID as PERSON_ID
                 from  caa_doc.CAA CAA,
                       caa_doc.NM_LICENCE_TYPE lt,
                       caa_doc.LICENCE l,
                       caa_doc.licence_log ll,
                       caa_doc.nm_licence_action la,
                       caa_doc.person p
                 where l.LICENCE_TYPE_ID = lt.ID
                   and l.PUBLISHER_CAA_ID = CAA.ID
                   and l.id = ll.licence_id
                   and l.person_id = p.id
                   and ll.licence_action_id = la.id",
                    new DbClause(printDocumentUri))
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
                        new
                        {
                            r.query_string,
                            r.old_id,
                            r.person_id
                        }))));
        }
    }
}
