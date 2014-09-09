using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Autofac.Features.OwnedInstances;
using Common.Api.Models;
using Common.Api.Repositories.UserRepository;
using Common.Api.UserContext;
using Common.Data;
using Common.Json;
using Common.Tests;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Gva.Api.Repositories.PersonRepository;
using Gva.MigrationTool.Nomenclatures;
using Newtonsoft.Json.Linq;
using Oracle.DataAccess.Client;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.MigrationTool.Sets
{
    public class Aircraft
    {
        private Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, IUserRepository, IFileRepository, IApplicationRepository, IPersonRepository, IOrganizationRepository, ICaseTypeRepository, ILotEventDispatcher, UserContext>>> dependencyFactory;
        private OracleConnection oracleConn;
        private SqlConnection sqlConn;

        public Aircraft(
            OracleConnection oracleConn,
            SqlConnection sqlConn,
            Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, IUserRepository, IFileRepository, IApplicationRepository, IPersonRepository, IOrganizationRepository, ICaseTypeRepository, ILotEventDispatcher, UserContext>>> dependencyFactory)
        {
            this.dependencyFactory = dependencyFactory;
            this.oracleConn = oracleConn;
            this.sqlConn = sqlConn;
        }

        public Tuple<Dictionary<int, int>, Dictionary<string, int>, Dictionary<int, JObject>> createAircraftsLots(Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            Dictionary<int, int> apexIdtoLotId = new Dictionary<int, int>();
            Dictionary<string, int> apexMSNtoLotId = new Dictionary<string, int>();
            Dictionary<string, int> fmIdtoLotId = new Dictionary<string, int>();
            Dictionary<int, JObject> aircraftLotIdToAircraftNom = new Dictionary<int, JObject>();

            using (var dependencies = dependencyFactory())
            {
                var unitOfWork = dependencies.Value.Item1;
                var lotRepository = dependencies.Value.Item2;
                var userRepository = dependencies.Value.Item3;
                var fileRepository = dependencies.Value.Item4;
                var applicationRepository = dependencies.Value.Item5;
                var personRepository = dependencies.Value.Item6;
                var organizationRepository = dependencies.Value.Item7;
                var caseTypeRepository = dependencies.Value.Item8;
                var lotEventDispatcher = dependencies.Value.Item9;
                var context = dependencies.Value.Item10;

                unitOfWork.DbContext.Configuration.AutoDetectChangesEnabled = false;

                Func<Lot> createLot = () =>
                {
                    var lot = lotRepository.CreateLot("Aircraft");
                    int aircraftCaseTypeId = caseTypeRepository.GetCaseTypesForSet("Aircraft").Single().GvaCaseTypeId;
                    caseTypeRepository.AddCaseTypes(lot, new int[] { aircraftCaseTypeId });

                    return lot;
                };

                foreach (var aircraftApexId in this.getAircraftApexIds())
                {
                    var lot = createLot();
                    var aircraftData = this.getAircraftData(aircraftApexId, noms);
                    lot.CreatePart("aircraftDataApex", aircraftData, context);

                    lot.Commit(context, lotEventDispatcher);

                    unitOfWork.Save();
                    Console.WriteLine("Created aircraftDataApex part for aircraft with APEX id {0}", aircraftApexId);

                    apexIdtoLotId.Add(aircraftApexId, lot.LotId);

                    var msn = aircraftData.Get<string>("manSN");
                    if (apexMSNtoLotId.ContainsKey(msn))
                    {
                        Console.WriteLine("DUPLICATE APEX MSN: {0}", msn);//TODO
                    }
                    else
                    {
                        apexMSNtoLotId.Add(msn, lot.LotId);
                    }
                }

                foreach (var aircraftFmId in this.getAircraftFmIds())
                {
                    var aircraftDataFM = this.getAircraftDataFM(aircraftFmId, noms);
                    var msn = aircraftDataFM.Get<string>("manSN");

                    Lot lot;
                    if (apexMSNtoLotId.ContainsKey(msn))
                    {
                        lot = lotRepository.GetLotIndex(apexMSNtoLotId[msn], fullAccess: true);
                    }
                    else
                    {
                        Console.WriteLine("MISSING AIRCRAFT WITH MSN {0} IN APEX", msn);//TODO
                        lot = createLot();
                    }

                    if (lot.Index.GetPart("aircraftData") != null)
                    {
                        Console.WriteLine("AIRCRAFT WITH MSN {0} IN FM HAS ALREADY BEEN MIGRATED", msn);//TODO
                        continue;
                    }

                    lot.CreatePart("aircraftData", aircraftDataFM, context);
                    lot.Commit(context, lotEventDispatcher);

                    unitOfWork.Save();
                    Console.WriteLine("Created aircraftData part for aircraft with FM id {0}", aircraftFmId);

                    fmIdtoLotId.Add(aircraftFmId, lot.LotId);

                    aircraftLotIdToAircraftNom.Add(lot.LotId, Utils.ToJObject(
                        new
                        {
                            nomValueId = lot.LotId,
                            name = aircraftDataFM.Get<string>("model"),
                            nameAlt = aircraftDataFM.Get<string>("modelAlt"),
                            textContent =
                                new
                                {
                                    airCategory = aircraftDataFM.Get<JObject>("airCategory"),
                                    aircraftProducer = aircraftDataFM.Get<JObject>("aircraftProducer")
                                }
                        }));
                }
            }

            return Tuple.Create(apexIdtoLotId, fmIdtoLotId, aircraftLotIdToAircraftNom);
        }

        public void migrateAircrafts(
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, int> aircraftApexIdtoLotId,
            Dictionary<string, int> aircraftFmIdtoLotId,
            Func<int?, JObject> getPersonByApexId,
            Func<int?, JObject> getOrgByApexId,
            Func<string, JObject> getPersonByFmOrgName,
            Func<string, JObject> getOrgByFmOrgName)
        {
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

            foreach (var aircraftApexId in this.getAircraftApexIds())
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

                    unitOfWork.DbContext.Configuration.AutoDetectChangesEnabled = false;

                    var lot = lotRepository.GetLotIndex(aircraftApexIdtoLotId[aircraftApexId], fullAccess: true);

                    var aircraftParts = this.getAircraftParts(aircraftApexId, noms);
                    foreach (var aircraftPart in aircraftParts)
                    {
                        lot.CreatePart("aircraftParts/*", aircraftPart, context);
                    }

                    var aircraftMaintenances = this.getAircraftMaintenances(aircraftApexId, getPersonByApexId, getOrgByApexId, noms);
                    foreach (var aircraftMaintenance in aircraftMaintenances)
                    {
                        lot.CreatePart("maintenances/*", aircraftMaintenance, context);
                    }

                    Func<string, JObject, PartVersion> addPartWithFiles = (path, content) =>
                    {
                        var pv = lot.CreatePart(path, content.Get<JObject>("part"), context);
                        fileRepository.AddFileReferences(pv, content.GetItems<FileDO>("files"));
                        return pv;
                    };

                    Dictionary<int, Tuple<GvaApplication, ApplicationNomDO>> applications =
                        new Dictionary<int, Tuple<GvaApplication, ApplicationNomDO>>();
                    var aircraftDocumentApplications = this.getAircraftDocumentApplications(aircraftApexId, noms);
                    foreach (var aircraftDocumentApplication in aircraftDocumentApplications)
                    {
                        var pv = addPartWithFiles("aircraftDocumentApplications/*", aircraftDocumentApplication);

                        GvaApplication application = new GvaApplication()
                        {
                            Lot = lot,
                            GvaAppLotPart = pv.Part
                        };

                        applicationRepository.AddGvaApplication(application);

                        applications.Add(
                            aircraftDocumentApplication.Get<int>("part.__oldId"),
                                Tuple.Create(
                                    application,
                                    new ApplicationNomDO
                                    {
                                        ApplicationId = 0, //will be set later
                                        PartIndex = pv.Part.Index,
                                        ApplicationName = aircraftDocumentApplication.Get<string>("part.applicationType.name")
                                    }));
                    }

                    unitOfWork.Save();

                    Dictionary<int, JObject> nomApplications = new Dictionary<int, JObject>();
                    foreach (var app in applications)
                    {
                        var gvaApp = app.Value.Item1;
                        var appNomDO = app.Value.Item2;
                        appNomDO.ApplicationId = gvaApp.GvaApplicationId;

                        nomApplications.Add(
                                app.Key,
                                Utils.ToJObject(appNomDO));
                    }

                    var aircraftDocumentOccurrences = this.getAircraftDocumentOccurrences(aircraftApexId, nomApplications, noms);
                    foreach (var aircraftDocumentOccurrence in aircraftDocumentOccurrences)
                    {
                        addPartWithFiles("documentOccurrences/*", aircraftDocumentOccurrence);
                    }

                    Dictionary<int, int> documentOwnersOldIdToPartIndex = new Dictionary<int, int>();
                    var aircraftDocumentOwners = this.getAircraftDocumentOwners(aircraftApexId, nomApplications, getPersonByApexId, getOrgByApexId, noms);
                    foreach (var aircraftDocumentOwner in aircraftDocumentOwners)
                    {
                        var pv = addPartWithFiles("aircraftDocumentOwners/*", aircraftDocumentOwner);
                        documentOwnersOldIdToPartIndex.Add(aircraftDocumentOwner.Get<int>("part.__oldId"), pv.Part.Index);
                    }

                    var aircraftDocumentOthers = this.getAircraftDocumentOthers(aircraftApexId, nomApplications, noms);
                    foreach (var aircraftDocumentOther in aircraftDocumentOthers)
                    {
                        addPartWithFiles("aircraftDocumentOthers/*", aircraftDocumentOther);
                    }

                    Dictionary<int, int> inspections = new Dictionary<int, int>();

                    var aircraftInspections = this.getAircraftInspections(aircraftApexId, nomApplications, getPersonByApexId, noms);
                    foreach (var aircraftInspection in aircraftInspections)
                    {
                        var pv = lot.CreatePart("inspections/*", aircraftInspection.Get<JObject>("part"), context);
                        applicationRepository.AddApplicationRefs(pv.Part, aircraftInspection.GetItems<ApplicationNomDO>("applications"));

                        inspections.Add(aircraftInspection.Get<int>("part.__oldId"), pv.Part.Index);
                    }

                    var aircraftDocumentDebts = this.getAircraftDocumentDebts(aircraftApexId, nomApplications, getPersonByApexId, noms, documentOwnersOldIdToPartIndex);
                    foreach (var aircraftDocumentDebt in aircraftDocumentDebts)
                    {
                        addPartWithFiles("aircraftDocumentDebts/*", aircraftDocumentDebt);
                    }

                    var aircraftCertRegistrations = this.getAircraftCertRegistrations(aircraftApexId, getPersonByApexId, nomApplications, noms, documentOwnersOldIdToPartIndex);
                    foreach (var aircraftCertRegistration in aircraftCertRegistrations)
                    {
                        addPartWithFiles("aircraftCertRegistrations/*", aircraftCertRegistration);

                        long certId = aircraftCertRegistration.Get<long>("part.__oldId");

                        var aircraftCertAirworthinesses = this.getAircraftCertAirworthinesses(certId, inspections, getPersonByApexId, nomApplications, noms);
                        foreach (var aircraftCertAirworthiness in aircraftCertAirworthinesses)
                        {
                            addPartWithFiles("aircraftCertAirworthinesses/*", aircraftCertAirworthiness);
                        }

                        var aircraftCertPermitsToFly = this.getAircraftCertPermitsToFly(certId, nomApplications, noms);
                        foreach (var aircraftCertPermitToFly in aircraftCertPermitsToFly)
                        {
                            addPartWithFiles("aircraftCertPermitsToFly/*", aircraftCertPermitToFly);
                        }

                        var aircraftCertNoises = this.getAircraftCertNoises(certId);
                        foreach (var aircraftCertNoise in aircraftCertNoises)
                        {
                            lot.CreatePart("aircraftCertNoises/*", aircraftCertNoise, context);
                        }
                    }

                    var aircraftCertMarks = this.getAircraftCertMarks(aircraftApexId, nomApplications, noms);
                    foreach (var aircraftCertMark in aircraftCertMarks)
                    {
                        addPartWithFiles("aircraftCertMarks/*", aircraftCertMark);
                    }

                    var aircraftCertSmods = this.getAircraftCertSmods(aircraftApexId, nomApplications, noms);
                    foreach (var aircraftCertSmod in aircraftCertSmods)
                    {
                        addPartWithFiles("aircraftCertSmods/*", aircraftCertSmod);
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

                    Console.WriteLine("Migrated APEX aircraftId: {0}", aircraftApexId);
                }
            }

            foreach (var aircraftFmId in this.getAircraftFmIds())
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

                    unitOfWork.DbContext.Configuration.AutoDetectChangesEnabled = false;

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

                    if (!aircraftFmIdtoLotId.ContainsKey(aircraftFmId))
                    {
                        //TODO remove, those are the ones with duplicate MSN skipped earlier
                        continue;
                    }

                    Lot lot = lotRepository.GetLotIndex(aircraftFmIdtoLotId[aircraftFmId], fullAccess: true);

                    var aircraftCertRegistrationsFM = this.getAircraftCertRegistrationsFM(aircraftFmId, noms, getInspector, getPersonByFmOrgName, getOrgByFmOrgName);
                    foreach (var aircraftCertRegistrationFM in aircraftCertRegistrationsFM)
                    {
                        var pv = lot.CreatePart("aircraftCertRegistrationsFM/*", aircraftCertRegistrationFM, context);

                        var regPart = Utils.ToJObject(
                            new
                            {
                                partIndex = pv.Part.Index,
                                description = aircraftCertRegistrationFM.Get<int>("certNumber").ToString()
                            });

                        int certId = aircraftCertRegistrationFM["__oldId"].Value<int>();

                        var aircraftCertAirworthinessFM = this.getAircraftCertAirworthinessFM(aircraftFmId, certId, noms, regPart, getInspector, getInspectorOrDefault);
                        lot.CreatePart("aircraftCertAirworthinessesFM/*", aircraftCertAirworthinessFM, context);

                        var aircraftDocumentDebtsFM = this.getAircraftDocumentDebtsFM(certId, regPart, noms, getInspector);
                        foreach (var aircraftDocumentDebtFM in aircraftDocumentDebtsFM)
                        {
                            try
                            {
                                lot.CreatePart("aircraftDocumentDebtsFM/*", aircraftDocumentDebtFM, context);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("ERROR CREATEPART AIRCRAFTDOCUMENTDEBTSFM {0}", e.Message);//TODO
                                throw e;
                            }
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
        }

        private bool checkValue(string val)
        {
            return !String.IsNullOrEmpty(val) && val.Trim() != "n/a" && val.Trim() != "n / a";
        }

        private int? toNum(string val)
        {
            int value;
            if (checkValue(val) && Int32.TryParse(val.Trim(), out value))
            {
                return value;
            }

            return null;
        }

        private decimal? toDecimal(string val)
        {
            decimal value;
            if(checkValue(val) && decimal.TryParse(val.Trim(), out value))
            {
                return value;
            }

            return null;
        }

        private DateTime? toDate(string val)
        {
            DateTime value;
            if(checkValue(val) && DateTime.TryParseExact(val.Trim(), "d.M.yyyy", null, System.Globalization.DateTimeStyles.None, out value))
            {
                return value;
            }

            return null;
        }

        private IList<string> getAircraftFmIds()
        {
            var ids = this.sqlConn.CreateStoreCommand("select n_Act_ID from Acts")
                .Materialize(r => r.Field<string>("n_Act_ID"));

            if (Migration.IsPartial)
            {
                ids = ids
                    .Where(id =>
                        id == "1286" || //LZ-TIM
                        id == "1303" || //LZ-YUP
                        id == "1473"); //LZ-KEC
            }

            return ids.ToList();
        }

        private JObject getAircraftDataFM(string aircraftId, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            return this.sqlConn.CreateStoreCommand(
                @"select * from Acts where {0} {1}",
                new DbClause("1=1"),
                new DbClause("and n_Act_ID = {0}", aircraftId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<string>("n_Act_ID"),
                        __migrTable = "Acts",
                        aircraftProducer = noms["aircraftProducersFm"].ByOldId(r.Field<string>("n_Act_Maker_ID")),
                        aircraftCategory = noms["aircraftCategories"].ByCode(r.Field<string>("t_Act_TypeCode")),
                        icao = r.Field<string>("t_Act_ICAO"),
                        model = r.Field<string>("t_Act_Bg"),
                        modelAlt = r.Field<string>("t_Act_EN"),
                        manSN = r.Field<string>("t_Act_MSN"),
                        engine = r.Field<string>("t_Act_EngBg"),
                        engineAlt = r.Field<string>("t_Act_EngEn"),
                        propeller = r.Field<string>("t_Act_PropBg"),
                        propellerAlt = r.Field<string>("t_Act_PropEn"),
                        modifOrWingColor = r.Field<string>("t_Act_ModifOrWingColorBg"),
                        modifOrWingColorAlt = r.Field<string>("t_Act_ModifOrWingColorEn"),
                        maxMassT = toDecimal(r.Field<string>("n_Act_MTOM")),
                        maxMassL = toDecimal(r.Field<string>("n_Act_MLMorPayMass")),
                        seats = toNum(r.Field<string>("n_Act_SeatNo")),
                        outputDate = toDate(r.Field<string>("d_Act_DateOutput")),
                        docRoom = r.Field<string>("t_Act_Place_of_Documents"),
                        cofAType = noms["CofATypesFm"].ByName(r.Field<string>("t_CofA_Type").Replace("\"", string.Empty)),
                        airCategory = noms["EASATypesFm"].ByName(r.Field<string>("t_Act_EASA_Type")),
                        euRegType = noms["EURegTypesFm"].ByName(r.Field<string>("t_Act_EU_RU")),
                        easaCategory = noms["EASACategoriesFm"].ByName(r.Field<string>("t_EASA_Category")),
                        tcds = r.Field<string>("t_EASA_TCDS"),
                        noise = new
                        {
                            issueNumber = toNum(r.Field<string>("n_Noise_No_Issued")),
                            issueDate = toDate(r.Field<string>("d_CofN_45_Date")),
                            tcdsn = r.Field<string>("t_Noise_TCDS"),
                            chapter = r.Field<string>("t_Noise_Chapter"),
                            lateral = toDecimal(r.Field<string>("n_Noise_Literal")),
                            approach = toDecimal(r.Field<string>("n_Noise_Approach")),
                            flyover = toDecimal(r.Field<string>("n_Noise_FlyOver")),
                            overflight = toDecimal(r.Field<string>("n_Noise_OverFlight")),
                            takeoff = toDecimal(r.Field<string>("n_Noise_TakeOff")),
                            modifications = r.Field<string>("t_Noise_AddModifBg"),
                            modificationsAlt = r.Field<string>("t_Noise_AddModifEn"),
                            notes = r.Field<string>("t_Noise_Remark")
                        }
                    }))
                .Single();
        }

        private IList<JObject> getAircraftDocumentDebtsFM(int certId, JObject regPart, Dictionary<string, Dictionary<string, NomValue>> noms, Func<string, JObject> getInspector)
        {
            return this.sqlConn.CreateStoreCommand(
                @"select * from Morts where {0} {1}",
                new DbClause("1=1"),
                new DbClause("and RegNo = {0}", certId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<string>("nRecNo"),
                        __migrTable = "Morts",
                        registration = regPart,
                        certId = toNum(r.Field<string>("RegNo")),
                        regDate = toDate(r.Field<string>("Date")),
                        aircraftDebtType = noms["aircraftDebtTypesFm"].ByName(r.Field<string>("Action").Trim()),
                        documentNumber = r.Field<string>("gt_DocCAA"),
                        documentDate = r.Field<string>("gd_DocCAA"),
                        aircraftCreditor = noms["aircraftCreditorsFm"].ByName(r.Field<string>("Creditor")),
                        creditorDocument = r.Field<string>("Doc Creditor"),
                        inspector = getInspector(r.Field<string>("tUser"))
                    }))
                .ToList();
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
                        nRegNum = toNum(r.Field<string>("nRegNum")).Value,
                        actRegNum = toNum(r.Field<string>("actRegNum")).Value,
                        certDate = toDate(r.Field<string>("dRegDate")),
                        regMark = r.Field<string>("tRegMark"),
                        incomingDocNumber = r.Field<string>("tDocCAA"),
                        incomingDocDate = toDate(r.Field<string>("dDateCAA")),
                        incomingDocDesc = r.Field<string>("tDocOther"),
                        inspector = getInspector(r.Field<string>("tRegUser")),
                        owner = getOrgOrPerson(r.Field<string>("ownerNameEn")),
                        oper = getOrgOrPerson(r.Field<string>("operNameEn")),
                        catAW = noms["aircraftCatAWsFm"].ByCodeOrDefault(r.Field<string>("tCatCode")),//use OrDefault to skip 0 and BLANK codes (empty)
                        aircraftLimitation = noms["aircraftLimitationsFm"].ByCode(r.Field<string>("nLimitID")),
                        leasingDocNumber = r.Field<string>("tR83_Zapoved"),
                        leasingDocDate = toDate(r.Field<string>("dR83_Data")),
                        leasingLessor = getOrgOrPerson(r.Field<string>("tLessor")),//TODO
                        leasingAgreement = r.Field<string>("tLessorAgreement"),
                        leasingEndDate = toDate(r.Field<string>("dLeaseDate")),
                        status = noms["aircraftRegStatsesFm"].ByCode(r.Field<string>("nStatus")),
                        EASA25Number = r.Field<string>("tEASA_25"),
                        EASA25Date = toDate(r.Field<string>("dEASA_25")),
                        EASA15Date = toDate(r.Field<string>("dEASA_15")),
                        cofRDate = toDate(r.Field<string>("dCofR_New")),
                        noiseDate = toDate(r.Field<string>("dNoise_New")),
                        noiseNumber = r.Field<string>("tNoise_New"),
                        paragraph = r.Field<string>("tAnnexII_Bg"),
                        paragraphAlt = r.Field<string>("tAnnexII_En"),
                        removal = new
                        {
                            date = toDate(r.Field<string>("dDeRegDate")),
                            reason = r.Field<string>("tDeDocOther"),
                            text = r.Field<string>("tRemarkDeReg"),
                            documentNumber = r.Field<string>("tDeDocCAA"),
                            documentDate = toDate(r.Field<string>("dDeDateCAA")),
                            inspector = getInspector(r.Field<string>("tDeUser"))
                        }
                    })
                .OrderBy(r => r.nRegNum)
                .Select(r => Utils.ToJObject(
                    new
                    {
                        r.__oldId,
                        r.__migrTable,
                        r.register,
                        actNumber = r.nRegNum,
                        certNumber = r.actRegNum >= r.nRegNum ? r.nRegNum: r.actRegNum,
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
                        lesorOrganization = r.leasingLessor.Item2,
                        lessorPerson = r.leasingLessor.Item3,
                        r.leasingAgreement,
                        r.leasingEndDate,
                        r.status,
                        r.EASA25Number,
                        r.EASA25Date,
                        r.EASA15Date,
                        r.cofRDate,
                        r.noiseDate,
                        r.noiseNumber,
                        r.paragraph,
                        r.paragraphAlt,
                        r.removal,
                        isActive = false,
                        isCurrent = false
                    }))
                .ToList();

            var lastReg = registrations.LastOrDefault();
            if (lastReg != null)
            {
                lastReg["isActive"] = true;
                lastReg["isCurrent"] = true;
            }

            return registrations;
        }

        private JObject getAircraftCertAirworthinessFM(
            string aircraftFmId,
            int certId,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            JObject regPart,
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

                        dDateEASA_25_Issue = toDate(r.Field<string>("dDateEASA_25_Issue")),
                        d_24_Issue = toDate(r.Field<string>("d_24_Issue")),
                        
                        dIssue = toDate(r.Field<string>("dIssue")),
                        dFrom = toDate(r.Field<string>("dFrom")),
                        dValid = toDate(r.Field<string>("dValid")),

                        t_CAA_Inspetor = r.Field<string>("t_CAA_Inspector"),
                        t_Reviewed_By = r.Field<string>("t_Reviewed_By"),
                        t_ARC_RefNo = r.Field<string>("t_ARC_RefNo")
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

            var aw =
                new JObject(
                    new JProperty("airworthinessCertificateType", Utils.ToJObject(certType)),
                    new JProperty("registration", regPart),
                    new JProperty("documentNumber", act.t_CofA_No),
                    new JProperty("issueDate", issueDate));

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
                    var review = new JObject(
                        new JProperty("issueDate", issues[i].dFrom),
                        new JProperty("validToDate", issues[i].dValid),
                        new JProperty("approvalNumber", issues[i].t_ARC_RefNo),
                        new JProperty("inspector", new JObject()));
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
                            review.Add("amendment1", new JObject(
                                new JProperty("issueDate", issues[i + 1].dFrom),
                                new JProperty("validToDate", issues[i + 1].dValid),
                                new JProperty("approvalNumber", issues[i + 1].t_ARC_RefNo),
                                new JProperty("inspector", getExaminerOrOther(issues[i + 1].t_Reviewed_By))));

                            trySetType(issues[i + 1].t_ARC_Type);
                            i++;

                            if (i < l - 2 && issues[i].dIssue == issues[i + 2].dIssue)
                            {
                                review.Add("amendment2", new JObject(
                                    new JProperty("issueDate", issues[i + 2].dFrom),
                                    new JProperty("validToDate", issues[i + 2].dValid),
                                    new JProperty("approvalNumber", issues[i + 2].t_ARC_RefNo),
                                    new JProperty("inspector", getExaminerOrOther(issues[i + 2].t_Reviewed_By))));

                                trySetType(issues[i + 2].t_ARC_Type);
                                i++;
                            }
                        }

                        review.Add("airworthinessReviewType", Utils.ToJObject(airworthinessReviewType));
                    }
                }
            }

            return aw;
        }

        private IList<int> getAircraftApexIds()
        {
            var ids = this.oracleConn.CreateStoreCommand("SELECT ID FROM CAA_DOC.AIRCRAFT")
                .Materialize(r => r.Field<int>("ID"));

            if (Migration.IsPartial)
            {
                ids = ids
                    .Where(id =>
                        id == 1286 || //LZ-TIM
                        id == 1303); //LZ-YUP
            }

            return ids.ToList();
        }

        private JObject getAircraftData(int aircraftId, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            return this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AIRCRAFT WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID = {0}", aircraftId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "AIRCRAFT",
                        name = r.Field<string>("NAME"),
                        model = r.Field<string>("MODEL"),
                        series = r.Field<string>("SERIES"),
                        nameAlt = r.Field<string>("NAME_TRANS"),
                        aircraftTypeGroup = noms["aircraftTypeGroups"].ByOldId(r.Field<long?>("ID_GROUP").ToString()),
                        aircraftCategory = noms["aircraftCategories"].ByOldId(r.Field<long?>("ID_CATEGORY").ToString()),
                        aircraftSCodeType = noms["aircraftSCodeTypes"].ByOldId(r.Field<long?>("ID_STYPE").ToString()),
                        aircraftProducer = noms["aircraftProducers"].ByOldId(r.Field<long?>("ID_MANUFACTURER").ToString()),
                        manPlace = r.Field<string>("MAN_PLACE"),
                        manDate = r.Field<DateTime?>("MAN_DATE"),
                        manSN = r.Field<string>("MSN"),
                        beaconCodeELT = r.Field<string>("ELT_HEX"),
                        maxMassT = r.Field<decimal?>("MAX_MASS_T"),
                        maxMassL = r.Field<decimal?>("MAX_MASS_L"),
                        ICAO = r.Field<string>("ICAO"),
                        docRoom = r.Field<string>("DOC_ROOM"),
                        mass = new
                        {
                            mass = r.Field<decimal?>("MASS"),
                            cax = r.Field<float?>("MASS_CAX"),
                            date = r.Field<DateTime?>("MASS_DATE")
                        },
                        ultralight = new
                        {
                            color = r.Field<string>("COLOR"),
                            colorAlt = r.Field<string>("COLOR_TRANS"),
                            seats = r.Field<string>("SEATS"),
                            payload = r.Field<string>("PAYLOAD")
                        },
                        noise = new
                        {
                            flyover = r.Field<float?>("FLYOVER_NOISE"),
                            approach = r.Field<float?>("APPROACH_NOISE"),
                            lateral = r.Field<float?>("LATERAL_NOISE"),
                            overflight = r.Field<float?>("OVERFLIGHT_NOISE"),
                            takeoff = r.Field<float?>("TAKE_OFF_NOISE")
                        },
                        radio = new
                        {
                            approvalNumber = r.Field<string>("RADIO_NO"),
                            approvalDate = r.Field<DateTime?>("RADIO_DATE"),
                            incommingApprovalnumber = r.Field<string>("RADIO_DOC"),
                            incommingApprovalDate = r.Field<DateTime?>("RADIO_DOC_DATE")
                        }
                    }))
                .Single();
        }

        private IList<JObject> getAircraftDocumentOwners(
            int aircraftId,
            Dictionary<int, JObject> nomApplications,
            Func<int?, JObject> getPersonByApexId,
            Func<int?, JObject> getOrgByApexId,
            Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            var parts = this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AC_OWNER WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "AC_OWNER",

                        __BOOK_PAGE_NO = (int?)r.Field<decimal?>("BOOK_PAGE_NO"),
                        __PAGES_COUNT = (int?)r.Field<decimal?>("PAGES_COUNT"),

                        aircraftRelation = noms["aircraftRelations"].ByOldId(r.Field<long?>("TYPE_RELATION").ToString()),
                        organization = getOrgByApexId((int?)r.Field<decimal?>("ID_FIRM")),
                        person = getPersonByApexId((int?)r.Field<decimal?>("ID_PERSON")),
                        documentNumber = r.Field<string>("DOC_NUM"),
                        documentDate = r.Field<DateTime?>("DOC_DATE"),
                        fromDate = r.Field<DateTime?>("DATE_FROM"),
                        toDate = r.Field<DateTime?>("DATE_TO"),
                        reasonTerminate = r.Field<string>("REASON_TERMINATE"),
                        notes = r.Field<string>("NOTES"),
                    }))
                .ToList();

            var files = this.oracleConn.CreateStoreCommand(
                @"SELECT O.ID,
                        D.DOC_ID,
                        D.MIME_TYPE,
                        D.DESCRIPTION,
                        D.INS_USER,
                        D.INS_DATE,
                        D.UPD_USER,
                        D.UPD_DATE,
                        D.NAME
                    FROM CAA_DOC.AC_OWNER O
                    JOIN CAA_DOC.DOCLIB_DOCUMENTS D ON O.ID + 20000000 = D.DOC_ID
                    WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and O.ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("DOC_ID"),
                        __migrTable = "DOCLIB_DOCUMENTS",
                        __AC_OWNER_ID = r.Field<int>("ID"),

                        key = Utils.DUMMY_FILE_KEY,//TODO
                        name = r.Field<string>("NAME"),
                        mimeType = r.Field<string>("MIME_TYPE")
                    }))
                .ToList();

            return (from part in parts
                    join file in files on part.Get<int>("__oldId") equals file.Get<int>("__AC_OWNER_ID") into fg
                    let file = fg.Any() ? fg.Single() : null //throw if more than one files present
                    let bookPageNumber = part.Get<int?>("__BOOK_PAGE_NO").ToString()
                    let pageCount = part.Get<int?>("__PAGES_COUNT")
                    select new JObject(
                        new JProperty("part",
                            Utils.Pluck(part,
                                new string[] 
                                {
                                    "__oldId",
                                    "__migrTable",

                                    "aircraftRelation",
                                    "organization",
                                    "person",
                                    "documentNumber",
                                    "documentDate",
                                    "fromDate",
                                    "toDate",
                                    "reasonTerminate",
                                    "notes",
                                })),
                        new JProperty("files",
                            new JArray(
                                new JObject(
                                    new JProperty("isAdded", true),
                                    new JProperty("file", Utils.Pluck(file, new string[] { "key", "name", "mimeType" })),
                                    new JProperty("caseType", Utils.ToJObject(noms["aircraftCaseTypes"].ByAlias("aircraft"))),
                                    new JProperty("bookPageNumber", bookPageNumber),
                                    new JProperty("pageCount", pageCount),
                                    new JProperty("applications", new JArray()))))))
                    .ToList();


        }

        private IList<JObject> getAircraftParts(int aircraftId, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            return this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AC_PART WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "AC_PART",
                        aircraftPart = noms["aircraftParts"].ByOldId(r.Field<long?>("PART_TYPE").ToString()),
                        partProducer = noms["aircraftProducers"].ByOldId(r.Field<long?>("ID_MANUFACTURER").ToString()),
                        model = r.Field<string>("PART_MODEL"),
                        modelAlt = r.Field<string>("PART_MODEL_TRANS"),
                        sn = r.Field<string>("SN"),
                        count = r.Field<decimal?>("PART_NUMBER"),
                        aircraftPartStatus = noms["aircraftPartStatuses"].ByOldId(r.Field<string>("NEW_YN").ToString()),
                        manDate = r.Field<DateTime?>("MAN_DATE"),
                        manPlace = r.Field<string>("MAN_PLACE"),
                        description = r.Field<string>("DESCRIPTION"),
                    }))
                .ToList();
        }

        private IList<JObject> getAircraftDocumentDebts(
            int aircraftId,
            Dictionary<int, JObject> nomApplications,
            Func<int?, JObject> getPersonByApexId,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, int> documentOwnersOldIdToPartIndex)
        {
            var parts = this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AC_DEBT WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "AC_DEBT",

                        __BOOK_PAGE_NO = (int?)r.Field<decimal?>("BOOK_PAGE_NO"),
                        __PAGES_COUNT = (int?)r.Field<decimal?>("PAGES_COUNT"),

                        aircraftDebtType = noms["aircraftDebtTypes"].ByOldId(r.Field<string>("REC_TYPE").ToString()),
                        regDate = r.Field<DateTime?>("REG_DATE"),
                        contractNumber = r.Field<string>("CONTRACT_NUM"),
                        contractDate = r.Field<DateTime?>("CONTRACT_DATE"),
                        startDate = r.Field<DateTime?>("START_DATE"),
                        inspector = getPersonByApexId((int?)r.Field<decimal?>("REG_EXAMINER_ID")),
                        startReason = r.Field<string>("START_REASON"),
                        startReasonAlt = r.Field<string>("START_REASON_TRANS"),
                        notes = r.Field<string>("NOTES"),
                        ltrNumber = r.Field<string>("LTR_CAA_NOM"),
                        ltrDate = r.Field<DateTime?>("LTR_CAA_DATE"),
                        ownerPartIndex = r.Field<int?>("ID_OWNER") != null ? documentOwnersOldIdToPartIndex[r.Field<int?>("ID_OWNER").Value] : (int?)null,
                        operatorPartIndex = r.Field<int?>("ID_OPER") != null ? documentOwnersOldIdToPartIndex[r.Field<int?>("ID_OPER").Value] : (int?)null,
                        endReason = r.Field<string>("END_REASON"),
                        endDate = r.Field<DateTime?>("END_DATE"),
                        closeAplicationNumber = r.Field<string>("CLOSE_APPL_DOC"),
                        closeAplicationDate = r.Field<DateTime?>("CLOSE_APPL_DATE"),
                        closeCaaAplicationNumber = r.Field<string>("CLOSE_CAA_DOC"),
                        closeCaaAplicationDate = r.Field<DateTime?>("CLOSE_CAA_DATE"),
                        closeInspector = getPersonByApexId((int?)r.Field<decimal?>("CLOSE_EXAMINER_ID")),
                        creditorName = r.Field<string>("CREDITOR_NAME"),
                        creditorNameAlt = r.Field<string>("CREDITOR_NAME_TRANS"),
                        creditorAddress = r.Field<string>("CREDITOR_ADRES"),
                        creditorEmail = r.Field<string>("CREDITOR_MAIL"),
                        creditorContact = r.Field<string>("CREDITOR_PERSON"),
                        creditorPhone = r.Field<string>("CREDITOR_TEL")
                    }))
                .ToList();

            var files = this.oracleConn.CreateStoreCommand(
                @"SELECT AD.ID,
                        D.DOC_ID,
                        D.MIME_TYPE,
                        D.DESCRIPTION,
                        D.INS_USER,
                        D.INS_DATE,
                        D.UPD_USER,
                        D.UPD_DATE,
                        D.NAME
                    FROM CAA_DOC.AC_DEBT AD
                    JOIN CAA_DOC.DOCLIB_DOCUMENTS D ON AD.ID + 30000000 = D.DOC_ID
                    WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and AD.ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("DOC_ID"),
                        __migrTable = "DOCLIB_DOCUMENTS",
                        __DEBT_ID = r.Field<int>("ID"),

                        key = Utils.DUMMY_FILE_KEY,//TODO
                        name = r.Field<string>("NAME"),
                        mimeType = r.Field<string>("MIME_TYPE")
                    }))
                .ToList();

            var apps = this.oracleConn.CreateStoreCommand(
                @"SELECT R.ID REQUEST_ID,
                        AD.ID AC_DEBT_ID
                    FROM CAA_DOC.REQUEST R
                    JOIN CAA_DOC.AIRCRAFT A ON A.ID = R.APPLICANT_AC_ID
                    JOIN CAA_DOC.AC_DEBT AD ON AD.ID_AIRCRAFT = A.ID
                                WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and AD.ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r =>
                    new
                    {
                        aircraftDebtId = r.Field<int>("AC_DEBT_ID"),
                        nomApp = nomApplications[r.Field<int>("REQUEST_ID")]
                    })
                .ToList();

            return (from part in parts
                    join file in files on part.Get<int>("__oldId") equals file.Get<int>("__DEBT_ID") into fg
                    join app in apps on part.Get<int>("__oldId") equals app.aircraftDebtId into ag
                    let file = fg.Any() ? fg.Single() : null //throw if more than one files present
                    let bookPageNumber = part.Get<int?>("__BOOK_PAGE_NO").ToString()
                    let pageCount = part.Get<int?>("__PAGES_COUNT")
                    select new JObject(
                        new JProperty("part",
                            Utils.Pluck(part,
                                new string[] 
                                {
                                    "__oldId",
                                    "__migrTable",

                                    "aircraftDebtType",
                                    "regDate",
                                    "contractNumber",
                                    "contractDate",
                                    "startDate",
                                    "inspector",
                                    "startReason",
                                    "startReasonAlt",
                                    "notes",
                                    "ltrNumber",
                                    "ltrDate",
                                    "owner",
                                    "oper",
                                    "endReason",
                                    "endDate",
                                    "closeAplicationNumber",
                                    "closeAplicationDate",
                                    "closeCaaAplicationNumber",
                                    "closeCaaAplicationDate",
                                    "closeInspector",
                                    "creditorName",
                                    "creditorNameAlt",
                                    "creditorAddress",
                                    "creditorEmail",
                                    "creditorContact",
                                    "creditorPhone",
                                })),
                        new JProperty("files",
                            new JArray(
                                new JObject(
                                    new JProperty("isAdded", true),
                                    new JProperty("file", Utils.Pluck(file, new string[] { "key", "name", "mimeType" })),
                                    new JProperty("caseType", Utils.ToJObject(noms["aircraftCaseTypes"].ByAlias("aircraft"))),
                                    new JProperty("bookPageNumber", bookPageNumber),
                                    new JProperty("pageCount", pageCount),
                                    new JProperty("applications", new JArray(ag.Select(a => a.nomApp))))))))
                    .ToList();
        }

        private IList<JObject> getAircraftMaintenances(
            int aircraftId,
            Func<int?, JObject> getPersonByApexId,
            Func<int?, JObject> getOrgByApexId,
            Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            return this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AC_MAINTENANCE WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "AC_MAINTENANCE",
                        lim145limitation = noms["lim145limitations"].ByOldId(r.Field<long?>("ID_MF145_LIMIT").ToString()),
                        notes = r.Field<string>("REMARKS"),
                        fromDate = r.Field<DateTime?>("DATE_FROM"),
                        toDate = r.Field<DateTime?>("DATE_TO"),
                        organization = getOrgByApexId((int?)r.Field<decimal?>("ID_FIRM")),
                        person = getPersonByApexId((int?)r.Field<decimal?>("ID_PERSON")),
                    }))
                .ToList();
        }

        private IList<JObject> getAircraftDocumentOccurrences(
            int aircraftId,
            Dictionary<int, JObject> nomApplications,
            Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            var parts = this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AC_OCCURRENCE WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_AC = {0}", aircraftId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "AC_OCCURRENCE",

                        __BOOK_PAGE_NO = (int?)r.Field<decimal?>("BOOK_PAGE_NO"),
                        __PAGES_COUNT = (int?)r.Field<decimal?>("PAGES_COUNT"),

                        localDate = r.Field<DateTime?>("LOCAL_DATE"),
                        localTime = Utils.TimeToMilliseconds(
                            r.Field<DateTime?>("LOCAL_DATE").Value.Hour,
                            r.Field<DateTime?>("LOCAL_DATE").Value.Minute),
                        aircraftOccurrenceClass = noms["aircraftOccurrenceClasses"].ByCode(r.Field<string>("CLASS")),
                        country = noms["countries"].ByOldId(r.Field<decimal?>("ID_COUNTRY").ToString()),
                        area = r.Field<string>("AREA"),
                        occurrenceNotes = r.Field<string>("TEXT"),
                        notes = r.Field<string>("NOTES")
                    }))
                .ToList();

            var files = this.oracleConn.CreateStoreCommand(
                @"SELECT O.ID,
                        D.DOC_ID,
                        D.MIME_TYPE,
                        D.DESCRIPTION,
                        D.INS_USER,
                        D.INS_DATE,
                        D.UPD_USER,
                        D.UPD_DATE,
                        D.NAME
                    FROM CAA_DOC.AC_OCCURRENCE O
                    JOIN CAA_DOC.DOCLIB_DOCUMENTS D ON O.ID + 70000000 = D.DOC_ID
                    WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and O.ID_AC = {0}", aircraftId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("DOC_ID"),
                        __migrTable = "DOCLIB_DOCUMENTS",
                        __OCCURRENCE_ID = r.Field<int>("ID"),

                        key = Utils.DUMMY_FILE_KEY,//TODO
                        name = r.Field<string>("NAME"),
                        mimeType = r.Field<string>("MIME_TYPE")
                    }))
                .ToList();

            return (from part in parts
                    join file in files on part.Get<int>("__oldId") equals file.Get<int>("__OCCURRENCE_ID") into fg
                    let file = fg.Any() ? fg.Single() : null //throw if more than one files present
                    let bookPageNumber = part.Get<int?>("__BOOK_PAGE_NO").ToString()
                    let pageCount = part.Get<int?>("__PAGES_COUNT")
                    select new JObject(
                        new JProperty("part",
                            Utils.Pluck(part,
                                new string[] 
                                {
                                    "__oldId",
                                    "__migrTable",

                                    "localDate",
                                    "localTime",
                                    "aircraftOccurrenceClass",
                                    "country",
                                    "area",
                                    "occurrenceNotes",
                                    "notes",
                                })),
                        new JProperty("files",
                            new JArray(
                                new JObject(
                                    new JProperty("isAdded", true),
                                    new JProperty("file", Utils.Pluck(file, new string[] { "key", "name", "mimeType" })),
                                    new JProperty("caseType", Utils.ToJObject(noms["aircraftCaseTypes"].ByAlias("aircraft"))),
                                    new JProperty("bookPageNumber", bookPageNumber),
                                    new JProperty("pageCount", pageCount),
                                    new JProperty("applications", new JArray()))))))
                    .ToList();
        }

        private IList<JObject> getAircraftInspections(
            int aircraftId,
            Dictionary<int, JObject> nomApplications,
            Func<int?, JObject> getPersonByApexId,
            Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            var disparities = this.oracleConn.CreateStoreCommand(
                @"SELECT ADT.ID ID_AUDIT,
                        AD.ID_REQUIREMENT,
                        RD.SEQ,
                        RD.REF_NUMBER,
                        RD.DESCRIPTION,
                        RD.REC_DISPRT_LEVEL,
                        RD.REMOVAL_DATE,
                        RD.RECTIFY_ACTION,
                        RD.CLOSURE_DATE,
                        RD.CLOSURE_DOC
                    FROM 
                    CAA_DOC.AUDITS ADT
                    LEFT OUTER JOIN CAA_DOC.REC_DISPARITY RD ON ADT.ID = RD.ID_AUDIT
                    LEFT OUTER JOIN CAA_DOC.AUDITS_DETAIL AD ON RD.ID_AUDIT_DETAIL = AD.ID",
                    new DbClause("ADT.ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r =>
                    new
                    {
                        ID_AUDIT = r.Field<long?>("ID_AUDIT"),
                        ID_REQUIREMENT = r.Field<long?>("ID_REQUIREMENT"),
                        SEQ = r.Field<decimal?>("SEQ"),
                        REF_NUMBER = r.Field<string>("REF_NUMBER"),
                        DESCRIPTION = r.Field<string>("DESCRIPTION"),
                        REC_DISPRT_LEVEL = r.Field<decimal?>("REC_DISPRT_LEVEL"),
                        REMOVAL_DATE = r.Field<DateTime?>("REMOVAL_DATE"),
                        RECTIFY_ACTION = r.Field<string>("RECTIFY_ACTION"),
                        CLOSURE_DATE = r.Field<DateTime?>("CLOSURE_DATE"),
                        CLOSURE_DOC = r.Field<string>("CLOSURE_DOC")
                    })
                    .GroupBy(g => g.ID_AUDIT)
                    .ToDictionary(
                        g => g.Key,
                        g => g
                            .Where(d => d.ID_REQUIREMENT.HasValue && d.REC_DISPRT_LEVEL.HasValue)
                            .OrderBy(d => d.SEQ)
                            .Select(d =>
                                new
                                {
                                    detailCode = noms["auditPartRequirements"].ByOldId(d.ID_REQUIREMENT.ToString()).Code,
                                    refNumber = d.REF_NUMBER,
                                    disparityLevel = noms["disparityLevels"].ByCode(d.REC_DISPRT_LEVEL.ToString()),
                                    rectifyAction = d.RECTIFY_ACTION,
                                    closureDocument = d.CLOSURE_DOC,
                                    description = d.DESCRIPTION,
                                    removalDate = d.REMOVAL_DATE,
                                    closureDate = d.CLOSURE_DATE
                                })
                            .ToArray());

            var inspectionDetails = this.oracleConn.CreateStoreCommand(
                @"SELECT ADT.ID ID_AUDIT,
                        AD.STATE,
                        AD.ID_REQUIREMENT
                    FROM 
                    CAA_DOC.AUDITS ADT
                    LEFT OUTER JOIN CAA_DOC.AUDITS_DETAIL AD ON ADT.ID = AD.ID_AUDIT
                    WHERE {0}",
                    new DbClause("ADT.ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r =>
                    new
                    {
                        ID_AUDIT = r.Field<int>("ID_AUDIT"),
                        STATE = r.Field<string>("STATE"),
                        ID_REQUIREMENT = r.Field<long?>("ID_REQUIREMENT")
                    })
                .GroupBy(e => e.ID_AUDIT)
                .ToDictionary(
                    g => g.Key,
                    g => g
                        .Where(i => i.ID_REQUIREMENT.HasValue)
                        .Select(ad =>
                            new
                            {
                                auditResult = noms["auditResults"].ByCode(ad.STATE ?? "-1"),
                                auditPartRequirement = noms["auditPartRequirements"].ByOldId(ad.ID_REQUIREMENT.ToString())
                            })
                        .Select(r =>
                            new
                            {
                                code = r.auditPartRequirement.Code,
                                subject = r.auditPartRequirement.Name,
                                auditResult = r.auditResult
                            })
                        .ToArray());

            var examiners = this.oracleConn.CreateStoreCommand(
                @"SELECT ADTR.SEQ,
                        E.PERSON_ID,
                        ADT.ID AUDIT_ID
                    FROM
                    CAA_DOC.AUDITS ADT
                    LEFT OUTER JOIN CAA_DOC.AUDITOR ADTR ON ADT.ID = ADTR.ID_ODIT
                    LEFT OUTER JOIN CAA_DOC.EXAMINER E ON ADTR.ID_EXAMINER = E.ID
                    WHERE {0}",
                    new DbClause("ADT.ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r =>
                    new
                    {
                        AUDIT_ID = r.Field<int>("AUDIT_ID"),
                        SEQ = r.Field<decimal?>("SEQ"),
                        PERSON_ID = r.Field<int?>("PERSON_ID"),
                    })
                .GroupBy(e => e.AUDIT_ID)
                .ToDictionary(
                    g => g.Key,
                    g => g
                        .Where(i => i.SEQ.HasValue && i.PERSON_ID.HasValue)
                        .OrderBy(i => i.SEQ.Value)
                        .Select(i => getPersonByApexId(i.PERSON_ID.Value))
                        .ToArray());

            return this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AUDITS WHERE {0}",
                new DbClause("ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        part =
                            new
                            {
                                __oldId = r.Field<int>("ID"),
                                __migrTable = "AUDITS",

                                documentNumber = r.Field<string>("DOC_NUMBER"),
                                auditPart = noms["auditParts"].ByOldId(r.Field<int>("ID_PART").ToString()),
                                auditReason = noms["auditReasons"].ByCode(r.Field<string>("REASON")),
                                auditType = noms["auditTypes"].ByCode(r.Field<string>("AUDIT_MODE")),
                                auditState = noms["auditStatuses"].ByCode(r.Field<string>("STATE")),
                                notification = noms["boolean"].ByCode(r.Field<string>("NOTIFICATION") == "Y" ? "Y" : "N"),
                                subject = r.Field<string>("SUBJECT"),
                                inspectionPlace = r.Field<string>("INSPECTION_PLACE"),
                                startDate = r.Field<DateTime?>("DATE_BEGIN"),
                                endDate = r.Field<DateTime?>("DATE_END"),

                                inspectionDetails = inspectionDetails[r.Field<int>("ID")],
                                disparities = disparities[r.Field<int>("ID")],
                                examiners = examiners[r.Field<int>("ID")],

                                inspectionFrom = r.Field<DateTime?>("INSPECTION_FROM"),
                                inspectionTo = r.Field<DateTime?>("INSPECTION_TO")
                            },
                        applications = (r.Field<decimal?>("ID_REQUEST") != null && nomApplications.ContainsKey(r.Field<int>("ID_REQUEST"))) ?
                            new object[] { nomApplications[r.Field<int>("ID_REQUEST")] } :
                            new object[0]
                    }))
                .ToList();
        }

        private IList<JObject> getAircraftDocumentApplications(int aircraftId, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            var parts = this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.REQUEST WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and APPLICANT_AC_ID = {0}", aircraftId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "REQUEST",
                        __REQUEST_DATE = r.Field<DateTime?>("REQUEST_DATE"),

                        __BOOK_PAGE_NO = (int?)r.Field<decimal?>("BOOK_PAGE_NO"),
                        __PAGES_COUNT = (int?)r.Field<decimal?>("PAGES_COUNT"),

                        documentNumber = r.Field<string>("DOC_NO"),
                        documentDate = r.Field<DateTime?>("DOC_DATE"),
                        notes = r.Field<string>("NOTES"),
                        applicationType = noms["applicationTypes"].ByOldId(r.Field<decimal?>("REQUEST_TYPE_ID").ToString()),
                        applicationPaymentType = noms["applicationPaymentTypes"].ByOldId(r.Field<decimal?>("PAYMENT_REASON_ID").ToString()),
                        currency = noms["currencies"].ByOldId(r.Field<decimal?>("CURRENCY_ID").ToString()),
                        taxAmount = r.Field<decimal?>("TAX_AMOUNT")
                    }))
                .ToList();

            var files = this.oracleConn.CreateStoreCommand(
                @"SELECT R.ID,
                        R.BOOK_PAGE_NO,
                        R.PAGES_COUNT,
                        D.DOC_ID,
                        D.MIME_TYPE,
                        D.DESCRIPTION,
                        D.INS_USER,
                        D.INS_DATE,
                        D.UPD_USER,
                        D.UPD_DATE,
                        D.NAME
                    FROM CAA_DOC.REQUEST R
                    JOIN CAA_DOC.DOCLIB_DOCUMENTS D ON R.ID + 90000000 = D.DOC_ID
                    WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and R.APPLICANT_AC_ID = {0}", aircraftId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("DOC_ID"),
                        __migrTable = "DOCLIB_DOCUMENTS",

                        __REQUEST_ID = r.Field<int>("ID"),

                        key = Utils.DUMMY_FILE_KEY,//TODO
                        name = r.Field<string>("NAME"),
                        mimeType = r.Field<string>("MIME_TYPE")
                    }))
                .ToList();

            return (from part in parts
                    join file in files on part.Get<int>("__oldId") equals file.Get<int>("__REQUEST_ID") into fg
                    let file = fg.Any() ? fg.Single() : null //throw if more than one files present
                    let bookPageNumber = part.Get<int?>("__BOOK_PAGE_NO").ToString()
                    let pageCount = part.Get<int?>("__PAGES_COUNT")
                    select new JObject(
                        new JProperty("part",
                            Utils.Pluck(part,
                                new string[] 
                                {
                                    "__oldId",
                                    "__migrTable",
                                    "__REQUEST_DATE",

                                    "documentNumber",
                                    "documentDate",
                                    "notes",
                                    "applicationType",
                                    "applicationPaymentType",
                                    "currency",
                                    "taxAmount"
                                })),
                        new JProperty("files",
                            new JArray(
                                new JObject(
                                    new JProperty("isAdded", true),
                                    new JProperty("file", Utils.Pluck(file, new string[] { "key", "name", "mimeType" })),
                                    new JProperty("caseType", Utils.ToJObject(noms["aircraftCaseTypes"].ByAlias("aircraft"))),
                                    new JProperty("bookPageNumber", bookPageNumber),
                                    new JProperty("pageCount", pageCount),
                                    new JProperty("applications", new JArray()))))))
                    .ToList();
        }

        private IList<JObject> getAircraftDocumentOthers(
            int aircraftId,
            Dictionary<int, JObject> nomApplications,
            Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            var parts = this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.PERSON_DOCUMENT WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and AC_ID = {0}", aircraftId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "PERSON_DOCUMENT",

                        __BOOK_PAGE_NO = (int?)r.Field<decimal?>("BOOK_PAGE_NO"),
                        __PAGES_COUNT = (int?)r.Field<decimal?>("PAGES_COUNT"),

                        documentNumber = r.Field<string>("DOC_NO"),
                        documentDateValidFrom = r.Field<DateTime?>("VALID_FROM"),
                        documentDateValidTo = r.Field<DateTime?>("VALID_TO"),
                        documentPublisher = r.Field<string>("DOC_PUBLISHER"),
                        otherDocumentType = noms["documentTypes"].ByOldId(r.Field<decimal?>("DOCUMENT_TYPE_ID").ToString()),
                        otherDocumentRole = noms["documentRoles"].ByOldId(r.Field<decimal?>("DOCUMENT_ROLE_ID").ToString()),
                        valid = noms["boolean"].ByCode(r.Field<string>("VALID_YN") == "Y" ? "Y" : "N"),
                        notes = r.Field<string>("NOTES"),
                    }))
                .ToList();

            var files = this.oracleConn.CreateStoreCommand(
                @"SELECT PD.ID,
                        D.DOC_ID,
                        D.MIME_TYPE,
                        D.DESCRIPTION,
                        D.INS_USER,
                        D.INS_DATE,
                        D.UPD_USER,
                        D.UPD_DATE,
                        D.NAME
                    FROM CAA_DOC.PERSON_DOCUMENT PD
                    JOIN CAA_DOC.DOCLIB_DOCUMENTS D ON PD.ID + 10000000 = D.DOC_ID
                    WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and PD.AC_ID = {0}", aircraftId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("DOC_ID"),
                        __migrTable = "DOCLIB_DOCUMENTS",
                        __AIRCRAFT_DOCUMENT_ID = r.Field<int>("ID"),

                        key = Utils.DUMMY_FILE_KEY,//TODO
                        name = r.Field<string>("NAME"),
                        mimeType = r.Field<string>("MIME_TYPE")
                    }))
                .ToList();

            return (from part in parts
                    join file in files on part.Get<int>("__oldId") equals file.Get<int>("__AIRCRAFT_DOCUMENT_ID") into fg
                    let file = fg.Any() ? fg.Single() : null //throw if more than one files present
                    let bookPageNumber = part.Get<int?>("__BOOK_PAGE_NO").ToString()
                    let pageCount = part.Get<int?>("__PAGES_COUNT")
                    select new JObject(
                        new JProperty("part",
                            Utils.Pluck(part,
                                new string[] 
                                {
                                    "__oldId",
                                    "__migrTable",

                                    "documentNumber",
                                    "documentDateValidFrom",
                                    "documentDateValidTo",
                                    "documentPublisher",
                                    "otherDocumentType",
                                    "otherDocumentRole",
                                    "valid",
                                    "notes",
                                })),
                        new JProperty("files",
                            new JArray(
                                new JObject(
                                    new JProperty("isAdded", true),
                                    new JProperty("file", Utils.Pluck(file, new string[] { "key", "name", "mimeType" })),
                                    new JProperty("caseType", Utils.ToJObject(noms["aircraftCaseTypes"].ByAlias("aircraft"))),
                                    new JProperty("bookPageNumber", bookPageNumber),
                                    new JProperty("pageCount", pageCount),
                                    new JProperty("applications", new JArray()))))))
                    .ToList();
        }

        private IList<JObject> getAircraftCertRegistrations(
            int aircraftId,
            Func<int?, JObject> getPersonByApexId,
            Dictionary<int, JObject> nomApplications,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, int> documentOwnersOldIdToPartIndex)
        {
            var parts = this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AC_CERTIFICATE WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<long>("ID"),
                        __migrTable = "AC_CERTIFICATE",

                        register = noms["registers"].ByCode(r.Field<long>("ID").ToString().Substring(0, 1)),
                        certNumber = r.Field<string>("CERT_NUMBER"),
                        certDate = r.Field<DateTime?>("CERT_DATE"),
                        aircraftNewOld = noms["aircraftPartStatuses"].ByCode(r.Field<string>("NEW_USED")),
                        operationType = noms["aircraftOperTypes"].ByOldId(r.Field<long?>("ID_TYPE_OPER").ToString()),
                        ownerPartIndex = r.Field<int?>("ID_OWNER") != null ? documentOwnersOldIdToPartIndex[r.Field<int?>("ID_OWNER").Value] : (int?)null,
                        operatorPartIndex = r.Field<int?>("ID_OPER") != null ? documentOwnersOldIdToPartIndex[r.Field<int?>("ID_OPER").Value] : (int?)null,
                        inspector = getPersonByApexId(r.Field<int?>("ID_EXAMINER_REG")),
                        regnotes = r.Field<string>("NOTES_REG"),
                        paragraph = r.Field<string>("PARAGRAPH"),
                        paragraphAlt = r.Field<string>("PARAGRAPH_TRANS"),
                        removalDate = r.Field<DateTime?>("REMOVAL_DATE"),
                        removalReason = noms["aircraftRemovalReasons"].ByOldId(r.Field<string>("REMOVAL_REASON")),
                        removalText = r.Field<string>("REMOVAL_TEXT"),
                        removalDocumentNumber = r.Field<string>("REMOVAL_DOC_CAA"),
                        removalDocumentDate = r.Field<DateTime?>("REMOVAL_DOC_DATE"),
                        removalInspector = getPersonByApexId(r.Field<int?>("REMOVAL_ID_EXAMINER")),
                        typeCert = new
                        {
                            aircraftTypeCertificateType = noms["aircraftTypeCertificateTypes"].ByCode(r.Field<string>("TC_TYPE")),
                            certNumber = r.Field<string>("TC_EASA"),
                            certDate = r.Field<DateTime?>("TC_DATE"),
                            certRelease = r.Field<string>("TC_REALEASE"),
                            country = noms["countries"].ByOldId(r.Field<decimal?>("TC_COUNTRY_ID").ToString())
                        }
                    }))
                .ToList();

            var apps = this.oracleConn.CreateStoreCommand(
                @"SELECT R.ID REQUEST_ID,
                        ACC.ID AC_CERT_ID
                    FROM CAA_DOC.REQUEST R
                    JOIN CAA_DOC.AIRCRAFT A ON A.ID = R.APPLICANT_AC_ID
                    JOIN CAA_DOC.AC_CERTIFICATE ACC ON ACC.ID_AIRCRAFT = A.ID
                                WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ACC.ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r =>
                    new
                    {
                        aircraftCerttificateId = r.Field<long>("AC_CERT_ID"),
                        nomApp = nomApplications[r.Field<int>("REQUEST_ID")]
                    })
                .ToList();

            return (from part in parts
                    join app in apps on part.Get<long>("__oldId") equals app.aircraftCerttificateId into ag
                    select new JObject(
                        new JProperty("part",
                            Utils.Pluck(part,
                                new string[] 
                                {
                                    "__oldId",
                                    "__migrTable",

                                    "register",
                                    "certNumber",
                                    "certDate",
                                    "aircraftNewOld",
                                    "operationType",
                                    "owner",
                                    "oper",
                                    "inspector",
                                    "regnotes",
                                    "paragraph",
                                    "paragraphAlt",
                                    "removalDate",
                                    "removalReason",
                                    "removalText",
                                    "removalDocumentNumber",
                                    "removalDocumentDate",
                                    "removalInspectorId",
                                    "typeCert"
                                })),
                        new JProperty("files",
                            new JArray(
                                new JObject(
                                    new JProperty("isAdded", true),
                                    new JProperty("file", null),
                                    new JProperty("caseType", Utils.ToJObject(noms["aircraftCaseTypes"].ByAlias("aircraft"))),
                                    new JProperty("bookPageNumber", null),
                                    new JProperty("pageCount", null),
                                    new JProperty("applications", new JArray(ag.Select(a => a.nomApp))))))))
                    .ToList();
        }

        private IList<JObject> getAircraftCertAirworthinesses(
            long certId,
            Dictionary<int, int> inspections,
            Func<int?, JObject> getPersonByApexId,
            Dictionary<int, JObject> nomApplications,
            Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            var parts = this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AC_AIRWORTHINESS WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_CERTIFICATE = {0}", certId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "AC_AIRWORTHINESS",
                        aircraftCertificateType = noms["aircraftCertificateTypes"].ByCode(r.Field<string>("CERTIFICATE_TYPE")),
                        certId = r.Field<long?>("ID_CERTIFICATE"),
                        refNumber = r.Field<string>("REF_NUMBER"),
                        issueDate = r.Field<DateTime?>("ISSUE_DATE"),
                        validToDate = r.Field<DateTime?>("VALID_TO"),
                        auditPartIndex = (r.Field<long?>("ID_AUDITS") != null && inspections.ContainsKey(r.Field<int>("ID_AUDITS"))) ?
                            inspections[r.Field<int>("ID_AUDITS")] :
                            (int?)null,//TODO
                        approvalId = r.Field<long?>("ID_APPROVAL"),//TODO
                        inspector = getPersonByApexId((int?)r.Field<decimal?>("ID_EXAMINER")),
                        valid = noms["boolean"].ByCode(r.Field<string>("VALID_YN") == "Y" ? "Y" : "N"),
                        firstAmmendment = new
                        {
                            issueDate = r.Field<DateTime?>("EXT1_DATE"),
                            validToDate = r.Field<DateTime?>("EXT1_VALID_TO"),
                            approvalId = r.Field<long?>("EXT1_APPROVAL_ID"),//TODO
                            inspector = getPersonByApexId((int?)r.Field<decimal?>("EXT1_ID_EXAMINER")),
                        },
                        secondAmmendment = new
                        {
                            issueDate = r.Field<DateTime?>("EXT2_DATE"),
                            validToDate = r.Field<DateTime?>("EXT2_VALID_TO"),
                            approvalId = r.Field<long?>("EXT2_APPROVAL_ID"),//TODO
                            inspector = getPersonByApexId((int?)r.Field<decimal?>("EXT2_ID_EXAMINER")),
                        },
                        export = new {
                            country = noms["countries"].ByOldId(r.Field<decimal?>("ID_COUNTRY_TRANSFER").ToString()),
                            exceptions = r.Field<string>("EXEMPTIONS"),
                            exceptionsAlt = r.Field<string>("EXEMPTIONS_TRANS"),
                            special = r.Field<string>("SPECIAL_REQ"),
                            specialAlt = r.Field<string>("SPECIAL_REQ_TRANS"),
                        },
                        revokeDate = r.Field<DateTime?>("REVOKE_DATE"),
                        revokeinspector = getPersonByApexId((int?)r.Field<decimal?>("REVOKE_ID_EXAMINER")),
                        revokeCause = r.Field<string>("SPECIAL_REQ")
                    }))
                .ToList();

            var apps = this.oracleConn.CreateStoreCommand(
                @"SELECT R.ID REQUEST_ID,
                        AW.ID AC_AW_ID
                    FROM CAA_DOC.REQUEST R
                    JOIN CAA_DOC.AC_AIRWORTHINESS AW ON AW.ID_REQUEST = R.ID
                                WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and AW.ID_CERTIFICATE = {0}", certId)
                )
                .Materialize(r =>
                    new
                    {
                        aircraftAirworthinessId = r.Field<int>("AC_AW_ID"),
                        nomApp = nomApplications[r.Field<int>("REQUEST_ID")]
                    })
                .ToList();

            return (from part in parts
                    join app in apps on part.Get<int>("__oldId") equals app.aircraftAirworthinessId into ag
                    select new JObject(
                        new JProperty("part",
                            Utils.Pluck(part,
                                new string[] 
                                {
                                    "__oldId",
                                    "__migrTable",

                                    "aircraftCertificateType",
                                    "certId",
                                    "refNumber",
                                    "issueDate",
                                    "validToDate",
                                    "auditPartIndex",
                                    "approvalId",
                                    "inspector",
                                    "valid",
                                    "firstAmmendment",
                                    "secondAmmendment",
                                    "export",
                                    "revokeDate",
                                    "revokeinspector",
                                    "revokeCause"
                                })),
                        new JProperty("files",
                            new JArray(
                                new JObject(
                                    new JProperty("isAdded", true),
                                    new JProperty("file", null),
                                    new JProperty("caseType", Utils.ToJObject(noms["aircraftCaseTypes"].ByAlias("aircraft"))),
                                    new JProperty("bookPageNumber", null),
                                    new JProperty("pageCount", null),
                                    new JProperty("applications", new JArray(ag.Select(a => a.nomApp))))))))
                    .ToList();
        }

        private IList<JObject> getAircraftCertMarks(int aircraftId, Dictionary<int, JObject> nomApplications, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            var parts = this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AC_MARK WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "AC_MARK",
                        ltrInNumber = r.Field<string>("LTR_IN_NOM"),
                        ltrInDate = r.Field<DateTime?>("LTR_IN_DATE"),
                        ltrCaaNumber = r.Field<string>("LTR_CAA_NOM"),
                        ltrCaaDate = r.Field<DateTime?>("LTR_CAA_DATE"),
                        mark = r.Field<string>("MARK"),
                        valid = noms["boolean"].ByCode(r.Field<string>("VALID_YN") == "Y" ? "Y" : "N")
                    }))
                .ToList();

            var apps = this.oracleConn.CreateStoreCommand(
                @"SELECT R.ID REQUEST_ID,
                        AM.ID AC_MARK_ID
                    FROM CAA_DOC.REQUEST R
                    JOIN CAA_DOC.AC_MARK AM ON AM.ID_REQUEST = R.ID
                                WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and AM.ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r =>
                    new
                    {
                        aircraftCertMarkId = r.Field<int>("AC_MARK_ID"),
                        nomApp = nomApplications[r.Field<int>("REQUEST_ID")]
                    })
                .ToList();

            return (from part in parts
                    join app in apps on part.Get<int>("__oldId") equals app.aircraftCertMarkId into ag
                    select new JObject(
                        new JProperty("part",
                            Utils.Pluck(part,
                                new string[] 
                                {
                                    "__oldId",
                                    "__migrTable",

                                    "ltrInNumber",
                                    "ltrInDate",
                                    "ltrCaaNumber",
                                    "ltrCaaDate",
                                    "mark",
                                    "valid"
                                })),
                        new JProperty("files",
                            new JArray(
                                new JObject(
                                    new JProperty("isAdded", true),
                                    new JProperty("file", null),
                                    new JProperty("caseType", Utils.ToJObject(noms["aircraftCaseTypes"].ByAlias("aircraft"))),
                                    new JProperty("bookPageNumber", null),
                                    new JProperty("pageCount", null),
                                    new JProperty("applications", new JArray(ag.Select(a => a.nomApp))))))))
                    .ToList();
        }

        private IList<JObject> getAircraftCertSmods(int aircraftId, Dictionary<int, JObject> nomApplications, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            var parts = this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AC_S_CODE WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "AC_S_CODE",
                        ltrInNumber = r.Field<string>("LTR_IN_NOM"),
                        ltrInDate = r.Field<DateTime?>("LTR_IN_DATE"),
                        ltrCaaNumber = r.Field<string>("LTR_CAA_NOM"),
                        ltrCaaDate = r.Field<DateTime?>("LTR_CAA_DATE"),
                        caaTo = r.Field<string>("LTR_CAA_TO"),
                        caaJob = r.Field<string>("LTR_CAA_TO_JOB"),
                        caaToAddress = r.Field<string>("LTR_CAA_TO_ADDRESS"),
                        scode = r.Field<string>("S_CODE"),
                        valid = noms["boolean"].ByCode(r.Field<string>("VALID_YN") == "Y" ? "Y" : "N")
                    }))
                .ToList();

            var apps = this.oracleConn.CreateStoreCommand(
                @"SELECT R.ID REQUEST_ID,
                        AC.ID AC_SCODE_ID
                    FROM CAA_DOC.REQUEST R
                    JOIN CAA_DOC.AC_S_CODE AC ON AC.ID_REQUEST = R.ID
                                WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and AC.ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r =>
                    new
                    {
                        aircraftCertScodeId = r.Field<int>("AC_SCODE_ID"),
                        nomApp = nomApplications[r.Field<int>("REQUEST_ID")]
                    })
                .ToList();

            return (from part in parts
                    join app in apps on part.Get<int>("__oldId") equals app.aircraftCertScodeId into ag
                    select new JObject(
                        new JProperty("part",
                            Utils.Pluck(part,
                                new string[] 
                                {
                                    "__oldId",
                                    "__migrTable",

                                    "ltrInNumber",
                                    "ltrInDate",
                                    "ltrCaaNumber",
                                    "ltrCaaDate",
                                    "caaTo",
                                    "caaJob",
                                    "caaToAddress",
                                    "scode",
                                    "valid"
                                })),
                        new JProperty("files",
                            new JArray(
                                new JObject(
                                    new JProperty("isAdded", true),
                                    new JProperty("file", null),
                                    new JProperty("caseType", Utils.ToJObject(noms["aircraftCaseTypes"].ByAlias("aircraft"))),
                                    new JProperty("bookPageNumber", null),
                                    new JProperty("pageCount", null),
                                    new JProperty("applications", new JArray(ag.Select(a => a.nomApp))))))))
                    .ToList();
        }

        private IList<JObject> getAircraftCertPermitsToFly(long certId, Dictionary<int, JObject> nomApplications, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            var parts = this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AC_FLY_PERMIT WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_CERTIFICATE = {0}", certId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "AC_FLY_PERMIT",
                        certId = r.Field<long?>("ID_CERTIFICATE"),
                        issueDate = r.Field<DateTime?>("ISSUE_DATE"),
                        issuePlace = r.Field<string>("ISSUE_PLACE"),
                        purpose = r.Field<string>("PURPOSE"),
                        purposeAlt = r.Field<string>("PURPOSE_TRANS"),
                        pointFrom = r.Field<string>("POINT_FROM"),
                        pointFromAlt = r.Field<string>("POINT_FROM_TRANS"),
                        pointTo = r.Field<string>("POINT_TO"),
                        pointToAlt = r.Field<string>("POINT_TO_TRANS"),
                        planStops = r.Field<string>("PLANED_STOPS"),
                        planStopsAlt = r.Field<string>("PLANED_STOPS_TRANS"),
                        validToDate = r.Field<DateTime?>("VALID_TO"),
                        notes = r.Field<string>("REMARKS"),
                        notesAlt = r.Field<string>("REMARKS_TRANS"),
                        crew = r.Field<string>("CREW"),
                        crewAlt = r.Field<string>("CREW_TRANS"),
                    }))
                .ToList();

            var apps = this.oracleConn.CreateStoreCommand(
                @"SELECT R.ID REQUEST_ID,
                        AFP.ID AC_FLYPERM_ID
                    FROM CAA_DOC.REQUEST R
                    JOIN CAA_DOC.AC_FLY_PERMIT AFP ON AFP.ID_REQUEST = R.ID
                                WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and AFP.ID_CERTIFICATE = {0}", certId)
                )
                .Materialize(r =>
                    new
                    {
                        aircraftCertFlyPermitId = r.Field<int>("AC_FLYPERM_ID"),
                        nomApp = nomApplications[r.Field<int>("REQUEST_ID")]
                    })
                .ToList();

            return (from part in parts
                    join app in apps on part.Get<int>("__oldId") equals app.aircraftCertFlyPermitId into ag
                    select new JObject(
                        new JProperty("part",
                            Utils.Pluck(part,
                                new string[] 
                                {
                                    "__oldId",
                                    "__migrTable",

                                    "certId",
                                    "issueDate",
                                    "issuePlace",
                                    "purpose",
                                    "purposeAlt",
                                    "pointFrom",
                                    "pointFromAlt",
                                    "pointTo",
                                    "pointToAlt",
                                    "planStops",
                                    "planStopsAlt",
                                    "valitoDate",
                                    "notes",
                                    "notesAlt",
                                    "crew",
                                    "crewAlt"
                                })),
                        new JProperty("files",
                            new JArray(
                                new JObject(
                                    new JProperty("isAdded", true),
                                    new JProperty("file", null),
                                    new JProperty("caseType", Utils.ToJObject(noms["aircraftCaseTypes"].ByAlias("aircraft"))),
                                    new JProperty("bookPageNumber", null),
                                    new JProperty("pageCount", null),
                                    new JProperty("applications", new JArray(ag.Select(a => a.nomApp))))))))
                    .ToList();
        }

        private IList<JObject> getAircraftCertNoises(long certId)
        {
            return this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AC_NOISE WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_CERTIFICATE = {0}", certId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "AC_S_CODE",
                        certId = r.Field<long?>("ID_CERTIFICATE"),
                        issueDate = r.Field<DateTime?>("ISSUE_DATE"),
                        standart = r.Field<string>("NOISE_STANDARD"),
                        standartAlt = r.Field<string>("NOISE_STANDARD_TRANS"),
                        flyover = r.Field<float?>("FLYOVER_NOISE"),
                        approach = r.Field<float?>("APPROACH_NOISE"),
                        lateral = r.Field<float?>("LATERAL_NOISE"),
                        overflight = r.Field<float?>("OVERFLIGHT_NOISE"),
                        takeoff = r.Field<float?>("TAKE_OFF_NOISE"),
                        modifications = r.Field<string>("MODIFICATIONS"),
                        modificationsAlt = r.Field<string>("MODIFICATIONS_TRANS"),
                        notes = r.Field<string>("REMARKS")
                    }))
                .ToList();
        }
    }
}
