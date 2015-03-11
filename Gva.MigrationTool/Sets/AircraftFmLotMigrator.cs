using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using Autofac.Features.OwnedInstances;
using Common.Api.Models;
using Common.Api.Repositories.UserRepository;
using Common.Api.UserContext;
using Common.Data;
using Common.Json;
using Common.Tests;
using Gva.Api.CommonUtils;
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

        public void StartMigrating(
            //input constants
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<string, int> aircraftFmIdtoLotId,
            Func<int?, JObject> getPersonByApexId,
            Func<string, JObject> getPersonByFmOrgName,
            Func<string, JObject> getOrgByFmOrgName,
            //intput
            ConcurrentQueue<string> aircraftFmIds,
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
                {"Н.Тотева"         , -1},//TODO
                {"П.МЛАДЕНОВ"       , -1},//TODO
                {"П.Юнашкова"       , 2349},
                {"С.Живков"         , 2396},
                {"С.Пенчев"         , 803},
                {"С.Тодоров"        , 5465},
                {"Т.Вълков"         , 5467},
                {"Ю.Иванчев"        , -1}
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

                        Func<string, JObject> getInspector = (tRegUser) => getInspectorImpl(tRegUser, true);

                        Func<string, JObject> getInspectorOrDefault = (tRegUser) => getInspectorImpl(tRegUser, false);

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

                        var aircraftCertRegistrationsFM = this.getAircraftCertRegistrationsFM(aircraftFmId, noms, getInspector, getPersonByFmOrgName, getOrgByFmOrgName);
                        foreach (var aircraftCertRegistrationFM in aircraftCertRegistrationsFM)
                        {
                            var pv = addPartWithFiles("aircraftCertRegistrationsFM/*", aircraftCertRegistrationFM);
                            int? certNumber = aircraftCertRegistrationFM.Get<int?>("part.certNumber");
                            int? actNumber = aircraftCertRegistrationFM.Get<int?>("part.actNumber");
                            var registration = new NomValue()
                            {
                                NomValueId = pv.Part.Index,
                                Name = string.Format(
                                    aircraftCertRegistrationFM.Get<string>("part.regMark") +
                                    (certNumber.HasValue ? string.Format("/рег.№ {0}", certNumber.ToString()) : string.Empty) +
                                    (actNumber.HasValue ? string.Format("/дел.№ {0}", actNumber.ToString()) : string.Empty))
                            };

                            int certId = aircraftCertRegistrationFM.Get<int>("part.__oldId");

                            var aircraftCertAirworthinessFM = this.getAircraftCertAirworthinessFM(aircraftFmId, certId, noms, registration, getInspector, getInspectorOrDefault);
                            if (aircraftCertAirworthinessFM != null)
                            {
                                addPartWithFiles("aircraftCertAirworthinessesFM/*", aircraftCertAirworthinessFM);
                            }
                        }

                        var aircraftDocumentDebtsFM = this.getAircraftDocumentDebtsFM(aircraftFmId, noms, getInspector);
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
            Func<string, JObject> getInspector,
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
                        s.tDeUser,
                        a.n_RegNum as actRegNum,
                        owner.tNameEN ownerNameEn,
                        oper.tNameEN operNameEn
                    from 
                    (select 1 as regNumber, nActID, nRegNum, dRegDate, tRegMark, tDocCAA, dDateCAA, tDocOther, tRegUser, nOwner,
                            nOper, tCatCode, nLimitID, tR83_Zapoved, dR83_Data, tLessor, tLessorAgreement, dLeaseDate,
                            nStatus, tEASA_25, dEASA_25, dEASA_15, dCofR_New, dNoise_New, tNoise_New, tAnnexII_Bg, tAnnexII_En,
                            dDeRegDate, tDeDocOther, tRemarkDeReg, tDeDocCAA, dDeDateCAA, tDeUser
                    from Reg1 as r1
                        union all
                    select 2 as regNumber, nActID, nRegNum, dRegDate, tRegMark, tDocCAA, dDateCAA, tDocOther, tRegUser, nOwner,
                            nOper, tCatCode, nLimitID, tR83_Zapoved, null as dR83_Data, tLessor, tLessorAgreement, dLeaseDate,
                            nStatus, null as tEASA_25, null as dEASA_25, null as dEASA_15, dCofR_New, dNoise_New, null as tNoise_New, tAnnexII_Bg, tAnnexII_En,
                            dDeRegDate, tDeDocOther, null as tRemarkDeReg, tDeDocCAA, dDeDateCAA, tDeUser
                    from Reg2 as r2) s
                    left outer join Acts a on a.n_Act_ID = s.nActID
                    left outer join Orgs owner on owner.nOrgID = s.nOwner
                    left outer join Orgs oper on oper.nOrgID = s.nOper
                where {0}",
                new DbClause("s.nActID = {0}", aircraftId)
                )
                .Materialize(r =>
                    new
                    {
                        __oldId = r.Field<string>("nRegNum"),
                        __migrTable = "Reg1, Reg2",
                        register = noms["registers"].ByCode(r.Field<int>("regNumber").ToString()),
                        nRegNum = Utils.FmToNum(r.Field<string>("nRegNum")).Value,
                        actRegNum = Utils.FmToNum(r.Field<string>("actRegNum")).Value,
                        certDate = Utils.FmToDate(r.Field<string>("dRegDate")),
                        regMark = r.Field<string>("tRegMark"),
                        incomingDocNumber = r.Field<string>("tDocCAA"),
                        incomingDocDate = Utils.FmToDate(r.Field<string>("dDateCAA")),
                        incomingDocDesc = r.Field<string>("tDocOther"),
                        inspector = getInspector(r.Field<string>("tRegUser")),
                        owner = getOrgOrPerson(r.Field<string>("ownerNameEn")),
                        oper = getOrgOrPerson(r.Field<string>("operNameEn")),
                        catAW = noms["aircraftCatAWsFm"].ByCodeOrDefault(r.Field<string>("tCatCode")),//use OrDefault to skip 0 and BLANK codes (empty)
                        aircraftLimitation = noms["aircraftLimitationsFm"].ByCode(r.Field<string>("nLimitID")),
                        leasingDocNumber = r.Field<string>("tR83_Zapoved"),
                        leasingDocDate = Utils.FmToDate(r.Field<string>("dR83_Data")),
                        leasingLessor = getOrgOrPerson(r.Field<string>("tLessor")),//TODO
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
                            reason = noms["aircraftRemovalReasonsFm"].ByAlias("migration"),
                            text = (r.Field<string>("tDeDocOther") + "\n\n" + r.Field<string>("tRemarkDeReg")).Trim(),
                            documentNumber = r.Field<string>("tDeDocCAA"),
                            documentDate = Utils.FmToDate(r.Field<string>("dDeDateCAA")),
                            inspector = getInspector(r.Field<string>("tDeUser"))
                        },
                        parsedToIntStatusCode = int.Parse(r.Field<string>("nStatus")),
                        splitStatus = noms["aircraftRegStatsesFm"].ByCode(r.Field<string>("nStatus")).Name.Split(new char[]{' '})
                    })
                .OrderBy(r => r.nRegNum)
                .Select(r => new JObject(
                        new JProperty("part",
                           Utils.ToJObject(new
                            {
                                r.__oldId,
                                r.__migrTable,
                                r.register,
                                actNumber = r.nRegNum,
                                certNumber = r.actRegNum >= r.nRegNum ? r.nRegNum : r.actRegNum,
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
                                lessorIsOrg = r.leasingLessor.Item1,
                                lessorOrganization = r.leasingLessor.Item2,
                                lessorPerson = r.leasingLessor.Item3,
                                r.leasingAgreement,
                                r.leasingEndDate,
                                status = r.parsedToIntStatusCode > 11 && r.parsedToIntStatusCode != 21 ? noms["aircraftRegStatsesFm"].ByAlias("removedByOrder") : r.status,
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
                                    orderNumber = r.parsedToIntStatusCode > 11 && r.parsedToIntStatusCode != 21 ? r.splitStatus.Skip(3).Take(1).Single() : "",
                                    reason = r.parsedToIntStatusCode > 11 && r.parsedToIntStatusCode != 21 ? noms["aircraftRemovalReasonsFm"].ByAlias("order") : r.removal.reason,
                                    text = r.removal.text,
                                    documentNumber = r.removal.documentNumber,
                                    documentDate = r.removal.documentDate,
                                    inspector = r.removal.inspector
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

                lastReg["part"]["isActive"] =  statusCode < 4 ? true : false;
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

                return new JObject(
                    new JProperty("part",
                        Utils.ToJObject(certNoise)),
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

        private JObject getAircraftCertAirworthinessFM(
            string aircraftFmId,
            int certId,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            NomValue registration,
            Func<string, JObject> getInspector,
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
                        t_CofA_No = r.Field<string>("t_CofA_No")
                    })
                .Single();

            var issues = this.sqlConn.CreateStoreCommand(
                @"select * from 
                    (select nRegNum, NumberIssue, t_ARC_Type, dDateEASA_25_Issue, d_24_Issue, dIssue, dFrom, dValid, t_CAA_Inspetor as t_CAA_Inspector, t_Reviewed_By, t_ARC_RefNo from CofA1 as r1
                        union all
                    select nRegNum, NumberIssue, t_ARC_Type, dDateEASA_25_Issue, d_24_Issue, dIssue, dFrom, dValid, t_CAA_Inspector, t_Reviewed_By, t_ARC_RefNo from CofA2 as r2) s
                where {0}
                order by NumberIssue",
                new DbClause("nRegNum = {0}", certId)
                )
                .Materialize(r =>
                    new
                    {
                        __oldId = r.Field<string>("nRegNum"),
                        __migrTable = "CofA1, CofA2",

                        t_ARC_Type = r.Field<string>("t_ARC_Type"),

                        dDateEASA_25_Issue = Utils.FmToDate(r.Field<string>("dDateEASA_25_Issue")),
                        d_24_Issue = Utils.FmToDate(r.Field<string>("d_24_Issue")),

                        dIssue = Utils.FmToDate(r.Field<string>("dIssue")),
                        dFrom = Utils.FmToDate(r.Field<string>("dFrom")),
                        dValid = Utils.FmToDate(r.Field<string>("dValid")),

                        t_CAA_Inspetor = r.Field<string>("t_CAA_Inspector"),
                        t_Reviewed_By = r.Field<string>("t_Reviewed_By")
                    })
                .ToList();

            if (issues.Count == 0)
            {
                return null;
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
                    issueDate = lastIssue.d_24_Issue ?? lastIssue.dIssue ?? lastIssue.dFrom;
                    break;
                case "f25":
                    issueDate = lastIssue.dDateEASA_25_Issue ?? lastIssue.dIssue ?? lastIssue.dFrom;
                    break;
                case "directive8":
                case "special":
                case "vla":
                case "unknown":
                    issueDate = lastIssue.dIssue ?? lastIssue.dFrom;
                    break;
                default:
                    throw new Exception("Unexpected ACT alias");
            }

            certType = noms["airworthinessCertificateTypes"].ByAlias(actAlias);

            var aw = Utils.ToJObject(new
            {
                airworthinessCertificateType = certType,
                registration = registration,
                documentNumber = act.t_CofA_No,
                issueDate = issueDate
            });

            if (actAlias == "special")
            {
                if (issues.Count > 1)
                {
                    Console.WriteLine("Special airworthiness should not have reviews CERTID = " + certId);
                }
            }
            else
            {
                var reviews = new JArray();
                aw.Add("reviews", reviews);

                int l = issues.Count;
                for (int i = 0; i < l; i++)
                {
                    var review = Utils.ToJObject( new {
                        issueDate = issues[i].dFrom,
                        validToDate = issues[i].dValid,
                        inspector = new JObject()
                    });

                    reviews.Add(review);

                    var inspector = getInspector(issues[i].t_CAA_Inspetor);
                    if (inspector != null)
                    {
                        ((JObject)review["inspector"]).Add("inspector", inspector);
                    }

                    if (actAlias == "f24" || actAlias == "f25")
                    {
                        NomValue airworthinessReviewType = noms["airworthinessReviewTypes"].ByAlias("unknown");
                        Action<string> trySetType = (arcType) =>
                        {
                            if (arcType != null)
                            {
                                if (arcType.Contains("15a"))
                                {
                                    airworthinessReviewType = noms["airworthinessReviewTypes"].ByAlias("15a");
                                }
                                else if (arcType.Contains("15b"))
                                {
                                    airworthinessReviewType = noms["airworthinessReviewTypes"].ByAlias("15b");
                                }
                            }
                        };

                        trySetType(issues[i].t_ARC_Type);

                        if (i < l - 1 && issues[i].dIssue == issues[i + 1].dIssue &&
                            !(i < l - 3 && issues[i].dIssue == issues[i + 3].dIssue)) //if more than 3 consecutive treat as separate
                        {
                            review.Add("amendment1", Utils.ToJObject( new {
                                issueDate = issues[i + 1].dFrom,
                                validToDate = issues[i + 1].dValid,
                                inspector = getExaminerOrOther(issues[i + 1].t_Reviewed_By)
                            }));

                            trySetType(issues[i + 1].t_ARC_Type);
                            i++;

                            if (i < l - 2 && issues[i].dIssue == issues[i + 2].dIssue)
                            {
                                review.Add("amendment2",  Utils.ToJObject( new {
                                    issueDate = issues[i + 2].dFrom,
                                    validToDate = issues[i + 2].dValid,
                                    inspector = getExaminerOrOther(issues[i + 2].t_Reviewed_By)
                                }));

                                trySetType(issues[i + 2].t_ARC_Type);
                                i++;
                            }
                        }

                        review.Add("airworthinessReviewType", Utils.ToJObject(airworthinessReviewType));
                    }
                }
            }

            return new JObject(
                    new JProperty("part", aw),
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

        private IList<JObject> getAircraftDocumentDebtsFM(string aircraftFmId, Dictionary<string, Dictionary<string, NomValue>> noms, Func<string, JObject> getInspector)
        {
            return this.sqlConn.CreateStoreCommand(
                @"select * from Morts mo 
                left outer join Morts_New mn on mn.n_Mort_ID_Old = mo.nRecNo
                where {0} {1}",
                new DbClause("1=1"),
                new DbClause("and nActID = {0}", aircraftFmId)
                )
                .Materialize(r => new JObject(
                    new JProperty("part",
                        Utils.ToJObject(new
                        {
                            __oldId = r.Field<string>("nRecNo"),
                            __migrTable = "Morts",
                            regDate = Utils.FmToDate(r.Field<string>("Date")),
                            aircraftDebtType = noms["aircraftDebtTypesFm"].ByName(r.Field<string>("Action").Trim()),
                            documentNumber = r.Field<string>("gt_DocCAA"),
                            documentDate = Utils.FmToDate(r.Field<string>("gd_DocCAA")),
                            aircraftCreditor = noms["aircraftCreditorsFm"].ByName(r.Field<string>("Creditor")),
                            creditorDocument = r.Field<string>("Doc Creditor"),
                            inspector = getInspector(r.Field<string>("tUser")),
                            isActive = r.Field<string>("n_Status") != "0",
                            close = new {
                                inspector = getInspector(r.Field<string>("t_Close_User")),
                                date = Utils.FmToDate(r.Field<string>("d_Close_Date")),
                                caaDoc = r.Field<string>("t_Close_CAA_Doc"),
                                caaDate = Utils.FmToDate(r.Field<string>("d_Close_CAA_Date")),
                                creditorDoc = r.Field<string>("t_Close_Creditor_Doc"),
                                creditorDate = Utils.FmToDate(r.Field<string>("d_Close_Creditor_Date")),
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
