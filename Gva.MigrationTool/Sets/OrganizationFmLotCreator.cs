using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.MigrationTool.Sets
{
    public class OrganizationFmLotCreator : IDisposable
    {
        private bool disposed = false;
        private Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, ICaseTypeRepository, ILotEventDispatcher, UserContext>>> dependencyFactory;
        private SqlConnection sqlConn;

        public OrganizationFmLotCreator(
            SqlConnection sqlConn,
            Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, ICaseTypeRepository, ILotEventDispatcher, UserContext>>> dependencyFactory)
        {
            this.dependencyFactory = dependencyFactory;
            this.sqlConn = sqlConn;
        }

        public void StartCreating(
            //input constants
            Dictionary<string, Dictionary<string, NomValue>> noms,
            //intput
            ConcurrentQueue<int> notCreatedFmOrgIds,
            //output
            ConcurrentDictionary<string, int> orgNamesEnToLotId,
            ConcurrentDictionary<string, int> orgUinToLotId,
            ConcurrentDictionary<int, JObject> orgLotIdToOrgNom,
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

            int organizationFmId;
            while (notCreatedFmOrgIds.TryDequeue(out organizationFmId))
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

                        List<NomValue> orgCaseTypes = new List<NomValue>() { 
                            noms["organizationCaseTypes"].ByAlias("others") 
                        };

                        var organizationData = this.getOrganizationData(organizationFmId, noms, orgCaseTypes);

                        var lot = lotRepository.CreateLot("Organization");

                        caseTypeRepository.AddCaseTypes(lot, organizationData.GetItems<JObject>("caseTypes").Select(t => t.Get<int>("nomValueId")));

                        lot.CreatePart("organizationData", organizationData, context);
                        lot.Commit(context, lotEventDispatcher);
                        unitOfWork.Save();
                        Console.WriteLine("Created organizationData part for organization fm with id {0}", organizationFmId);

                        string name = organizationData.Get<string>("name");
                        string nameAlt = organizationData.Get<string>("nameAlt");

                        int lotId = lot.LotId;
                        if (!orgNamesEnToLotId.TryAdd(nameAlt, lotId))
                        {
                            Console.WriteLine("Duplicated organization nameAlt {0} for fm orgId {1}", nameAlt, organizationFmId);
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
                                Console.WriteLine("Duplicated organization Uin {0} for fm orgId {1}", nameAlt, organizationFmId);
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

        public void StartCreatingMissingLessors(
            //input constants
            Dictionary<string, Dictionary<string, NomValue>> noms,
            List<JObject> notCreatedLessors,
            //output
            ConcurrentDictionary<string, int> orgNamesEnToLotId,
            ConcurrentDictionary<int, JObject> orgLotIdToOrgNom,
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

            foreach (JObject lessor in notCreatedLessors)
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

                        List<NomValue> orgCaseTypes = new List<NomValue>() { 
                            noms["organizationCaseTypes"].ByAlias("others") 
                        };

                        string lessorName = lessor.Get<string>("lessorName");
                        string lessorByNameBG = lessor.Get<string>("lessorByNameBG");
                        string lessorByAddrAndNameBG = lessor.Get<string>("lessorByAddrAndNameBG");
                        string lessorByAddrAndNameEN = lessor.Get<string>("lessorByAddrAndNameEN");

                        string orgName;
                        if(!string.IsNullOrEmpty(lessorByAddrAndNameEN))
                        {
                            orgName = lessorByAddrAndNameEN;
                        }
                        else if(!string.IsNullOrEmpty(lessorByAddrAndNameBG))
                        {
                            orgName = lessorByAddrAndNameBG;
                        }
                        else if(!string.IsNullOrEmpty(lessorByNameBG))
                        {
                            orgName = lessorByNameBG;
                        }
                        else
                        {
                            orgName = lessorName;
                        }

                        if (orgNamesEnToLotId.ContainsKey(orgName))
                        {
                            continue;
                        }

                        var lot = lotRepository.CreateLot("Organization");

                        var organizationData = new
                        {
                            caseTypes = orgCaseTypes,
                            name = orgName,
                            nameAlt = orgName,
                            organizationKind = noms["organizationKinds"].ByCode("0"),
                            organizationType = noms["organizationTypes"].ByCode("N/A"),
                            Valid = noms["boolean"].ByCode("Y")
                        };

                        lot.CreatePart("organizationData", organizationData, context);
                        lot.Commit(context, lotEventDispatcher);
                        unitOfWork.Save();
                        Console.WriteLine("Created organizationData part for organization lessor with name {0}", orgName);

                        int lotId = lot.LotId;

                        bool orgLotIdAdded = orgLotIdToOrgNom.TryAdd(lotId, Utils.ToJObject(
                            new
                            {
                                nomValueId = lotId,
                                name = orgName,
                                nameAlt = orgName
                            }));

                        if (!orgLotIdAdded)
                        {
                            throw new Exception(string.Format("lotId {0} already present in orgLotIdToOrgNom dictionary", lotId));
                        }

                        if (!orgNamesEnToLotId.ContainsKey(orgName))
                        {
                            orgNamesEnToLotId.TryAdd(orgName, lotId);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in creation of organization with name {0}", lessor.Get<string>("lessorName"));
                    cts.Cancel();
                    throw;
                }
            }
        }

        private JObject getOrganizationData(int organizationId, Dictionary<string, Dictionary<string, NomValue>> noms, List<NomValue> caseTypes)
        {
            return this.sqlConn.CreateStoreCommand(
                @"select * from Orgs where {0}",
                new DbClause("nOrgID = {0}", organizationId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("nOrgID"),
                        __migrTable = "Orgs",
                        caseTypes = caseTypes,
                        name = r.Field<string>("tNameBG"),
                        nameAlt = r.Field<string>("tNameEN"),
                        uin = r.Field<string>("t_EIK_EGN"),
                        icao = r.Field<string>("tICAO"),
                        iata = r.Field<string>("tIATA"),
                        sita = r.Field<string>("tSITA"),
                        organizationType = noms["organizationTypes"].ByCode(r.Field<string>("tGrpCode").ToString()),
                        organizationKind = noms["organizationKinds"].ByName(r.Field<string>("t_type_Org")),
                        phones = r.Field<string>("tTopPhone"),
                        notes = r.Field<string>("Remark"),
                        valid = noms["boolean"].ByCode(r.Field<string>("Active") == "1" ? "Y" : "N"),
                        dateValidTo = Utils.FmToDate(r.Field<string>("dCertValid")),
                        docRoom = r.Field<string>("tRoom"),
                    }))
                .Single();
        }

        public List<JObject> getNotCreatedLessorsNames(ConcurrentDictionary<string, int> orgNamesEnToLotId)
        {
            List<JObject> result = new List<JObject>();

            var lessors = this.sqlConn.CreateStoreCommand(
                @"select distinct s.tLessor,
                       lessor.tNameEN as lessorByNameBG,
                       lessorAddressBG.tNameEN as lessorByAddrAndNameBG,
                       lessorAddressEN.tNameEN as lessorByAddrAndNameEN
                    from 
                    (select tLessor from Reg1 as r1
                    union all
                    select tLessor from Reg2 as r2) s
                    left outer join Orgs lessor on lessor.tNameBG = s.tLessor
                    left outer join Orgs lessorAddressBG on s.tLessor like lessorAddressBG.tNameBG + char(10) + lessorAddressBG.tAdrStreetBG + '%'
                    left outer join Orgs lessorAddressEN on s.tLessor like lessorAddressEN.tNameEN + char(10) + lessorAddressEN.tAdrStreetEN + '%'
                    where s.tLessor is not null and s.tLessor != ''"
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        lessorName = r.Field<string>("tLessor").Replace("\n", " "),
                        lessorByNameBG = r.Field<string>("lessorByNameBG"),
                        lessorByAddrAndNameBG = r.Field<string>("lessorByAddrAndNameBG"),
                        lessorByAddrAndNameEN = r.Field<string>("lessorByAddrAndNameEN"),
                    }))
                    .ToList();

            foreach(var lessor in lessors)
            {
                if (orgNamesEnToLotId.ContainsKey(lessor.Get<string>("lessorName")))
                {
                    continue;
                }
                else if(!string.IsNullOrEmpty(lessor.Get<string>("lessorByNameBG")) && orgNamesEnToLotId.ContainsKey(lessor.Get<string>("lessorByNameBG")))
                {
                    continue;
                }
                else if(!string.IsNullOrEmpty(lessor.Get<string>("lessorByAddrAndNameEN")) && orgNamesEnToLotId.ContainsKey(lessor.Get<string>("lessorByAddrAndNameEN")))
                {
                    continue;
                }
                else if (!string.IsNullOrEmpty(lessor.Get<string>("lessorByNameBG")) && orgNamesEnToLotId.ContainsKey(lessor.Get<string>("lessorByNameBG")))
                {
                    continue;
                }
                else
                {
                    result.Add(lessor);
                }
            }

            return result;
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
