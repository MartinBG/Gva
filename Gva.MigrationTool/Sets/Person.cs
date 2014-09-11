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
using Common.Api.Repositories.UserRepository;
using Regs.Api.LotEvents;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.ModelsDO;
using Common.Json;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Autofac.Features.OwnedInstances;
using Common.Tests;
using Gva.Api.Repositories.PersonRepository;

namespace Gva.MigrationTool.Sets
{
    public class Person
    {
        private Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, IUserRepository, IFileRepository, IApplicationRepository, ICaseTypeRepository, IPersonRepository, ILotEventDispatcher, UserContext>>> dependencyFactory;
        private OracleConnection oracleConn;

        public Person(OracleConnection oracleConn,
            Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, IUserRepository, IFileRepository, IApplicationRepository, ICaseTypeRepository, IPersonRepository, ILotEventDispatcher, UserContext>>> dependencyFactory)
        {
            this.dependencyFactory = dependencyFactory;
            this.oracleConn = oracleConn;
        }

        public Tuple<Dictionary<int, int>, Dictionary<string, int>, Dictionary<int, JObject>> createPersonsLots(Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            Dictionary<int, int> personIdToLotId = new Dictionary<int, int>();
            Dictionary<string, int> personEgnToLotId = new Dictionary<string, int>();
            Dictionary<int, JObject> personLotIdToPersonNom = new Dictionary<int, JObject>();

            using (var dependencies = this.dependencyFactory())
            {
                var unitOfWork = dependencies.Value.Item1;
                var lotRepository = dependencies.Value.Item2;
                var userRepository = dependencies.Value.Item3;
                var fileRepository = dependencies.Value.Item4;
                var applicationRepository = dependencies.Value.Item5;
                var caseTypeRepository = dependencies.Value.Item6;
                var lotEventDispatcher = dependencies.Value.Item8;
                var context = dependencies.Value.Item9;

                unitOfWork.DbContext.Configuration.AutoDetectChangesEnabled = false;

                foreach (var personId in this.getPersonIds())
                {
                    var personCaseTypes = this.getPersonCaseTypes(personId, noms);
                    personCaseTypes.Add(noms["personCaseTypes"].ByAlias("person"));

                    var inspectorData = this.getInspectorData(personId, noms);
                    if (inspectorData != null)
                    {
                        personCaseTypes.Add(noms["personCaseTypes"].ByAlias("inspector"));
                    }

                    var personData = this.getPersonData(personId, noms, personCaseTypes);

                    var lot = lotRepository.CreateLot("Person");

                    caseTypeRepository.AddCaseTypes(lot, personData.GetItems<JObject>("caseTypes").Select(ct => ct.Get<int>("nomValueId")));

                    lot.CreatePart("personData", personData, context);
                    if (inspectorData != null)
                    {
                        lot.CreatePart("inspectorData", inspectorData, context);
                    }

                    lot.Commit(context, lotEventDispatcher);
                    unitOfWork.Save();
                    Console.WriteLine("Created personData part for person with id {0}", personId);

                    int lotId = lot.LotId;
                    personIdToLotId.Add(personId, lotId);

                    var egn = personData.Get<string>("uin");
                    if (!string.IsNullOrWhiteSpace(egn))
                    {
                        personEgnToLotId.Add(egn, lotId);
                    }

                    string name = string.Format("{0} {1} {2}", personData.Get<string>("firstName"), personData.Get<string>("middleName"), personData.Get<string>("lastName"));
                    string nameAlt = string.Format("{0} {1} {2}", personData.Get<string>("firstNameAlt"), personData.Get<string>("middleNameAlt"), personData.Get<string>("lastNameAlt"));
                    personLotIdToPersonNom.Add(lotId, Utils.ToJObject(
                        new
                        {
                            nomValueId = lotId,
                            name = name,
                            nameAlt = nameAlt
                        }));
                }
            }

            return Tuple.Create(personIdToLotId, personEgnToLotId, personLotIdToPersonNom);
        }
         
        public void migratePersons(
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, int> personIdToLotId,
            Func<int?, JObject> getAircraftByApexId,
            Func<int?, JObject> getPersonByApexId,
            Func<int?, JObject> getOrgByApexId)
        {
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            foreach (var personId in this.getPersonIds())
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
                    var personRepository = dependencies.Value.Item7;
                    var lotEventDispatcher = dependencies.Value.Item8;
                    var context = dependencies.Value.Item9;

                    unitOfWork.DbContext.Configuration.AutoDetectChangesEnabled = false;

                    var lot = lotRepository.GetLotIndex(personIdToLotId[personId], fullAccess: true);

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

                    Dictionary<int, string> appApexIdToStaffTypeCode = getPersonApplicationsStaffTypeCodes(personId);

                    Dictionary<int, Tuple<GvaApplication, ApplicationNomDO>> applications =
                        new Dictionary<int, Tuple<GvaApplication, ApplicationNomDO>>();
                    
                    var personDocumentApplications = this.getPersonDocumentApplications(personId, noms, appApexIdToStaffTypeCode);
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
                                        PartIndex = pv.Part.Index,
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

                    var personDocuments = this.getPersonDocuments(personId, nomApplications, noms, appApexIdToStaffTypeCode);
                    Dictionary<int, int> trainingOldIdToPartIndex = new Dictionary<int, int>();
                    Dictionary<int, int> examOldIdToPartIndex = new Dictionary<int, int>();
                    Dictionary<int, int> checkOldIdToPartIndex = new Dictionary<int, int>();
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

                            var pv = addPartWithFiles("personDocumentChecks/*", personDocument);
                            checkOldIdToPartIndex.Add(personDocument.Get<int>("part.__oldId"), pv.Part.Index);
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

                            var pv = addPartWithFiles("personDocumentTrainings/*", personDocument);
                            trainingOldIdToPartIndex.Add(personDocument.Get<int>("part.__oldId"), pv.Part.Index);
                        }
                        else if (personDocument.Get<string>("part.__DOCUMENT_TYPE_CODE") == "2" &&
                          personDocument.Get<string>("part.__DOCUMENT_ROLE_CODE") == "6")
                        {
                            Utils.Pluck(personDocument.Get<JObject>("part"), new string[]
                            {
                                "__oldId",
                                "__migrTable",

                                "documentNumber",
                                "valid",
                                "documentDateValidFrom",
                                "documentDateValidTo",
                                "documentType",
                                "documentRole",
                                "documentPublisher",
                                "notes"
                            });

                            var pv = addPartWithFiles("personDocumentExams/*", personDocument);
                            examOldIdToPartIndex.Add(personDocument.Get<int>("part.__oldId"), pv.Part.Index);
                        }
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

                    var personDocumentEducations = this.getPersonDocumentEducations(personId, nomApplications, noms, appApexIdToStaffTypeCode);
                    foreach (var docEducation in personDocumentEducations)
                    {
                        addPartWithFiles("personDocumentEducations/*", docEducation);
                    }

                    var personDocumentEmployments = this.getPersonDocumentEmployments(personId, noms, getOrgByApexId);
                    foreach (var docEmployment in personDocumentEmployments)
                    {
                        addPartWithFiles("personDocumentEmployments/*", docEmployment);
                    }

                    var personDocumentMedicals = this.getPersonDocumentMedicals(personId, nomApplications, noms, appApexIdToStaffTypeCode);
                    Dictionary<int, int> medicalOldIdToPartIndex = new Dictionary<int, int>();
                    foreach (var docMedical in personDocumentMedicals)
                    {
                        var pv = addPartWithFiles("personDocumentMedicals/*", docMedical);
                        medicalOldIdToPartIndex.Add(docMedical.Get<int>("part.__oldId"), pv.Part.Index);
                    }

                    var personFlyingExperiences = this.getPersonFlyingExperiences(personId, noms, getOrgByApexId, getAircraftByApexId);
                    foreach (var flyingExperience in personFlyingExperiences)
                    {
                        lot.CreatePart("personFlyingExperiences/*", flyingExperience, context);
                    }

                    var personStatuses = this.getPersonStatuses(personId, noms);
                    foreach (var personStatus in personStatuses)
                    {
                        lot.CreatePart("personStatuses/*", personStatus, context);
                    }

                    var personRatings = this.getPersonRatings(personId, getPersonByApexId, noms);
                    Dictionary<int, int> ratingOldIdToPartIndex = new Dictionary<int, int>();
                    foreach (var personRating in personRatings)
                    {
                        int nextIndex = 0;

                        foreach (var edition in personRating["editions"].Cast<JObject>())
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

                        ratingOldIdToPartIndex.Add(personRating.Get<int>("__oldId"), pv.Part.Index);
                    }

                    var personLicences = this.getPersonLicences(personId, getPersonByApexId, nomApplications, noms, ratingOldIdToPartIndex, medicalOldIdToPartIndex, trainingOldIdToPartIndex, examOldIdToPartIndex, checkOldIdToPartIndex);
                    Dictionary<int, int> licenceOldIdToPartIndex = new Dictionary<int, int>();
                    foreach (var personLicence in personLicences)
                    {
                        int nextIndex = 0;

                        foreach (var edition in personLicence["editions"].Cast<JObject>())
                        {
                            edition.Add("index", nextIndex);
                            nextIndex++;
                        }

                        personLicence.Add("nextIndex", nextIndex);

                        var pv = lot.CreatePart("licences/*", personLicence, context);
                        licenceOldIdToPartIndex.Add(personLicence.Get<int>("__oldId"), pv.Part.Index);
                    }

                    //replace included licence ids with part indexes
                    foreach (var personLicence in personLicences)
                    {
                        foreach (var edition in personLicence.GetItems<JObject>("editions"))
                        {
                            edition.Property("includedLicences").Value = new JArray(edition.GetItems<int>("includedLicences").Select(l => licenceOldIdToPartIndex[l]).ToArray());
                        }
                    }

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
                .Materialize(r => r.Field<int>("ID"))
                .OrderByDescending(r => r)
                //.Take(1000)
                //.Where(r => r == 6730) //РАДОСТИНА
                .Where(id => new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 13, 14, 16, 19, 21, 24, 38, 41, 42, 61, 101, 102, 104, 122, 123, 124, 125, 127, 128, 129, 130, 131, 132, 133, 134, 135, 142, 150, 162, 177, 182, 189, 259, 265, 288, 442, 453, 462, 469, 518, 522, 536, 563, 682, 704, 762, 782, 784, 803, 822, 849, 862, 882, 924, 942, 962, 969, 988, 989, 991, 1006, 1013, 1015, 1021, 1026, 1030, 1063, 1070, 1071, 1087, 1104, 1114, 1116, 1117, 1149, 1150, 1153, 1222, 1268, 1302, 1341, 1344, 1405, 1500, 1544, 1546, 1581, 1604, 1605, 1608, 1619, 1678, 1738, 1800, 1828, 1834, 1938, 2007, 2103, 2112, 2139, 2143, 2193, 2219, 2286, 2318, 2373, 2396, 2414, 2417, 2425, 2590, 2606, 2631, 2644, 2654, 2659, 2666, 2683, 2708, 2712, 2713, 2715, 2748, 2783, 2786, 2889, 2894, 2953, 2955, 2961, 3059, 3095, 3100, 3285, 3294, 3372, 3373, 3394, 3458, 3491, 3521, 3569, 3646, 3679, 3816, 3897, 3913, 3933, 3959, 3999, 4015, 4028, 4039, 4100, 4124, 4128, 4162, 4213, 4324, 4535, 5443, 5463, 5464, 5465, 5466, 5467, 5468, 5483, 5603, 5823, 6023, 6203, 6730, 7328, 8086, 9184, 9200 }.Contains(id))
                .ToList();
        }

        private NomValue getPersonCaseTypeByStaffTypeCode(Dictionary<string, Dictionary<string, NomValue>> noms, string code)
        {
            var caseTypeAliases = new Dictionary<string, string>()
            {
                { "F", "flightCrew" },
                { "T", "ovd"},
                { "G", "to_vs"},
                { "M", "to_suvd"}
            };

            return noms["personCaseTypes"].ByAlias(caseTypeAliases[code]);
        }

        private List<NomValue> getPersonCaseTypes(int personId, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            return this.oracleConn.CreateStoreCommand(
                @"SELECT CODE FROM CAA_DOC.NM_STAFF_TYPE WHERE ID IN 
                    (SELECT STAFF_TYPE_ID FROM CAA_DOC.LICENCE L INNER JOIN CAA_DOC.NM_LICENCE_TYPE LT ON L.LICENCE_TYPE_ID = LT.ID WHERE {0}
                    UNION ALL SELECT STAFF_TYPE_ID FROM CAA_DOC.RATING_CAA WHERE {0}
                    UNION ALL SELECT PD.STAFF_TYPE_ID FROM CAA_DOC.PERSON_DOCUMENT PD INNER JOIN CAA_DOC.NM_LICENCE_TYPE LT ON PD.LICENCE_TYPE_ID = LT.ID WHERE {0})",
                new DbClause("PERSON_ID = {0}", personId))
                .Materialize(r => r.Field<string>("CODE"))
                .Select(c => getPersonCaseTypeByStaffTypeCode(noms, c))
                .ToList();
        }

        private JObject getPersonData(int personId, Dictionary<string, Dictionary<string, NomValue>> noms, List<NomValue> caseTypes)
        {
            var lins = new Dictionary<string, string>()
            {
                { "1", "pilots"           },
                { "2", "flyingCrew"       },
                { "3", "crewStaff"        },
                { "4", "headFlights"      },
                { "5", "airlineEngineers" },
                { "6", "dispatchers"      },
                { "7", "paratroopers"     },
                { "8", "engineersRVD"     },
                { "9", "deltaplaner"      }
            };

            return this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.PERSON WHERE {0}",
                new DbClause("ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "PERSON",
                        caseTypes = caseTypes,
                        lin = r.Field<int?>("LIN"),
                        linType = r.Field<string>("LIN") != null ?
                            noms["linTypes"].ByCode(lins[r.Field<string>("LIN").Substring(0, 1)]) :
                            noms["linTypes"].ByCode("none"),
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
                @"SELECT * FROM CAA_DOC.PERSON_ADDRESS WHERE {0}",
                new DbClause("PERSON_ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
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

        private Dictionary<int, string> getPersonApplicationsStaffTypeCodes(int personId)
        {
            return this.oracleConn.CreateStoreCommand(
                @"SELECT R.ID REQUEST_ID,
                        ST.CODE LICENCE_STAFF_TYPE_CODE
                    FROM CAA_DOC.REQUEST R
                    LEFT OUTER JOIN CAA_DOC.LICENCE_LOG LL ON LL.REQUEST_ID = R.ID
                    LEFT OUTER JOIN CAA_DOC.LICENCE L ON LL.LICENCE_ID = L.ID
                    LEFT OUTER JOIN CAA_DOC.NM_LICENCE_TYPE LT ON L.LICENCE_TYPE_ID = LT.ID
                    LEFT OUTER JOIN CAA_DOC.NM_STAFF_TYPE ST ON ST.ID = LT.STAFF_TYPE_ID
                    WHERE {0}",
                new DbClause("R.APPLICANT_PERSON_ID = {0}", personId)
                )
                .Materialize(r =>
                    new
                    {
                        REQUEST_ID = r.Field<int>("REQUEST_ID"),
                        LICENCE_STAFF_TYPE_CODE = r.Field<string>("LICENCE_STAFF_TYPE_CODE"),
                    })
                .GroupBy(r => r.REQUEST_ID)
                .Select(g =>
                    new
                    {
                        REQUEST_ID = g.Key,
                        LICENCE_STAFF_TYPE_CODE = g.Select(r => r.LICENCE_STAFF_TYPE_CODE).Where(c => !string.IsNullOrEmpty(c)).FirstOrDefault()
                    })
                .ToDictionary(r => r.REQUEST_ID, r => r.LICENCE_STAFF_TYPE_CODE);
        }

        private IList<JObject> getPersonDocumentApplications(int personId, Dictionary<string, Dictionary<string, NomValue>> noms, Dictionary<int, string> appApexIdToStaffTypeCode)
        {
            var parts = this.oracleConn.CreateStoreCommand(
                @"SELECT R.ID,
                        R.BOOK_PAGE_NO,
                        R.PAGES_COUNT,
                        R.DOC_NO,
                        R.DOC_DATE,
                        R.REQUEST_DATE,
                        R.NOTES,
                        R.REQUEST_TYPE_ID,
                        R.PAYMENT_REASON_ID,
                        R.CURRENCY_ID,
                        R.TAX_AMOUNT
                    FROM CAA_DOC.REQUEST R
                    WHERE {0}",
                new DbClause("R.APPLICANT_PERSON_ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "REQUEST",

                        __BOOK_PAGE_NO = r.Field<int?>("BOOK_PAGE_NO"),
                        __PAGES_COUNT = r.Field<int?>("PAGES_COUNT"),
                        __REQUEST_DATE = r.Field<DateTime?>("REQUEST_DATE"),

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
                    WHERE {0}",
                new DbClause("R.APPLICANT_PERSON_ID = {0}", personId)
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
                    let licenceStaffTypeCode = appApexIdToStaffTypeCode[part.Get<int>("__oldId")]
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
                                    new JProperty("caseType",
                                        (licenceStaffTypeCode != null ? Utils.ToJObject(getPersonCaseTypeByStaffTypeCode(noms, licenceStaffTypeCode)) : null)
                                        ?? Utils.ToJObject(noms["personCaseTypes"].ByAlias("person"))),
                                    new JProperty("bookPageNumber", bookPageNumber),
                                    new JProperty("pageCount", pageCount),
                                    new JProperty("applications", new JArray()))))))
                    .ToList();
        }

        private IList<JObject> getPersonDocuments(int personId, IDictionary<int, JObject> nomApplications, Dictionary<string, Dictionary<string, NomValue>> noms, Dictionary<int, string> appApexIdToStaffTypeCode)
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
                        DR.CATEGORY_CODE DOCUMENT_ROLE_CATEGORY_CODE,
                        ST.CODE LICENCE_STAFF_TYPE_CODE
                    FROM CAA_DOC.PERSON_DOCUMENT PD
                    JOIN CAA_DOC.NM_DOCUMENT_TYPE DT ON PD.DOCUMENT_TYPE_ID = DT.ID
                    JOIN CAA_DOC.NM_DOCUMENT_ROLE DR ON PD.DOCUMENT_ROLE_ID = DR.ID
                    LEFT OUTER JOIN CAA_DOC.NM_LICENCE_TYPE LT ON PD.LICENCE_TYPE_ID = LT.ID
                    LEFT OUTER JOIN CAA_DOC.NM_STAFF_TYPE ST ON ST.ID = LT.STAFF_TYPE_ID
                    WHERE {0}",
                new DbClause("PD.PERSON_ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "PERSON_DOCUMENT",

                        __DOCUMENT_TYPE_CODE = r.Field<string>("DOCUMENT_TYPE_CODE"),
                        __DOCUMENT_TYPE_ID_DIRECTION = r.Field<string>("DOCUMENT_TYPE_ID_DIRECTION"),

                        __DOCUMENT_ROLE_CODE = r.Field<string>("DOCUMENT_ROLE_CODE"),
                        __DOCUMENT_ROLE_CATEGORY_CODE = r.Field<string>("DOCUMENT_ROLE_CATEGORY_CODE"),
                        __DOCUMENT_ROLE_ID_DIRECTION = r.Field<string>("DOCUMENT_ROLE_ID_DIRECTION"),

                        __BOOK_PAGE_NO = r.Field<int?>("BOOK_PAGE_NO"),
                        __PAGES_COUNT = r.Field<int?>("PAGES_COUNT"),

                        __LICENCE_STAFF_TYPE_CODE = r.Field<string>("LICENCE_STAFF_TYPE_CODE"),

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
                    WHERE {0}",
                new DbClause("PD.PERSON_ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("DOC_ID"),
                        __migrTable = "DOCLIB_DOCUMENTS",
                        __PERSON_DOCUMENT_ID = r.Field<int>("ID"),

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
                    WHERE {0}",
                new DbClause("PD.PERSON_ID = {0}", personId)
                )
                .Materialize(r =>
                    new
                    {
                        PERSON_DOCUMENT_ID = r.Field<int>("PERSON_DOCUMENT_ID"),
                        REQUEST_ID = r.Field<int>("REQUEST_ID")
                    })
                .ToList();

            return (from part in parts
                    join file in files on part.Get<int>("__oldId") equals file.Get<int>("__PERSON_DOCUMENT_ID") into fg
                    join app in apps on part.Get<int>("__oldId") equals app.PERSON_DOCUMENT_ID into ag
                    let file = fg.Any() ? fg.Single() : null //throw if more than one files present
                    let bookPageNumber = part.Get<int?>("__BOOK_PAGE_NO").ToString()
                    let pageCount = part.Get<int?>("__PAGES_COUNT")
                    let appLicenceStaffTypeCode = ag.Select(a => appApexIdToStaffTypeCode[a.REQUEST_ID]).Where(c => c != null).FirstOrDefault()
                    let licenceStaffTypeCode = part.Get<string>("__LICENCE_STAFF_TYPE_CODE")
                    select new JObject(
                        new JProperty("part", part),
                        new JProperty("files", 
                            new JArray(
                                new JObject(
                                    new JProperty("isAdded", true),
                                    new JProperty("file", Utils.Pluck(file, new string[] { "key", "name", "mimeType" })),
                                    new JProperty("caseType",
                                        (appLicenceStaffTypeCode != null ? Utils.ToJObject(getPersonCaseTypeByStaffTypeCode(noms, appLicenceStaffTypeCode)) : null)
                                        ?? (licenceStaffTypeCode != null ? Utils.ToJObject(getPersonCaseTypeByStaffTypeCode(noms, licenceStaffTypeCode)) : null)
                                        ?? Utils.ToJObject(noms["personCaseTypes"].ByAlias("person"))),
                                    new JProperty("bookPageNumber", bookPageNumber),
                                    new JProperty("pageCount", pageCount),
                                    new JProperty("applications", new JArray(ag.Select(a => nomApplications[a.REQUEST_ID]))))))))
                    .ToList();
        }

        private IList<JObject> getPersonDocumentEducations(int personId, IDictionary<int, JObject> nomApplications, Dictionary<string, Dictionary<string, NomValue>> noms, Dictionary<int, string> appApexIdToStaffTypeCode)
        {
            var parts = this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.EDUCATION WHERE {0}",
                new DbClause("PERSON_ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
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
                    WHERE {0}",
                new DbClause("E.PERSON_ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("DOC_ID"),
                        __migrTable = "DOCLIB_DOCUMENTS",
                        __EDUCATION_ID = r.Field<int>("ID"),

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
                    WHERE {0}",
                new DbClause("E.PERSON_ID = {0}", personId)
                )
                .Materialize(r =>
                    new
                    {
                        EDUCATION_ID = r.Field<int>("EDUCATION_ID"),
                        REQUEST_ID = r.Field<int>("REQUEST_ID")
                    })
                .ToList();

            return (from part in parts
                    join file in files on part.Get<int>("__oldId") equals file.Get<int>("__EDUCATION_ID") into fg
                    join app in apps on part.Get<int>("__oldId") equals app.EDUCATION_ID into ag
                    let file = fg.Any() ? fg.Single() : null //throw if more than one files present
                    let bookPageNumber = part.Get<int?>("__BOOK_PAGE_NO").ToString()
                    let pageCount = part.Get<int?>("__PAGES_COUNT")
                    let appLicenceStaffTypeCode = ag.Select(a => appApexIdToStaffTypeCode[a.REQUEST_ID]).Where(c => c != null).FirstOrDefault()
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
                                    new JProperty("caseType",
                                        (appLicenceStaffTypeCode != null ? Utils.ToJObject(getPersonCaseTypeByStaffTypeCode(noms, appLicenceStaffTypeCode)) : null)
                                        ?? Utils.ToJObject(noms["personCaseTypes"].ByAlias("person"))),
                                    new JProperty("bookPageNumber", bookPageNumber),
                                    new JProperty("pageCount", pageCount),
                                    new JProperty("applications", new JArray(ag.Select(a => nomApplications[a.REQUEST_ID]))))))))
                    .ToList();
        }

        private IList<JObject> getPersonDocumentEmployments(int personId, Dictionary<string, Dictionary<string, NomValue>> noms, Func<int?, JObject> getOrgByApexId)
        {
            var parts = this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.EMPLOYEE WHERE {0}",
                new DbClause("PERSON_ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "EMPLOYEE",

                        __BOOK_PAGE_NO = (int?)r.Field<decimal?>("BOOK_PAGE_NO"),
                        __PAGES_COUNT = (int?)r.Field<decimal?>("PAGES_COUNT"),

                        hiredate = r.Field<DateTime?>("HIREDATE"),
                        valid = noms["boolean"].ByCode(r.Field<string>("VALID_YN") == "Y" ? "Y" : "N"),
                        organization = getOrgByApexId((int?)r.Field<decimal?>("FIRM_ID")),
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
                    WHERE {0}",
                new DbClause("E.PERSON_ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("DOC_ID"),
                        __migrTable = "DOCLIB_DOCUMENTS",

                        __EMPLOYEE_ID = r.Field<int>("ID"),

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
                                    new JProperty("caseType", Utils.ToJObject(noms["personCaseTypes"].ByAlias("person"))),
                                    new JProperty("bookPageNumber", bookPageNumber),
                                    new JProperty("pageCount", pageCount),
                                    new JProperty("applications", new JArray()))))))
                    .ToList();
        }

        private IList<JObject> getPersonDocumentMedicals(int personId, IDictionary<int, JObject> nomApplications, Dictionary<string, Dictionary<string, NomValue>> noms, Dictionary<int, string> appApexIdToStaffTypeCode)
        {
            var medLimitations = this.oracleConn
                .CreateStoreCommand(
                    @"SELECT * FROM 
                        CAA_DOC.MED_CERT_LIMITATION 
                        WHERE MED_CERT_ID in (SELECT ID FROM CAA_DOC.MED_CERT WHERE {0})",
                    new DbClause("PERSON_ID = {0}", personId))
                .Materialize(r =>
                    new
                    {
                        medId = r.Field<int>("MED_CERT_ID"),
                        limitation = noms["medLimitation"].ByOldId(r.Field<decimal?>("MED_LIMIT_ID").ToString()),
                    })
                .GroupBy(l => l.medId)
                .ToDictionary(g => g.Key, g => g.Select(l => l.limitation).ToArray());

            var parts = this.oracleConn
                .CreateStoreCommand(
                    @"SELECT * FROM CAA_DOC.MED_CERT WHERE {0}",
                    new DbClause("PERSON_ID = {0}", personId))
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "MED_CERT",

                        __BOOK_PAGE_NO = (int?)r.Field<decimal?>("BOOK_PAGE_NO"),
                        __PAGES_COUNT = (int?)r.Field<decimal?>("PAGES_COUNT"),

                        documentNumberPrefix = r.Field<string>("NO1"),
                        documentNumber = r.Field<string>("NO2"),
                        documentNumberSuffix = r.Field<string>("NO3"),
                        documentDateValidFrom = r.Field<DateTime?>("DATE_FROM"),
                        documentDateValidTo = r.Field<DateTime?>("DATE_TO"),
                        medClass = noms["medClasses"].ByOldId(r.Field<decimal?>("MED_CLASS_ID").ToString()),
                        documentPublisher = noms["medDocPublishers"].ByName(r.Field<string>("PUBLISHER_NAME")),
                        limitations = medLimitations.ContainsKey(r.Field<int>("ID")) ? medLimitations[r.Field<int>("ID")] : null,
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
                    WHERE {0}",
                new DbClause("MC.PERSON_ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("DOC_ID"),
                        __migrTable = "DOCLIB_DOCUMENTS",

                        __MED_CERT_ID = r.Field<int>("ID"),

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
                    WHERE {0}",
                new DbClause("MC.PERSON_ID = {0}", personId)
                )
                .Materialize(r =>
                    new
                    {
                        MED_CERT_ID = r.Field<int>("MED_CERT_ID"),
                        REQUEST_ID = r.Field<int>("REQUEST_ID")
                    })
                .ToList();

            return (from part in parts
                    join file in files on part.Get<int>("__oldId") equals file.Get<int>("__MED_CERT_ID") into fg
                    join app in apps on part.Get<int>("__oldId") equals app.MED_CERT_ID into ag
                    let file = fg.Any() ? fg.Single() : null //throw if more than one files present
                    let bookPageNumber = part.Get<int?>("__BOOK_PAGE_NO").ToString()
                    let pageCount = part.Get<int?>("__PAGES_COUNT")
                    let appLicenceStaffTypeCode = ag.Select(a => appApexIdToStaffTypeCode[a.REQUEST_ID]).Where(c => c != null).FirstOrDefault()
                    select new JObject(
                        new JProperty("part",
                            Utils.Pluck(part,
                                new string[] 
                                {
                                    "__oldId",
                                    "__migrTable",

                                    "documentNumberPrefix",
                                    "documentNumber",
                                    "documentNumberSuffix",
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
                                    new JProperty("caseType",
                                        (appLicenceStaffTypeCode != null ? Utils.ToJObject(getPersonCaseTypeByStaffTypeCode(noms, appLicenceStaffTypeCode)) : null)
                                        ?? Utils.ToJObject(noms["personCaseTypes"].ByAlias("person"))),
                                    new JProperty("bookPageNumber", bookPageNumber),
                                    new JProperty("pageCount", pageCount),
                                    new JProperty("applications", new JArray(ag.Select(a => nomApplications[a.REQUEST_ID]))))))))
                    .ToList();
        }

        private IList<JObject> getPersonFlyingExperiences(
            int personId,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Func<int?, JObject> getOrgByApexId,
            Func<int?, JObject> getAircraftByApexId)
        {
            return this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.FLYING_EXPERIENCE WHERE {0}",
                new DbClause("PERSON_ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "FLYING_EXPERIENCE",

                        staffType = noms["staffTypes"].ByOldId(r.Field<decimal?>("STAFF_TYPE_ID").ToString()),
                        documentDate = r.Field<DateTime?>("DOCUMENT_DATE"),
                        period = new { month = r.Field<string>("PERIOD_MONTH"), year = r.Field<string>("PERIOD_YEAR") },
                        organization = getOrgByApexId(r.Field<int?>("FIRM_ID")),
                        aircraft = getAircraftByApexId(r.Field<int?>("AC_ID")),
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
                        total = Utils.TimeToMilliseconds(r.Field<int?>("AMOUNT"), r.Field<short?>("AMOUNT_M")),
                        totalDoc = Utils.TimeToMilliseconds(r.Field<int?>("AMOUNT_SUM"), r.Field<short?>("AMOUNT_M_SUM")),
                        totalLastMonths = Utils.TimeToMilliseconds(r.Field<int?>("AMOUNT_12"), r.Field<short?>("AMOUNT_M_12")),
                        notes = r.Field<string>("NOTES")
                    }))
                .ToList();
        }

        private IList<JObject> getPersonStatuses(int personId, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            return this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.PERSON_STATE WHERE {0}",
                new DbClause("PERSON_ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "PERSON_STATE",

                        personStatusType = noms["personStatusTypes"].ByCode(r.Field<string>("REASON_CODE").ToString()),
                        documentNumber = r.Field<string>("DOCUMENT_NUM"),
                        documentDateValidFrom = r.Field<DateTime?>("BEGIN_DATE"),
                        documentDateValidTo = r.Field<DateTime?>("END_DATE"),
                        notes = r.Field<string>("REMARKS")
                    }))
                .ToList();
        }

        private NomValue[] transformLimitations66(string limitations66, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            Dictionary<string, string> preSplitLimFixups = new Dictionary<string, string>
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

            Dictionary<string, string> postSplitLimFixups = new Dictionary<string, string>
            {
                { "3.", "3" }
            };

            if (limitations66 == null)
            {
                return null;
            }

            Func<string, string> preSplitLimFixup = limitations =>
            {
                foreach (var f in preSplitLimFixups)
                {
                    limitations = limitations.Replace(f.Key, f.Value);
                }

                return limitations;
            };

            Func<string, string> postSplitLimFixup = lim =>
            {
                if (postSplitLimFixups.ContainsKey(lim))
                {
                    return postSplitLimFixups[lim];
                }
                else
                {
                    return lim;
                }
            };

            return
                preSplitLimFixup(limitations66)
                .Split(',')
                .Select(sc => 
                    noms["limitations66"]
                    .ByCode(postSplitLimFixup(sc.Trim())))
                .ToArray();
        }

        private IList<JObject> getPersonRatings(int personId, Func<int?, JObject> getPersonByApexId, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            Func<string, NomValue[]> transformSubclasses = (s) =>
            {
                if (s == null)
                {
                    return null;
                }

                return s.Split(',').Select(sc => noms["ratingSubClasses"].ByCode(sc.Trim())).ToArray();
            };

            var editions = oracleConn.CreateStoreCommand(
                @"SELECT E.PERSON_ID EXAMINER_ID,
                        RD.ID,
                        RD.RATING_CAA_ID,
                        RD.SUBCLASSES,
                        RD.LIMITATIONS,
                        RD.ISSUE_DATE,
                        RD.VALID_DATE,
                        RD.NOTES,
                        RD.NOTES_TRANS
                    FROM CAA_DOC.RATING_CAA R
                    JOIN CAA_DOC.RATING_CAA_DATES RD ON RD.RATING_CAA_ID = R.ID
                    LEFT OUTER JOIN CAA_DOC.EXAMINER E ON E.ID = RD.EXAMINER_ID
                    WHERE {0}",
                new DbClause("R.PERSON_ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "RATING_CAA_DATES",
                        __RATING_CAA_ID = r.Field<int>("RATING_CAA_ID"),

                        ratingSubClasses = transformSubclasses(r.Field<string>("SUBCLASSES")),
                        limitations = transformLimitations66(r.Field<string>("LIMITATIONS"), noms),
                        documentDateValidFrom = r.Field<DateTime?>("ISSUE_DATE"),
                        documentDateValidTo = r.Field<DateTime?>("VALID_DATE"),
                        notes = r.Field<string>("NOTES"),
                        notesAlt = r.Field<string>("NOTES_TRANS"),
                        inspector = getPersonByApexId((int?)r.Field<decimal?>("EXAMINER_ID"))
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
                @"SELECT * FROM CAA_DOC.RATING_CAA WHERE {0}",
                new DbClause("PERSON_ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
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
                        aircraftTypeCategory = noms["aircraftClases66"].ByOldId(r.Field<long?>("RATING_CAT66_ID").ToString()),
                        caa = noms["caa"].ByOldId(r.Field<decimal?>("CAA_ID").ToString()),
                        editions = editions[r.Field<int>("ID")]
                    }))
                .ToList();;
        }

        private IList<JObject> getPersonLicences(
            int personId,
            Func<int?, JObject> getPersonByApexId,
            IDictionary<int, JObject> nomApplications,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, int> ratings,
            Dictionary<int, int> medicals,
            Dictionary<int, int> trainings,
            Dictionary<int, int> exams,
            Dictionary<int, int> checks)
        {
            var includedRatings = oracleConn.CreateStoreCommand(
                @"SELECT LL.ID LICENCE_LOG_ID,
                        R.ID RATING_ID
                    FROM CAA_DOC.LICENCE L
                    JOIN CAA_DOC.LICENCE_LOG LL ON LL.LICENCE_ID = L.ID
                    LEFT OUTER JOIN CAA_DOC.LICENCE_RATING_INCL LRI ON LRI.LICENCE_LOG_ID = LL.ID
                    LEFT OUTER JOIN CAA_DOC.RATING_CAA_DATES RD ON RD.ID = LRI.RATING_DATES_ID
                    LEFT OUTER JOIN CAA_DOC.RATING_CAA R ON R.ID = RD.RATING_CAA_ID
                    WHERE {0}",
                new DbClause("L.PERSON_ID = {0}", personId)
                )
                .Materialize(r => new
                {
                    LICENCE_LOG_ID = r.Field<int>("LICENCE_LOG_ID"),
                    RATING_ID = r.Field<int?>("RATING_ID")
                })
                .GroupBy(r => r.LICENCE_LOG_ID)
                .ToDictionary(g => g.Key, g => g.Where(r => r.RATING_ID != null).Select(r => ratings[r.RATING_ID.Value]).ToArray());

            var includedMedicals = oracleConn.CreateStoreCommand(
                @"SELECT LL.ID LICENCE_LOG_ID,
                        LMCI.MED_CERT_ID
                    FROM CAA_DOC.LICENCE L
                    JOIN CAA_DOC.LICENCE_LOG LL ON LL.LICENCE_ID = L.ID
                    LEFT OUTER JOIN CAA_DOC.LICENCE_MED_CERT_INCL LMCI ON LMCI.LICENCE_LOG_ID = LL.ID
                    WHERE {0}",
                new DbClause("L.PERSON_ID = {0}", personId)
                )
                .Materialize(r => new
                {
                    LICENCE_LOG_ID = r.Field<int>("LICENCE_LOG_ID"),
                    MED_CERT_ID = r.Field<int?>("MED_CERT_ID")
                })
                .GroupBy(r => r.LICENCE_LOG_ID)
                .ToDictionary(g => g.Key, g => g.Where(r => r.MED_CERT_ID != null).Select(r => medicals[r.MED_CERT_ID.Value]).ToArray());

            var includedDocuments = oracleConn.CreateStoreCommand(
                @"SELECT LL.ID LICENCE_LOG_ID,
                        LDI.DOC_ID PERSON_DOCUMENT_ID
                    FROM CAA_DOC.LICENCE L
                    JOIN CAA_DOC.LICENCE_LOG LL ON LL.LICENCE_ID = L.ID
                    LEFT OUTER JOIN CAA_DOC.LICENCE_DOC_INCL LDI ON LDI.LICENCE_LOG_ID = LL.ID
                    WHERE {0}",
                new DbClause("L.PERSON_ID = {0}", personId)
                )
                .Materialize(r => new
                {
                    LICENCE_LOG_ID = r.Field<int>("LICENCE_LOG_ID"),
                    PERSON_DOCUMENT_ID = r.Field<int?>("PERSON_DOCUMENT_ID")
                })
                .Select(r => new
                {
                    LICENCE_LOG_ID = r.LICENCE_LOG_ID,
                    PERSON_DOCUMENT_ID = r.PERSON_DOCUMENT_ID,
                    trainingPartIndex = (r.PERSON_DOCUMENT_ID != null && trainings.ContainsKey(r.PERSON_DOCUMENT_ID.Value)) ? trainings[r.PERSON_DOCUMENT_ID.Value] : (int?)null,
                    checkPartIndex = (r.PERSON_DOCUMENT_ID != null && checks.ContainsKey(r.PERSON_DOCUMENT_ID.Value)) ? checks[r.PERSON_DOCUMENT_ID.Value] : (int?)null
                });

            foreach (var doc in includedDocuments.Where(d => d.PERSON_DOCUMENT_ID != null && d.trainingPartIndex == null && d.checkPartIndex == null))
            {
                Console.WriteLine("PERSON_DOCUMENT_ID {0} included in LICENCE_LOG_ID {1} is not a training, exam or check for PERSON_ID {2}", doc.PERSON_DOCUMENT_ID, doc.LICENCE_LOG_ID, personId);
            }

            var includedTrainings = includedDocuments
                .GroupBy(r => r.LICENCE_LOG_ID)
                .ToDictionary(g => g.Key, g => g.Where(r => r.trainingPartIndex != null).Select(r => r.trainingPartIndex.Value).ToArray());

            var includedChecks = includedDocuments
                .GroupBy(r => r.LICENCE_LOG_ID)
                .ToDictionary(g => g.Key, g => g.Where(r => r.checkPartIndex != null).Select(r => r.checkPartIndex.Value).ToArray());

            var includedLicences = oracleConn.CreateStoreCommand(
                @"SELECT LL.ID LICENCE_LOG_ID,
                        LLI.LICENCE_ID
                    FROM CAA_DOC.LICENCE L
                    JOIN CAA_DOC.LICENCE_LOG LL ON LL.LICENCE_ID = L.ID
                    LEFT OUTER JOIN CAA_DOC.LICENCE_LICENCE_INCL LLI ON LLI.LICENCE_LOG_ID = LL.ID
                    WHERE {0}",
                new DbClause("L.PERSON_ID = {0}", personId)
                )
                .Materialize(r => new
                {
                    LICENCE_LOG_ID = r.Field<int>("LICENCE_LOG_ID"),
                    LICENCE_ID = r.Field<int?>("LICENCE_ID")
                })
                .GroupBy(r => r.LICENCE_LOG_ID)
                .ToDictionary(g => g.Key, g => g.Where(r => r.LICENCE_ID != null).Select(r => r.LICENCE_ID.Value).ToArray());

            var editions = oracleConn.CreateStoreCommand(
                @"SELECT E.PERSON_ID EXAMINER_ID,
                        LL.LICENCE_ID,
                        LL.ID,
                        LL.ISSUE_DATE,
                        LL.VALID_DATE,
                        LL.NOTES,
                        LL.NOTES_TRANS,
                        LL.LICENCE_ACTION_ID,
                        LL.PAPER_NO,
                        LL.BOOK_PAGE_NO,
                        LL.PAGES_COUNT,
                        LL.LIMITATIONS_OLD,
                        LL.LIM_OTHER,
                        LL.LIM_MED_CERT,
                        LL.LIM_AT_A,
                        LL.LIM_AT_B1,
                        LL.LIM_AP_A,
                        LL.LIM_AP_B1,
                        LL.LIM_HT_A,
                        LL.LIM_HT_B1,
                        LL.LIM_HP_A,
                        LL.LIM_HP_B1,
                        LL.LIM_AVIONICS,
                        LL.LIM_PE_B3,
                        LL.REQUEST_ID
                    FROM CAA_DOC.LICENCE L
                    JOIN CAA_DOC.LICENCE_LOG LL ON LL.LICENCE_ID = L.ID
                    LEFT OUTER JOIN CAA_DOC.EXAMINER E ON E.ID = LL.EXAMINER_ID
                    WHERE {0}",
                new DbClause("L.PERSON_ID = {0}", personId)
                )
                .Materialize(r =>
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "LICENCE_LOG",

                        __LICENCE_ID = r.Field<int>("LICENCE_ID"),

                        //TODO show somewhere?
                        __BOOK_PAGE_NO = r.Field<int?>("BOOK_PAGE_NO"),
                        __PAGES_COUNT = r.Field<int?>("PAGES_COUNT"),
                        __LIM_OTHER = r.Field<string>("LIM_OTHER"),
                        __LIM_MED_CERT = r.Field<string>("LIM_MED_CERT"),

                        inspector = getPersonByApexId(r.Field<int?>("EXAMINER_ID")),
                        documentDateValidFrom = r.Field<DateTime?>("ISSUE_DATE"),
                        documentDateValidTo = r.Field<DateTime?>("VALID_DATE"),
                        notes = r.Field<string>("NOTES"),
                        notesAlt = r.Field<string>("NOTES_TRANS"),
                        licenceAction = noms["licenceActions"].ByOldId(r.Field<string>("LICENCE_ACTION_ID")),
                        stampNumber = r.Field<string>("PAPER_NO"),
                        limitations = transformLimitations66(r.Field<string>("LIMITATIONS_OLD"), noms),

                        applications = r.Field<int?>("REQUEST_ID") != null ? new JObject[] { nomApplications[r.Field<int?>("REQUEST_ID").Value] } : new JObject[0],
                        includedRatings = includedRatings[r.Field<int>("ID")],
                        includedMedicals = includedMedicals[r.Field<int>("ID")],
                        includedTrainings = includedTrainings[r.Field<int>("ID")],
                        includedLicences = includedLicences[r.Field<int>("ID")],
                        includedChecks = includedChecks[r.Field<int>("ID")],

                        amlLimitations = new
                        {
                            at_a_Ids = transformLimitations66(r.Field<string>("LIM_AT_A"), noms),
                            at_b1_Ids = transformLimitations66(r.Field<string>("LIM_AT_B1"), noms),
                            ap_a_Ids = transformLimitations66(r.Field<string>("LIM_AP_A"), noms),
                            ap_b1_Ids = transformLimitations66(r.Field<string>("LIM_AP_B1"), noms),
                            ht_a_Ids = transformLimitations66(r.Field<string>("LIM_HT_A"), noms),
                            ht_b1_Ids = transformLimitations66(r.Field<string>("LIM_HT_B1"), noms),
                            hp_a_Ids = transformLimitations66(r.Field<string>("LIM_HP_A"), noms),
                            hp_b1_Ids = transformLimitations66(r.Field<string>("LIM_HP_B1"), noms),
                            avionics_Ids = transformLimitations66(r.Field<string>("LIM_AVIONICS"), noms),
                            pe_b3_Ids = transformLimitations66(r.Field<string>("LIM_PE_B3"), noms)
                        }
                    })
                .ToList()
                .GroupBy(r => r.__LICENCE_ID)
                .ToDictionary(g => g.Key,
                    g => g.Select(r => Utils.ToJObject(
                        new
                        {
                            r.__oldId,
                            r.__migrTable,

                            r.__BOOK_PAGE_NO,
                            r.__PAGES_COUNT,
                            r.__LIM_OTHER,
                            r.__LIM_MED_CERT,

                            r.inspector,
                            r.documentDateValidFrom,
                            r.documentDateValidTo,
                            r.notes,
                            r.notesAlt,
                            r.licenceAction,
                            r.stampNumber,
                            r.limitations,

                            r.applications,
                            r.includedRatings,
                            r.includedMedicals,
                            r.includedTrainings,
                            r.includedLicences,
                            r.includedChecks,

                            r.amlLimitations
                        })));

            var statuses = oracleConn.CreateStoreCommand(
                @"SELECT L.ID LICENCE_ID,
                        E.PERSON_ID EXAMINER_ID,
                        LCS.ID LICENCE_CHANGE_STAT_ID,
                        LCS.CHANGE_DATE,
                        LCS.CHANGE_REASON_ID,
                        LCS.CHANGE_TO_VALID_YN,
                        LCS.NOTES,
                        LCS.INS_USER,
                        LCS.INS_DATE,
                        LCS.UPD_USER,
                        LCS.UPD_DATE
                    FROM CAA_DOC.LICENCE L
                    JOIN CAA_DOC.LICENCE_CHANGE_STAT LCS ON LCS.LICENCE_ID = L.ID
                    LEFT OUTER JOIN CAA_DOC.EXAMINER E ON E.ID = LCS.EXAMINER_ID
                    WHERE {0}",
                new DbClause("L.PERSON_ID = {0}", personId)
                )
                .Materialize(r =>
                    new
                    {
                        __oldId = r.Field<decimal>("LICENCE_CHANGE_STAT_ID"),
                        __migrTable = "LICENCE_CHANGE_STAT",

                        LICENCE_ID = r.Field<decimal>("LICENCE_ID"),
                        LICENCE_CHANGE_STAT_ID = r.Field<decimal>("LICENCE_CHANGE_STAT_ID"),

                        changeDate = r.Field<DateTime>("CHANGE_DATE"),
                        changeReason = noms["licenceChangeReasons"].ByOldId(r.Field<decimal>("CHANGE_REASON_ID").ToString()),
                        valid = noms["boolean"].ByCode(r.Field<string>("CHANGE_TO_VALID_YN")),
                        notes = r.Field<string>("NOTES"),
                        inspector = getPersonByApexId((int?)r.Field<decimal?>("EXAMINER_ID"))
                    })
                .GroupBy(r => r.LICENCE_ID)
                .ToDictionary(g => g.Key, g => 
                    g.Select(r => Utils.ToJObject(
                        new
                        {
                            r.__oldId,
                            r.__migrTable,
                            
                            r.changeDate,
                            r.changeReason,
                            r.valid,
                            r.notes,
                            r.inspector
                        })).ToArray());

            return this.oracleConn.CreateStoreCommand(
                @"SELECT L.ID,
                        L.LICENCE_TYPE_ID,
                        L.LICENCE_NO,
                        L.FOREIGN_LICENCE_NO,
                        L.VALID_YN,
                        L.PUBLISHER_CAA_ID,
                        L.FOREIGN_CAA_ID,
                        L.EMPLOYEE_ID,
                        L.ISSUE_DATE,
                        LT.STAFF_TYPE_ID,
                        LT.CODE as LICENCE_TYPE_CODE
                    FROM CAA_DOC.LICENCE L
                    INNER JOIN CAA_DOC.NM_LICENCE_TYPE LT ON L.LICENCE_TYPE_ID = LT.ID
                    WHERE {0}",
                new DbClause("L.PERSON_ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "LICENCE",

                        //TODO show somewhere?
                        __PUBLISHER_CAA_ID = r.Field<int?>("PUBLISHER_CAA_ID"),
                        __FOREIGN_CAA_ID = r.Field<int?>("FOREIGN_CAA_ID"),
                        __EMPLOYEE_ID = r.Field<int?>("EMPLOYEE_ID"),
                        __ISSUE_DATE = r.Field<DateTime?>("ISSUE_DATE"),

                        licenceType = noms["licenceTypes"].ByOldId(r.Field<string>("LICENCE_TYPE_ID")),
                        staffType = noms["staffTypes"].ByOldId(r.Field<string>("STAFF_TYPE_ID")),
                        fcl = noms["boolean"].ByCode(r.Field<string>("LICENCE_TYPE_CODE").Contains("FCL") ? "Y" : "N"),
                        licenceNumber = r.Field<string>("LICENCE_NO"),
                        foreignLicenceNumber = r.Field<string>("FOREIGN_LICENCE_NO"),
                        valid = noms["boolean"].ByCode(r.Field<string>("VALID_YN") == "Y" ? "Y" : "N"),
                        editions = editions[r.Field<int>("ID")],
                        statuses = statuses.ContainsKey(r.Field<int>("ID")) ? statuses[r.Field<int>("ID")] : null
                    }))
                .ToList();
        }

        private JObject getInspectorData(int personId, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            return this.oracleConn.CreateStoreCommand(
                @"SELECT E.ID,
                        E.EXAMINER_CODE,
                        E.VALID_YN,
                        E.CAA_ID,
                        E.STAMP_NUM
                    FROM CAA_DOC.EXAMINER E
                    JOIN CAA_DOC.PERSON P ON P.ID = E.PERSON_ID
                    WHERE E.CAA_ID IS NOT NULL {0}",
                new DbClause("AND P.ID = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "EXAMINER",

                        examinerCode = r.Field<string>("EXAMINER_CODE"),
                        caa = noms["caa"].ByOldId(r.Field<int>("CAA_ID").ToString()),
                        stampNum = r.Field<string>("STAMP_NUM"),
                        valid = noms["boolean"].ByCode(r.Field<string>("VALID_YN") == "Y" ? "Y" : "N")
                    }))
                .SingleOrDefault();
        }
    }
}
