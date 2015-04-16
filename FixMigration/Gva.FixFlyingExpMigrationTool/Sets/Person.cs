using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using Autofac.Features.OwnedInstances;
using Gva.FixFlyingExpMigrationTool;
using Gva.FixFlyingExpMigrationTool.Sets;
using Oracle.ManagedDataAccess.Client;

namespace Gva.FixFlyingExpMigrationTool.Sets
{
    public class Person
    {
        private OracleConnection oracleConn;
        private Func<Owned<PersonLotFixMigrator>> personLotFixMigratorFactory;

        public Person(
            OracleConnection oracleConn,
            Func<Owned<PersonLotFixMigrator>> personLotFixMigratorFactory)
        {
            this.oracleConn = oracleConn;
            this.personLotFixMigratorFactory = personLotFixMigratorFactory;
        }

        public void fixPersonsFlyingExperiencesMigration()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;

            ConcurrentQueue<int> personLotIds = new ConcurrentQueue<int>(this.personLotFixMigratorFactory().Value.GetPersonsLotIdsWithFlyingExperiences());

            Utils.RunParallel("ParallelMigrations", ct,
                () => this.personLotFixMigratorFactory().Value,
                (personLotMigrator) =>
                {
                    using (personLotMigrator)
                    {
                        personLotMigrator.StartFixOfMigration(personLotIds, cts, ct);
                    }
                })
                .Wait();

            timer.Stop();
            Console.WriteLine("Fix persons flying experience parts time - {0}", timer.Elapsed.TotalMinutes);
        }
    }
}
