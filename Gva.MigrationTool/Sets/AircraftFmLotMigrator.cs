using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Autofac.Features.OwnedInstances;
using Common.Api.Models;
using Common.Api.Repositories.UserRepository;
using Common.Api.UserContext;
using Common.Data;
using Common.Json;
using Common.Tests;
using Gva.Api.CommonUtils;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Gva.Api.Repositories.PersonRepository;
using Gva.MigrationTool.Nomenclatures;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.MigrationTool.Sets
{
    public class AircraftFmLotMigrator : IDisposable
    {
        private bool disposed = false;
        private Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, IUserRepository, IFileRepository, IApplicationRepository, IPersonRepository, IOrganizationRepository, ICaseTypeRepository, ILotEventDispatcher, UserContext>>> dependencyFactory;
        private SqlConnection sqlConn;

        public AircraftFmLotMigrator(
            SqlConnection sqlConn,
            Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, IUserRepository, IFileRepository, IApplicationRepository, IPersonRepository, IOrganizationRepository, ICaseTypeRepository, ILotEventDispatcher, UserContext>>> dependencyFactory)
        {
            this.dependencyFactory = dependencyFactory;
            this.sqlConn = sqlConn;
        }

        public void MigrateEmptyActNumbers(
            //input constants
            Dictionary<string, Dictionary<string, NomValue>> noms,
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
            try
            {
                ct.ThrowIfCancellationRequested();

                using (var dependencies = dependencyFactory())
                {
                    var unitOfWork = dependencies.Value.Item1;
                    var invalidActNumberEntries = this.sqlConn.CreateStoreCommand(
                        @"select a.regNumber,
                            (case
                                when a.tRegId LIKE 'II - %' then substring(a.tRegId, 6, 10000)
                                when a.tRegId LIKE 'II-%' then substring(a.tRegId, 4, 10000)
                                else a.tRegId end) as actNumber
                            from
                            (select 1 as regNumber, tRegId from Reg1 where nStatus = '0'
                            union all
                            select 2 as regNumber, tRegId from Reg2 where nStatus = '0') a")
                        .Materialize(r =>
                            new
                            {
                                actNumber = r.Field<int>("actNumber"),
                                registerId = noms["registers"].ByCode(r.Field<string>("regNumber")).NomValueId
                            })
                            .OrderBy(a => a.actNumber)
                             .Select(a => new GvaInvalidActNumber()
                             {
                                 ActNumber = a.actNumber,
                                 RegisterId = a.registerId
                             })
                            .ToList();

                    unitOfWork.DbContext.Set<GvaInvalidActNumber>().AddRange(invalidActNumberEntries);

                    unitOfWork.Save();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error in migration of invalid act number");

                cts.Cancel();
                throw;
            }
        }

        public void StartMigrating(
            //input constants
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<string, int> aircraftFmIdtoLotId,
            Func<int?, JObject> getPersonByApexId,
            Func<string, JObject> getPersonByFmOrgName,
            Func<string, JObject> getOrgByFmOrgName,
            //intput
            ConcurrentQueue<string> aircraftFmIds,
            //output
            ConcurrentDictionary<string, int> regMarkToLotId,
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

            Dictionary<string, int> inspectorsFM = new Dictionary<string, int>()
            {
                {"Стар запис"       , -1},//TODO
                {"А.Борисов"        , 6203},
                {"А.Станков"        , 1149},
                {"Б.Божинов"        , -1},//TODO
                {"В.Василев"        , -1},
                {"В.Драганов"       , 5464},
                {"В.Дяков"          , 3897},
                {"В.Михайлов"       , 2666},
                {"В.Наньов"         , 6023},
                {"В.Пешев"          , 2606},
                {"В.Текнеджиев"     , 2219},
                {"Г.Илчов"          , -1},//TODO
                {"Г.Андонов"        , 4028},
                {"Г.Андонов ARS 02" , 4028},
                {"Е.Крайкова"       , 7328},
                {"И.Банковски"      , 5466},
                {"И.Иванов"         , -1},//TODO
                {"И.Коев"           , 5468},
                {"И.Найденов"       , -1},//TODO
                {"И.С.Иванов"       , 3491},
                {"Ив.Иванов"        , 2590},
                {"Ил.Иванов"        , 1087},
                {"К.Гълъбов"        , 4162},
                {"М.Илов"           , 9184},
                {"М.Митов"          , -1},//TODO
                {"Н. Начев"         , 1150},
                {"Н.Василев"        , 5463},
                {"Н.Джамбов"        , 2286},
                {"Н.Начев"          , 1150},
                {"Н.Тотева"         , 7737},
                {"П.МЛАДЕНОВ"       , -1},//TODO
                {"П.Юнашкова"       , 2349},
                {"С.Живков"         , 2396},
                {"С.Пенчев"         , 803},
                {"С.Тодоров"        , 5465},
                {"Т.Вълков"         , 5467},
                {"Ю.Иванчев"        , -1},
                {"В.Димитров"       , -1}
            };

            string aircraftFmId;
            while (aircraftFmIds.TryDequeue(out aircraftFmId))
            {
                ct.ThrowIfCancellationRequested();

                try
                {
                    using (var dependencies = dependencyFactory())
                    {
                        var unitOfWork = dependencies.Value.Item1;
                        var lotRepository = dependencies.Value.Item2;
                        var userRepository = dependencies.Value.Item3;
                        var fileRepository = dependencies.Value.Item4;
                        var applicationRepository = dependencies.Value.Item5;
                        var personRepository = dependencies.Value.Item6;
                        var organizationRepository = dependencies.Value.Item7;
                        var lotEventDispatcher = dependencies.Value.Item9;
                        var context = dependencies.Value.Item10;

                        var lot = lotRepository.GetLotIndex(aircraftFmIdtoLotId[aircraftFmId], fullAccess: true);

                        Func<string, bool, JObject> getInspectorImpl = (tRegUser, showErrorIfMissing) =>
                        {
                            if (string.IsNullOrWhiteSpace(tRegUser))
                            {
                                return null;
                            }

                            if (!inspectorsFM.ContainsKey(tRegUser))
                            {
                                if (showErrorIfMissing)
                                {
                                    Console.WriteLine("CANNOT FIND INSPECTOR {0}", tRegUser);//TODO
                                }
                                return null;
                            }

                            int personId = inspectorsFM[tRegUser];
                            if (personId == -1)
                            {
                                Console.WriteLine("INSPECTOR {0} IS NOT MAPPED TO PERSONID", tRegUser);//TODO
                                return null;
                            }

                            return getPersonByApexId(personId);
                        };

                        Func<string, JObject> getInspectorOrDefault = (tRegUser) => getInspectorImpl(tRegUser, false);

                        Func<string, JObject> getInspectorOrOther = (tRegUser) =>
                        {
                            JObject inspector = null;
                            string other = null;

                            if (!string.IsNullOrEmpty(tRegUser))
                            {
                                JObject person = getInspectorOrDefault(tRegUser);
                                if (person == null)
                                {
                                    other = tRegUser;
                                }
                                else
                                {
                                    inspector = person;
                                }
                            }

                            return Utils.ToJObject(
                                new
                                {
                                    inspector,
                                    examiner = (JObject)null,
                                    other
                                });
                        };

                        Func<string, JObject, PartVersion> addPartWithFiles = (path, content) =>
                        {
                            var pv = lot.CreatePart(path, content.Get<JObject>("part"), context);
                            fileRepository.AddFileReferences(pv.Part, content.GetItems<CaseDO>("files"));
                            return pv;
                        };

                        if (!aircraftFmIdtoLotId.ContainsKey(aircraftFmId))
                        {
                            //TODO remove, those are the ones with duplicate MSN skipped earlier
                            continue;
                        }

                        var aircraftCertNoiseFM = this.getAircraftCertNoiseFM(aircraftFmId, noms);
                        if (aircraftCertNoiseFM != null)
                        {
                            addPartWithFiles("aircraftCertNoises/*", aircraftCertNoiseFM);
                        }

                        Dictionary<int, Tuple<string, NomValue>> registrations = new Dictionary<int, Tuple<string, NomValue>>();
                        var aircraftCertRegistrationsFM = this.getAircraftCertRegistrationsFM(aircraftFmId, noms, getInspectorOrOther, getPersonByFmOrgName, getOrgByFmOrgName);
                        foreach (var aircraftCertRegistrationFM in aircraftCertRegistrationsFM)
                        {
                            var pv = addPartWithFiles("aircraftCertRegistrationsFM/*", aircraftCertRegistrationFM);
                            int? certNumber = aircraftCertRegistrationFM.Get<int?>("part.certNumber");
                            int? actNumber = aircraftCertRegistrationFM.Get<int?>("part.actNumber");
                            string regMark = aircraftCertRegistrationFM.Get<string>("part.regMark");
                            var registration = new NomValue()
                            {
                                NomValueId = pv.Part.Index,
                                Name = string.Format(
                                    regMark +
                                    (certNumber.HasValue ? string.Format("/рег.№ {0}", certNumber.ToString()) : string.Empty) +
                                    (actNumber.HasValue ? string.Format("/дел.№ {0}", actNumber.ToString()) : string.Empty))
                            };
                            if (!string.IsNullOrEmpty(regMark) && !regMarkToLotId.ContainsKey(regMark))
                            {
                                regMarkToLotId.TryAdd(regMark, lot.LotId);
                            }

                            string registerCode = aircraftCertRegistrationFM.Get<string>("part.register.code");
                            int certId = aircraftCertRegistrationFM.Get<int>("part.__oldId");
                            if (!registrations.ContainsKey(certId))
                            {
                                registrations.Add(certId, new Tuple<string, NomValue>(registerCode, registration));
                            }
                        }

                        var aircraftCertAirworthinessesFM = this.getAircraftCertAirworthinessFM(aircraftFmId, registrations, noms, getInspectorOrOther, getInspectorOrDefault);
                        foreach (var aircraftCertAirworthinessFM in aircraftCertAirworthinessesFM)
                        {
                            addPartWithFiles("aircraftCertAirworthinessesFM/*", aircraftCertAirworthinessFM);
                        }

                        var aircraftDocumentDebtsFM = this.getAircraftDocumentDebtsFM(aircraftFmId, noms, getInspectorOrOther);
                        foreach (var aircraftDocumentDebtFM in aircraftDocumentDebtsFM)
                        {
                            try
                            {
                                addPartWithFiles("aircraftDocumentDebtsFM/*", aircraftDocumentDebtFM);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("ERROR CREATEPART AIRCRAFTDOCUMENTDEBTSFM {0}", e.Message);//TODO
                                throw e;
                            }
                        }

                        try
                        {
                            lot.Commit(context, lotEventDispatcher);
                        }
                        // ignore empty commit exceptions
                        catch (InvalidOperationException)
                        {
                        }

                        unitOfWork.Save();

                        Console.WriteLine("Migrated FM aircraftId: {0}", aircraftFmId);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Error in FM aircraftId: {0}", aircraftFmId);

                    cts.Cancel();
                    throw;
                }
            }
        }

        private IList<JObject> getAircraftCertRegistrationsFM(
            string aircraftId,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Func<string, JObject> getInspectorOrOther,
            Func<string, JObject> getPersonByFmOrgName,
            Func<string, JObject> getOrgByFmOrgName)
        {
            Func<string, Tuple<bool, JObject, JObject>> getOrgOrPerson = (nameEn) =>
            {
                if (string.IsNullOrWhiteSpace(nameEn))
                {
                    return Tuple.Create(true, (JObject)null, (JObject)null);
                }

                JObject org = getOrgByFmOrgName(nameEn);
                if (org != null)
                {
                    return Tuple.Create(true, org, (JObject)null);
                }

                JObject person = getPersonByFmOrgName(nameEn);
                if (person != null)
                {
                    return Tuple.Create(true, (JObject)null, person);
                }

                Console.WriteLine("CANNOT MAP FM ORG {0}", nameEn);//TODO throw
                return Tuple.Create(true, (JObject)null, (JObject)null);
            };

            Regex isOrderStatus = new Regex(@"Заличен съгласно заповед (\w+-\w+-\w+)\s*\/\s*(\d+.\d+.\d+)", RegexOptions.IgnoreCase);
            var registrations = this.sqlConn.CreateStoreCommand(
                @"select s.regNumber,
                        s.nActID,
                        s.nRegNum,
                        s.dRegDate,
                        s.tRegMark,
                        s.tDocCAA,
                        s.dDateCAA,
                        s.tDocOther,
                        s.tRegUser,
                        s.tCatCode,
                        s.nLimitID,
                        s.tR83_Zapoved,
                        s.dR83_Data,
                        s.tLessor,
                        lessor.tNameEN as lessorByNameBG,
                        lessorAddressBG.tNameEN as lessorByAddrAndNameBG,
                        lessorAddressEN.tNameEN as lessorByAddrAndNameEN,
                        s.tLessorAgreement,
                        s.dLeaseDate,
                        s.nStatus,
                        s.tEASA_25,
                        s.dEASA_25,
                        s.dEASA_15,
                        s.dCofR_New,
                        s.dNoise_New,
                        s.tNoise_New,
                        s.tAnnexII_Bg,
                        s.tAnnexII_En,
                        s.dDeRegDate,
                        s.tDeDocOther,
                        s.tRemarkDeReg,
                        s.tDeDocCAA,
                        s.dDeDateCAA,
                        (case
                           when a.t_CofR_No LIKE 'II - %' then substring(a.t_CofR_No, 6, 10000)
                           when a.t_CofR_No LIKE 'II-%' then substring(a.t_CofR_No, 4, 10000)
                           else a.t_CofR_No end) as actRegNum,
                        owner.tNameEN ownerNameEn,
                        oper.tNameEN operNameEn
                    from 
                    (select 1 as regNumber, nActID, nRegNum, dRegDate, tRegMark, tDocCAA, dDateCAA, tDocOther, tRegUser, nOwner,
                            nOper, tCatCode, nLimitID, tR83_Zapoved, dR83_Data, tLessor, tLessorAgreement, dLeaseDate,
                            nStatus, tEASA_25, dEASA_25, dEASA_15, dCofR_New, dNoise_New, tNoise_New, tAnnexII_Bg, tAnnexII_En,
                            dDeRegDate, tDeDocOther, tRemarkDeReg, tDeDocCAA, dDeDateCAA
                    from Reg1 as r1
                        union all
                    select 2 as regNumber, nActID, nRegNum, dRegDate, tRegMark, tDocCAA, dDateCAA, tDocOther, tRegUser, nOwner,
                            nOper, tCatCode, nLimitID, tR83_Zapoved, null as dR83_Data, tLessor, tLessorAgreement, dLeaseDate,
                            nStatus, null as tEASA_25, null as dEASA_25, null as dEASA_15, dCofR_New, dNoise_New, null as tNoise_New, tAnnexII_Bg, tAnnexII_En,
                            dDeRegDate, tDeDocOther, null as tRemarkDeReg, tDeDocCAA, dDeDateCAA
                    from Reg2 as r2) s
                    left outer join Acts a on a.n_Act_ID = s.nActID
                    left outer join Orgs owner on owner.nOrgID = s.nOwner
                    left outer join Orgs oper on oper.nOrgID = s.nOper
                    left outer join Orgs lessor on lessor.tNameBG = s.tLessor
                    left outer join Orgs lessorAddressBG on s.tLessor like lessorAddressBG.tNameBG + char(10) + lessorAddressBG.tAdrStreetBG + '%'
                    left outer join Orgs lessorAddressEN on s.tLessor like lessorAddressEN.tNameEN + char(10) + lessorAddressEN.tAdrStreetEN + '%'
                where {0} and s.nStatus != '0'",
                new DbClause("s.nActID = {0}", aircraftId)
                )
                .Materialize(r =>
                    new
                    {
                        __oldId = r.Field<string>("nRegNum"),
                        __migrTable = "Reg1, Reg2",
                        register = noms["registers"].ByCode(r.Field<int>("regNumber").ToString()),
                        nRegNum = Utils.FmToNum(r.Field<string>("nRegNum")).Value,
                        actRegNum = Utils.FmToNum(r.Field<string>("actRegNum")),
                        certDate = Utils.FmToDate(r.Field<string>("dRegDate")),
                        regMark = r.Field<string>("tRegMark"),
                        incomingDocNumber = !string.IsNullOrEmpty(r.Field<string>("tDeDocCAA")) ? r.Field<string>("tDeDocCAA") : r.Field<string>("tDocCAA"),
                        incomingDocDate = !string.IsNullOrEmpty(r.Field<string>("dDeDateCAA")) ? Utils.FmToDate(r.Field<string>("dDeDateCAA")) : Utils.FmToDate(r.Field<string>("dDateCAA")),
                        incomingDocDesc = !string.IsNullOrEmpty(r.Field<string>("tDeDocOther")) ? r.Field<string>("tDeDocOther") : r.Field<string>("tDocOther"),
                        inspector = getInspectorOrOther(r.Field<string>("tRegUser")),
                        owner = getOrgOrPerson(r.Field<string>("ownerNameEn")),
                        oper = getOrgOrPerson(r.Field<string>("operNameEn")),
                        catAW = noms["aircraftCatAWsFm"].ByCodeOrDefault(r.Field<string>("tCatCode")),//use OrDefault to skip 0 and BLANK codes (empty)
                        aircraftLimitation = noms["aircraftLimitationsFm"].ByCode(r.Field<string>("nLimitID")),
                        leasingDocNumber = r.Field<string>("tR83_Zapoved"),
                        leasingDocDate = Utils.FmToDate(r.Field<string>("dR83_Data")),
                        tLessor = getOrgOrPerson(r.Field<string>("tLessor").Replace("\n", " ")),
                        lessorByNameBG = getOrgOrPerson(r.Field<string>("lessorByNameBG")),
                        lessorByAddrAndNameBG = getOrgOrPerson(r.Field<string>("lessorByAddrAndNameBG")),
                        lessorByAddrAndNameEN = getOrgOrPerson(r.Field<string>("lessorByAddrAndNameEN")),
                        leasingAgreement = r.Field<string>("tLessorAgreement"),
                        leasingEndDate = Utils.FmToDate(r.Field<string>("dLeaseDate")),
                        status = noms["aircraftRegStatsesFm"].ByCode(r.Field<string>("nStatus")),
                        EASA25Number = r.Field<string>("tEASA_25"),
                        EASA25Date = Utils.FmToDate(r.Field<string>("dEASA_25")),
                        EASA15Date = Utils.FmToDate(r.Field<string>("dEASA_15")),
                        cofRDate = Utils.FmToDate(r.Field<string>("dCofR_New")),
                        noiseDate = Utils.FmToDate(r.Field<string>("dNoise_New")),
                        noiseNumber = r.Field<string>("tNoise_New"),
                        paragraph = r.Field<string>("tAnnexII_Bg"),
                        paragraphAlt = r.Field<string>("tAnnexII_En"),
                        removal = new
                        {
                            date = Utils.FmToDate(r.Field<string>("dDeRegDate")),
                            text = (r.Field<string>("tDeDocOther") + "\n\n" + r.Field<string>("tRemarkDeReg")).Trim(),
                            documentNumber = !string.IsNullOrEmpty(r.Field<string>("tDeDocCAA")) ? r.Field<string>("tDeDocCAA") : null,
                            documentDate = Utils.FmToDate(r.Field<string>("dDeDateCAA"))
                        },
                        parsedToIntStatusCode = int.Parse(r.Field<string>("nStatus")),
                        matchedStatus = isOrderStatus.Match(noms["aircraftRegStatsesFm"].ByCode(r.Field<string>("nStatus")).Name)
                    })
                .OrderBy(r => r.certDate)
                .Select(r => new JObject(
                        new JProperty("part",
                           Utils.ToJObject(new
                            {
                                r.__oldId,
                                r.__migrTable,
                                r.register,
                                actNumber = r.nRegNum,
                                certNumber = (!r.actRegNum.HasValue || r.actRegNum.Value >= r.nRegNum) ? r.nRegNum : r.actRegNum,
                                r.certDate,
                                r.regMark,
                                r.incomingDocNumber,
                                r.incomingDocDate,
                                r.incomingDocDesc,
                                r.inspector,
                                ownerIsOrg = r.owner.Item1,
                                ownerOrganization = r.owner.Item2,
                                ownerPerson = r.owner.Item3,
                                operIsOrg = r.oper.Item1,
                                operOrganization = r.oper.Item2,
                                operPerson = r.oper.Item3,
                                catAW = r.catAW != null ? new NomValue[] { r.catAW } : null,
                                r.aircraftLimitation,
                                r.leasingDocNumber,
                                r.leasingDocDate,
                                lessorType = (r.lessorByAddrAndNameEN.Item2 != null || r.lessorByAddrAndNameBG.Item2 != null || r.lessorByNameBG.Item2 != null || r.tLessor.Item2 != null) ? "organization" :
                                    (r.lessorByAddrAndNameEN.Item3 != null || r.lessorByAddrAndNameBG.Item3 != null || r.lessorByNameBG.Item3 != null || r.tLessor.Item3 != null ? "person" : null),
                                lessorOrganization = r.lessorByAddrAndNameEN.Item2 ?? r.lessorByAddrAndNameBG.Item2 ?? r.lessorByNameBG.Item2 ?? r.tLessor.Item2,
                                lessorPerson = r.lessorByAddrAndNameEN.Item3 ?? r.lessorByAddrAndNameBG.Item3 ?? r.lessorByNameBG.Item3 ?? r.tLessor.Item3,
                                r.leasingAgreement,
                                r.leasingEndDate,
                                status = (r.parsedToIntStatusCode >= 6 && r.parsedToIntStatusCode != 21 && r.parsedToIntStatusCode != 10) ? noms["aircraftRegStatsesFm"].ByAlias("removed") :
                                ((r.parsedToIntStatusCode >= 1 && r.parsedToIntStatusCode < 4) ? noms["aircraftRegStatsesFm"].ByAlias("lastActiveReg") : r.status),
                                r.EASA25Number,
                                r.EASA25Date,
                                r.EASA15Date,
                                r.cofRDate,
                                r.noiseDate,
                                r.noiseNumber,
                                r.paragraph,
                                r.paragraphAlt,
                                removal =
                                new 
                                {
                                    date = r.removal.date,
                                    orderNumber = r.parsedToIntStatusCode > 11 && r.parsedToIntStatusCode != 21 && r.matchedStatus.Success ? r.matchedStatus.Groups[1].Value : null,
                                    reason = r.parsedToIntStatusCode > 11 && r.parsedToIntStatusCode != 21 ? noms["aircraftRemovalReasonsFm"].ByAlias("order") : null,
                                    text = r.removal.text,
                                    documentNumber = r.removal.documentNumber,
                                    documentDate = r.removal.documentDate
                                },
                                isActive = false,
                                isCurrent = false
                            })),
                            new JProperty("files",
                                new JArray(
                                    new JObject(
                                        new JProperty("isAdded", true),
                                        new JProperty("file", null),
                                        new JProperty("caseType", Utils.ToJObject(noms["aircraftCaseTypes"].ByAlias("aircraft"))),
                                        new JProperty("bookPageNumber", null),
                                        new JProperty("pageCount", null),
                                        new JProperty("applications", new JArray()))))))
                .ToList();
                        
            var lastReg = registrations.LastOrDefault();
            if (lastReg != null)
            {
                int statusCode = int.Parse(lastReg.Get<string>("part.status.code"));
                string statusAlias = lastReg.Get<string>("part.status.alias");

                switch (statusAlias)
                {
                    case "expiredContract":
                        lastReg["part"]["removal"]["reason"] = Utils.ToJObject(noms["aircraftRemovalReasonsFm"].ByAlias("expiredContract"));
                        break;
                    case "changedOwnership":
                        lastReg["part"]["removal"]["reason"] = Utils.ToJObject(noms["aircraftRemovalReasonsFm"].ByAlias("changedOwnership"));
                        break;
                    case "totaled":
                        lastReg["part"]["removal"]["reason"] = Utils.ToJObject(noms["aircraftRemovalReasonsFm"].ByAlias("totaled"));
                        break;
                }

                lastReg["part"]["isActive"] =  statusCode == 1 || statusCode == 2 ? true : false;
                lastReg["part"]["isCurrent"] = true;
            }

            return registrations;
        }

        private JObject getAircraftCertNoiseFM(
            string aircraftFmId,
            Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            var certNoise = this.sqlConn.CreateStoreCommand(
                @"select * from Acts where {0}",
                new DbClause("n_Act_ID = {0}", aircraftFmId))
                .Materialize(r =>
                    new
                    {
                        __oldId = r.Field<int>("n_Act_ID"),
                        __migrTable = "Acts",
                        issueNumber = r.Field<string>("t_CofN_45_No"),
                        issueDate =  Utils.FmToDate(r.Field<string>("d_CofN_45_Date")),
                        printDate = Utils.FmToDate(r.Field<string>("d_CofN_45_Date_Print")),
                        chapter = r.Field<string>("t_Noise_Chapter"),
                        tcdsn = r.Field<string>("t_Noise_TCDS"),
                        flyover = Utils.FmToDecimal(r.Field<string>("n_Noise_FlyOver")),
                        approach = Utils.FmToDecimal(r.Field<string>("n_Noise_Approach")),
                        lateral = Utils.FmToDecimal(r.Field<string>("n_Noise_Literal")),
                        overflight = Utils.FmToDecimal(r.Field<string>("n_Noise_OverFlight")),
                        takeoff = Utils.FmToDecimal(r.Field<string>("n_Noise_TakeOff")),
                        additionalModifications = r.Field<string>("t_Noise_AddModifBg"),
                        additionalModificationsAlt = r.Field<string>("t_Noise_AddModifEn"),
                        remarks = r.Field<string>("t_Noise_Remark")
                    })
                .Single();

            bool isAnyPropNotEmpty = certNoise.GetType().GetProperties()
                .Where(p => p.Name != "__migrTable" && p.Name != "__oldId")
                .Any(p => !string.IsNullOrEmpty((p.GetValue(certNoise) as string)));

            if(!isAnyPropNotEmpty)
            {
                return null;
            }
            else
            {
                return new JObject(
                    new JProperty("part", Utils.ToJObject(certNoise)),
                    new JProperty("files",
                        new JArray(
                            new JObject(
                                new JProperty("isAdded", true),
                                new JProperty("file", null),
                                new JProperty("caseType", Utils.ToJObject(noms["aircraftCaseTypes"].ByAlias("aircraft"))),
                                new JProperty("bookPageNumber", null),
                                new JProperty("pageCount", null),
                                new JProperty("applications", new JArray())))));
            }
        }

        private List<JObject> getAircraftCertAirworthinessFM(
            string aircraftFmId,
            Dictionary<int, Tuple<string, NomValue>> registrations,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Func<string, JObject> getInspectorOrOther,
            Func<string, JObject> getInspectorOrDefault)
        {
            Dictionary<string, string> actMap = new Dictionary<string, string>
            {
                { "\"BG Form\""     , "directive8" },
                { "\"Tech Cert\""   , "vla" },
                { "BG Form"         , "directive8" },
                { "EASA"            , "f25" },
                { "EASA 24"         , "f24" },
                { "EASA 25"         , "f25" },
                { "EXP"             , "special" },
                { "Old BG Form"     , "directive8" },
                { "Tech Cert"       , "vla" }
            };

            Dictionary<string, string> issuesActMap = new Dictionary<string, string>
            {
                {"ARC", "unknown"},
                {"Permit to Flight", "unknown"},
                {"Experimental", "special"},
                {"Special - Experimental", "special"},
                {"Form 15a", "15a"},
                {"Form 15a from EU Member", "15a"},
                {"Form 15a Reissue", "15aReissue"},
                {"Form 15a Заверка", "15aReissue"},
                {"Form 15b", "15b"},
                {"Form 15b Заверка", "15bReissue"},
                {"Old BG Form", "directive8"},
                {"Old BG Form - Заверка", "directive8Reissue"},
                {"Tech Cert", "vla"},
                {"Tech Cert - Заверка", "vlaReissue"}
            };

            Func<string, JObject> getExaminerOrOther = (reviewedBy) =>
            {
                JObject examiner = null;
                string other = null;

                if (!string.IsNullOrEmpty(reviewedBy))
                {
                    JObject reviewedByExaminer = getInspectorOrDefault(reviewedBy);
                    if (reviewedByExaminer == null)
                    {
                        other = reviewedBy;
                    }
                    else
                    {
                        examiner = reviewedByExaminer;
                    }
                }

                return Utils.ToJObject(
                    new
                    {
                        inspector = (JObject)null,
                        examiner,
                        other,
                    });
            };

            var act = this.sqlConn.CreateStoreCommand(
                @"select * from Acts where {0}",
                    new DbClause("n_Act_ID = {0}", aircraftFmId)
                )
                .Materialize(r =>
                    new
                    {
                        t_CofA_Type = r.Field<string>("t_CofA_Type"),
                        t_CofA_No = r.Field<string>("t_CofA_No"),
                        d_CofA_Date = Utils.FmToDate(r.Field<string>("d_CofA_Date")),
                    })
                .Single();

            var issues = this.sqlConn.CreateStoreCommand(
                @"select * from 
                    (select nActId, nRegNum, nRegNumActive, nStatus, Reg_ID, NumberIssue, t_ARC_Type, dDateEASA_25_Issue, d_24_Issue, dIssue, dFrom, dValid, t_Reviewed_by, t_ARC_RefNo from CofA1 as r1
                        union all
                    select nActId, nRegNum, nRegNumActive, nStatus, Reg_ID, NumberIssue, t_ARC_Type, dDateEASA_25_Issue, d_24_Issue, dIssue, dFrom, dValid, t_Reviewed_by, t_ARC_RefNo from CofA2 as r2) s
                where {0} and not(s.dIssue = '' and s.dFrom = '' and s.dValid = '') and s.nStatus <> 0
                order by CAST(s.nRegNum as int), CAST(s.nRegNumActive as int) desc,
                         CAST(case
	                        when s.REG_ID LIKE 'II - %' then substring(s.REG_ID, 6, 10000)
	                        when s.REG_ID LIKE 'II-%' then substring(s.REG_ID, 4, 10000)
	                        else s.REG_ID end as int),
                        s.NumberIssue",
                new DbClause("s.nActId = {0}", aircraftFmId)
                )
                .Materialize(r =>
                    new
                    {
                        __oldId = r.Field<string>("nRegNum"),
                        __migrTable = "CofA1, CofA2",
                        Reg_ID = r.Field<string>("Reg_ID"),
                        certId = r.Field<string>("nRegNum"),
                        t_ARC_Type = r.Field<string>("t_ARC_Type"),
                        dDateEASA_25_Issue = Utils.FmToDate(r.Field<string>("dDateEASA_25_Issue")),
                        d_24_Issue = Utils.FmToDate(r.Field<string>("d_24_Issue")),

                        dIssue = Utils.FmToDate(r.Field<string>("dIssue")),
                        dFrom = Utils.FmToDate(r.Field<string>("dFrom")),
                        dValid = Utils.FmToDate(r.Field<string>("dValid")),

                        t_Reviewed_by = r.Field<string>("t_Reviewed_by")
                    })
                .ToList();

            if (issues.Count == 0)
            {
                return new List<JObject>();
            }

            var lastIssue = issues.Last();
            string actAlias = null;
            if (actMap.ContainsKey(act.t_CofA_Type))
            {
                actAlias = actMap[act.t_CofA_Type];
            }

            if (string.IsNullOrEmpty(actAlias))
            {
                if (lastIssue.d_24_Issue != null)
                {
                    actAlias = "f24";
                }
                else if (lastIssue.dDateEASA_25_Issue != null)
                {
                    actAlias = "f25";
                }
                else if (lastIssue.dValid.HasValue && DateTime.Compare(lastIssue.dValid.Value, new DateTime(2008, 7, 18)) < 0)
                {
                    if (registrations.Any(r => r.Value.Item1 == "2"))
                    {
                        actAlias = "vla";
                    }
                    else
                    {
                        actAlias = "directive8";
                    }
                }
                else
                {
                    actAlias = "unknown";
                }
            }

            NomValue certType;
            DateTime? issueDate;
            switch (actAlias)
            {
                case "f24":
                    issueDate = act.d_CofA_Date ?? lastIssue.d_24_Issue ?? lastIssue.dIssue ?? lastIssue.dFrom;
                    break;
                case "f25":
                    issueDate = act.d_CofA_Date ?? lastIssue.dDateEASA_25_Issue ?? lastIssue.dIssue ?? lastIssue.dFrom;
                    break;
                case "directive8":
                case "special":
                case "vla":
                case "unknown":
                    issueDate = act.d_CofA_Date ?? lastIssue.dIssue ?? lastIssue.dFrom;
                    break;
                default:
                    throw new Exception("Unexpected ACT alias");
            }

            List<JObject> airworthinesses = new List<JObject>();

            certType = noms["airworthinessCertificateTypes"].ByAlias(actAlias);
            int certId = int.Parse(lastIssue.certId);
            JObject registration = registrations.ContainsKey(certId) ? Utils.ToJObject(registrations[certId].Item2) : null;

            if (actAlias != "f24" && actAlias != "f25")
            {
                foreach (var issue in issues)
                {
                    string type = issuesActMap.ContainsKey(issue.t_ARC_Type) ? issuesActMap[issue.t_ARC_Type] : "unknown";
                    JObject airworthinessCertType = Utils.ToJObject(noms["airworthinessCertificateTypes"].ByAlias(type));

                    airworthinesses.Add(
                        Utils.ToJObject(new
                        {
                            airworthinessCertificateType =  airworthinessCertType,
                            registration = registration,
                            documentNumber = act.t_CofA_No,
                            issueDate = issue.dIssue,
                            validFromDate = issue.dFrom,
                            validToDate = issue.dValid,
                            inspector = getInspectorOrOther(issue.t_Reviewed_by)
                        }));
                }
            }
            else
            {
                var aw = Utils.ToJObject(new
                {
                    airworthinessCertificateType = certType,
                    registration = registration,
                    documentNumber = act.t_CofA_No,
                    issueDate = issueDate
                });

                airworthinesses.Add(aw);

                var formIssues = issues.Where(i => i.t_ARC_Type == "" || i.t_ARC_Type.Contains("BG Form")).ToList();
                if (formIssues.Count() > 0)
                {
                    var reserveType = !formIssues.First().dValid.HasValue || DateTime.Compare(formIssues.First().dValid.Value, new DateTime(2008, 7, 18)) < 0 ? "directive8" : "unknown";
                    foreach (var issue in formIssues)
                    {
                        string type = issuesActMap.ContainsKey(issue.t_ARC_Type) ? issuesActMap[issue.t_ARC_Type] : reserveType;

                        airworthinesses.Add(Utils.ToJObject(new
                        {
                            airworthinessCertificateType = Utils.ToJObject(noms["airworthinessCertificateTypes"].ByAlias(type)),
                            registration = registration,
                            documentNumber = act.t_CofA_No,
                            issueDate = issue.dIssue,
                            validFromDate = issue.dFrom,
                            validToDate = issue.dValid,
                            inspector = getExaminerOrOther(issue.t_Reviewed_by)
                        }));
                    }
                }

                foreach (var issue in issues.Where(i => !i.t_ARC_Type.Contains("BG Form") && i.t_ARC_Type != ""))
                {
                    string type = issuesActMap.ContainsKey(issue.t_ARC_Type) ? issuesActMap[issue.t_ARC_Type] : "unknown";
                    JObject airworthinessCertType = Utils.ToJObject(noms["airworthinessCertificateTypes"].ByAlias(type));

                    airworthinesses.Add(
                        Utils.ToJObject(new
                        {
                            airworthinessCertificateType = airworthinessCertType,
                            registration = registration,
                            documentNumber = act.t_CofA_No,
                            issueDate = issue.dIssue,
                            validFromDate = issue.dFrom,
                            validToDate = issue.dValid,
                            inspector = getExaminerOrOther(issue.t_Reviewed_by)
                        }));
                }
            }

            List<JObject> airworthinessResults = new List<JObject>();
            foreach (var airworthiness in airworthinesses)
            { 
                var result = new JObject(
                    new JProperty("part", airworthiness),
                    new JProperty("files",
                            new JArray(
                                new JObject(
                                    new JProperty("isAdded", true),
                                    new JProperty("file", null),
                                    new JProperty("caseType", Utils.ToJObject(noms["aircraftCaseTypes"].ByAlias("aircraft"))),
                                    new JProperty("bookPageNumber", null),
                                    new JProperty("pageCount", null),
                                    new JProperty("applications", new JArray())))));

                airworthinessResults.Add(result);
            }

            return airworthinessResults;
        }

        private IList<JObject> getAircraftDocumentDebtsFM(
            string aircraftFmId,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Func<string, JObject> getInspectorOrOther)
        {
            return this.sqlConn.CreateStoreCommand(
                @"SELECT n_Mort_ID,
                    d_Open_Date,
                    t_Type,
                    t_Open_CAA_Doc,
                    t_Open_Notes,
                    d_Open_CAA_Date,
                    t_Creditor_Name,
                    d_Creditor_Date,
                    t_Creditor_Doc,
                    t_Open_User,
                    n_Status,
                    t_Close_User,
                    d_Close_Date,
                    t_Close_CAA_Doc,
                    d_Close_CAA_Date,
                    t_Close_Creditor_Doc,
                    d_Close_Creditor_Date,
                    t_Close_Notes
                    FROM Morts_New WHERE {0}",
                new DbClause("n_Acts_ID = {0}", aircraftFmId)
                )
                .Materialize(r => new JObject(
                    new JProperty("part",
                        Utils.ToJObject(new
                        {
                            __oldId = r.Field<string>("n_Mort_ID"),
                            __migrTable = "Morts_New",
                            regDate = Utils.FmToDate(r.Field<string>("d_Open_Date")),
                            aircraftDebtType = r.Field<string>("t_Type").Contains("ЗАЛО") ? noms["aircraftDebtTypesFm"].ByName("ЗАЛОГ") :
                                (r.Field<string>("t_Type").Contains("ЗАПОР") ? noms["aircraftDebtTypesFm"].ByName("ЗАПОР") : null),
                            documentNumber = r.Field<string>("t_Open_CAA_Doc"),
                            documentDate = Utils.FmToDate(r.Field<string>("d_Open_CAA_Date")),
                            aircraftApplicant = noms["aircraftCreditorsFm"].ByName(r.Field<string>("t_Creditor_Name")),
                            notes = r.Field<string>("t_Open_Notes"),
                            inspector = getInspectorOrOther(r.Field<string>("t_Open_User")),
                            isActive = r.Field<string>("n_Status") != "0",
                            theirDate = Utils.FmToDate(r.Field<string>("d_Creditor_Date")),
                            theirNumber = r.Field<string>("t_Creditor_Doc"),
                            close = new {
                                inspector = getInspectorOrOther(r.Field<string>("t_Close_User")),
                                date = Utils.FmToDate(r.Field<string>("d_Close_Date")),
                                caaDoc = r.Field<string>("t_Close_CAA_Doc"),
                                caaDate = Utils.FmToDate(r.Field<string>("d_Close_CAA_Date")),
                                theirNumber = r.Field<string>("t_Close_Creditor_Doc"),
                                theirDate = Utils.FmToDate(r.Field<string>("d_Close_Creditor_Date")),
                                notes = r.Field<string>("t_Close_Notes")
                            }
                        })),
                        new JProperty("files",
                            new JArray(
                                new JObject(
                                    new JProperty("isAdded", true),
                                    new JProperty("file", null),
                                    new JProperty("caseType", Utils.ToJObject(noms["aircraftCaseTypes"].ByAlias("aircraft"))),
                                    new JProperty("bookPageNumber", null),
                                    new JProperty("pageCount", null),
                                    new JProperty("applications", new JArray()))))))
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
