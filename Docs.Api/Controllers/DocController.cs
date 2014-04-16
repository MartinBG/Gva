using Common.Api.Models;
using Common.Extensions;
using Docs.Api.DataObjects;
using Docs.Api.Enums;
using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.Entity;
using Common.Api.UserContext;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Configuration;

namespace Docs.Api.Controllers
{
    [Authorize]
    public class DocController : ApiController
    {
        private Common.Data.IUnitOfWork unitOfWork;
        private Docs.Api.Repositories.DocRepository.IDocRepository docRepository;
        private UserContext userContext;

        public DocController(Common.Data.IUnitOfWork unitOfWork,
            Docs.Api.Repositories.DocRepository.IDocRepository docRepository)
        {
            this.unitOfWork = unitOfWork;
            this.docRepository = docRepository;
        }

        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            this.userContext = this.Request.GetUserContext();
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
            //? hot fix: load fist 1000 docs, so the paging with datatable will work
            limit = 1000;
            offset = 0;

            UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);
            DocUnitPermission docUnitPermissionRead = this.unitOfWork.DbContext.Set<DocUnitPermission>().SingleOrDefault(e => e.Alias == "Read");
            DocSourceType docSourceType = this.unitOfWork.DbContext.Set<DocSourceType>().SingleOrDefault(e => e.Alias == "Internet");
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
                        docStatuses,
                        docUnitPermissionRead,
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
                        docStatuses,
                        docUnitPermissionRead,
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
                       docStatuses,
                       docUnitPermissionRead,
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
                      docStatuses,
                      docUnitPermissionRead,
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
                      docStatuses,
                      docUnitPermissionRead,
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
                      docStatuses,
                      docUnitPermissionRead,
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
                      docStatuses,
                      docUnitPermissionRead,
                      docSourceType,
                      unitUser,
                      out totalCount);
                    break;
                case "new":
                    throw new NotImplementedException();
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
                      docUnitPermissionRead,
                      unitUser,
                      out totalCount);
                    break;
            };

            List<DocListItemDO> returnValue = docs.Select(e => new DocListItemDO(e, unitUser)).ToList();

            List<int> loadedDocIds = returnValue.Where(e => e.DocId.HasValue).Select(e => e.DocId.Value).ToList();

            List<DocHasRead> docHasReadsForList = this.unitOfWork.DbContext.Set<DocHasRead>()
                .Where(du => du.UnitId == unitUser.UnitId && loadedDocIds.Contains(du.DocId))
               .ToList();

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

                item.SetIsRead(docHasReadsForList.Where(e => e.DocId == item.DocId).ToList(), unitUser);
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

                DocListItemDO minDoc = returnValue.FirstOrDefault(e => e.DocId == min);
                DocListItemDO maxDoc = returnValue.FirstOrDefault(e => e.DocId == max);

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

                    List<DocTypeClassification> docTypeClassifications = this.unitOfWork.DbContext.Set<DocTypeClassification>()
                        .Where(e => e.DocDirectionId == newDoc.DocDirectionId && e.DocTypeId == newDoc.DocTypeId)
                        .ToList();

                    List<DocTypeUnitRole> docTypeUnitRoles = this.unitOfWork.DbContext.Set<DocTypeUnitRole>()
                        .Where(e => e.DocDirectionId == newDoc.DocDirectionId && e.DocTypeId == newDoc.DocTypeId)
                        .ToList();

                    DocUnitRole importedBy = this.unitOfWork.DbContext.Set<DocUnitRole>()
                        .SingleOrDefault(e => e.Alias == "ImportedBy");

                    newDoc.CreateDocProperties(
                        parentDocRelation,
                        preDoc.DocCasePartTypeId,
                        docTypeClassifications,
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

                    this.unitOfWork.Save();

                    this.docRepository.spSetDocUsers(newDoc.DocId);

                    if (preDoc.Register)
                    {
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
        public IHttpActionResult CreateChildDoc(int id, string docEntryTypeAlias = null)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);
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
                        docType = this.unitOfWork.DbContext.Set<DocType>().SingleOrDefault(e => e.Alias.ToLower() == docEntryTypeAlias.ToLower());
                        docSubject = "Разпределение чрез резолюция";
                        break;
                    case "task":
                        docType = this.unitOfWork.DbContext.Set<DocType>().SingleOrDefault(e => e.Alias.ToLower() == docEntryTypeAlias.ToLower());
                        docSubject = "Разпределение чрез задача";
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

                List<DocTypeClassification> docTypeClassifications = this.unitOfWork.DbContext.Set<DocTypeClassification>()
                    .Where(e => e.DocDirectionId == newDoc.DocDirectionId && e.DocTypeId == newDoc.DocTypeId)
                    .ToList();

                List<DocTypeUnitRole> docTypeUnitRoles = this.unitOfWork.DbContext.Set<DocTypeUnitRole>()
                    .Where(e => e.DocDirectionId == newDoc.DocDirectionId && e.DocTypeId == newDoc.DocTypeId)
                    .ToList();

                DocUnitRole importedBy = this.unitOfWork.DbContext.Set<DocUnitRole>()
                        .SingleOrDefault(e => e.Alias == "ImportedBy");

                newDoc.CreateDocProperties(
                       parentDocRelation,
                       internalDocCasePartType.DocCasePartTypeId,
                       docTypeClassifications,
                       null,
                       docTypeUnitRoles,
                       importedBy,
                       unitUser,
                       null,
                       null,
                       this.userContext);

                this.unitOfWork.Save();

                this.docRepository.spSetDocUsers(newDoc.DocId);

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

            List<DocUser> docUsers = this.docRepository.GetActiveDocUsersForDocByUnitId(id, unitUser);

            DocUnitPermission readPermission = this.unitOfWork.DbContext.Set<DocUnitPermission>()
                .SingleOrDefault(e => e.Alias == "Read");

            if (!docUsers.Any(e => e.DocUnitPermissionId == readPermission.DocUnitPermissionId))
            {
                return BadRequest();
            }

            var doc = this.docRepository.Find(id,
                e => e.DocStatus,
                e => e.DocEntryType,
                e => e.DocCasePartType,
                e => e.DocDirection,
                e => e.DocType);

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
             .Include(e => e.Unit)
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

            foreach (var df in doc.DocFiles)
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

            returnValue.CanRead = docUsers.Any(e => e.DocId == doc.DocId && e.DocUnitPermission.Alias == "Read");
            returnValue.CanEdit = docUsers.Any(e => e.DocId == doc.DocId && e.DocUnitPermission.Alias == "Edit");
            returnValue.CanRegister = docUsers.Any(e => e.DocId == doc.DocId && e.DocUnitPermission.Alias == "Register");
            returnValue.CanManagement = docUsers.Any(e => e.DocId == doc.DocId && e.DocUnitPermission.Alias == "Management");
            returnValue.CanESign = docUsers.Any(e => e.DocId == doc.DocId && e.DocUnitPermission.Alias == "ESign");
            returnValue.CanFinish = docUsers.Any(e => e.DocId == doc.DocId && e.DocUnitPermission.Alias == "Finish");
            returnValue.CanReverse = docUsers.Any(e => e.DocId == doc.DocId && e.DocUnitPermission.Alias == "Reverse");

            returnValue.CanSubstituteManagement = docUsers.Any(e => e.DocId == doc.DocId && e.DocUnitPermission.Alias == "SubstituteManagement");
            returnValue.CanDeleteManagement = docUsers.Any(e => e.DocId == doc.DocId && e.DocUnitPermission.Alias == "DeleteManagement");
            returnValue.CanEditTechElectronicServiceStage = docUsers.Any(e => e.DocId == doc.DocId && e.DocUnitPermission.Alias == "EditTech");
            returnValue.CanEditTech = docUsers.Any(e => e.DocId == doc.DocId && e.DocUnitPermission.Alias == "EditTechElectronicServiceStage");

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
                DateTime currentDate = DateTime.Now;
                var oldDoc = this.docRepository.Find(id,
                        e => e.DocCorrespondents,
                        e => e.DocFiles,
                        e => e.DocUnits);

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

                #region DocClassifications

                //?
                ////docclassifications deletion
                //foreach (var docClassification in doc.DocClassifications.Where(e => !e.IsNew && e.IsDeleted))
                //{
                //    DocClassification dc = this.unitOfWork.Repo<DocClassification>().Find(docClassification.DocClassificationId.Value);
                //    this.unitOfWork.Repo<DocClassification>().Remove(dc);
                //}

                ////docclassifications add
                //foreach (var docClassification in doc.DocClassifications.Where(e => e.IsNew && !e.IsDeleted))
                //{
                //    DocClassification dc = new DocClassification();
                //    dc.Doc = oldDoc;
                //    dc.ClassificationId = docClassification.ClassificationId.Value;
                //    dc.ClassificationByUserId = docClassification.ClassificationByUserId.Value;
                //    dc.ClassificationDate = docClassification.ClassificationDate;
                //    dc.IsActive = docClassification.IsActive;

                //    this.unitOfWork.Repo<DocClassification>().Add(dc);
                //}

                #endregion

                #region Emails

                //    AdministrativeEmailType workflowActionRequestEmailType = this.unitOfWork.Repo<AdministrativeEmailType>().GetByAlias("WorkflowActionRequest");
                //    AdministrativeEmailType workflowActionEmailType = this.unitOfWork.Repo<AdministrativeEmailType>().GetByAlias("WorkflowAction");
                //    AdministrativeEmailStatus emailStatusNew = this.unitOfWork.Repo<AdministrativeEmailStatus>().GetByAlias("New");

                //    //docworkflows add
                //    foreach (var docWorkflow in doc.DocWorkflows.Where(e => e.IsNew && !e.IsDeleted))
                //    {
                //        DocWorkflow dw = new DocWorkflow();
                //        dw.Doc = oldDoc;
                //        dw.DocWorkflowActionId = docWorkflow.DocWorkflowActionId;
                //        dw.EventDate = eventDate;
                //        dw.Note = docWorkflow.Note;
                //        dw.PrincipalUnitId = docWorkflow.PrincipalUnitId;
                //        dw.ToUnitId = docWorkflow.ToUnitId;
                //        dw.UserId = docWorkflow.UserId;
                //        dw.YesNo = string.IsNullOrWhiteSpace(docWorkflow.YesNo) ? (bool?)null : bool.Parse(docWorkflow.YesNo);

                //        this.unitOfWork.Repo<DocWorkflow>().Add(dw);

                //        //Sending email
                //        DocWorkflowAction docWorkflowAction = this.unitOfWork.Repo<DocWorkflowAction>().Find(docWorkflow.DocWorkflowActionId);
                //        User toUser = dw.ToUnitId.HasValue ? this.unitOfWork.Repo<User>().GetByUnitId(dw.ToUnitId.Value) : null;
                //        //User principalUser = dw.PrincipalUnitId.HasValue ? this.unitOfWork.Repo<User>().GetByUnitId(dw.PrincipalUnitId.Value) : null;

                //        if ((docWorkflowAction.Alias == "SignRequest" || docWorkflowAction.Alias == "DiscussRequest" || docWorkflowAction.Alias == "ApprovalRequest" || docWorkflowAction.Alias == "RegistrationRequest") &&
                //            toUser != null &&
                //            !String.IsNullOrWhiteSpace(toUser.Email))
                //        {
                //            AdministrativeEmail email = new AdministrativeEmail();
                //            email.TypeId = workflowActionRequestEmailType.AdministrativeEmailTypeId;
                //            email.UserId = toUser.UserId;
                //            email.Param1 = docWorkflow.Username;
                //            email.Param2 = doc.DocTypeName;
                //            email.Param3 = doc.DocSubject;
                //            email.Param4 = String.Format(Request.RequestUri.OriginalString.Replace(Request.RequestUri.PathAndQuery, "/#/docs/{0}"), doc.DocId);
                //            email.StatusId = emailStatusNew.AdministrativeEmailStatusId;
                //            email.Subject = workflowActionRequestEmailType.Subject;
                //            email.Body = workflowActionRequestEmailType.Body.Replace("@@Param1", email.Param1).Replace("@@Param2", email.Param2).Replace("@@Param3", email.Param3).Replace("@@Param4", email.Param4);

                //            this.unitOfWork.Repo<AdministrativeEmail>().Add(email);
                //        }
                //        else if ((docWorkflowAction.Alias == "Sign" || docWorkflowAction.Alias == "Discuss" || docWorkflowAction.Alias == "Approval"))
                //        {
                //            DocWorkflow requestDocWorkflow = this.unitOfWork.Repo<DocWorkflow>().GetByDocIdAndActionAlias(dw.Doc.DocId, docWorkflowAction.Alias + "Request");
                //            if (requestDocWorkflow != null)
                //            {
                //                User principalUser = requestDocWorkflow.PrincipalUnitId.HasValue ? this.unitOfWork.Repo<User>().GetByUnitId(requestDocWorkflow.PrincipalUnitId.Value) : null;

                //                if (principalUser != null && !String.IsNullOrWhiteSpace(principalUser.Email))
                //                {
                //                    AdministrativeEmail email = new AdministrativeEmail();
                //                    email.TypeId = workflowActionEmailType.AdministrativeEmailTypeId;
                //                    email.UserId = principalUser.UserId;
                //                    email.Param1 = doc.DocTypeName;
                //                    email.Param2 = doc.DocSubject;
                //                    email.Param3 = String.Format(Request.RequestUri.OriginalString.Replace(Request.RequestUri.PathAndQuery, "/#/docs/{0}"), doc.DocId);
                //                    email.StatusId = emailStatusNew.AdministrativeEmailStatusId;
                //                    email.Subject = workflowActionEmailType.Subject;
                //                    email.Body = workflowActionEmailType.Body.Replace("@@Param1", email.Param1).Replace("@@Param2", email.Param2).Replace("@@Param3", email.Param3);

                //                    this.unitOfWork.Repo<AdministrativeEmail>().Add(email);
                //                }
                //            }
                //        }

                //    }

                #endregion

                #region DocFiles

                List<DocFileDO> allDocFiles = doc.DocFiles;
                List<DocFileType> docFileTypes = this.unitOfWork.DbContext.Set<DocFileType>().ToList();

                foreach (var file in allDocFiles.Where(e => !e.IsNew && e.IsDeleted && e.DocFileId.HasValue))
                {
                    oldDoc.DeleteDocFile(file.DocFileId.Value, this.userContext);
                    //? mark as deleted
                    //DocFile df = this.unitOfWork.Repo<DocFile>().Find(file.DocFileId.Value);
                    //df.IsActive = false;
                    //df.Name = string.Format("{0} (изтрит от {1})", df.Name, this.userContext.FullName);
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

                //?
                //    //file update - ticket
                //    foreach (var file in docFiles.Where(e => !e.IsNew && !e.IsDeleted && e.IsEdited && e.TicketId.HasValue))
                //    {
                //        Ticket ticket = this.unitOfWork.Repo<Ticket>().Find(file.TicketId.Value);
                //        if (ticket.NewKey.HasValue)
                //        {
                //            DocFile df = this.unitOfWork.Repo<DocFile>().Find(file.DocFileId.Value);
                //            df.DocFileContentId = ticket.NewKey.Value;
                //        }
                //    }

                #endregion

                this.unitOfWork.Save();
                this.docRepository.spSetDocUsers(oldDoc.DocId);

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
                Doc doc = this.docRepository.UpdateDocCasePartType(
                    id,
                    Helper.StringToVersion(docVersion),
                    docCasePartTypeId, this.userContext);

                this.unitOfWork.Save();

                this.docRepository.spSetDocUsers(doc.DocId);

                transaction.Commit();

                return Ok();
            }
        }

        [HttpPost]
        public IHttpActionResult UpdateTechDoc(int id, string docVersion, DocDO doc)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var oldDoc = this.docRepository.Find(id, e => e.DocUnits);

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

                this.unitOfWork.Save();

                this.docRepository.spSetDocUsers(oldDoc.DocId);

                transaction.Commit();

                return Ok();
            }
        }

        [HttpPost]
        public IHttpActionResult RegisterDoc(int id, string docVersion)
        {
            UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);

            Doc doc = this.docRepository.Find(id,
                e => e.DocRelations);

            this.docRepository.RegisterDoc(
                doc,
                unitUser,
                this.userContext,
                true,
                Helper.StringToVersion(docVersion));

            this.unitOfWork.Save();

            return Ok();
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
            bool? closure = null)
        {
            List<DocStatus> docStatuses = this.unitOfWork.DbContext.Set<DocStatus>().ToList();
            List<DocCasePartType> docCasePartTypes = this.unitOfWork.DbContext.Set<DocCasePartType>().ToList();
            List<DocRelation> docRelations;

            this.docRepository.NextDocStatus(
                id,
                Helper.StringToVersion(docVersion),
                closure ?? false,
                docStatuses,
                docCasePartTypes,
                this.userContext,
                out docRelations);

            if (docRelations.Any())
            {
                List<DocRelationDO> docRelationDOs = docRelations.Select(e => new DocRelationDO(e)).ToList();

                return Ok(new
                    {
                        docRelations = docRelationDOs
                    });
            }

            this.unitOfWork.Save();

            return Ok();
        }

        /// <summary>
        /// Анулиране на документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <param name="docVersion">Версия на документ</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CancelDoc(int id, string docVersion)
        {
            DocStatus cancelDocStatus = this.unitOfWork.DbContext.Set<DocStatus>()
                .SingleOrDefault(e => e.Alias.ToLower() == "Canceled".ToLower());

            this.docRepository.CancelDoc(
                id,
                Helper.StringToVersion(docVersion),
                cancelDocStatus,
                this.userContext);

            this.unitOfWork.Save();

            return Ok();
        }

        /// <summary>
        /// Сторниране на управление на документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <param name="docVersion">Версия на документ</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ReverseStatus(int id, string docVersion)
        {
            List<DocStatus> docStatuses = this.unitOfWork.DbContext.Set<DocStatus>().ToList();

            this.docRepository.ReverseDocStatus(
                id,
                Helper.StringToVersion(docVersion),
                docStatuses,
                this.userContext);

            this.unitOfWork.Save();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult CreateDocWorkflow(int id, string docVersion, DocWorkflowDO docWorkflow)
        {
            Doc doc = this.docRepository.Find(id);

            doc.EnsureForProperVersion(Helper.StringToVersion(docVersion));

            DocWorkflowAction docWorkflowAction = this.unitOfWork.DbContext.Set<DocWorkflowAction>()
                .SingleOrDefault(e => e.Alias.ToLower() == docWorkflow.DocWorkflowActionAlias.ToLower());

            bool? yesNo = null;

            if (docWorkflow.YesNoId.HasValue)
            {
                yesNo = Convert.ToBoolean(docWorkflow.YesNoId.Value);
            }

            doc.CreateDocWorkflow(
                docWorkflowAction,
                DateTime.Now,
                yesNo,
                docWorkflow.ToUnitId,
                docWorkflow.PrincipalUnitId,
                docWorkflow.Note,
                docWorkflow.UnitUserId,
                this.userContext);

            this.unitOfWork.Save();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteDocWorkflow(int id, int itemId, string docVersion)
        {
            Doc doc = this.docRepository.Find(id,
                e => e.DocWorkflows);

            doc.EnsureForProperVersion(Helper.StringToVersion(docVersion));

            doc.DeleteDocWorkflow(itemId, this.userContext);

            this.unitOfWork.Save();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult CreateDocElectronicServiceStage(
            int id,
            string docVersion,
            DocElectronicServiceStageDO docElectronicServiceStage)
        {
            Doc doc = this.docRepository.Find(this.docRepository.GetCaseId(id),
                e => e.DocElectronicServiceStages);

            doc.EnsureForProperVersion(Helper.StringToVersion(docVersion));

            doc.CreateDocElectronicServiceStage(
                docElectronicServiceStage.ElectronicServiceStageId,
                docElectronicServiceStage.StartingDate,
                docElectronicServiceStage.ExpectedEndingDate,
                docElectronicServiceStage.EndingDate,
                true,
                this.userContext);

            this.unitOfWork.Save();

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetCurrentDocElectronicServiceStage(int id, string docVersion)
        {
            Doc doc = this.docRepository.Find(this.docRepository.GetCaseId(id),
                e => e.DocElectronicServiceStages);

            doc.EnsureForProperVersion(Helper.StringToVersion(docVersion));

            return Ok(doc.GetCurrentDocElectronicServiceStage());
        }

        [HttpPost]
        public IHttpActionResult UpdateCurrentDocElectronicServiceStage(
            int id,
            string docVersion,
            DocElectronicServiceStageDO docElectronicServiceStage)
        {
            Doc doc = this.docRepository.Find(this.docRepository.GetCaseId(id),
                e => e.DocElectronicServiceStages);

            doc.EnsureForProperVersion(Helper.StringToVersion(docVersion));

            doc.UpdateCurrentDocElectronicServiceStage(
                docElectronicServiceStage.ElectronicServiceStageId,
                docElectronicServiceStage.StartingDate,
                docElectronicServiceStage.ExpectedEndingDate,
                docElectronicServiceStage.EndingDate,
                this.userContext);

            this.unitOfWork.Save();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult EndCurrentDocElectronicServiceStage(
            int id,
            string docVersion,
            DocElectronicServiceStageDO docElectronicServiceStage)
        {
            Doc doc = this.docRepository.Find(this.docRepository.GetCaseId(id),
                e => e.DocElectronicServiceStages);

            doc.EnsureForProperVersion(Helper.StringToVersion(docVersion));

            doc.EndCurrentDocElectronicServiceStage(docElectronicServiceStage.EndingDate.Value, this.userContext);

            this.unitOfWork.Save();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteCurrentDocElectronicServiceStage(int id, string docVersion)
        {
            Doc doc = this.docRepository.Find(this.docRepository.GetCaseId(id),
                e => e.DocElectronicServiceStages);

            doc.EnsureForProperVersion(Helper.StringToVersion(docVersion));

            doc.ReverseDocElectronicServiceStage(doc.GetCurrentDocElectronicServiceStage(), this.userContext);

            this.unitOfWork.Save();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead(int id, string docVersion)
        {
            UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);

            this.docRepository.MarkAsRead(
                id,
                Helper.StringToVersion(docVersion),
                unitUser.UnitId,
                this.userContext);

            this.unitOfWork.Save();

            return Ok();
        }

        [HttpPost]
        public IHttpActionResult MarkAsUnread(int id, string docVersion)
        {
            UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);

            this.docRepository.MarkAsUnread(
                id,
                Helper.StringToVersion(docVersion),
                unitUser.UnitId,
                this.userContext);

            this.unitOfWork.Save();

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult ReadExternalLinks(int id)
        {
            //? implement connection to application
            throw new NotImplementedException();
        }

        [HttpPost]
        public IHttpActionResult CreateDocFileTicket(int id, int docFileId, Guid fileKey)
        {
            Ticket ticket = new Ticket();
            ticket.TicketId = Guid.NewGuid();
            ticket.DocFileId = docFileId;
            ticket.OldKey = fileKey;
            ticket.VisualizationMode = (int)VisualizationMode.DisplayWithoutSignature;

            this.unitOfWork.DbContext.Set<Ticket>().Add(ticket);
            this.unitOfWork.Save();

            string portalAddress = ConfigurationManager.AppSettings["PortalWebAddress"].ToString();
            string accessUrl = String.Format("{0}/Ais/Access?ticketId={1}", portalAddress, ticket.TicketId);

            return Ok(new { url = accessUrl });
        }
    }
}
