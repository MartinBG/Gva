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
using Gva.Api.Repositories.FileRepository;
using Gva.Api.ModelsDO;
using Common.Json;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.LotEventHandlers.EquipmentView;
using Gva.Api.LotEventHandlers.AircraftView;
using Gva.Api.LotEventHandlers.AirportView;
using Gva.Api.Repositories.AircraftRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.OrganizationRepository;

namespace Gva.MigrationTool.Sets
{
    public static class Person
    {
        public static Dictionary<int, int> personOldIdsLotIds;
        public static Dictionary<int, int> aircraftOldIdsLotIds;
        public static Dictionary<int, int> organizationOldIdsLotIds;
        public static Dictionary<string, Dictionary<string, NomValue>> noms;

        public static void createPersonsLots(OracleConnection oracleCon, Dictionary<string, Dictionary<string, NomValue>> n)
        {
            noms = n;
            var context = new UserContext(2);
            Dictionary<int, Lot> oldIdsLots = new Dictionary<int, Lot>();
            var personIds = Person.getPersonIds(oracleCon);

            using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
            {
                var userContext = new UserContext(2);
                var lotRepository = new LotRepository(unitOfWork);
                var caseTypeRepository = new CaseTypeRepository(unitOfWork);
                var userRepository = new UserRepository(unitOfWork);
                var fileRepository = new FileRepository(unitOfWork);
                var applicationRepository = new ApplicationRepository(unitOfWork);
                var aircraftRegistrationAwRepository = new AircraftRegistrationAwRepository(unitOfWork);
                var organizationRepository = new OrganizationRepository(unitOfWork);
                var aircraftRepository = new AircraftRepository(unitOfWork);

                var lotEventDispatcher = Utils.CreateLotEventDispatcher(unitOfWork, userRepository, aircraftRegistrationAwRepository);

                unitOfWork.DbContext.Configuration.AutoDetectChangesEnabled = false;

                Set personSet = lotRepository.GetSet("Person");

                foreach (var personId in personIds)
                {
                    if (personId >= 200)
                    {
                        break;
                    }
                    var lot = personSet.CreateLot(context);
                    oldIdsLots.Add(personId, lot);
                    var personData = Person.getPersonData(oracleCon, personId);
                    caseTypeRepository.AddCaseTypes(lot, personData.GetItems<JObject>("caseTypes"));
                    lot.CreatePart("personData", personData, context);
                    lot.Commit(context, lotEventDispatcher);
                    unitOfWork.Save();
                    Console.WriteLine("Created personData part for person with id {0}", personId);
                }
            }

            personOldIdsLotIds = oldIdsLots.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.LotId);
        }

        public static void migratePersons(OracleConnection con, Dictionary<string, Dictionary<string, NomValue>> n, Dictionary<int, int> aIdsLots, Dictionary<int, int> oIdsLots)
        {
            noms = n;
            aircraftOldIdsLotIds = aIdsLots;
            organizationOldIdsLotIds = oIdsLots;
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            IList<int> personIds = Person.getPersonIds(con);

            foreach (var personId in personIds) //new int[] { 6730 })
            {
                if (personId >= 200)
                {
                    break;
                }

                using (IUnitOfWork unitOfWork = Utils.CreateUnitOfWork())
                {
                    unitOfWork.DbContext.Configuration.AutoDetectChangesEnabled = false;

                    var userContext = new UserContext(2);
                    var lotRepository = new LotRepository(unitOfWork);
                    var caseTypeRepository = new CaseTypeRepository(unitOfWork);
                    var userRepository = new UserRepository(unitOfWork);
                    var fileRepository = new FileRepository(unitOfWork);
                    var applicationRepository = new ApplicationRepository(unitOfWork);
                    var aircraftRegistrationAwRepository = new AircraftRegistrationAwRepository(unitOfWork);
                    var organizationRepository = new OrganizationRepository(unitOfWork);
                    var aircraftRepository = new AircraftRepository(unitOfWork);

                    var lotEventDispatcher = Utils.CreateLotEventDispatcher(unitOfWork, userRepository, aircraftRegistrationAwRepository);

                    //var lot = lotRepository.GetSet("Person").CreateLot(userContext);
                    var lot = lotRepository.GetLotIndex(personOldIdsLotIds[personId]);

                    var personAddresses = Person.getPersonAddresses(con, personId);
                    foreach (var address in personAddresses)
                    {
                        lot.CreatePart("personAddresses/*", address, userContext);
                    }

                    Func<string, JObject, PartVersion> addPartWithFiles = (path, content) =>
                    {
                        var pv = lot.CreatePart(path, content.Get<JObject>("part"), userContext);
                        fileRepository.AddFileReferences(pv, content.GetItems<FileDO>("files"));
                        return pv;
                    };

                    Dictionary<int, Tuple<GvaApplication, ApplicationNomDO>> applications =
                        new Dictionary<int, Tuple<GvaApplication, ApplicationNomDO>>();
                    var personDocumentApplications = Person.getPersonDocumentApplications(con, personId);
                    foreach (var docApplication in personDocumentApplications)
                    {
                        var pv = addPartWithFiles("personDocumentApplications/*", docApplication);

                        GvaApplication application = new GvaApplication()
                        {
                            Lot = lot,
                            GvaAppLotPart = pv.Part
                        };

                        applicationRepository.AddGvaApplication(application);

                        applications.Add(
                            docApplication.Get<int>("part.__oldId"),
                                Tuple.Create(
                                    application,
                                    new ApplicationNomDO
                                    {
                                        ApplicationId = 0, //will be set later
                                        PartIndex = pv.Part.Index.Value,
                                        ApplicationName = docApplication.Get<string>("part.applicationType.name")
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

                    var personDocuments = Person.getPersonDocuments(con, personId, nomApplications);
                    foreach (var personDocument in personDocuments)
                    {
                        if ((new string[] { "3", "4", "5" }).Contains(personDocument.Get<string>("part.__DOCUMENT_TYPE_CODE")) &&
                            personDocument.Get<string>("part.__DOCUMENT_ROLE_CODE") == "2")
                        {
                            Utils.Pluck(personDocument.Get<JObject>("part"), new string[]
                            {
                                "__oldId",
                                "__migrTable",

                                "documentType",
                                "documentNumber",
                                "documentDateValidFrom",
                                "documentDateValidTo",
                                "documentPublisher",
                                "valid",
                                "notes"
                            });

                            addPartWithFiles("personDocumentIds/*", personDocument);
                        } else if (!(new string[] { "3", "4", "5" }).Contains(personDocument.Get<string>("part.__DOCUMENT_TYPE_CODE")) &&
                            personDocument.Get<string>("part.__DOCUMENT_ROLE_CATEGORY_CODE") == "T")
                        {
                            Utils.Pluck(personDocument.Get<JObject>("part"), new string[]
                            {
                                "__oldId",
                                "__migrTable",

                                "documentNumber",
                                "documentPersonNumber",
                                "documentDateValidFrom",
                                "documentDateValidTo",
                                "documentPublisher",
                                "staffType",
                                "ratingType",
                                "aircraftTypeGroup",
                                "ratingClass",
                                "authorization",
                                "licenceType",
                                "locationIndicator",
                                "sector",
                                "personCheckRatingValue",
                                "documentType",
                                "documentRole",
                                "valid",
                                "notes"
                            });

                            addPartWithFiles("personDocumentChecks/*", personDocument);
                        }
                        else if (!(new string[] { "3", "4", "5" }).Contains(personDocument.Get<string>("part.__DOCUMENT_TYPE_CODE")) &&
                          personDocument.Get<string>("part.__DOCUMENT_ROLE_CATEGORY_CODE") == "O")
                        {
                            Utils.Pluck(personDocument.Get<JObject>("part"), new string[]
                            {
                                "__oldId",
                                "__migrTable",

                                "documentNumber",
                                "documentPersonNumber",
                                "documentDateValidFrom",
                                "documentDateValidTo",
                                "documentPublisher",
                                "staffType",
                                "ratingType",
                                "aircraftTypeGroup",
                                "ratingClass",
                                "authorization",
                                "licenceType",
                                "locationIndicator",
                                "sector",
                                "engLangLevel",
                                "documentType",
                                "documentRole",
                                "valid",
                                "notes",
                            });

                            //general document
                            if (personDocument.Get<JObject>("part.staffType") == null)
                            {
                                personDocument.Get<JObject>("part")["staffType"] = Utils.ToJObject(noms["trainingStaffTypes"].ByOldId("0"));
                            }

                            addPartWithFiles("personDocumentTrainings/*", personDocument);
                        }
                        //else if (personDocument.Get<string>("part.__DOCUMENT_TYPE_CODE") == "2" &&
                        //  personDocument.Get<string>("part.__DOCUMENT_ROLE_CODE") == "6")
                        //{
                        //    Utils.Pluck(personDocument.Get<JObject>("part"), new string[]
                        //    {
                        //        "__oldId",
                        //        "__migrTable",

                        //        "documentNumber",
                        //        "documentDateValidFrom",
                        //        "documentPublisher",
                        //        "valid",
                        //        "notes"
                        //    });

                        //    addPartWithFiles("personDocumentExams/*", personDocument);
                        //}
                        else
                        {
                            Utils.Pluck(personDocument.Get<JObject>("part"), new string[]
                            {
                                "__oldId",
                                "__migrTable",

                                "documentNumber",
                                "documentPersonNumber",
                                "documentDateValidFrom",
                                "documentDateValidTo",
                                "documentPublisher ",
                                "documentType",
                                "documentRole",
                                "valid",
                                "notes",
                            });

                            addPartWithFiles("personDocumentOthers/*", personDocument);
                        }
                    }

                    var personDocumentEducations = Person.getPersonDocumentEducations(con, personId, nomApplications);
                    foreach (var docEducation in personDocumentEducations)
                    {
                        addPartWithFiles("personDocumentEducations/*", docEducation);
                    }

                    var personDocumentEmployments = Person.getPersonDocumentEmployments(con, personId, organizationRepository);
                    foreach (var docEmployment in personDocumentEmployments)
                    {
                        addPartWithFiles("personDocumentEmployments/*", docEmployment);
                    }

                    var personDocumentMedicals = Person.getPersonDocumentMedicals(con, personId, nomApplications);
                    foreach (var docMedical in personDocumentMedicals)
                    {
                        addPartWithFiles("personDocumentMedicals/*", docMedical);
                    }

                    var personFlyingExperiences = Person.getPersonFlyingExperiences(con, personId, organizationRepository, aircraftRepository);
                    foreach (var flyingExperience in personFlyingExperiences)
                    {
                        lot.CreatePart("personFlyingExperiences/*", flyingExperience, userContext);
                    }

                    var personStatuses = Person.getPersonStatuses(con, personId);
                    foreach (var personStatus in personStatuses)
                    {
                        lot.CreatePart("personStatuses/*", personStatus, userContext);
                    }


                    //var personRatings = Person.getPersonRatings(con, personId);
                    //foreach (var personRating in personRatings)
                    //{
                    //    var p = lot.CreatePart("ratings/*", personRating, context);

                    //    var personRatingEditions = Person.getPersonRatingEditions(con, (int)personRating.GetValue("__oldId"));
                    //    foreach (var personRatingEdition in personRatingEditions)
                    //    {
                    //        lot.CreatePart(p.Part.Path + "/editions/*", personRatingEdition, context);

                    //    }
                    //}

                    //var personLicences = Person.getPersonLicences(con, personId);
                    //foreach (var personLicence in personLicences)
                    //{
                    //    lot.CreatePart("/licences/*", personLicence, context);
                    //}

                    try
                    {
                        lot.Commit(userContext, lotEventDispatcher);
                    }
                    //swallow the Cannot commit without modifications exception
                    catch (InvalidOperationException)
                    {
                    }

                    unitOfWork.Save();

                    Console.WriteLine("Migrated personId: {0}", personId);
                }
            }

            timer.Stop();
            Console.WriteLine("Person migration time - {0}", timer.Elapsed.TotalMinutes);
        }

        public static IList<int> getPersonIds(OracleConnection con)
        {
            return con.CreateStoreCommand("SELECT ID FROM CAA_DOC.PERSON")
                .Materialize(r => (int)r.Field<decimal>("ID"))
                    .ToList();
        }

        public static JObject getPersonData(OracleConnection con, int personId)
        {
            var lins = new Dictionary<string, string>()
                {
                    { "1", "pilots"           },
                    { "2", "flyingCrew"       },
                    { "3", "crewStaff"        },
                    { "4", "HeadFlights"      },
                    { "5", "AirlineEngineers" },
                    { "6", "dispatchers"      },
                    { "7", "paratroopers"     },
                    { "8", "engineersRVD"     },
                    { "9", "deltaplaner"      }
                };

            return con.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.PERSON WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
                        __migrTable = "PERSON",
                        caseTypes = new JObject[] { Utils.DUMMY_PILOT_CASE_TYPE },
                        lin = r.Field<string>("LIN"),
                        linType = noms["linTypes"].ByCode(lins[r.Field<string>("LIN").Substring(0, 1)]),
                        uin = r.Field<string>("EGN"),
                        firstName = r.Field<string>("NAME"),
                        firstNameAlt = r.Field<string>("NAME_TRANS"),
                        middleName = r.Field<string>("SURNAME"),
                        middleNameAlt = r.Field<string>("SURNAME_TRANS"),
                        lastName = r.Field<string>("FAMILY"),
                        lastNameAlt = r.Field<string>("FAMILY_TRANS"),
                        dateOfBirth = r.Field<DateTime>("DATE_OF_BIRTH"),
                        placeOfBirth = noms["cities"].ByOldId(r.Field<decimal?>("PLACE_OF_BIRTH_ID").ToString()),
                        country = noms["countries"].ByOldId(r.Field<decimal?>("COUNTRY_ID").ToString()),
                        sex = noms["gender"].ByOldId(r.Field<decimal?>("SEX_ID").ToString()),
                        email = r.Field<string>("E_MAIL"),
                        fax = r.Field<string>("FAX"),
                        phone1 = r.Field<string>("PHONE1"),
                        phone2 = r.Field<string>("PHONE2"),
                        phone3 = r.Field<string>("PHONE3"),
                        phone4 = r.Field<string>("PHONE4"),
                        phone5 = r.Field<string>("PHONE5")
                    }))
                .Single();
        }

        public static IList<JObject> getPersonAddresses(OracleConnection con, int personId)
        {
            return con.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.PERSON_ADDRESS WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and PERSON_ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
                        __migrTable = "PERSON_ADDRESS",

                        addressType = noms["addressTypes"].ByOldId(r.Field<decimal?>("ADDRESS_TYPE_ID").ToString()),
                        settlement = noms["cities"].ByOldId(r.Field<decimal?>("TOWN_VILLAGE_ID").ToString()),
                        address = r.Field<string>("ADDRESS"),
                        addressAlt = r.Field<string>("ADDRESS_TRANS"),
                        phone = r.Field<string>("PHONES"),
                        valid = noms["boolean"].ByCode(r.Field<string>("VALID_YN") == "Y" ? "Y" : "N"),
                        postalCode = r.Field<string>("POSTAL_CODE")
                    }))
                .ToList();
        }

        public static IList<JObject> getPersonDocumentApplications(OracleConnection con, int personId)
        {
            var parts = con.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.REQUEST WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and APPLICANT_PERSON_ID = {0}", personId)
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

            var files = con.CreateStoreCommand(
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
                new DbClause("and R.APPLICANT_PERSON_ID = {0}", personId)
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

        public static IList<JObject> getPersonDocuments(OracleConnection con, int personId, IDictionary<int, JObject> nomApplications)
        {
            var parts = con.CreateStoreCommand(
                @"SELECT PD.ID,
                        PD.DOC_NO,
                        PD.PERSON_NUM,
                        PD.VALID_FROM,
                        PD.VALID_TO,
                        PD.DOC_PUBLISHER,
                        PD.STAFF_TYPE_ID,
                        PD.RATING_TYPE_ID,
                        PD.ID_AC_GROUP,
                        PD.RATING_CLASS_ID,
                        PD.AUTHORIZATION_ID,
                        PD.LICENCE_TYPE_ID,
                        PD.ID_INDICATOR,
                        PD.SECTOR,
                        PD.DOCUMENT_TYPE_ID,
                        PD.DOCUMENT_ROLE_ID,
                        PD.VALID_YN,
                        PD.NOTES,
                        PD.RATING_VALUE,
                        PD.NM_EN_LANG_ID,
                        PD.BOOK_PAGE_NO,
                        PD.PAGES_COUNT,
                        DT.CODE DOCUMENT_TYPE_CODE,
                        DT.ID_DIRECTION DOCUMENT_TYPE_ID_DIRECTION,
                        DR.ID_DIRECTION DOCUMENT_ROLE_ID_DIRECTION,
                        DR.CODE DOCUMENT_ROLE_CODE,
                        DR.CATEGORY_CODE DOCUMENT_ROLE_CATEGORY_CODE
                    FROM CAA_DOC.PERSON_DOCUMENT PD
                    JOIN CAA_DOC.NM_DOCUMENT_TYPE DT ON PD.DOCUMENT_TYPE_ID = DT.ID
                    JOIN CAA_DOC.NM_DOCUMENT_ROLE DR ON PD.DOCUMENT_ROLE_ID = DR.ID
                    WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and PD.PERSON_ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
                        __migrTable = "PERSON_DOCUMENT",

                        __DOCUMENT_TYPE_CODE = r.Field<string>("DOCUMENT_TYPE_CODE"),
                        __DOCUMENT_TYPE_ID_DIRECTION = r.Field<string>("DOCUMENT_TYPE_ID_DIRECTION"),

                        __DOCUMENT_ROLE_CODE = r.Field<string>("DOCUMENT_ROLE_CODE"),
                        __DOCUMENT_ROLE_CATEGORY_CODE = r.Field<string>("DOCUMENT_ROLE_CATEGORY_CODE"),
                        __DOCUMENT_ROLE_ID_DIRECTION = r.Field<string>("DOCUMENT_ROLE_ID_DIRECTION"),

                        __BOOK_PAGE_NO = (int?)r.Field<decimal?>("BOOK_PAGE_NO"),
                        __PAGES_COUNT = (int?)r.Field<decimal?>("PAGES_COUNT"),

                        documentNumber = r.Field<string>("DOC_NO"),
                        documentPersonNumber = r.Field<decimal?>("PERSON_NUM"),
                        documentDateValidFrom = r.Field<DateTime?>("VALID_FROM"),
                        documentDateValidTo = r.Field<DateTime?>("VALID_TO"),
                        documentPublisher = r.Field<string>("DOC_PUBLISHER"),
                        staffType = noms["staffTypes"].ByOldId(r.Field<decimal?>("STAFF_TYPE_ID").ToString()),
                        ratingType = noms["ratingTypes"].ByOldId(r.Field<decimal?>("RATING_TYPE_ID").ToString()),
                        aircraftTypeGroup = noms["aircraftTypeGroups"].ByOldId(r.Field<long?>("ID_AC_GROUP").ToString()),
                        ratingClass = noms["ratingClassGroups"].ByOldId(r.Field<decimal?>("RATING_CLASS_ID").ToString()),
                        authorization = noms["authorizations"].ByOldId(r.Field<decimal?>("AUTHORIZATION_ID").ToString()),
                        licenceType = noms["licenceTypes"].ByOldId(r.Field<decimal?>("LICENCE_TYPE_ID").ToString()),
                        locationIndicator = noms["locationIndicators"].ByOldId(r.Field<long?>("ID_INDICATOR").ToString()),
                        sector = r.Field<string>("SECTOR"),
                        documentType = noms["documentTypes"].ByOldId(r.Field<decimal?>("DOCUMENT_TYPE_ID").ToString()),
                        documentRole = noms["documentRoles"].ByOldId(r.Field<decimal?>("DOCUMENT_ROLE_ID").ToString()),
                        valid = noms["boolean"].ByCode(r.Field<string>("VALID_YN") == "Y" ? "Y" : "N"),
                        notes = r.Field<string>("NOTES"),

                        personCheckRatingValue = noms["personCheckRatingValues"].ByCode(r.Field<string>("RATING_VALUE")),
                        engLangLevel = noms["engLangLevels"].ByOldId(r.Field<long?>("NM_EN_LANG_ID").ToString()),
                    }))
                .ToList();

            var files = con.CreateStoreCommand(
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
                new DbClause("and PD.PERSON_ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("DOC_ID"),
                        __migrTable = "DOCLIB_DOCUMENTS",
                        __PERSON_DOCUMENT_ID = (int)r.Field<decimal>("ID"),

                        key = Utils.DUMMY_FILE_KEY,//TODO
                        name = r.Field<string>("NAME"),
                        mimeType = r.Field<string>("MIME_TYPE")
                    }))
                .ToList();

            var apps = con.CreateStoreCommand(
                @"SELECT R.ID REQUEST_ID,
                        PD.ID PERSON_DOCUMENT_ID
                    FROM CAA_DOC.REQUEST R 
                    JOIN CAA_DOC.REQUEST_INVENTORY RI ON RI.REQUEST_ID = R.ID
                    JOIN CAA_DOC.PERSON_DOCUMENT PD ON PD.ID = RI.PERSON_DOCUMENT_ID
                    WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and PD.PERSON_ID = {0}", personId)
                )
                .Materialize(r =>
                    new
                    {
                        persondDocumentId = (int)r.Field<decimal>("PERSON_DOCUMENT_ID"),
                        nomApp = nomApplications[(int)r.Field<decimal>("REQUEST_ID")]
                    })
                .ToList();

            return (from part in parts
                    join file in files on part.Get<int>("__oldId") equals file.Get<int>("__PERSON_DOCUMENT_ID") into fg
                    join app in apps on part.Get<int>("__oldId") equals app.persondDocumentId into ag
                    let file = fg.Any() ? fg.Single() : null //throw if more than one files present
                    let bookPageNumber = part.Get<int?>("__BOOK_PAGE_NO").ToString()
                    let pageCount = part.Get<int?>("__PAGES_COUNT")
                    select new JObject(
                        new JProperty("part", part),
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

        public static IList<JObject> getPersonDocumentEducations(OracleConnection con, int personId, IDictionary<int, JObject> nomApplications)
        {
            var parts = con.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.EDUCATION WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and PERSON_ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
                        __migrTable = "EDUCATION",

                        __BOOK_PAGE_NO = (int?)r.Field<decimal?>("BOOK_PAGE_NO"),
                        __PAGES_COUNT = (int?)r.Field<decimal?>("PAGES_COUNT"),

                        documentNumber = r.Field<string>("NO"),
                        completionDate = r.Field<DateTime?>("COMPLETION_DATE"),
                        speciality = r.Field<string>("SPECIALITY"),
                        school = noms["schools"].ByOldId(r.Field<decimal?>("SCHOOL_ID").ToString()),
                        graduation = noms["graduations"].ByOldId(r.Field<decimal?>("GRADUATION_ID").ToString()),
                        notes = r.Field<string>("NOTES")
                    }))
                .ToList();

            var files = con.CreateStoreCommand(
                @"SELECT E.ID,
                        D.DOC_ID,
                        D.MIME_TYPE,
                        D.DESCRIPTION,
                        D.INS_USER,
                        D.INS_DATE,
                        D.UPD_USER,
                        D.UPD_DATE,
                        D.NAME
                    FROM CAA_DOC.EDUCATION E
                    JOIN CAA_DOC.DOCLIB_DOCUMENTS D ON E.ID + 60000000 = D.DOC_ID
                    WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and E.PERSON_ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("DOC_ID"),
                        __migrTable = "DOCLIB_DOCUMENTS",
                        __EDUCATION_ID = (int)r.Field<decimal>("ID"),

                        key = Utils.DUMMY_FILE_KEY,//TODO
                        name = r.Field<string>("NAME"),
                        mimeType = r.Field<string>("MIME_TYPE")
                    }))
                .ToList();

            var apps = con.CreateStoreCommand(
                @"SELECT R.ID REQUEST_ID,
                        E.ID EDUCATION_ID
                    FROM CAA_DOC.REQUEST R 
                    JOIN CAA_DOC.REQUEST_INVENTORY RI ON RI.REQUEST_ID = R.ID
                    JOIN CAA_DOC.EDUCATION E ON E.ID = RI.EDUCATION_ID
                    WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and E.PERSON_ID = {0}", personId)
                )
                .Materialize(r =>
                    new
                    {
                        educationId = (int)r.Field<decimal>("EDUCATION_ID"),
                        nomApp = nomApplications[(int)r.Field<decimal>("REQUEST_ID")]
                    })
                .ToList();

            return (from part in parts
                    join file in files on part.Get<int>("__oldId") equals file.Get<int>("__EDUCATION_ID") into fg
                    join app in apps on part.Get<int>("__oldId") equals app.educationId into ag
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
                                    "completionDate",
                                    "speciality",
                                    "school",
                                    "graduation",
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
                                    new JProperty("applications", new JArray(ag.Select(a => a.nomApp))))))))
                    .ToList();
        }

        public static IList<JObject> getPersonDocumentEmployments(OracleConnection con, int personId, IOrganizationRepository or)
        {
            var parts = con.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.EMPLOYEE WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and PERSON_ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
                        __migrTable = "EMPLOYEE",

                        __BOOK_PAGE_NO = (int?)r.Field<decimal?>("BOOK_PAGE_NO"),
                        __PAGES_COUNT = (int?)r.Field<decimal?>("PAGES_COUNT"),

                        hiredate = r.Field<DateTime?>("HIREDATE"),
                        valid = noms["boolean"].ByCode(r.Field<string>("VALID_YN") == "Y" ? "Y" : "N"),
                        organization = Utils.GetOrganization(organizationOldIdsLotIds.GetLotId((int?)r.Field<decimal?>("FIRM_ID")), or),
                        employmentCategory = noms["employmentCategories"].ByOldId(r.Field<decimal?>("JOB_CATEGORY_ID").ToString()),
                        country = noms["countries"].ByOldId(r.Field<decimal?>("COUNTRY_ID").ToString()),
                        notes = r.Field<string>("NOTES")
                    }))
                .ToList();

            var files = con.CreateStoreCommand(
                @"SELECT E.ID,
                        E.BOOK_PAGE_NO,
                        E.PAGES_COUNT,
                        D.DOC_ID,
                        D.MIME_TYPE,
                        D.DESCRIPTION,
                        D.INS_USER,
                        D.INS_DATE,
                        D.UPD_USER,
                        D.UPD_DATE,
                        D.NAME
                    FROM CAA_DOC.EMPLOYEE E
                    JOIN CAA_DOC.DOCLIB_DOCUMENTS D ON E.ID + 100000000 = D.DOC_ID
                    WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and E.PERSON_ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("DOC_ID"),
                        __migrTable = "DOCLIB_DOCUMENTS",

                        __EMPLOYEE_ID = (int)r.Field<decimal>("ID"),

                        key = Utils.DUMMY_FILE_KEY,//TODO
                        name = r.Field<string>("NAME"),
                        mimeType = r.Field<string>("MIME_TYPE")
                    }))
                .ToList();

            return (from part in parts
                    join file in files on part.Get<int>("__oldId") equals file.Get<int>("__EMPLOYEE_ID") into fg
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

                                    "hiredate",
                                    "valid",
                                    "organization",
                                    "employmentCategory",
                                    "country",
                                    "notes"
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

        public static IList<JObject> getPersonDocumentMedicals(OracleConnection con, int personId, IDictionary<int, JObject> nomApplications)
        {
            var medLimitations = con
                .CreateStoreCommand(
                    @"SELECT * FROM 
                        CAA_DOC.MED_CERT_LIMITATION 
                        WHERE MED_CERT_ID in (SELECT ID FROM CAA_DOC.MED_CERT WHERE {0})",
                    new DbClause("1=1"),
                    new DbClause("and PERSON_ID = {0}", personId))
                .Materialize(r =>
                    new
                    {
                        medId = (int)r.Field<decimal?>("MED_CERT_ID"),
                        limitation = noms["medLimitation"].ByOldId(r.Field<decimal?>("MED_LIMIT_ID").ToString()),
                    })
                .GroupBy(l => l.medId)
                .ToDictionary(g => g.Key, g => g.Select(l => l.limitation).ToArray());

            var parts = con
                .CreateStoreCommand(
                    @"SELECT * FROM CAA_DOC.MED_CERT WHERE {0} {1}",
                    new DbClause("1=1"),
                    new DbClause("and PERSON_ID = {0}", personId))
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
                        __migrTable = "MED_CERT",

                        __BOOK_PAGE_NO = (int?)r.Field<decimal?>("BOOK_PAGE_NO"),
                        __PAGES_COUNT = (int?)r.Field<decimal?>("PAGES_COUNT"),

                        documentNumberPrefix = r.Field<string>("NO1"),
                        documentNumber = r.Field<string>("NO2"),
                        documentNumbeSuffix = r.Field<string>("NO3"),
                        documentDateValidFrom = r.Field<DateTime?>("DATE_FROM"),
                        documentDateValidTo = r.Field<DateTime?>("DATE_TO"),
                        medClass = noms["medClasses"].ByOldId(r.Field<decimal?>("MED_CLASS_ID").ToString()),
                        documentPublisher = noms["medDocPublishers"].ByName(r.Field<string>("PUBLISHER_NAME")),
                        limitations = medLimitations.ContainsKey((int)r.Field<decimal>("ID")) ? medLimitations[(int)r.Field<decimal>("ID")] : null,
                        notes = r.Field<string>("NOTES")
                    }))
                .ToList();

            var files = con.CreateStoreCommand(
                @"SELECT MC.ID,
                        MC.BOOK_PAGE_NO,
                        MC.PAGES_COUNT,
                        D.DOC_ID,
                        D.MIME_TYPE,
                        D.DESCRIPTION,
                        D.INS_USER,
                        D.INS_DATE,
                        D.UPD_USER,
                        D.UPD_DATE,
                        D.NAME
                    FROM CAA_DOC.MED_CERT MC
                    JOIN CAA_DOC.DOCLIB_DOCUMENTS D ON MC.ID + 40000000 = D.DOC_ID
                    WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and MC.PERSON_ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("DOC_ID"),
                        __migrTable = "DOCLIB_DOCUMENTS",

                        __MED_CERT_ID = (int)r.Field<decimal>("ID"),

                        key = Utils.DUMMY_FILE_KEY,//TODO
                        name = r.Field<string>("NAME"),
                        mimeType = r.Field<string>("MIME_TYPE")
                    }))
                .ToList();

            var apps = con.CreateStoreCommand(
                @"SELECT R.ID REQUEST_ID,
                        MC.ID MED_CERT_ID
                    FROM CAA_DOC.REQUEST R 
                    JOIN CAA_DOC.REQUEST_INVENTORY RI ON RI.REQUEST_ID = R.ID
                    JOIN CAA_DOC.MED_CERT MC ON MC.ID = RI.MED_CERT_ID
                    WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and MC.PERSON_ID = {0}", personId)
                )
                .Materialize(r =>
                    new
                    {
                        medicalId = (int)r.Field<decimal>("MED_CERT_ID"),
                        nomApp = nomApplications[(int)r.Field<decimal>("REQUEST_ID")]
                    })
                .ToList();

            return (from part in parts
                    join file in files on part.Get<int>("__oldId") equals file.Get<int>("__MED_CERT_ID") into fg
                    join app in apps on part.Get<int>("__oldId") equals app.medicalId into ag
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

                                    "documentNumberPrefix",
                                    "documentNumber",
                                    "documentNumbeSuffix",
                                    "documentDateValidFrom",
                                    "documentDateValidTo",
                                    "medClass",
                                    "documentPublisher",
                                    "limitations",
                                    "notes"
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

        public static IList<JObject> getPersonFlyingExperiences(OracleConnection con, int personId, IOrganizationRepository or, IAircraftRepository ar)
        {
            return con.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.FLYING_EXPERIENCE WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and PERSON_ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
                        __migrTable = "FLYING_EXPERIENCE",

                        staffType = noms["staffTypes"].ByOldId(r.Field<decimal?>("STAFF_TYPE_ID").ToString()),
                        documentDate = r.Field<DateTime?>("DOCUMENT_DATE"),
                        period = new { month = r.Field<string>("PERIOD_MONTH"), year = r.Field<string>("PERIOD_YEAR") },
                        organization = Utils.GetOrganization(organizationOldIdsLotIds.GetLotId((int?)r.Field<decimal?>("FIRM_ID")), or),
                        aircraft = Utils.GetAircraft(aircraftOldIdsLotIds.GetLotId((int?)r.Field<long?>("AC_ID")), ar),
                        ratingType = noms["ratingTypes"].ByOldId(r.Field<decimal?>("RATING_TYPE_ID").ToString()),
                        ratingClass = noms["ratingClassGroups"].ByOldId(r.Field<decimal?>("RATING_CLASS_ID").ToString()),
                        authorization = noms["authorizations"].ByOldId(r.Field<decimal?>("AUTHORIZATION_ID").ToString()),
                        licenceType = noms["licenceTypes"].ByOldId(r.Field<decimal?>("LICENCE_TYPE_ID").ToString()),
                        locationIndicator = noms["locationIndicators"].ByOldId(r.Field<long?>("INDICATOR_ID").ToString()),
                        sector = r.Field<string>("SECTOR"),
                        experienceRole = noms["experienceRoles"].ByOldId(r.Field<decimal?>("EXPERIENCE_ROLE_ID").ToString()),
                        experienceMeasure = noms["experienceMeasures"].ByOldId(r.Field<decimal?>("MEASURE_ID").ToString()),
                        dayIFR = new { hours = r.Field<short?>("AMNTH_DAY_I"), minutes = r.Field<short?>("AMNTM_DAY_I") },
                        dayVFR = new { hours = r.Field<short?>("AMNTH_DAY_V"), minutes = r.Field<short?>("AMNTM_DAY_V") },
                        dayLandings = r.Field<short?>("LND_DAY"),
                        nightIFR = new { hours = r.Field<short?>("AMNTH_NGT_I"), minutes = r.Field<short?>("AMNTM_NGT_I") },
                        nightVFR = new { hours = r.Field<short?>("AMNTH_NGT_V"), minutes = r.Field<short?>("AMNTM_NGT_V") },
                        nightLandings = r.Field<short?>("LND_NGT"),
                        total = new { hours = r.Field<int?>("AMOUNT"), minutes = r.Field<short?>("AMOUNT_M") },
                        totalDoc = new { hours = r.Field<int?>("AMOUNT_SUM"), minutes = r.Field<short?>("AMOUNT_M_SUM") },
                        totalLastMonths = new { hours = r.Field<int?>("AMOUNT_12"), minutes = r.Field<short?>("AMOUNT_M_12") },
                        notes = r.Field<string>("NOTES")
                    }))
                .ToList();
        }

        public static IList<JObject> getPersonStatuses(OracleConnection con, int personId)
        {
            return con.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.PERSON_STATE WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and PERSON_ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = (int)r.Field<long>("ID"),
                        __migrTable = "PERSON_STATE",

                        personStatusType = noms["personStatusTypes"].ByCode(r.Field<string>("REASON_CODE").ToString()),
                        documentNumber = r.Field<string>("DOCUMENT_NUM"),
                        documentDateValidFrom = r.Field<DateTime?>("BEGIN_DATE"),
                        documentDateValidTo = r.Field<DateTime?>("END_DATE"),
                        notes = r.Field<string>("REMARKS")
                    }))
                .ToList();
        }

        //public static IList<JObject> getPersonRatings(OracleConnection con, int personId)
        //{
        //    return con.CreateStoreCommand(
        //        @"SELECT * FROM CAA_DOC.RATING_CAA WHERE {0} {1}",
        //        new DbClause("1=1"),
        //        new DbClause("and PERSON_ID = {0}", personId)
        //        )
        //        .Materialize(r => Utils.ToJObject(
        //            new
        //            {
        //                __oldId = (int)r.Field<decimal>("ID"),
        //                __migrTable = "RATING_CAA",
        //                staffTypeId = noms["staffTypes"].ByOldId(r.Field<decimal?>("STAFF_TYPE_ID").ToString()).NomValueId(),
        //                personRatingLevelId = noms["personRatingLevels"].ByCode(r.Field<string>("RATING_STEPEN")).NomValueId(),
        //                ratingClassId = noms["ratingClassGroups"].ByOldId(r.Field<decimal?>("RATING_CLASS_ID").ToString()).NomValueId(),
        //                ratingTypeId = noms["ratingTypes"].ByOldId(r.Field<decimal?>("RATING_TYPE_ID").ToString()).NomValueId(),
        //                authorizationId = noms["authorizations"].ByOldId(r.Field<decimal?>("AUTHORIZATION_ID").ToString()).NomValueId(),
        //                personRatingModelId = noms["personRatingModels"].ByCode(r.Field<string>("RATING_MODEL")).NomValueId(),
        //                locationIndicatorId = noms["locationIndicators"].ByOldId(r.Field<long?>("INDICATOR_ID").ToString()).NomValueId(),
        //                sector = r.Field<string>("SECTOR"),
        //                aircraftTypeGroupId = noms["aircraftTypeGroups"].ByOldId(r.Field<long?>("RATING_GROUP66_ID").ToString()).NomValueId(),
        //                aircraftTypeCategoryId = noms["aircraftClases66"].ByOldId(r.Field<long?>("RATING_CAT66_ID").ToString()).NomValueId(),
        //                caaId = noms["caa"].ByOldId(r.Field<decimal?>("CAA_ID").ToString()).NomValueId(),
        //            }))
        //        .ToList();
        //}

        //public static IList<JObject> getPersonRatingEditions(OracleConnection con, int ratingId)
        //{
        //    return con.CreateStoreCommand(
        //        @"SELECT * FROM CAA_DOC.RATING_CAA_DATES WHERE {0} {1}",
        //        new DbClause("1=1"),
        //        new DbClause("and RATING_CAA_ID = {0}", ratingId)
        //        )
        //        .Materialize(r => Utils.ToJObject(
        //            new
        //            {
        //                __oldId = (int)r.Field<decimal>("ID"),
        //                __migrTable = "RATING_CAA_DATES",
        //                ratingSubClassIds = (r.Field<string>("SUBCLASSES") != null) ? r.Field<string>("SUBCLASSES").Split(',').Select(sc => noms["ratingSubClasses"].ByCode(sc.Trim()).NomValueId()).ToArray() : null,
        //                limitationIds = (r.Field<string>("LIMITATIONS") != null) ? r.Field<string>("LIMITATIONS").Split(',').Select(lim => noms["limitations66"].ByCode(lim.Trim()).NomValueId()).ToArray() : null,
        //                documentDateValidFrom = r.Field<DateTime?>("ISSUE_DATE"),
        //                documentDateValidTo = r.Field<DateTime?>("VALID_DATE"),
        //                notes = r.Field<string>("NOTES"),
        //                notesAlt = r.Field<string>("NOTES_TRANS"),
        //                inspectorId = r.Field<decimal?>("EXAMINER_ID")
        //            }))
        //        .ToList();
        //}
    }
}
