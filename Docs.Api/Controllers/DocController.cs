using Common.Api.UserContext;
using Common.Blob;
using Common.Extensions;
using Common.Utils;
using Docs.Api.DataObjects;
using Docs.Api.Enums;
using Docs.Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Http;
using Common.Api.StaticNomenclatures;
using Rio.Data.Utils;
using Common.Api.Models;
using Rio.Objects;
using Rio.Data.Abbcdn;
using System.ServiceModel;
using Abbcdn;
using Rio.Data.ServiceContracts.AppCommunicator;
using Docs.Api.Repositories.EmailRepository;

namespace Docs.Api.Controllers
{
    [Authorize]
    public class DocController : ApiController
    {
        private Common.Data.IUnitOfWork unitOfWork;
        private Docs.Api.Repositories.DocRepository.IDocRepository docRepository;
        private Docs.Api.Repositories.ClassificationRepository.IClassificationRepository classificationRepository;
        private Docs.Api.Repositories.EmailRepository.IEmailRepository emailRepository;
        private UserContext userContext;

        public DocController(Common.Data.IUnitOfWork unitOfWork,
            Docs.Api.Repositories.DocRepository.IDocRepository docRepository,
            Docs.Api.Repositories.ClassificationRepository.IClassificationRepository classificationRepository,
            Docs.Api.Repositories.EmailRepository.IEmailRepository emailRepository)
        {
            this.unitOfWork = unitOfWork;
            this.docRepository = docRepository;
            this.classificationRepository = classificationRepository;
            this.emailRepository = emailRepository;
        }

        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            try
            {
                this.userContext = this.Request.GetUserContext();
            }
            catch { }
        }

        /// <summary>
        /// Търсене на документи
        /// </summary>
        /// <param name="docPage">Филтър за търсене на документ по ключова дума</param>
        /// <param name="fromDate">Филтър за търсене на документ по дата</param>
        /// <param name="toDate">Филтър за търсене на документ по дата</param>
        /// <param name="regUri">Филтър за търсене на документ по номер</param>
        /// <param name="docName">Филтър за търсене на документ по наименование</param>
        /// <param name="docTypeId">Филтър за търсене на документ по тип</param>
        /// <param name="docStatusId">Филтър за търсене на документ по статус</param>
        /// <param name="corrs">Филтър за търсене на документ по кореспонденти</param>
        /// <param name="units">Филтър за търсене на документ по служители</param>
        /// <param name="ds">Параметър на търсенето</param>
        /// <param name="limit">Брой резултати на страница</param>
        /// <param name="offset">Параметър за страницирането</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetDocs(
            int limit = 10,
            int offset = 0,
            string filter = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            string regUri = null,
            string docName = null,
            int? docTypeId = null,
            int? docStatusId = null,
            bool? hideRead = null,
            bool? isCase = null,
            string corrs = null,
            string units = null,
            string ds = null
            )
        {
            this.userContext = this.Request.GetUserContext();

            UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);
            ClassificationPermission readPermission = this.unitOfWork.DbContext.Set<ClassificationPermission>().SingleOrDefault(e => e.Alias == "Read");
            DocSourceType docSourceType = this.unitOfWork.DbContext.Set<DocSourceType>().SingleOrDefault(e => e.Alias == "Internet");
            DocCasePartType docCasePartType = this.unitOfWork.DbContext.Set<DocCasePartType>().SingleOrDefault(e => e.Alias == "Control");
            List<DocStatus> docStatuses = this.unitOfWork.DbContext.Set<DocStatus>().Where(e => e.IsActive).ToList();
            List<DocUnitRole> docUnitRoles = this.unitOfWork.DbContext.Set<DocUnitRole>()
                .Where(e => e.Alias.ToLower() == "incharge" || e.Alias.ToLower() == "controlling" || e.Alias.ToLower() == "to")
                .ToList();

            int totalCount = 0;
            DocView docView = DocView.Normal;
            List<Doc> docs = new List<Doc>();

            switch (filter)
            {
                case null:
                case "current":
                    //текущи преписки
                    docs = this.docRepository.GetCurrentCaseDocs(
                        fromDate,
                        toDate,
                        regUri,
                        docName,
                        docTypeId,
                        docStatusId,
                        hideRead,
                        isCase,
                        corrs,
                        units,
                        ds,
                        limit,
                        offset,
                        docCasePartType,
                        docStatuses,
                        readPermission,
                        unitUser,
                        out totalCount);
                    break;
                case "finished":
                    //приключени преписки
                    docs = this.docRepository.GetFinishedCaseDocs(
                        fromDate,
                        toDate,
                        regUri,
                        docName,
                        docTypeId,
                        docStatusId,
                        hideRead,
                        isCase,
                        corrs,
                        units,
                        ds,
                        limit,
                        offset,
                        docCasePartType,
                        docStatuses,
                        readPermission,
                        unitUser,
                        out totalCount);
                    break;
                case "manage":
                    //за управление
                    docs = this.docRepository.GetDocsForManagement(
                       fromDate,
                       toDate,
                       regUri,
                       docName,
                       docTypeId,
                       docStatusId,
                       hideRead,
                       isCase,
                       corrs,
                       units,
                       ds,
                       limit,
                       offset,
                       docCasePartType,
                       docStatuses,
                       readPermission,
                       unitUser,
                       out totalCount);

                    docView = DocView.ForManagement;
                    break;
                case "control":
                    //за контрол и изпълнение
                    docs = this.docRepository.GetDocsForControl(
                      fromDate,
                      toDate,
                      regUri,
                      docName,
                      docTypeId,
                      docStatusId,
                      hideRead,
                      isCase,
                      corrs,
                      units,
                      ds,
                      limit,
                      offset,
                      docCasePartType,
                      docStatuses,
                      readPermission,
                      docUnitRoles,
                      unitUser,
                      out totalCount);

                    docView = DocView.ForControl;
                    break;
                case "draft":
                    //чернови
                    docs = this.docRepository.GetDraftDocs(
                      fromDate,
                      toDate,
                      regUri,
                      docName,
                      docTypeId,
                      docStatusId,
                      hideRead,
                      isCase,
                      corrs,
                      units,
                      ds,
                      limit,
                      offset,
                      docCasePartType,
                      docStatuses,
                      readPermission,
                      unitUser,
                      out totalCount);
                    break;
                case "unfinished":
                    //неприключени
                    docs = this.docRepository.GetUnfinishedDocs(
                      fromDate,
                      toDate,
                      regUri,
                      docName,
                      docTypeId,
                      docStatusId,
                      hideRead,
                      isCase,
                      corrs,
                      units,
                      ds,
                      limit,
                      offset,
                      docCasePartType,
                      docStatuses,
                      readPermission,
                      unitUser,
                      out totalCount);
                    break;
                case "portal":
                    //от портал
                    docs = this.docRepository.GetPortalDocs(
                      fromDate,
                      toDate,
                      regUri,
                      docName,
                      docTypeId,
                      docStatusId,
                      hideRead,
                      isCase,
                      corrs,
                      units,
                      ds,
                      limit,
                      offset,
                      docCasePartType,
                      docStatuses,
                      readPermission,
                      docSourceType,
                      unitUser,
                      out totalCount);
                    break;
                case "new":
                    //нова поща
                    throw new NotImplementedException();
                case "allControl":
                    //всички контролни
                    docs = this.docRepository.GetControlDocs(
                      fromDate,
                      toDate,
                      regUri,
                      docName,
                      docTypeId,
                      docStatusId,
                      hideRead,
                      isCase,
                      corrs,
                      units,
                      ds,
                      limit,
                      offset,
                      docCasePartType,
                      readPermission,
                      unitUser,
                      out totalCount);
                    break;
                case "all":
                default:
                    //всички
                    docs = this.docRepository.GetDocs(
                      fromDate,
                      toDate,
                      regUri,
                      docName,
                      docTypeId,
                      docStatusId,
                      hideRead,
                      isCase,
                      corrs,
                      units,
                      ds,
                      limit,
                      offset,
                      docCasePartType,
                      readPermission,
                      unitUser,
                      out totalCount);
                    break;
            };

            List<int> loadedDocIds = docs.Select(e => e.DocId).ToList();

            var docHasReads = this.unitOfWork.DbContext.Set<DocHasRead>()
                .Where(e => e.UnitId == unitUser.UnitId && loadedDocIds.Contains(e.DocId)).ToList();

            List<DocListItemDO> returnValue = docs.Select(e => new DocListItemDO(e, unitUser)).ToList();

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

                        var docCorrespondents = this.unitOfWork.DbContext.Set<DocCorrespondent>()
                            .Include(e => e.Correspondent.CorrespondentType)
                            .Where(e => e.DocId == rootId.Value)
                            .ToList();

                        item.DocCorrespondents.AddRange(docCorrespondents.Select(e => new DocCorrespondentDO(e)).ToList());
                    }
                }
                else
                {
                    var docCorrespondents = this.unitOfWork.DbContext.Set<DocCorrespondent>()
                        .Include(e => e.Correspondent.CorrespondentType)
                        .Where(e => e.DocId == item.DocId)
                        .ToList();

                    item.DocCorrespondents.AddRange(docCorrespondents.Select(e => new DocCorrespondentDO(e)).ToList());
                }
            }

            StringBuilder sb = new StringBuilder();

            if (totalCount >= 10000)
            {
                sb.Append("Има повече от 10000 резултата, моля, въведете допълнителни филтри.");
            }

            List<int> docIds = Helper.GetIdListFromString(ds);
            if (docIds.Any())
            {
                int min = docIds.Min(),
                    max = docIds.Max();

                DocListItemDO minDoc = new DocListItemDO(this.docRepository.Find(min));
                DocListItemDO maxDoc = new DocListItemDO(this.docRepository.Find(max));

                if (minDoc != null && maxDoc != null)
                {
                    sb.AppendFormat("Документите са регистрирани с номера от <strong>{0}</strong> до <strong>{1}</strong>", minDoc.RegUri, maxDoc.RegUri);
                }
                else
                {
                    sb.Append("Проблем с регистрирането на документите");
                }
            }

            return Ok(new
                {
                    docView = docView.ToString(),
                    documents = returnValue,
                    documentCount = totalCount,
                    msg = sb.ToString()
                });
        }

        [HttpGet]
        public IHttpActionResult GetDocsForChange(
            int id,
            int limit = 10,
            int offset = 0,
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
            this.userContext = this.Request.GetUserContext();

            UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);

            ClassificationPermission readPermission = this.unitOfWork.DbContext.Set<ClassificationPermission>().SingleOrDefault(e => e.Alias == "Read");

            int totalCount = 0;

            List<Doc> docs = new List<Doc>();
            List<int> docRelations = this.docRepository.fnGetSubordinateDocs(id);

            docs = this.docRepository.GetDocsForChange(
                      fromDate,
                      toDate,
                      regUri,
                      docName,
                      docTypeId,
                      docStatusId,
                      corrs,
                      units,
                      docRelations,
                      limit,
                      offset,
                      readPermission,
                      unitUser,
                      out totalCount);

            List<DocListItemDO> returnValue = docs.Select(e => new DocListItemDO(e, unitUser)).ToList();

            StringBuilder sb = new StringBuilder();

            if (totalCount >= 10000)
            {
                sb.Append("Има повече от 10000 резултата, моля, въведете допълнителни филтри.");
            }

            return Ok(new
            {
                documents = returnValue,
                documentCount = totalCount,
                msg = sb.ToString()
            });
        }

        [HttpPost]
        public IHttpActionResult ChangeDocParent(int id, int newDocId)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);

                ClassificationPermission docMovementPermission = this.classificationRepository.GetByAlias("DocMovement");
                ClassificationPermission readPermission = this.classificationRepository.GetByAlias("Read");

                bool hasDocMovementPermission =
                    this.classificationRepository.HasPermission(unitUser.UnitId, id, docMovementPermission.ClassificationPermissionId) &&
                    this.classificationRepository.HasPermission(unitUser.UnitId, id, readPermission.ClassificationPermissionId);

                if (!hasDocMovementPermission)
                {
                    return BadRequest("Not enough permissions!");
                }

                Doc doc = this.docRepository.Find(id);

                if (doc == null)
                {
                    return NotFound();
                }

                int oldCaseId = this.docRepository.GetCaseId(id);

                List<int> docIds = this.docRepository.fnGetSubordinateDocs(id);

                DocRelation newDocRelation = this.unitOfWork.DbContext.Set<DocRelation>().FirstOrDefault(e => e.DocId == newDocId);

                List<DocRelation> docRelations = this.docRepository.GetCaseRelationsByDocId(id);

                foreach (var docRelation in docRelations)
                {
                    if (docIds.Contains(docRelation.DocId))
                    {
                        if (docRelation.DocId == id)
                        {
                            docRelation.ParentDocId = newDocId;
                        }

                        docRelation.RootDocId = newDocRelation.RootDocId;
                    }
                }

                doc.IsCase = false;

                this.unitOfWork.Save();

                this.docRepository.ExecSpSetDocUnitTokens(docId: oldCaseId);
                this.docRepository.ExecSpSetDocUnitTokens(docId: newDocId);

                if (oldCaseId != id)
                {
                    this.docRepository.RearangeReceiptOrder(oldCaseId, id, everything: false);
                }

                int? boundary = this.docRepository.GetNextReceiptOrder(newDocId);
                if (boundary.HasValue)
                {
                    this.docRepository.RearangeBoundaryReceiptOrder(newDocId, id, boundary.Value);
                }
                else
                {
                    this.docRepository.RearangeReceiptOrder(newDocId, 0, everything: true);
                }

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok();
            }
        }

        [HttpPost]
        public IHttpActionResult ChangeDocClassification(int id, List<DocClassificationDO> docClassifications)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                DateTime currentDate = DateTime.Now;

                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);

                ClassificationPermission techEditPermission = this.classificationRepository.GetByAlias("EditTech");
                ClassificationPermission readPermission = this.classificationRepository.GetByAlias("Read");

                bool hasTechEditPermission =
                    this.classificationRepository.HasPermission(unitUser.UnitId, id, techEditPermission.ClassificationPermissionId) &&
                    this.classificationRepository.HasPermission(unitUser.UnitId, id, readPermission.ClassificationPermissionId);

                if (!hasTechEditPermission)
                {
                    return BadRequest("Not enough permissions!");
                }

                var doc = this.docRepository.Find(id,
                    e => e.DocClassifications);

                if (doc == null)
                {
                    return NotFound();
                }

                //delete
                foreach (var docClassification in docClassifications.Where(e => e.IsRemoved))
                {
                    if (docClassification.DocClassificationId.HasValue)
                    {
                        DocClassification dc = doc.DocClassifications.FirstOrDefault(e => e.DocClassificationId == docClassification.DocClassificationId.Value);

                        if (dc != null)
                        {
                            this.unitOfWork.DbContext.Set<DocClassification>().Remove(dc);
                        }
                    }
                }

                //update
                foreach (var docClassification in docClassifications.Where(e => !e.IsAdded && !e.IsRemoved))
                {
                    if (docClassification.DocClassificationId.HasValue)
                    {
                        DocClassification dc = doc.DocClassifications.FirstOrDefault(e => e.DocClassificationId == docClassification.DocClassificationId.Value);

                        if (dc != null)
                        {
                            dc.IsActive = docClassification.IsActive;
                            dc.IsInherited = docClassification.IsInherited;
                        }
                    }
                }

                //add
                foreach (var docClassification in docClassifications.Where(e => e.IsAdded && !e.IsRemoved && e.IsActive))
                {
                    DocClassification dc = new DocClassification();
                    dc.Doc = doc;
                    dc.ClassificationId = docClassification.ClassificationId.Value;
                    dc.ClassificationByUserId = unitUser.UserId;
                    dc.ClassificationDate = currentDate;
                    dc.IsActive = docClassification.IsActive;
                    dc.IsInherited = docClassification.IsInherited;

                    this.unitOfWork.DbContext.Set<DocClassification>().Add(dc);
                }

                this.unitOfWork.Save();

                this.docRepository.ExecSpSetDocTokens(docId: doc.DocId);

                transaction.Commit();

                return Ok(new
                {
                    id = doc.DocId
                });
            }
        }

        [HttpPost]
        public IHttpActionResult CreateNewCase(int id)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);

                ClassificationPermission docMovementPermission = this.classificationRepository.GetByAlias("DocMovement");
                ClassificationPermission readPermission = this.classificationRepository.GetByAlias("Read");

                bool hasDocMovementPermission =
                    this.classificationRepository.HasPermission(unitUser.UnitId, id, docMovementPermission.ClassificationPermissionId) &&
                    this.classificationRepository.HasPermission(unitUser.UnitId, id, readPermission.ClassificationPermissionId);

                if (!hasDocMovementPermission)
                {
                    return BadRequest("Not enough permissions!");
                }

                Doc doc = this.docRepository.Find(id);

                if (doc == null)
                {
                    return NotFound();
                }

                int oldCaseId = this.docRepository.GetCaseId(id);

                List<int> docIds = this.docRepository.fnGetSubordinateDocs(id);
                List<DocRelation> docRelations = this.docRepository.GetCaseRelationsByDocId(id);

                foreach (var docRelation in docRelations)
                {
                    if (docIds.Contains(docRelation.DocId))
                    {
                        if (docRelation.DocId == id)
                        {
                            docRelation.ParentDocId = null;
                        }

                        docRelation.RootDocId = id;
                    }
                }

                doc.IsCase = true;

                this.unitOfWork.Save();

                this.docRepository.ExecSpSetDocUnitTokens(docId: oldCaseId);
                this.docRepository.ExecSpSetDocUnitTokens(docId: id);

                this.docRepository.RearangeReceiptOrder(oldCaseId, id, everything: false);
                this.docRepository.RearangeReceiptOrder(id, 0, everything: true);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok();
            }
        }

        /// <summary>
        /// Създаване на нов документ
        /// </summary>
        /// <param name="preDoc">Данни за нов документ</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateDoc(PreDocDO preDoc)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);
                DocEntryType documentEntryType = this.unitOfWork.DbContext.Set<DocEntryType>().SingleOrDefault(e => e.Alias == "Document");
                DocFormatType electronicDocFormatType = this.unitOfWork.DbContext.Set<DocFormatType>().SingleOrDefault(e => e.Alias == "Electronic");
                DocStatus draftStatus = this.unitOfWork.DbContext.Set<DocStatus>().SingleOrDefault(e => e.Alias == "Draft");

                List<Doc> createdDocs = new List<Doc>();

                for (int i = 0; i < preDoc.DocNumbers; i++)
                {
                    Doc newDoc = this.docRepository.CreateDoc(
                        preDoc.DocDirectionId,
                        documentEntryType.DocEntryTypeId,
                        draftStatus.DocStatusId,
                        preDoc.DocSubject,
                        preDoc.DocCasePartTypeId,
                        null,
                        null,
                        preDoc.DocTypeId,
                        preDoc.DocFormatTypeId,
                        null,
                        userContext);

                    DocRelation parentDocRelation = null;
                    if (preDoc.ParentDocId.HasValue)
                    {
                        parentDocRelation = this.unitOfWork.DbContext.Set<DocRelation>().FirstOrDefault(e => e.DocId == preDoc.ParentDocId.Value);
                    }

                    ElectronicServiceStage electronicServiceStage = null;
                    if (parentDocRelation == null)
                    {
                        electronicServiceStage = this.unitOfWork.DbContext.Set<ElectronicServiceStage>()
                            .SingleOrDefault(e => e.DocTypeId == newDoc.DocTypeId && e.IsFirstByDefault);
                    }

                    List<DocTypeUnitRole> docTypeUnitRoles = this.unitOfWork.DbContext.Set<DocTypeUnitRole>()
                        .Where(e => e.DocDirectionId == newDoc.DocDirectionId && e.DocTypeId == newDoc.DocTypeId)
                        .ToList();

                    DocUnitRole importedBy = this.unitOfWork.DbContext.Set<DocUnitRole>()
                        .SingleOrDefault(e => e.Alias == "ImportedBy");

                    List<DocTypeClassification> docTypeClassifications = null;
                    List<DocClassification> parentDocClassifications = null;

                    if (parentDocRelation == null)
                    {
                        docTypeClassifications = this.unitOfWork.DbContext.Set<DocTypeClassification>()
                            .Where(e => e.DocDirectionId == newDoc.DocDirectionId &&
                                e.DocTypeId == newDoc.DocTypeId &&
                                e.IsActive)
                            .ToList();
                    }
                    else
                    {
                        parentDocClassifications = this.unitOfWork.DbContext.Set<DocClassification>()
                            .Where(e => e.DocId == parentDocRelation.DocId &&
                                e.IsActive &&
                                e.IsInherited)
                            .ToList();
                    }

                    newDoc.CreateDocProperties(
                        parentDocRelation,
                        preDoc.DocCasePartTypeId,
                        docTypeClassifications,
                        parentDocClassifications,
                        electronicServiceStage,
                        docTypeUnitRoles,
                        importedBy,
                        unitUser,
                        preDoc.Correspondents,
                        null,
                        this.userContext);

                    if (newDoc.IsCase)
                    {
                        this.docRepository.GenerateAccessCode(newDoc, this.userContext);
                    }

                    if (newDoc.DocFormatTypeId == electronicDocFormatType.DocFormatTypeId)
                    {
                        DocType docType = this.unitOfWork.DbContext.Set<DocType>().SingleOrDefault(e => e.DocTypeId == newDoc.DocTypeId);

                        byte[] eDocFileContent = CreateRioObject(docType.ElectronicServiceFileTypeUri, parentDocRelation != null ? parentDocRelation.RootDocId : null, preDoc.ParentDocId);

                        if (eDocFileContent != null)
                        {
                            Guid eDocFileBlobKey = Guid.Empty;

                            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
                            {
                                connection.Open();
                                using (var blobWriter = new BlobWriter(connection))
                                using (var stream = blobWriter.OpenStream())
                                {
                                    stream.Write(eDocFileContent, 0, eDocFileContent.Length);
                                    eDocFileBlobKey = blobWriter.GetBlobKey();
                                }
                            }

                            DocFileKind publicDocFileKind = this.unitOfWork.DbContext.Set<DocFileKind>().SingleOrDefault(e => e.Alias == "PublicAttachedFile");
                            DocFileOriginType editableDocFileOriginType = this.unitOfWork.DbContext.Set<DocFileOriginType>().SingleOrDefault(e => e.Alias == "EditableFile");
                            DocFileType docFileType = this.unitOfWork.DbContext.Set<DocFileType>().SingleOrDefault(e => e.DocTypeUri == docType.ElectronicServiceFileTypeUri);

                            newDoc.CreateDocFile(
                                publicDocFileKind.DocFileKindId,
                                docFileType.DocFileTypeId,
                                editableDocFileOriginType.DocFileOriginTypeId,
                                "Електронен документ",
                                "E-Document.xml",
                                String.Empty,
                                eDocFileBlobKey,
                                userContext);
                        }
                    }

                    this.unitOfWork.Save();

                    this.docRepository.ExecSpSetDocTokens(docId: newDoc.DocId);
                    this.docRepository.ExecSpSetDocUnitTokens(docId: newDoc.DocId);

                    newDoc.SetReceiptOrder(this.docRepository.GetNextReceiptOrder(newDoc.DocId), this.userContext);

                    if (preDoc.Register)
                    {
                        ClassificationPermission registerPermission = this.classificationRepository.GetByAlias("Register");

                        bool hasRegisterPermission =
                            this.classificationRepository.HasPermission(unitUser.UnitId, newDoc.DocId, registerPermission.ClassificationPermissionId);

                        if (!hasRegisterPermission)
                        {
                            return BadRequest("Not enough permissions!");
                        }

                        this.docRepository.RegisterDoc(newDoc, unitUser, this.userContext);
                    }

                    this.unitOfWork.Save();

                    createdDocs.Add(newDoc);
                }

                transaction.Commit();

                if (createdDocs.Count == 1 && !preDoc.Register)
                {
                    return Ok(new
                        {
                            docId = createdDocs.FirstOrDefault().DocId
                        });
                }
                else
                {
                    string ids = Helper.GetStringFromIdList(createdDocs.Select(e => e.DocId).ToList());

                    return Ok(new
                        {
                            ids = ids
                        });
                }
            }
        }

        /// <summary>
        /// Създаване на подчинена резолюция/задача/забележка
        /// </summary>
        /// <param name="id">Идентификатор на родителския документ</param>
        /// <param name="docEntryTypeAlias">Тип на документа</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateChildInternalDoc(int id, string docEntryTypeAlias = null, string docTypeAlias = null)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);

                ClassificationPermission managementPermission = this.classificationRepository.GetByAlias("Management");
                ClassificationPermission readPermission = this.classificationRepository.GetByAlias("Read");

                bool hasManagementPermission =
                    this.classificationRepository.HasPermission(unitUser.UnitId, id, managementPermission.ClassificationPermissionId) &&
                    this.classificationRepository.HasPermission(unitUser.UnitId, id, readPermission.ClassificationPermissionId);

                DocEntryType documentEntryType = this.unitOfWork.DbContext.Set<DocEntryType>()
                    .SingleOrDefault(e => e.Alias.ToLower() == docEntryTypeAlias.ToLower());
                DocDirection internalDocDirection = this.unitOfWork.DbContext.Set<DocDirection>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "Internal".ToLower());
                DocStatus draftStatus = this.unitOfWork.DbContext.Set<DocStatus>().SingleOrDefault(e => e.Alias == "Draft");
                DocCasePartType internalDocCasePartType = this.unitOfWork.DbContext.Set<DocCasePartType>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "Internal".ToLower());
                DocFormatType paperDocFormatType = this.unitOfWork.DbContext.Set<DocFormatType>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "Paper".ToLower());
                DocType docType = null;
                string docSubject = string.Empty;

                switch (docEntryTypeAlias.ToLower())
                {
                    case "resolution":
                        if (!hasManagementPermission)
                        {
                            return BadRequest("Not enough permissions!");
                        }

                        docType = this.unitOfWork.DbContext.Set<DocType>().SingleOrDefault(e => e.Alias.ToLower() == docTypeAlias.ToLower());
                        docSubject = docTypeAlias.Contains("ParentOnly") ? "Разпределение чрез резолюция върху документ" : "Разпределение чрез резолюция";
                        break;
                    case "task":
                        if (!hasManagementPermission)
                        {
                            return BadRequest("Not enough permissions!");
                        }

                        docType = this.unitOfWork.DbContext.Set<DocType>().SingleOrDefault(e => e.Alias.ToLower() == docTypeAlias.ToLower());
                        docSubject = docTypeAlias.Contains("ParentOnly") ? "Разпределение чрез задача върху документ" : "Разпределение чрез задача";
                        break;
                    case "remark":
                        docType = this.unitOfWork.DbContext.Set<DocType>().SingleOrDefault(e => e.Alias.ToLower() == docEntryTypeAlias.ToLower());
                        docSubject = "Допълнителна информация чрез забележка";
                        break;
                };

                Doc newDoc = this.docRepository.CreateDoc(
                    internalDocDirection.DocDirectionId,
                    documentEntryType.DocEntryTypeId,
                    draftStatus.DocStatusId,
                    docSubject,
                    internalDocCasePartType.DocCasePartTypeId,
                    null,
                    null,
                    docType.DocTypeId,
                    paperDocFormatType.DocFormatTypeId,
                    null,
                    userContext);

                DocRelation parentDocRelation = this.unitOfWork.DbContext.Set<DocRelation>()
                    .FirstOrDefault(e => e.DocId == id);

                List<DocTypeUnitRole> docTypeUnitRoles = this.unitOfWork.DbContext.Set<DocTypeUnitRole>()
                    .Where(e => e.DocDirectionId == newDoc.DocDirectionId && e.DocTypeId == newDoc.DocTypeId)
                    .ToList();

                DocUnitRole importedBy = this.unitOfWork.DbContext.Set<DocUnitRole>()
                        .SingleOrDefault(e => e.Alias == "ImportedBy");

                List<DocTypeClassification> docTypeClassifications = null;
                List<DocClassification> parentDocClassifications = null;

                if (parentDocRelation == null)
                {
                    docTypeClassifications = this.unitOfWork.DbContext.Set<DocTypeClassification>()
                        .Where(e => e.DocDirectionId == newDoc.DocDirectionId &&
                            e.DocTypeId == newDoc.DocTypeId &&
                            e.IsActive)
                        .ToList();
                }
                else
                {
                    parentDocClassifications = this.unitOfWork.DbContext.Set<DocClassification>()
                        .Where(e => e.DocId == parentDocRelation.DocId &&
                            e.IsActive &&
                            e.IsInherited)
                        .ToList();
                }

                newDoc.CreateDocProperties(
                       parentDocRelation,
                       internalDocCasePartType.DocCasePartTypeId,
                       docTypeClassifications,
                       parentDocClassifications,
                       null,
                       docTypeUnitRoles,
                       importedBy,
                       unitUser,
                       null,
                       null,
                       this.userContext);

                this.unitOfWork.Save();

                this.docRepository.ExecSpSetDocTokens(docId: newDoc.DocId);
                this.docRepository.ExecSpSetDocUnitTokens(docId: newDoc.DocId);

                transaction.Commit();

                return Ok(new
                    {
                        docId = newDoc.DocId
                    });
            }
        }

        [HttpPost]
        public IHttpActionResult CreateChildPubliclDoc(int id, NewPublicDocDO publicDoc)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);

                ClassificationPermission managementPermission = this.classificationRepository.GetByAlias("Management");
                ClassificationPermission readPermission = this.classificationRepository.GetByAlias("Read");
                ClassificationPermission registerPermission = this.classificationRepository.GetByAlias("Register");

                bool hasManagementPermission =
                    this.classificationRepository.HasPermission(unitUser.UnitId, id, managementPermission.ClassificationPermissionId) &&
                    this.classificationRepository.HasPermission(unitUser.UnitId, id, readPermission.ClassificationPermissionId);

                if (!hasManagementPermission)
                {
                    return Unauthorized();
                }

                DocEntryType documentEntryType = this.unitOfWork.DbContext.Set<DocEntryType>()
                    .SingleOrDefault(e => e.Alias == "Document");
                DocDirection outgoingDocDirection = this.unitOfWork.DbContext.Set<DocDirection>()
                    .SingleOrDefault(e => e.Alias == "Outgoing");
                DocFormatType electronicDocFormatType = this.unitOfWork.DbContext.Set<DocFormatType>()
                    .SingleOrDefault(e => e.Alias == "Electronic");
                DocStatus draftStatus = this.unitOfWork.DbContext.Set<DocStatus>()
                    .SingleOrDefault(e => e.Alias == "Draft");
                DocCasePartType publicDocCasePartType = this.unitOfWork.DbContext.Set<DocCasePartType>()
                    .SingleOrDefault(e => e.Alias == "Public");
                DocType docType = this.unitOfWork.DbContext.Set<DocType>()
                    .SingleOrDefault(e => e.Alias.ToLower() == publicDoc.DocTypeAlias.ToLower());

                Doc newDoc = this.docRepository.CreateDoc(
                    outgoingDocDirection.DocDirectionId,
                    documentEntryType.DocEntryTypeId,
                    draftStatus.DocStatusId,
                    docType.Name,
                    publicDocCasePartType.DocCasePartTypeId,
                    null,
                    null,
                    docType.DocTypeId,
                    electronicDocFormatType.DocFormatTypeId,
                    null,
                    userContext);

                DocRelation parentDocRelation = this.unitOfWork.DbContext.Set<DocRelation>()
                    .FirstOrDefault(e => e.DocId == id);

                List<DocTypeUnitRole> docTypeUnitRoles = this.unitOfWork.DbContext.Set<DocTypeUnitRole>()
                    .Where(e => e.DocDirectionId == newDoc.DocDirectionId && e.DocTypeId == newDoc.DocTypeId)
                    .ToList();

                DocUnitRole importedBy = this.unitOfWork.DbContext.Set<DocUnitRole>()
                        .SingleOrDefault(e => e.Alias == "ImportedBy");

                List<DocClassification> parentDocClassifications = this.unitOfWork.DbContext.Set<DocClassification>()
                        .Where(e => e.DocId == parentDocRelation.DocId &&
                            e.IsActive &&
                            e.IsInherited)
                        .ToList();

                newDoc.CreateDocProperties(
                    parentDocRelation,
                    publicDocCasePartType.DocCasePartTypeId,
                    null,
                    parentDocClassifications,
                    null,
                    docTypeUnitRoles,
                    importedBy,
                    unitUser,
                    publicDoc.Correspondents,
                    null,
                    this.userContext);

                DocUnitRole from = this.unitOfWork.DbContext.Set<DocUnitRole>()
                    .SingleOrDefault(e => e.Alias == "From");

                newDoc.CreateDocUnit(unitUser.UnitId, from.DocUnitRoleId, this.userContext);

                DocUnitRole madeBy = this.unitOfWork.DbContext.Set<DocUnitRole>()
                    .SingleOrDefault(e => e.Alias == "MadeBy");

                newDoc.CreateDocUnit(unitUser.UnitId, madeBy.DocUnitRoleId, this.userContext);

                this.unitOfWork.Save();

                this.docRepository.RegisterDoc(newDoc, unitUser, this.userContext);

                byte[] eDocFileContent = CreateRioObject(docType.ElectronicServiceFileTypeUri, parentDocRelation.RootDocId, id, newDoc);

                if (eDocFileContent != null)
                {
                    Guid eDocFileBlobKey = Guid.Empty;

                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
                    {
                        connection.Open();
                        using (var blobWriter = new BlobWriter(connection))
                        using (var stream = blobWriter.OpenStream())
                        {
                            stream.Write(eDocFileContent, 0, eDocFileContent.Length);
                            eDocFileBlobKey = blobWriter.GetBlobKey();
                        }
                    }

                    DocFileKind publicDocFileKind = this.unitOfWork.DbContext.Set<DocFileKind>().SingleOrDefault(e => e.Alias == "PublicAttachedFile");
                    DocFileOriginType editableDocFileOriginType = this.unitOfWork.DbContext.Set<DocFileOriginType>().SingleOrDefault(e => e.Alias == "EditableFile");
                    DocFileType docFileType = this.unitOfWork.DbContext.Set<DocFileType>().SingleOrDefault(e => e.DocTypeUri == docType.ElectronicServiceFileTypeUri);

                    newDoc.CreateDocFile(
                        publicDocFileKind.DocFileKindId,
                        docFileType.DocFileTypeId,
                        editableDocFileOriginType.DocFileOriginTypeId,
                        "Електронен документ",
                        "E-Document.xml",
                        String.Empty,
                        eDocFileBlobKey,
                        userContext);
                }

                this.unitOfWork.Save();

                this.docRepository.ExecSpSetDocTokens(docId: newDoc.DocId);
                this.docRepository.ExecSpSetDocUnitTokens(docId: newDoc.DocId);

                newDoc.SetReceiptOrder(this.docRepository.GetNextReceiptOrder(newDoc.DocId), this.userContext);

                bool hasRegisterPermission =
                    this.classificationRepository.HasPermission(unitUser.UnitId, newDoc.DocId, registerPermission.ClassificationPermissionId);
                if (!hasRegisterPermission)
                {
                    return Unauthorized();
                }

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new
                {
                    docId = newDoc.DocId
                });
            }
        }

        /// <summary>
        /// Преглед на документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetDoc(int id)
        {
            DateTime currentDate = DateTime.Now;

            UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>()
                .Include(e => e.User)
                .Include(e => e.Unit)
                .FirstOrDefault(e => e.UserId == this.userContext.UserId);

            List<vwDocUser> vwDocUsers = this.docRepository.GetvwDocUsersForDocByUnitId(id, unitUser);

            ClassificationPermission readPermission = this.unitOfWork.DbContext.Set<ClassificationPermission>()
                .SingleOrDefault(e => e.Alias == "Read");

            if (!vwDocUsers.Any(e => e.ClassificationPermissionId == readPermission.ClassificationPermissionId))
            {
                return BadRequest("Not enough permissions!");
            }

            var doc = this.docRepository.Find(id,
                e => e.DocStatus,
                e => e.DocEntryType,
                e => e.DocCasePartType,
                e => e.DocDirection,
                e => e.DocType,
                e => e.DocSourceType);

            if (doc == null)
            {
                return NotFound();
            }

            #region Load

            this.unitOfWork.DbContext.Set<DocHasRead>()
            .Where(e => e.DocId == id)
            .ToList();

            this.unitOfWork.DbContext.Set<DocFile>()
             .Include(e => e.DocFileOriginType)
             .Include(e => e.DocFileType)
             .Include(e => e.DocFileKind)
             .Where(e => e.DocId == id)
             .ToList();

            this.unitOfWork.DbContext.Set<DocUnit>()
             .Include(e => e.Unit.UnitRelations.Select(er => er.ParentUnit))
             .Include(e => e.DocUnitRole)
             .Where(e => e.DocId == id)
             .ToList();

            this.unitOfWork.DbContext.Set<DocCorrespondent>()
             .Include(e => e.Correspondent.CorrespondentType)
             .Where(e => e.DocId == id)
             .ToList();

            this.unitOfWork.DbContext.Set<DocCorrespondentContact>()
             .Include(e => e.CorrespondentContact.Correspondent)
             .Where(e => e.DocId == id)
             .ToList();

            this.unitOfWork.DbContext.Set<DocWorkflow>()
                .Include(e => e.DocWorkflowAction)
                .Include(e => e.ToUnit)
                .Include(e => e.PrincipalUnit)
                .Where(e => e.DocId == id)
                .ToList();

            this.unitOfWork.DbContext.Set<DocClassification>()
             .Include(e => e.Classification)
             .Where(e => e.DocId == id)
             .ToList();

            #endregion

            var returnValue = new DocDO(doc, unitUser);

            #region DocCorrespondents

            foreach (var dc in doc.DocCorrespondents)
            {
                returnValue.DocCorrespondents.Add(new NomDo(dc));
            }

            #endregion

            #region DocWorkflows

            foreach (var dw in doc.DocWorkflows.OrderBy(e => e.DocWorkflowId))
            {
                returnValue.DocWorkflows.Add(new DocWorkflowDO(dw));
            }

            #endregion

            #region DocUnits

            List<DocUnitRole> docUnitRoles = this.unitOfWork.DbContext.Set<DocUnitRole>().ToList();
            foreach (var du in doc.DocUnits)
            {
                switch (du.DocUnitRole.Alias)
                {
                    case "From":
                        returnValue.DocUnitsFrom.Add(new NomDo(du));
                        break;
                    case "To":
                        returnValue.DocUnitsTo.Add(new NomDo(du));
                        break;
                    case "ImportedBy":
                        returnValue.DocUnitsImportedBy.Add(new NomDo(du));
                        break;
                    case "MadeBy":
                        returnValue.DocUnitsMadeBy.Add(new NomDo(du));
                        break;
                    case "CCopy":
                        returnValue.DocUnitsCCopy.Add(new NomDo(du));
                        break;
                    case "InCharge":
                        returnValue.DocUnitsInCharge.Add(new NomDo(du));
                        break;
                    case "Controlling":
                        returnValue.DocUnitsControlling.Add(new NomDo(du));
                        break;
                    case "Readers":
                        returnValue.DocUnitsReaders.Add(new NomDo(du));
                        break;
                    case "Editors":
                        returnValue.DocUnitsEditors.Add(new NomDo(du));
                        break;
                    case "Registrators":
                        returnValue.DocUnitsRegistrators.Add(new NomDo(du));
                        break;
                };
            }

            #endregion

            #region DocClassifications

            foreach (var dc in doc.DocClassifications)
            {
                returnValue.DocClassifications.Add(new DocClassificationDO(dc));
            }

            #endregion

            #region DocFiles

            DocFile editable = doc.DocFiles.FirstOrDefault(e => e.DocFileOriginTypeId.HasValue && e.DocFileOriginType.Alias == "EditableFile");
            if (editable != null)
            {
                returnValue.JObjectForm = editable.DocFileType.DocTypeUri;
            }

            foreach (var df in doc.DocFiles.Where(e => !e.DocFileOriginTypeId.HasValue || e.DocFileOriginType.Alias != "EditableFile"))
            {
                if (df.DocFileKind.Alias.ToLower() == "PrivateAttachedFile".ToLower())
                {
                    returnValue.PrivateDocFiles.Add(new DocFileDO(df));
                }
                else if (df.DocFileKind.Alias.ToLower() == "PublicAttachedFile".ToLower())
                {
                    returnValue.PublicDocFiles.Add(new DocFileDO(df));
                }
            }

            #endregion

            #region DocRelations

            returnValue.DocRelations.AddRange(
                this.docRepository.GetCaseRelationsByDocId(id,
                    e => e.Doc.DocCasePartType,
                    e => e.Doc.DocCasePartMovements.Select(dc => dc.User),
                    e => e.Doc.DocDirection,
                    e => e.Doc.DocType,
                    e => e.Doc.DocStatus)
                   .Select(e => new DocRelationDO(e))
                );

            #endregion

            #region DocElectronicServiceStages

            returnValue.DocElectronicServiceStages.AddRange(
                this.docRepository.GetCaseElectronicServiceStagesByDocId(id,
                    e => e.ElectronicServiceStage.ElectronicServiceStageExecutors.Select(ee => ee.Unit))
                    .Select(e => new DocElectronicServiceStageDO(e))
                );

            #endregion

            #region Set permissions

            returnValue.CanRead = vwDocUsers.Any(e => e.DocId == doc.DocId && e.ClassificationPermission.Alias == "Read");

            returnValue.CanEdit = vwDocUsers.Any(e => e.DocId == doc.DocId && e.ClassificationPermission.Alias == "Edit");

            returnValue.CanRegister = vwDocUsers.Any(e => e.DocId == doc.DocId && e.ClassificationPermission.Alias == "Register");

            returnValue.CanPublicFileManagement = vwDocUsers.Any(e => e.DocId == doc.DocId && e.ClassificationPermission.Alias == "PublicFileManagement");

            returnValue.CanManagement = vwDocUsers.Any(e => e.DocId == doc.DocId && e.ClassificationPermission.Alias == "Management");

            returnValue.CanDocWorkflowSign = vwDocUsers.Any(e => e.DocId == doc.DocId && e.ClassificationPermission.Alias == "DocWorkflowSign");

            returnValue.CanDocWorkflowDiscuss = vwDocUsers.Any(e => e.DocId == doc.DocId && e.ClassificationPermission.Alias == "DocWorkflowDiscuss");

            returnValue.CanSubstituteManagement = vwDocUsers.Any(e => e.DocId == doc.DocId && e.ClassificationPermission.Alias == "SubstituteManagement");

            returnValue.CanDocWorkflowManagement = vwDocUsers.Any(e => e.DocId == doc.DocId && e.ClassificationPermission.Alias == "DocWorkflowManagement");

            returnValue.CanESign = vwDocUsers.Any(e => e.DocId == doc.DocId && e.ClassificationPermission.Alias == "ESign");

            returnValue.CanFinish = vwDocUsers.Any(e => e.DocId == doc.DocId && e.ClassificationPermission.Alias == "Finish");

            returnValue.CanReverse = vwDocUsers.Any(e => e.DocId == doc.DocId && e.ClassificationPermission.Alias == "Reverse");

            returnValue.CanEditTech = vwDocUsers.Any(e => e.DocId == doc.DocId && e.ClassificationPermission.Alias == "EditTechElectronicServiceStage");

            returnValue.CanEditTechElectronicServiceStage = vwDocUsers.Any(e => e.DocId == doc.DocId && e.ClassificationPermission.Alias == "EditTech");

            returnValue.CanDocCasePartManagement = vwDocUsers.Any(e => e.DocId == doc.DocId && e.ClassificationPermission.Alias == "DocCasePartManagement");

            returnValue.CanDocMovement = vwDocUsers.Any(e => e.DocId == doc.DocId && e.ClassificationPermission.Alias == "DocMovement");

            returnValue.CanSendMail = vwDocUsers.Any(e => e.DocId == doc.DocId && e.ClassificationPermission.Alias == "SendMail");

            #endregion

            returnValue.Set();

            return Ok(returnValue);
        }

        /// <summary>
        /// Редакция на документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <param name="doc">Нови данни на документ</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateDoc(int id, DocDO doc)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);

                ClassificationPermission editPermission = this.classificationRepository.GetByAlias("Edit");
                ClassificationPermission readPermission = this.classificationRepository.GetByAlias("Read");

                bool hasEditPermission =
                    this.classificationRepository.HasPermission(unitUser.UnitId, id, editPermission.ClassificationPermissionId) &&
                    this.classificationRepository.HasPermission(unitUser.UnitId, id, readPermission.ClassificationPermissionId);

                if (!hasEditPermission)
                {
                    return BadRequest("Not enough permissions!");
                }

                DateTime currentDate = DateTime.Now;
                var oldDoc = this.docRepository.Find(id,
                        e => e.DocCorrespondents,
                        e => e.DocFiles.Select(df => df.DocFileOriginType),
                        e => e.DocFiles.Select(df => df.DocFileType),
                        e => e.DocUnits);

                if (oldDoc == null)
                {
                    return NotFound();
                }

                oldDoc.EnsureForProperVersion(doc.Version);

                oldDoc.DocSourceTypeId = doc.DocSourceTypeId;
                oldDoc.DocSubject = doc.DocSubject;
                oldDoc.DocBody = doc.DocBody;
                oldDoc.CorrRegNumber = doc.CorrRegNumber;
                oldDoc.CorrRegDate = doc.CorrRegDate;
                oldDoc.AssignmentTypeId = doc.AssignmentTypeId;
                oldDoc.AssignmentDate = doc.AssignmentDate;
                oldDoc.AssignmentDeadline = doc.AssignmentDeadline;
                oldDoc.ModifyDate = DateTime.Now;
                oldDoc.ModifyUserId = userContext.UserId;

                #region DocUnits

                List<DocUnitRole> docUnitRoles = this.unitOfWork.DbContext.Set<DocUnitRole>().ToList();

                doc.DocUnitsFrom.ForEach(e => { e.ForeignKeyId = docUnitRoles.Single(p => p.Alias == "From").DocUnitRoleId; });
                doc.DocUnitsTo.ForEach(e => { e.ForeignKeyId = docUnitRoles.Single(p => p.Alias == "To").DocUnitRoleId; });
                doc.DocUnitsImportedBy.ForEach(e => { e.ForeignKeyId = docUnitRoles.Single(p => p.Alias == "ImportedBy").DocUnitRoleId; });
                doc.DocUnitsMadeBy.ForEach(e => { e.ForeignKeyId = docUnitRoles.Single(p => p.Alias == "MadeBy").DocUnitRoleId; });
                doc.DocUnitsCCopy.ForEach(e => { e.ForeignKeyId = docUnitRoles.Single(p => p.Alias == "CCopy").DocUnitRoleId; });
                doc.DocUnitsInCharge.ForEach(e => { e.ForeignKeyId = docUnitRoles.Single(p => p.Alias == "InCharge").DocUnitRoleId; });
                doc.DocUnitsControlling.ForEach(e => { e.ForeignKeyId = docUnitRoles.Single(p => p.Alias == "Controlling").DocUnitRoleId; });
                doc.DocUnitsReaders.ForEach(e => { e.ForeignKeyId = docUnitRoles.Single(p => p.Alias == "Readers").DocUnitRoleId; });
                doc.DocUnitsEditors.ForEach(e => { e.ForeignKeyId = docUnitRoles.Single(p => p.Alias == "Editors").DocUnitRoleId; });
                doc.DocUnitsRegistrators.ForEach(e => { e.ForeignKeyId = docUnitRoles.Single(p => p.Alias == "Registrators").DocUnitRoleId; });

                var allDocUnits = doc.DocUnitsTo.Union(
                    doc.DocUnitsFrom.Union(
                        doc.DocUnitsImportedBy.Union(
                            doc.DocUnitsMadeBy.Union(
                                doc.DocUnitsCCopy.Union(
                                    doc.DocUnitsInCharge.Union(
                                        doc.DocUnitsControlling.Union(
                                            doc.DocUnitsReaders.Union(
                                                doc.DocUnitsEditors.Union(
                                                    doc.DocUnitsRegistrators)))))))));

                var listDocUnits = oldDoc.DocUnits.ToList();

                for (var i = 0; i < listDocUnits.Count; i++)
                {
                    var matching = allDocUnits
                        .Where(e => e.NomValueId == listDocUnits[i].UnitId && e.ForeignKeyId == listDocUnits[i].DocUnitRoleId)
                        .ToList();

                    if (matching.Any())
                    {
                        matching.ForEach(e => { e.IsProcessed = true; });
                    }
                    else
                    {
                        oldDoc.DeleteDocUnit(listDocUnits[i], this.userContext);
                    }
                }
                foreach (var du in allDocUnits.Where(e => !e.IsProcessed))
                {
                    oldDoc.CreateDocUnit(du.NomValueId, du.ForeignKeyId, this.userContext);
                }

                #endregion

                #region DocCorrespondents

                var listDocCorrespondents = oldDoc.DocCorrespondents.ToList();

                for (var i = 0; i < listDocCorrespondents.Count; i++)
                {
                    var matching = doc.DocCorrespondents
                        .Where(e => e.NomValueId == listDocCorrespondents[i].CorrespondentId)
                        .ToList();

                    if (matching.Any())
                    {
                        matching.ForEach(e => { e.IsProcessed = true; });
                    }
                    else
                    {
                        oldDoc.DeleteDocCorrespondent(listDocCorrespondents[i], this.userContext);
                    }
                }
                foreach (var du in doc.DocCorrespondents.Where(e => !e.IsProcessed))
                {
                    oldDoc.CreateDocCorrespondent(du.NomValueId, this.userContext);
                }

                #endregion

                #region DocFiles

                DocFile editable = oldDoc.DocFiles.FirstOrDefault(e => e.DocFileOriginTypeId.HasValue && e.DocFileOriginType.Alias == "EditableFile");

                if (editable != null)
                {
                    byte[] content = null;

                    if (editable.DocFileType.DocTypeUri == "Checklist")
                    {
                        string contentStr = JsonConvert.SerializeObject(doc.JObject);
                        content = System.Text.Encoding.UTF8.GetBytes(contentStr);
                    }
                    else //doc is rio object
                    {
                        content = RioObjectUtils.GetBytesFromJObject(editable.DocFileType.DocTypeUri, doc.JObject);
                    }

                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
                    {
                        connection.Open();
                        using (var blobWriter = new BlobWriter(connection))
                        using (var stream = blobWriter.OpenStream())
                        {
                            stream.Write(content, 0, content.Length);
                            editable.DocFileContentId = blobWriter.GetBlobKey();
                        }
                    }
                }

                List<DocFileDO> allDocFiles = doc.DocFiles;
                List<DocFileType> docFileTypes = this.unitOfWork.DbContext.Set<DocFileType>().ToList();

                foreach (var file in allDocFiles.Where(e => !e.IsNew && e.IsDeleted && e.DocFileId.HasValue))
                {
                    oldDoc.DeleteDocFile(file.DocFileId.Value, this.userContext);
                }

                foreach (var file in allDocFiles.Where(e => !e.IsNew && !e.IsDeleted && e.IsDirty && e.DocFileId.HasValue))
                {
                    var docFileType = docFileTypes.FirstOrDefault(e => e.Extention == Path.GetExtension(file.File.Name));

                    if (docFileType == null)
                    {
                        docFileType = docFileTypes.FirstOrDefault(e => e.Alias == "UnknownBinary");
                    }

                    oldDoc.UpdateDocFile(file.DocFileId.Value, file.DocFileKindId, docFileType.DocFileTypeId, file.Name, file.File.Name, "", file.File.Key, userContext);
                }

                foreach (var file in allDocFiles.Where(e => e.IsNew && !e.IsDeleted))
                {
                    var docFileType = docFileTypes.FirstOrDefault(e => e.Extention == Path.GetExtension(file.File.Name));

                    if (docFileType == null)
                    {
                        docFileType = docFileTypes.FirstOrDefault(e => e.Alias == "UnknownBinary");
                    }

                    oldDoc.CreateDocFile(file.DocFileKindId, docFileType.DocFileTypeId, file.Name, file.File.Name, String.Empty, file.File.Key, userContext);
                }

                #endregion

                this.unitOfWork.Save();

                this.docRepository.ExecSpSetDocTokens(docId: oldDoc.DocId);
                this.docRepository.ExecSpSetDocUnitTokens(docId: oldDoc.DocId);

                transaction.Commit();

                return Ok();
            }
        }

        /// <summary>
        /// Редакция на раздел на преписка, в който се намира документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <param name="docCasePartTypeId">Идентификатор на тип раздел</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateDocCasePartType(int id, string docVersion, int docCasePartTypeId)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);

                ClassificationPermission docCasePartManagementPermission = this.classificationRepository.GetByAlias("DocCasePartManagement");
                ClassificationPermission readPermission = this.classificationRepository.GetByAlias("Read");

                bool hasDocCasePartManagementPermission =
                    this.classificationRepository.HasPermission(unitUser.UnitId, id, docCasePartManagementPermission.ClassificationPermissionId) &&
                    this.classificationRepository.HasPermission(unitUser.UnitId, id, readPermission.ClassificationPermissionId);

                if (!hasDocCasePartManagementPermission)
                {
                    return BadRequest("Not enough permissions!");
                }

                Doc doc = this.docRepository.UpdateDocCasePartType(
                    id,
                    Helper.StringToVersion(docVersion),
                    docCasePartTypeId, this.userContext);

                this.unitOfWork.Save();

                this.docRepository.ExecSpSetDocTokens(docId: doc.DocId);
                this.docRepository.ExecSpSetDocUnitTokens(docId: doc.DocId);

                transaction.Commit();

                return Ok();
            }
        }

        [HttpPost]
        public IHttpActionResult UpdateTechDoc(int id, string docVersion, DocDO doc)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);

                ClassificationPermission editTechPermission = this.classificationRepository.GetByAlias("EditTech");
                ClassificationPermission readPermission = this.classificationRepository.GetByAlias("Read");

                bool hasEditTechPermission =
                    this.classificationRepository.HasPermission(unitUser.UnitId, id, editTechPermission.ClassificationPermissionId) &&
                    this.classificationRepository.HasPermission(unitUser.UnitId, id, readPermission.ClassificationPermissionId);

                if (!hasEditTechPermission)
                {
                    return BadRequest("Not enough permissions!");
                }

                Doc oldDoc = this.docRepository.Find(id,
                    e => e.DocType,
                    e => e.DocUnits);

                if (oldDoc == null)
                {
                    return NotFound();
                }

                if (doc.UnregisterDoc)
                {
                    oldDoc.Unregister(this.userContext);
                }

                if (doc.PrimaryRegisterIndexId.HasValue)
                {
                    DocRegister docRegister = this.unitOfWork.DbContext.Set<DocRegister>()
                        .FirstOrDefault(e => e.RegisterIndexId == doc.PrimaryRegisterIndexId.Value);

                    if (docRegister != null)
                    {
                        oldDoc.DocRegisterId = docRegister.DocRegisterId;
                    }
                    else
                    {
                        oldDoc.DocRegisterId = this.docRepository.spGetDocRegisterIdByRegisterIndexId(doc.PrimaryRegisterIndexId.Value);
                    }
                }

                if (oldDoc.DocTypeId != doc.DocTypeId || oldDoc.DocDirectionId != doc.DocDirectionId)
                {
                    #region DocUnits

                    List<DocUnitRole> docUnitRoles = this.unitOfWork.DbContext.Set<DocUnitRole>().ToList();

                    doc.DocUnitsFrom.ForEach(e => { e.ForeignKeyId = docUnitRoles.Single(p => p.Alias == "From").DocUnitRoleId; });
                    doc.DocUnitsTo.ForEach(e => { e.ForeignKeyId = docUnitRoles.Single(p => p.Alias == "To").DocUnitRoleId; });
                    doc.DocUnitsImportedBy.ForEach(e => { e.ForeignKeyId = docUnitRoles.Single(p => p.Alias == "ImportedBy").DocUnitRoleId; });
                    doc.DocUnitsMadeBy.ForEach(e => { e.ForeignKeyId = docUnitRoles.Single(p => p.Alias == "MadeBy").DocUnitRoleId; });
                    doc.DocUnitsCCopy.ForEach(e => { e.ForeignKeyId = docUnitRoles.Single(p => p.Alias == "CCopy").DocUnitRoleId; });
                    doc.DocUnitsInCharge.ForEach(e => { e.ForeignKeyId = docUnitRoles.Single(p => p.Alias == "InCharge").DocUnitRoleId; });
                    doc.DocUnitsControlling.ForEach(e => { e.ForeignKeyId = docUnitRoles.Single(p => p.Alias == "Controlling").DocUnitRoleId; });
                    doc.DocUnitsReaders.ForEach(e => { e.ForeignKeyId = docUnitRoles.Single(p => p.Alias == "Readers").DocUnitRoleId; });
                    doc.DocUnitsEditors.ForEach(e => { e.ForeignKeyId = docUnitRoles.Single(p => p.Alias == "Editors").DocUnitRoleId; });
                    doc.DocUnitsRegistrators.ForEach(e => { e.ForeignKeyId = docUnitRoles.Single(p => p.Alias == "Registrators").DocUnitRoleId; });

                    var allDocUnits = doc.DocUnitsTo.Union(
                        doc.DocUnitsFrom.Union(
                            doc.DocUnitsImportedBy.Union(
                                doc.DocUnitsMadeBy.Union(
                                    doc.DocUnitsCCopy.Union(
                                        doc.DocUnitsInCharge.Union(
                                            doc.DocUnitsControlling.Union(
                                                doc.DocUnitsReaders.Union(
                                                    doc.DocUnitsEditors.Union(
                                                        doc.DocUnitsRegistrators)))))))));

                    var listDocUnits = oldDoc.DocUnits.ToList();

                    for (var i = 0; i < listDocUnits.Count; i++)
                    {
                        var matching = allDocUnits
                            .Where(e => e.NomValueId == listDocUnits[i].UnitId && e.ForeignKeyId == listDocUnits[i].DocUnitRoleId)
                            .ToList();

                        if (matching.Any())
                        {
                            matching.ForEach(e => { e.IsProcessed = true; });
                        }
                        else
                        {
                            oldDoc.DeleteDocUnit(listDocUnits[i], this.userContext);
                        }
                    }
                    foreach (var du in allDocUnits.Where(e => !e.IsProcessed))
                    {
                        oldDoc.CreateDocUnit(du.NomValueId, du.ForeignKeyId, this.userContext);
                    }

                    #endregion

                    oldDoc.DocTypeId = doc.DocTypeId;
                    oldDoc.DocDirectionId = doc.DocDirectionId;
                }

                oldDoc.ReceiptOrder = doc.ReceiptOrder;

                this.unitOfWork.Save();

                this.docRepository.ExecSpSetDocTokens(docId: oldDoc.DocId);
                this.docRepository.ExecSpSetDocUnitTokens(docId: oldDoc.DocId);

                transaction.Commit();

                return Ok();
            }
        }

        [HttpPost]
        public IHttpActionResult RegisterDoc(int id, string docVersion)
        {
            UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);

            ClassificationPermission registerPermission = this.classificationRepository.GetByAlias("Register");
            ClassificationPermission readPermission = this.classificationRepository.GetByAlias("Read");

            bool hasRegisterPermission =
                this.classificationRepository.HasPermission(unitUser.UnitId, id, registerPermission.ClassificationPermissionId) &&
                this.classificationRepository.HasPermission(unitUser.UnitId, id, readPermission.ClassificationPermissionId);

            if (!hasRegisterPermission)
            {
                return BadRequest("Not enough permissions!");
            }

            Doc doc = this.docRepository.Find(id,
                e => e.DocRelations);

            string result = this.docRepository.RegisterDoc(
                doc,
                unitUser,
                this.userContext,
                true,
                Helper.StringToVersion(docVersion));

            this.unitOfWork.Save();

            return Ok(new
            {
                result = result
            });
        }

        [HttpPost]
        public IHttpActionResult ManualRegisterDoc(int id, string docVersion, string regUri, DateTime regDate)
        {
            UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);

            ClassificationPermission registerPermission = this.classificationRepository.GetByAlias("Register");
            ClassificationPermission readPermission = this.classificationRepository.GetByAlias("Read");

            bool hasRegisterPermission =
                this.classificationRepository.HasPermission(unitUser.UnitId, id, registerPermission.ClassificationPermissionId) &&
                this.classificationRepository.HasPermission(unitUser.UnitId, id, readPermission.ClassificationPermissionId);

            if (!hasRegisterPermission)
            {
                return BadRequest("Not enough permissions!");
            }

            Doc doc = this.docRepository.Find(id,
                e => e.DocRelations);

            string result = this.docRepository.ManualRegisterDoc(
                doc,
                unitUser,
                this.userContext,
                regUri,
                regDate,
                true,
                Helper.StringToVersion(docVersion));

            this.unitOfWork.Save();

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetDocRegisterIndex(int id)
        {
            Doc doc = this.docRepository.Find(id,
                e => e.DocRegister,
                e => e.DocType);

            // no usage of secondary register index for now
            if (doc.DocRegisterId.HasValue)
            {
                return Ok(new
                {
                    primaryRegisterIndexId = doc.DocRegister.RegisterIndexId,
                    secondaryRegisterIndexId = doc.DocType.SecondaryRegisterIndexId
                });
            }
            else
            {
                return Ok(new
                {
                    primaryRegisterIndexId = doc.DocType.PrimaryRegisterIndexId,
                    secondaryRegisterIndexId = doc.DocType.SecondaryRegisterIndexId
                });
            }
        }

        /// <summary>
        /// Управление на документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <param name="docVersion">Версия на документ</param>
        /// <param name="closure">Автоматично приключване на всички подчиненни документи</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult SetNextStatus(
            int id,
            string docVersion,
            string alias,
            [FromUri] int[] checkedIds = null,
            bool? closure = null
            )
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);

                ClassificationPermission readPermission = this.classificationRepository.GetByAlias("Read");

                bool hasReadPermission = this.classificationRepository.HasPermission(unitUser.UnitId, id, readPermission.ClassificationPermissionId);

                if (!hasReadPermission)
                {
                    return BadRequest("Not enough permissions!");
                }

                Doc doc = this.docRepository.Find(id,
                    e => e.DocType,
                    e => e.DocRelations,
                    e => e.DocUnits.Select(d => d.Unit),
                    e => e.DocEntryType);

                if (doc == null)
                {
                    return NotFound();
                }

                List<DocStatus> docStatuses = this.unitOfWork.DbContext.Set<DocStatus>().ToList();
                List<DocCasePartType> docCasePartTypes = this.unitOfWork.DbContext.Set<DocCasePartType>().ToList();
                List<DocRelation> docRelations;

                string targetDocStatusAlias = alias;

                if (string.IsNullOrEmpty(alias))
                {
                    this.docRepository.NextDocStatus(
                        id,
                        Helper.StringToVersion(docVersion),
                        closure ?? false,
                        docStatuses,
                        docCasePartTypes,
                        checkedIds,
                        this.userContext,
                        out targetDocStatusAlias,
                        out docRelations);
                }
                else
                {
                    this.docRepository.NextDocStatus(
                       id,
                       Helper.StringToVersion(docVersion),
                       alias,
                       closure ?? false,
                       docStatuses,
                       docCasePartTypes,
                       checkedIds,
                       this.userContext,
                       out docRelations);
                }

                if (docRelations.Any())
                {
                    List<DocRelationDO> docRelationDOs = docRelations.Select(e => new DocRelationDO(e)).ToList();

                    return Ok(new
                        {
                            docRelations = docRelationDOs
                        });
                }

                this.unitOfWork.Save();

                bool hasStagePermission = false;

                switch (targetDocStatusAlias)
                {
                    case "Prepared":
                        ClassificationPermission tmp0 = this.classificationRepository.GetByAlias("Edit");

                        hasStagePermission = this.classificationRepository.HasPermission(unitUser.UnitId, id, tmp0.ClassificationPermissionId);
                        break;
                    case "Processed":
                        ClassificationPermission tmp1 = this.classificationRepository.GetByAlias("Edit");
                        ClassificationPermission tmp2 = this.classificationRepository.GetByAlias("Management");

                        hasStagePermission =
                            this.classificationRepository.HasPermission(unitUser.UnitId, id, tmp1.ClassificationPermissionId) ||
                            this.classificationRepository.HasPermission(unitUser.UnitId, id, tmp2.ClassificationPermissionId);
                        break;
                    case "Finished":
                        ClassificationPermission tmp3 = this.classificationRepository.GetByAlias("Finish");

                        hasStagePermission = this.classificationRepository.HasPermission(unitUser.UnitId, id, tmp3.ClassificationPermissionId);
                        break;
                    default:
                        throw new Exception("Unappropriate status for Doc");
                };

                if (!hasStagePermission)
                {
                    return BadRequest("Not enough permissions!");
                }

                if (doc.DocEntryType.Alias == "Resolution" || doc.DocEntryType.Alias == "Task")
                {
                    if (targetDocStatusAlias == "Finished")
                    {
                        Email email = this.emailRepository.CreateResolutionEmail(doc, Request);

                        this.unitOfWork.DbContext.Set<Email>().Add(email);
                    }
                }
                else if (doc.DocEntryType.Alias == "Document" || doc.DocEntryType.Alias == "Remark")
                {
                    if (targetDocStatusAlias == "Processed")
                    {
                        Email email = this.emailRepository.CreateDocumentEmail(doc, Request);

                        this.unitOfWork.DbContext.Set<Email>().Add(email);
                    }
                }

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok();
            }
        }

        /// <summary>
        /// Анулиране на документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <param name="docVersion">Версия на документ</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CancelDoc(
            int id,
            string docVersion)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);

                ClassificationPermission finishPermission = this.classificationRepository.GetByAlias("Finish");
                ClassificationPermission readPermission = this.classificationRepository.GetByAlias("Read");

                bool hasDocMovementPermission =
                    this.classificationRepository.HasPermission(unitUser.UnitId, id, finishPermission.ClassificationPermissionId) &&
                    this.classificationRepository.HasPermission(unitUser.UnitId, id, readPermission.ClassificationPermissionId);

                if (!hasDocMovementPermission)
                {
                    return BadRequest("Not enough permissions!");
                }

                DocStatus cancelDocStatus = this.unitOfWork.DbContext.Set<DocStatus>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "Canceled".ToLower());

                this.docRepository.CancelDoc(
                    id,
                    Helper.StringToVersion(docVersion),
                    cancelDocStatus,
                    this.userContext);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok();
            }
        }

        /// <summary>
        /// Сторниране на управление на документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <param name="docVersion">Версия на документ</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ReverseStatus(
            int id,
            string docVersion,
            string alias = null)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);

                ClassificationPermission reversePermission = this.classificationRepository.GetByAlias("Reverse");
                ClassificationPermission readPermission = this.classificationRepository.GetByAlias("Read");

                bool hasReversePermission =
                    this.classificationRepository.HasPermission(unitUser.UnitId, id, reversePermission.ClassificationPermissionId) &&
                    this.classificationRepository.HasPermission(unitUser.UnitId, id, readPermission.ClassificationPermissionId);

                if (!hasReversePermission)
                {
                    return BadRequest("Not enough permissions!");
                }

                List<DocStatus> docStatuses = this.unitOfWork.DbContext.Set<DocStatus>().ToList();

                if (string.IsNullOrEmpty(alias))
                {
                    this.docRepository.ReverseDocStatus(
                        id,
                        Helper.StringToVersion(docVersion),
                        docStatuses,
                        this.userContext);
                }
                else
                {
                    this.docRepository.ReverseDocStatus(
                        id,
                        Helper.StringToVersion(docVersion),
                        alias,
                        docStatuses,
                        this.userContext);
                }

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok();
            }
        }

        [HttpPost]
        public IHttpActionResult CreateDocWorkflow(int id, string docVersion, DocWorkflowDO docWorkflow)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                Doc doc = this.docRepository.Find(id,
                    e => e.DocType,
                    e => e.DocRelations);

                if (doc == null)
                {
                    return NotFound();
                }

                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);

                ClassificationPermission readPermission = this.classificationRepository.GetByAlias("Read");

                DocStatus preparedStatus = this.classificationRepository.GetDocStatusByAlias("Prepared");

                bool isRequest = false;
                string[] requests = { "signrequest", "discussrequest", "approvalrequest", "registrationrequest" };
                string dwAlias = docWorkflow.DocWorkflowActionAlias.ToLower();

                if (requests.Contains(dwAlias))
                {
                    isRequest = true;

                    ClassificationPermission managementPermission = this.classificationRepository.GetByAlias("Management");
                    ClassificationPermission editPermission = this.classificationRepository.GetByAlias("Edit");

                    bool hasPermission = (doc.DocStatusId == preparedStatus.DocStatusId) &&
                        (this.classificationRepository.HasPermission(unitUser.UnitId, id, managementPermission.ClassificationPermissionId) ||
                            this.classificationRepository.HasPermission(unitUser.UnitId, id, editPermission.ClassificationPermissionId)) &&
                        this.classificationRepository.HasPermission(unitUser.UnitId, id, readPermission.ClassificationPermissionId);

                    if (!hasPermission)
                    {
                        return BadRequest("Not enough permissions!");
                    }
                }
                else if (dwAlias == "Sign".ToLower())
                {
                    isRequest = false;

                    ClassificationPermission docWorkflowSignPermission = this.classificationRepository.GetByAlias("DocWorkflowSign");

                    bool hasPermission = (doc.DocStatusId == preparedStatus.DocStatusId) &&
                        this.classificationRepository.HasPermission(unitUser.UnitId, id, docWorkflowSignPermission.ClassificationPermissionId) &&
                        this.classificationRepository.HasPermission(unitUser.UnitId, id, readPermission.ClassificationPermissionId);

                    if (!hasPermission)
                    {
                        return BadRequest("Not enough permissions!");
                    }
                }
                else if (dwAlias == "Discuss".ToLower() || dwAlias == "Approval".ToLower())
                {
                    isRequest = false;

                    ClassificationPermission docWorkflowDiscussPermission = this.classificationRepository.GetByAlias("DocWorkflowDiscuss");

                    bool hasPermission = (doc.DocStatusId == preparedStatus.DocStatusId) &&
                        this.classificationRepository.HasPermission(unitUser.UnitId, id, docWorkflowDiscussPermission.ClassificationPermissionId) &&
                        this.classificationRepository.HasPermission(unitUser.UnitId, id, readPermission.ClassificationPermissionId);

                    if (!hasPermission)
                    {
                        return BadRequest("Not enough permissions!");
                    }
                }
                else
                {
                    throw new Exception("Unrecognized doc workflow");
                }

                var docWorkflowUnitUser = this.unitOfWork.DbContext.Set<UnitUser>().Find(docWorkflow.UnitUserId);

                if (!isRequest && docWorkflow.PrincipalUnitId.HasValue && docWorkflowUnitUser.UnitId != docWorkflow.PrincipalUnitId.Value)
                {
                    ClassificationPermission substituteManagementPermission = this.classificationRepository.GetByAlias("SubstituteManagement");

                    bool hasPermission = (doc.DocStatusId == preparedStatus.DocStatusId) &&
                        this.classificationRepository.HasPermission(unitUser.UnitId, id, substituteManagementPermission.ClassificationPermissionId) &&
                        this.classificationRepository.HasPermission(unitUser.UnitId, id, readPermission.ClassificationPermissionId);

                    if (!hasPermission)
                    {
                        return BadRequest("Not enough permissions!");
                    }
                }

                doc.EnsureForProperVersion(Helper.StringToVersion(docVersion));

                DocWorkflowAction docWorkflowAction = this.unitOfWork.DbContext.Set<DocWorkflowAction>()
                    .SingleOrDefault(e => e.Alias.ToLower() == docWorkflow.DocWorkflowActionAlias.ToLower());

                bool? yesNo = BooleanNomConvert.ToBool(docWorkflow.YesNo);

                DocWorkflow dw = doc.CreateDocWorkflow(
                    docWorkflowAction,
                    DateTime.Now,
                    yesNo,
                    docWorkflow.ToUnitId,
                    docWorkflow.PrincipalUnitId,
                    docWorkflow.Note,
                    docWorkflow.UnitUserId,
                    this.userContext);

                this.unitOfWork.Save();

                Email email = this.emailRepository.CreateDocWorkflowEmail(doc, dw, isRequest, Request);

                this.unitOfWork.DbContext.Set<Email>().Add(email);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok();
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteDocWorkflow(int id, int itemId, string docVersion)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);

                ClassificationPermission docWorkflowManagementPermission = this.classificationRepository.GetByAlias("DocWorkflowManagement");
                ClassificationPermission readPermission = this.classificationRepository.GetByAlias("Read");

                bool hasDocWorkflowManagementPermission =
                    this.classificationRepository.HasPermission(unitUser.UnitId, id, docWorkflowManagementPermission.ClassificationPermissionId) &&
                    this.classificationRepository.HasPermission(unitUser.UnitId, id, readPermission.ClassificationPermissionId);

                if (!hasDocWorkflowManagementPermission)
                {
                    return BadRequest("Not enough permissions!");
                }

                Doc doc = this.docRepository.Find(id,
                    e => e.DocWorkflows);

                if (doc == null)
                {
                    return NotFound();
                }

                doc.EnsureForProperVersion(Helper.StringToVersion(docVersion));

                doc.DeleteDocWorkflow(itemId, this.userContext);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok();
            }
        }

        [HttpPost]
        public IHttpActionResult CreateDocElectronicServiceStage(
            int id,
            string docVersion,
            DocElectronicServiceStageDO docElectronicServiceStage)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                Doc doc = this.docRepository.Find(this.docRepository.GetCaseId(id),
                    e => e.DocCorrespondents.Select(d => d.Correspondent),
                    e => e.DocElectronicServiceStages);

                doc.EnsureForProperVersion(Helper.StringToVersion(docVersion));

                DocElectronicServiceStage dess = doc.CreateDocElectronicServiceStage(
                    docElectronicServiceStage.ElectronicServiceStageId,
                    docElectronicServiceStage.StartingDate,
                    docElectronicServiceStage.ExpectedEndingDate,
                    docElectronicServiceStage.EndingDate,
                    true,
                    this.userContext);

                this.unitOfWork.Save();

                Email email = this.emailRepository.CreateElectronicServiceStageChangeEmail(doc, dess, Request);

                this.unitOfWork.DbContext.Set<Email>().Add(email);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok();
            }
        }

        [HttpGet]
        public IHttpActionResult GetCurrentDocElectronicServiceStage(int id, string docVersion)
        {
            Doc doc = this.docRepository.Find(this.docRepository.GetCaseId(id),
                e => e.DocElectronicServiceStages);

            doc.EnsureForProperVersion(Helper.StringToVersion(docVersion));

            return Ok(doc.GetCurrentDocElectronicServiceStage() ?? doc.GetLatestDocElectronicServiceStage());
        }

        [HttpPost]
        public IHttpActionResult UpdateCurrentDocElectronicServiceStage(
            int id,
            string docVersion,
            DocElectronicServiceStageDO docElectronicServiceStage)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);

                int caseDocId = this.docRepository.GetCaseId(id);

                ClassificationPermission editTechElectronicServiceStagePermission = this.classificationRepository.GetByAlias("EditTechElectronicServiceStage");
                ClassificationPermission readPermission = this.classificationRepository.GetByAlias("Read");

                bool hasEditTechElectronicServiceStagePermission =
                    this.classificationRepository.HasPermission(unitUser.UnitId, caseDocId, editTechElectronicServiceStagePermission.ClassificationPermissionId) &&
                    this.classificationRepository.HasPermission(unitUser.UnitId, caseDocId, readPermission.ClassificationPermissionId);

                if (!hasEditTechElectronicServiceStagePermission)
                {
                    return BadRequest("Not enough permissions!");
                }

                Doc doc = this.docRepository.Find(caseDocId,
                    e => e.DocElectronicServiceStages);

                if (doc == null)
                {
                    return NotFound();
                }

                doc.EnsureForProperVersion(Helper.StringToVersion(docVersion));

                doc.UpdateCurrentDocElectronicServiceStage(
                    docElectronicServiceStage.ElectronicServiceStageId,
                    docElectronicServiceStage.StartingDate,
                    docElectronicServiceStage.ExpectedEndingDate,
                    docElectronicServiceStage.EndingDate,
                    this.userContext);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok();
            }
        }

        [HttpPost]
        public IHttpActionResult EndCurrentDocElectronicServiceStage(
            int id,
            string docVersion,
            DocElectronicServiceStageDO docElectronicServiceStage)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                Doc doc = this.docRepository.Find(this.docRepository.GetCaseId(id),
                    e => e.DocElectronicServiceStages);

                if (doc == null)
                {
                    return NotFound();
                }

                doc.EnsureForProperVersion(Helper.StringToVersion(docVersion));

                doc.EndCurrentDocElectronicServiceStage(docElectronicServiceStage.EndingDate.Value, this.userContext);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok();
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteCurrentDocElectronicServiceStage(int id, string docVersion)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);

                int caseDocId = this.docRepository.GetCaseId(id);

                ClassificationPermission editTechElectronicServiceStagePermission = this.classificationRepository.GetByAlias("EditTechElectronicServiceStage");
                ClassificationPermission readPermission = this.classificationRepository.GetByAlias("Read");

                bool hasEditTechElectronicServiceStagePermission =
                    this.classificationRepository.HasPermission(unitUser.UnitId, caseDocId, editTechElectronicServiceStagePermission.ClassificationPermissionId) &&
                    this.classificationRepository.HasPermission(unitUser.UnitId, caseDocId, readPermission.ClassificationPermissionId);

                if (!hasEditTechElectronicServiceStagePermission)
                {
                    return BadRequest("Not enough permissions!");
                }

                Doc doc = this.docRepository.Find(caseDocId,
                    e => e.DocElectronicServiceStages);

                if (doc == null)
                {
                    return NotFound();
                }

                doc.EnsureForProperVersion(Helper.StringToVersion(docVersion));

                doc.ReverseDocElectronicServiceStage(doc.GetCurrentDocElectronicServiceStage() ?? doc.GetLatestDocElectronicServiceStage(), this.userContext);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok();
            }
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead(int id, string docVersion)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);

                this.docRepository.MarkAsRead(
                    id,
                    Helper.StringToVersion(docVersion),
                    unitUser.UnitId,
                    this.userContext);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok();
            }
        }

        [HttpPost]
        public IHttpActionResult MarkAsUnread(int id, string docVersion)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);

                this.docRepository.MarkAsUnread(
                    id,
                    Helper.StringToVersion(docVersion),
                    unitUser.UnitId,
                    this.userContext);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok();
            }
        }

        [HttpPost]
        public IHttpActionResult CreateDocFileTicket(int id, int docFileId, Guid fileKey)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);

                ClassificationPermission editPermission = this.classificationRepository.GetByAlias("Edit");
                ClassificationPermission readPermission = this.classificationRepository.GetByAlias("Read");

                bool hasEditPermission =
                    this.classificationRepository.HasPermission(unitUser.UnitId, id, editPermission.ClassificationPermissionId) &&
                    this.classificationRepository.HasPermission(unitUser.UnitId, id, readPermission.ClassificationPermissionId);

                if (!hasEditPermission)
                {
                    return BadRequest("Not enough permissions!");
                }

                Ticket ticket = new Ticket();
                ticket.TicketId = Guid.NewGuid();
                ticket.DocFileId = docFileId;
                ticket.BlobOldKey = fileKey;
                ticket.VisualizationMode = (int)VisualizationMode.DisplayWithoutSignature;

                this.unitOfWork.DbContext.Set<Ticket>().Add(ticket);
                this.unitOfWork.Save();

                string portalAddress = ConfigurationManager.AppSettings["Docs.Api:PortalWebAddress"].ToString();
                string accessUrl = String.Format("{0}/Ais/Access?ticketId={1}", portalAddress, ticket.TicketId);

                transaction.Commit();

                return Ok(new { url = accessUrl });
            }
        }

        [HttpPost]
        public IHttpActionResult CreateAbbcdnTicket(string docTypeUri, Guid abbcdnKey)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                Ticket ticket = new Ticket();
                ticket.TicketId = Guid.NewGuid();
                ticket.DocTypeUri = docTypeUri;
                ticket.AbbcdnKey = abbcdnKey;
                ticket.VisualizationMode = (int)VisualizationMode.DisplayWithoutSignature;

                this.unitOfWork.DbContext.Set<Ticket>().Add(ticket);
                this.unitOfWork.Save();

                string portalAddress = ConfigurationManager.AppSettings["Docs.Api:PortalWebAddress"].ToString();
                string accessUrl = String.Format("{0}/Ais/Access?ticketId={1}", portalAddress, ticket.TicketId);

                transaction.Commit();

                return Ok(new { url = accessUrl });
            }
        }

        [HttpGet]
        public IHttpActionResult GetCorrespondentEmail(int id)
        {
            Doc doc = this.docRepository.Find(id,
                e => e.DocCorrespondents.Select(dc => dc.Correspondent),
                e => e.DocStatus,
                e => e.DocFiles.Select(k => k.DocFileKind),
                e => e.DocFiles.Select(k => k.DocFileOriginType));

            if (doc == null)
            {
                return NotFound();
            }

            Doc caseDoc = this.docRepository.Find(this.docRepository.GetCaseId(id));

            CorrespondentEmailDO email = new CorrespondentEmailDO();
            Correspondent correspondent = new Correspondent();

            foreach (var item in doc.DocCorrespondents)
            {
                NomDo corrNom = new NomDo();
                corrNom.NomValueId = item.Correspondent.CorrespondentId;
                corrNom.Name = item.Correspondent.DisplayName + " " + item.Correspondent.Email;
                corrNom.Alias = item.Correspondent.Alias;
                corrNom.IsActive = item.Correspondent.IsActive;

                email.EmailTo.Add(corrNom);
            }

            foreach (var item in doc.DocFiles.Where(e => e.DocFileKind.Alias == "PublicAttachedFile"))
            {
                email.PublicFiles.Add(new DocFileDO(item));
            }

            EmailType emailType = this.emailRepository.GetEmailTypeByAlias("CorrespondentEmail");

            email.EmailBcc = "";
            email.EmailTypeId = emailType.EmailTypeId;
            email.Subject = emailType.Subject
                .Replace("@@CaseNum", !string.IsNullOrEmpty(caseDoc.RegUri) ? caseDoc.RegUri : "[Няма номер на преписка]");
            email.Body = emailType.Body
                .Replace("@@CaseNum", !string.IsNullOrEmpty(caseDoc.RegUri) ? caseDoc.RegUri : "[Няма номер на преписка]")
                .Replace("@@DocViewUrl", EmailRepository.DocViewUrl)
                .Replace("@@CaseViewUrl", EmailRepository.CaseViewUrl)
                .Replace("@@AccessCode", caseDoc.AccessCode);

            return Ok(new
            {
                email = email
            });
        }

        [HttpPost]
        public IHttpActionResult SendCorrespondentEmail(int id, CorrespondentEmailDO email)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);

                ClassificationPermission sendEmailPermission = this.classificationRepository.GetByAlias("SendMail");
                ClassificationPermission readPermission = this.classificationRepository.GetByAlias("Read");

                bool hasSendEmailPermission =
                    this.classificationRepository.HasPermission(unitUser.UnitId, id, sendEmailPermission.ClassificationPermissionId) &&
                    this.classificationRepository.HasPermission(unitUser.UnitId, id, readPermission.ClassificationPermissionId);

                if (!hasSendEmailPermission)
                {
                    return BadRequest("Not enough permissions!");
                }

                Email correspondentEmail = this.emailRepository.CreateCorrespondentEmail(email);

                this.unitOfWork.DbContext.Set<Email>().Add(correspondentEmail);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok();
            }
        }

        [HttpPost]
        public IHttpActionResult SendCompetenceTransferDoc(int id, int electronicServiceProviderId)
        {
            var doc = this.docRepository.Find(id,
               e => e.DocFiles.Select(df => df.DocFileOriginType),
               e => e.DocFiles.Select(df => df.DocFileType));

            if (doc == null)
            {
                return BadRequest();
            }

            DocFile editable = doc.DocFiles.FirstOrDefault(e => e.DocFileOriginTypeId.HasValue && e.DocFileOriginType.Alias == "EditableFile");
            if (editable == null || editable.DocFileType.Alias != "ContainerTransferFileCompetence")
            {
                return BadRequest();
            }

            byte[] content;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
            {
                connection.Open();

                using (MemoryStream m1 = new MemoryStream())
                using (var blobStream = new BlobReadStream(connection, "dbo", "Blobs", "Content", "Key", editable.DocFileContentId))
                {
                    blobStream.CopyTo(m1);
                    content = m1.ToArray();
                }
            }

            string xmlContent = Utf8Utils.GetString(content);

            ElectronicServiceProvider electronicServiceProvider =
                this.unitOfWork.DbContext.Set<ElectronicServiceProvider>().FirstOrDefault(e => e.ElectronicServiceProviderId == electronicServiceProviderId);

            using (var channelFactory = new ChannelFactory<IApplicationCommunicator>("AppCommunicatorEndpoint", new EndpointAddress(electronicServiceProvider.EndPointAddress)))
            {
                DocumentRequest documentRequest = new DocumentRequest()
                {
                    DocumentData = xmlContent,
                    DocumentFileName = String.Format("CompetenceTransferDoc_{0}.xml", editable.DocFileContentId.ToString()),
                    DocumentGuid = editable.DocFileContentId
                };

                var service = channelFactory.CreateChannel();
                DocumentInfo documentInfo = service.ProcessStructuredDocument(documentRequest);
            }

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetRioObjectEditableFile(int id)
        {
            var doc = this.docRepository.Find(id,
               e => e.DocFiles.Select(df => df.DocFileOriginType),
               e => e.DocFiles.Select(df => df.DocFileType));

            if (doc == null)
            {
                return NotFound();
            }

            DocFile editable = doc.DocFiles.FirstOrDefault(e => e.DocFileOriginTypeId.HasValue && e.DocFileOriginType.Alias == "EditableFile");

            byte[] content;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
            {
                connection.Open();

                using (MemoryStream m1 = new MemoryStream())
                using (var blobStream = new BlobReadStream(connection, "dbo", "Blobs", "Content", "Key", editable.DocFileContentId))
                {
                    blobStream.CopyTo(m1);
                    content = m1.ToArray();
                }
            }

            var jObject = RioObjectUtils.GetJObjectFromBytes(editable.DocFileType.DocTypeUri, content);

            return Ok(new
            {
                content = jObject
            });
        }

        private byte[] CreateRioObject(string docTypeUri, int? caseDocId, int? parentDocId, Doc doc = null)
        {
            byte[] content = null;

            try
            {
                //Common
                if (docTypeUri == RioDocumentMetadata.ReceiptNotAcknowledgedMessageMetadata.DocumentTypeURIValue)
                {
                    var activeServiceProvider = GetActiveElectronicServiceProvider();
                    var correspondentNamesUinEmail = GetCorrespondentNamesUinAndEmail(parentDocId);

                    content = RioObjectUtils.CreateR0090ReceiptNotAcknowledgedMessage(
                        correspondentNamesUinEmail.Item1,
                        correspondentNamesUinEmail.Item2,
                        correspondentNamesUinEmail.Item3,
                        correspondentNamesUinEmail.Item4,
                        correspondentNamesUinEmail.Item5,
                        null,
                        null,
                        null,
                        activeServiceProvider.Code,
                        activeServiceProvider.Name,
                        activeServiceProvider.Bulstat);
                }
                //Common
                else if (docTypeUri == RioDocumentMetadata.ReceiptAcknowledgedMessageMetadata.DocumentTypeURIValue)
                {
                    var activeServiceProvider = GetActiveElectronicServiceProvider();
                    var correspondentNamesUinEmail = GetCorrespondentNamesUinAndEmail(parentDocId);
                    var registeredBy = String.Format("Служител {0}", activeServiceProvider.Name);

                    content = RioObjectUtils.CreateR0101ReceiptAcknowledgedMessage(
                        null,
                        registeredBy,
                        null,
                        null,
                        null,
                        null,
                        correspondentNamesUinEmail.Item1,
                        correspondentNamesUinEmail.Item2,
                        correspondentNamesUinEmail.Item3,
                        correspondentNamesUinEmail.Item4,
                        correspondentNamesUinEmail.Item5,
                        null,
                        null,
                        null,
                        activeServiceProvider.Code,
                        activeServiceProvider.Name,
                        activeServiceProvider.Bulstat);
                }
                //Common
                else if (docTypeUri == RioDocumentMetadata.RemovingIrregularitiesInstructionsMetadata.DocumentTypeURIValue)
                {
                    var activeServiceProvider = GetActiveElectronicServiceProvider();
                    var correspondentNamesUinEmail = GetCorrespondentNamesUinAndEmail(parentDocId);
                    var registeredBy = String.Format("Служител {0}", activeServiceProvider.Name);
                    Doc caseDoc = null;
                    if (caseDocId.HasValue)
                    {
                        caseDoc = this.unitOfWork.DbContext.Set<Doc>().SingleOrDefault(e => e.DocId == caseDocId.Value);
                    }
                    Doc parentDoc = null;
                    if (parentDocId.HasValue)
                    {
                        parentDoc = this.unitOfWork.DbContext.Set<Doc>().SingleOrDefault(e => e.DocId == parentDocId.Value);
                    }

                    content = RioObjectUtils.CreateR3010RemovingIrregularitiesInstructions(
                        activeServiceProvider.Name,
                        caseDoc != null ? caseDoc.RegDate : null,
                        caseDoc != null ? caseDoc.RegIndex : null,
                        caseDoc != null ? caseDoc.RegNumber.HasValue ? caseDoc.RegNumber.ToString() : null : null,
                        parentDoc != null ? parentDoc.RegDate : null,
                        parentDoc != null ? parentDoc.RegIndex : null,
                        parentDoc != null ? parentDoc.RegNumber.HasValue ? parentDoc.RegNumber.ToString() : null : null,
                        doc != null ? doc.RegDate : null,
                        doc != null ? doc.RegIndex : null,
                        doc != null ? doc.RegNumber.HasValue ? doc.RegNumber.ToString() : null : null,
                        correspondentNamesUinEmail.Item1,
                        correspondentNamesUinEmail.Item2,
                        correspondentNamesUinEmail.Item3,
                        correspondentNamesUinEmail.Item4,
                        correspondentNamesUinEmail.Item5,
                        activeServiceProvider.Code,
                        activeServiceProvider.Name,
                        activeServiceProvider.Bulstat,
                        null,
                        registeredBy);
                }
                //Common
                else if (docTypeUri == RioDocumentMetadata.ContainerTransferFileCompetenceMetadata.DocumentTypeURIValue)
                {
                    var activeServiceProvider = GetActiveElectronicServiceProvider();
                    var correspondentNamesUinEmail = GetCorrespondentNamesUinAndEmail(parentDocId);
                    var competenceFiles = GetCompetenceFilesByDocId(caseDocId);
                    Doc caseDoc = null;
                    if (caseDocId.HasValue)
                    {
                        caseDoc = this.unitOfWork.DbContext.Set<Doc>().SingleOrDefault(e => e.DocId == caseDocId.Value);
                    }

                    content = RioObjectUtils.CreateR6064ContainerTransferFileCompetence(
                        competenceFiles,
                        caseDoc != null ? caseDoc.RegDate : null,
                        caseDoc != null ? caseDoc.RegIndex : null,
                        caseDoc != null ? caseDoc.RegNumber.HasValue ? caseDoc.RegNumber.ToString() : null : null,
                        correspondentNamesUinEmail.Item1,
                        correspondentNamesUinEmail.Item2,
                        correspondentNamesUinEmail.Item3,
                        correspondentNamesUinEmail.Item4,
                        activeServiceProvider.Code,
                        activeServiceProvider.Name,
                        activeServiceProvider.Bulstat);
                }
                //Mosv
                else if (docTypeUri == RioDocumentMetadata.DecisionGrantAccessPublicInformationMetadata.DocumentTypeURIValue)
                {
                    content = RioObjectUtils.CreateR6090DecisionGrantAccessPublicInformation();
                }

                return content;
            }
            catch
            {
                return null;
            }
        }

        private ElectronicServiceProvider GetActiveElectronicServiceProvider()
        {
            return this.unitOfWork.DbContext.Set<ElectronicServiceProvider>().FirstOrDefault(e => e.IsActive);
        }

        private Tuple<string, string, string, string, string> GetCorrespondentNamesUinAndEmail(int? docId)
        {
            string firstName = null;
            string secondName = null;
            string lastName = null;
            string uin = null;
            string email = null;

            if (docId.HasValue)
            {

                DocCorrespondent docCorrespondent =
                    this.unitOfWork.DbContext.Set<DocCorrespondent>()
                    .FirstOrDefault(e => e.DocId == docId);

                if (docCorrespondent != null)
                {
                    Correspondent correspondent =
                        this.unitOfWork.DbContext.Set<Correspondent>()
                        .Include(e => e.CorrespondentType)
                        .SingleOrDefault(e => e.CorrespondentId == docCorrespondent.CorrespondentId);

                    if (correspondent != null)
                    {
                        if (correspondent.CorrespondentType.Alias == "BulgarianCitizen")
                        {
                            firstName = correspondent.BgCitizenFirstName;
                            lastName = correspondent.BgCitizenLastName;
                            uin = correspondent.BgCitizenUIN;
                        }
                        else if (correspondent.CorrespondentType.Alias == "Foreigner")
                        {
                            firstName = correspondent.ForeignerFirstName;
                            lastName = correspondent.ForeignerLastName;
                        }
                        else if (correspondent.CorrespondentType.Alias == "LegalEntity")
                        {
                            if (correspondent.LegalEntityName != null || correspondent.LegalEntityBulstat != null)
                            {
                                firstName = (correspondent.LegalEntityName ?? String.Empty + " " + correspondent.LegalEntityBulstat ?? String.Empty).Trim();
                            }
                        }
                        else if (correspondent.CorrespondentType.Alias == "ForeignLegalEntity")
                        {
                            firstName = correspondent.FLegalEntityName;
                        }

                        email = correspondent.Email;
                    }
                }
            }

            return new Tuple<string, string, string, string, string>(firstName, secondName, lastName, uin, email);
        }

        private List<Rio.Data.Utils.RioObjectUtils.CompetenceContainerFile> GetCompetenceFilesByDocId(int? caseDocId)
        {
            List<Rio.Data.Utils.RioObjectUtils.CompetenceContainerFile> competenceFiles = new List<Rio.Data.Utils.RioObjectUtils.CompetenceContainerFile>();

            if (caseDocId.HasValue)
            {
                var publicCaseDocs = docRepository.FindPublicLeafsByDocId(caseDocId.Value);

                if (publicCaseDocs.Count > 0)
                {
                    using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
                    using (var channelFactory = new ChannelFactory<IAbbcdn>("WSHttpBinding_IAbbcdn"))
                    using (var abbcdnStorage = new AbbcdnStorage(channelFactory))
                    {
                        connection.Open();

                        foreach (var doc in publicCaseDocs)
                        {
                            DocFile primaryDocFile =
                                this.unitOfWork.DbContext.Set<DocFile>()
                                .Include(d => d.DocFileType)
                                .Where(d => d.DocId == doc.DocId && d.DocFileKind.Alias == "PublicAttachedFile")
                                .OrderByDescending(d => d.IsPrimary)
                                .ThenBy(d => d.DocFileId)
                                .FirstOrDefault();

                            Rio.Data.Utils.RioObjectUtils.CompetenceContainerFile competenceContainerFile = new Rio.Data.Utils.RioObjectUtils.CompetenceContainerFile();
                            if (primaryDocFile == null)
                            {
                                competenceContainerFile.DocumentTypeName = doc.DocType.Name;
                                competenceContainerFile.DocumentRegIndex = doc.RegIndex;
                                competenceContainerFile.DocumentRegNumber = doc.RegNumber.ToString();
                                competenceContainerFile.DocumentRegDate = doc.RegDate;
                            }
                            else
                            {
                                byte[] fileContent = null;

                                using (MemoryStream mStream = new MemoryStream())
                                using (var blobStream = new BlobReadStream(connection, "dbo", "Blobs", "Content", "Key", primaryDocFile.DocFileContentId))
                                {
                                    blobStream.CopyTo(mStream);
                                    fileContent = mStream.ToArray();
                                }

                                var uploadFileInfo = abbcdnStorage.UploadFile(fileContent, primaryDocFile.DocFileName);

                                if (!String.IsNullOrWhiteSpace(primaryDocFile.DocFileType.DocTypeUri))
                                {
                                    competenceContainerFile.DocumentTypeName = RioDocumentMetadata.GetMetadataByDocumentTypeURI(primaryDocFile.DocFileType.DocTypeUri).DocumentTypeName;
                                }
                                else
                                {
                                    competenceContainerFile.DocumentTypeName = doc.DocType.Name;
                                }
                                competenceContainerFile.DocumentTypeUri = primaryDocFile.DocFileType.DocTypeUri;
                                competenceContainerFile.DocumentRegIndex = doc.RegIndex;
                                competenceContainerFile.DocumentRegNumber = doc.RegNumber.ToString();
                                competenceContainerFile.DocumentRegDate = doc.RegDate;
                                //competenceContainerFile.Abbcdnconfig = SerializeAbbcdn(uploadFileInfo, primaryDocFile.DocFileName, primaryDocFile.DocFileType.MimeType);
                                competenceContainerFile.Abbcdnconfig = new Abbcdnconfig()
                                {
                                    AttachedDocumentUniqueIdentifier = uploadFileInfo.FileKey.ToString(),
                                    AttachedDocumentHash = uploadFileInfo.ContentHash,
                                    AttachedDocumentSize = uploadFileInfo.ContentSize.ToString(),
                                    AttachedDocumentFileName = primaryDocFile.DocFileName,
                                    AttachedDocumentFileType = primaryDocFile.DocFileType.MimeType
                                };

                            }

                            competenceFiles.Add(competenceContainerFile);
                        }
                    }
                }
            }

            return competenceFiles;
        }

        private string SerializeAbbcdn(UploadFileInfo fileInfo, string fileName, string mimeType)
        {
            Abbcdnconfig cdnXml = new Abbcdnconfig()
            {
                AttachedDocumentUniqueIdentifier = fileInfo.FileKey.ToString(),
                AttachedDocumentHash = fileInfo.ContentHash,
                AttachedDocumentSize = fileInfo.ContentSize.ToString(),
                AttachedDocumentFileName = fileName,
                AttachedDocumentFileType = mimeType
            };

            return XmlSerializerUtils.XmlSerializeObjectToString(cdnXml);
        }
    }
}
