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
using Gva.Api.CommonUtils;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Gva.Api.Repositories.PersonRepository;
using Gva.MigrationTool.Nomenclatures;
using Gva.MigrationTool.Sets.Common;
using Newtonsoft.Json.Linq;
using Oracle.ManagedDataAccess.Client;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.MigrationTool.Sets
{
    public class AircraftApexLotMigrator : IDisposable
    {
        private bool disposed = false;
        private Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, IUserRepository, IFileRepository, IApplicationRepository, IPersonRepository, IOrganizationRepository, ICaseTypeRepository, ILotEventDispatcher, UserContext>>> dependencyFactory;
        private OracleConnection oracleConn;

        public AircraftApexLotMigrator(
            OracleConnection oracleConn,
            Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, IUserRepository, IFileRepository, IApplicationRepository, IPersonRepository, IOrganizationRepository, ICaseTypeRepository, ILotEventDispatcher, UserContext>>> dependencyFactory)
        {
            this.dependencyFactory = dependencyFactory;
            this.oracleConn = oracleConn;
        }

        public void StartMigrating(
            //input constants
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, int> personIdToLotId,
            Dictionary<int, int> aircraftApexIdtoLotId,
            Func<int?, JObject> getPersonByApexId,
            Func<int?, JObject> getOrgByApexId,
            Dictionary<int, string> blobIdsToFileKeys,
            //intput
            ConcurrentQueue<int> aircraftApexIds,
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

            int aircraftApexId;
            while (aircraftApexIds.TryDequeue(out aircraftApexId))
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

                        int lotId;
                        if (!aircraftApexIdtoLotId.TryGetValue(aircraftApexId, out lotId))
                        {
                            continue;
                        }

                        var lot = lotRepository.GetLotIndex(lotId, fullAccess: true);

                        Func<string, JObject, PartVersion> addPartWithFiles = (path, content) =>
                        {
                            var pv = lot.CreatePart(path, content.Get<JObject>("part"), context);
                            fileRepository.AddFileReferences(pv.Part, content.GetItems<CaseDO>("files"));
                            return pv;
                        };

                        Dictionary<int, Tuple<GvaApplication, ApplicationNomDO>> applications =
                            new Dictionary<int, Tuple<GvaApplication, ApplicationNomDO>>();
                        var aircraftDocumentApplications = this.getAircraftDocumentApplications(aircraftApexId, noms, blobIdsToFileKeys);
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
                                            ApplicationName = aircraftDocumentApplication.Get<string>("part.applicationType.name"),
                                            ApplicationCode = aircraftDocumentApplication.Get<string>("part.applicationType.code"),
                                            OldDocumentNumber = aircraftDocumentApplication.Get<string>("part.oldDocumentNumber")
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

                        var aircraftDocumentOccurrences = this.getAircraftDocumentOccurrences(aircraftApexId, nomApplications, noms, blobIdsToFileKeys);
                        foreach (var aircraftDocumentOccurrence in aircraftDocumentOccurrences)
                        {
                            addPartWithFiles("documentOccurrences/*", aircraftDocumentOccurrence);
                        }

                        var aircraftDocumentOwners = this.getAircraftDocumentOwners(aircraftApexId, nomApplications, getPersonByApexId, getOrgByApexId, noms, blobIdsToFileKeys);
                        foreach (var aircraftDocumentOwner in aircraftDocumentOwners)
                        {
                            var pv = addPartWithFiles("aircraftDocumentOwners/*", aircraftDocumentOwner);
                        }

                        var aircraftDocumentOthers = this.getAircraftDocumentOthers(aircraftApexId, nomApplications, noms, blobIdsToFileKeys);
                        foreach (var aircraftDocumentOther in aircraftDocumentOthers)
                        {
                            addPartWithFiles("aircraftDocumentOthers/*", aircraftDocumentOther);
                        }

                        Dictionary<int, int> inspections = new Dictionary<int, int>();

                        var aircraftInspections = this.getAircraftInspections(aircraftApexId, nomApplications, getPersonByApexId, blobIdsToFileKeys, noms);
                        foreach (var aircraftInspection in aircraftInspections)
                        {
                            var pv = addPartWithFiles("inspections/*", aircraftInspection);
                            inspections.Add(aircraftInspection.Get<int>("part.__oldId"), pv.Part.Index);
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
                catch (Exception)
                {
                    Console.WriteLine("Error in APEX aircraftId: {0}", aircraftApexId);

                    cts.Cancel();
                    throw;
                }
            }
        }

        private IList<JObject> getAircraftDocumentApplications(
            int aircraftId,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, string> blobIdsToFileKeys)
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

                        oldDocumentNumber = r.Field<string>("DOC_NO"),
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

                                    "oldDocumentNumber",
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


        private IList<JObject> getAircraftDocumentOccurrences(
            int aircraftId,
            Dictionary<int, JObject> nomApplications,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, string> blobIdsToFileKeys)
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

                        key = blobIdsToFileKeys[r.Field<int>("DOC_ID")],
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

        private IList<JObject> getAircraftDocumentOwners(
            int aircraftId,
            Dictionary<int, JObject> nomApplications,
            Func<int?, JObject> getPersonByApexId,
            Func<int?, JObject> getOrgByApexId,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, string> blobIdsToFileKeys)
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

                        key = blobIdsToFileKeys[r.Field<int>("DOC_ID")],
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

        private IList<JObject> getAircraftDocumentOthers(
            int aircraftId,
            Dictionary<int, JObject> nomApplications,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, string> blobIdsToFileKeys)
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

                        key = blobIdsToFileKeys[r.Field<int>("DOC_ID")],
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

        private IList<JObject> getAircraftInspections(
            int aircraftId,
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

            var inspectors = this.oracleConn.CreateStoreCommand(
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

            var controlCards = this.oracleConn.CreateStoreCommand(
                @"SELECT A.ID,
                        D.DOC_ID,
                        D.MIME_TYPE,
                        D.NAME
                    FROM CAA_DOC.AUDITS A
                    LEFT OUTER JOIN CAA_DOC.DOCLIB_DOCUMENTS D ON A.ID + 80000000 = D.DOC_ID
                    WHERE {0}",
                new DbClause("A.ID_AIRCRAFT = {0}", aircraftId)
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
                new DbClause("ID_AIRCRAFT = {0}", aircraftId)
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
                            inspectors = inspectors[r.Field<int>("ID")],

                            inspectionFrom = r.Field<DateTime?>("INSPECTION_FROM"),
                            inspectionTo = r.Field<DateTime?>("INSPECTION_TO")
                        })),
                        new JProperty("files",
                            new JArray(
                                new JObject(
                                    new JProperty("isAdded", true),
                                    new JProperty("file", null),
                                    new JProperty("caseType", Utils.ToJObject(noms["aircraftCaseTypes"].ByAlias("aircraft"))),
                                    new JProperty("bookPageNumber", null),
                                    new JProperty("pageCount", null),
                                    new JProperty("applications",
                                        (r.Field<decimal?>("ID_REQUEST") != null && nomApplications.ContainsKey(r.Field<int>("ID_REQUEST"))) ?
                                        new object[] { nomApplications[r.Field<int>("ID_REQUEST")] } :
                                        new object[0]))))))
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
