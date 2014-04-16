using Common.Api.UserContext;
using Common.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oracle.DataAccess.Client;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gva.MigrationTool.Nomenclatures;
using Common.Api.Models;
using Docs.Api.Models;
using Gva.Api.Models;
using Gva.Api.LotEventHandlers.PersonView;
using Gva.Api.LotEventHandlers.OrganizationView;
using Gva.Api.LotEventHandlers.InventoryView;
using Gva.Api.LotEventHandlers.ApplicationView;
using Common.Api.Repositories.UserRepository;
using Regs.Api.LotEvents;
using System.Data.SqlClient;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.ModelsDO;
using Common.Json;
using Gva.Api.LotEventHandlers.AircraftView;
using Gva.Api.LotEventHandlers.AirportView;
using Gva.Api.LotEventHandlers.EquipmentView;
using Gva.Api.Repositories.AircraftRepository;

namespace Gva.MigrationTool.Sets
{
    class Aircraft
    {
        public static Dictionary<string, Dictionary<string, NomValue>> noms;

        public static void migrateAircrafts(OracleConnection oracleCon, SqlConnection sqlCon, Dictionary<string, Dictionary<string, NomValue>> n)
        {

            noms = n;
            var context = new UserContext(2);
            Dictionary<string, int> apexAircraftsIdsMsn = new Dictionary<string, int>();


            var aircraftIds = Aircraft.getAircraftIds(oracleCon);
            foreach (var aircraftId in aircraftIds)
            {
                if (aircraftId >= 200)
                    break;

                using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
                {
                    var lotRepository = new LotRepository(unitOfWork);
                    var userRepository = new UserRepository(unitOfWork);
                    var fileRepository = new FileRepository(unitOfWork);
                    var applicationRepository = new ApplicationRepository(unitOfWork);
                    var aircraftRegistrationAwRepository = new AircraftRegistrationAwRepository(unitOfWork);

                    var lotEventDispatcher = new LotEventDispatcher(new List<ILotEventHandler>()
                    {
                        new AircraftRegistrationAwHandler(unitOfWork),
                        new AircraftRegistrationHandler(unitOfWork, aircraftRegistrationAwRepository),
                        new AircraftRegistrationNewAwHandler(unitOfWork),
                        new AircraftRegistrationNumberHandler(unitOfWork),
                        new AircraftViewDataHandler(unitOfWork),
                        new AirportDataHandler(unitOfWork),
                        new ApplicationsViewAircraftHandler(unitOfWork),
                        new ApplicationsViewAirportHandler(unitOfWork),
                        new ApplicationsViewEquipmentHandler(unitOfWork),
                        new ApplicationsViewOrganizationHandler(unitOfWork),
                        new ApplicationsViewPersonHandler(unitOfWork),
                        new EquipmentDataHandler(unitOfWork),
                        new AircraftApplicationHandler(unitOfWork, userRepository),
                        new AircraftDebtHandler(unitOfWork, userRepository),
                        new AircraftInspectionHandler(unitOfWork, userRepository),
                        new AircraftOccurrenceHandler(unitOfWork, userRepository),
                        new AircraftOtherHandler(unitOfWork, userRepository),
                        new AircraftOwnerHandler(unitOfWork, userRepository),
                        new AirportApplicationHandler(unitOfWork, userRepository),
                        new AirportInspectionHandler(unitOfWork, userRepository),
                        new AirportOtherHandler(unitOfWork, userRepository),
                        new AirportOwnerHandler(unitOfWork, userRepository),
                        new EquipmentApplicationHandler(unitOfWork, userRepository),
                        new EquipmentInspectionHandler(unitOfWork, userRepository),
                        new EquipmentOtherHandler(unitOfWork, userRepository),
                        new EquipmentOwnerHandler(unitOfWork, userRepository),
                        new OrganizationApplicationHandler(unitOfWork, userRepository),
                        new OrganizationOtherHandler(unitOfWork, userRepository),
                        new PersonApplicationHandler(unitOfWork, userRepository),
                        new PersonCheckHandler(unitOfWork, userRepository),
                        new PersonDocumentIdHandler(unitOfWork, userRepository),
                        new PersonEducationHandler(unitOfWork, userRepository),
                        new PersonEmploymentHandler(unitOfWork, userRepository),
                        new PersonMedicalHandler(unitOfWork, userRepository),
                        new PersonOtherHandler(unitOfWork, userRepository),
                        new PersonTrainingHandler(unitOfWork, userRepository),
                        new OrganizationViewDataHandler(unitOfWork),
                        new PersonViewDataHandler(unitOfWork),
                        new PersonViewEmploymentHandler(unitOfWork),
                        new PersonViewLicenceHandler(unitOfWork),
                        new PersonViewRatingHandler(unitOfWork)
                    });

                    var list = new List<string>()
                    {
                        "0103",
                        "011",
                        "0922",
                        "1087",
                        "174710",
                        "174725",
                        "421",
                        "422",
                        "5602",
                        "6001",
                        "8007",
                        "9002",
                        "W-571"
                    };
                    var aircraftData = Aircraft.getAircraftData(oracleCon, aircraftId);
                    if (list.Contains(aircraftData["manSN"].Value<string>()))
                    {
                        continue;
                    }

                    Set aircraftSet = lotRepository.GetSet("Aircraft");

                    unitOfWork.DbContext.Configuration.AutoDetectChangesEnabled = false;
                    var lot = aircraftSet.CreateLot(context);

                    
                    lot.CreatePart("aircraftDataApex", aircraftData, context);

                    lot.Commit(context, lotEventDispatcher);
                    unitOfWork.Save();

                    var manSN = aircraftData["manSN"].Value<string>();
                    apexAircraftsIdsMsn.Add(manSN, lot.LotId);

                    var aircraftParts = Aircraft.getAircraftParts(oracleCon, aircraftId);
                    foreach (var aircraftPart in aircraftParts)
                    {
                        lot.CreatePart("aircraftParts/*", aircraftPart, context);
                    }

                    var aircraftMaintenances = Aircraft.getAircraftMaintenances(oracleCon, aircraftId);
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
                    var aircraftDocumentApplications = Aircraft.getAircraftDocumentApplications(oracleCon, aircraftId);
                    foreach (var aircraftDocumentApplication in aircraftDocumentApplications)
                    {
                        var pv = addPartWithFiles("aircraftDocumentApplications/*", aircraftDocumentApplication);
                        //lot.CreatePart("aircraftDocumentApplications/*", aircraftDocumentApplication, context);

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
                                        PartIndex = pv.Part.Index.Value,
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

                    var aircraftDocumentOccurrences = Aircraft.getAircraftDocumentOccurrences(oracleCon, aircraftId, nomApplications);
                    foreach (var aircraftDocumentOccurrence in aircraftDocumentOccurrences)
                    {
                        addPartWithFiles("documentOccurrences/*", aircraftDocumentOccurrence);
                    }

                    var aircraftDocumentDebts = Aircraft.getAircraftDocumentDebts(oracleCon, aircraftId, nomApplications);
                    foreach (var aircraftDocumentDebt in aircraftDocumentDebts)
                    {
                        addPartWithFiles("aircraftDocumentDebts/*", aircraftDocumentDebt);
                    }

                    var aircraftDocumentOwners = Aircraft.getAircraftDocumentOwners(oracleCon, aircraftId, nomApplications);
                    foreach (var aircraftDocumentOwner in aircraftDocumentOwners)
                    {
                        addPartWithFiles("aircraftDocumentOwners/*", aircraftDocumentOwner);
                    }

                    var aircraftDocumentOthers = Aircraft.getAircraftDocumentOthers(oracleCon, aircraftId, nomApplications);
                    foreach (var aircraftDocumentOther in aircraftDocumentOthers)
                    {
                        addPartWithFiles("aircraftDocumentOthers/*", aircraftDocumentOther);
                    }

                    Dictionary<int, int> inspections = new Dictionary<int, int>();

                    var aircraftInspections = Aircraft.getAircraftInspections(oracleCon, aircraftId, nomApplications);
                    foreach (var aircraftInspection in aircraftInspections)
                    {
                        var pv = lot.CreatePart("inspections/*", aircraftInspection, context);
                        inspections.Add(aircraftInspection["__oldId"].Value<int>(), pv.Part.Index.Value);
                    }

                    var aircraftCertRegistrations = Aircraft.getAircraftCertRegistrations(oracleCon, aircraftId);
                    foreach (var aircraftCertRegistration in aircraftCertRegistrations)
                    {
                        lot.CreatePart("aircraftCertRegistrations/*", aircraftCertRegistration, context);

                        int certId = aircraftCertRegistration["__oldId"].Value<int>();

                        var aircraftCertAirworthinesses = Aircraft.getAircraftCertAirworthinesses(oracleCon, certId, inspections);
                        foreach (var aircraftCertAirworthiness in aircraftCertAirworthinesses)
                        {
                            lot.CreatePart("aircraftCertAirworthinesses/*", aircraftCertAirworthiness, context);
                        }

                        var aircraftCertPermitsToFly = Aircraft.getAircraftCertPermitsToFly(oracleCon, certId);
                        foreach (var aircraftCertPermitToFly in aircraftCertPermitsToFly)
                        {
                            lot.CreatePart("aircraftCertPermitsToFly/*", aircraftCertPermitToFly, context);
                        }

                        var aircraftCertNoises = Aircraft.getAircraftCertNoises(oracleCon, certId);
                        foreach (var aircraftCertNoise in aircraftCertNoises)
                        {
                            lot.CreatePart("aircraftCertNoises/*", aircraftCertNoise, context);
                        }
                    }

                    var aircraftCertMarks = Aircraft.getAircraftCertMarks(oracleCon, aircraftId);
                    foreach (var aircraftCertMark in aircraftCertMarks)
                    {
                        lot.CreatePart("aircraftCertMarks/*", aircraftCertMark, context);
                    }

                    var aircraftCertSmods = Aircraft.getAircraftCertSmods(oracleCon, aircraftId);
                    foreach (var aircraftCertSmod in aircraftCertSmods)
                    {
                        lot.CreatePart("aircraftCertSmods/*", aircraftCertSmod, context);
                    }


                    lot.Commit(context, lotEventDispatcher);

                    unitOfWork.Save();

                    Console.WriteLine("Migrated APEX aircraftId: {0} manSN: {1}", aircraftId, manSN);
                }
            }

            var aircraftIdsMsnsFM = getAircraftIdsMsnsFM(sqlCon);
            foreach (var aircraftIdMsnFM in aircraftIdsMsnsFM)
            {
                using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
                {
                    var lotRepository = new LotRepository(unitOfWork);
                    var userRepository = new UserRepository(unitOfWork);
                    var fileRepository = new FileRepository(unitOfWork);
                    var applicationRepository = new ApplicationRepository(unitOfWork);
                    var aircraftRegistrationAwRepository = new AircraftRegistrationAwRepository(unitOfWork);

                    var lotEventDispatcher = new LotEventDispatcher(new List<ILotEventHandler>()
                    {
                        new AircraftRegistrationAwHandler(unitOfWork),
                        new AircraftRegistrationHandler(unitOfWork, aircraftRegistrationAwRepository),
                        new AircraftRegistrationNewAwHandler(unitOfWork),
                        new AircraftRegistrationNumberHandler(unitOfWork),
                        new AircraftViewDataHandler(unitOfWork),
                        new AirportDataHandler(unitOfWork),
                        new ApplicationsViewAircraftHandler(unitOfWork),
                        new ApplicationsViewAirportHandler(unitOfWork),
                        new ApplicationsViewEquipmentHandler(unitOfWork),
                        new ApplicationsViewOrganizationHandler(unitOfWork),
                        new ApplicationsViewPersonHandler(unitOfWork),
                        new EquipmentDataHandler(unitOfWork),
                        new AircraftApplicationHandler(unitOfWork, userRepository),
                        new AircraftDebtHandler(unitOfWork, userRepository),
                        new AircraftInspectionHandler(unitOfWork, userRepository),
                        new AircraftOccurrenceHandler(unitOfWork, userRepository),
                        new AircraftOtherHandler(unitOfWork, userRepository),
                        new AircraftOwnerHandler(unitOfWork, userRepository),
                        new AirportApplicationHandler(unitOfWork, userRepository),
                        new AirportInspectionHandler(unitOfWork, userRepository),
                        new AirportOtherHandler(unitOfWork, userRepository),
                        new AirportOwnerHandler(unitOfWork, userRepository),
                        new EquipmentApplicationHandler(unitOfWork, userRepository),
                        new EquipmentInspectionHandler(unitOfWork, userRepository),
                        new EquipmentOtherHandler(unitOfWork, userRepository),
                        new EquipmentOwnerHandler(unitOfWork, userRepository),
                        new OrganizationApplicationHandler(unitOfWork, userRepository),
                        new OrganizationOtherHandler(unitOfWork, userRepository),
                        new PersonApplicationHandler(unitOfWork, userRepository),
                        new PersonCheckHandler(unitOfWork, userRepository),
                        new PersonDocumentIdHandler(unitOfWork, userRepository),
                        new PersonEducationHandler(unitOfWork, userRepository),
                        new PersonEmploymentHandler(unitOfWork, userRepository),
                        new PersonMedicalHandler(unitOfWork, userRepository),
                        new PersonOtherHandler(unitOfWork, userRepository),
                        new PersonTrainingHandler(unitOfWork, userRepository),
                        new OrganizationViewDataHandler(unitOfWork),
                        new PersonViewDataHandler(unitOfWork),
                        new PersonViewEmploymentHandler(unitOfWork),
                        new PersonViewLicenceHandler(unitOfWork),
                        new PersonViewRatingHandler(unitOfWork)
                    });

                    Lot lot;
                    Set aircraftSet = lotRepository.GetSet("Aircraft");

                    if (apexAircraftsIdsMsn.Where(a => a.Key == aircraftIdMsnFM.Item2).Any())
                    {
                        lot = lotRepository.GetLotIndex(apexAircraftsIdsMsn[aircraftIdMsnFM.Item2]);
                    }
                    else
                    {
                        continue;
                        Console.WriteLine("Unknown aircraft with id - {0}, msn - {1}", aircraftIdMsnFM.Item1, aircraftIdMsnFM.Item2);
                        lot = aircraftSet.CreateLot(context);
                    }

                    var aircraftDataFM = Aircraft.getAircraftDataFM(sqlCon, aircraftIdMsnFM.Item1);
                    lot.CreatePart("aircraftData", aircraftDataFM, context);
                    lot.Commit(context, lotEventDispatcher);
                    unitOfWork.Save();

                    var aircraftCertRegistrationsFM = Aircraft.getAircraftCertRegistrationsFM(sqlCon, aircraftIdMsnFM.Item1);
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

                        var aircraftCertAirworthinessesFM = Aircraft.getAircraftCertAirworthinessesFM(sqlCon, certId, regNom);
                        foreach (var aircraftCertAirworthinessFM in aircraftCertAirworthinessesFM)
                        {
                            lot.CreatePart("aircraftCertAirworthinessesFM/*", aircraftCertAirworthinessFM, context);
                        }

                        var aircraftDocumentDebtsFM = Aircraft.getAircraftDocumentDebtsFM(sqlCon, certId, regNom);
                        foreach (var aircraftDocumentDebtFM in aircraftDocumentDebtsFM)
                        {
                            try
                            {
                                lot.CreatePart("aircraftDocumentDebtsFM/*", aircraftDocumentDebtFM, context);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Error CreatePart aircraftDocumentDebtsFM {0}", e.Message);
                                throw e;
                            }
                        }
                    }

                    var aircraftCertNoisesFM = Aircraft.getAircraftCertNoisesFM(sqlCon, aircraftIdMsnFM.Item1);
                    foreach (var aircraftCertNoiseFM in aircraftCertNoisesFM)
                    {
                        lot.CreatePart("aircraftCertNoisesFM/*", aircraftCertNoiseFM, context);
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

                    Console.WriteLine("Migrated FM aircraftIdMsnFM: {0}", aircraftIdMsnFM);
                }
            }
        }

        public static bool checkValue(string val)
        {
            return !String.IsNullOrEmpty(val) && val.Trim() != "n/a" && val.Trim() != "n / a";
        }

        public static int? toNum(string val)
        {
            int value;
            if (checkValue(val) && Int32.TryParse(val.Trim(), out value))
            {
                return value;
            }

            return null;
        }

        public static decimal? toDecimal(string val)
        {
            decimal value;
            if(checkValue(val) && decimal.TryParse(val.Trim(), out value))
            {
                return value;
            }

            return null;
        }

        public static DateTime? toDate(string val)
        {
            DateTime value;
            if(checkValue(val) && DateTime.TryParseExact(val.Trim(), "d.M.yyyy", null, System.Globalization.DateTimeStyles.None, out value))
            {
                return value;
            }

            return null;
        }

        public static IList<Tuple<string, string>> getAircraftIdsMsnsFM(SqlConnection sqlCon)
        {
            return sqlCon.CreateStoreCommand("select * from Acts")
                .Materialize(r => new Tuple<string, string>(r.Field<string>("n_Act_ID"), r.Field<string>("t_Act_MSN")))
                    .ToList();
        }



        public static JObject getAircraftDataFM(SqlConnection sqlCon, string aircraftId)
        {
            return sqlCon.CreateStoreCommand(
                @"select * from Acts where {0} {1}",
                new DbClause("1=1"),
                new DbClause("and n_Act_ID = {0}", aircraftId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = r.Field<string>("n_Act_ID"),
                        __migrTable = "Acts",
                        aircraftProducer = noms["aircraftProducers"].ByOldId(r.Field<string>("n_Act_Maker_ID")),
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
                        CofATypeId = r.Field<string>("t_CofA_Type"),//TODO
                        EASATypeId = r.Field<string>("t_Act_EASA_Type"),//TODO
                        EURegTypeId = r.Field<string>("t_Act_EU_RU"),//TODO
                        EASACategoryId = r.Field<string>("t_EASA_Category"),//TODO
                        tcds = r.Field<string>("t_EASA_TCDS")
                    })))
                .Single();
        }

        public static IList<JObject> getAircraftDocumentDebtsFM(SqlConnection sqlCon, int certId, JObject regNom)
        {
            return sqlCon.CreateStoreCommand(
                @"select * from Morts where {0} {1}",
                new DbClause("1=1"),
                new DbClause("and RegNo = {0}", certId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = r.Field<string>("nRecNo"),
                        __migrTable = "Morts",
                        registration = regNom,
                        certId = toNum(r.Field<string>("RegNo")),
                        regDate = toDate(r.Field<string>("Date")),
                        aircraftDebtType = new { name = "ЗАЛОГ - УЧРЕДЕН" },// r.Field<string>("Action"),//TODO
                        documentNumber = r.Field<string>("gt_DocCAA"),
                        documentDate = r.Field<string>("gd_DocCAA"),
                        aircraftCreditorId = r.Field<string>("Creditor"),//TODO
                        creditorDocument = r.Field<string>("Doc Creditor"),
                        inspectorId = r.Field<string>("tUser")//TODO
                    })))
                .ToList();
        }

        public static IList<JObject> getAircraftCertRegistrationsFM(SqlConnection sqlCon, string aircraftId)
        {
            return sqlCon.CreateStoreCommand(
                @"select * from 
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
                where {0} {1}",
                new DbClause("1=1"),
                new DbClause("and nActID = {0}", aircraftId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = r.Field<string>("nRegNum"),
                        __migrTable = "Reg1, Reg2",
                        register = noms["registers"].Values.First(), //TODO r.Field<int>("regNumber"),
                        certNumber = toNum(r.Field<string>("nRegNum")),
                        certDate = toDate(r.Field<string>("dRegDate")),
                        regMark = r.Field<string>("tRegMark"),
                        incomingDocNumber = r.Field<string>("tDocCAA"),
                        incomingDocDate = toDate(r.Field<string>("dDateCAA")),
                        incomingDocDesc = r.Field<string>("tDocOther"),
                        inspectorId = r.Field<string>("tRegUser"),//TODO
                        ownerId = r.Field<string>("nOwner"),//TODO
                        operatorId = r.Field<string>("nOper"),//TODO
                        aircraftCategory = noms["aircraftCategories"].ByCodeOrDefault(r.Field<string>("tCatCode")) ?? noms["aircraftCategories"].ByCode("A2"),//TODO
                        aircraftLimitation = noms["aircraftLimitationsFm"].ByCode(r.Field<string>("nLimitID")),
                        leasingDocNumber = r.Field<string>("tR83_Zapoved"),
                        leasingDocDate = toDate(r.Field<string>("dR83_Data")),
                        leasingLessor = r.Field<string>("tLessor"),
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
                        removalDate = toDate(r.Field<string>("dDeRegDate")),
                        removalReason = r.Field<string>("tDeDocOther"),
                        removalText = r.Field<string>("tRemarkDeReg"),
                        removalDocumentNumber = r.Field<string>("tDeDocCAA"),
                        removalDocumentDate = toDate(r.Field<string>("dDeDateCAA")),
                        removalInspectorId = r.Field<string>("tDeUser")//TODO
                    })))
                .ToList();
        }

        public static IList<JObject> getAircraftCertAirworthinessesFM(SqlConnection sqlCon, int certId, JObject regNom)
        {
            return sqlCon.CreateStoreCommand(
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
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
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
                        inspectorId = r.Field<string>("t_Reviewed_By"),//TODO
                        incomingDocNumber = r.Field<string>("tDocCAA"),
                        incomingDocDate = toDate(r.Field<string>("dDocCAA")),
                        EASA25IssueDate = toDate(r.Field<string>("dDateEASA_25_Issue")),
                        EASA24IssueDate = toDate(r.Field<string>("d_24_Issue")),
                        EASA24IssueValidToDate = toDate(r.Field<string>("d_24_Valid")),
                        EASA15IssueDate = toDate(r.Field<string>("d_ARC_Issue")),
                        EASA15IssueValidToDate = toDate(r.Field<string>("d_ARC_Valid")),
                        EASA15IssueRefNo = r.Field<string>("t_ARC_RefNo")
                    })))
                .ToList();
        }

        public static IList<JObject> getAircraftCertNoisesFM(SqlConnection sqlCon, string aircraftId)
        {
            return sqlCon.CreateStoreCommand(
                @"select * from Acts where {0} {1}",
                new DbClause("1=1"),
                new DbClause("and n_Act_ID = {0}", aircraftId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = r.Field<string>("n_Act_ID"),
                        __migrTable = "Acts",
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
                        notes = r.Field<string>("t_Noise_Remark"),
                    })))
                .ToList();
        }

        public static IList<int> getAircraftIds(OracleConnection oracleCon)
        {
            return oracleCon.CreateStoreCommand("SELECT ID FROM CAA_DOC.AIRCRAFT")
                .Materialize(r => (int)r.Field<long>("ID"))
                    .ToList();
        }

        public static JObject getAircraftData(OracleConnection oracleCon, int aircraftId)
        {
            return oracleCon.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AIRCRAFT WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID = {0}", aircraftId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = (int)r.Field<long>("ID"),
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
                    })))
                .Single();
        }

        public static IList<JObject> getAircraftDocumentOwners(OracleConnection oracleCon, int aircraftId, Dictionary<int, JObject> nomApplications)
        {
            var parts = oracleCon.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AC_OWNER WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = (int)r.Field<long>("ID"),
                        __migrTable = "AC_OWNER",

                        __BOOK_PAGE_NO = (int?)r.Field<decimal?>("BOOK_PAGE_NO"),
                        __PAGES_COUNT = (int?)r.Field<decimal?>("PAGES_COUNT"),

                        aircraftRelation = noms["aircraftRelations"].ByOldId(r.Field<long?>("TYPE_RELATION").ToString()),
                        organizationId = r.Field<decimal?>("ID_FIRM"),//TODO
                        personId = r.Field<decimal?>("ID_PERSON"),//TODO
                        documentNumber = r.Field<string>("DOC_NUM"),
                        documentDate = r.Field<DateTime?>("DOC_DATE"),
                        fromDate = r.Field<DateTime?>("DATE_FROM"),
                        toDate = r.Field<DateTime?>("DATE_TO"),
                        reasonTerminate = r.Field<string>("REASON_TERMINATE"),
                        notes = r.Field<string>("NOTES"),
                    }))
                .ToList();

            var files = oracleCon.CreateStoreCommand(
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
                        __oldId = (int)r.Field<decimal>("DOC_ID"),
                        __migrTable = "DOCLIB_DOCUMENTS",
                        __AC_OWNER_ID = (int)r.Field<long>("ID"),

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
                                    "organizationId",
                                    "personId",
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

        public static IList<JObject> getAircraftParts(OracleConnection oracleCon, int aircraftId)
        {
            return oracleCon.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AC_PART WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = (int)r.Field<long>("ID"),
                        __migrTable = "AC_PART",
                        aircraftPart = noms["aircraftParts"].ByOldId(r.Field<long?>("PART_TYPE").ToString()),
                        partProducerId = r.Field<long?>("ID_MANUFACTURER"),//TODO
                        model = r.Field<string>("PART_MODEL"),
                        modelAlt = r.Field<string>("PART_MODEL_TRANS"),
                        sn = r.Field<string>("SN"),
                        count = r.Field<decimal?>("PART_NUMBER"),
                        aircraftPartStatus = noms["aircraftPartStatuses"].ByOldId(r.Field<string>("NEW_YN").ToString()),
                        manDate = r.Field<DateTime?>("MAN_DATE"),
                        manPlace = r.Field<string>("MAN_PLACE"),
                        description = r.Field<string>("DESCRIPTION"),
                    })))
                .ToList();
        }

        public static IList<JObject> getAircraftDocumentDebts(OracleConnection oracleCon, int aircraftId, Dictionary<int, JObject> nomApplications)
        {
            var parts = oracleCon.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AC_DEBT WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = (int)r.Field<long>("ID"),
                        __migrTable = "AC_DEBT",

                        __BOOK_PAGE_NO = (int?)r.Field<decimal?>("BOOK_PAGE_NO"),
                        __PAGES_COUNT = (int?)r.Field<decimal?>("PAGES_COUNT"),

                        aircraftDebtType = noms["aircraftDebtTypes"].ByOldId(r.Field<string>("REC_TYPE").ToString()),
                        regDate = r.Field<DateTime?>("REG_DATE"),
                        contractNumber = r.Field<string>("CONTRACT_NUM"),
                        contractDate = r.Field<DateTime?>("CONTRACT_DATE"),
                        startDate = r.Field<DateTime?>("START_DATE"),
                        inspectorId = r.Field<decimal?>("REG_EXAMINER_ID"),//TODO
                        startReason = r.Field<string>("START_REASON"),
                        startReasonAlt = r.Field<string>("START_REASON_TRANS"),
                        notes = r.Field<string>("NOTES"),
                        ltrNumber = r.Field<string>("LTR_CAA_NOM"),
                        ltrDate = r.Field<DateTime?>("LTR_CAA_DATE"),
                        ownerId = r.Field<long?>("ID_OWNER"),//TODO
                        operatorId = r.Field<long?>("ID_OPER"),//TODO
                        endReason = r.Field<string>("END_REASON"),
                        endDate = r.Field<DateTime?>("END_DATE"),
                        closeAplicationNumber = r.Field<string>("CLOSE_APPL_DOC"),
                        closeAplicationDate = r.Field<DateTime?>("CLOSE_APPL_DATE"),
                        closeCaaAplicationNumber = r.Field<string>("CLOSE_CAA_DOC"),
                        closeCaaAplicationDate = r.Field<DateTime?>("CLOSE_CAA_DATE"),
                        closeInspectorId = r.Field<decimal?>("CLOSE_EXAMINER_ID"),//TODO
                        creditorName = r.Field<string>("CREDITOR_NAME"),
                        creditorNameAlt = r.Field<string>("CREDITOR_NAME_TRANS"),
                        creditorAddress = r.Field<string>("CREDITOR_ADRES"),
                        creditorEmail = r.Field<string>("CREDITOR_MAIL"),
                        creditorContact = r.Field<string>("CREDITOR_PERSON"),
                        creditorPhone = r.Field<string>("CREDITOR_TEL")
                    }))
                .ToList();

            var files = oracleCon.CreateStoreCommand(
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
                        __oldId = (int)r.Field<decimal>("DOC_ID"),
                        __migrTable = "DOCLIB_DOCUMENTS",
                        __DEBT_ID = (int)r.Field<long>("ID"),

                        key = Utils.DUMMY_FILE_KEY,//TODO
                        name = r.Field<string>("NAME"),
                        mimeType = r.Field<string>("MIME_TYPE")
                    }))
                .ToList();

            var apps = oracleCon.CreateStoreCommand(
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
                        aircraftDebtId = (int)r.Field<long>("AC_DEBT_ID"),
                        nomApp = nomApplications[(int)r.Field<decimal>("REQUEST_ID")]
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
                                    "inspectorId",
                                    "startReason",
                                    "startReasonAlt",
                                    "notes",
                                    "ltrNumber",
                                    "ltrDate",
                                    "ownerId",
                                    "operatorId",
                                    "endReason",
                                    "endDate",
                                    "closeAplicationNumber",
                                    "closeAplicationDate",
                                    "closeCaaAplicationNumber",
                                    "closeCaaAplicationDate",
                                    "closeInspectorId",
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

        public static IList<JObject> getAircraftMaintenances(OracleConnection oracleCon, int aircraftId)
        {
            return oracleCon.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AC_MAINTENANCE WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = (int)r.Field<long>("ID"),
                        __migrTable = "AC_MAINTENANCE",
                        lim145limitation = noms["lim145limitations"].ByOldId(r.Field<long?>("ID_MF145_LIMIT").ToString()),
                        notes = r.Field<string>("REMARKS"),
                        fromDate = r.Field<DateTime?>("DATE_FROM"),
                        toDate = r.Field<DateTime?>("DATE_TO"),
                        organizationId = r.Field<decimal?>("ID_FIRM"),//TODO
                        personId = r.Field<decimal?>("ID_PERSON"),//TODO
                    })))
                .ToList();
        }

        public static IList<JObject> getAircraftDocumentOccurrences(OracleConnection oracleCon, int aircraftId, Dictionary<int, JObject> nomApplications)
        {
            var parts = oracleCon.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AC_OCCURRENCE WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_AC = {0}", aircraftId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = (int)r.Field<long>("ID"),
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

            var files = oracleCon.CreateStoreCommand(
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
                        __oldId = (int)r.Field<decimal>("DOC_ID"),
                        __migrTable = "DOCLIB_DOCUMENTS",
                        __OCCURRENCE_ID = (int)r.Field<long>("ID"),

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

        public static IList<JObject> getAircraftInspections(OracleConnection oracleCon, int aircraftId, Dictionary<int, JObject> nomApplications)
        {
            var inspectionDisparities = oracleCon.CreateStoreCommand(
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

            var inspectionDetails = oracleCon.CreateStoreCommand(
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

            var inspectors = oracleCon.CreateStoreCommand(
                @"SELECT * FROM 
                    CAA_DOC.AUDITOR
                    WHERE ID_ODIT in (SELECT ID FROM CAA_DOC.AUDITS WHERE {0})",
                    new DbClause("ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r =>
                    new
                    {
                        sortOrder = r.Field<decimal?>("SEQ").ToString(),
                        examinerId = r.Field<decimal?>("ID_EXAMINER")//TODO
                    })
                .ToArray();

            return oracleCon.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AUDITS WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = (int)r.Field<long>("ID"),
                        __migrTable = "AUDITS",
                        documentNumber = r.Field<string>("DOC_NUMBER"),
                        auditReason = noms["auditReasons"].ByCode(r.Field<string>("REASON")),
                        auditType = noms["auditTypes"].ByCode(r.Field<string>("AUDIT_MODE")),
                        subject = r.Field<string>("SUBJECT"),
                        auditState = noms["auditStatuses"].ByCode(r.Field<string>("STATE")),
                        notification = r.Field<string>("NOTIFICATION"),
                        startDate = r.Field<DateTime?>("DATE_BEGIN"),
                        endDate = r.Field<DateTime?>("DATE_END"),
                        inspectionPlace = r.Field<string>("INSPECTION_PLACE"),
                        inspectionFrom = r.Field<DateTime?>("INSPECTION_FROM"),
                        inspectionTo = r.Field<DateTime?>("INSPECTION_TO"),
                        auditDetails = inspectionDetails,
                        examiners = inspectors,
                        applications = (r.Field<decimal?>("ID_REQUEST") != null && nomApplications.ContainsKey((int)r.Field<decimal?>("ID_REQUEST"))) ?
                            nomApplications[(int)r.Field<decimal?>("ID_REQUEST")] :
                            null
                    })))
                .ToList();
        }

        public static IList<JObject> getAircraftDocumentApplications(OracleConnection oracleCon, int aircraftId)
        {
            var parts = oracleCon.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.REQUEST WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and APPLICANT_AC_ID = {0}", aircraftId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
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

            var files = oracleCon.CreateStoreCommand(
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
                        __oldId = (int)r.Field<decimal>("DOC_ID"),
                        __migrTable = "DOCLIB_DOCUMENTS",

                        __REQUEST_ID = (int)r.Field<decimal>("ID"),

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

        public static IList<JObject> getAircraftDocumentOthers(OracleConnection oracleCon, int aircraftId, Dictionary<int, JObject> nomApplications)
        {
            var parts = oracleCon.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.PERSON_DOCUMENT WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and AC_ID = {0}", aircraftId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
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

            var files = oracleCon.CreateStoreCommand(
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
                        __oldId = (int)r.Field<decimal>("DOC_ID"),
                        __migrTable = "DOCLIB_DOCUMENTS",
                        __AIRCRAFT_DOCUMENT_ID = (int)r.Field<decimal>("ID"),

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

        public static IList<JObject> getAircraftCertRegistrations(OracleConnection oracleCon, int aircraftId)
        {
            return oracleCon.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AC_CERTIFICATE WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = (int)r.Field<long>("ID"),
                        __migrTable = "AC_CERTIFICATE",
                        register = r.Field<long>("ID").ToString().Substring(0, 1),
                        certNumber = r.Field<string>("CERT_NUMBER"),
                        certDate = r.Field<DateTime?>("CERT_DATE"),
                        aircraftNewOld = noms["aircraftPartStatuses"].ByCode(r.Field<string>("NEW_USED")),
                        operationType = noms["aircraftOperTypes"].ByOldId(r.Field<long?>("ID_TYPE_OPER").ToString()),
                        ownerId = noms["documentRoles"].ByOldId(r.Field<long?>("ID_OWNER").ToString()),//TODO
                        operId = r.Field<long?>("ID_OPER"),//TODO
                        inspectorId = r.Field<decimal?>("ID_EXAMINER_REG"),//TODO
                        regnotes = r.Field<string>("NOTES_REG"),
                        paragraph = r.Field<string>("PARAGRAPH"),
                        paragraphAlt = r.Field<string>("PARAGRAPH_TRANS"),
                        removalDate = r.Field<DateTime?>("REMOVAL_DATE"),
                        removalReason = noms["aircraftRemovalReasons"].ByOldId(r.Field<string>("REMOVAL_REASON")),
                        removalText = r.Field<string>("REMOVAL_TEXT"),
                        removalDocumentNumber = r.Field<string>("REMOVAL_DOC_CAA"),
                        removalDocumentDate = r.Field<DateTime?>("REMOVAL_DOC_DATE"),
                        removalInspectorId = r.Field<decimal?>("REMOVAL_ID_EXAMINER"),//TODO
                        typeCert = new
                        {
                            aircraftTypeCertificateType = noms["aircraftTypeCertificateTypes"].ByCode(r.Field<string>("TC_TYPE")),
                            certNumber = r.Field<string>("TC_EASA"),
                            certDate = r.Field<DateTime?>("TC_DATE"),
                            certRelease = r.Field<string>("TC_REALEASE"),
                            country = noms["countries"].ByOldId(r.Field<decimal?>("TC_COUNTRY_ID").ToString())
                        }
                    })))
                .ToList();
        }

        public static IList<JObject> getAircraftCertAirworthinesses(OracleConnection oracleCon, int certId, Dictionary<int, int> inspections)
        {
            return oracleCon.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AC_AIRWORTHINESS WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_CERTIFICATE = {0}", certId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = (int)r.Field<long>("ID"),
                        __migrTable = "AC_AIRWORTHINESS",
                        aircraftCertificateType = noms["aircraftCertificateTypes"].ByCode(r.Field<string>("CERTIFICATE_TYPE")),
                        certId = r.Field<long?>("ID_CERTIFICATE"),
                        refNumber = r.Field<string>("REF_NUMBER"),
                        issueDate = r.Field<DateTime?>("ISSUE_DATE"),
                        validToDate = r.Field<DateTime?>("VALID_TO"),
                        auditPartIndex = (r.Field<long?>("ID_AUDITS") != null && inspections.ContainsKey((int)r.Field<long?>("ID_AUDITS"))) ?
                            inspections[(int)r.Field<long?>("ID_AUDITS")] :
                            (int?)null,//TODO
                        approvalId = r.Field<long?>("ID_APPROVAL"),//TODO
                        inspectorId = r.Field<decimal?>("ID_EXAMINER"),//TODO
                        valid = noms["boolean"].ByCode(r.Field<string>("VALID_YN") == "Y" ? "Y" : "N"),
                        ext1Date = r.Field<DateTime?>("EXT1_DATE"),
                        ext1validtoDate = r.Field<DateTime?>("EXT1_VALID_TO"),
                        ext1approvalId = r.Field<long?>("EXT1_APPROVAL_ID"),//TODO
                        ext1inspectorId = r.Field<decimal?>("EXT1_ID_EXAMINER"),//TODO
                        ext2Date = r.Field<DateTime?>("EXT2_DATE"),
                        ext2validtoDate = r.Field<DateTime?>("EXT2_VALID_TO"),
                        ext2approvalId = r.Field<long?>("EXT2_APPROVAL_ID"),//TODO
                        ext2inspectorId = r.Field<decimal?>("EXT2_ID_EXAMINER"),//TODO
                        country = noms["countries"].ByOldId(r.Field<decimal?>("ID_COUNTRY_TRANSFER").ToString()),
                        exemptions = r.Field<string>("EXEMPTIONS"),
                        exemptionsAlt = r.Field<string>("EXEMPTIONS_TRANS"),
                        specialReq = r.Field<string>("SPECIAL_REQ"),
                        specialReqAlt = r.Field<string>("SPECIAL_REQ_TRANS"),
                        revokeDate = r.Field<DateTime?>("REVOKE_DATE"),
                        revokeinspectorId = r.Field<decimal?>("REVOKE_ID_EXAMINER"),//TODO
                        revokeCause = r.Field<string>("SPECIAL_REQ")
                    })))
                .ToList();
        }

        public static IList<JObject> getAircraftCertMarks(OracleConnection oracleCon, int aircraftId)
        {
            return oracleCon.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AC_MARK WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
                        __migrTable = "AC_MARK",
                        ltrInNumber = r.Field<string>("LTR_IN_NOM"),
                        ltrInDate = r.Field<DateTime?>("LTR_IN_DATE"),
                        ltrCaaNumber = r.Field<string>("LTR_CAA_NOM"),
                        ltrCaaDate = r.Field<DateTime?>("LTR_CAA_DATE"),
                        mark = r.Field<string>("MARK"),
                        valid = noms["boolean"].ByCode(r.Field<string>("VALID_YN") == "Y" ? "Y" : "N")
                    })))
                .ToList();
        }

        public static IList<JObject> getAircraftCertSmods(OracleConnection oracleCon, int aircraftId)
        {
            return oracleCon.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AC_S_CODE WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_AIRCRAFT = {0}", aircraftId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
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
                    })))
                .ToList();
        }

        public static IList<JObject> getAircraftCertPermitsToFly(OracleConnection oracleCon, int certId)
        {
            return oracleCon.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AC_FLY_PERMIT WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_CERTIFICATE = {0}", certId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
                        __migrTable = "AC_FLY_PERMIT",
                        certId = r.Field<long?>("ID_CERTIFICATE"),
                        issueDate = r.Field<DateTime?>("ISSUE_DATE"),
                        issuePlace = r.Field<string>("ISSUE_PLACE"),
                        purpose = r.Field<DateTime?>("PURPOSE"),
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
                    })))
                .ToList();
        }

        public static IList<JObject> getAircraftCertNoises(OracleConnection oracleCon, int certId)
        {
            return oracleCon.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AC_NOISE WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_CERTIFICATE = {0}", certId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = (int)r.Field<long>("ID"),
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
                    })))
                .ToList();
        }
    }
}
