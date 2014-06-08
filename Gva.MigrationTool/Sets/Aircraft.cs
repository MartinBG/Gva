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
        private Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, IUserRepository, IFileRepository, IApplicationRepository, IPersonRepository, IOrganizationRepository, ILotEventDispatcher, UserContext>>> dependencyFactory;
        private OracleConnection oracleConn;
        private SqlConnection sqlConn;

        public Aircraft(
            OracleConnection oracleConn,
            SqlConnection sqlConn,
            Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, IUserRepository, IFileRepository, IApplicationRepository, IPersonRepository, IOrganizationRepository, ILotEventDispatcher, UserContext>>> dependencyFactory)
        {
            this.dependencyFactory = dependencyFactory;
            this.oracleConn = oracleConn;
            this.sqlConn = sqlConn;
        }

        public Tuple<Dictionary<int, int>, Dictionary<string, int>> createAircraftsLots(Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            Dictionary<int, int> apexIdtoLotId = new Dictionary<int, int>();
            Dictionary<string, int> apexMSNtoLotId = new Dictionary<string, int>();
            Dictionary<string, int> fmIdtoLotId = new Dictionary<string, int>();

            using (var dependencies = dependencyFactory())
            {
                var unitOfWork = dependencies.Value.Item1;
                var lotRepository = dependencies.Value.Item2;
                var userRepository = dependencies.Value.Item3;
                var fileRepository = dependencies.Value.Item4;
                var applicationRepository = dependencies.Value.Item5;
                var personRepository = dependencies.Value.Item6;
                var organizationRepository = dependencies.Value.Item7;
                var lotEventDispatcher = dependencies.Value.Item8;
                var context = dependencies.Value.Item9;

                unitOfWork.DbContext.Configuration.AutoDetectChangesEnabled = false;

                Set aircraftSet = lotRepository.GetSet("Aircraft");



                foreach (var aircraftApexId in this.getAircraftApexIds())
                {
                    var lot = aircraftSet.CreateLot(context);
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
                        lot = lotRepository.GetLotIndex(apexMSNtoLotId[msn]);
                    }
                    else
                    {
                        Console.WriteLine("MISSING AIRCRAFT WITH MSN {0} IN APEX", msn);//TODO
                        lot = aircraftSet.CreateLot(context);
                    }

                    if (lot.GetPart("aircraftData") != null)
                    {
                        Console.WriteLine("AIRCRAFT WITH MSN {0} IN FM HAS ALREADY BEEN MIGRATED", msn);//TODO
                        continue;
                    }

                    lot.CreatePart("aircraftData", aircraftDataFM, context);
                    lot.Commit(context, lotEventDispatcher);

                    unitOfWork.Save();
                    Console.WriteLine("Created aircraftData part for aircraft with FM id {0}", aircraftFmId);

                    fmIdtoLotId.Add(aircraftFmId, lot.LotId);
                }
            }

            return Tuple.Create(apexIdtoLotId, fmIdtoLotId);
        }

        public void migrateAircrafts(
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, int> aircraftApexIdtoLotId,
            Dictionary<string, int> aircraftFmIdtoLotId,
            Dictionary<int, int> personApexIdToLotId,
            Dictionary<int, int> orgApexIdToLotId)
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
                    var lotEventDispatcher = dependencies.Value.Item8;
                    var context = dependencies.Value.Item9;

                    unitOfWork.DbContext.Configuration.AutoDetectChangesEnabled = false;

                    Func<int?, JObject> getPerson = (personApexId) => Utils.GetPerson(personApexId, personRepository, personApexIdToLotId);
                    Func<int?, JObject> getOrganization = (orgApexId) => Utils.GetOrganization(orgApexId, organizationRepository, orgApexIdToLotId);

                    var lot = lotRepository.GetLotIndex(aircraftApexIdtoLotId[aircraftApexId]);

                    var aircraftParts = this.getAircraftParts(aircraftApexId, noms);
                    foreach (var aircraftPart in aircraftParts)
                    {
                        lot.CreatePart("aircraftParts/*", aircraftPart, context);
                    }

                    var aircraftMaintenances = this.getAircraftMaintenances(aircraftApexId, getPerson, getOrganization, noms);
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
                    var aircraftDocumentOwners = this.getAircraftDocumentOwners(aircraftApexId, nomApplications, getPerson, getOrganization, noms);
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

                    var aircraftInspections = this.getAircraftInspections(aircraftApexId, nomApplications, getPerson, noms);
                    foreach (var aircraftInspection in aircraftInspections)
                    {
                        var pv = lot.CreatePart("inspections/*", aircraftInspection, context);
                        inspections.Add(aircraftInspection["__oldId"].Value<int>(), pv.Part.Index);
                    }

                    var aircraftDocumentDebts = this.getAircraftDocumentDebts(aircraftApexId, nomApplications, getPerson, noms, documentOwnersOldIdToPartIndex);
                    foreach (var aircraftDocumentDebt in aircraftDocumentDebts)
                    {
                        addPartWithFiles("aircraftDocumentDebts/*", aircraftDocumentDebt);
                    }

                    var aircraftCertRegistrations = this.getAircraftCertRegistrations(aircraftApexId, getPerson, nomApplications, noms, documentOwnersOldIdToPartIndex);
                    foreach (var aircraftCertRegistration in aircraftCertRegistrations)
                    {
                        addPartWithFiles("aircraftCertRegistrations/*", aircraftCertRegistration);

                        long certId = aircraftCertRegistration.Get<long>("part.__oldId");

                        var aircraftCertAirworthinesses = this.getAircraftCertAirworthinesses(certId, inspections, getPerson, nomApplications, noms);
                        foreach (var aircraftCertAirworthiness in aircraftCertAirworthinesses)
                        {
                            addPartWithFiles("aircraftCertAirworthinesses/*", aircraftCertAirworthiness);
                        }

                        var aircraftCertPermitsToFly = this.getAircraftCertPermitsToFly(certId, nomApplications);
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
                    var lotEventDispatcher = dependencies.Value.Item8;
                    var context = dependencies.Value.Item9;

                    unitOfWork.DbContext.Configuration.AutoDetectChangesEnabled = false;

                    Func<int?, JObject> getPerson = (personApexId) => Utils.GetPerson(personApexId, personRepository, personApexIdToLotId);
                    Func<int?, JObject> getOrganization = (orgApexId) => Utils.GetOrganization(orgApexId, organizationRepository, orgApexIdToLotId);
                    Func<string, JObject> getInspector = (tRegUser) =>
                    {
                        if (string.IsNullOrWhiteSpace(tRegUser))
                        {
                            return null;
                        }

                        if (!inspectorsFM.ContainsKey(tRegUser))
                        {
                            Console.WriteLine("CANNOT FIND INSPECTOR {0}", tRegUser);//TODO
                            return null;
                        }

                        int personId = inspectorsFM[tRegUser];
                        if (personId == -1)
                        {
                            Console.WriteLine("INSPECTOR {0} IS NOT MAPPED TO PERSONID", tRegUser);//TODO
                            return null;
                        }

                        return getPerson(personId);
                    };

                    if (!aircraftFmIdtoLotId.ContainsKey(aircraftFmId))
                    {
                        //TODO remove, those are the ones with duplicate MSN skipped earlier
                        continue;
                    }

                    Lot lot = lotRepository.GetLotIndex(aircraftFmIdtoLotId[aircraftFmId]);

                    var aircraftCertRegistrationsFM = this.getAircraftCertRegistrationsFM(aircraftFmId, noms, getInspector, getPerson, getOrganization);
                    foreach (var aircraftCertRegistrationFM in aircraftCertRegistrationsFM)
                    {
                        var pv = lot.CreatePart("aircraftCertRegistrationsFM/*", aircraftCertRegistrationFM, context);
                        lot.Commit(context, lotEventDispatcher);
                        unitOfWork.Save();
                        
                        var regNom = Utils.ToJObject(
                            new
                            {
                                nomValueId = pv.Part.Index,
                                name = aircraftCertRegistrationFM.Get<int>("certNumber").ToString()
                            });

                        int certId = aircraftCertRegistrationFM["__oldId"].Value<int>();

                        var aircraftCertAirworthinessesFM = this.getAircraftCertAirworthinessesFM(certId, regNom, getInspector);
                        foreach (var aircraftCertAirworthinessFM in aircraftCertAirworthinessesFM)
                        {
                            lot.CreatePart("aircraftCertAirworthinessesFM/*", aircraftCertAirworthinessFM, context);
                        }

                        var aircraftDocumentDebtsFM = this.getAircraftDocumentDebtsFM(certId, regNom, noms, getInspector);
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
            return this.sqlConn.CreateStoreCommand("select n_Act_ID from Acts")
                .Materialize(r => r.Field<string>("n_Act_ID"))
                .ToList();
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
                        ModifOrWingColor = r.Field<string>("t_Act_ModifOrWingColorBg"),
                        ModifOrWingColorAlt = r.Field<string>("t_Act_ModifOrWingColorEn"),
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

        private IList<JObject> getAircraftDocumentDebtsFM(int certId, JObject regNom, Dictionary<string, Dictionary<string, NomValue>> noms, Func<string, JObject> getInspector)
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
                        registration = regNom,
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
            Func<int?, JObject> getPerson,
            Func<int?, JObject> getOrganization)
        {
            Dictionary<string, int> fmOrgToOrgMap = new Dictionary<string, int>
            {
                { "“Бългериан Авиейшън Груп” ЕАД", 447 },
                { "АВИОКОМПАНИЯ  “ХЕМУС  ЕР”", 563 },
                { "БЪЛГАРИЯ  ЕР", 568 },
                { "“България Ер” АД", 568 },
                { "ЕЪРКРАФТ  ЛИИЗ  ЕООД", 1432 },
                { "Еъркрафт Лииз ЕООД\nбул.”Брюксел” 1\n1540 София", 1432 },
                { "“Еър Лазур - Дженерал Авиейшън” ЕООД - Стар адрес", 565 },
                { "ЕЪР  ЛАЗУР-ДЖЕНЕРАЛ  АВИЕЙШЪН", 565 },
                { "УНИКРЕДИТ  ЛИЗИНГ", 467 },
                { "“УниКредит Лизинг” АД\nул. “Златен рог” № 22\nСофия 1407\nБългария", 467 },
                { "“УниКредит Лизинг” АД\nбул. “Цариградско шосе” 40\nСофия 1784\nБългария", 467 },
                { "ЕТ “КЕНТАВЪР - ЕЛИЗАР АТАНАСОВ”", 807 },
            };

            Func<string, Tuple<bool, JObject, JObject>> getOrgOrPerson = (name) =>
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return Tuple.Create(true, (JObject)null, (JObject)null);
                }

                name = name.Trim();

                if (!fmOrgToOrgMap.ContainsKey(name))
                {
                    Console.WriteLine("CANNOT MAP FM ORG {0}", name);//TODO
                    return Tuple.Create(true, (JObject)null, (JObject)null);
                }

                return Tuple.Create(true, getOrganization(fmOrgToOrgMap[name]), (JObject)null);
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
                        owner.tNameBG ownerName,
                        oper.tNameBG operName
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
                        owner = getOrgOrPerson(r.Field<string>("ownerName")),
                        oper = getOrgOrPerson(r.Field<string>("operName")),
                        aircraftCategory = noms["aircraftCategories"].ByCodeOrDefault(r.Field<string>("tCatCode")) ?? noms["aircraftCategories"].ByCode("A2"),//TODO
                        aircraftLimitation = noms["aircraftLimitationsFm"].ByCode(r.Field<string>("nLimitID")),
                        leasingDocNumber = r.Field<string>("tR83_Zapoved"),
                        leasingDocDate = toDate(r.Field<string>("dR83_Data")),
                        leasingLessor = getOrgOrPerson(r.Field<string>("tLessor")),
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
                        r.aircraftCategory,
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

            var lastReg = registrations.Last();
            lastReg["isActive"] = true;
            lastReg["isCurrent"] = true;

            return registrations;
        }

        private IList<JObject> getAircraftCertAirworthinessesFM(int certId, JObject regNom, Func<string, JObject> getInspector)
        {
            return this.sqlConn.CreateStoreCommand(
                @"select * from 
                    (select nRegNum, NumberIssue, dIssue, dFrom, dValid, t_Reviewed_By, tDocCAA,
                            dDocCAA, dDateEASA_25_Issue, d_24_Issue, d_24_Valid, d_ARC_Issue, d_ARC_Valid, t_ARC_RefNo
                    from CofA1 as r1
                        union all
                    select nRegNum, NumberIssue, dIssue, dFrom, dValid, t_Reviewed_By, tDocCAA,
                            dDocCAA, dDateEASA_25_Issue, d_24_Issue, d_24_Valid, d_ARC_Issue, d_ARC_Valid, t_ARC_RefNo
                    from CofA2 as r2) s
                where {0} {1}",
                new DbClause("1=1"),
                new DbClause("and nRegNum = {0}", certId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<string>("nRegNum"),
                        __migrTable = "CofA1, CofA2",
                        registration = regNom,
                        certId = toNum(r.Field<string>("nRegNum")),
                        issueNumber = r.Field<string>("NumberIssue"),
                        issueDate = toDate(r.Field<string>("dIssue")),
                        validfromDate = toDate(r.Field<string>("dFrom")),
                        validtoDate = toDate(r.Field<string>("dValid")),
                        inspector = getInspector(r.Field<string>("t_Reviewed_By")),
                        incomingDocNumber = r.Field<string>("tDocCAA"),
                        incomingDocDate = toDate(r.Field<string>("dDocCAA")),
                        EASA25IssueDate = toDate(r.Field<string>("dDateEASA_25_Issue")),
                        EASA24IssueDate = toDate(r.Field<string>("d_24_Issue")),
                        EASA24IssueValidToDate = toDate(r.Field<string>("d_24_Valid")),
                        EASA15IssueDate = toDate(r.Field<string>("d_ARC_Issue")),
                        EASA15IssueValidToDate = toDate(r.Field<string>("d_ARC_Valid")),
                        EASA15IssueRefNo = r.Field<string>("t_ARC_RefNo")
                    }))
                .ToList();
        }

        private IList<int> getAircraftApexIds()
        {
            return this.oracleConn.CreateStoreCommand("SELECT ID FROM CAA_DOC.AIRCRAFT")
                .Materialize(r => r.Field<int>("ID"))
                .ToList();
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
            Func<int?, JObject> getPerson,
            Func<int?, JObject> getOrganization,
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
                        organization = getOrganization((int?)r.Field<decimal?>("ID_FIRM")),
                        person = getPerson((int?)r.Field<decimal?>("ID_PERSON")),
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
                                    new JProperty("caseType", Utils.DUMMY_PILOT_CASE_TYPE),
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
            Func<int?, JObject> getPerson,
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
                        inspector = getPerson((int?)r.Field<decimal?>("REG_EXAMINER_ID")),
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
                        closeInspector = getPerson((int?)r.Field<decimal?>("CLOSE_EXAMINER_ID")),
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
                                    new JProperty("caseType", Utils.DUMMY_PILOT_CASE_TYPE),
                                    new JProperty("bookPageNumber", bookPageNumber),
                                    new JProperty("pageCount", pageCount),
                                    new JProperty("applications", new JArray(ag.Select(a => a.nomApp))))))))
                    .ToList();
        }

        private IList<JObject> getAircraftMaintenances(
            int aircraftId,
            Func<int?, JObject> getPerson,
            Func<int?, JObject> getOrganization,
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
                        organization = getOrganization((int?)r.Field<decimal?>("ID_FIRM")),
                        person = getPerson((int?)r.Field<decimal?>("ID_PERSON")),
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
                        localTime = new
                        {
                            hours = r.Field<DateTime?>("LOCAL_DATE").Value.Hour,
                            minutes = r.Field<DateTime?>("LOCAL_DATE").Value.Minute
                        },
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
                                    new JProperty("caseType", Utils.DUMMY_PILOT_CASE_TYPE),
                                    new JProperty("bookPageNumber", bookPageNumber),
                                    new JProperty("pageCount", pageCount),
                                    new JProperty("applications", new JArray()))))))
                    .ToList();
        }

        private IList<JObject> getAircraftInspections(
            int aircraftId,
            Dictionary<int, JObject> nomApplications,
            Func<int?, JObject> getPerson,
            Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            var inspectionDisparities = this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM 
                                CAA_DOC.REC_DISPARITY
                                WHERE ID_AUDIT in (SELECT ID FROM CAA_DOC.AUDITS WHERE {0})",
                    new DbClause("ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r =>
                    new
                    {
                        idAudit = r.Field<long?>("ID_AUDIT"),
                        idAuditDetail = r.Field<long?>("ID_AUDIT_DETAIL"),
                        sortOrder = r.Field<decimal?>("SEQ"),
                        refNumber = r.Field<string>("REF_NUMBER"),
                        description = r.Field<string>("DESCRIPTION"),
                        disparityLevel = r.Field<decimal?>("REC_DISPRT_LEVEL"),
                        removalDate = r.Field<DateTime?>("REMOVAL_DATE"),
                        rectifyAction = r.Field<string>("RECTIFY_ACTION"),
                        closureDate = r.Field<DateTime?>("CLOSURE_DATE"),
                        closureDocument = r.Field<string>("CLOSURE_DOC")
                    })
                    .GroupBy(g => g.idAudit)
                    .ToDictionary(g => g.Key, g =>
                        g.GroupBy(k => k.idAuditDetail)
                        .ToDictionary(k => k.Key, k => k.Select(n =>
                            new
                            {
                                n.sortOrder,
                                n.refNumber,
                                n.description,
                                n.disparityLevel,
                                n.removalDate,
                                n.rectifyAction,
                                n.closureDate,
                                n.closureDocument
                            }).ToArray()));

            var inspectionDetails = this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM 
                    CAA_DOC.AUDITS_DETAIL
                    WHERE ID_AUDIT in (SELECT ID FROM CAA_DOC.AUDITS WHERE {0})",
                    new DbClause("ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r =>
                    new
                    {
                        auditResult = noms["auditResults"].ByCode(r.Field<string>("STATE")),
                        auditPartRequirement = noms["auditPartRequirements"].ByOldId(r.Field<long?>("ID_REQUIREMENT").ToString()),
                        disparities = inspectionDisparities.Keys.Contains(r.Field<long?>("ID_AUDIT")) ?
                            (inspectionDisparities[r.Field<long?>("ID_AUDIT")].ContainsKey(r.Field<long?>("ID")) ? inspectionDisparities[r.Field<long?>("ID_AUDIT")][r.Field<long?>("ID")] : null) :
                            null
                    })
                .ToArray();

            var inspectors = this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM 
                    CAA_DOC.AUDITOR
                    WHERE ID_ODIT in (SELECT ID FROM CAA_DOC.AUDITS WHERE {0})",
                    new DbClause("ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r =>
                    new
                    {
                        sortOrder = r.Field<decimal?>("SEQ").ToString(),
                        examinerId = getPerson((int?)r.Field<decimal?>("ID_EXAMINER")),
                    })
                .ToArray();

            return this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AUDITS WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "AUDITS",
                        documentNumber = r.Field<string>("DOC_NUMBER"),
                        auditReason = noms["auditReasons"].ByCode(r.Field<string>("REASON")),
                        auditType = noms["auditTypes"].ByCode(r.Field<string>("AUDIT_MODE")),
                        subject = r.Field<string>("SUBJECT"),
                        auditState = noms["auditStatuses"].ByCode(r.Field<string>("STATE")),
                        notification = noms["boolean"].ByCode(r.Field<string>("NOTIFICATION") == "Y" ? "Y" : "N"),
                        startDate = r.Field<DateTime?>("DATE_BEGIN"),
                        endDate = r.Field<DateTime?>("DATE_END"),
                        inspectionPlace = r.Field<string>("INSPECTION_PLACE"),
                        inspectionFrom = r.Field<DateTime?>("INSPECTION_FROM"),
                        inspectionTo = r.Field<DateTime?>("INSPECTION_TO"),
                        auditDetails = inspectionDetails,
                        examiners = inspectors,
                        applications = (r.Field<decimal?>("ID_REQUEST") != null && nomApplications.ContainsKey(r.Field<int>("ID_REQUEST"))) ?
                            new JArray () { nomApplications[r.Field<int>("ID_REQUEST")] } :
                            null
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

                        __BOOK_PAGE_NO = (int?)r.Field<decimal?>("BOOK_PAGE_NO"),
                        __PAGES_COUNT = (int?)r.Field<decimal?>("PAGES_COUNT"),

                        documentNumber = r.Field<string>("DOC_NO"),
                        documentDate = r.Field<DateTime?>("DOC_DATE"),
                        requestDate = r.Field<DateTime?>("REQUEST_DATE"),
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

                                    "documentNumber",
                                    "documentDate",
                                    "requestDate",
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
                                    new JProperty("caseType", Utils.DUMMY_PILOT_CASE_TYPE),
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
                                    new JProperty("caseType", Utils.DUMMY_PILOT_CASE_TYPE),
                                    new JProperty("bookPageNumber", bookPageNumber),
                                    new JProperty("pageCount", pageCount),
                                    new JProperty("applications", new JArray()))))))
                    .ToList();
        }

        private IList<JObject> getAircraftCertRegistrations(
            int aircraftId,
            Func<int?, JObject> getPerson,
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

                        register = r.Field<long>("ID").ToString().Substring(0, 1),
                        certNumber = r.Field<string>("CERT_NUMBER"),
                        certDate = r.Field<DateTime?>("CERT_DATE"),
                        aircraftNewOld = noms["aircraftPartStatuses"].ByCode(r.Field<string>("NEW_USED")),
                        operationType = noms["aircraftOperTypes"].ByOldId(r.Field<long?>("ID_TYPE_OPER").ToString()),
                        ownerPartIndex = r.Field<int?>("ID_OWNER") != null ? documentOwnersOldIdToPartIndex[r.Field<int?>("ID_OWNER").Value] : (int?)null,
                        operatorPartIndex = r.Field<int?>("ID_OPER") != null ? documentOwnersOldIdToPartIndex[r.Field<int?>("ID_OPER").Value] : (int?)null,
                        inspector = getPerson(r.Field<int?>("ID_EXAMINER_REG")),
                        regnotes = r.Field<string>("NOTES_REG"),
                        paragraph = r.Field<string>("PARAGRAPH"),
                        paragraphAlt = r.Field<string>("PARAGRAPH_TRANS"),
                        removalDate = r.Field<DateTime?>("REMOVAL_DATE"),
                        removalReason = noms["aircraftRemovalReasons"].ByOldId(r.Field<string>("REMOVAL_REASON")),
                        removalText = r.Field<string>("REMOVAL_TEXT"),
                        removalDocumentNumber = r.Field<string>("REMOVAL_DOC_CAA"),
                        removalDocumentDate = r.Field<DateTime?>("REMOVAL_DOC_DATE"),
                        removalInspector = getPerson(r.Field<int?>("REMOVAL_ID_EXAMINER")),
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
                                    new JProperty("caseType", Utils.DUMMY_PILOT_CASE_TYPE),
                                    new JProperty("bookPageNumber", null),
                                    new JProperty("pageCount", null),
                                    new JProperty("applications", new JArray(ag.Select(a => a.nomApp))))))))
                    .ToList();
        }

        private IList<JObject> getAircraftCertAirworthinesses(
            long certId,
            Dictionary<int, int> inspections,
            Func<int?, JObject> getPerson,
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
                        inspector = getPerson((int?)r.Field<decimal?>("ID_EXAMINER")),
                        valid = noms["boolean"].ByCode(r.Field<string>("VALID_YN") == "Y" ? "Y" : "N"),
                        firstAmmendment = new
                        {
                            issueDate = r.Field<DateTime?>("EXT1_DATE"),
                            validToDate = r.Field<DateTime?>("EXT1_VALID_TO"),
                            approvalId = r.Field<long?>("EXT1_APPROVAL_ID"),//TODO
                            inspector = getPerson((int?)r.Field<decimal?>("EXT1_ID_EXAMINER")),
                        },
                        secondAmmendment = new
                        {
                            issueDate = r.Field<DateTime?>("EXT2_DATE"),
                            validToDate = r.Field<DateTime?>("EXT2_VALID_TO"),
                            approvalId = r.Field<long?>("EXT2_APPROVAL_ID"),//TODO
                            inspector = getPerson((int?)r.Field<decimal?>("EXT2_ID_EXAMINER")),
                        },
                        export = new {
                            country = noms["countries"].ByOldId(r.Field<decimal?>("ID_COUNTRY_TRANSFER").ToString()),
                            exceptions = r.Field<string>("EXEMPTIONS"),
                            exceptionsAlt = r.Field<string>("EXEMPTIONS_TRANS"),
                            special = r.Field<string>("SPECIAL_REQ"),
                            specialAlt = r.Field<string>("SPECIAL_REQ_TRANS"),
                        },
                        revokeDate = r.Field<DateTime?>("REVOKE_DATE"),
                        revokeinspector = getPerson((int?)r.Field<decimal?>("REVOKE_ID_EXAMINER")),
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
                                    new JProperty("caseType", Utils.DUMMY_PILOT_CASE_TYPE),
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
                                    new JProperty("caseType", Utils.DUMMY_PILOT_CASE_TYPE),
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
                                    new JProperty("caseType", Utils.DUMMY_PILOT_CASE_TYPE),
                                    new JProperty("bookPageNumber", null),
                                    new JProperty("pageCount", null),
                                    new JProperty("applications", new JArray(ag.Select(a => a.nomApp))))))))
                    .ToList();
        }

        private IList<JObject> getAircraftCertPermitsToFly(long certId, Dictionary<int, JObject> nomApplications)
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
                        valitoDate = r.Field<DateTime?>("VALID_TO"),
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
                                    new JProperty("caseType", Utils.DUMMY_PILOT_CASE_TYPE),
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
