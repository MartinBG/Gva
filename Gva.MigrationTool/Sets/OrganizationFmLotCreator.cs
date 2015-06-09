using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Autofac.Extras.Attributed;
using Autofac.Features.OwnedInstances;
using Common.Api.Models;
using Common.Api.UserContext;
using Common.Data;
using Common.Json;
using Common.Tests;
using Gva.Api.CommonUtils;
using Gva.Api.ModelsDO;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.MigrationTool.Nomenclatures;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.MigrationTool.Sets
{
    public class OrganizationFmLotCreator : IDisposable
    {
        private bool disposed = false;
        private Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, ICaseTypeRepository, IFileRepository, ILotEventDispatcher, UserContext>>> dependencyFactory;
        private SqlConnection sqlConnGvaAircraft;
        private SqlConnection sqlConnSCodes;

        public OrganizationFmLotCreator(
            [WithKey("gvaAircraft")]SqlConnection sqlConnGvaAircraft,
            [WithKey("sCodes")]SqlConnection sqlConnSCodes,
            Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, ICaseTypeRepository, IFileRepository, ILotEventDispatcher, UserContext>>> dependencyFactory)
        {
            this.dependencyFactory = dependencyFactory;
            this.sqlConnGvaAircraft = sqlConnGvaAircraft;
            this.sqlConnSCodes = sqlConnSCodes;
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
                this.sqlConnGvaAircraft.Open();
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
                        var lotEventDispatcher = dependencies.Value.Item5;
                        var context = dependencies.Value.Item6;

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
                this.sqlConnGvaAircraft.Open();
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
                        var lotEventDispatcher = dependencies.Value.Item5;
                        var context = dependencies.Value.Item6;

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

        public void StartCreatingSModeCodeOperators(
            //input constants
           Dictionary<string, Dictionary<string, NomValue>> noms,
           List<JObject> sModeCodeOperators,
            //output
           ConcurrentDictionary<int, int> orgOperatorIdToLotId,
           ConcurrentDictionary<string, int> orgNamesEnToLotId,
           ConcurrentDictionary<int, JObject> orgLotIdToOrgNom,
            //cancellation
           CancellationTokenSource cts,
           CancellationToken ct)
        {
            try
            {
                this.sqlConnSCodes.Open();
            }
            catch (Exception)
            {
                cts.Cancel();
                throw;
            }

            foreach (JObject oper in sModeCodeOperators)
            {
                ct.ThrowIfCancellationRequested();

                try
                {
                    using (var dependencies = this.dependencyFactory())
                    {
                        var unitOfWork = dependencies.Value.Item1;
                        var lotRepository = dependencies.Value.Item2;
                        var caseTypeRepository = dependencies.Value.Item3;
                        var fileRepository = dependencies.Value.Item4;
                        var lotEventDispatcher = dependencies.Value.Item5;
                        var context = dependencies.Value.Item6;

                        List<NomValue> orgCaseTypes = new List<NomValue>() { 
                            noms["organizationCaseTypes"].ByAlias("others") 
                        };
                        string operName = oper.Get<string>("operatorName");
                        string operNameEN = oper.Get<string>("operatorNameEN");
                        string status = oper.Get<string>("status");
                        int operId = oper.Get<int>("operId");
                        string adrStr = oper.Get<string>("adrStr");
                        string adrCity = oper.Get<string>("adrCity");

                        int lotId;
                        Lot lotIndex;
                        if (operNameEN != null && orgNamesEnToLotId.ContainsKey(operNameEN))
                        {
                            orgOperatorIdToLotId.TryAdd(operId, orgNamesEnToLotId[operNameEN]);
                            lotId = orgNamesEnToLotId[operNameEN];
                        }
                        else if (orgNamesEnToLotId.ContainsKey(operName))
                        {
                            orgOperatorIdToLotId.TryAdd(operId, orgNamesEnToLotId[operName]);
                            lotId = orgNamesEnToLotId[operName];
                        }
                        else
                        { 
                            var lot = lotRepository.CreateLot("Organization");

                            var organizationData = new
                            {
                                caseTypes = orgCaseTypes,
                                name = operName,
                                nameAlt = operName,
                                organizationKind = noms["organizationKinds"].ByCode("0"),
                                organizationType = noms["organizationTypes"].ByCode("N/A"),
                                valid = status == "1" ? noms["boolean"].ByCode("Y") : noms["boolean"].ByCode("N"),
                            };

                            lot.CreatePart("organizationData", organizationData, context);
                            lot.Commit(context, lotEventDispatcher);
                            unitOfWork.Save();
                            Console.WriteLine("Created organizationData part for organization smode code operator with name {0}", operName);

                            lotId = lot.LotId;

                            bool orgLotIdAdded = orgLotIdToOrgNom.TryAdd(lotId, Utils.ToJObject(
                                new
                                {
                                    nomValueId = lotId,
                                    name = operName,
                                    nameAlt = operNameEN ?? operName
                                }));

                            if (!orgLotIdAdded)
                            {
                                throw new Exception(string.Format("lotId {0} already present in orgLotIdToOrgNom dictionary", lotId));
                            }

                            if (!orgOperatorIdToLotId.ContainsKey(operId))
                            {
                                orgOperatorIdToLotId.TryAdd(operId, lotId);
                            }
                        }

                        
                        string post = null;
                        string city = null;
                        if (!string.IsNullOrEmpty(adrCity))
                        {
                            var postCodeAndCity = new Regex(@"^(\D+)(\s)(\d+)$|^(\D+)$|^(\d+)(\s)(\D+)$");
                            Match match = postCodeAndCity.Match(adrCity);
                            if (match.Success)
                            {
                                post = !string.IsNullOrEmpty(match.Groups[3].Value) ? match.Groups[3].Value : match.Groups[5].Value;
                                city = !string.IsNullOrEmpty(match.Groups[1].Value) ? match.Groups[1].Value : 
                                    (!string.IsNullOrEmpty(match.Groups[7].Value) ? match.Groups[7].Value : match.Groups[4].Value);
                            }
                        }

                        var settlement = !string.IsNullOrEmpty(city) ? noms["cities"].Where(v => v.Value.Name == city.Trim()).Select(v => v.Value).FirstOrDefault() : null;
                        

                        lotIndex = lotRepository.GetLotIndex(lotId, fullAccess: true);
                        var partContent = Utils.ToJObject(new
                        {
                            __oldId = operId,
                            __migrTable = "sModeCodeOperatorAddress",
                            addressType = noms["addressTypes"].ByCode("TMP"),
                            valid = noms["boolean"].ByCode("Y"),
                            settlement = settlement,
                            postalCode = post,
                            address = string.Format("{0} {1}", adrStr, adrCity),
                            addressAlt = string.Format("{0} {1}", adrStr, adrCity)
                        });

                        PartVersion partVersion = null;
                        string caseTypeNomAlias = null;
                        if (lotIndex.Set.Alias == "Person")
                        {
                            partVersion  = lotIndex.CreatePart("personAddresses/*", partContent, context);
                            caseTypeNomAlias = "personCaseTypes";
                        }
                        else
                        {
                            partVersion = lotIndex.CreatePart("organizationAddresses/*", partContent, context);
                            caseTypeNomAlias = "organizationCaseTypes";
                        }

                        JArray files = new JArray(){
                            noms[caseTypeNomAlias].Values.Select(c => 
                                Utils.ToJObject(new
                                {
                                    isAdded = true,
                                    file = (object)null,
                                    caseType = Utils.ToJObject(c),
                                    bookPageNumber = (string)null,
                                    pageCount = (int?)null,
                                    applications =  new JArray()
                                }))
                            };
                        fileRepository.AddFileReferences(partVersion.Part, files.Select(f => f.ToObject<CaseDO>()));
                        unitOfWork.Save();
                        try
                        {
                            lotIndex.Commit(context, lotEventDispatcher);
                        }
                        //swallow the Cannot commit without modifications exception
                        catch (InvalidOperationException)
                        {
                        }
                        unitOfWork.Save();

                        Console.WriteLine("Migrated sModeCode operatorId : {0}", operId);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in creation of organization with smode code operator name {0}", oper.Get<string>("operatorName"));
                    cts.Cancel();
                    throw;
                }
            }
        }

        private JObject getOrganizationData(int organizationId, Dictionary<string, Dictionary<string, NomValue>> noms, List<NomValue> caseTypes)
        {
            return this.sqlConnGvaAircraft.CreateStoreCommand(
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

            var lessors = this.sqlConnGvaAircraft.CreateStoreCommand(
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

        public List<JObject> getSModeCodeOperators(
            ConcurrentDictionary<string, int> orgNamesEnToLotId, 
            Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            var orgDictNameBgToNameEn = this.sqlConnGvaAircraft.CreateStoreCommand(
                @"select 
                       distinct tNameBG,
                       tNameEN
                       from Orgs
                       where tNameEN != ''"
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        orgNameBG = r.Field<string>("tNameBG"),
                        orgNameEN = r.Field<string>("tNameEN")
                    }))
                    .GroupBy(s =>  s.Get<string>("orgNameBG"))
                    .ToDictionary(g => 
                        g.Key,
                        g => g.Select(n => n.Get<string>("orgNameEN")).First());

            return this.sqlConnSCodes.CreateStoreCommand(
                @"select 
                       distinct Name,
                        OperID,
                        Status,
                        Adr_Str,
                        Adr_City
                    from Oper
                    where OperID != '0'"
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        operatorName = r.Field<string>("Name"),
                        operatorNameEN = orgDictNameBgToNameEn.ContainsKey(r.Field<string>("Name")) ? orgDictNameBgToNameEn[r.Field<string>("Name")] : null,
                        status = r.Field<string>("Status"),
                        operId = Utils.FmToNum(r.Field<string>("OperID")),
                        adrStr = r.Field<string>("Adr_Str"),
                        adrCity = r.Field<string>("Adr_City")
                    }))
                    .ToList();
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
                    this.sqlConnGvaAircraft.Dispose();
                    this.sqlConnSCodes.Dispose();
                }
            }
            finally
            {
                disposed = true;
            }
        }
    }
}
