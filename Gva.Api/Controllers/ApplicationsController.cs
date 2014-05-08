using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web.Http;
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
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.PersonRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;
using Gva.Api.Repositories.OrganizationRepository;
using System.Text.RegularExpressions;
using Gva.Api.Repositories.AircraftRepository;
using Gva.Api.Repositories.EquipmentRepository;
using Gva.Api.Repositories.AirportRepository;
using System.IO;

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
            this.lotEventDispatcher = lotEventDispatcher;
        }

        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            this.userContext = this.Request.GetUserContext();
        }

        [Route("")]
        public IHttpActionResult GetApplications(DateTime? fromDate = null, DateTime? toDate = null, string lin = null, int offset = 0, int? limit = null)
        {
            var applications = this.applicationRepository.GetApplications(fromDate: fromDate, toDate: toDate, lin: lin, offset: offset, limit: limit);

            return Ok(applications);
        }

        [Route("{id:int}")]
        public IHttpActionResult GetApplication(int id)
        {
            var application = this.applicationRepository.Find(id,
                e => e.GvaAppLotFiles.Select(f => f.DocFile),
                e => e.GvaAppLotFiles.Select(f => f.GvaLotFile),
                e => e.Lot.Set,
                e => e.Doc.DocFiles);

            if (application == null)
            {
                throw new Exception("Cannot find application with id " + id);
            }

            ApplicationDO returnValue = new ApplicationDO(application);

            if (application.Lot.Set.Alias == "Person")
            {
                returnValue.Person = new PersonDO(this.personRepository.GetPerson(application.LotId));
            }
            else if (application.Lot.Set.Alias == "Organization")
            {
                returnValue.Organization = new OrganizationDO(this.organizationRepository.GetOrganization(application.LotId));
            }
            else if (application.Lot.Set.Alias == "Aircraft")
            {
                returnValue.Aircraft = new AircraftDO(this.aircraftRepository.GetAircraft(application.LotId));
            }
            else if (application.Lot.Set.Alias == "Airport")
            {
                returnValue.Airport = new AirportDO(this.airportRepository.GetAirport(application.LotId));
            }
            else if (application.Lot.Set.Alias == "Equipment")
            {
                returnValue.Equipment = new EquipmentDO(this.equipmentRepository.GetEquipment(application.LotId));
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
                var docRelations = this.docRepository.GetCaseRelationsByDocId(application.DocId.Value,
                    e => e.Doc.DocFiles,
                    e => e.Doc.DocDirection,
                    e => e.Doc.DocType,
                    e => e.Doc.DocStatus,
                    e => e.Doc.DocEntryType
                    );

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
                    PageNumber = linkNewPart.Get<int>("appFile.pageCount")
                };

                GvaAppLotFile gvaAppLotFile = new GvaAppLotFile()
                {
                    GvaApplication = application,
                    GvaLotFile = lotFile,
                    DocFile = docFile
                };

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
                    PageNumber = newPart.Get<int>("appFile.pageCount")
                };

                GvaAppLotFile gvaAppLotFile = new GvaAppLotFile()
                {
                    GvaApplication = application,
                    GvaLotFile = lotFile,
                    DocFile = docFile
                };

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

                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == userContext.UserId);
                DocEntryType documentEntryType = this.unitOfWork.DbContext.Set<DocEntryType>().SingleOrDefault(e => e.Alias == "Document");
                DocStatus draftStatus = this.unitOfWork.DbContext.Set<DocStatus>().SingleOrDefault(e => e.Alias == "Draft");
                DocCasePartType internalDocCasePartType = this.unitOfWork.DbContext.Set<DocCasePartType>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "Public".ToLower());
                DocSourceType manualSource = this.unitOfWork.DbContext.Set<DocSourceType>().SingleOrDefault(e => e.Alias == "Manual");

                var gvaCorrespondents = this.applicationRepository.GetGvaCorrespondentsByLotId(applicationNewDO.LotId);

                foreach (var corrId in applicationNewDO.PreDoc.Correspondents)
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

                Doc newDoc = docRepository.CreateDoc(
                    applicationNewDO.PreDoc.DocDirectionId,
                    documentEntryType.DocEntryTypeId,
                    draftStatus.DocStatusId,
                    applicationNewDO.PreDoc.DocSubject,
                    applicationNewDO.PreDoc.DocCasePartTypeId,
                    manualSource.DocSourceTypeId,
                    null,
                    applicationNewDO.PreDoc.DocTypeId,
                    applicationNewDO.PreDoc.DocFormatTypeId,
                    null,
                    userContext);

                List<DocTypeClassification> docTypeClassifications = this.unitOfWork.DbContext.Set<DocTypeClassification>()
                    .Where(e => e.DocDirectionId == newDoc.DocDirectionId && e.DocTypeId == newDoc.DocTypeId)
                    .ToList();

                List<DocTypeUnitRole> docTypeUnitRoles = this.unitOfWork.DbContext.Set<DocTypeUnitRole>()
                        .Where(e => e.DocDirectionId == newDoc.DocDirectionId && e.DocTypeId == newDoc.DocTypeId)
                        .ToList();

                ElectronicServiceStage electronicServiceStage = this.unitOfWork.DbContext.Set<ElectronicServiceStage>()
                        .SingleOrDefault(e => e.DocTypeId == newDoc.DocTypeId && e.IsFirstByDefault);

                DocUnitRole importedBy = this.unitOfWork.DbContext.Set<DocUnitRole>()
                        .SingleOrDefault(e => e.Alias == "ImportedBy");

                newDoc.CreateDocProperties(
                        null,
                        internalDocCasePartType.DocCasePartTypeId,
                        docTypeClassifications,
                        electronicServiceStage,
                        docTypeUnitRoles,
                        importedBy,
                        unitUser,
                        applicationNewDO.PreDoc.Correspondents,
                        null,
                        this.userContext);

                this.docRepository.GenerateAccessCode(newDoc, userContext);

                this.unitOfWork.Save();

                this.docRepository.spSetDocUsers(newDoc.DocId);

                this.docRepository.RegisterDoc(newDoc, unitUser, userContext);

                GvaApplication newGvaApplication = new GvaApplication()
                {
                    LotId = applicationNewDO.LotId,
                    Doc = newDoc,
                };

                applicationRepository.AddGvaApplication(newGvaApplication);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new
                {
                    applicationId = newGvaApplication.GvaApplicationId,
                    docId = newGvaApplication.DocId,
                });
            }
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
            DocUnitPermission docUnitPermissionRead = this.unitOfWork.DbContext.Set<DocUnitPermission>().SingleOrDefault(e => e.Alias == "Read");
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
                docStatuses,
                docUnitPermissionRead,
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
    }
}