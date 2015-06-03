using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using Autofac.Extras.Attributed;
using Autofac.Features.OwnedInstances;
using Common.Api.Models;
using Common.Api.UserContext;
using Common.Data;
using Common.Tests;
using Gva.Api.CommonUtils;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.MigrationTool.Nomenclatures;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.MigrationTool.Sets
{
    public class SModeCodeLotCreator : IDisposable
    {
        private bool disposed = false;
        private Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, ICaseTypeRepository, ILotEventDispatcher, UserContext>>> dependencyFactory;
        private SqlConnection sqlConn;

        public SModeCodeLotCreator(
            [WithKey("sCodes")]SqlConnection sqlConn,
            Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, ICaseTypeRepository, ILotEventDispatcher, UserContext>>> dependencyFactory)
        {
            this.dependencyFactory = dependencyFactory;
            this.sqlConn = sqlConn;
        }

        public void StartCreating(
            //input constants
            Dictionary<string, Dictionary<string, NomValue>> noms,
            //intput
            ConcurrentQueue<string> sModeCodeIds,
            ConcurrentDictionary<string, int> regMarkToLotId,
            Func<int, JObject> getOrgBySModeCodeOperId,
            //cancellation
            CancellationTokenSource cts,
            CancellationToken ct)
        {
            try
            {
                this.sqlConn.Open();
            }
            catch (Exception)
            {
                cts.Cancel();
                throw;
            }

            string sModeCodeId;
            while (sModeCodeIds.TryDequeue(out sModeCodeId))
            {
                ct.ThrowIfCancellationRequested();

                try
                {
                    using (var dependencies = this.dependencyFactory())
                    {
                        var unitOfWork = dependencies.Value.Item1;
                        var lotRepository = dependencies.Value.Item2;
                        var caseTypeRepository = dependencies.Value.Item3;
                        var lotEventDispatcher = dependencies.Value.Item4;
                        var context = dependencies.Value.Item5;

                        var lot = lotRepository.CreateLot("SModeCode");
                        int sModeCodeCaseTypeId = caseTypeRepository.GetCaseTypesForSet("SModeCode").Single().GvaCaseTypeId;
                        caseTypeRepository.AddCaseTypes(lot, new int[] { sModeCodeCaseTypeId });

                        var sModeCodeData = this.getSModeCodeData(sModeCodeId, noms, regMarkToLotId, getOrgBySModeCodeOperId);
                        lot.CreatePart("sModeCodeData", sModeCodeData, context);
                        lot.Commit(context, lotEventDispatcher);

                        unitOfWork.Save();
                        Console.WriteLine("Created sModeCodeData part for sModeCodeId with ident {0}", sModeCodeId);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in creation of smode code with ident {0}", sModeCodeId);
                    cts.Cancel();
                    throw;
                }
            }
        }

        private JObject getSModeCodeData(
            string sModeCodeId,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            ConcurrentDictionary<string, int> regMarkToLotId,
            Func<int, JObject> getOrgBySModeCodeOperId)
        {
            return this.sqlConn.CreateStoreCommand(
                @"select * from Data where 1=1 {0}",
                new DbClause("and Ident = {0}", sModeCodeId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<string>("Ident"),
                        __migrTable = "Data",
                        type = noms["sModeCodeTypes"].ByCode(r.Field<string>("Oper_ID") == "1" ? "M" : "A"),
                        valid = noms["boolean"].ByCode(r.Field<string>("Status") == "1" ? "Y" : "N"),
                        codeHex = r.Field<string>("S_Mode_Hex"),
                        aircraftId = regMarkToLotId.ContainsKey(r.Field<string>("Act_Reg_Mark")) ? regMarkToLotId[r.Field<string>("Act_Reg_Mark")] : (int?)null,
                        theirNumber = r.Field<string>("Oper_S_Letter"),
                        theirDate = Utils.FmToDate(r.Field<string>("Oper_S_Date")),
                        caaNumber = r.Field<string>("CAA_S_Letter"),
                        caaDate = Utils.FmToDate(r.Field<string>("CAA_S_Date")),
                        applicantIsOrg = true,
                        applicantOrganization = getOrgBySModeCodeOperId(Utils.FmToNum(r.Field<string>("Oper_ID")).Value),
                        note = !regMarkToLotId.ContainsKey(r.Field<string>("Act_Reg_Mark")) ? r.Field<string>("Act_Reg_Mark") : null
                    }))
                .Single();
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
                    this.sqlConn.Dispose();
                }
            }
            finally
            {
                disposed = true;
            }
        }
    }
}
