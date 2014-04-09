using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
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
            IDocRepository docRepository,
            ICorrespondentRepository correspondentRepository,
            IApplicationRepository applicationRepository,
            IFileRepository fileRepository,
            ILotEventDispatcher lotEventDispatcher)
            : base(lotRepository, fileRepository, unitOfWork, lotEventDispatcher)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.personRepository = personRepository;
            this.organizationRepository = organizationRepository;
            this.aircraftRepository = aircraftRepository;
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

            var appFilesAll = this.unitOfWork.DbContext.Set<GvaAppLotFile>()
                .Include(e => e.GvaLotFile.LotPart.SetPart)
                .Include(e => e.GvaLotFile.GvaCaseType)
                .Include(e => e.GvaLotFile.DocFile)
                .Include(e => e.GvaLotFile.GvaFile)
                .Include(e => e.DocFile)
                .Where(e => e.GvaApplicationId == id)
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

                dynamic appPart = linkNewPart.Value<JObject>("appPart");
                dynamic appFile = linkNewPart.Value<JObject>("appFile");

                GvaApplication application = this.applicationRepository.Find(id.Value);
                Lot lot = this.lotRepository.GetLotIndex(application.LotId);

                SetPart setPart = this.unitOfWork.DbContext.Set<SetPart>().FirstOrDefault(e => e.Alias == setPartAlias);
                string path = setPart.PathRegex.Remove(setPart.PathRegex.IndexOf("\\"), 4).Remove(0, 1) + "*";
                PartVersion partVersion = lot.CreatePart(path, appPart, userContext);
                lot.Commit(userContext, lotEventDispatcher);

                if  (Regex.IsMatch(setPart.Alias, @"\w+(Application)"))
                {
                    application.GvaAppLotPart = partVersion.Part;
                }

                int docFileId = (int)appFile.docFileId;
                DocFile docFile = this.unitOfWork.DbContext.Set<DocFile>().FirstOrDefault(e => e.DocFileId == docFileId);

                GvaLotFile lotFile = new GvaLotFile()
                {
                    LotPart = partVersion.Part,
                    DocFile = docFile,
                    GvaCaseTypeId = (int)appFile.caseTypeId,
                    PageNumber = (int)appFile.pageCount
                };
                lotFile.SavePageIndex((string)appFile.bookPageNumber);

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

                dynamic appPart = newPart.Value<JObject>("appPart");
                dynamic appFile = newPart.Value<JObject>("appFile");

                GvaApplication application = this.applicationRepository.Find(id.Value);
                Lot lot = this.lotRepository.GetLotIndex(application.LotId);

                SetPart setPart = this.unitOfWork.DbContext.Set<SetPart>().FirstOrDefault(e => e.Alias == setPartAlias);
                string path = setPart.PathRegex.Remove(setPart.PathRegex.IndexOf("\\"), 4).Remove(0, 1) + "*";

                PartVersion partVersion = lot.CreatePart(path, appPart, userContext);
                lot.Commit(userContext, lotEventDispatcher);

                if (Regex.IsMatch(setPart.Alias, @"\w+(Application)"))
                {
                    application.GvaAppLotPart = partVersion.Part;
                }
               
                var doc = this.docRepository.Find(docId.Value);
                var docFile = doc.CreateDocFile(
                    (int)appFile.docFileKindId,
                    (int)appFile.docFileTypeId,
                    (string)appFile.name,
                    (string)appFile.file.name,
                    String.Empty,
                    (Guid)appFile.file.key,
                    true,
                    true,
                    userContext);

                GvaLotFile lotFile = new GvaLotFile()
                {
                    LotPart = partVersion.Part,
                    DocFile = docFile,
                    GvaCaseTypeId = (int)appFile.caseTypeId,
                    PageNumber = (int)appFile.pageCount
                };
                lotFile.SavePageIndex((string)appFile.bookPageNumber);

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
                foreach (var file in files.Where(e => e.IsNew && !e.IsDeleted))
                {
                    doc.CreateDocFile(file.DocFileKindId, file.DocFileTypeId, file.Name, file.File.Name, String.Empty, file.File.Key, userContext);
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
                DocSourceType manuelSoruce = this.unitOfWork.DbContext.Set<DocSourceType>().SingleOrDefault(e => e.Alias == "Manual");

                var gvaCorrespondent = this.applicationRepository.GetGvaCorrespondentByLotId(applicationNewDO.LotId);
                Correspondent correspondent = null;
                if (gvaCorrespondent == null)
                {
                    var lot = this.lotRepository.GetLotIndex(applicationNewDO.LotId);
                    CorrespondentGroup applicantCorrespondentGroup = this.unitOfWork.DbContext.Set<CorrespondentGroup>().SingleOrDefault(e => e.Alias == "Applicants");//?
                    CorrespondentType bgCorrespondentType = this.unitOfWork.DbContext.Set<CorrespondentType>().SingleOrDefault(e => e.Alias == "BulgarianCitizen");//?

                    if (applicationNewDO.LotSetAlias == "Person")
                    {
                        dynamic personData = lot.GetPartContent("personData");

                        correspondent = this.correspondentRepository.CreateBgCitizen(
                         applicantCorrespondentGroup.CorrespondentGroupId,
                         bgCorrespondentType.CorrespondentTypeId,
                         true,
                         (string)personData.firstName,
                         (string)personData.lastName,
                         (string)personData.uin,
                         this.userContext);
                        correspondent.Email = (string)personData.email;

                        correspondent.CreateCorrespondentContact(
                        String.Format("{0} {1} {2}", (string)personData.firstName, (string)personData.middleName, (string)personData.lastName),
                        (string)personData.uin,
                        null,
                        true,
                        userContext);
                    }
                    //todo ??
                    else if (applicationNewDO.LotSetAlias == "Organization")
                    {
                        dynamic organizationData = lot.GetPartContent("organizationData");

                        correspondent = this.correspondentRepository.CreateBgCitizen(
                        applicantCorrespondentGroup.CorrespondentGroupId,
                        bgCorrespondentType.CorrespondentTypeId,
                        true,
                        (string)organizationData.name,
                        "",
                        "",
                        this.userContext);
                    }
                    //todo ??
                    else if (applicationNewDO.LotSetAlias == "Aircraft")
                    {
                        dynamic aircraftData = lot.GetPartContent("aircraftData");

                        correspondent = this.correspondentRepository.CreateBgCitizen(
                        applicantCorrespondentGroup.CorrespondentGroupId,
                        bgCorrespondentType.CorrespondentTypeId,
                        true,
                        ((string)aircraftData.model + " " + (string)aircraftData.icao),
                        "",
                        "",
                        this.userContext);
                    }

                    this.unitOfWork.Save();

                    gvaCorrespondent = new GvaCorrespondent();
                    gvaCorrespondent.Correspondent = correspondent;
                    gvaCorrespondent.LotId = applicationNewDO.LotId;
                    gvaCorrespondent.IsActive = true;

                    this.applicationRepository.AddGvaCorrespondent(gvaCorrespondent);
                }
                else
                {
                    correspondent = gvaCorrespondent.Correspondent;
                }

                Doc newDoc = docRepository.CreateDoc(
                    applicationNewDO.Doc.DocDirectionId,
                    documentEntryType.DocEntryTypeId,
                    draftStatus.DocStatusId,
                    applicationNewDO.Doc.DocSubject,
                    applicationNewDO.Doc.DocCasePartTypeId,
                    manuelSoruce.DocSourceTypeId,
                    applicationNewDO.Doc.DocDestinationTypeId,
                    applicationNewDO.Doc.DocTypeId,
                    applicationNewDO.Doc.DocFormatTypeId,
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
                        correspondent != null ? new List<int> { correspondent.CorrespondentId } : null,
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

        [Route("app")]
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
    }
}