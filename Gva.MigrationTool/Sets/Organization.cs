using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using Common.Api.Models;
using Newtonsoft.Json.Linq;
using Gva.Api.CommonUtils;
using Oracle.ManagedDataAccess.Client;

namespace Gva.MigrationTool.Sets
{
    public class Organization
    {
        private OracleConnection oracleConn;
        private Func<Owned<OrganizationLotCreator>> organizationLotCreatorFactory;
        private Func<Owned<OrganizationFmLotCreator>> OrganizationFmLotCreatorFactory;
        private Func<Owned<OrganizationLotMigrator>> organizationLotMigratorFactory;
        

        public Organization(OracleConnection oracleConn,
            Func<Owned<OrganizationLotCreator>> organizationLotCreatorFactory,
            Func<Owned<OrganizationFmLotCreator>> OrganizationFmLotCreatorFactory,
            Func<Owned<OrganizationLotMigrator>> organizationLotMigratorFactory)
        {
            this.oracleConn = oracleConn;
            this.organizationLotCreatorFactory = organizationLotCreatorFactory;
            this.OrganizationFmLotCreatorFactory = OrganizationFmLotCreatorFactory;
            this.organizationLotMigratorFactory = organizationLotMigratorFactory;
        }

        public Tuple<Dictionary<int, int>, ConcurrentDictionary<string, int>, ConcurrentDictionary<string, int>, ConcurrentDictionary<int, JObject>> createOrganizationsLots(Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;

            ConcurrentQueue<int> organizationIds = new ConcurrentQueue<int>(this.getOrganizationIds());

            ConcurrentDictionary<int, int> orgIdToLotId = new ConcurrentDictionary<int, int>();
            ConcurrentDictionary<string, int> orgNamesEnToLotId = new ConcurrentDictionary<string, int>();
            ConcurrentDictionary<string, int> orgUinToLotId = new ConcurrentDictionary<string, int>();
            ConcurrentDictionary<int, JObject> orgLotIdToOrgNom = new ConcurrentDictionary<int, JObject>();

            Utils.RunParallel("ParallelMigrations", ct,
                () => this.organizationLotCreatorFactory().Value,
                (organizationLotCreator) =>
                {
                    using (organizationLotCreator)
                    {
                        organizationLotCreator.StartCreating(noms, organizationIds, orgIdToLotId, orgNamesEnToLotId, orgUinToLotId, orgLotIdToOrgNom, cts, ct);
                    }
                })
                .Wait();

            timer.Stop();
            Console.WriteLine("Organization lot creation time - {0}", timer.Elapsed.TotalMinutes);

            return Tuple.Create(
                orgIdToLotId.ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                orgNamesEnToLotId,
                orgUinToLotId,
                orgLotIdToOrgNom);
        }

        public Tuple<Dictionary<string, int>, Dictionary<string, int>, Dictionary<int, JObject>, Dictionary<int, int>> createOrganizationsFmLots(
            Dictionary<string, Dictionary<string, NomValue>> noms,
            ConcurrentQueue<int> notCreatedFmOrgIds,
            ConcurrentDictionary<string, int> orgNamesEnToLotId,
            ConcurrentDictionary<string, int> orgUinToLotId,
            ConcurrentDictionary<int, JObject> orgLotIdToOrgNom)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;

            Utils.RunParallel("ParallelMigrations", ct,
                () => this.OrganizationFmLotCreatorFactory().Value,
                (organizationFmLotCreator) =>
                {
                    using (organizationFmLotCreator)
                    {
                        organizationFmLotCreator.StartCreating(noms, notCreatedFmOrgIds, orgNamesEnToLotId, orgUinToLotId, orgLotIdToOrgNom, cts, ct);
                    }
                })
                .Wait();

            Console.WriteLine("Organization fm lot creation time - {0}", timer.Elapsed.TotalMinutes);
            timer.Stop();

            Stopwatch timer2 = new Stopwatch();
            timer2.Start();
            List<JObject> notCreatedLessors = this.OrganizationFmLotCreatorFactory().Value.getNotCreatedLessorsNames(orgNamesEnToLotId);
            this.OrganizationFmLotCreatorFactory().Value.StartCreatingMissingLessors(noms, notCreatedLessors, orgNamesEnToLotId, orgLotIdToOrgNom, cts, ct);
            Console.WriteLine("Organization lessors fm lot creation time - {0}", timer2.Elapsed.TotalMinutes);
            timer2.Stop();

            Stopwatch timer3 = new Stopwatch();
            timer3.Start();
            ConcurrentDictionary<int, int> orgOperatorIdToLotId = new ConcurrentDictionary<int, int>();

            List<JObject> sModeCodeOperators = this.OrganizationFmLotCreatorFactory().Value.getSModeCodeOperators(orgNamesEnToLotId, noms);

            this.OrganizationFmLotCreatorFactory().Value.StartCreatingSModeCodeOperators(noms, sModeCodeOperators, orgOperatorIdToLotId, orgNamesEnToLotId, orgLotIdToOrgNom, cts, ct);

            Console.WriteLine("Organization sModeCodes Operators fm lot creation time - {0}", timer3.Elapsed.TotalMinutes);
            timer3.Stop();

            return Tuple.Create(
                orgNamesEnToLotId.ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                orgUinToLotId.ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                orgLotIdToOrgNom.ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                orgOperatorIdToLotId.ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
        }

        public void migrateOrganizations(
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, int> personIdToLotId,
            Dictionary<int, int> orgApexIdToLotId,
            Func<int?, JObject> getAircraftByApexId,
            Func<int?, JObject> getPersonByApexId,
            Dictionary<int, string> blobIdsToFileKeys)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;

            ConcurrentQueue<int> organizationIds = new ConcurrentQueue<int>(this.getOrganizationIds());

            Utils.RunParallel("ParallelMigrations", ct,
                () => this.organizationLotMigratorFactory().Value,
                (organizationLotMigrator) =>
                {
                    using (organizationLotMigrator)
                    {
                        organizationLotMigrator.StartMigrating(noms, personIdToLotId, orgApexIdToLotId, getAircraftByApexId, getPersonByApexId, blobIdsToFileKeys, organizationIds, cts, ct);
                    }
                })
                .Wait();

            timer.Stop();
            Console.WriteLine("Organization migration time - {0}", timer.Elapsed.TotalMinutes);
        }

        private IList<int> getOrganizationIds()
        {
            var ids = this.oracleConn.CreateStoreCommand("SELECT ID FROM CAA_DOC.FIRM")
                .Materialize(r => r.Field<int>("ID"));

            if (Migration.IsPartialMigration)
            {
                ids = ids
                    .Where(id => new int[] { 203, 206, 317, 367, 447, 467, 561, 563, 565, 567, 568, 742, 807, 833, 1432 }.Contains(id));
            }

            return ids.ToList();
        }
    }
}
