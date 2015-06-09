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
using Newtonsoft.Json.Linq;

namespace Gva.MigrationTool.Sets
{
    public class SModeCode
    {
        private SqlConnection sqlConn;
        private Func<Owned<SModeCodeLotCreator>> sModeCodeLotCreatorFactory;

        public SModeCode(
            [WithKey("sCodes")]SqlConnection sqlConn,
            Func<Owned<SModeCodeLotCreator>> sModeCodeLotCreatorFactory)
        {
            this.sqlConn = sqlConn;
            this.sModeCodeLotCreatorFactory = sModeCodeLotCreatorFactory;
        }

        public void createSModeCodesLots(
            Dictionary<string, Dictionary<string, NomValue>> noms,
            ConcurrentDictionary<string, int> regMarkToLotId,
            Func<int, JObject> getOrgBySModeCodeOperId)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;

            ConcurrentQueue<string> sModeCodeIds = new ConcurrentQueue<string>(this.getSModeCodesIds());

            Utils.RunParallel("ParallelMigrations", ct,
                () => this.sModeCodeLotCreatorFactory().Value,
                (sModeCodeLotCreator) =>
                {
                    using (sModeCodeLotCreator)
                    {
                        sModeCodeLotCreator.StartCreating(noms, sModeCodeIds, regMarkToLotId, getOrgBySModeCodeOperId, cts, ct);
                    }
                })
                .Wait();

            timer.Stop();
            Console.WriteLine("sModeCodes lot creation time - {0}", timer.Elapsed.TotalMinutes);
        }

        private IList<string> getSModeCodesIds()
        {
            var ids = this.sqlConn.CreateStoreCommand("select Ident from Data where Ident not in ('1', '10', '123', '81')")
                .Materialize(r => r.Field<string>("Ident"));

            if (Migration.IsPartialMigration)
            {
                ids = ids.Where(id =>
                        id == "225" ||
                        id == "233");
            }

            return ids.ToList();
        }
    }
}
