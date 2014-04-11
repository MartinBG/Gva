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

namespace Gva.MigrationTool.Sets
{
    class Person
    {
        public static Dictionary<string, Dictionary<string, NomValue>> noms;

        public static void migratePersons(OracleConnection con, Dictionary<string, Dictionary<string, NomValue>> n)
        {
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            using (IUnitOfWork unitOfWork = new UnitOfWork(
                new IDbConfiguration[] { new RegsDbConfiguration(), new CommonDbConfiguration(), new DocsDbConfiguration(), new GvaDbConfiguration() }))
            {
                unitOfWork.DbContext.Configuration.AutoDetectChangesEnabled = false;

                var lotRepository = new LotRepository(unitOfWork);
                var userRepository = new UserRepository(unitOfWork);
                var lotEventDispatcher = new LotEventDispatcher(new List<ILotEventHandler>()
                {
                    new ApplicationsViewAircraftHandler(unitOfWork),
                    new ApplicationsViewPersonHandler(unitOfWork),
                    new ApplicationsViewOrganizationHandler(unitOfWork),
                    new ApplicationsViewAirportHandler(unitOfWork),
                    new ApplicationsViewEquipmentHandler(unitOfWork),
                    new AircraftApplicationHandler(unitOfWork, userRepository),
                    new AircraftDebtHandler(unitOfWork, userRepository),
                    new AircraftInspectionHandler(unitOfWork, userRepository),
                    new AircraftOccurrenceHandler(unitOfWork, userRepository),
                    new AircraftOtherHandler(unitOfWork, userRepository),
                    new AircraftOwnerHandler(unitOfWork, userRepository),
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

                var context = new UserContext(2);

                noms = n;
                Set personSet = lotRepository.GetSet("Person");

                var personIds = Person.getPersonIds(con);
                foreach (var personId in personIds)
                {
                    var lot = personSet.CreateLot(context);

                    var personData = Person.getPersonData(con, personId);
                    lot.CreatePart("personData", personData, context);

                    var personAddresses = Person.getPersonAddresses(con, personId);
                    foreach (var address in personAddresses)
                    {
                        lot.CreatePart("personAddresses/*", address, context);
                    }

                    var personDocumentIds = Person.getPersonDocumentIds(con, personId);
                    foreach (var docId in personDocumentIds)
                    {
                        lot.CreatePart("personDocumentIds/*", docId, context);
                    }

                    var personDocumentChecks = Person.getPersonDocumentChecks(con, personId);
                    foreach (var docCheck in personDocumentChecks)
                    {
                        lot.CreatePart("personDocumentChecks/*", docCheck, context);
                    }

                    var personDocumentEducations = Person.getPersonDocumentEducations(con, personId);
                    foreach (var docEducation in personDocumentEducations)
                    {
                        lot.CreatePart("personDocumentEducations/*", docEducation, context);
                    }

                    var personDocumentEmployments = Person.getPersonDocumentEmployments(con, personId);
                    foreach (var docEmployment in personDocumentEmployments)
                    {
                        lot.CreatePart("personDocumentEmployments/*", docEmployment, context);
                    }

                    var personDocumentMedicals = Person.getPersonDocumentMedicals(con, personId);
                    foreach (var docMedical in personDocumentMedicals)
                    {
                        lot.CreatePart("personDocumentMedicals/*", docMedical, context);
                    }

                    var personDocumentOthers = Person.getPersonDocumentOthers(con, personId);
                    foreach (var docOther in personDocumentOthers)
                    {
                        lot.CreatePart("personDocumentOthers/*", docOther, context);
                    }

                    //var personDocumentApplications = Person.getPersonDocumentApplications(con, personId);
                    //foreach (var docApplication in personDocumentApplications)
                    //{
                    //    lot.CreatePart("personDocumentApplications/*", docApplication, context);
                    //}

                    //var personDocumentExams = Person.getPersonDocumentExams(con, personId);
                    //foreach (var docExam in personDocumentExams)
                    //{
                    //    lot.CreatePart("personDocumentExams/*", docExam, context);
                    //}

                    var personDocumentTrainings = Person.getPersonDocumentTrainings(con, personId);
                    foreach (var docTraining in personDocumentTrainings)
                    {
                        lot.CreatePart("personDocumentTrainings/*", docTraining, context);
                    }

                    var personFlyingExperiences = Person.getPersonFlyingExperiences(con, personId);
                    foreach (var flyingExperience in personFlyingExperiences)
                    {
                        lot.CreatePart("personFlyingExperiences/*", flyingExperience, context);
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

                    var personStatuses = Person.getPersonStatuses(con, personId);
                    foreach (var personStatus in personStatuses)
                    {
                        lot.CreatePart("personStatuses/*", personStatus, context);
                    }

                    //var personLicences = Person.getPersonLicences(con, personId);
                    //foreach (var personLicence in personLicences)
                    //{
                    //    lot.CreatePart("/licences/*", personLicence, context);
                    //}

                    lot.Commit(context, lotEventDispatcher);

                    unitOfWork.Save();
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
            return con.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.PERSON WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID = {0}", personId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
                        __migrTable = "PERSON",
                        lin = r.Field<string>("LIN"),
                        uin = r.Field<string>("EGN"),
                        firstName = r.Field<string>("NAME"),
                        firstNameAlt = r.Field<string>("NAME_TRANS"),
                        middleName = r.Field<string>("SURNAME"),
                        middleNameAlt = r.Field<string>("SURNAME_TRANS"),
                        lastName = r.Field<string>("FAMILY"),
                        lastNameAlt = r.Field<string>("FAMILY_TRANS"),
                        dateOfBirth = r.Field<DateTime>("DATE_OF_BIRTH"),
                        placeOfBirthId = noms["cities"].ByOldId(r.Field<decimal?>("PLACE_OF_BIRTH_ID").ToString()).NomValueId(),
                        countryId = noms["countries"].ByOldId(r.Field<decimal?>("COUNTRY_ID").ToString()).NomValueId(),
                        sexId = noms["gender"].ByOldId(r.Field<decimal?>("SEX_ID").ToString()).NomValueId(),
                        email = r.Field<string>("E_MAIL"),
                        fax = r.Field<string>("FAX"),
                        phone1 = r.Field<string>("PHONE1"),
                        phone2 = r.Field<string>("PHONE2"),
                        phone3 = r.Field<string>("PHONE3"),
                        phone4 = r.Field<string>("PHONE4"),
                        phone5 = r.Field<string>("PHONE5")
                    })))
                .Single();
        }

        public static IList<JObject> getPersonAddresses(OracleConnection con, int personId)
        {
            return con.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.PERSON_ADDRESS WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and PERSON_ID = {0}", personId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
                        __migrTable = "PERSON_ADDRESS",
                        addressTypeId = noms["addressTypes"].ByOldId(r.Field<decimal?>("ADDRESS_TYPE_ID").ToString()).NomValueId(),
                        settlementId = noms["cities"].ByOldId(r.Field<decimal?>("TOWN_VILLAGE_ID").ToString()).NomValueId(),
                        address = r.Field<string>("ADDRESS"),
                        addressAlt = r.Field<string>("ADDRESS_TRANS"),
                        phone = r.Field<string>("PHONES"),
                        valid = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        postalCode = r.Field<string>("POSTAL_CODE")
                    })))
                .ToList();
        }

        public static IList<JObject> getPersonDocumentIds(OracleConnection con, int personId)
        {
            return con.CreateStoreCommand(
                @"SELECT * FROM 
                    CAA_DOC.PERSON_DOCUMENT 
                WHERE DOCUMENT_TYPE_ID in (SELECT ID FROM CAA_DOC.NM_DOCUMENT_TYPE WHERE CODE in ('3','4','5')) and 
                      DOCUMENT_ROLE_ID in (SELECT ID FROM CAA_DOC.NM_DOCUMENT_ROLE WHERE CODE = '2') 
                      {0}",
                new DbClause("and PERSON_ID = {0}", personId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
                        __migrTable = "PERSON_DOCUMENT",
                        documentNumber = r.Field<string>("DOC_NO"),
                        documentDateValidFrom = r.Field<DateTime>("VALID_FROM"),
                        documentDateValidTo = r.Field<DateTime?>("VALID_TO"),
                        documentPublisher = r.Field<string>("DOC_PUBLISHER"),
                        personOtherDocumentTypeId = noms["documentTypes"].ByOldId(r.Field<decimal?>("DOCUMENT_TYPE_ID").ToString()).NomValueId(),
                        valid = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        notes = r.Field<string>("NOTES"),
                        bookPageNumber = r.Field<decimal?>("BOOK_PAGE_NO"),
                        pageCount = r.Field<decimal?>("PAGES_COUNT")
                    })))
                .ToList();
        }

        public static IList<JObject> getPersonDocumentChecks(OracleConnection con, int personId)
        {
            return con.CreateStoreCommand(
                @"SELECT * FROM
                    CAA_DOC.PERSON_DOCUMENT
                    WHERE DOCUMENT_TYPE_ID in (SELECT ID FROM CAA_DOC.NM_DOCUMENT_TYPE WHERE CODE not in ('3','4','5')) and
                          DOCUMENT_ROLE_ID in (SELECT ID FROM CAA_DOC.NM_DOCUMENT_ROLE WHERE CATEGORY_CODE = 'T')
                          {0}",
                new DbClause("and PERSON_ID = {0}", personId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
                        __migrTable = "PERSON_DOCUMENT",
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
                        personCheckRatingValue = noms["personCheckRatingValues"].ByCode(r.Field<string>("RATING_VALUE")),
                        documentType = noms["documentTypes"].ByOldId(r.Field<decimal?>("DOCUMENT_TYPE_ID").ToString()),
                        documentRole = noms["documentRoles"].ByOldId(r.Field<decimal?>("DOCUMENT_ROLE_ID").ToString()),
                        valid = noms["boolean"].ByCode(r.Field<string>("VALID_YN") == "Y" ? "Y" : "N"),
                        notes = r.Field<string>("NOTES"),
                        bookPageNumber = r.Field<decimal?>("BOOK_PAGE_NO"),
                        pageCount = r.Field<decimal?>("PAGES_COUNT")
                    })))
                .ToList();
        }

        public static IList<JObject> getPersonDocumentEducations(OracleConnection con, int personId)
        {
            return con.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.EDUCATION WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and PERSON_ID = {0}", personId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
                        __migrTable = "EDUCATION",
                        documentNumber = r.Field<string>("NO"),
                        completionDate = r.Field<DateTime?>("COMPLETION_DATE"),
                        speciality = r.Field<string>("SPECIALITY"),
                        schoolId = noms["schools"].ByOldId(r.Field<decimal?>("SCHOOL_ID").ToString()).NomValueId(),
                        graduationId = noms["graduations"].ByOldId(r.Field<decimal?>("GRADUATION_ID").ToString()).NomValueId(),
                        notes = r.Field<string>("NOTES"),
                        bookPageNumber = r.Field<decimal?>("BOOK_PAGE_NO"),
                        pageCount = r.Field<decimal?>("PAGES_COUNT")
                    })))
                .ToList();
        }

        public static IList<JObject> getPersonDocumentEmployments(OracleConnection con, int personId)
        {
            return con.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.EMPLOYEE WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and PERSON_ID = {0}", personId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
                        __migrTable = "EMPLOYEE",
                        hiredate = r.Field<DateTime?>("HIREDATE"),
                        valid = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        organizationId = r.Field<decimal?>("FIRM_ID"),
                        employmentCategoryId = noms["employmentCategories"].ByOldId(r.Field<decimal?>("JOB_CATEGORY_ID").ToString()).NomValueId(),
                        countryId = noms["countries"].ByOldId(r.Field<decimal?>("COUNTRY_ID").ToString()).NomValueId(),
                        notes = r.Field<string>("NOTES"),
                        bookPageNumber = r.Field<decimal?>("BOOK_PAGE_NO"),
                        pageCount = r.Field<decimal?>("PAGES_COUNT")
                    })))
                .ToList();
        }

        public static IList<JObject> getPersonDocumentMedicals(OracleConnection con, int personId)
        {
            var medLimitations = con
                .CreateStoreCommand(
                    @"SELECT * FROM 
                        CAA_DOC.MED_CERT_LIMITATION 
                        WHERE MED_CERT_ID in (SELECT ID FROM CAA_DOC.MED_CERT WHERE {0})",
                        new DbClause("PERSON_ID = {0}", personId))
                .Materialize(r =>
                    new
                    {
                        medId = (int)r.Field<decimal?>("MED_CERT_ID"),
                        limitationId = noms["medLimitation"].ByOldId(r.Field<decimal?>("MED_LIMIT_ID").ToString()).NomValueId(),
                    })
                .GroupBy(l => l.medId)
                .ToDictionary(g => g.Key, g => g.Select(l => l.limitationId).ToArray());

            return con
                .CreateStoreCommand(
                    @"SELECT * FROM CAA_DOC.MED_CERT WHERE {0} {1}",
                        new DbClause("1=1"),
                        new DbClause("and PERSON_ID = {0}", personId))
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
                        __migrTable = "MED_CERT",
                        documentNumberPrefix = r.Field<string>("NO1"),
                        documentNumber = r.Field<string>("NO2"),
                        documentNumbeSuffix = r.Field<string>("NO3"),
                        documentDateValidFrom = r.Field<DateTime?>("DATE_FROM"),
                        documentDateValidTo = r.Field<DateTime?>("DATE_TO"),
                        medClassId = noms["medClasses"].ByOldId(r.Field<decimal?>("MED_CLASS_ID").ToString()).NomValueId(),
                        documentPublisher = r.Field<string>("PUBLISHER_NAME"),
                        limitationIds = medLimitations.ContainsKey((int)r.Field<decimal>("ID")) ? medLimitations[(int)r.Field<decimal>("ID")] : null,
                        notes = r.Field<string>("NOTES"),
                        bookPageNumber = r.Field<decimal?>("BOOK_PAGE_NO"),
                        pageCount = r.Field<decimal?>("PAGES_COUNT")
                    })))
                .ToList();
        }

        public static IList<JObject> getPersonDocumentOthers(OracleConnection con, int personId)
        {
            return con.CreateStoreCommand(
                @"SELECT * FROM
                    CAA_DOC.PERSON_DOCUMENT
                    WHERE DOCUMENT_TYPE_ID in (SELECT ID FROM CAA_DOC.NM_DOCUMENT_TYPE WHERE CODE not in ('3','4','5','115') and (id_direction is null or (id_direction in ('F','G','T')))) and
                          DOCUMENT_ROLE_ID in (SELECT ID FROM CAA_DOC.NM_DOCUMENT_ROLE WHERE CATEGORY_CODE = 'A' and (id_direction is null or (id_direction in ('F','G','T'))))
                          {0}",
                new DbClause("and PERSON_ID = {0}", personId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
                        __migrTable = "PERSON_DOCUMENT",
                        documentNumber = r.Field<string>("DOC_NO"),
                        documentPersonNumber = r.Field<decimal?>("PERSON_NUM"),
                        documentDateValidFrom = r.Field<DateTime?>("VALID_FROM"),
                        documentDateValidTo = r.Field<DateTime?>("VALID_TO"),
                        documentPublisher = r.Field<string>("DOC_PUBLISHER"),
                        personOtherDocumentTypeId = noms["documentTypes"].ByOldId(r.Field<decimal?>("DOCUMENT_TYPE_ID").ToString()).NomValueId(),
                        personOtherDocumentRoleId = noms["documentRoles"].ByOldId(r.Field<decimal?>("DOCUMENT_ROLE_ID").ToString()).NomValueId(),
                        valid = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        notes = r.Field<string>("NOTES"),
                        bookPageNumber = r.Field<decimal?>("BOOK_PAGE_NO"),
                        pageCount = r.Field<decimal?>("PAGES_COUNT")
                    })))
                .ToList();
        }


        //TODO
        public static IList<JObject> getPersonDocumentApplications(OracleConnection con, int personId)
        {
            return con.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.REQUEST WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and PERSON_ID = {0}", personId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
                        __migrTable = "REQUEST",
                    })))
                .ToList();
        }

        public static IList<JObject> getPersonDocumentExams(OracleConnection con, int personId)
        {
            return con.CreateStoreCommand(
                @"SELECT * FROM 
                    CAA_DOC.PERSON_DOCUMENT
                    WHERE DOCUMENT_TYPE_ID in (SELECT ID FROM CAA_DOC.NM_DOCUMENT_TYPE WHERE CODE = '2') and
                          DOCUMENT_ROLE_ID in (SELECT ID FROM CAA_DOC.NM_DOCUMENT_ROLE WHERE CODE = '6')
                          {0}",
                new DbClause("and PERSON_ID = {0}", personId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
                        __migrTable = "PERSON_DOCUMENT",
                        documentNumber = r.Field<string>("DOC_NO"),
                        documentDateValidFrom = r.Field<DateTime?>("VALID_FROM"),
                        documentPublisher = r.Field<string>("DOC_PUBLISHER"),
                        valid = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        notes = r.Field<string>("NOTES"),
                        bookPageNumber = r.Field<decimal?>("BOOK_PAGE_NO"),
                        pageCount = r.Field<decimal?>("PAGES_COUNT")
                    })))
                .ToList();
        }

        public static IList<JObject> getPersonFlyingExperiences(OracleConnection con, int personId)
        {
            return con.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.FLYING_EXPERIENCE WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and PERSON_ID = {0}", personId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
                        __migrTable = "FLYING_EXPERIENCE",
                        staffTypeId = noms["staffTypes"].ByOldId(r.Field<decimal?>("STAFF_TYPE_ID").ToString()).NomValueId(),
                        documentDate = r.Field<DateTime?>("DOCUMENT_DATE"),
                        period = new { month = r.Field<string>("PERIOD_MONTH"), year = r.Field<string>("PERIOD_YEAR") },
                        organizationId = r.Field<decimal?>("FIRM_ID"),
                        aircraftId = r.Field<long?>("AC_ID"),
                        ratingTypeId = noms["ratingTypes"].ByOldId(r.Field<decimal?>("RATING_TYPE_ID").ToString()).NomValueId(),
                        ratingClassId = noms["ratingClassGroups"].ByOldId(r.Field<decimal?>("RATING_CLASS_ID").ToString()).NomValueId(),
                        authorizationId = noms["authorizations"].ByOldId(r.Field<decimal?>("AUTHORIZATION_ID").ToString()).NomValueId(),
                        licenceTypeId = noms["licenceTypes"].ByOldId(r.Field<decimal?>("LICENCE_TYPE_ID").ToString()).NomValueId(),
                        locationIndicatorId = noms["locationIndicators"].ByOldId(r.Field<long?>("INDICATOR_ID").ToString()).NomValueId(),
                        sector = r.Field<string>("SECTOR"),
                        experienceRoleId = noms["experienceRoles"].ByOldId(r.Field<decimal?>("EXPERIENCE_ROLE_ID").ToString()).NomValueId(),
                        experienceMeasureId = noms["experienceMeasures"].ByOldId(r.Field<decimal?>("MEASURE_ID").ToString()).NomValueId(),
                        dayIFR = new { hours = r.Field<short?>("AMNTH_DAY_I"), minutes = r.Field<short?>("AMNTM_DAY_I") },
                        dayVFR = new { hours = r.Field<short?>("AMNTH_DAY_V"), minutes = r.Field<short?>("AMNTM_DAY_V") },
                        dayLandings = r.Field<short?>("LND_DAY"),
                        nightIFR = new { hours = r.Field<short?>("AMNTH_NGT_I"), minutes = r.Field<short?>("AMNTM_NGT_I") },
                        nightVFR = new { hours = r.Field<short?>("AMNTH_NGT_V"), minutes = r.Field<short?>("AMNTM_NGT_V") },
                        nightLandings = r.Field<short?>("LND_NGT"),
                        Total = new { hours = r.Field<int?>("AMOUNT"), minutes = r.Field<short?>("AMOUNT_M") },
                        TotalDoc = new { hours = r.Field<int?>("AMOUNT_SUM"), minutes = r.Field<short?>("AMOUNT_M_SUM") },
                        TotalLastmonths = new { hours = r.Field<int?>("AMOUNT_12"), minutes = r.Field<short?>("AMOUNT_M_12") },
                        notes = r.Field<string>("NOTES")
                    })))
                .ToList();
        }

        public static IList<JObject> getPersonRatings(OracleConnection con, int personId)
        {
            return con.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.RATING_CAA WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and PERSON_ID = {0}", personId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
                        __migrTable = "RATING_CAA",
                        staffTypeId = noms["staffTypes"].ByOldId(r.Field<decimal?>("STAFF_TYPE_ID").ToString()).NomValueId(),
                        personRatingLevelId = noms["personRatingLevels"].ByCode(r.Field<string>("RATING_STEPEN")).NomValueId(),
                        ratingClassId = noms["ratingClassGroups"].ByOldId(r.Field<decimal?>("RATING_CLASS_ID").ToString()).NomValueId(),
                        ratingTypeId = noms["ratingTypes"].ByOldId(r.Field<decimal?>("RATING_TYPE_ID").ToString()).NomValueId(),
                        authorizationId = noms["authorizations"].ByOldId(r.Field<decimal?>("AUTHORIZATION_ID").ToString()).NomValueId(),
                        personRatingModelId = noms["personRatingModels"].ByCode(r.Field<string>("RATING_MODEL")).NomValueId(),
                        locationIndicatorId = noms["locationIndicators"].ByOldId(r.Field<long?>("INDICATOR_ID").ToString()).NomValueId(),
                        sector = r.Field<string>("SECTOR"),
                        aircraftTypeGroupId = noms["aircraftTypeGroups"].ByOldId(r.Field<long?>("RATING_GROUP66_ID").ToString()).NomValueId(),
                        aircraftTypeCategoryId = noms["aircraftClases66"].ByOldId(r.Field<long?>("RATING_CAT66_ID").ToString()).NomValueId(),
                        caaId = noms["caa"].ByOldId(r.Field<decimal?>("CAA_ID").ToString()).NomValueId(),
                    })))
                .ToList();
        }

        public static IList<JObject> getPersonRatingEditions(OracleConnection con, int ratingId)
        {
            return con.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.RATING_CAA_DATES WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and RATING_CAA_ID = {0}", ratingId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
                        __migrTable = "RATING_CAA_DATES",
                        ratingSubClassIds = (r.Field<string>("SUBCLASSES") != null) ? r.Field<string>("SUBCLASSES").Split(',').Select(sc => noms["ratingSubClasses"].ByCode(sc.Trim()).NomValueId()).ToArray() : null,
                        limitationIds = (r.Field<string>("LIMITATIONS") != null) ? r.Field<string>("LIMITATIONS").Split(',').Select(lim => noms["limitations66"].ByCode(lim.Trim()).NomValueId()).ToArray() : null,
                        documentDateValidFrom = r.Field<DateTime?>("ISSUE_DATE"),
                        documentDateValidTo = r.Field<DateTime?>("VALID_DATE"),
                        notes = r.Field<string>("NOTES"),
                        notesAlt = r.Field<string>("NOTES_TRANS"),
                        inspectorId = r.Field<decimal?>("EXAMINER_ID")
                    })))
                .ToList();
        }

        public static IList<JObject> getPersonStatuses(OracleConnection con, int personId)
        {
            return con.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.PERSON_STATE WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and PERSON_ID = {0}", personId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = (int)r.Field<long>("ID"),
                        __migrTable = "PERSON_STATE",
                        personStatusTypeId = noms["personStatusTypes"].ByCode(r.Field<string>("REASON_CODE").ToString()).NomValueId(),
                        documentNumber = r.Field<string>("DOCUMENT_NUM"),
                        documentDateValidFrom = r.Field<DateTime?>("BEGIN_DATE"),
                        documentDateValidTo = r.Field<DateTime?>("END_DATE"),
                        notes = r.Field<string>("REMARKS")
                    })))
                .ToList();
        }

        public static IList<JObject> getPersonDocumentTrainings(OracleConnection con, int personId)
        {
            return con.CreateStoreCommand(
                @"SELECT * FROM
                    CAA_DOC.PERSON_DOCUMENT
                    WHERE DOCUMENT_TYPE_ID in (SELECT ID FROM CAA_DOC.NM_DOCUMENT_TYPE WHERE CODE not in ('3','4','5') and (id_direction is null or (id_direction in ('F','G','T')))) and
                          DOCUMENT_ROLE_ID in (SELECT ID FROM CAA_DOC.NM_DOCUMENT_ROLE WHERE CATEGORY_CODE = 'O' and (id_direction is null or (id_direction in ('F','G','T'))))
                          {0}",
                new DbClause("and PERSON_ID = {0}", personId)
                )
                .Materialize(r => JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(
                    new
                    {
                        __oldId = (int)r.Field<decimal>("ID"),
                        __migrTable = "PERSON_DOCUMENT",
                        documentNumber = r.Field<string>("DOC_NO"),
                        documentPersonNumber = r.Field<decimal?>("PERSON_NUM"),
                        documentDateValidFrom = r.Field<DateTime?>("VALID_FROM"),
                        documentDateValidTo = r.Field<DateTime?>("VALID_TO"),
                        documentPublisher = r.Field<string>("DOC_PUBLISHER"),
                        staffTypeId = noms["staffTypes"].ByOldId(r.Field<decimal?>("STAFF_TYPE_ID").ToString()).NomValueId(),
                        ratingTypeId = noms["ratingTypes"].ByOldId(r.Field<decimal?>("RATING_TYPE_ID").ToString()).NomValueId(),
                        aircraftTypeGroupId = noms["aircraftTypeGroups"].ByOldId(r.Field<long?>("ID_AC_GROUP").ToString()).NomValueId(),
                        ratingClassId = noms["ratingClassGroups"].ByOldId(r.Field<decimal?>("RATING_CLASS_ID").ToString()).NomValueId(),
                        authorizationId = noms["authorizations"].ByOldId(r.Field<decimal?>("AUTHORIZATION_ID").ToString()).NomValueId(),
                        licenceTypeId = noms["licenceTypes"].ByOldId(r.Field<decimal?>("LICENCE_TYPE_ID").ToString()).NomValueId(),
                        locationIndicatorId = noms["locationIndicators"].ByOldId(r.Field<long?>("ID_INDICATOR").ToString()).NomValueId(),
                        sector = r.Field<string>("SECTOR"),
                        engLangLevelId = noms["engLangLevels"].ByOldId(r.Field<long?>("NM_EN_LANG_ID").ToString()).NomValueId(),
                        personOtherDocumentTypeId = noms["documentTypes"].ByOldId(r.Field<decimal?>("DOCUMENT_TYPE_ID").ToString()).NomValueId(),
                        personOtherDocumentRoleId = noms["documentRoles"].ByOldId(r.Field<decimal?>("DOCUMENT_ROLE_ID").ToString()).NomValueId(),
                        valid = r.Field<string>("VALID_YN") == "Y" ? true : false,
                        notes = r.Field<string>("NOTES"),
                        bookPageNumber = r.Field<decimal?>("BOOK_PAGE_NO"),
                        pageCount = r.Field<decimal?>("PAGES_COUNT")
                    })))
                .ToList();
        }
    }
}
