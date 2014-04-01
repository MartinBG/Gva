using AutoMapper;
using Common.Api.UserContext;
using Common.Data;
using Docs.Api.DataObjects;
using Docs.Api.Enums;
using Docs.Api.Models;
using Docs.Api.Repositories.DocRepository;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.PersonRepository;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;
using Regs.Api.LotEvents;
using Gva.Api.Mappers.Resolvers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web.Http;
using Docs.Api.Repositories.CorrespondentRepository;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/apps")]
    [Authorize]
    public class ApplicationsController : GvaLotsController
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IPersonRepository personRepository;
        private IDocRepository docRepository;
        private ICorrespondentRepository correspondentRepository;
        private IApplicationRepository applicationRepository;
        private ILotEventDispatcher lotEventDispatcher;
        private UserContext userContext;

        public ApplicationsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IPersonRepository personRepository,
            IDocRepository docRepository,
            ICorrespondentRepository correspondentRepository,
            IApplicationRepository applicationRepository,
            IFileRepository fileRepository,
            ILotEventDispatcher lotEventDispatcher,
            FileResolver fileResolver)
            : base(lotRepository, fileRepository, unitOfWork, lotEventDispatcher, fileResolver)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.personRepository = personRepository;
            this.docRepository = docRepository;
            this.correspondentRepository = correspondentRepository;
            this.applicationRepository = applicationRepository;
            this.docRepository = docRepository;
            this.lotEventDispatcher = lotEventDispatcher;
        }

        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            this.userContext = this.Request.GetUserContext();
        }

        [Route("")]
        public IHttpActionResult GetApplications(DateTime? fromDate = null, DateTime? toDate = null, string lin = null)
        {
            var applications = this.applicationRepository.GetApplications(fromDate, toDate, lin);

            return Ok(applications);
        }

        [Route("{id}")]
        public IHttpActionResult GetApplication(int id)
        {
            var application = this.applicationRepository.Find(id,
                e => e.GvaAppLotFiles.Select(f => f.DocFile),
                e => e.GvaAppLotFiles.Select(f => f.GvaLotFile),
                //e => e.GvaLotObjects,
                e => e.Doc.DocFiles);

            if (application == null)
            {
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }

            ApplicationDO returnValue = new ApplicationDO(application);

            returnValue.Person = Mapper.Map<GvaPerson, PersonDO>(this.personRepository.GetPerson(application.LotId));

            var docRelations = this.docRepository.GetCaseRelationsByDocId(application.DocId.Value,
                e => e.Doc.DocFiles,
                e => e.Doc.DocDirection,
                e => e.Doc.DocType,
                e => e.Doc.DocStatus,
                e => e.Doc.DocEntryType
                );

            foreach (var dr in docRelations)
            {
                ApplicationDocRelationDO applicationDocRelation = new ApplicationDocRelationDO(dr);

                if (dr.Doc.DocEntryType.Alias == "Document")
                {
                    foreach (var docFile in dr.Doc.DocFiles)
                    {
                        var appFile = this.unitOfWork.DbContext.Set<GvaAppLotFile>()
                            .Include(e => e.GvaLotFile.LotPart.SetPart)
                            .FirstOrDefault(e => e.DocFileId == docFile.DocFileId);

                        applicationDocRelation.ApplicationLotFiles.Add(new ApplicationLotFileDO(docFile, appFile));
                    }

                    returnValue.ApplicationDocCase.Add(applicationDocRelation);
                }
            }

            return Ok(returnValue);
        }

        [Route("{id}/parts/linkNew")]
        public IHttpActionResult PostLinkNewPart(int id, ApplicationPartDO linkNewPart)
        {
            if (string.IsNullOrEmpty(linkNewPart.SetPartAlias))
            {
                return BadRequest();
            }

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();

                GvaApplication application = this.applicationRepository.Find(id);
                Lot lot = this.lotRepository.GetLotIndex(application.LotId);

                SetPart setPart = this.unitOfWork.DbContext.Set<SetPart>().FirstOrDefault(e => e.Alias == linkNewPart.SetPartAlias);
                string path = setPart.PathRegex.Remove(setPart.PathRegex.IndexOf("\\"), 4).Remove(0, 1) + "*";
                PartVersion partVersion = lot.CreatePart(path, linkNewPart.AppPart, userContext);
                lot.Commit(userContext, lotEventDispatcher);

                if (setPart.Alias == "application")
                {
                    application.GvaAppLotPart = partVersion.Part;
                }

                int docFileId = (int)linkNewPart.AppFile.docFileId;
                DocFile docFile = this.unitOfWork.DbContext.Set<DocFile>().FirstOrDefault(e => e.DocFileId == docFileId);

                GvaLotFile lotFile = new GvaLotFile()
                {
                    LotPart = partVersion.Part,
                    DocFile = docFile,
                    GvaCaseTypeId = (int)linkNewPart.AppFile.caseTypeId,
                    PageNumber = (int)linkNewPart.AppFile.pageCount
                };
                lotFile.SavePageIndex((string)linkNewPart.AppFile.bookPageNumber);

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
        public IHttpActionResult PostNewPart(int id, ApplicationPartDO newPart)
        {
            if (newPart.DocId == null)
            {
                return BadRequest();
            }

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();

                GvaApplication application = this.applicationRepository.Find(id);
                Lot lot = this.lotRepository.GetLotIndex(application.LotId);

                SetPart setPart = this.unitOfWork.DbContext.Set<SetPart>().FirstOrDefault(e => e.Alias == newPart.SetPartAlias);
                string path = setPart.PathRegex.Remove(setPart.PathRegex.IndexOf("\\"), 4).Remove(0, 1) + "*";

                PartVersion partVersion = lot.CreatePart(path, newPart.AppPart, userContext);
                lot.Commit(userContext, lotEventDispatcher);

                if (setPart.Alias == "application")
                {
                    application.GvaAppLotPart = partVersion.Part;
                }

                var doc = this.docRepository.Find(newPart.DocId.Value);
                var docFile = doc.CreateDocFile(
                    (int)newPart.AppFile.docFileKindId,
                    (int)newPart.AppFile.docFileTypeId,
                    (string)newPart.AppFile.name,
                    (string)newPart.AppFile.file.name,
                    String.Empty,
                    (Guid)newPart.AppFile.file.key,
                    true,
                    true,
                    userContext);

                GvaLotFile lotFile = new GvaLotFile()
                {
                    LotPart = partVersion.Part,
                    DocFile = docFile,
                    GvaCaseTypeId = (int)newPart.AppFile.caseTypeId,
                    PageNumber = (int)newPart.AppFile.pageCount
                };
                lotFile.SavePageIndex((string)newPart.AppFile.bookPageNumber);

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
        public IHttpActionResult PostLinkExistingPart(int id, ApplicationPartDO linkExistingPart)
        {
            if (linkExistingPart.DocFileId == null || linkExistingPart.PartId == null)
            {
                return BadRequest();
            }

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                GvaApplication application = this.applicationRepository.Find(id);
                DocFile docFile = this.unitOfWork.DbContext.Set<DocFile>().FirstOrDefault(e => e.DocFileId == linkExistingPart.DocFileId);

                GvaLotFile gvaLotFile = this.unitOfWork.DbContext.Set<GvaLotFile>()
                    .Include(e => e.LotPart.SetPart)
                    .FirstOrDefault(e => e.LotPartId == linkExistingPart.PartId);

                if (gvaLotFile.LotPart.SetPart.Alias == "application")
                {
                    application.GvaAppLotPart = gvaLotFile.LotPart;
                }

                GvaAppLotFile gvaAppLotFile = new GvaAppLotFile()
                {
                    GvaApplication = application,
                    GvaLotFile = gvaLotFile,
                    DocFile = docFile
                };

                this.applicationRepository.AddGvaAppLotFile(gvaAppLotFile);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok();
            }
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

                var gvaCorrespondent = personRepository.GetGvaCorrespondentByPersonId(applicationNewDO.LotId);
                Correspondent correspondent;
                if (gvaCorrespondent == null)
                {
                    var lot = this.lotRepository.GetLotIndex(applicationNewDO.LotId);
                    dynamic personData = lot.GetPartContent("personData");

                    CorrespondentGroup applicantCorrespondentGroup = this.unitOfWork.DbContext.Set<CorrespondentGroup>().SingleOrDefault(e => e.Alias == "Applicants");//?
                    CorrespondentType bgCorrespondentType = this.unitOfWork.DbContext.Set<CorrespondentType>().SingleOrDefault(e => e.Alias == "BulgarianCitizen");//?

                    correspondent = this.correspondentRepository.CreateBgCitizen(
                        applicantCorrespondentGroup.CorrespondentGroupId,
                        bgCorrespondentType.CorrespondentTypeId,
                        true,
                        (string)personData.firstName,
                        (string)personData.lastName,
                        personData.uin,
                        this.userContext);

                    correspondent.Email = (string)personData.email;

                    correspondent.CreateCorrespondentContact(
                        String.Format("{0} {1} {2}", (string)personData.firstName, (string)personData.middleName, (string)personData.lastName),
                        (string)personData.uin,
                        null,
                        true,
                        userContext);

                    this.unitOfWork.Save();

                    gvaCorrespondent = new GvaCorrespondent();
                    gvaCorrespondent.Correspondent = correspondent;
                    gvaCorrespondent.LotId = applicationNewDO.LotId;
                    gvaCorrespondent.IsActive = true;

                    this.personRepository.AddGvaCorrespondent(gvaCorrespondent);
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

                newDoc.CreateDocProperties(
                        null,
                        internalDocCasePartType.DocCasePartTypeId,
                        docTypeClassifications,
                        electronicServiceStage,
                        docTypeUnitRoles,
                        correspondent != null ? new List<int> { correspondent.CorrespondentId } : null,
                        null,
                        this.userContext);

                this.docRepository.GenerateAccessCode(newDoc, userContext);

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

        [Route("{id}/attachDocFile")]
        [HttpPost]
        public IHttpActionResult PostAttachDocFile(int id, int docId, DocFileDO[] files)
        {
            UserContext userContext = this.Request.GetUserContext();

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var doc = this.docRepository.Find(docId);
                foreach(var file in files)
                {
                    doc.CreateDocFile(file.DocFileKindId, file.DocFileTypeId, file.Name, file.File.Name, String.Empty, file.File.Key, userContext);
                }

                this.unitOfWork.Save();

                transaction.Commit();
            }

            return Ok();
        }
    }
}