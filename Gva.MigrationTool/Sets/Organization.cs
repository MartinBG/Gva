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
using Gva.Api.Repositories.AircraftRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Autofac.Features.OwnedInstances;
using Common.Tests;
using Gva.Api.Repositories.PersonRepository;

namespace Gva.MigrationTool.Sets
{
    public class Organization
    {
        private Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, IUserRepository, IFileRepository, IApplicationRepository, IPersonRepository, IAircraftRepository, ILotEventDispatcher, ICaseTypeRepository, UserContext>>> dependencyFactory;
        private OracleConnection oracleConn;

        public Organization(OracleConnection oracleConn,
             Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, IUserRepository, IFileRepository, IApplicationRepository, IPersonRepository, IAircraftRepository, ILotEventDispatcher, ICaseTypeRepository, UserContext>>> dependencyFactory)
        {
            this.dependencyFactory = dependencyFactory;
            this.oracleConn = oracleConn;
        }

        public Tuple<Dictionary<int, int>, Dictionary<string, int>, Dictionary<string, int>, Dictionary<int, JObject>> createOrganizationsLots(Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            Dictionary<int, int> orgIdToLotId = new Dictionary<int, int>();
            Dictionary<string, int> orgNamesEnToLotId = new Dictionary<string, int>();
            Dictionary<string, int> orgUinToLotId = new Dictionary<string, int>();
            Dictionary<int, JObject> orgLotIdToOrgNom = new Dictionary<int, JObject>();
            var organizationIds = this.getOrganizationIds();

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

                unitOfWork.DbContext.Configuration.AutoDetectChangesEnabled = false;

                foreach (var organizationId in organizationIds)
                {
                    var lot = lotRepository.CreateLot("Organization", context);

                    var organizationData = this.getOrganizationData(organizationId, noms);
                    caseTypeRepository.AddCaseTypes(lot, organizationData.GetItems<JObject>("caseTypes"));

                    lot.CreatePart("organizationData", organizationData, context);
                    lot.Commit(context, lotEventDispatcher);
                    unitOfWork.Save();
                    Console.WriteLine("Created organizationData part for organization with id {0}", organizationId);

                    int lotId = lot.LotId;
                    orgIdToLotId.Add(organizationId, lotId);

                    string name = organizationData.Get<string>("name");
                    string nameAlt = organizationData.Get<string>("nameAlt");

                    orgNamesEnToLotId.Add(nameAlt, lotId);

                    if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(nameAlt))
                    {
                        throw new Exception("No name or nameAlt");// all orgs should have nameAlt
                    }

                    string uin = organizationData.Get<string>("uin");
                    if (!string.IsNullOrWhiteSpace(uin))
                    {
                        orgUinToLotId.Add(uin, lotId);
                    }

                    orgLotIdToOrgNom.Add(lotId, Utils.ToJObject(
                        new
                        {
                            nomValueId = lotId,
                            name = name,
                            nameAlt = nameAlt
                        }));
                }
            }

            return Tuple.Create(orgIdToLotId, orgNamesEnToLotId, orgUinToLotId, orgLotIdToOrgNom);
        }

        public void migrateOrganizations(
            Dictionary<string, Dictionary<string, NomValue>> noms,
            Dictionary<int, int> orgApexIdToLotId,
            Func<int?, JObject> getAircraftByApexId,
            Func<int?, JObject> getPersonByApexId)
        {
            foreach (var organizationId in this.getOrganizationIds())
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

                    var lot = lotRepository.GetLotIndex(orgApexIdToLotId[organizationId]);

                    Func<string, JObject, PartVersion> addPartWithFiles = (path, content) =>
                    {
                        var pv = lot.CreatePart(path, content.Get<JObject>("part"), context);
                        fileRepository.AddFileReferences(pv, content.GetItems<FileDO>("files"));
                        return pv;
                    };

                    Dictionary<int, Tuple<GvaApplication, ApplicationNomDO>> applications =
                        new Dictionary<int, Tuple<GvaApplication, ApplicationNomDO>>();
                    var aircraftDocumentApplications = this.getOrganizationDocumentApplications(organizationId, noms);
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
                    }

                    //Dictionary<int, int> inspections = new Dictionary<int, int>();

                    var organizationInspections = this.getOrganizationInspections(organizationId, nomApplications, getPersonByApexId, noms);
                    foreach (var organizationInspection in organizationInspections)
                    {
                        var pv = lot.CreatePart("organizationInspections/*", organizationInspection, context);
                        //inspections.Add(aircraftInspection["__oldId"].Value<int>(), pv.Part.Index.Value);
                    }

                    var organizationAddresses = this.getOrganizationAddress(organizationId, noms);
                    foreach (var organizationAddress in organizationAddresses)
                    {
                        lot.CreatePart("organizationAddresses/*", organizationAddress, context);
                    }

                    Dictionary<int, JObject> organizationDocuments = new Dictionary<int, JObject>();

                    var organizationDocumentOthers = this.getOrganizationDocumentOthers(organizationId, nomApplications, noms);
                    foreach (var organizationDocumentOther in organizationDocumentOthers)
                    {
                        var pv = addPartWithFiles("organizationDocumentOthers/*", organizationDocumentOther);
                        organizationDocuments.Add(organizationDocumentOther["part"]["__oldId"].Value<int>(), pv.Content);
                    }

                    var organizationAuditPlans = this.getOrganizationAuditPlan(organizationId, noms);
                    foreach (var organizationAuditPlan in organizationAuditPlans)
                    {
                        lot.CreatePart("organizationAuditplans/*", organizationAuditPlan, context);
                    }

                    var organizationApprovals = this.getOrganizationApproval(organizationId, noms, organizationDocuments, getPersonByApexId, nomApplications);
                    foreach (var organizationApproval in organizationApprovals)
                    {
                        lot.CreatePart("organizationApprovals/*", organizationApproval, context);
                    }

                    //var organizationRecommendations = this.getOrganizationRecommendation(organizationId, noms, organizationDocuments, getPerson, nomApplications);
                    //foreach (var organizationRecommendation in organizationRecommendations)
                    //{
                    //    lot.CreatePart("organizationRecommendations/*", organizationRecommendation, context);
                    //}


                    var organizationManagementStaffs = this.getOrganizationManagementStaff(organizationId, noms, getPersonByApexId);
                    foreach (var organizationManagementStaff in organizationManagementStaffs)
                    {
                        lot.CreatePart("organizationStaffManagement/*", organizationManagementStaff, context);
                    }

                    var organizationStaffExaminers = this.getOrganizationStaffExaminers(organizationId, noms, getPersonByApexId);
                    foreach (var organizationStaffExaminer in organizationStaffExaminers)
                    {
                        if (organizationStaffExaminer.Get<int?>("person.nomValueId") == null)
                        {
                            Console.WriteLine("CANNOT FIND PERSON FOR organizationStaffExaminer WITH oldId " + organizationStaffExaminer.Get<int>("__oldId"));
                            continue;
                        }

                        lot.CreatePart("organizationStaffExaminers/*", organizationStaffExaminer, context);
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
        }

        private IList<int> getOrganizationIds()
        {
            return this.oracleConn.CreateStoreCommand("SELECT ID FROM CAA_DOC.FIRM")
                .Materialize(r => r.Field<int>("ID"))
                .Where(id => new int[] { 206, 317, 367, 447, 467, 561, 563, 565, 567, 568, 742, 807, 833, 1432 }.Contains(id))
                .ToList();
        }

        private JObject getOrganizationData(int organizationId, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            return this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.FIRM WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID = {0}", organizationId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "FIRM",
                        caseTypes = new[] //TODO
                        {
                            Utils.DUMMY_APPROVED_ORG_CASE_TYPE
                        },
                        name = r.Field<string>("NAME"),
                        nameAlt = r.Field<string>("NAME_TRANS"),
                        code = r.Field<string>("CODE"),
                        uin = r.Field<string>("BULSTAT"),
                        CAO = r.Field<string>("CAO"),
                        dateСАОFirstIssue = r.Field<DateTime?>("CAO_ISS"),
                        dateСАОLastIssue = r.Field<DateTime?>("CAO_LAST"),
                        dateСАОValidTo = r.Field<DateTime?>("CAO_VALID"),
                        ICAO = r.Field<string>("ICAO"),
                        IATA = r.Field<string>("IATA"),
                        SITA = r.Field<string>("SITA"),
                        organizationType = noms["organizationTypes"].ByOldId(r.Field<long?>("ID_FIRM_TYPE").ToString()),
                        organizationKind = noms["organizationKinds"].ByCode(r.Field<string>("TYPE_ORG")),
                        phones = r.Field<string>("PHONES"),
                        webSite = r.Field<string>("WEB_SITE"),
                        Note = r.Field<string>("REMARKS"),
                        valid = noms["boolean"].ByCode(r.Field<string>("VALID_YN") == "Y" ? "Y" : "N"),
                        dateValidTo = r.Field<DateTime?>("VALID_TO_DATE"),
                        docRoom = r.Field<string>("DOC_ROOM"),
                    }))
                .Single();
        }

        private IList<JObject> getOrganizationAddress(int organizationId, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            return this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.FIRM_ADDRESS WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and FIRM_ID = {0}", organizationId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
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
                    }))
                .ToList();
        }

        //TODO
        private IList<JObject> getOrganizationApproval(
            int organizationId, Dictionary<string,
            Dictionary<string, NomValue>> noms,
            Dictionary<int, JObject> orgDocuments,
            Func<int?, JObject> getPersonByApexId,
            Dictionary<int, JObject> nomApplications)
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
                                r.Field<long?>("ID_SCH_147") == null ? r.Field<long?>("ID_SCH_MG") == null ? null : limMG[r.Field<long?>("ID_SCH_MG")] : lim147[r.Field<long?>("ID_SCH_147")] :
                                lim145[r.Field<long?>("ID_SCH_145")],
                        linkedDocument = orgDocuments.ContainsKey(r.Field<int>("PERSON_DOCUMENT_ID")) ? orgDocuments[r.Field<int>("PERSON_DOCUMENT_ID")] : null,
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
                        n.linkedDocument
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

            var amendmentsResults = this.oracleConn.CreateStoreCommand(
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
                            new JArray () { nomApplications[r.Field<int>("ID_REQUEST")] } :
                            null

                    })
                .GroupBy(g => g.__approvalId)
                .ToDictionary(a => a.Key, a => a.Select(n =>
                    new
                    {
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
                        n.applications
                    }).ToArray());

            return this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.APPROVAL WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_FIRM = {0}", organizationId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "APPROVAL",
                        approvalType = noms["approvalTypes"].ByCode(r.Field<string>("APPROVAL_TYPE")),
                        documentNumber = r.Field<string>("REFERENCE"),
                        documentDateIssue = r.Field<DateTime?>("ISSUE_DATE"),
                        approvalState = noms["approvalStates"].ByCode(r.Field<string>("STATE")),
                        approvalStateDate = r.Field<DateTime?>("STATE_DATE"),
                        approvalStateNote = r.Field<string>("STATE_REMARKS"),
                        amendments = amendmentsResults.ContainsKey(r.Field<int>("ID")) ? amendmentsResults[r.Field<int>("ID")] : null

                    }))
                .ToList();
        }

        private IList<JObject> getOrganizationInspections(
            int organizationId,
            Dictionary<int, JObject> nomApplications,
            Func<int?, JObject> getPersonByApexId,
            Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            var inspectionDisparities = this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM 
                    CAA_DOC.REC_DISPARITY
                    WHERE ID_AUDIT in (SELECT ID FROM CAA_DOC.AUDITS WHERE {0})",
                    new DbClause("ID_FIRM = {0}", organizationId)
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
                    new DbClause("ID_FIRM = {0}", organizationId)
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
                    new DbClause("ID_FIRM = {0}", organizationId)
                )
                .Materialize(r =>
                    new
                    {
                        sortOrder = r.Field<decimal?>("SEQ").ToString(),
                        examinerId = getPersonByApexId((int?)r.Field<decimal?>("ID_EXAMINER")),
                    })
                .ToArray();

            return this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AUDITS WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_FIRM = {0}", organizationId)
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
                            new JArray() { nomApplications[r.Field<int>("ID_REQUEST")] } :
                            null
                    }))
                .ToList();
        }

        private IList<JObject> getOrganizationAuditPlan(int organizationId, Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            return this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.AUDITS_PLAN WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and FIRM_ID = {0}", organizationId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "AUDITS_PLAN",
                        auditPartRequirementId = noms["auditPartRequirements"].ByOldId(r.Field<long?>("NM_REQUIRM_ID").ToString()),
                        planYear = r.Field<short?>("PLAN_YEAR"),
                        planMonth = r.Field<short?>("PLAN_MONTH")
                    }))
                .ToList();
        }

        private IList<JObject> getOrganizationDocumentApplications(int organizationId, Dictionary<string, Dictionary<string, NomValue>> noms)
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
                                    new JProperty("caseType", Utils.DUMMY_APPROVED_ORG_CASE_TYPE), //TODO
                                    new JProperty("bookPageNumber", bookPageNumber),
                                    new JProperty("pageCount", pageCount),
                                    new JProperty("applications", new JArray()))))))
                    .ToList();
        }

        private IList<JObject> getOrganizationDocumentOthers(
            int organizationId,
            Dictionary<int, JObject> nomApplications,
            Dictionary<string, Dictionary<string, NomValue>> noms)
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

                        key = Utils.DUMMY_FILE_KEY,//TODO
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
                                    new JProperty("caseType", Utils.DUMMY_APPROVED_ORG_CASE_TYPE), //TODO
                                    new JProperty("bookPageNumber", bookPageNumber),
                                    new JProperty("pageCount", pageCount),
                                    new JProperty("applications", new JArray()))))))
                    .ToList();
        }

        //TODO
        private IList<JObject> getOrganizationRecommendation(
            int organizationId,
            Func<int?, JObject> getPerson,
            Dictionary<int, JObject> nomApplications,
            Dictionary<string, Dictionary<string, NomValue>> noms)
        {
            var disparitiesResults = this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.REC_DISPARITY WHERE ID_REC_COMPLIANCE IN ( SELECT ID FROM CAA_DOC.REC_COMPLIANCE WHERE ID_REC IN ( SELECT ID FROM CAA_DOC.RECOMMENDATION WHERE {0} {1}))",
                new DbClause("1=1"),
                new DbClause("and ID_FIRM = {0}", organizationId)
                )
                .Materialize(r =>
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "REC_DISPARITY",
                        sortOrder = r.Field<decimal?>("SEQ"),
                        refNumber = r.Field<string>("REF_NUMBER"),
                        description = r.Field<string>("DESCRIPTION"),
                        disparityLevel = r.Field<decimal?>("REC_DISPRT_LEVEL"),
                        removalDate = r.Field<DateTime?>("REMOVAL_DATE"),
                        rectifyAction = r.Field<string>("RECTIFY_ACTION"),
                        closureDate = r.Field<DateTime?>("CLOSURE_DATE"),
                        closureDocument = r.Field<string>("CLOSURE_DOC")
                    })
                .ToArray();

            var descriptionReviewResults = this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.REC_COMPLIANCE WHERE ID_REC IN ( SELECT ID FROM CAA_DOC.RECOMMENDATION WHERE {0} {1})",
                new DbClause("1=1"),
                new DbClause("and ID_FIRM = {0}", organizationId)
                )
                .Materialize(r =>
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "REC_COMPLIANCE",
                        auditPartSectionDetailId = r.Field<long?>("ID_SECTION"),
                        auditResultId = r.Field<string>("STATE"),
                        disparities = disparitiesResults
                    })
                .ToArray();

            var examiners = this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.REC_AUDITOR WHERE ID_REC IN ( SELECT ID FROM CAA_DOC.RECOMMENDATION WHERE {0} {1})",
                new DbClause("1=1"),
                new DbClause("and ID_FIRM = {0}", organizationId)
                )
                .Materialize(r =>
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "REC_AUDITOR",
                        part = r.Field<string>("PART"),
                        sortOrder = r.Field<decimal?>("SEQ"),
                        examiner = getPerson((int?)r.Field<decimal?>("ID_EXAMINER")),
                    })
                .GroupBy(g => g.part)
                .ToDictionary(d => d.Key, d => d.Select(n =>
                    new
                    {
                        n.part,
                        n.sortOrder,
                        n.examiner
                    }).ToArray());

            var parts = this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.RECOMMENDATION WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and ID_FIRM = {0}", organizationId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "RECOMMENDATION",

                        recommendationPartId = r.Field<long?>("ID_PART"),
                        formDate = r.Field<DateTime?>("FORM_DATE"),
                        formText = r.Field<string>("FORM_TEXT"),
                        interviewedStaff = r.Field<string>("INTERVIEWED_STAFF"),
                        fromDate = r.Field<DateTime?>("NADZOR_FROM"),
                        toDate = r.Field<DateTime?>("NADZOR_TO"),
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
                        documentDescription = r.Field<string>("DESCRIPTION_DOC"),
                        recommendation = r.Field<string>("RECOMMENDATION"),
                        part1 = new
                        {
                            examiners = examiners.ContainsKey("1") ? examiners["1"] : null
                        },
                        part2 = new
                        {
                            examiners = examiners.ContainsKey("2") ? examiners["2"] : null
                        },
                        part3 = new
                        {
                            examiners = examiners.ContainsKey("3") ? examiners["3"] : null
                        },
                        part4 = new
                        {
                            examiners = examiners.ContainsKey("4") ? examiners["4"] : null
                        },
                        part5 = new
                        {
                            examiners = examiners.ContainsKey("5") ? examiners["5"] : null
                        },
                        descriptionReview = descriptionReviewResults
                    }))
                .ToList();

            var apps = this.oracleConn.CreateStoreCommand(
                @"SELECT R.ID REQUEST_ID,
                        RCM.ID RCM_ID
                    FROM CAA_DOC.REQUEST R
                    JOIN CAA_DOC.RECOMMENDATION RCM ON RCM.ID_REQUEST = R.ID
                                WHERE {0} {1}",
                new DbClause("1=1"),
                new DbClause("and RCM.ID_FIRM = {0}", organizationId)
                )
                .Materialize(r =>
                    new
                    {
                        recommendationId = r.Field<int>("RCM_ID"),
                        nomApp = nomApplications[r.Field<int>("REQUEST_ID")]
                    })
                .ToList();

            return (from part in parts
                    join app in apps on part.Get<int>("__oldId") equals app.recommendationId into ag
                    select new JObject(
                        new JProperty("part",
                            Utils.Pluck(part,
                                new string[] 
                                {
                                    "__oldId",
                                    "__migrTable",

                                    "recommendationPartId",
                                    "formDate",
                                    "formText",
                                    "interviewedStaff",
                                    "fromDate",
                                    "toDate",
                                    "finished1Date",
                                    "town1",
                                    "finished2Date",
                                    "town2",
                                    "finished3Date",
                                    "town3",
                                    "finished4Date",
                                    "town4",
                                    "finished5Date",
                                    "town5",
                                    "documentDescription",
                                    "recommendation",
                                    "part1",
                                    "part2",
                                    "part3",
                                    "part4",
                                    "part5",
                                    "descriptionReview"
                                })),
                        new JProperty("files",
                            new JArray(
                                new JObject(
                                    new JProperty("isAdded", true),
                                    new JProperty("file", null),
                                    new JProperty("caseType", Utils.DUMMY_APPROVED_ORG_CASE_TYPE), //TODO
                                    new JProperty("bookPageNumber", null),
                                    new JProperty("pageCount", null),
                                    new JProperty("applications", new JArray(ag.Select(a => a.nomApp))))))))
                    .ToList();
        }

        private IList<JObject> getOrganizationManagementStaff(int organizationId, Dictionary<string, Dictionary<string, NomValue>> noms, Func<int?, JObject> getPersonByApexId)
        {
            return this.oracleConn.CreateStoreCommand(
                @"SELECT * FROM CAA_DOC.MANAGEMENT_STAFF WHERE REQUEST_ID IN (SELECT ID FROM CAA_DOC.REQUEST WHERE {0} {1})",
                new DbClause("1=1"),
                new DbClause("and APPLICANT_FIRM_ID = {0}", organizationId)
                )
                .Materialize(r => Utils.ToJObject(
                    new
                    {
                        __oldId = r.Field<int>("ID"),
                        __migrTable = "MANAGEMENT_STAFF",
                        position = r.Field<string>("POSITION"),
                        person = getPersonByApexId((int?)r.Field<decimal?>("PERSON_ID")),
                        testDate = r.Field<DateTime?>("TEST_DATE"),
                        testScore = noms["testScores"].ByCode(r.Field<string>("TEST_SCORE")),
                        number = r.Field<decimal?>("REQUEST_ID"),
                        valid = noms["boolean"].ByCode(r.Field<string>("VALID_YN") == "Y" ? "Y" : "N")
                    }))
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
                .Materialize(r => Utils.ToJObject(
                    new
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
                    }))
                .ToList();
        }
    }
}
