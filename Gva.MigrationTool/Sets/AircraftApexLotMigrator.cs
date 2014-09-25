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
            Dictionary<string, Dictionary<string, NomValue>> noms, Dictionary<int, int> aircraftApexIdtoLotId,
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
                            fileRepository.AddFileReferences(pv.Part, content.GetItems<FileDO>("files"));
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

                        var aircraftDocumentOccurrences = this.getAircraftDocumentOccurrences(aircraftApexId, nomApplications, noms, blobIdsToFileKeys);
                        foreach (var aircraftDocumentOccurrence in aircraftDocumentOccurrences)
                        {
                            addPartWithFiles("documentOccurrences/*", aircraftDocumentOccurrence);
                        }

                        Dictionary<int, int> documentOwnersOldIdToPartIndex = new Dictionary<int, int>();
                        var aircraftDocumentOwners = this.getAircraftDocumentOwners(aircraftApexId, nomApplications, getPersonByApexId, getOrgByApexId, noms, blobIdsToFileKeys);
                        foreach (var aircraftDocumentOwner in aircraftDocumentOwners)
                        {
                            var pv = addPartWithFiles("aircraftDocumentOwners/*", aircraftDocumentOwner);
                            documentOwnersOldIdToPartIndex.Add(aircraftDocumentOwner.Get<int>("part.__oldId"), pv.Part.Index);
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
                            var pv = lot.CreatePart("inspections/*", aircraftInspection.Get<JObject>("part"), context);
                            applicationRepository.AddApplicationRefs(pv.Part, aircraftInspection.GetItems<ApplicationNomDO>("applications"));

                            inspections.Add(aircraftInspection.Get<int>("part.__oldId"), pv.Part.Index);
                        }

                        var aircraftDocumentDebts = this.getAircraftDocumentDebts(aircraftApexId, nomApplications, getPersonByApexId, noms, documentOwnersOldIdToPartIndex, blobIdsToFileKeys);
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
                catch (Exception)
                {
                    cts.Cancel();
                    throw;
                }
            }
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
                        count = r.Field<int>("PART_NUMBER"),
                        aircraftPartStatus = noms["aircraftPartStatuses"].ByOldId(r.Field<string>("NEW_YN").ToString()),
                        manDate = r.Field<DateTime?>("MAN_DATE"),
                        manPlace = r.Field<string>("MAN_PLACE"),
                        description = r.Field<string>("DESCRIPTION"),
                    }))
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
                                controlCard = controlCards[r.Field<int>("ID")],
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

        private IList<JObject> getAircraftDocumentDebts(
            int aircraftId,
            Dictionary<int, JObject> nomApplications,
            Func<int?, JObject> getPersonByApexId,
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, int> documentOwnersOldIdToPartIndex,
            Dictionary<int, string> blobIdsToFileKeys)
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

                        key = blobIdsToFileKeys[r.Field<int>("DOC_ID")],
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
                        export = new
                        {
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
