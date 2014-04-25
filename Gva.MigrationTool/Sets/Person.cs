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
using Autofac.Features.OwnedInstances;
using Common.Tests;
using Gva.Api.Repositories.PersonRepository;

namespace Gva.MigrationTool.Sets
{
    public class Person
    {
        private Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, IUserRepository, IFileRepository, IApplicationRepository, ICaseTypeRepository, IOrganizationRepository, IAircraftRepository, IPersonRepository, ILotEventDispatcher, UserContext>>> dependencyFactory;
        private OracleConnection oracleConn;

        public Person(OracleConnection oracleConn,
            Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, IUserRepository, IFileRepository, IApplicationRepository, ICaseTypeRepository, IOrganizationRepository, IAircraftRepository, IPersonRepository, ILotEventDispatcher, UserContext>>> dependencyFactory)
        {
            this.dependencyFactory = dependencyFactory;
            this.oracleConn = oracleConn;
        }

        public Dictionary<int, int> createPersonsLots(Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            Dictionary<int, int> personIdToLotId = new Dictionary<int, int>();

            using (var dependencies = this.dependencyFactory())
            {
                var unitOfWork = dependencies.Value.Item1;
                var lotRepository = dependencies.Value.Item2;
                var userRepository = dependencies.Value.Item3;
                var fileRepository = dependencies.Value.Item4;
                var applicationRepository = dependencies.Value.Item5;
                var caseTypeRepository = dependencies.Value.Item6;
                var organizationRepository = dependencies.Value.Item7;
                var aircraftRepository = dependencies.Value.Item8;
                var lotEventDispatcher = dependencies.Value.Item10;
                var context = dependencies.Value.Item11;

                unitOfWork.DbContext.Configuration.AutoDetectChangesEnabled = false;

                Set personSet = lotRepository.GetSet("Person");

                foreach (var personId in this.getPersonIds().OrderByDescending(i => i).Take(1000))
                {
                    //if (personId != 6730)
                    //{
                    //    continue;
                    //}

                    var personData = this.getPersonData(personId, noms);

                    if (personData.Get<string>("lin") == null)
                    {
                        Console.WriteLine("No LIN for person with id {0}", personId);
                        continue;
                    }

                    var lot = personSet.CreateLot(context);

                    caseTypeRepository.AddCaseTypes(lot, personData.GetItems<JObject>("caseTypes"));
                    lot.CreatePart("personData", personData, context);
                    lot.Commit(context, lotEventDispatcher);
                    unitOfWork.Save();
                    Console.WriteLine("Created personData part for person with id {0}", personId);

                    personIdToLotId.Add(personId, lot.LotId);
                }
            }

            return personIdToLotId;
        }
         
        public void migratePersons(
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, int> aircraftApexIdtoLotId,
            Dictionary<int, int> organizationIdtoLotId,
            Dictionary<int, int> personIdToLotId)
        {
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            foreach (var personId in this.getPersonIds().OrderByDescending(i => i).Take(1000)) //new int[] { 6730 })
            {
                if (!personIdToLotId.ContainsKey(personId))
                {
                    continue;
                }

                using (var dependencies = this.dependencyFactory())
                {
                    var unitOfWork = dependencies.Value.Item1;
                    var lotRepository = dependencies.Value.Item2;
                    var userRepository = dependencies.Value.Item3;
                    var fileRepository = dependencies.Value.Item4;
                    var applicationRepository = dependencies.Value.Item5;
                    var caseTypeRepository = dependencies.Value.Item6;
                    var organizationRepository = dependencies.Value.Item7;
                    var aircraftRepository = dependencies.Value.Item8;
                    var personRepository = dependencies.Value.Item9;
                    var lotEventDispatcher = dependencies.Value.Item10;
                    var context = dependencies.Value.Item11;

                    unitOfWork.DbContext.Configuration.AutoDetectChangesEnabled = false;

                    Func<int?, JObject> getPerson = (personApexId) => Utils.GetPerson(personApexId, personRepository, personIdToLotId);

                    var lot = lotRepository.GetLotIndex(personIdToLotId[personId]);

                    var personAddresses = this.getPersonAddresses(personId, noms);
                    foreach (var address in personAddresses)
                    {
                        lot.CreatePart("personAddresses/*", address, context);
                    }

                    Func<string, JObject, PartVersion> addPartWithFiles = (path, content) =>
                    {
                        var pv = lot.CreatePart(path, content.Get<JObject>("part"), context);
                        fileRepository.AddFileReferences(pv, content.GetItems<FileDO>("files"));
                        return pv;
                    };

                    Dictionary<int, Tuple<GvaApplication, ApplicationNomDO>> applications =
                        new Dictionary<int, Tuple<GvaApplication, ApplicationNomDO>>();
                    var personDocumentApplications = this.getPersonDocumentApplications(personId, noms);
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

                    var personDocuments = this.getPersonDocuments(personId, nomApplications, noms);
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

                    var personDocumentEducations = this.getPersonDocumentEducations(personId, nomApplications, noms);
                    foreach (var docEducation in personDocumentEducations)
                    {
                        addPartWithFiles("personDocumentEducations/*", docEducation);
                    }

                    var personDocumentEmployments = this.getPersonDocumentEmployments(personId, organizationRepository, noms, organizationIdtoLotId);
                    foreach (var docEmployment in personDocumentEmployments)
                    {
                        addPartWithFiles("personDocumentEmployments/*", docEmployment);
                    }

                    var personDocumentMedicals = this.getPersonDocumentMedicals(personId, nomApplications, noms);
                    foreach (var docMedical in personDocumentMedicals)
                    {
                        addPartWithFiles("personDocumentMedicals/*", docMedical);
                    }

                    var personFlyingExperiences = this.getPersonFlyingExperiences(personId, organizationRepository, aircraftRepository, noms, organizationIdtoLotId, aircraftApexIdtoLotId);
                    foreach (var flyingExperience in personFlyingExperiences)
                    {
                        lot.CreatePart("personFlyingExperiences/*", flyingExperience, context);
                    }

                    var personStatuses = this.getPersonStatuses(personId, noms);
                    foreach (var personStatus in personStatuses)
                    {
                        lot.CreatePart("personStatuses/*", personStatus, context);
                    }

                    var personRatings = this.getPersonRatings(personId, getPerson, noms);
                    foreach (var personRating in personRatings)
                    {
                        int nextIndex = 0;

                        foreach (var edition in personRating.GetItems<JObject>("editions"))
                        {
                            edition.Add("index", nextIndex);
                            nextIndex++;
                        }

                        personRating.Add("nextIndex", nextIndex);

                        var pv = lot.CreatePart("ratings/*", personRating, context);

                        foreach (var edition in personRating.GetItems<JObject>("editions"))
                        {
                            applicationRepository.AddApplicationRefs(pv.Part, edition.GetItems<ApplicationNomDO>("applications"));
                        }
                    }

                    //var personLicences = Person.getPersonLicences(con, personId);
                    //foreach (var personLicence in personLicences)
                    //{
                    //    lot.CreatePart("/licences/*", personLicence, context);
                    //}

                    try
                    {
                        lot.Commit(context, lotEventDispatcher);
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

        private IList<int> getPersonIds()
        {
            return this.oracleConn.CreateStoreCommand("SELECT ID FROM CAA_DOC.PERSON")
                .Materialize(r => (int)r.Field<decimal>("ID"))
                    .ToList();
        }

        private JObject getPersonData(int personId, Dictionary<string, Dictionary<string, NomValue>> noms)
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

            return this.oracleConn.CreateStoreCommand(
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
                        linType = r.Field<string>("LIN") != null ?
                            noms["linTypes"].ByCode(lins[r.Field<string>("LIN").Substring(0, 1)]):
                            null,
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

        private IList<JObject> getPersonAddresses(int personId, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            return this.oracleConn.CreateStoreCommand(
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

        private IList<JObject> getPersonDocumentApplications(int personId, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            var parts = this.oracleConn.CreateStoreCommand(
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

        private IList<JObject> getPersonDocuments(int personId, IDictionary<int, JObject> nomApplications, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            var parts = this.oracleConn.CreateStoreCommand(
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

            var apps = this.oracleConn.CreateStoreCommand(
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

        private IList<JObject> getPersonDocumentEducations(int personId, IDictionary<int, JObject> nomApplications, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            var parts = this.oracleConn.CreateStoreCommand(
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

            var files = this.oracleConn.CreateStoreCommand(
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

            var apps = this.oracleConn.CreateStoreCommand(
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

        private IList<JObject> getPersonDocumentEmployments(int personId, IOrganizationRepository or, Dictionary<string, Dictionary<string, NomValue>> noms, Dictionary<int, int> organizationIdtoLotId)
        {
            var parts = this.oracleConn.CreateStoreCommand(
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
                        organization = Utils.GetOrganization((int?)r.Field<decimal?>("FIRM_ID"), or, organizationIdtoLotId),
                        employmentCategory = noms["employmentCategories"].ByOldId(r.Field<decimal?>("JOB_CATEGORY_ID").ToString()),
                        country = noms["countries"].ByOldId(r.Field<decimal?>("COUNTRY_ID").ToString()),
                        notes = r.Field<string>("NOTES")
                    }))
                .ToList();

            var files = this.oracleConn.CreateStoreCommand(
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

        private IList<JObject> getPersonDocumentMedicals(int personId, IDictionary<int, JObject> nomApplications, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            var medLimitations = this.oracleConn
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

            var parts = this.oracleConn
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

            var files = this.oracleConn.CreateStoreCommand(
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

            var apps = this.oracleConn.CreateStoreCommand(
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

        private IList<JObject> getPersonFlyingExperiences(
            int personId,
            IOrganizationRepository or,
            IAircraftRepository ar,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, int> organizationIdtoLotId,
            Dictionary<int, int> aircraftIdtoLotId)
        {
            return this.oracleConn.CreateStoreCommand(
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
                        organization = Utils.GetOrganization((int?)r.Field<decimal?>("FIRM_ID"), or, organizationIdtoLotId),
                        aircraft = Utils.GetAircraft((int?)r.Field<long?>("AC_ID"), ar, aircraftIdtoLotId),
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

        private IList<JObject> getPersonStatuses(int personId, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            return this.oracleConn.CreateStoreCommand(
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

        private IList<JObject> getPersonRatings(int personId, Func<int?, JObject> getPerson, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            Dictionary<string, string> limitationFixups = new Dictionary<string, string>
            {
                { "NO, AVC", "NO:AVC" },
                { "No, Electr. AVC-Line", "No:Electr. AVC-Line" },
                { "No, Electr. AVC", "No:Electr. AVC" },
                { "No, Radio", "No:Radio" },
                { "No, Electr. RADIO", "No:Radio" },
                { "No, Electr, AVC(FAD)", "No:Electr:AVC(FAD)" },
                { "Limited to,  Composite structure aeroplanes", "Limited to: Composite structure aeroplanes" },
                { "Limited to,  Metal structure aeroplanes", "Limited to: Metal structure aeroplanes" }
            };

            Func<string, NomValue[]> transformLimitations = (l) =>
            {
                if (l == null)
                {
                    return null;
                }

                foreach (var kvp in limitationFixups)
                {
                    l = l.Replace(kvp.Key, kvp.Value);
                }

                return l.Split(',').Select(sc => noms["limitations66"].ByCode(sc.Trim())).ToArray();
            };

            Func<string, NomValue[]> transformSubclasses = (s) =>
            {
                if (s == null)
                {
                    return null;
                }

                return s.Split(',').Select(sc => noms["ratingSubClasses"].ByCode(sc.Trim())).ToArray();
            };

            var editions = oracleConn.CreateStoreCommand(
                @"SELECT RD.ID,
                        RD.RATING_CAA_ID,
                        RD.SUBCLASSES,
                        RD.LIMITATIONS,
                        RD.ISSUE_DATE,
                        RD.VALID_DATE,
                        RD.NOTES,
                        RD.NOTES_TRANS,
                        RD.EXAMINER_ID
                    FROM CAA_DOC.RATING_CAA R
                    JOIN CAA_DOC.RATING_CAA_DATES RD ON RD.RATING_CAA_ID = R.ID
                    WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("AND R.PERSON_ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
                        __migrTable = "RATING_CAA_DATES",
                        __RATING_CAA_ID = (int)r.Field<decimal>("RATING_CAA_ID"),

                        ratingSubClasses = transformSubclasses(r.Field<string>("SUBCLASSES")),
                        limitations = transformLimitations(r.Field<string>("LIMITATIONS")),
                        documentDateValidFrom = r.Field<DateTime?>("ISSUE_DATE"),
                        documentDateValidTo = r.Field<DateTime?>("VALID_DATE"),
                        notes = r.Field<string>("NOTES"),
                        notesAlt = r.Field<string>("NOTES_TRANS"),
                        inspector = getPerson((int?)r.Field<decimal?>("EXAMINER_ID"))
                    }))
                .ToList()
                .GroupBy(r => r.Get<int>("__RATING_CAA_ID"))
                .ToDictionary(g => g.Key,
                    g => g.Select(r => Utils.Pluck(r,
                        new string[] 
                        {
                            "__oldId",
                            "__migrTable",

                            "ratingSubClasses",
                            "limitations",
                            "documentDateValidFrom",
                            "documentDateValidTo",
                            "notes",
                            "notesAlt",
                            "inspector"
                        })));

            return this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.RATING_CAA WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and PERSON_ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
                        __migrTable = "RATING_CAA",

                        staffType = noms["staffTypes"].ByOldId(r.Field<decimal?>("STAFF_TYPE_ID").ToString()),
                        personRatingLevel = noms["personRatingLevels"].ByCode(r.Field<string>("RATING_STEPEN")),
                        ratingClass = noms["ratingClassGroups"].ByOldId(r.Field<decimal?>("RATING_CLASS_ID").ToString()),
                        ratingType = noms["ratingTypes"].ByOldId(r.Field<decimal?>("RATING_TYPE_ID").ToString()),
                        authorization = noms["authorizations"].ByOldId(r.Field<decimal?>("AUTHORIZATION_ID").ToString()),
                        personRatingModel = noms["personRatingModels"].ByCode(r.Field<string>("RATING_MODEL")),
                        locationIndicator = noms["locationIndicators"].ByOldId(r.Field<long?>("INDICATOR_ID").ToString()),
                        sector = r.Field<string>("SECTOR"),
                        aircraftTypeGroup = noms["aircraftTypeGroups"].ByOldId(r.Field<long?>("RATING_GROUP66_ID").ToString()),
                        aircraftTypeCategory = noms["aircraftClases66"].ByOldId(r.Field<long?>("RATING_CAT66_ID").ToString()).NomValueId(),
                        caa = noms["caa"].ByOldId(r.Field<decimal?>("CAA_ID").ToString()),
                        editions = editions[(int)r.Field<decimal>("ID")]
                    }))
                .ToList();;
        }
    }
}
