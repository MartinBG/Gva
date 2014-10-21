using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Autofac.Features.OwnedInstances;
using Common.Api.Models;
using Common.Api.Repositories.UserRepository;
using Common.Api.UserContext;
using Common.Data;
using Common.Json;
using Common.Tests;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.Repositories.AircraftRepository;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.PersonRepository;
using Gva.MigrationTool.Nomenclatures;
using Gva.MigrationTool.Sets.Common;
using Newtonsoft.Json.Linq;
using Oracle.DataAccess.Client;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.MigrationTool.Sets
{
    public class OrganizationLotMigrator : IDisposable
    {
        private bool disposed = false;
        private Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, IUserRepository, IFileRepository, IApplicationRepository, IPersonRepository, IAircraftRepository, ILotEventDispatcher, ICaseTypeRepository, UserContext>>> dependencyFactory;
        private OracleConnection oracleConn;

        public OrganizationLotMigrator(
            OracleConnection oracleConn,
            Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, IUserRepository, IFileRepository, IApplicationRepository, IPersonRepository, IAircraftRepository, ILotEventDispatcher, ICaseTypeRepository, UserContext>>> dependencyFactory)
        {
            this.dependencyFactory = dependencyFactory;
            this.oracleConn = oracleConn;
        }

        public void StartMigrating(
            //input constants
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, int> personIdToLotId,
            Dictionary<int, int> orgApexIdToLotId,
            Func<int?, JObject> getAircraftByApexId,
            Func<int?, JObject> getPersonByApexId,
            Dictionary<int, string> blobIdsToFileKeys,
            //intput
            ConcurrentQueue<int> organizationIds,
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

            int organizationId;
            while (organizationIds.TryDequeue(out organizationId))
            {
                ct.ThrowIfCancellationRequested();

                try
                {
                    using (var dependencies = this.dependencyFactory())
                    {
                        var unitOfWork = dependencies.Value.Item1;
                        var lotRepository = dependencies.Value.Item2;
                        var userRepository = dependencies.Value.Item3;
                        var fileRepository = dependencies.Value.Item4;
                        var applicationRepository = dependencies.Value.Item5;
                        var personRepository = dependencies.Value.Item6;
                        var aircraftRepository = dependencies.Value.Item7;
                        var lotEventDispatcher = dependencies.Value.Item8;
                        var caseTypeRepository = dependencies.Value.Item9;
                        var context = dependencies.Value.Item10;

                        var lot = lotRepository.GetLotIndex(orgApexIdToLotId[organizationId], fullAccess: true);
                        bool isApprovedOrg = this.getIsApprovedOrg(organizationId);

                        Func<string, JObject, PartVersion> addPartWithFiles = (path, content) =>
                        {
                            var pv = lot.CreatePart(path, content.Get<JObject>("part"), context);
                            fileRepository.AddFileReferences(pv.Part, content.GetItems<CaseDO>("files"));
                            return pv;
                        };

                        Dictionary<int, Tuple<GvaApplication, ApplicationNomDO>> applications =
                            new Dictionary<int, Tuple<GvaApplication, ApplicationNomDO>>();
                        var aircraftDocumentApplications = this.getOrganizationDocumentApplications(organizationId, noms, blobIdsToFileKeys, isApprovedOrg);
                        foreach (var aircraftDocumentApplication in aircraftDocumentApplications)
                        {
                            var pv = addPartWithFiles("organizationDocumentApplications/*", aircraftDocumentApplication);

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

                            IList<GvaApplicationStage> appStages = CommonUtils.getApplicationStages(this.oracleConn, personIdToLotId, appNomDO.ApplicationId, app.Key);

                            foreach (GvaApplicationStage stage in appStages)
                            {
                                unitOfWork.DbContext.Set<GvaApplicationStage>().Add(stage);
                            }
                        }

                        Dictionary<int, int> inspectionPartIndexes = new Dictionary<int, int>();
                        var organizationInspections = this.getOrganizationInspections(organizationId, nomApplications, getPersonByApexId, blobIdsToFileKeys, noms);
                        foreach (var organizationInspection in organizationInspections)
                        {
                            var pv = addPartWithFiles("organizationInspections/*", organizationInspection);
                            inspectionPartIndexes.Add(organizationInspection.Get<int>("part.__oldId"), pv.Part.Index);
                        }

                        var organizationAddresses = this.getOrganizationAddress(organizationId, noms);
                        foreach (var organizationAddress in organizationAddresses)
                        {
                            addPartWithFiles("organizationAddresses/*", organizationAddress);
                        }

                        Dictionary<int, PartVersion> organizationDocuments = new Dictionary<int, PartVersion>();

                        var organizationDocumentOthers = this.getOrganizationDocumentOthers(organizationId, nomApplications, noms, blobIdsToFileKeys, isApprovedOrg);
                        foreach (var organizationDocumentOther in organizationDocumentOthers)
                        {
                            var pv = addPartWithFiles("organizationDocumentOthers/*", organizationDocumentOther);
                            organizationDocuments.Add(organizationDocumentOther["part"]["__oldId"].Value<int>(), pv);
                        }

                        var organizationApprovalAmendments = this.getOrganizationApprovalAmendments(organizationId, noms, organizationDocuments, getPersonByApexId, nomApplications, isApprovedOrg);
                        var organizationApprovals = this.getOrganizationApproval(organizationId, noms, isApprovedOrg);
                        foreach (var organizationApproval in organizationApprovals)
                        {
                            var approvalPartVersion = addPartWithFiles("approvals/*", organizationApproval);

                            int nextIndex = 0;

                            foreach (var amendment in organizationApprovalAmendments[organizationApproval.Get<int>("part.__oldId")])
                            {
                                var amendmentPart = amendment["part"] as JObject;
                                amendmentPart.Add("approvalPartIndex", approvalPartVersion.Part.Index);
                                amendmentPart.Add("index", nextIndex);
                                nextIndex++;

                                var amendmentPartVersion = addPartWithFiles("approvalAmendments/*", amendment);
                            }

                        }

                        var organizationRecommendations = this.getOrganizationRecommendation(organizationId, getPersonByApexId, nomApplications, noms, inspectionPartIndexes);
                        foreach (var organizationRecommendation in organizationRecommendations)
                        {
                            addPartWithFiles("organizationRecommendations/*", organizationRecommendation);
                        }

                        var organizationManagementStaffs = this.getOrganizationManagementStaff(organizationId, noms, getPersonByApexId, isApprovedOrg);
                        foreach (var organizationManagementStaff in organizationManagementStaffs)
                        {
                            addPartWithFiles("organizationStaffManagement/*", organizationManagementStaff);
                        }

                        var organizationStaffExaminers = this.getOrganizationStaffExaminers(organizationId, noms, getPersonByApexId);
                        foreach (var organizationStaffExaminer in organizationStaffExaminers)
                        {
                            if (organizationStaffExaminer.Get<int?>("part.person.nomValueId") == null)
                            {
                                Console.WriteLine("CANNOT FIND PERSON FOR organizationStaffExaminer WITH oldId " + organizationStaffExaminer.Get<int>("part.__oldId"));
                                continue;
                            }
                            addPartWithFiles("organizationStaffExaminers/*", organizationStaffExaminer);
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

                        Console.WriteLine("Migrated organizationId: {0}", organizationId);
                    }
                }
                catch (Exception)
                {
                    cts.Cancel();
                    throw;
                }
            }
        }

        private bool getIsApprovedOrg(int organizationId)
        {
            int approvalCount = this.oracleConn.CreateStoreCommand(
                @"SELECT COUNT(*) AP_COUNT FROM CAA_DOC.APPROVAL WHERE {0}",
                new DbClause("ID_FIRM = {0}", organizationId))
                .Materialize(r => r.Field<int>("AP_COUNT"))
                .Single();

            int auditsCount = this.oracleConn.CreateStoreCommand(
                @"SELECT COUNT(*) A_COUNT FROM CAA_DOC.AUDITS WHERE {0}",
                new DbClause("ID_FIRM = {0}", organizationId))
                .Materialize(r => r.Field<int>("A_COUNT"))
                .Single();

            List<NomValue> result = new List<NomValue>();

            return approvalCount > 0 || auditsCount > 0;
        }

        private IList<JObject> getOrganizationDocumentApplications(
            int organizationId,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, string> blobIdsToFileKeys,
            bool isApprovedOrg)
        {
            var parts = this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.REQUEST WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and APPLICANT_FIRM_ID = {0}", organizationId)
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
                new DbClause("and R.APPLICANT_FIRM_ID = {0}", organizationId)
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
                                    new JProperty("caseType", isApprovedOrg ?
                                        Utils.ToJObject(noms["organizationCaseTypes"].ByAlias("approvedOrg")) :
                                        Utils.ToJObject(noms["organizationCaseTypes"].ByAlias("others"))),
                                    new JProperty("bookPageNumber", bookPageNumber),
                                    new JProperty("pageCount", pageCount),
                                    new JProperty("applications", new JArray()))))))
                    .ToList();
        }

        private IList<JObject> getOrganizationInspections(
            int organizationId,
            Dictionary<int, JObject> nomApplications,
            Func<int?, JObject> getPersonByApexId,
            Dictionary<int, string> blobIdsToFileKeys,
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
                    new DbClause("ADT.ID_FIRM = {0}", organizationId)
                )
                .Materialize(r =>
                    new
                    {
                        ID_AUDIT = r.Field<int>("ID_AUDIT"),
                        ID_REQUIREMENT = r.Field<int?>("ID_REQUIREMENT"),
                        SEQ = r.Field<int?>("SEQ"),
                        REF_NUMBER = r.Field<string>("REF_NUMBER"),
                        DESCRIPTION = r.Field<string>("DESCRIPTION"),
                        REC_DISPRT_LEVEL = r.Field<int?>("REC_DISPRT_LEVEL"),
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
                    new DbClause("ADT.ID_FIRM = {0}", organizationId)
                )
                .Materialize(r =>
                    new
                    {
                        ID_AUDIT = r.Field<int>("ID_AUDIT"),
                        STATE = r.Field<string>("STATE"),
                        ID_REQUIREMENT = r.Field<int?>("ID_REQUIREMENT")
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
                    new DbClause("ADT.ID_FIRM = {0}", organizationId)
                )
                .Materialize(r =>
                    new
                    {
                        AUDIT_ID = r.Field<int>("AUDIT_ID"),
                        SEQ = r.Field<int?>("SEQ"),
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

            var controlCards = this.oracleConn.CreateStoreCommand(
                @"SELECT A.ID,
                        D.DOC_ID,
                        D.MIME_TYPE,
                        D.NAME
                    FROM CAA_DOC.AUDITS A
                    LEFT OUTER JOIN CAA_DOC.DOCLIB_DOCUMENTS D ON A.ID + 80000000 = D.DOC_ID
                    WHERE {0}",
                new DbClause("A.ID_FIRM = {0}", organizationId)
                )
                .Materialize(r => 
                    new
                    {
                        AUDIT_ID = r.Field<int>("ID"),
                        DOC_ID = r.Field<int?>("DOC_ID"),
                        NAME = r.Field<string>("NAME"),
                        MIME_TYPE = r.Field<string>("MIME_TYPE")
                    })
                .ToDictionary(a => a.AUDIT_ID, a => a.DOC_ID == null ? null :
                    new
                    {
                        key = blobIdsToFileKeys[a.DOC_ID.Value],
                        name = a.NAME,
                        mimeType = a.MIME_TYPE
                    });

            return this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AUDITS WHERE {0}",
                new DbClause("ID_FIRM = {0}", organizationId)
                )
                .Materialize(r => new JObject(
                    new JProperty("part",
                        Utils.ToJObject(new
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
                                controlCard = controlCards[r.Field<int>("ID")],
                                inspectionDetails = inspectionDetails[r.Field<int>("ID")],
                                disparities = disparities[r.Field<int>("ID")],
                                examiners = examiners[r.Field<int>("ID")]
                            })),
                        new JProperty("files",
                            new JArray(
                                new JObject(
                                    new JProperty("isAdded", true),
                                    new JProperty("file", null),
                                    new JProperty("caseType", Utils.ToJObject(noms["organizationCaseTypes"].ByAlias("approvedOrg"))),
                                    new JProperty("bookPageNumber", null),
                                    new JProperty("pageCount", null),
                                    new JProperty("applications", 
                                        (r.Field<decimal?>("ID_REQUEST") != null && nomApplications.ContainsKey(r.Field<int>("ID_REQUEST"))) ?
                                        new object[] { nomApplications[r.Field<int>("ID_REQUEST")] } :
                                        new object[0]))))))
                        .ToList();
        }

        private IList<JObject> getOrganizationAddress(int organizationId, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            JArray files = new JArray(){
                noms["organizationCaseTypes"].Values.Select(c => 
                    Utils.ToJObject(new
                    {
                        isAdded = true,
                        file = (object)null,
                        caseType = Utils.ToJObject(c),
                        bookPageNumber = (string)null,
                        pageCount = (int?)null,
                        applications =  new JArray()
                    }))
            };

            return this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.FIRM_ADDRESS WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and FIRM_ID = {0}", organizationId)
                )
                .Materialize(r => new JObject(
                    new JProperty("part",
                        Utils.ToJObject(new
                        {
                            __oldId = r.Field<int>("ID"),
                            __migrTable = "FIRM_ADDRESS",
                            addressType = noms["addressTypes"].ByOldId(r.Field<decimal?>("ADDRESS_TYPE_ID").ToString()),
                            valid = noms["boolean"].ByCode(r.Field<string>("VALID_YN") == "Y" ? "Y" : "N"),
                            settlement = noms["cities"].ByOldId(r.Field<decimal?>("TOWN_VILLAGE_ID").ToString()),
                            address = r.Field<string>("ADDRESS"),
                            addressAlt = r.Field<string>("ADDRESS_TRANS"),
                            phone = r.Field<string>("PHONES"),
                            fax = r.Field<string>("FAXES"),
                            postalCode = r.Field<string>("POSTAL_CODE"),
                            contactPerson = r.Field<string>("CONTACT_PERSON"),
                            email = r.Field<string>("E_MAIL")
                        })),
                        new JProperty("files", files)))
                .ToList();
        }

        private IList<JObject> getOrganizationDocumentOthers(
            int organizationId,
            Dictionary<int, JObject> nomApplications,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, string> blobIdsToFileKeys,
            bool isApprovedOrg)
        {
            var parts = this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.PERSON_DOCUMENT WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and FIRM_ID = {0}", organizationId)
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
                        documentType = noms["documentTypes"].ByOldId(r.Field<decimal?>("DOCUMENT_TYPE_ID").ToString()),
                        documentRole = noms["documentRoles"].ByOldId(r.Field<decimal?>("DOCUMENT_ROLE_ID").ToString()),
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
                new DbClause("and PD.FIRM_ID = {0}", organizationId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("DOC_ID"),
                        __migrTable = "DOCLIB_DOCUMENTS",
                        __ORGANIZATION_DOCUMENT_ID = r.Field<int>("ID"),

                        key = blobIdsToFileKeys[r.Field<int>("DOC_ID")],
                        name = r.Field<string>("NAME"),
                        mimeType = r.Field<string>("MIME_TYPE")
                    }))
                .ToList();

            return (from part in parts
                    join file in files on part.Get<int>("__oldId") equals file.Get<int>("__ORGANIZATION_DOCUMENT_ID") into fg
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
                                    "documentType",
                                    "documentRole",
                                    "valid",
                                    "notes",
                                })),
                        new JProperty("files",
                            new JArray(
                                new JObject(
                                    new JProperty("isAdded", true),
                                    new JProperty("file", Utils.Pluck(file, new string[] { "key", "name", "mimeType" })),
                                    new JProperty("caseType", isApprovedOrg ?
                                        Utils.ToJObject(noms["organizationCaseTypes"].ByAlias("approvedOrg")) :
                                        Utils.ToJObject(noms["organizationCaseTypes"].ByAlias("others"))),
                                    new JProperty("bookPageNumber", bookPageNumber),
                                    new JProperty("pageCount", pageCount),
                                    new JProperty("applications", new JArray()))))))
                    .ToList();
        }
        private IDictionary<int, IEnumerable<JObject>> getOrganizationApprovalAmendments(
            int organizationId, 
            Dictionary<string,Dictionary<string, NomValue>> noms,
            Dictionary<int, PartVersion> orgDocuments,
            Func<int?, JObject> getPersonByApexId,
            Dictionary<int, JObject> nomApplications,
            bool isApprovedOrg)
        {

            var limMG = this.oracleConn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.SCH_MG_APPROVAL")
                .Materialize(r =>
                    new
                    {
                        __oldId = r.Field<long?>("ID"),
                        limitation = r.Field<string>("TYPE_AC")
                    })
                .ToDictionary(l => l.__oldId, l => l.limitation);

            var lim145 = this.oracleConn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.SCH_MF145_APPROVAL")
                .Materialize(r =>
                    new
                    {
                        __oldId = r.Field<long?>("ID"),
                        limitation = noms["lim145limitations"].ByOldId(r.Field<long?>("ID_MF145_LIMIT").ToString()).Name()
                    })
                .ToDictionary(l => l.__oldId, l => l.limitation);

            var lim147 = this.oracleConn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.SCH_147_APPROVAL")
                .Materialize(r =>
                    new
                    {
                        __oldId = r.Field<long?>("ID"),
                        limitation = noms["lim147limitations"].ByOldId(r.Field<long?>("ID_147_LIMIT").ToString()).Name()
                    })
                .ToDictionary(l => l.__oldId, l => l.limitation);

            var includedDocumentsResults = this.oracleConn.CreateStoreCommand(
                @"SELECT ADI.ID, ADI.EXAMINER_ID, ADI.APPROWAL_DATE, ADI.ID_SCH_145, ADI.ID_SCH_147, ADI.ID_SCH_MG, ADI.PERSON_DOCUMENT_ID, APS.ID AS APS_ID
                    FROM CAA_DOC.APPROVAL_DOC_INCL ADI
                    JOIN CAA_DOC.APPROVAL_SCHEDULE APS
                        ON ADI.APPROVAL_ID = APS.ID
                    JOIN CAA_DOC.APPROVAL AP
                        ON APS.ID_APPROVAL = AP.ID
                    WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and AP.ID_FIRM = {0}", organizationId)
                )
                .Materialize(r =>
                    new
                    {
                        __approvalScheduleId = r.Field<int>("APS_ID"),
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "APPROVAL_DOC_INCL",
                        inspector = getPersonByApexId((int?)r.Field<decimal?>("EXAMINER_ID")),
                        approvalDate = r.Field<DateTime?>("APPROWAL_DATE"),
                        linkedLim = r.Field<long?>("ID_SCH_145") == null ?
                                r.Field<long?>("ID_SCH_147") == null ? r.Field<long?>("ID_SCH_MG") == null ? null : 
                                new {
                                    Id = r.Field<long?>("ID_SCH_MG"),
                                    Text = limMG[r.Field<long?>("ID_SCH_MG")]
                                } :
                                new {
                                    Id = r.Field<long?>("ID_SCH_147"),
                                    Text = limMG[r.Field<long?>("ID_SCH_147")]
                                } :
                                new {
                                    Id = r.Field<long?>("ID_SCH_145"),
                                    Text = limMG[r.Field<long?>("ID_SCH_145")]
                                },
                        partIndex = orgDocuments.ContainsKey(r.Field<int>("PERSON_DOCUMENT_ID")) ?
                            (int?)orgDocuments[r.Field<int>("PERSON_DOCUMENT_ID")].Part.Index :
                            null,
                        setPartAlias = orgDocuments.ContainsKey(r.Field<int>("PERSON_DOCUMENT_ID")) ?
                            orgDocuments[r.Field<int>("PERSON_DOCUMENT_ID")].Part.SetPart.Alias :
                            null,
                    })
                .GroupBy(g => g.__approvalScheduleId)
                .ToDictionary(l => l.Key, l => l.Select(n =>
                    new
                    {
                        n.__oldId,
                        n.__migrTable,
                        n.inspector,
                        n.approvalDate,
                        n.linkedLim,
                        n.partIndex,
                        n.setPartAlias
                    }).ToArray());

            var limsMGResults = this.oracleConn.CreateStoreCommand(
                @"SELECT MGA.ID, MGA.TYPE_AC, MGA.QUALITY_SYSTEM, MGA.AW_APPROVAL, MGA.PF_APPROVAL, APS.ID AS APS_ID
                    FROM CAA_DOC.SCH_MG_APPROVAL MGA
                    JOIN CAA_DOC.APPROVAL_SCHEDULE APS
                        ON MGA.ID_MG_APPROVAL = APS.ID
                    JOIN CAA_DOC.APPROVAL AP
                        ON APS.ID_APPROVAL = AP.ID
                    WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and AP.ID_FIRM = {0}", organizationId)
                )
                .Materialize(r =>
                    new
                    {
                        __approvalScheduleId = r.Field<int>("APS_ID"),
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "SCH_MG_APPROVAL",
                        aircraftTypeGroup = r.Field<string>("TYPE_AC"),
                        qualitySystem = r.Field<string>("QUALITY_SYSTEM"),
                        awapproval = noms["boolean"].ByCode(r.Field<string>("AW_APPROVAL") == "Y" ? "Y" : "N"),
                        pfapproval = noms["boolean"].ByCode(r.Field<string>("PF_APPROVAL") == "Y" ? "Y" : "N")
                    })
                .GroupBy(g => g.__approvalScheduleId)
                .ToDictionary(l => l.Key, l => l.Select(n =>
                    new
                    {
                        n.__oldId,
                        n.__migrTable,
                        n.aircraftTypeGroup,
                        n.qualitySystem,
                        n.awapproval,
                        n.pfapproval
                    }).ToArray());

            var limsMF145Results = this.oracleConn.CreateStoreCommand(
                @"SELECT A145.ID, A145.ID_MF145_LIMIT, A145.LIMITATION, A145.BASE, A145.LINE, APS.ID AS APS_ID
                    FROM CAA_DOC.SCH_MF145_APPROVAL A145
                    JOIN CAA_DOC.APPROVAL_SCHEDULE APS
                        ON A145.ID_MF145_APPROVAL = APS.ID
                    JOIN CAA_DOC.APPROVAL AP
                        ON APS.ID_APPROVAL = AP.ID
                    WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and AP.ID_FIRM = {0}", organizationId)
                )
                .Materialize(r =>
                    new
                    {
                        __approvalScheduleId = r.Field<int>("APS_ID"),
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "SCH_MF145_APPROVAL",
                        lim145limitation = noms["lim145limitations"].ByOldId(r.Field<long?>("ID_MF145_LIMIT").ToString()).Name(),
                        lim145limitationText = r.Field<string>("LIMITATION"),
                        basic = noms["boolean"].ByCode(r.Field<string>("BASE") == "Y" ? "Y" : "N"),
                        line = noms["boolean"].ByCode(r.Field<string>("LINE") == "Y" ? "Y" : "N"),
                    })
                .GroupBy(g => g.__approvalScheduleId)
                .ToDictionary(l => l.Key, l => l.Select(n =>
                    new
                    {
                        n.__oldId,
                        n.__migrTable,
                        n.lim145limitation,
                        n.lim145limitationText,
                        n.basic,
                        n.line
                    }).ToArray());

            var lims147Results = this.oracleConn.CreateStoreCommand(
                @"SELECT A147.ID, A147.ID_147_LIMIT, A147.LIMITATION, A147.SORT_ORDER, APS.ID AS APS_ID
                    FROM CAA_DOC.SCH_147_APPROVAL A147
                    JOIN CAA_DOC.APPROVAL_SCHEDULE APS
                        ON A147.ID_APPROVAL = APS.ID
                    JOIN CAA_DOC.APPROVAL AP
                        ON APS.ID_APPROVAL = AP.ID
                    WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and AP.ID_FIRM = {0}", organizationId)
                )
                .Materialize(r =>
                    new
                    {
                        __approvalScheduleId = r.Field<int>("APS_ID"),
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "SCH_147_APPROVAL",
                        lim147limitation = noms["lim147limitations"].ByOldId(r.Field<long?>("ID_147_LIMIT").ToString()).Name(),
                        lim147limitationText = r.Field<string>("LIMITATION"),
                        sortOrder = r.Field<short?>("SORT_ORDER")
                    })
                .GroupBy(g => g.__approvalScheduleId)
                .ToDictionary(l => l.Key, l => l.Select(n =>
                    new
                    {
                        n.__oldId,
                        n.__migrTable,
                        n.lim147limitation,
                        n.lim147limitationText,
                        n.sortOrder,
                    }).ToArray());

            return this.oracleConn.CreateStoreCommand(
                @"SELECT APS.ID, APS.APPROVAL_TYPE, APS.REFERENCE, APS.ISSUE_DATE, APS.CHANGE_NUM, APS.ID_REQUEST, AP.ID AS AP_ID
                    FROM CAA_DOC.APPROVAL_SCHEDULE APS
                    JOIN CAA_DOC.APPROVAL AP
                        ON APS.ID_APPROVAL = AP.ID
                    WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and AP.ID_FIRM = {0}", organizationId)
                )
                .Materialize(r =>
                    new
                    {
                        __approvalId = r.Field<int>("AP_ID"),
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "APPROVAL_SCHEDULE",
                        approvalType = noms["approvalTypes"].ByCode(r.Field<string>("APPROVAL_TYPE")),
                        documentNumber = r.Field<string>("REFERENCE"),
                        documentDateIssue = r.Field<DateTime?>("ISSUE_DATE"),
                        changeNum = r.Field<short?>("CHANGE_NUM"),
                        lims147 = r.Field<long?>("ID") != null && lims147Results.ContainsKey(r.Field<int>("ID")) ? lims147Results[r.Field<int>("ID")] : null,
                        lims145 = r.Field<long?>("ID") != null && limsMF145Results.ContainsKey(r.Field<int>("ID")) ? limsMF145Results[r.Field<int>("ID")] : null,
                        limsMG = r.Field<long?>("ID") != null && limsMGResults.ContainsKey(r.Field<int>("ID")) ? limsMGResults[r.Field<int>("ID")] : null,
                        includedDocuments = r.Field<long?>("ID") != null && includedDocumentsResults.ContainsKey(r.Field<int>("ID")) ? includedDocumentsResults[r.Field<int>("ID")] : null,
                        applications = (r.Field<decimal?>("ID_REQUEST") != null && nomApplications.ContainsKey(r.Field<int>("ID_REQUEST"))) ?
                            new JArray() { nomApplications[r.Field<int>("ID_REQUEST")] } :
                            new JArray()
                    })
                .GroupBy(g => g.__approvalId)
                .ToDictionary(a => a.Key, a => a.Select(n =>
                    new JObject(
                        new JProperty("part",
                         Utils.ToJObject( new {
                            n.__oldId,
                            n.__migrTable,
                            n.approvalType,
                            n.documentNumber,
                            n.documentDateIssue,
                            n.changeNum,
                            n.lims147,
                            n.lims145,
                            n.limsMG,
                            n.includedDocuments,
                        })),
                        new JProperty("files",
                            new JArray(
                              new JObject(
                                    new JProperty("isAdded", true),
                                    new JProperty("file", null),
                                    new JProperty("caseType", isApprovedOrg ?
                                        Utils.ToJObject(noms["organizationCaseTypes"].ByAlias("approvedOrg")) :
                                        Utils.ToJObject(noms["organizationCaseTypes"].ByAlias("others"))),
                                    new JProperty("bookPageNumber", null),
                                    new JProperty("pageCount", null),
                                    new JProperty("applications", n.applications)))))));
        }

        //TODO
        private IList<JObject> getOrganizationApproval(
            int organizationId, 
            Dictionary<string, Dictionary<string, NomValue>> noms,
            bool isApprovedOrg)
        {
            return this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.APPROVAL WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_FIRM = {0}", organizationId)
                )
                .Materialize(r => new JObject(
                    new JProperty("part",
                        Utils.ToJObject(new
                        {
                            __oldId = r.Field<int>("ID"),
                            __migrTable = "APPROVAL",
                            approvalType = noms["approvalTypes"].ByCode(r.Field<string>("APPROVAL_TYPE")),
                            documentNumber = r.Field<string>("REFERENCE"),
                            documentDateIssue = r.Field<DateTime?>("ISSUE_DATE"),
                            approvalState = noms["approvalStates"].ByCode(r.Field<string>("STATE")),
                            approvalStateDate = r.Field<DateTime?>("STATE_DATE"),
                            approvalStateNote = r.Field<string>("STATE_REMARKS"),
                        })),
                        new JProperty("files",
                            new JArray(
                              new JObject(
                                    new JProperty("isAdded", true),
                                    new JProperty("file", null),
                                    new JProperty("caseType", isApprovedOrg ?
                                        Utils.ToJObject(noms["organizationCaseTypes"].ByAlias("approvedOrg")) :
                                        Utils.ToJObject(noms["organizationCaseTypes"].ByAlias("others"))),
                                    new JProperty("bookPageNumber", null),
                                    new JProperty("pageCount", null),
                                    new JProperty("applications", new JArray()))))))
                .ToList();
        }

        private IList<JObject> getOrganizationRecommendation(
            int organizationId,
            Func<int?, JObject> getPersonByApexId,
            Dictionary<int, JObject> nomApplications,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, int> inspectionPartIndexes)
        {
            JArray files = new JArray(){
                noms["organizationCaseTypes"].Values.Select(c => 
                    Utils.ToJObject(new
                    {
                        isAdded = true,
                        file = (object)null,
                        caseType = Utils.ToJObject(c),
                        bookPageNumber = (string)null,
                        pageCount = (int?)null,
                        applications =  new JArray()
                    }))
            };

            var disparities = this.oracleConn.CreateStoreCommand(
                @"SELECT R.ID REC_ID,
                        RC.ID_SECTION SECTION_DETAIL_ID,
                        RD.SEQ,
                        RD.REF_NUMBER,
                        RD.DESCRIPTION,
                        RD.REC_DISPRT_LEVEL,
                        RD.REMOVAL_DATE,
                        RD.RECTIFY_ACTION,
                        RD.CLOSURE_DATE,
                        RD.CLOSURE_DOC
                    FROM 
                    CAA_DOC.RECOMMENDATION R
                    LEFT OUTER JOIN CAA_DOC.REC_COMPLIANCE RC ON R.ID = RC.ID_REC
                    LEFT OUTER JOIN CAA_DOC.REC_DISPARITY RD ON RC.ID = RD.ID_REC_COMPLIANCE",
                    new DbClause("R.ID_FIRM = {0}", organizationId)
                )
                .Materialize(r =>
                    new
                    {
                        REC_ID = r.Field<int>("REC_ID"),
                        SECTION_DETAIL_ID = r.Field<int?>("SECTION_DETAIL_ID"),
                        SEQ = r.Field<int?>("SEQ"),
                        REF_NUMBER = r.Field<string>("REF_NUMBER"),
                        DESCRIPTION = r.Field<string>("DESCRIPTION"),
                        REC_DISPRT_LEVEL = r.Field<decimal?>("REC_DISPRT_LEVEL"),
                        REMOVAL_DATE = r.Field<DateTime?>("REMOVAL_DATE"),
                        RECTIFY_ACTION = r.Field<string>("RECTIFY_ACTION"),
                        CLOSURE_DATE = r.Field<DateTime?>("CLOSURE_DATE"),
                        CLOSURE_DOC = r.Field<string>("CLOSURE_DOC")
                    })
                    .GroupBy(g => g.REC_ID)
                    .ToDictionary(
                        g => g.Key,
                        g => g
                            .Where(d => d.SECTION_DETAIL_ID.HasValue && d.REC_DISPRT_LEVEL.HasValue)
                            .OrderBy(d => d.SEQ)
                            .Select(d =>
                                new
                                {
                                    detailCode = noms["auditPartSectionDetails"].ByOldId(d.SECTION_DETAIL_ID.ToString()).Code,
                                    refNumber = d.REF_NUMBER,
                                    disparityLevel = noms["disparityLevels"].ByCode(d.REC_DISPRT_LEVEL.ToString()),
                                    rectifyAction = d.RECTIFY_ACTION,
                                    closureDocument = d.CLOSURE_DOC,
                                    description = d.DESCRIPTION,
                                    removalDate = d.REMOVAL_DATE,
                                    closureDate = d.CLOSURE_DATE
                                })
                            .ToArray());

            var recommendationDetails = this.oracleConn.CreateStoreCommand(
                @"SELECT R.ID REC_ID,
                        RC.ID_SECTION SECTION_DETAIL_ID,
                        RC.STATE,
                        S.NAME SECTION_NAME,
                        S.CODE SECTION_CODE
                    FROM 
                    CAA_DOC.RECOMMENDATION R
                    LEFT OUTER JOIN CAA_DOC.REC_COMPLIANCE RC ON R.ID = RC.ID_REC
                    LEFT OUTER JOIN CAA_DOC.NM_SECTION_DETAIL SD ON RC.ID_SECTION  = SD.ID
                    LEFT OUTER JOIN CAA_DOC.NM_SECTION S ON SD.ID_SECTION = S.ID
                    WHERE {0}",
                    new DbClause("R.ID_FIRM = {0}", organizationId)
                )
                .Materialize(r =>
                    new
                    {
                        REC_ID = r.Field<int>("REC_ID"),
                        SECTION_DETAIL_ID = r.Field<int?>("SECTION_DETAIL_ID"),
                        STATE = r.Field<string>("STATE"),
                        SECTION_NAME = r.Field<string>("SECTION_NAME"),
                        SECTION_CODE = r.Field<string>("SECTION_CODE")
                    })
                .GroupBy(e => e.REC_ID)
                .ToDictionary(
                    g => g.Key,
                    g => g
                        .Where(i => i.SECTION_DETAIL_ID.HasValue)
                        .Select(ad =>
                            new
                            {
                                sectionName = ad.SECTION_NAME,
                                sectionCode = ad.SECTION_CODE,
                                recommendationResult = noms["recommendationResults"].ByCode(ad.STATE ?? "U"),
                                auditPartSectionDetail = noms["auditPartSectionDetails"].ByOldId(ad.SECTION_DETAIL_ID.ToString())
                            })
                        .GroupBy(ad => new { ad.sectionName, ad.sectionCode })
                        .Select(g1 =>
                            new
                            {
                                sectionName = g1.Key.sectionName,
                                sectionCode = g1.Key.sectionCode,
                                details = g1
                                    .Select(gi =>
                                        new
                                        {
                                            subject = gi.auditPartSectionDetail.Name,
                                            recommendationResult = gi.recommendationResult,
                                            code = gi.auditPartSectionDetail.Code,
                                        })
                                    .ToArray()
                            })
                        .ToArray());

            var inspections = this.oracleConn.CreateStoreCommand(
                @"SELECT R.ID REC_ID,
                        A.ID AUDIT_ID
                    FROM CAA_DOC.RECOMMENDATION R
                    LEFT OUTER JOIN CAA_DOC.AUDITS A ON R.ID = A.ID_REC
                    WHERE {0}",
                new DbClause("R.ID_FIRM = {0}", organizationId)
                )
                .Materialize(r =>
                    new
                    {
                        REC_ID = r.Field<int>("REC_ID"),
                        AUDIT_ID = r.Field<int?>("AUDIT_ID"),
                    })
                .GroupBy(r => r.REC_ID)
                .ToDictionary(
                    g => g.Key,
                    g => g
                        .Where(i => i.AUDIT_ID.HasValue)
                        .Select(gi => inspectionPartIndexes[gi.AUDIT_ID.Value])
                        .ToArray());

            var examiners = this.oracleConn.CreateStoreCommand(
                @"SELECT A.PART,
                        A.SEQ,
                        E.PERSON_ID,
                        R.ID REC_ID
                    FROM
                    CAA_DOC.RECOMMENDATION R
                    LEFT OUTER JOIN CAA_DOC.REC_AUDITOR A ON R.ID = A.ID_REC
                    LEFT OUTER JOIN CAA_DOC.EXAMINER E ON A.ID_EXAMINER = E.ID
                    WHERE {0}",
                    new DbClause("R.ID_FIRM = {0}", organizationId)
                )
                .Materialize(r =>
                    new
                    {
                        PART = r.Field<string>("PART"),
                        REC_ID = r.Field<int>("REC_ID"),
                        SEQ = r.Field<decimal?>("SEQ"),
                        PERSON_ID = r.Field<int?>("PERSON_ID"),
                    })
                .GroupBy(e => e.REC_ID)
                .ToDictionary(
                    g => g.Key,
                    g => g
                        .Where(i => i.SEQ.HasValue && i.PERSON_ID.HasValue && !string.IsNullOrEmpty(i.PART))
                        .GroupBy(i => i.PART)
                        .ToDictionary(
                            g1 => g1.Key,
                            g1 => g1
                                .OrderBy(i => i.SEQ.Value)
                                .Select(i => getPersonByApexId(i.PERSON_ID.Value))
                                .ToArray()));

            return this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.RECOMMENDATION WHERE {0}",
                new DbClause("ID_FIRM = {0}", organizationId)
                )
                .Materialize(r => new JObject(
                    new JProperty("part",
                        Utils.ToJObject(new
                        {
                            __oldId = r.Field<int>("ID"),
                            __migrTable = "RECOMMENDATION",

                            auditPart = noms["auditParts"].ByOldId(r.Field<int>("ID_PART").ToString()),

                            finished1Date = r.Field<DateTime?>("DATE_FINISHED_P1"),
                            town1 = r.Field<string>("TOWN_P1"),
                            finished2Date = r.Field<DateTime?>("DATE_FINISHED_P2"),
                            town2 = r.Field<string>("TOWN_P2"),
                            finished3Date = r.Field<DateTime?>("DATE_FINISHED_P3"),
                            town3 = r.Field<string>("TOWN_P3"),
                            finished4Date = r.Field<DateTime?>("DATE_FINISHED_P4"),
                            town4 = r.Field<string>("TOWN_P4"),
                            finished5Date = r.Field<DateTime?>("DATE_FINISHED_P5"),
                            town5 = r.Field<string>("TOWN_P5"),
                            part1Examiners = examiners[r.Field<int>("ID")].ContainsKey("1") ? examiners[r.Field<int>("ID")]["1"] : new object[0],
                            part2Examiners = examiners[r.Field<int>("ID")].ContainsKey("2") ? examiners[r.Field<int>("ID")]["2"] : new object[0],
                            part3Examiners = examiners[r.Field<int>("ID")].ContainsKey("3") ? examiners[r.Field<int>("ID")]["3"] : new object[0],
                            part4Examiners = examiners[r.Field<int>("ID")].ContainsKey("4") ? examiners[r.Field<int>("ID")]["4"] : new object[0],
                            part5Examiners = examiners[r.Field<int>("ID")].ContainsKey("5") ? examiners[r.Field<int>("ID")]["5"] : new object[0],

                            formDate = r.Field<DateTime?>("FORM_DATE"),
                            formText = r.Field<string>("FORM_TEXT"),
                            interviewedStaff = r.Field<string>("INTERVIEWED_STAFF"),
                            inspectionFromDate = r.Field<DateTime?>("NADZOR_FROM"),
                            inspectionToDate = r.Field<DateTime?>("NADZOR_TO"),
                            documentDescription = r.Field<string>("DESCRIPTION_DOC"),
                            recommendation = r.Field<string>("RECOMMENDATION"),

                            inspections = inspections[r.Field<int>("ID")],
                            recommendationDetails = recommendationDetails[r.Field<int>("ID")],
                            disparities = disparities[r.Field<int>("ID")]
                        })),
                        new JProperty("files", files)))
                .ToList();
        }

        private IList<JObject> getOrganizationManagementStaff(
            int organizationId,
            Dictionary<string,
            Dictionary<string,
            NomValue>> noms,
            Func<int?,
            JObject> getPersonByApexId,
            bool isApprovedOrg)
        {
            return this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.MANAGEMENT_STAFF WHERE REQUEST_ID IN (SELECT ID FROM CAA_DOC.REQUEST WHERE {0} {1})",
                new DbClause("1=1"),
                new DbClause("and APPLICANT_FIRM_ID = {0}", organizationId)
                )
                .Materialize(r => new JObject(
                    new JProperty("part",
                        Utils.ToJObject(new
                        {
                            __oldId = r.Field<int>("ID"),
                            __migrTable = "MANAGEMENT_STAFF",
                            position = r.Field<string>("POSITION"),
                            person = getPersonByApexId((int?)r.Field<decimal?>("PERSON_ID")),
                            testDate = r.Field<DateTime?>("TEST_DATE"),
                            testScore = noms["testScores"].ByCode(r.Field<string>("TEST_SCORE")),
                            number = r.Field<decimal?>("REQUEST_ID"),
                            valid = noms["boolean"].ByCode(r.Field<string>("VALID_YN") == "Y" ? "Y" : "N")
                        })),
                        new JProperty("files",
                            new JArray(
                                new JObject(
                                    new JProperty("isAdded", true),
                                    new JProperty("file", null),
                                    new JProperty("caseType", isApprovedOrg ?
                                        Utils.ToJObject(noms["organizationCaseTypes"].ByAlias("approvedOrg")) :
                                        Utils.ToJObject(noms["organizationCaseTypes"].ByAlias("others"))), 
                                    new JProperty("bookPageNumber", null),
                                    new JProperty("pageCount", null),
                                    new JProperty("applications", new JArray()))))))
                .ToList();
        }

        private IList<JObject> getOrganizationStaffExaminers(int organizationId, Dictionary<string, Dictionary<string, NomValue>> noms, Func<int?, JObject> getPersonByApexId)
        {
            var approvedAircrafts = this.oracleConn.CreateStoreCommand(
                @"SELECT EAA.ID,
                        EAA.ID_EXAMINER,
                        EAA.ID_AC_GROUP,
                        EAA.DATE_APPROVED,
                        EAA.ID_EXAMINER_APPROVED,
                        EAA.VALID_YN,
                        EAA.NOTES,
                        E.PERSON_ID
                    FROM CAA_DOC.EXAMINER_AC_APPROVED EAA
                    JOIN CAA_DOC.EXAMINER E ON E.ID = EAA.ID_EXAMINER_APPROVED
                    WHERE {0}",
                new DbClause("EAA.ID_FIRM = {0}", organizationId)
                )
                .Materialize(r =>
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "EXAMINER_AC_APPROVED",

                        __ID_EXAMINER = r.Field<int>("ID_EXAMINER"),

                        aircraftTypeGroup = noms["aircraftTypeGroups"].ByOldId(r.Field<int>("ID_AC_GROUP").ToString()),
                        inspector = getPersonByApexId(r.Field<int>("PERSON_ID")),
                        dateApproved = r.Field<DateTime>("DATE_APPROVED"),
                        notes = r.Field<string>("NOTES"),
                        valid = noms["boolean"].ByCode(r.Field<string>("VALID_YN") == "Y" ? "Y" : "N")
                    })
                .GroupBy(r => r.__ID_EXAMINER)
                .ToDictionary(g => g.Key, g => g.Select(r => Utils.ToJObject(
                    new
                    {
                        r.__oldId,
                        r.__migrTable,

                        r.aircraftTypeGroup,
                        r.inspector,
                        r.dateApproved,
                        r.notes,
                        r.valid
                    }))
                    .ToArray());

            JArray files = new JArray(){
                noms["organizationCaseTypes"].Values.Select(c => 
                    Utils.ToJObject(new
                    {
                        isAdded = true,
                        file = (object)null,
                        caseType = Utils.ToJObject(c),
                        bookPageNumber = (string)null,
                        pageCount = (int?)null,
                        applications =  new JArray()
                    }))
            };

            return this.oracleConn.CreateStoreCommand(
                @"SELECT ID,
                        EXAMINER_CODE,
                        VALID_YN,
                        PERSON_ID,
                        STAMP_NUM,
                        PERMITED_AW,
                        PERMITED_CHECK
                    FROM CAA_DOC.EXAMINER
                    WHERE CAA_ID IS NULL {0}",
                new DbClause("AND ID_FIRM = {0}", organizationId)
                )
                .Materialize(r => new JObject(
                    new JProperty("part",
                        Utils.ToJObject(new
                        {
                            __oldId = r.Field<int>("ID"),
                            __migrTable = "EXAMINER",

                            examinerCode = r.Field<string>("EXAMINER_CODE"),
                            stampNum = r.Field<string>("STAMP_NUM"),
                            valid = noms["boolean"].ByCode(r.Field<string>("VALID_YN") == "Y" ? "Y" : "N"),
                            person = getPersonByApexId(r.Field<int>("PERSON_ID")),
                            permitedAW = noms["boolean"].ByCode(r.Field<string>("PERMITED_AW") == "Y" ? "Y" : "N"),
                            permitedCheck = noms["boolean"].ByCode(r.Field<string>("PERMITED_CHECK") == "Y" ? "Y" : "N"),
                            approvedAircrafts = approvedAircrafts.ContainsKey(r.Field<int>("ID")) ? approvedAircrafts[r.Field<int>("ID")] : new JObject[0]
                        })),
                        new JProperty("files", files)))
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
