using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Autofac.Features.OwnedInstances;
using Common.Api.Models;
using Common.Api.UserContext;
using Common.Data;
using Common.Json;
using Common.Tests;
using Gva.Api.CommonUtils;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.MigrationTool.Nomenclatures;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.MigrationTool.Sets
{
    public class OrganizationLotCreator : IDisposable
    {
        private bool disposed = false;
        private Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, ILotEventDispatcher, ICaseTypeRepository, UserContext>>> dependencyFactory;
        private OracleConnection oracleConn;

        public OrganizationLotCreator(
            OracleConnection oracleConn,
            Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, ILotEventDispatcher, ICaseTypeRepository, UserContext>>> dependencyFactory)
        {
            this.dependencyFactory = dependencyFactory;
            this.oracleConn = oracleConn;
        }

        public void StartCreating(
            //input constants
            Dictionary<string, Dictionary<string, NomValue>> noms,
            //intput
            ConcurrentQueue<int> organizationIds,
            //output
            ConcurrentDictionary<int, int> orgIdToLotId,
            ConcurrentDictionary<string, int> orgNamesEnToLotId,
            ConcurrentDictionary<string, int> orgUinToLotId,
            ConcurrentDictionary<int, JObject> orgLotIdToOrgNom,
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

            int organizationId;
            while (organizationIds.TryDequeue(out organizationId))
            {
                ct.ThrowIfCancellationRequested();

                try
                {
                    using (var dependencies = this.dependencyFactory())
                    {
                        var unitOfWork = dependencies.Value.Item1;
                        var lotRepository = dependencies.Value.Item2;
                        var lotEventDispatcher = dependencies.Value.Item3;
                        var caseTypeRepository = dependencies.Value.Item4;
                        var context = dependencies.Value.Item5;

                        List<NomValue> orgCaseTypes = new List<NomValue>();
                        bool isApprovedOrg = this.getIsApprovedOrg(organizationId);
                        if (isApprovedOrg)
                        {
                            orgCaseTypes.Add(noms["organizationCaseTypes"].ByAlias("approvedOrg"));
                        }
                        orgCaseTypes.Add(noms["organizationCaseTypes"].ByAlias("others"));

                        var organizationData = this.getOrganizationData(organizationId, noms, orgCaseTypes);

                        var lot = lotRepository.CreateLot("Organization");

                        caseTypeRepository.AddCaseTypes(lot, organizationData.GetItems<JObject>("caseTypes").Select(t => t.Get<int>("nomValueId")));

                        lot.CreatePart("organizationData", organizationData, context);
                        lot.Commit(context, lotEventDispatcher);
                        unitOfWork.Save();
                        Console.WriteLine("Created organizationData part for organization with id {0}", organizationId);

                        int lotId = lot.LotId;
                        if (!orgIdToLotId.TryAdd(organizationId, lotId))
                        {
                            throw new Exception(string.Format("organizationId {0} already present in orgIdToLotId dictionary", organizationId));
                        }

                        string name = organizationData.Get<string>("name");
                        string nameAlt = organizationData.Get<string>("nameAlt");

                        if (!orgNamesEnToLotId.TryAdd(nameAlt, lotId))
                        {
                            Console.WriteLine("Duplicated organization nameAlt {0} for orgId {1}", nameAlt, organizationId);
                        }

                        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(nameAlt))
                        {
                            throw new Exception("No name or nameAlt");// all orgs should have nameAlt
                        }

                        string uin = organizationData.Get<string>("uin");
                        if (!string.IsNullOrWhiteSpace(uin))
                        {
                            if (!orgUinToLotId.TryAdd(uin, lotId))
                            {
                                Console.WriteLine("Duplicated organization Uin {0} for orgId {1}", nameAlt, organizationId);
                            }
                        }

                        bool orgLotIdAdded = orgLotIdToOrgNom.TryAdd(lotId, Utils.ToJObject(
                            new
                            {
                                nomValueId = lotId,
                                name = name,
                                nameAlt = nameAlt
                            }));

                        if (!orgLotIdAdded)
                        {
                            throw new Exception(string.Format("lotId {0} already present in orgLotIdToOrgNom dictionary", lotId));
                        }
                    }
                }
                catch (Exception)
                {
                    cts.Cancel();
                    throw;
                }
            }
        }

        private bool getIsApprovedOrg(int organizationId)
        {
            int approvalCount = this.oracleConn.CreateStoreCommand(
                @"SELECT COUNT(*) AP_COUNT FROM CAA_DOC.APPROVAL WHERE {0}",
                new DbClause("ID_FIRM = {0}", organizationId))
                .Materialize(r => r.Field<int>("AP_COUNT"))
                .Single();

            int auditsCount = this.oracleConn.CreateStoreCommand(
                @"SELECT COUNT(*) A_COUNT FROM CAA_DOC.AUDITS WHERE {0}",
                new DbClause("ID_FIRM = {0}", organizationId))
                .Materialize(r => r.Field<int>("A_COUNT"))
                .Single();

            List<NomValue> result = new List<NomValue>();

            return approvalCount > 0 || auditsCount > 0;
        }

        private JObject getOrganizationData(int organizationId, Dictionary<string, Dictionary<string, NomValue>> noms, List<NomValue> caseTypes)
        {
            return this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.FIRM WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID = {0}", organizationId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "FIRM",
                        caseTypes = caseTypes,
                        name = r.Field<string>("NAME"),
                        nameAlt = r.Field<string>("NAME_TRANS"),
                        code = r.Field<string>("CODE"),
                        uin = r.Field<string>("BULSTAT"),
                        cao = r.Field<string>("CAO"),
                        dateCaoFirstIssue = r.Field<DateTime?>("CAO_ISS"),
                        dateCaoLastIssue = r.Field<DateTime?>("CAO_LAST"),
                        dateCaoValidTo = r.Field<DateTime?>("CAO_VALID"),
                        icao = r.Field<string>("ICAO"),
                        iata = r.Field<string>("IATA"),
                        sita = r.Field<string>("SITA"),
                        organizationType = noms["organizationTypes"].ByOldId(r.Field<long?>("ID_FIRM_TYPE").ToString()),
                        organizationKind = noms["organizationKinds"].ByCode(r.Field<string>("TYPE_ORG")),
                        phones = r.Field<string>("PHONES"),
                        webSite = r.Field<string>("WEB_SITE"),
                        notes = r.Field<string>("REMARKS"),
                        valid = noms["boolean"].ByCode(r.Field<string>("VALID_YN") == "Y" ? "Y" : "N"),
                        dateValidTo = r.Field<DateTime?>("VALID_TO_DATE"),
                        docRoom = r.Field<string>("DOC_ROOM"),
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
