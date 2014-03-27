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
        private IApplicationRepository applicationRepository;
        private ILotEventDispatcher lotEventDispatcher;

        public ApplicationsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IPersonRepository personRepository,
            IDocRepository docRepository,
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
            this.applicationRepository = applicationRepository;
            this.docRepository = docRepository;
            this.lotEventDispatcher = lotEventDispatcher;
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
            //todo 
            if (string.IsNullOrEmpty(linkNewPart.SetPartAlias))
            {
                throw new InvalidOperationException(string.Format("Error: empty parameters."));
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

        [Route("{id}/parts/new")]
        public IHttpActionResult PostNewPart(int id, ApplicationPartDO newPart)
        {
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

                var doc = this.docRepository.Find(newPart.DocId);
                var docFile = doc.CreateDocFile(
                    (int)newPart.AppFile.docFileKindId,
                    (int)newPart.AppFile.docFileTypeId,
                    (string)newPart.AppFile.name,
                    (string)newPart.AppFile.docFile.name,
                    String.Empty,
                    (Guid)newPart.AppFile.docFile.key,
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

        [Route("new")]
        public IHttpActionResult PostNewApplication(ApplicationNewDO applicationNewDO)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();

                DocEntryType documentEntryType = this.unitOfWork.DbContext.Set<DocEntryType>().SingleOrDefault(e => e.Alias == "Document");
                DocStatus draftStatus = this.unitOfWork.DbContext.Set<DocStatus>().SingleOrDefault(e => e.Alias == "Draft");
                DocSourceType manuelSoruce = this.unitOfWork.DbContext.Set<DocSourceType>().SingleOrDefault(e => e.Alias == "Manual");

                var lot = this.lotRepository.GetLotIndex(applicationNewDO.LotId);

                dynamic personAppData = applicationNewDO.AppPart;

                PartVersion partVersion = lot.CreatePart("personDocumentApplications/*", personAppData, userContext);

                lot.Commit(userContext, lotEventDispatcher);

                Doc doc = docRepository.CreateDoc(
                    applicationNewDO.Doc.DocDirectionId,
                    documentEntryType.DocEntryTypeId,
                    draftStatus.DocStatusId,
                    applicationNewDO.Doc.DocSubject,
                    applicationNewDO.Doc.DocCasePartTypeId,
                    null,
                    applicationNewDO.Doc.DocDestinationTypeId,
                    applicationNewDO.Doc.DocTypeId,
                    applicationNewDO.Doc.DocFormatTypeId,
                    null,
                    userContext);

                var docRelation = new DocRelation()
                {
                    Doc = doc,
                    RootDoc = doc
                };
                doc.DocRelations.Add(docRelation);

                dynamic appFile = applicationNewDO.AppFile;
                var docFile = doc.CreateDocFile(
                    (int)appFile.docFileKindId,
                    (int)appFile.docFileTypeId,
                    (string)appFile.name,
                    (string)appFile.docFile.name,
                    String.Empty,
                    (Guid)appFile.docFile.key,
                    true,
                    true,
                    userContext);


                GvaLotFile lotFile = new GvaLotFile()
                {
                    LotPart = partVersion.Part,
                    DocFile = docFile,
                    GvaCaseTypeId = (int)appFile.caseTypeId,
                    PageIndex = (string)appFile.bookPageNumber,
                    PageNumber = (int)appFile.pageCount
                };
                lotFile.SavePageIndex(lotFile.PageIndex);

                GvaApplication application = new GvaApplication()
                {
                    LotId = applicationNewDO.LotId,
                    Doc = doc,
                    GvaAppLotPart = partVersion.Part
                };

                GvaAppLotFile gvaAppLotFile = new GvaAppLotFile()
                {
                    GvaApplication = application,
                    GvaLotFile = lotFile,
                    DocFile = docFile

                };

                applicationRepository.AddGvaAppLotFile(gvaAppLotFile);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new
                {
                    gvaApplicationId = application.GvaApplicationId
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
                    gvaApplicationId = application.GvaApplicationId
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

            List<int> loadedDocIds = returnValue.Where(e => e.DocId.HasValue).Select(e => e.DocId.Value).ToList();
            List<DocUser> docUsersForList = this.unitOfWork.DbContext.Set<DocUser>()
               .Where(du => du.UnitId == unitUser.UnitId && du.IsActive && loadedDocIds.Contains(du.DocId))
               .ToList();

            foreach (var item in returnValue)
            {
                if (docView == DocView.ForControl || docView == DocView.ForManagement)
                {
                    int? rootId = this.unitOfWork.DbContext.Set<DocRelation>()
                        .FirstOrDefault(e => e.DocId == item.DocId)
                        .RootDocId;

                    if (rootId.HasValue)
                    {
                        DocRelation rootDocRelation = this.unitOfWork.DbContext.Set<DocRelation>()
                            .Include(e => e.Doc.DocDirection)
                            .Include(e => e.Doc.DocCasePartType)
                            .Include(e => e.Doc.DocType)
                            .FirstOrDefault(e => e.DocId == rootId.Value);

                        item.CaseDocRelation = new DocRelationDO(rootDocRelation);
                    }
                }

                item.SetDocUsers(docUsersForList.Where(e => e.DocId == item.DocId).ToList(), unitUser);

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
    }
}