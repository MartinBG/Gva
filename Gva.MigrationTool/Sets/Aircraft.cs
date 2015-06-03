using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Autofac.Extras.Attributed;
using Autofac.Features.OwnedInstances;
using Common.Api.Models;
using Gva.Api.CommonUtils;
using Gva.Api.ModelsDO.Aircrafts;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;

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
        private Func<Owned<AircraftRadioCertsMigrator>> aircraftRadioCertsMigratorFactory;

        public Aircraft(
            OracleConnection oracleConn,
            [WithKey("gvaAircraft")]SqlConnection sqlConn,
            Func<Owned<AircraftApexLotCreator>> aircraftApexLotCreatorFactory,
            Func<Owned<AircraftFmLotCreator>> aircraftFmLotCreatorFactory,
            Func<Owned<AircraftApexLotMigrator>> aircraftApexLotMigratorFactory,
            Func<Owned<AircraftFmLotMigrator>> aircraftFmLotMigratorFactory,
            Func<Owned<AircraftRadioCertsMigrator>> aircraftRadioCertsMigratorFactory)
        {
            this.oracleConn = oracleConn;
            this.sqlConn = sqlConn;
            this.aircraftApexLotCreatorFactory = aircraftApexLotCreatorFactory;
            this.aircraftFmLotCreatorFactory = aircraftFmLotCreatorFactory;
            this.aircraftApexLotMigratorFactory = aircraftApexLotMigratorFactory;
            this.aircraftFmLotMigratorFactory = aircraftFmLotMigratorFactory;
            this.aircraftRadioCertsMigratorFactory = aircraftRadioCertsMigratorFactory;
        }

        public Tuple<Dictionary<int, int>, Dictionary<string, int>, Dictionary<int, JObject>> createAircraftsLots(Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;

            ConcurrentDictionary<string, int> fmIdtoLotId = new ConcurrentDictionary<string, int>();
            ConcurrentQueue<string> aircraftFmIds = new ConcurrentQueue<string>(this.getAircraftFmIds());
            ConcurrentDictionary<string, int> MSNtoLotId = new ConcurrentDictionary<string, int>();
            ConcurrentDictionary<int, JObject> aircraftLotIdToAircraftNom = new ConcurrentDictionary<int, JObject>();

            Utils.RunParallel("ParallelMigrations", ct,
                () => this.aircraftFmLotCreatorFactory().Value,
                (aircraftFmLotCreator) =>
                {
                    using (aircraftFmLotCreator)
                    {
                        aircraftFmLotCreator.StartCreating(noms, aircraftFmIds, fmIdtoLotId, MSNtoLotId, aircraftLotIdToAircraftNom, cts, ct);
                    }
                })
                .Wait();

            ConcurrentDictionary<int, int> apexIdtoLotId = new ConcurrentDictionary<int, int>();
            ConcurrentQueue<int> aircraftApexIds = new ConcurrentQueue<int>(this.getAircraftApexIds());
            ConcurrentDictionary<string, int> unusedMSNs =
               new ConcurrentDictionary<string, int>(MSNtoLotId.ToDictionary(kvp => kvp.Key, kvp => 0));//using the keys only, as there is no ConcurrentSet

            Utils.RunParallel("ParallelMigrations", ct,
                () => this.aircraftApexLotCreatorFactory().Value,
                (aircraftApexLotCreator) =>
                {
                    using (aircraftApexLotCreator)
                    {
                        aircraftApexLotCreator.StartCreating(noms, MSNtoLotId.ToDictionary(kvp => kvp.Key, kvp => kvp.Value), aircraftApexIds, unusedMSNs, apexIdtoLotId, cts, ct);
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

        public ConcurrentDictionary<string, int> migrateAircrafts(
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

            this.aircraftFmLotMigratorFactory().Value.MigrateEmptyActNumbers(noms, cts, ct);

            // use the keys in aircraftFmIdtoLotId because some aircrafts have duplicate MSNs and may have been skipped
            ConcurrentQueue<string> aircraftFmIds = new ConcurrentQueue<string>(aircraftFmIdtoLotId.Keys);
            ConcurrentDictionary<string, int> regMarkToLotId  = new ConcurrentDictionary<string, int>();

            Utils.RunParallel("ParallelMigrations", ct,
                () => this.aircraftFmLotMigratorFactory().Value,
                (aircraftFmLotMigrator) =>
                {
                    using (aircraftFmLotMigrator)
                    {
                        aircraftFmLotMigrator.StartMigrating(noms, aircraftFmIdtoLotId, getPersonByApexId, getPersonByFmOrgName, getOrgByFmOrgName, aircraftFmIds, regMarkToLotId, cts, ct);
                    }
                })
                .Wait();

            timer.Stop();
            Console.WriteLine("Aircraft migration time - {0}", timer.Elapsed.TotalMinutes);

            return regMarkToLotId;
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

        public void migrateAircraftRadioCerts(Dictionary<string, Dictionary<string, NomValue>> noms, Dictionary<int, List<AircraftCertRadioDO>> radios)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;

            ConcurrentDictionary<int, List<AircraftCertRadioDO>> radiosByAircraftLotId =
                new ConcurrentDictionary<int, List<AircraftCertRadioDO>>(radios);
            ConcurrentQueue<int> radiosByLotId = new ConcurrentQueue<int>(radios.Keys);

            Utils.RunParallel("ParallelMigrations", ct,
                () => this.aircraftRadioCertsMigratorFactory().Value,
                (aircraftRadioCertsMigrator) =>
                {
                    using (aircraftRadioCertsMigrator)
                    {
                        aircraftRadioCertsMigrator.StartMigrating(noms, radiosByAircraftLotId, radiosByLotId, cts, ct);
                    }
                })
                .Wait();

            timer.Stop();
            Console.WriteLine("Aircraft radio certs migration time - {0}", timer.Elapsed.TotalMinutes);
        }
    }
}
