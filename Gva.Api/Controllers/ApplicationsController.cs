using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Common.Json;
using Docs.Api.DataObjects;
using Docs.Api.Enums;
using Docs.Api.Models;
using Docs.Api.Repositories.CorrespondentRepository;
using Docs.Api.Repositories.DocRepository;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Airports;
using Gva.Api.ModelsDO.Equipments;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.AircraftRepository;
using Gva.Api.Repositories.AirportRepository;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.ApplicationStageRepository;
using Gva.Api.Repositories.EquipmentRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Gva.Api.Repositories.PersonRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/apps")]
    [Authorize]
    public class ApplicationsController : GvaLotsController
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IPersonRepository personRepository;
        private IOrganizationRepository organizationRepository;
        private IAircraftRepository aircraftRepository;
        private IAirportRepository airportRepository;
        private IEquipmentRepository equipmentRepository;
        private IDocRepository docRepository;
        private ICorrespondentRepository correspondentRepository;
        private IApplicationRepository applicationRepository;
        private INomRepository nomRepository;
        private IApplicationStageRepository applicationStageRepository;
        private IFileRepository fileRepository;
        private ILotEventDispatcher lotEventDispatcher;
        private UserContext userContext;

        public ApplicationsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IPersonRepository personRepository,
            IOrganizationRepository organizationRepository,
            IAircraftRepository aircraftRepository,
            IAirportRepository airportRepository,
            IEquipmentRepository equipmentRepository,
            IDocRepository docRepository,
            ICorrespondentRepository correspondentRepository,
            IApplicationRepository applicationRepository,
            INomRepository nomRepository,
            IApplicationStageRepository applicationStageRepository,
            IFileRepository fileRepository,
            ILotEventDispatcher lotEventDispatcher)
            : base(applicationRepository, lotRepository, fileRepository, unitOfWork, lotEventDispatcher)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.personRepository = personRepository;
            this.organizationRepository = organizationRepository;
            this.aircraftRepository = aircraftRepository;
            this.airportRepository = airportRepository;
            this.equipmentRepository = equipmentRepository;
            this.docRepository = docRepository;
            this.correspondentRepository = correspondentRepository;
            this.applicationRepository = applicationRepository;
            this.nomRepository = nomRepository;
            this.applicationStageRepository = applicationStageRepository;
            this.lotEventDispatcher = lotEventDispatcher;
            this.fileRepository = fileRepository;
        }

        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            this.userContext = this.Request.GetUserContext();
        }

        [Route("")]
        public IHttpActionResult GetApplications(
            string filter = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? personLin = null,
            string aircraftIcao = null,
            string organizationUin = null,
            int offset = 0, 
            int? limit = null
            )
        {
            var applications = this.applicationRepository.GetApplications(
                lotSetAlias: filter,
                fromDate: fromDate,
                toDate: toDate,
                personLin: personLin,
                aircraftIcao: aircraftIcao,
                organizationUin: organizationUin,
                offset: offset,
                limit: limit);

            return Ok(applications);
        }

        [Route("{id:int}")]
        public IHttpActionResult GetApplication(int id)
        {
            var application = this.unitOfWork.DbContext.Set<GvaApplication>()
                .Include(a => a.Doc)
                .SingleOrDefault(a => a.GvaApplicationId == id);

            if (application == null)
            {
                throw new Exception("Cannot find application with id " + id);
            }

            this.unitOfWork.DbContext.Set<DocFile>()
                .Where(df => df.DocId == application.DocId)
                .Load();

            this.unitOfWork.DbContext.Set<GvaAppLotFile>()
                .Include(af => af.GvaLotFile)
                .Include(af => af.DocFile)
                .Where(af => af.GvaApplicationId == id)
                .Load();

            Set set = this.unitOfWork.DbContext.Set<Lot>()
                .Single(l => l.LotId == application.LotId)
                .Set;

            ApplicationDO returnValue = new ApplicationDO(application, set.Alias, set.SetId);

            if (set.Alias == "Person")
            {
                returnValue.Person = new PersonViewDO(this.personRepository.GetPerson(application.LotId));
            }
            else if (set.Alias == "Organization")
            {
                returnValue.Organization = new OrganizationDO(this.organizationRepository.GetOrganization(application.LotId));
            }
            else if (set.Alias == "Aircraft")
            {
                returnValue.Aircraft = new AircraftViewDO(this.aircraftRepository.GetAircraft(application.LotId));
            }
            else if (set.Alias == "Airport")
            {
                returnValue.Airport = new AirportViewDO(this.airportRepository.GetAirport(application.LotId));
            }
            else if (set.Alias == "Equipment")
            {
                returnValue.Equipment = new EquipmentViewDO(this.equipmentRepository.GetEquipment(application.LotId));
            }

            var appFilesAll = this.unitOfWork.DbContext.Set<GvaAppLotFile>()
                .Include(e => e.GvaLotFile.LotPart.SetPart)
                .Include(e => e.GvaLotFile.GvaCaseType)
                .Include(e => e.GvaLotFile.DocFile)
                .Include(e => e.GvaLotFile.GvaFile)
                .Include(e => e.DocFile)
                .Where(e => e.GvaApplicationId == id)
                .ToList();

            returnValue.AppLotObjects = this.unitOfWork.DbContext.Set<GvaLotObject>()
                .Include(e => e.LotPart.SetPart)
                .Where(e => e.GvaApplicationId == id)
                .ToList()
                .Select(e => new ApplicationLotObjectDO(e))
                .ToList();

            if (application.DocId.HasValue)
            {
                var docRelations = this.docRepository.GetCaseRelationsByDocId(
                    application.DocId.Value,
                    e => e.Doc.DocFiles,
                    e => e.Doc.DocDirection,
                    e => e.Doc.DocType,
                    e => e.Doc.DocStatus,
                    e => e.Doc.DocEntryType);

                List<GvaAppLotFile> appFilesInCase = new List<GvaAppLotFile>();

                foreach (var dr in docRelations)
                {
                    ApplicationDocRelationDO appDocRelation = new ApplicationDocRelationDO(dr);

                    if (dr.Doc.DocEntryType.Alias == "Document")
                    {
                        foreach (var docFile in dr.Doc.DocFiles)
                        {
                            GvaAppLotFile appFileInDoc = appFilesAll.FirstOrDefault(e => e.DocFileId == docFile.DocFileId);
                            if (appFileInDoc != null)
                            {
                                appFilesInCase.Add(appFileInDoc);
                            }

                            appDocRelation.ApplicationLotFiles.Add(new ApplicationLotFileDO(appFileInDoc, docFile));
                        }

                        returnValue.AppDocCase.Add(appDocRelation);
                    }
                }

                returnValue.AppFilesNotInCase = appFilesAll
                    .Except(appFilesInCase)
                    .ToList()
                    .Select(e => new ApplicationLotFileDO(e, null))
                    .ToList();
            }
            else
            {
                returnValue.AppFilesNotInCase = appFilesAll
                    .ToList()
                    .Select(e => new ApplicationLotFileDO(e, null))
                    .ToList();
            }

            return Ok(returnValue);
        }

        [Route("{id}/parts/linkNew")]
        public IHttpActionResult PostCreatePartAndLink(int? id, string setPartAlias, JObject linkNewPart)
        {
            if (!id.HasValue || string.IsNullOrEmpty(setPartAlias))
            {
                return BadRequest();
            }

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();

                GvaApplication application = this.applicationRepository.Find(id.Value);
                Lot lot = this.lotRepository.GetLotIndex(application.LotId);

                SetPart setPart = this.unitOfWork.DbContext.Set<SetPart>().FirstOrDefault(e => e.Alias == setPartAlias);
                string path = setPart.PathRegex.Remove(setPart.PathRegex.IndexOf("\\"), 4).Remove(0, 1) + "*";
                PartVersion partVersion = lot.CreatePart(path, linkNewPart.Value<JObject>("appPart"), userContext);
                lot.Commit(userContext, lotEventDispatcher);

                if (Regex.IsMatch(setPart.Alias, @"\w+(Application)"))
                {
                    application.GvaAppLotPart = partVersion.Part;
                }

                int docFileId = linkNewPart.Get<int>("appFile.docFileId");
                DocFile docFile = this.unitOfWork.DbContext.Set<DocFile>().FirstOrDefault(e => e.DocFileId == docFileId);

                GvaLotFile lotFile = new GvaLotFile()
                {
                    LotPart = partVersion.Part,
                    DocFile = docFile,
                    GvaCaseTypeId = linkNewPart.Get<int>("appFile.caseTypeId"),
                    PageIndex = linkNewPart.Get<string>("appFile.bookPageNumber"),
                    PageIndexInt = this.fileRepository.GetPageIndexInt(linkNewPart.Get<string>("appFile.bookPageNumber")),
                    PageNumber = linkNewPart.Get<int>("appFile.pageCount")
                };

                GvaAppLotFile gvaAppLotFile = new GvaAppLotFile()
                {
                    GvaApplication = application,
                    GvaLotFile = lotFile,
                    DocFile = docFile
                };

                this.applicationRepository.AddGvaLotFile(lotFile);
                this.applicationRepository.AddGvaAppLotFile(gvaAppLotFile);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok();
            }
        }

        [Route("{id}/parts/create")]
        public IHttpActionResult PostCreateDocFilePartAndLink(int? id, int? docId, string setPartAlias, JObject newPart)
        {
            if (!id.HasValue || !docId.HasValue || string.IsNullOrEmpty(setPartAlias))
            {
                return BadRequest();
            }

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();

                GvaApplication application = this.applicationRepository.Find(id.Value);
                Lot lot = this.lotRepository.GetLotIndex(application.LotId);

                SetPart setPart = this.unitOfWork.DbContext.Set<SetPart>().FirstOrDefault(e => e.Alias == setPartAlias);
                string path = setPart.PathRegex.Remove(setPart.PathRegex.IndexOf("\\"), 4).Remove(0, 1) + "*";

                PartVersion partVersion = lot.CreatePart(path, newPart.Value<JObject>("appPart"), userContext);
                lot.Commit(userContext, lotEventDispatcher);

                if (Regex.IsMatch(setPart.Alias, @"\w+(Application)"))
                {
                    application.GvaAppLotPart = partVersion.Part;
                }

                var doc = this.docRepository.Find(docId.Value);
                var docFileTypes = this.unitOfWork.DbContext.Set<DocFileType>().ToList();

                var docFileType = docFileTypes.FirstOrDefault(e => e.Extention == Path.GetExtension(newPart.Get<string>("appFile.file.name")));
                if (docFileType == null)
                {
                    docFileType = docFileTypes.FirstOrDefault(e => e.Alias == "UnknownBinary");
                }

                var docFile = doc.CreateDocFile(
                    newPart.Get<int>("appFile.docFileKindId"),
                    docFileType.DocFileTypeId,
                    null,
                    newPart.Get<string>("appFile.name"),
                    newPart.Get<string>("appFile.file.name"),
                    String.Empty,
                    newPart.Get<Guid>("appFile.file.key"),
                    true,
                    true,
                    userContext);

                GvaLotFile lotFile = new GvaLotFile()
                {
                    LotPart = partVersion.Part,
                    DocFile = docFile,
                    GvaCaseTypeId = newPart.Get<int>("appFile.caseTypeId"),
                    PageIndex = newPart.Get<string>("appFile.bookPageNumber"),
                    PageIndexInt = this.fileRepository.GetPageIndexInt(newPart.Get<string>("appFile.bookPageNumber")),
                    PageNumber = newPart.Get<int>("appFile.pageCount")
                };

                GvaAppLotFile gvaAppLotFile = new GvaAppLotFile()
                {
                    GvaApplication = application,
                    GvaLotFile = lotFile,
                    DocFile = docFile
                };

                this.applicationRepository.AddGvaLotFile(lotFile);
                this.applicationRepository.AddGvaAppLotFile(gvaAppLotFile);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok();
            }
        }

        [Route("{id}/parts/linkExisting")]
        public IHttpActionResult PostLinkPart(int? id, int? docFileId, int? partId)
        {
            if (!id.HasValue || !docFileId.HasValue || !partId.HasValue)
            {
                return BadRequest();
            }

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                GvaApplication application = this.applicationRepository.Find(id.Value);
                DocFile docFile = this.unitOfWork.DbContext.Set<DocFile>().FirstOrDefault(e => e.DocFileId == docFileId.Value);

                GvaLotFile gvaLotFile = this.unitOfWork.DbContext.Set<GvaLotFile>()
                    .Include(e => e.LotPart.SetPart)
                    .FirstOrDefault(e => e.LotPartId == partId);

                if (gvaLotFile != null)
                {
                    if (Regex.IsMatch(gvaLotFile.LotPart.SetPart.Alias, @"\w+(Application)"))
                    {
                        application.GvaAppLotPart = gvaLotFile.LotPart;
                    }

                    GvaAppLotFile gvaAppLotFile = this.unitOfWork.DbContext.Set<GvaAppLotFile>()
                        .Include(e => e.DocFile)
                        .FirstOrDefault(e => e.GvaApplicationId == id && e.GvaLotFileId == gvaLotFile.GvaLotFileId);

                    if (gvaAppLotFile == null)
                    {
                        GvaAppLotFile addGvaAppLotFile = new GvaAppLotFile()
                        {
                            GvaApplication = application,
                            GvaLotFile = gvaLotFile,
                            DocFile = docFile
                        };

                        this.applicationRepository.AddGvaAppLotFile(addGvaAppLotFile);
                    }
                    else if (gvaAppLotFile.DocFile == null)
                    {
                        gvaAppLotFile.DocFile = docFile;
                    }
                }

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok();
            }
        }

        [Route("{id}/docFiles/create")]
        public IHttpActionResult PostCreateDocFile(int id, int docId, DocFileDO[] files)
        {
            UserContext userContext = this.Request.GetUserContext();

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var doc = this.docRepository.Find(docId);
                var docFileTypes = this.unitOfWork.DbContext.Set<DocFileType>().ToList();

                foreach (var file in files.Where(e => e.IsNew && !e.IsDeleted))
                {
                    var docFileType = docFileTypes.FirstOrDefault(e => e.Extention == Path.GetExtension(file.File.Name));

                    if (docFileType == null)
                    {
                        docFileType = docFileTypes.FirstOrDefault(e => e.Alias == "UnknownBinary");
                    }

                    doc.CreateDocFile(file.DocFileKindId, docFileType.DocFileTypeId, file.Name, file.File.Name, String.Empty, file.File.Key, userContext);
                }

                this.unitOfWork.Save();

                transaction.Commit();
            }

            return Ok();
        }

        [Route("create")]
        public IHttpActionResult PostNewApplication(ApplicationNewDO applicationNewDO)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();

                var gvaCorrespondents = this.applicationRepository.GetGvaCorrespondentsByLotId(applicationNewDO.LotId);

                foreach (var corrId in applicationNewDO.Correspondents)
                {
                    if (!gvaCorrespondents.Any(e => e.CorrespondentId == corrId))
                    {
                        GvaCorrespondent gvaCorrespondent = new GvaCorrespondent();
                        gvaCorrespondent.CorrespondentId = corrId;
                        gvaCorrespondent.LotId = applicationNewDO.LotId;
                        gvaCorrespondent.IsActive = true;

                        this.applicationRepository.AddGvaCorrespondent(gvaCorrespondent);
                    }
                }

                DocDirection direction = this.unitOfWork.DbContext.Set<DocDirection>()
                    .SingleOrDefault(d => d.Alias == "Incomming");
                DocEntryType documentEntryType = this.unitOfWork.DbContext.Set<DocEntryType>()
                    .SingleOrDefault(e => e.Alias == "Document");
                DocStatus draftStatus = this.unitOfWork.DbContext.Set<DocStatus>()
                    .SingleOrDefault(s => s.Alias == "Draft");
                DocCasePartType casePartType = this.unitOfWork.DbContext.Set<DocCasePartType>()
                    .SingleOrDefault(pt => pt.Alias == "Public");
                DocSourceType manualSource = this.unitOfWork.DbContext.Set<DocSourceType>().
                    SingleOrDefault(st => st.Alias == "Manual");
                DocFormatType formatType = this.unitOfWork.DbContext.Set<DocFormatType>()
                    .SingleOrDefault(ft => ft.Alias == "Paper");

                NomValue applicationType = this.nomRepository.GetNomValue("applicationTypes", applicationNewDO.ApplicationType.NomValueId);

                Doc newDoc = docRepository.CreateDoc(
                    direction.DocDirectionId,
                    documentEntryType.DocEntryTypeId,
                    draftStatus.DocStatusId,
                    applicationType.Name,
                    casePartType.DocCasePartTypeId,
                    manualSource.DocSourceTypeId,
                    null,
                    applicationType.TextContent.Get<int>("documentTypeId"),
                    formatType.DocFormatTypeId,
                    null,
                    userContext);

                DocCasePartType internalDocCasePartType = this.unitOfWork.DbContext.Set<DocCasePartType>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "public");

                List<DocTypeClassification> docTypeClassifications = this.unitOfWork.DbContext.Set<DocTypeClassification>()
                    .Where(e => e.DocDirectionId == newDoc.DocDirectionId && e.DocTypeId == newDoc.DocTypeId)
                    .ToList();

                ElectronicServiceStage electronicServiceStage = this.unitOfWork.DbContext.Set<ElectronicServiceStage>()
                    .SingleOrDefault(e => e.DocTypeId == newDoc.DocTypeId && e.IsFirstByDefault);

                List<DocTypeUnitRole> docTypeUnitRoles = this.unitOfWork.DbContext.Set<DocTypeUnitRole>()
                    .Where(e => e.DocDirectionId == newDoc.DocDirectionId && e.DocTypeId == newDoc.DocTypeId)
                    .ToList();

                DocUnitRole importedBy = this.unitOfWork.DbContext.Set<DocUnitRole>()
                    .SingleOrDefault(e => e.Alias == "ImportedBy");

                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>()
                    .FirstOrDefault(e => e.UserId == userContext.UserId);

                newDoc.CreateDocProperties(
                        null,
                        internalDocCasePartType.DocCasePartTypeId,
                        docTypeClassifications,
                        null,
                        electronicServiceStage,
                        docTypeUnitRoles,
                        importedBy,
                        unitUser,
                        applicationNewDO.Correspondents,
                        null,
                        this.userContext);

                this.docRepository.GenerateAccessCode(newDoc, userContext);

                this.unitOfWork.Save();

                this.docRepository.ExecSpSetDocTokens(docId: newDoc.DocId);
                this.docRepository.ExecSpSetDocUnitTokens(docId: newDoc.DocId);

                this.docRepository.RegisterDoc(newDoc, unitUser, userContext);

                var lot = this.lotRepository.GetLotIndex(applicationNewDO.LotId);

                var applicationJson = new
                {
                    documentNumber = newDoc.RegIndex,
                    documentDate = newDoc.RegDate.Value.ToString("yyyy-MM-ddTHH:mm:ss"),
                    applicationType = new
                    {
                        nomValueId = applicationNewDO.ApplicationType.NomValueId,
                        nomId = applicationNewDO.ApplicationType.NomId,
                        name = applicationNewDO.ApplicationType.Name,
                        nameAlt = applicationNewDO.ApplicationType.NameAlt
                    }
                };

                PartVersion partVersion = lot.CreatePart(applicationNewDO.SetPartPath + "/*", JObject.FromObject(applicationJson), userContext);

                lot.Commit(userContext, lotEventDispatcher);

                GvaApplication newGvaApplication = new GvaApplication()
                {
                    LotId = applicationNewDO.LotId,
                    Doc = newDoc,
                    GvaAppLotPart = partVersion.Part
                };

                applicationRepository.AddGvaApplication(newGvaApplication);

                GvaLotFile lotFile = new GvaLotFile()
                {
                    LotPart = partVersion.Part,
                    DocFile = null,
                    GvaCaseTypeId = applicationNewDO.CaseTypeId,
                    PageIndex = null,
                    PageIndexInt = null,
                    PageNumber = null
                };

                GvaAppLotFile gvaAppLotFile = new GvaAppLotFile()
                {
                    GvaApplication = newGvaApplication,
                    GvaLotFile = lotFile,
                    DocFile = null
                };

                this.applicationRepository.AddGvaLotFile(lotFile);
                this.applicationRepository.AddGvaAppLotFile(gvaAppLotFile);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new
                {
                    LotId = lot.LotId,
                    GvaApplicationId = newGvaApplication.GvaApplicationId,
                    PartIndex = partVersion.Part.Index
                });
            }
        }

        [Route(@"appPart/{lotId}/{*path:regex(^aircraftDocumentApplications/\d+$)}"),
         Route(@"appPart/{lotId}/{*path:regex(^airportDocumentApplications/\d+$)}"),
         Route(@"appPart/{lotId}/{*path:regex(^equipmentDocumentApplications/\d+$)}"),
         Route(@"appPart/{lotId}/{*path:regex(^organizationDocumentApplications/\d+$)}"),
         Route(@"appPart/{lotId}/{*path:regex(^personDocumentApplications/\d+$)}")]
        public IHttpActionResult GetApplicationPart(string path, int lotId)
        {
            return base.GetFilePart(lotId, path, null);
        }

        [Route(@"appPart/{lotId}/{*path:regex(^aircraftDocumentApplications/\d+$)}"),
         Route(@"appPart/{lotId}/{*path:regex(^airportDocumentApplications/\d+$)}"),
         Route(@"appPart/{lotId}/{*path:regex(^equipmentDocumentApplications/\d+$)}"),
         Route(@"appPart/{lotId}/{*path:regex(^organizationDocumentApplications/\d+$)}"),
         Route(@"appPart/{lotId}/{*path:regex(^personDocumentApplications/\d+$)}")]
        public IHttpActionResult PostApplicationPart(string path, int lotId, JObject application)
        {
            return base.PostPart(lotId, path, application);
        }

        [Route("link")]
        public IHttpActionResult PostLinkApplication(ApplicationLinkDO applicationLinkDO)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                GvaApplication application = new GvaApplication()
                {
                    LotId = applicationLinkDO.LotId,
                    DocId = applicationLinkDO.DocId
                };

                applicationRepository.AddGvaApplication(application);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new
                {
                    applicationId = application.GvaApplicationId,
                    docId = application.DocId,
                });
            }
        }

        [Route("notLinkedDocs")]
        public IHttpActionResult GetNotLinkedDocs(
            int limit = 10,
            int offset = 0,
            string filter = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            string regUri = null,
            string docName = null,
            int? docTypeId = null,
            int? docStatusId = null,
            string corrs = null,
            string units = null
            )
        {
            //? hot fix: load fist 1000 docs, so the paging with datatable will work
            limit = 1000;
            offset = 0;

            UserContext userContext = this.Request.GetUserContext();

            UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == userContext.UserId);
            ClassificationPermission readPermission = this.unitOfWork.DbContext.Set<ClassificationPermission>().SingleOrDefault(e => e.Alias == "Read");
            DocCasePartType docCasePartType = this.unitOfWork.DbContext.Set<DocCasePartType>().SingleOrDefault(e => e.Alias == "Control");
            List<DocStatus> docStatuses = this.unitOfWork.DbContext.Set<DocStatus>().Where(e => e.IsActive).ToList();

            int totalCount = 0;
            DocView docView = DocView.Normal;
            List<Doc> docs = this.docRepository.GetCurrentCaseDocs(
                fromDate,
                toDate,
                regUri,
                docName,
                docTypeId,
                docStatusId,
                false,
                true,
                corrs,
                units,
                null,
                limit,
                offset,
                docCasePartType,
                docStatuses,
                readPermission,
                unitUser,
                out totalCount);

            var gvaApplciations = applicationRepository.GetLinkedToDocsApplications().ToList();

            docs = docs.Where(e => !gvaApplciations.Any(a => a.DocId.Value == e.DocId)).ToList();

            List<DocListItemDO> returnValue = docs.Select(e => new DocListItemDO(e, unitUser)).ToList();

            foreach (var item in returnValue)
            {
                var docCorrespondents = this.unitOfWork.DbContext.Set<DocCorrespondent>()
                    .Include(e => e.Correspondent.CorrespondentType)
                    .Where(e => e.DocId == item.DocId)
                    .ToList();

                item.DocCorrespondents.AddRange(docCorrespondents.Select(e => new DocCorrespondentDO(e)).ToList());
            }

            StringBuilder sb = new StringBuilder();

            if (totalCount >= 10000)
            {
                sb.Append("Има повече от 10000 резултата, моля, въведете допълнителни филтри.");
            }

            return Ok(new
            {
                docView = docView.ToString(),
                documents = returnValue,
                documentCount = totalCount,
                msg = sb.ToString()
            });
        }

        [Route("docFile")]
        [HttpGet]
        public IHttpActionResult GetDocFile(int? docFileId = null)
        {
            if (docFileId == null)
            {
                return BadRequest();
            }

            DocFile docFile = this.unitOfWork.DbContext.Set<DocFile>().Find(docFileId.Value);

            DocFileDO returnValue = new DocFileDO(docFile);

            return Ok(returnValue);
        }

        [Route("doc")]
        [HttpGet]
        public IHttpActionResult GetDoc(int? docId = null)
        {
            if (docId == null)
            {
                return BadRequest();
            }

            Doc doc = this.unitOfWork.DbContext.Set<Doc>().Find(docId.Value);

            return Ok(new { documentNumber = doc.RegUri });
        }

        [Route("appByDocId")]
        [HttpGet]
        public IHttpActionResult GetApplicationByDocId(int? docId = null)
        {
            if (docId == null)
            {
                return BadRequest();
            }

            var application = this.applicationRepository.GetGvaApplicationByDocId(docId.Value);

            if (application != null)
            {
                return Ok(new { id = application.GvaApplicationId });
            }

            return Ok();
        }

        [Route("personDocumentValues")]
        [HttpGet]
        public IHttpActionResult GetPersonDocumentValues(int docFileId)
        {
            var docFile = this.unitOfWork.DbContext.Set<DocFile>()
                .Include(e => e.DocFileOriginType)
                .SingleOrDefault(e => e.DocFileId == docFileId);

            if (docFile != null && docFile.DocFileOriginType != null && docFile.DocFileOriginType.Alias == "EApplicationAttachedFile" && docFile.Name == "Копие от личната карта")
            {
                return Ok(new
                {
                    Values = new
                    {
                        DocumentNumber = "1234",
                        DocumentDateValidFrom = new DateTime(2014, 4, 15),
                        DocumentPublisher = "МВР"
                    }
                });
            }

            return Ok();
        }

        [Route("getGvaCorrespodents")]
        [HttpGet]
        public IHttpActionResult GetGvaCorrespodents(int lotId)
        {
            var gvaCorrespodents = this.applicationRepository.GetGvaCorrespondentsByLotId(lotId);

            if (gvaCorrespodents.Any())
            {
                return Ok(new { corrs = gvaCorrespodents.Select(e => e.CorrespondentId).ToList() });
            }

            return Ok(new { corrs = new int[0] });
        }

        [Route("{appId}/stages")]
        [HttpGet]
        public IHttpActionResult GetApplicationStages(int appId)
        {
            var applicationStages = this.applicationStageRepository.GetApplicationStages(appId);

            return Ok(applicationStages.Select(a => new ApplicationStageDO(a)));
        }

        [Route("{appId}/stages/{stageId}")]
        [HttpGet]
        public IHttpActionResult GetApplicationStage(int appId, int stageId)
        {
            return Ok(new ApplicationStageDO(this.applicationStageRepository.GetApplicationStage(appId, stageId)));
        }

        [Route("{appId}/stages")]
        [HttpPost]
        public IHttpActionResult PostNewApplicationStage(int appId, JObject appStage)
        {
            GvaApplicationStage stage = new GvaApplicationStage()
            {
                GvaApplicationId = appId,
                GvaStageId = appStage.Get<int>("stageId"),
                StartingDate = appStage.Get<DateTime>("date"),
                InspectorLotId = appStage.Get<int?>("inspectorId"),
                Ordinal = appStage.Get<int>("ordinal")
            };

            var applicationStage = this.unitOfWork.DbContext.Set<GvaApplicationStage>().Add(stage);

            this.unitOfWork.Save();

            return Ok(applicationStage);
        }

        [Route("{appId}/stages/{stageId}")]
        [HttpPost]
        public IHttpActionResult PostApplicationStage(int appId, int stageId, JObject appStage)
        {
            var applicationStage = this.unitOfWork.DbContext.Set<GvaApplicationStage>().Find(stageId);
            if (applicationStage != null)
            {
                applicationStage.GvaStageId = appStage.Get<int>("stageId");
                applicationStage.StartingDate = appStage.Get<DateTime>("date");
                applicationStage.InspectorLotId = appStage.Get<int?>("inspectorId");
                applicationStage.Ordinal = appStage.Get<int>("ordinal");
            }

            this.unitOfWork.Save();

            return Ok(applicationStage);
        }

        [Route("{appId}/stages/{stageId}")]
        [HttpDelete]
        public IHttpActionResult DeleteApplicationStage(int appId, int stageId)
        {
            var appStage = this.unitOfWork.DbContext.Set<GvaApplicationStage>()
                .Where(e => e.GvaAppStageId == stageId)
                .SingleOrDefault();

            var applicationStage = this.unitOfWork.DbContext.Set<GvaApplicationStage>()
                .Remove(appStage);

            this.unitOfWork.Save();

            return Ok(applicationStage);
        }
    }
}