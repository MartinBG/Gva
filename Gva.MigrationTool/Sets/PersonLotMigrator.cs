using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Autofac.Features.OwnedInstances;
using Common.Api.Models;
using Common.Api.UserContext;
using Common.Data;
using Common.Json;
using Common.Tests;
using Gva.Api.CommonUtils;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.MigrationTool.Nomenclatures;
using Gva.MigrationTool.Sets.Common;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.MigrationTool.Sets
{
    public class PersonLotMigrator : IDisposable
    {
        private bool disposed = false;
        private Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, IFileRepository, IApplicationRepository, ILotEventDispatcher, UserContext>>> dependencyFactory;
        private OracleConnection oracleConn;

        public PersonLotMigrator(
            OracleConnection oracleConn,
            Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, IFileRepository, IApplicationRepository, ILotEventDispatcher, UserContext>>> dependencyFactory)
        {
            this.dependencyFactory = dependencyFactory;
            this.oracleConn = oracleConn;
        }

        public void StartMigrating(
            //input constants
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, int> personIdToLotId,
            Func<int?, JObject> getAircraftByApexId,
            Func<int?, JObject> getPersonByApexId,
            Func<int?, JObject> getOrgByApexId,
            Dictionary<int, string> blobIdsToFileKeys,
            //intput
            ConcurrentQueue<int> personIds,
            //cancellation
            CancellationTokenSource cts,
            CancellationToken ct)
        {
            try
            {
                this.oracleConn.Open();
            }
            catch (Exception)
            {
                cts.Cancel();
                throw;
            }

            int personId;
            while (personIds.TryDequeue(out personId))
            {
                ct.ThrowIfCancellationRequested();

                try
                {
                    using (var dependencies = this.dependencyFactory())
                    {
                        var unitOfWork = dependencies.Value.Item1;
                        var lotRepository = dependencies.Value.Item2;
                        var fileRepository = dependencies.Value.Item3;
                        var applicationRepository = dependencies.Value.Item4;
                        var lotEventDispatcher = dependencies.Value.Item5;
                        var context = dependencies.Value.Item6;

                        var lot = lotRepository.GetLotIndex(personIdToLotId[personId], fullAccess: true);

                        Func<string, JObject, PartVersion<JObject>> addPartWithFiles = (path, content) =>
                        {
                            var pv = lot.CreatePart(path, content.Get<JObject>("part"), context);
                            fileRepository.AddFileReferences(pv.Part, content.GetItems<CaseDO>("files"));
                            return pv;
                        };

                        Dictionary<int, string> appApexIdToStaffTypeCode = getPersonApplicationsStaffTypeCodes(personId);

                        Dictionary<int, Tuple<GvaApplication, ApplicationNomDO>> applications =
                            new Dictionary<int, Tuple<GvaApplication, ApplicationNomDO>>();

                        var personDocumentApplications = this.getPersonDocumentApplications(personId, noms, appApexIdToStaffTypeCode, blobIdsToFileKeys);
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
                                            ApplicationName = docApplication.Get<string>("part.applicationType.name"),
                                            ApplicationCode = docApplication.Get<string>("part.applicationType.code"),
                                            OldDocumentNumber = docApplication.Get<string>("part.oldDocumentNumber")
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

                            IList<GvaApplicationStage> appStages = CommonUtils.GetApplicationStages(this.oracleConn, personIdToLotId, appNomDO.ApplicationId, app.Key);

                            foreach (GvaApplicationStage stage in appStages)
                            {
                                unitOfWork.DbContext.Set<GvaApplicationStage>().Add(stage);
                            }
                        }

                        var personDocuments = this.getPersonDocuments(personId, nomApplications, noms, appApexIdToStaffTypeCode, blobIdsToFileKeys);
                        Dictionary<int, int> trainingOldIdToPartIndex = new Dictionary<int, int>();
                        Dictionary<int, int> langCertOldIdToPartIndex = new Dictionary<int, int>();
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
                            }
                            else if (!(new string[] { "3", "4", "5" }).Contains(personDocument.Get<string>("part.__DOCUMENT_TYPE_CODE")) &&
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
                                "ratingTypes",
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
                                personDocument.Get<string>("part.__DOCUMENT_ROLE_CATEGORY_CODE") == "O" &&
                                (personDocument.Get<string>("part.__DOCUMENT_ROLE_CODE") == "ENG" ||
                                personDocument.Get<string>("part.__DOCUMENT_ROLE_CODE") == "BG"))
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
                                "ratingTypes",
                                "aircraftTypeGroup",
                                "ratingClass",
                                "authorization",
                                "licenceType",
                                "locationIndicator",
                                "sector",
                                "langLevel",
                                "langLevelEntries",
                                "documentType",
                                "documentRole",
                                "valid",
                                "notes",
                            });

                                var pv = addPartWithFiles("personDocumentLangCertificates/*", personDocument);
                                langCertOldIdToPartIndex.Add(personDocument.Get<int>("part.__oldId"), pv.Part.Index);
                            }
                            else if (!(new string[] { "3", "4", "5" }).Contains(personDocument.Get<string>("part.__DOCUMENT_TYPE_CODE")) &&
                                personDocument.Get<string>("part.__DOCUMENT_ROLE_CATEGORY_CODE") == "O" &&
                                personDocument.Get<string>("part.__DOCUMENT_ROLE_CODE") == "6")
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
                                    "documentType",
                                    "documentRole",
                                    "valid",
                                    "notes",
                                });

                                var pv = addPartWithFiles("personDocumentTrainings/*", personDocument);
                                examOldIdToPartIndex.Add(personDocument.Get<int>("part.__oldId"), pv.Part.Index);
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
                                    "ratingTypes",
                                    "aircraftTypeGroup",
                                    "ratingClass",
                                    "authorization",
                                    "licenceType",
                                    "locationIndicator",
                                    "sector",
                                    "documentType",
                                    "documentRole",
                                    "valid",
                                    "notes",
                                });

                                var pv = addPartWithFiles("personDocumentTrainings/*", personDocument);
                                trainingOldIdToPartIndex.Add(personDocument.Get<int>("part.__oldId"), pv.Part.Index);
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

                        var personAddresses = this.getPersonAddresses(personId, noms);
                        foreach (var address in personAddresses)
                        {
                            addPartWithFiles("personAddresses/*", address);
                        }

                        var personDocumentEducations = this.getPersonDocumentEducations(personId, nomApplications, noms, appApexIdToStaffTypeCode, blobIdsToFileKeys);
                        foreach (var docEducation in personDocumentEducations)
                        {
                            addPartWithFiles("personDocumentEducations/*", docEducation);
                        }

                        var personDocumentEmployments = this.getPersonDocumentEmployments(personId, noms, getOrgByApexId, blobIdsToFileKeys);
                        Dictionary<int, PartVersion<JObject>> employmentsByOldId = new Dictionary<int, PartVersion<JObject>>();
                        foreach (var docEmployment in personDocumentEmployments)
                        {
                            var pv = addPartWithFiles("personDocumentEmployments/*", docEmployment);
                            employmentsByOldId.Add(docEmployment.Get<int>("part.__oldId"), pv);
                        }

                        var personDocumentMedicals = this.getPersonDocumentMedicals(personId, nomApplications, noms, appApexIdToStaffTypeCode, blobIdsToFileKeys);
                        Dictionary<int, int> medicalOldIdToPartIndex = new Dictionary<int, int>();
                        foreach (var docMedical in personDocumentMedicals)
                        {
                            var pv = addPartWithFiles("personDocumentMedicals/*", docMedical);
                            medicalOldIdToPartIndex.Add(docMedical.Get<int>("part.__oldId"), pv.Part.Index);
                        }

                        var personFlyingExperiences = this.getPersonFlyingExperiences(personId, noms, getOrgByApexId, getAircraftByApexId);
                        foreach (var flyingExperience in personFlyingExperiences)
                        {
                            addPartWithFiles("personFlyingExperiences/*", flyingExperience);
                        }

                        var personStatuses = this.getPersonStatuses(personId, noms);
                        foreach (var personStatus in personStatuses)
                        {
                            addPartWithFiles("personStatuses/*", personStatus);
                        }

                        var personRatingEditions = this.getPersonRatingEditions(personId, getPersonByApexId, noms,  nomApplications);
                        var personRatings = this.getPersonRatings(personId, noms);
                        Dictionary<int, JObject> ratingEditionOldIdToPartIndex = new Dictionary<int, JObject>();
                        foreach (var personRating in personRatings)
                        {
                            var ratingPartVersion = addPartWithFiles("ratings/*", personRating);

                            int nextIndex = 0;

                            foreach (var edition in personRatingEditions[personRating.Get<int>("part.__oldId")])
                            {
                                var editionPart = edition["part"] as JObject;
                                editionPart.Add("ratingPartIndex", ratingPartVersion.Part.Index);
                                editionPart.Add("index", nextIndex);
                                nextIndex++;

                                var editionPartVersion = addPartWithFiles("ratingEditions/*", edition);

                                ratingEditionOldIdToPartIndex.Add(edition.Get<int>("part.__oldId"),
                                    new JObject() {
                                        new JProperty("ind", ratingPartVersion.Part.Index),
                                        new JProperty("index", editionPartVersion.Part.Index)
                                    });
                            }
                        }

                        var personLicenceEditions = this.getPersonLicenceEditions(personId, getPersonByApexId, nomApplications, noms, ratingEditionOldIdToPartIndex, medicalOldIdToPartIndex, trainingOldIdToPartIndex, langCertOldIdToPartIndex, examOldIdToPartIndex, checkOldIdToPartIndex);
                        var personLicences = this.getPersonLicences(personId, getPersonByApexId, noms, employmentsByOldId);
                        Dictionary<int, int> licenceOldIdToPartIndex = new Dictionary<int, int>();
                        foreach (var personLicence in personLicences)
                        {
                            var licencePartVersion = addPartWithFiles("licences/*", personLicence);

                            int nextIndex = 0;

                            foreach (var edition in personLicenceEditions[personLicence.Get<int>("part.__oldId")])
                            {
                                var editionPart = edition["part"] as JObject;
                                editionPart.Add("licencePartIndex", licencePartVersion.Part.Index);
                                editionPart.Add("index", nextIndex);
                                nextIndex++;

                                var editionPartVersion = addPartWithFiles("licenceEditions/*", edition);
                            }

                            licenceOldIdToPartIndex.Add(personLicence.Get<int>("part.__oldId"), licencePartVersion.Part.Index);
                        }

                        //replace included licence ids with part indexes
                        foreach (var personLicence in personLicences)
                        {
                            foreach (var edition in personLicenceEditions[personLicence.Get<int>("part.__oldId")])
                            {
                                var editionPart = edition["part"] as JObject;
                                editionPart.Property("includedLicences").Value = new JArray(edition.GetItems<int>("includedLicences").Select(l => licenceOldIdToPartIndex[l]).ToArray());
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
                catch (Exception)
                {
                    Console.WriteLine("Error in personId: {0}", personId);

                    cts.Cancel();
                    throw;
                }
            }
        }

        private IList<JObject> getPersonAddresses(int personId, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            var parts = this.oracleConn.CreateStoreCommand(
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

            var files = noms["personCaseTypes"].Values.Select(ct => new JObject(
                new JProperty("isAdded", true),
                new JProperty("file", null),
                new JProperty("caseType", Utils.ToJObject(ct)),
                new JProperty("bookPageNumber", null),
                new JProperty("pageCount", null),
                new JProperty("applications", new JArray())));

            return parts.Select(p => new JObject(
                new JProperty("part", p),
                new JProperty("files", files))).ToList();
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

        private IList<JObject> getPersonDocumentApplications(
            int personId,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, string> appApexIdToStaffTypeCode,
            Dictionary<int, string> blobIdsToFileKeys)
        {
            Dictionary<int, JObject[]> applicationExams = this.oracleConn.CreateStoreCommand(
                @"SELECT
                    et.test_name, 
                    er.test_code,
                    er.on_date,
                    er.id_request,
                    et.qlf_code,
                    et.qlf_name
                FROM CAA_DOC.EXAMS_REQUEST er
                LEFT JOIN (select distinct test_code, test_name, qlf_code, qlf_name from CAA_DOC.exams_test) et on er.test_code = et.test_code
                LEFT JOIN CAA_DOC.REQUEST r on r.id = er.id_request
                WHERE {0}",
                new DbClause("r.applicant_person_id = {0}", personId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldApplicationId = r.Field<int>("id_request"),
                        date = r.Field<DateTime?>("on_date"),
                        examData = new
                        {
                            Name = r.Field<string>("test_name"),
                            Code = r.Field<string>("test_code"),
                            QualificationName = r.Field<string>("qlf_code"),
                            QualificationCode = r.Field<string>("qlf_name")
                        },
                       
                    }))
                    .ToList()
                    .GroupBy(r => r.Get<int>("__oldApplicationId"))
                    .ToDictionary(r => r.Key, r => r.ToArray());

            Func<string, int> parseStringToInt = (stringValue) =>
                        {
                            int outValue;
                            int.TryParse(stringValue, out outValue);
                            return outValue;
                        };

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
                        R.TAX_AMOUNT,
                        R.ID_SCHOOL,
                        ECC.CERT_CAMP_CODE,
                        ECC.CERT_CAMP_NAME
                    FROM CAA_DOC.REQUEST R
                    LEFT JOIN (select cert_camp_code, cert_camp_name, qlf_code from CAA_DOC.EXAMS_CERT_CAMPAIGN) ecc on r.cert_camp_code = ecc.cert_camp_code
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
                        oldDocumentNumber = r.Field<string>("DOC_NO"),
                        documentDate = r.Field<DateTime?>("DOC_DATE"),
                        notes = r.Field<string>("NOTES"),
                        applicationType = noms["applicationTypes"].ByOldId(r.Field<decimal?>("REQUEST_TYPE_ID").ToString()),
                        applicationPaymentType = noms["applicationPaymentTypes"].ByOldId(r.Field<decimal?>("PAYMENT_REASON_ID").ToString()),
                        currency = noms["currencies"].ByOldId(r.Field<decimal?>("CURRENCY_ID").ToString()),
                        taxAmount = r.Field<decimal?>("TAX_AMOUNT"),
                        examinationSystemData = new {
                            exams = r.Field<string>("CERT_CAMP_CODE") != null && applicationExams.ContainsKey(r.Field<int>("ID")) ? applicationExams[r.Field<int>("ID")] : new JObject[0],
                            qualifications = new JObject[0],
                            certCampaign = r.Field<string>("CERT_CAMP_CODE") != null ?
                                new
                                {
                                    NomValueId = parseStringToInt(r.Field<string>("CERT_CAMP_CODE").Substring(0, 6)),
                                    Name = r.Field<string>("CERT_CAMP_NAME"),
                                    Code = r.Field<string>("CERT_CAMP_CODE")
                                } : null,
                            school = r.Field<decimal?>("ID_SCHOOL").HasValue ? noms["applicationPaymentTypes"].ByOldId(r.Field<decimal?>("ID_SCHOOL").ToString()) : null
                        }
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

                        key = blobIdsToFileKeys[r.Field<int>("DOC_ID")],
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
                                    "oldDocumentNumber",
                                    "documentNumber",
                                    "documentDate",
                                    "notes",
                                    "applicationType",
                                    "applicationPaymentType",
                                    "currency",
                                    "taxAmount", 
                                    "examinationSystemData"
                                })),
                        new JProperty("files",
                            new JArray(
                                new JObject(
                                    new JProperty("isAdded", true),
                                    new JProperty("file", Utils.Pluck(file, new string[] { "key", "name", "mimeType" })),
                                    new JProperty("caseType",
                                        (licenceStaffTypeCode != null ? Utils.ToJObject(PersonUtils.getPersonCaseTypeByStaffTypeCode(noms, licenceStaffTypeCode)) : null)
                                        ?? Utils.ToJObject(noms["personCaseTypes"].ByAlias("person"))),
                                    new JProperty("bookPageNumber", bookPageNumber),
                                    new JProperty("pageCount", pageCount),
                                    new JProperty("applications", new JArray()))))))
                    .ToList();
        }

        private IList<JObject> getPersonDocuments(
            int personId,
            IDictionary<int, JObject> nomApplications,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, string> appApexIdToStaffTypeCode,
            Dictionary<int, string> blobIdsToFileKeys)
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
                    LEFT OUTER JOIN CAA_DOC.NM_STAFF_TYPE ST ON ST.ID = PD.STAFF_TYPE_ID
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

                        documentNumber = !string.IsNullOrWhiteSpace(r.Field<string>("DOC_NO")) ? r.Field<string>("DOC_NO") : null,
                        documentPersonNumber = r.Field<int?>("PERSON_NUM"),
                        documentDateValidFrom = r.Field<DateTime?>("VALID_FROM"),
                        documentDateValidTo = r.Field<DateTime?>("VALID_TO"),
                        documentPublisher = r.Field<string>("DOC_PUBLISHER"),
                        ratingTypes = noms["ratingTypes"].ByOldId(r.Field<decimal?>("RATING_TYPE_ID").ToString()) != null ? 
                        new List<NomValue>() { noms["ratingTypes"].ByOldId(r.Field<decimal?>("RATING_TYPE_ID").ToString()) } :
                        new List<NomValue>(),
                        aircraftTypeGroup = noms["aircraftTypeGroups"].ByOldId(r.Field<long?>("ID_AC_GROUP").ToString()),
                        ratingClass = noms["ratingClasses"].ByOldId(r.Field<decimal?>("RATING_CLASS_ID").ToString()),
                        authorization = noms["authorizations"].ByOldId(r.Field<decimal?>("AUTHORIZATION_ID").ToString()),
                        licenceType = noms["licenceTypes"].ByOldId(r.Field<decimal?>("LICENCE_TYPE_ID").ToString()),
                        locationIndicator = noms["locationIndicators"].ByOldId(r.Field<long?>("ID_INDICATOR").ToString()),
                        sector = r.Field<string>("SECTOR"),
                        documentType = noms["documentTypes"].ByOldId(r.Field<decimal?>("DOCUMENT_TYPE_ID").ToString()),
                        documentRole = noms["documentRoles"].ByOldId(r.Field<decimal?>("DOCUMENT_ROLE_ID").ToString()),
                        valid = noms["boolean"].ByCode(r.Field<string>("VALID_YN") == "Y" ? "Y" : "N"),
                        notes = r.Field<string>("NOTES"),

                        personCheckRatingValue = noms["personCheckRatingValues"].ByCode(r.Field<string>("RATING_VALUE")),
                        langLevel = noms["langLevels"].ByOldId(r.Field<long?>("NM_EN_LANG_ID").ToString()),
                        langLevelEntries = new JArray()
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

                        key = blobIdsToFileKeys[r.Field<int>("DOC_ID")],
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
                                        (appLicenceStaffTypeCode != null ? Utils.ToJObject(PersonUtils.getPersonCaseTypeByStaffTypeCode(noms, appLicenceStaffTypeCode)) : null)
                                        ?? (licenceStaffTypeCode != null ? Utils.ToJObject(PersonUtils.getPersonCaseTypeByStaffTypeCode(noms, licenceStaffTypeCode)) : null)
                                        ?? Utils.ToJObject(noms["personCaseTypes"].ByAlias("person"))),
                                    new JProperty("bookPageNumber", bookPageNumber),
                                    new JProperty("pageCount", pageCount),
                                    new JProperty("applications", new JArray(ag.Select(a => nomApplications[a.REQUEST_ID]))))))))
                    .ToList();
        }

        private IList<JObject> getPersonDocumentEducations(
            int personId,
            IDictionary<int, JObject> nomApplications,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, string> appApexIdToStaffTypeCode,
            Dictionary<int, string> blobIdsToFileKeys)
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

                        key = blobIdsToFileKeys[r.Field<int>("DOC_ID")],
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
                                        (appLicenceStaffTypeCode != null ? Utils.ToJObject(PersonUtils.getPersonCaseTypeByStaffTypeCode(noms, appLicenceStaffTypeCode)) : null)
                                        ?? Utils.ToJObject(noms["personCaseTypes"].ByAlias("person"))),
                                    new JProperty("bookPageNumber", bookPageNumber),
                                    new JProperty("pageCount", pageCount),
                                    new JProperty("applications", new JArray(ag.Select(a => nomApplications[a.REQUEST_ID]))))))))
                    .ToList();
        }

        private IList<JObject> getPersonDocumentEmployments(
            int personId,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Func<int?, JObject> getOrgByApexId,
            Dictionary<int, string> blobIdsToFileKeys)
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

                        key = blobIdsToFileKeys[r.Field<int>("DOC_ID")],
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

        private IList<JObject> getPersonDocumentMedicals(
            int personId,
            IDictionary<int, JObject> nomApplications,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, string> appApexIdToStaffTypeCode,
            Dictionary<int, string> blobIdsToFileKeys)
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
                        limitations = medLimitations.ContainsKey(r.Field<int>("ID")) ? medLimitations[r.Field<int>("ID")] : new NomValue[0],
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

                        key = blobIdsToFileKeys[r.Field<int>("DOC_ID")],
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
                                        (appLicenceStaffTypeCode != null ? Utils.ToJObject(PersonUtils.getPersonCaseTypeByStaffTypeCode(noms, appLicenceStaffTypeCode)) : null)
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
                .Materialize(r => new JObject(
                    new JProperty("part",
                        Utils.ToJObject(new
                        {
                            __oldId = r.Field<int>("ID"),
                            __migrTable = "FLYING_EXPERIENCE",

                            documentDate = r.Field<DateTime?>("DOCUMENT_DATE"),
                            period = new { month = r.Field<string>("PERIOD_MONTH"), year = r.Field<string>("PERIOD_YEAR") },
                            organization = getOrgByApexId(r.Field<int?>("FIRM_ID")),
                            aircraft = getAircraftByApexId(r.Field<int?>("AC_ID")),
                            ratingTypes = noms["ratingTypes"].ByOldId(r.Field<decimal?>("RATING_TYPE_ID").ToString()) != null ? 
                            new List<NomValue>() { noms["ratingTypes"].ByOldId(r.Field<decimal?>("RATING_TYPE_ID").ToString()) } :
                            new List<NomValue>(),
                            ratingClass = noms["ratingClasses"].ByOldId(r.Field<decimal?>("RATING_CLASS_ID").ToString()),
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
                         })),
                         new JProperty("files",
                            new JArray(
                                new JObject(
                                    new JProperty("isAdded", true),
                                    new JProperty("file", null),
                                    new JProperty("caseType", r.Field<string>("STAFF_TYPE_ID") != null? Utils.ToJObject(PersonUtils.getPersonCaseTypeByStaffTypeCode(noms, (noms["staffTypes"].ByOldId(r.Field<string>("STAFF_TYPE_ID")).Code))): null),
                                    new JProperty("bookPageNumber", null),
                                    new JProperty("pageCount", null),
                                    new JProperty("applications", new JArray()))))))
                        .ToList();
        }

        private IList<JObject> getPersonStatuses(int personId, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            var parts = this.oracleConn.CreateStoreCommand(
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

            var files = noms["personCaseTypes"].Values.Select(ct => new JObject(
                new JProperty("isAdded", true),
                new JProperty("file", null),
                new JProperty("caseType", Utils.ToJObject(ct)),
                new JProperty("bookPageNumber", null),
                new JProperty("pageCount", null),
                new JProperty("applications", new JArray())));

            return parts.Select(p => new JObject(
                new JProperty("part", p),
                new JProperty("files", files))).ToList();
        }

        private IDictionary<int, IEnumerable<JObject>> getPersonRatingEditions(
            int personId,
            Func<int?, JObject> getPersonByApexId,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, JObject> nomApplications)
        {
            Func<string, NomValue[]> transformSubclasses = (s) =>
            {
                if (s == null)
                {
                    return null;
                }

                return s.Split(',').Select(sc => noms["ratingSubClasses"].ByCode(sc.Trim())).ToArray();
            };

            return oracleConn.CreateStoreCommand(
                @"SELECT E.PERSON_ID EXAMINER_ID,
                        RD.ID,
                        RD.RATING_CAA_ID,
                        RD.SUBCLASSES,
                        RD.LIMITATIONS,
                        RD.ISSUE_DATE,
                        RD.VALID_DATE,
                        RD.NOTES,
                        RD.NOTES_TRANS,
                        RD.REQUEST_ID,
                        ST.CODE as STAFF_TYPE_CODE
                    FROM CAA_DOC.RATING_CAA R
                    JOIN CAA_DOC.RATING_CAA_DATES RD ON RD.RATING_CAA_ID = R.ID
                    LEFT OUTER JOIN CAA_DOC.EXAMINER E ON E.ID = RD.EXAMINER_ID
                    INNER JOIN CAA_DOC.NM_STAFF_TYPE ST ON R.STAFF_TYPE_ID = ST.ID
                    WHERE {0}",
                new DbClause("R.PERSON_ID = {0}", personId)
                )
                .Materialize(r =>
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "RATING_CAA_DATES",

                        __RATING_CAA_ID = r.Field<int>("RATING_CAA_ID"),

                        caseType = Utils.ToJObject(PersonUtils.getPersonCaseTypeByStaffTypeCode(noms, (r.Field<string>("STAFF_TYPE_CODE")))),
                        ratingSubClasses = transformSubclasses(r.Field<string>("SUBCLASSES")) ?? new NomValue[0],
                        limitations = transformLimitations66(r.Field<string>("LIMITATIONS"), noms) ?? new NomValue[0],
                        documentDateValidFrom = r.Field<DateTime?>("ISSUE_DATE"),
                        documentDateValidTo = r.Field<DateTime?>("VALID_DATE"),
                        notes = r.Field<string>("NOTES"),
                        notesAlt = r.Field<string>("NOTES_TRANS"),
                        inspector = getPersonByApexId((int?)r.Field<decimal?>("EXAMINER_ID")),
                        applications = r.Field<decimal?>("REQUEST_ID") != null ? new JObject[] { nomApplications[(int)r.Field<decimal?>("REQUEST_ID").Value] } : new JObject[0],
                    })
                .ToList()
                .OrderBy(r => r.documentDateValidFrom)
                .GroupBy(r => r.__RATING_CAA_ID)
                .ToDictionary(g => g.Key,
                    g => g.Select(r => new JObject(
                        new JProperty("part",
                            Utils.ToJObject(
                                new
                                {
                                    r.__oldId,
                                    r.__migrTable,

                                    r.__RATING_CAA_ID,

                                    r.ratingSubClasses,
                                    r.limitations,
                                    r.documentDateValidFrom,
                                    r.documentDateValidTo,
                                    r.notes,
                                    r.notesAlt,
                                    r.inspector
                                })),
                            new JProperty("files",
                                new JArray(
                                  new JObject(
                                        new JProperty("isAdded", true),
                                        new JProperty("file", null),
                                        new JProperty("caseType", r.caseType),
                                        new JProperty("bookPageNumber", null),
                                        new JProperty("pageCount", null),
                                        new JProperty("applications", r.applications)))))));
        }

        private IList<JObject> getPersonRatings(int personId, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            return this.oracleConn.CreateStoreCommand(
                @"SELECT ST.CODE as STAFF_TYPE_CODE,
                        R.ID,
                        R.RATING_STEPEN,
                        R.RATING_CLASS_ID,
                        R.RATING_TYPE_ID,
                        R.AUTHORIZATION_ID,
                        R.RATING_MODEL,
                        R.INDICATOR_ID,
                        R.SECTOR,
                        R.RATING_GROUP66_ID,
                        R.RATING_CAT66_ID,
                        R.CAA_ID
                    FROM CAA_DOC.RATING_CAA R
                    INNER JOIN CAA_DOC.NM_STAFF_TYPE ST ON R.STAFF_TYPE_ID = ST.ID
                    WHERE {0}",
                new DbClause("PERSON_ID = {0}", personId)
                )
                .Materialize(r => new JObject(
                    new JProperty("part",
                        Utils.ToJObject(new
                        {
                            __oldId = r.Field<int>("ID"),
                            __migrTable = "RATING_CAA",

                            personRatingLevel = noms["personRatingLevels"].ByCode(r.Field<string>("RATING_STEPEN")),
                            ratingClass = noms["ratingClasses"].ByOldId(r.Field<decimal?>("RATING_CLASS_ID").ToString()),
                            ratingTypes = noms["ratingTypes"].ByOldId(r.Field<decimal?>("RATING_TYPE_ID").ToString()) != null ?
                            new List<NomValue>() { noms["ratingTypes"].ByOldId(r.Field<decimal?>("RATING_TYPE_ID").ToString()) } :
                            new List<NomValue>(),
                            authorization = noms["authorizations"].ByOldId(r.Field<decimal?>("AUTHORIZATION_ID").ToString()),
                            personRatingModel = noms["personRatingModels"].ByCode(r.Field<string>("RATING_MODEL")),
                            locationIndicator = noms["locationIndicators"].ByOldId(r.Field<long?>("INDICATOR_ID").ToString()),
                            sector = r.Field<string>("SECTOR"),
                            aircraftTypeGroup = noms["aircraftTypeGroups"].ByOldId(r.Field<long?>("RATING_GROUP66_ID").ToString()),
                            aircraftTypeCategory = noms["aircraftClases66"].ByOldId(r.Field<long?>("RATING_CAT66_ID").ToString()),
                            caa = noms["caa"].ByOldId(r.Field<decimal?>("CAA_ID").ToString())
                        })),
                         new JProperty("files",
                            new JArray(
                              new JObject(
                                    new JProperty("isAdded", true),
                                    new JProperty("file", null),
                                    new JProperty("caseType", Utils.ToJObject(PersonUtils.getPersonCaseTypeByStaffTypeCode(noms, (r.Field<string>("STAFF_TYPE_CODE"))))),
                                    new JProperty("bookPageNumber", null),
                                    new JProperty("pageCount", null),
                                    new JProperty("applications", new JArray()))))))
                .ToList();
        }

        private IDictionary<int, IEnumerable<JObject>> getPersonLicenceEditions(
            int personId,
            Func<int?, JObject> getPersonByApexId,
            IDictionary<int, JObject> nomApplications,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, JObject> ratingEditions,
            Dictionary<int, int> medicals,
            Dictionary<int, int> trainings,
            Dictionary<int, int> langCerts,
            Dictionary<int, int> exams,
            Dictionary<int, int> checks)
        {
            var includedRatings = oracleConn.CreateStoreCommand(
                @"SELECT LL.ID LICENCE_LOG_ID,
                        R.ID RATING_ID,
                        RD.ID EDITION_ID,
                        LRI.SORT_ORDER
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
                    RATING_ID = r.Field<int?>("RATING_ID"),
                    EDITION_ID = r.Field<int?>("EDITION_ID"),
                    SORT_ORDER = r.Field<int?>("SORT_ORDER")
                })
                .GroupBy(r => r.LICENCE_LOG_ID)
                .ToDictionary(g => g.Key, g => g.Where(r => r.EDITION_ID != null)
                    .Select(r =>
                        new
                        {
                            edition = ratingEditions[r.EDITION_ID.Value],
                            orderNum = r.SORT_ORDER
                        })
                        .OrderBy(r => r.orderNum)
                        .Select(r =>
                            new {
                                ind = r.edition.Get<int>("ind"),
                                index = r.edition.Get<int>("index")
                            }).ToArray());

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
                    examPartIndex = (r.PERSON_DOCUMENT_ID != null && exams.ContainsKey(r.PERSON_DOCUMENT_ID.Value)) ? exams[r.PERSON_DOCUMENT_ID.Value] : (int?)null,
                    trainingPartIndex = (r.PERSON_DOCUMENT_ID != null && trainings.ContainsKey(r.PERSON_DOCUMENT_ID.Value)) ? trainings[r.PERSON_DOCUMENT_ID.Value] : (int?)null,
                    langCertPartIndex = (r.PERSON_DOCUMENT_ID != null && langCerts.ContainsKey(r.PERSON_DOCUMENT_ID.Value)) ? langCerts[r.PERSON_DOCUMENT_ID.Value] : (int?)null,
                    checkPartIndex = (r.PERSON_DOCUMENT_ID != null && checks.ContainsKey(r.PERSON_DOCUMENT_ID.Value)) ? checks[r.PERSON_DOCUMENT_ID.Value] : (int?)null
                });

            foreach (var doc in includedDocuments.Where(d => d.PERSON_DOCUMENT_ID != null && d.trainingPartIndex == null && d.checkPartIndex == null && d.examPartIndex == null && d.langCertPartIndex == null))
            {
                Console.WriteLine("PERSON_DOCUMENT_ID {0} included in LICENCE_LOG_ID {1} is not a training, language certificate, exam or check for PERSON_ID {2}", doc.PERSON_DOCUMENT_ID, doc.LICENCE_LOG_ID, personId);
            }

            var includedTrainings = includedDocuments
                .GroupBy(r => r.LICENCE_LOG_ID)
                .ToDictionary(g => g.Key, g => g.Where(r => r.trainingPartIndex != null).Select(r => r.trainingPartIndex.Value).ToArray());

            var includedLangCerts = includedDocuments
                .GroupBy(r => r.LICENCE_LOG_ID)
                .ToDictionary(g => g.Key, g => g.Where(r => r.langCertPartIndex != null).Select(r => r.langCertPartIndex.Value).ToArray());

            var includedExams = includedDocuments
                .GroupBy(r => r.LICENCE_LOG_ID)
                .ToDictionary(g => g.Key, g => g.Where(r => r.examPartIndex != null).Select(r => r.examPartIndex.Value).ToArray());

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

            return oracleConn.CreateStoreCommand(
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
                        LL.REQUEST_ID,
                        LT.STAFF_TYPE_ID
                    FROM CAA_DOC.LICENCE L
                    JOIN CAA_DOC.LICENCE_LOG LL ON LL.LICENCE_ID = L.ID
                    LEFT OUTER JOIN CAA_DOC.EXAMINER E ON E.ID = LL.EXAMINER_ID
                    INNER JOIN CAA_DOC.NM_LICENCE_TYPE LT ON L.LICENCE_TYPE_ID = LT.ID
                    WHERE {0}",
                new DbClause("L.PERSON_ID = {0}", personId)
                )
                .Materialize(r =>
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "LICENCE_LOG",

                        __LICENCE_ID = r.Field<int>("LICENCE_ID"),

                        __LIM_MED_CERT = r.Field<string>("LIM_MED_CERT"),

                        staffType = noms["staffTypes"].ByOldId(r.Field<string>("STAFF_TYPE_ID")),
                        bookPageNumber = r.Field<int?>("BOOK_PAGE_NO"),
                        pageCount = r.Field<int?>("PAGES_COUNT"),

                        inspector = getPersonByApexId(r.Field<int?>("EXAMINER_ID")),
                        documentDateValidFrom = r.Field<DateTime?>("ISSUE_DATE"),
                        documentDateValidTo = r.Field<DateTime?>("VALID_DATE"),
                        notes = r.Field<string>("NOTES"),
                        notesAlt = r.Field<string>("NOTES_TRANS"),
                        licenceAction = noms["licenceActions"].ByOldId(r.Field<string>("LICENCE_ACTION_ID")),
                        stampNumber = r.Field<string>("PAPER_NO"),
                        limitations = (r.Field<string>("LIMITATIONS_OLD") != null ? transformLimitations66(r.Field<string>("LIMITATIONS_OLD"), noms) : new NomValue[0])
                            .Union(r.Field<string>("LIM_OTHER") != null ? transformLimitations66(r.Field<string>("LIM_OTHER"), noms) : new NomValue[0]),
                        applications = r.Field<int?>("REQUEST_ID") != null ? new JObject[] { nomApplications[r.Field<int?>("REQUEST_ID").Value] } : new JObject[0],
                        includedRatings = includedRatings[r.Field<int>("ID")],
                        includedMedicals = includedMedicals[r.Field<int>("ID")],
                        includedTrainings = includedTrainings[r.Field<int>("ID")],
                        includedLangCerts = includedLangCerts[r.Field<int>("ID")],
                        includedLicences = includedLicences[r.Field<int>("ID")],
                        includedChecks = includedChecks[r.Field<int>("ID")],
                        includedExams = includedExams[r.Field<int>("ID")],

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
                .OrderBy(r => r.documentDateValidFrom)
                .GroupBy(r => r.__LICENCE_ID)
                .ToDictionary(g => g.Key,
                    g => g.Select(r => new JObject(
                        new JProperty("part",
                            Utils.ToJObject(
                                new
                                {
                                    r.__oldId,
                                    r.__migrTable,

                                    r.inspector,
                                    r.documentDateValidFrom,
                                    r.documentDateValidTo,
                                    r.notes,
                                    r.notesAlt,
                                    r.licenceAction,
                                    r.stampNumber,
                                    r.limitations,

                                    r.includedRatings,
                                    r.includedMedicals,
                                    r.includedTrainings,
                                    r.includedLangCerts,
                                    r.includedLicences,
                                    r.includedChecks,
                                    r.includedExams,

                                    r.amlLimitations
                                })),
                        new JProperty("files",
                            new JArray(
                                new JObject(
                                    new JProperty("isAdded", true),
                                    new JProperty("file", null),
                                    new JProperty("caseType", (r.staffType.Code != null ? Utils.ToJObject(PersonUtils.getPersonCaseTypeByStaffTypeCode(noms, r.staffType.Code)) : null)
                                        ?? Utils.ToJObject(noms["personCaseTypes"].ByAlias("person"))),
                                    new JProperty("bookPageNumber", r.bookPageNumber),
                                    new JProperty("pageCount", r.pageCount),
                                    new JProperty("applications", r.applications)),
                            new JObject(
                                    new JProperty("isAdded", true),
                                    new JProperty("file", null),
                                    new JProperty("caseType", (r.staffType.Code != null ? Utils.ToJObject(PersonUtils.getPersonCaseTypeByStaffTypeCode(noms, r.staffType.Code)) : null)
                                        ?? Utils.ToJObject(noms["personCaseTypes"].ByAlias("person"))),
                                    new JProperty("bookPageNumber", null),
                                    new JProperty("pageCount", null),
                                    new JProperty("applications", new JArray())))))));
        }

        private IList<JObject> getPersonLicences(
            int personId,
            Func<int?, JObject> getPersonByApexId,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, PartVersion<JObject>> employmentsByOldId)
        {
            Func<int?, JObject> getEmployment = (employmentId) =>
            {
                if (!employmentId.HasValue)
                {
                    return null;
                }
                else if (!employmentsByOldId.ContainsKey(employmentId.Value))
                {
                    return null;
                }
                else
                {
                    var employment = employmentsByOldId[employmentId.Value];
                    return new JObject(
                        new JProperty("nomValueId", employment.Part.Index),
                        new JProperty("name", string.Format(
                            "{0}, {1} {2}",
                            employment.Content.Get<string>("organization.name"),
                            employment.Content.Get<DateTime>("hiredate").ToString("dd.MM.yyyy"),
                            employment.Content.Get<string>("valid.code") == "N" ? "(НЕВАЛИДНА)" : null)));
                }
            };
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
                        valid = noms["boolean"].ByCode(r.Field<string>("CHANGE_TO_VALID_YN") == "Y" ? "Y" : "N"),
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
                        ST.CODE as STAFF_TYPE_CODE,
                        LT.CODE as LICENCE_TYPE_CODE
                    FROM CAA_DOC.LICENCE L
                    INNER JOIN CAA_DOC.NM_LICENCE_TYPE LT ON L.LICENCE_TYPE_ID = LT.ID
                    INNER JOIN CAA_DOC.NM_STAFF_TYPE ST ON LT.STAFF_TYPE_ID = ST.ID
                    WHERE {0}",
                new DbClause("L.PERSON_ID = {0}", personId)
                )
                .Materialize(r => new JObject(
                    new JProperty("part",
                        Utils.ToJObject(new
                        {
                            __oldId = r.Field<int>("ID"),
                            __migrTable = "LICENCE",

                            //TODO show somewhere?
                            __ISSUE_DATE = r.Field<DateTime?>("ISSUE_DATE"),

                            licenceType = noms["licenceTypes"].ByOldId(r.Field<string>("LICENCE_TYPE_ID")),
                            isFcl = r.Field<string>("LICENCE_TYPE_CODE").Contains("FCL") ? true : false,
                            licenceNumber = r.Field<string>("LICENCE_NO"),
                            foreignLicenceNumber = r.Field<string>("FOREIGN_LICENCE_NO"),
                            valid = noms["boolean"].ByCode(r.Field<string>("VALID_YN") == "Y" ? "Y" : "N"),
                            statuses = statuses.ContainsKey(r.Field<int>("ID")) ? statuses[r.Field<int>("ID")] : null,
                            publisher = noms["caa"].ByOldId(r.Field<int?>("PUBLISHER_CAA_ID").ToString()),
                            foreignPublisher = noms["caa"].ByOldId(r.Field<int?>("FOREIGN_CAA_ID").ToString()),
                            employment = getEmployment(r.Field<int?>("EMPLOYEE_ID"))
                        })),
                        new JProperty("files",
                            new JArray(
                              new JObject(
                                    new JProperty("isAdded", true),
                                    new JProperty("file", null),
                                    new JProperty("caseType", Utils.ToJObject(PersonUtils.getPersonCaseTypeByStaffTypeCode(noms, (r.Field<string>("STAFF_TYPE_CODE"))))),
                                    new JProperty("bookPageNumber", null),
                                    new JProperty("pageCount", null),
                                    new JProperty("applications", new JArray()))))))
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
                { "Limited to,  Metal structure aeroplanes", "Limited to: Metal structure aeroplanes" },
                { "Group 3,  Piston-engine airplanes-Limited to,  Metal Aeroplanes", "Group 3: Piston-engine airplanes-Limited to: Metal Aeroplanes" },
                { "Piston-engine airplanes-Limited to,  Metal Aeroplanes", "Group 3: Piston-engine airplanes-Limited to: Metal Aeroplanes" },
                { "Limited to,  Metal aeroplanes", "Limited to: Metal aeroplanes" },
                { "Limited to,  Metal and Composite structure aeroplanes", "Limited to: Metal and Composite structure aeroplanes" },
                { "Limited to,  Composite aeroplanes", "Limited to: Composite aeroplanes" },
                { "Limited to,  Metal tubing and fabric aeroplanes", "Limited to: Metal tubing and fabric aeroplanes" }
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
                .Select(sc => postSplitLimFixup(sc.Trim()))
                .Where(sc => noms["limitations66"].Any(v => v.Value.Code == sc))
                .Select(sc => noms["limitations66"].ByCode(sc))
                .ToArray();
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
                    this.oracleConn.Dispose();
                }
            }
            finally
            {
                disposed = true;
            }
        }
    }
}
