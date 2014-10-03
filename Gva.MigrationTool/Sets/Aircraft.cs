using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Features.OwnedInstances;
using Common.Api.Models;
using Newtonsoft.Json.Linq;
using Oracle.DataAccess.Client;

namespace Gva.MigrationTool.Sets
{
    public class Aircraft
    {
        private OracleConnection oracleConn;
        private SqlConnection sqlConn;
        private Func<Owned<AircraftApexLotCreator>> aircraftApexLotCreatorFactory;
        private Func<Owned<AircraftFmLotCreator>> aircraftFmLotCreatorFactory;
        private Func<Owned<AircraftApexLotMigrator>> aircraftApexLotMigratorFactory;
        private Func<Owned<AircraftFmLotMigrator>> aircraftFmLotMigratorFactory;

        public Aircraft(
            OracleConnection oracleConn,
            SqlConnection sqlConn,
            Func<Owned<AircraftApexLotCreator>> aircraftApexLotCreatorFactory,
            Func<Owned<AircraftFmLotCreator>> aircraftFmLotCreatorFactory,
            Func<Owned<AircraftApexLotMigrator>> aircraftApexLotMigratorFactory,
            Func<Owned<AircraftFmLotMigrator>> aircraftFmLotMigratorFactory)
        {
            this.oracleConn = oracleConn;
            this.sqlConn = sqlConn;
            this.aircraftApexLotCreatorFactory = aircraftApexLotCreatorFactory;
            this.aircraftFmLotCreatorFactory = aircraftFmLotCreatorFactory;
            this.aircraftApexLotMigratorFactory = aircraftApexLotMigratorFactory;
            this.aircraftFmLotMigratorFactory = aircraftFmLotMigratorFactory;
        }

        public Tuple<Dictionary<int, int>, Dictionary<string, int>, Dictionary<int, JObject>> createAircraftsLots(Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;

            ConcurrentDictionary<int, int> apexIdtoLotId = new ConcurrentDictionary<int, int>();
            ConcurrentDictionary<string, int> apexMSNtoLotId = new ConcurrentDictionary<string, int>();
            ConcurrentDictionary<string, int> fmIdtoLotId = new ConcurrentDictionary<string, int>();
            ConcurrentDictionary<int, JObject> aircraftLotIdToAircraftNom = new ConcurrentDictionary<int, JObject>();

            ConcurrentQueue<int> aircraftApexIds = new ConcurrentQueue<int>(this.getAircraftApexIds());

            Utils.RunParallel("ParallelMigrations", ct,
                () => this.aircraftApexLotCreatorFactory().Value,
                (aircraftApexLotCreator) =>
                {
                    using (aircraftApexLotCreator)
                    {
                        aircraftApexLotCreator.StartCreating(noms, aircraftApexIds, apexIdtoLotId, apexMSNtoLotId, cts, ct);
                    }
                })
                .Wait();

            ConcurrentQueue<string> aircraftFmIds = new ConcurrentQueue<string>(this.getAircraftFmIds());
            ConcurrentDictionary<string, int> unusedMSNs =
                new ConcurrentDictionary<string, int>(apexMSNtoLotId.ToDictionary(kvp => kvp.Key, kvp => 0));//using the keys only, as there is no ConcurrentSet

            Utils.RunParallel("ParallelMigrations", ct,
                () => this.aircraftFmLotCreatorFactory().Value,
                (aircraftFmLotCreator) =>
                {
                    using (aircraftFmLotCreator)
                    {
                        aircraftFmLotCreator.StartCreating(noms, apexMSNtoLotId.ToDictionary(kvp => kvp.Key, kvp => kvp.Value), aircraftFmIds, unusedMSNs, fmIdtoLotId, aircraftLotIdToAircraftNom, cts, ct);
                    }
                })
                .Wait();

            timer.Stop();
            Console.WriteLine("Aircraft lot creation time - {0}", timer.Elapsed.TotalMinutes);

            return Tuple.Create(
                    apexIdtoLotId.ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                    fmIdtoLotId.ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                    aircraftLotIdToAircraftNom.ToDictionary(kvp => kvp.Key, kvp => kvp.Value));
        }

        public void migrateAircrafts(
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, int> personIdToLotId,
            Dictionary<int, int> aircraftApexIdtoLotId,
            Dictionary<string, int> aircraftFmIdtoLotId,
            Func<int?, JObject> getPersonByApexId,
            Func<int?, JObject> getOrgByApexId,
            Func<string, JObject> getPersonByFmOrgName,
            Func<string, JObject> getOrgByFmOrgName,
            Dictionary<int, string> blobIdsToFileKeys)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;

            ConcurrentQueue<int> aircraftApexIds = new ConcurrentQueue<int>(this.getAircraftApexIds());

            Utils.RunParallel("ParallelMigrations", ct,
                () => this.aircraftApexLotMigratorFactory().Value,
                (aircraftApexLotMigrator) =>
                {
                    using (aircraftApexLotMigrator)
                    {
                        aircraftApexLotMigrator.StartMigrating(noms, personIdToLotId, aircraftApexIdtoLotId, getPersonByApexId, getOrgByApexId, blobIdsToFileKeys, aircraftApexIds, cts, ct);
                    }
                })
                .Wait();

            ConcurrentQueue<string> aircraftFmIds = new ConcurrentQueue<string>(this.getAircraftFmIds());

            Utils.RunParallel("ParallelMigrations", ct,
                () => this.aircraftFmLotMigratorFactory().Value,
                (aircraftFmLotMigrator) =>
                {
                    using (aircraftFmLotMigrator)
                    {
                        aircraftFmLotMigrator.StartMigrating(noms, aircraftFmIdtoLotId, getPersonByApexId, getPersonByFmOrgName, getOrgByFmOrgName, aircraftFmIds, cts, ct);
                    }
                })
                .Wait();

            timer.Stop();
            Console.WriteLine("Aircraft migration time - {0}", timer.Elapsed.TotalMinutes);
        }

        private IList<string> getAircraftFmIds()
        {
            var ids = this.sqlConn.CreateStoreCommand("select n_Act_ID from Acts")
                .Materialize(r => r.Field<string>("n_Act_ID"));

            if (Migration.IsPartialMigration)
            {
                ids = ids
                    .Where(id =>
                        id == "1286" || //LZ-TIM
                        id == "1303" || //LZ-YUP
                        id == "1473"); //LZ-KEC
            }

            return ids.ToList();
        }

        private IList<int> getAircraftApexIds()
        {
            var ids = this.oracleConn.CreateStoreCommand("SELECT ID FROM CAA_DOC.AIRCRAFT")
                .Materialize(r => r.Field<int>("ID"));

            if (Migration.IsPartialMigration)
            {
                ids = ids
                    .Where(id =>
                        id == 1286 || //LZ-TIM
                        id == 1303); //LZ-YUP
            }

            return ids.ToList();
        }
    }
}
