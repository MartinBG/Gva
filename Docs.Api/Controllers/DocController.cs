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

namespace Docs.Api.Controllers
{
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
        public HttpResponseMessage GetDocs(
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

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new
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
        public HttpResponseMessage CreateDoc(PreDocDO preDoc)
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

                    int? rootDocId = null;
                    if (preDoc.ParentDocId.HasValue)
                    {
                        DocRelation parentDocRelation = this.unitOfWork.DbContext.Set<DocRelation>().FirstOrDefault(e => e.DocId == preDoc.ParentDocId.Value);
                        if (parentDocRelation != null)
                        {
                            rootDocId = parentDocRelation.RootDocId;
                        }
                    }
                    newDoc.CreateDocRelation(preDoc.ParentDocId, rootDocId, userContext);

                    //? parent/child classifications inheritance
                    List<DocTypeClassification> docTypeClassifications = this.unitOfWork.DbContext.Set<DocTypeClassification>()
                        .Where(e => e.DocDirectionId == newDoc.DocDirectionId && e.DocTypeId == newDoc.DocTypeId)
                        .ToList();

                    foreach (var docTypeClassification in docTypeClassifications)
                    {
                        newDoc.CreateDocClassification(docTypeClassification.ClassificationId, userContext);
                    }

                    List<DocTypeUnitRole> docTypeUnitRoles = this.unitOfWork.DbContext.Set<DocTypeUnitRole>()
                        .Where(e => e.DocDirectionId == newDoc.DocDirectionId && e.DocTypeId == newDoc.DocTypeId)
                        .ToList();

                    foreach (var docTypeUnitRole in docTypeUnitRoles)
                    {
                        newDoc.CreateDocUnit(docTypeUnitRole.UnitId, docTypeUnitRole.DocTypeUnitRoleId, userContext);
                    }

                    foreach (var correspondent in preDoc.Correspondents)
                    {
                        newDoc.CreateDocCorrespondent(correspondent, userContext);
                    }

                    if (newDoc.IsCase)
                    {
                        this.docRepository.GenerateAccessCode(newDoc, userContext);
                    }

                    this.unitOfWork.Save();

                    this.docRepository.spSetDocUsers(newDoc.DocId);

                    if (preDoc.Register)
                    {
                        this.docRepository.RegisterDoc(newDoc, unitUser, userContext);
                    }

                    this.unitOfWork.Save();

                    createdDocs.Add(newDoc);
                }

                transaction.Commit();

                if (createdDocs.Count == 1 && !preDoc.Register)
                {
                    PreDocDO returnValue = new PreDocDO(createdDocs.FirstOrDefault());
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
                }
                else
                {
                    string ids = Helper.GetStringFromIdList(createdDocs.Select(e => e.DocId).ToList());
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { ids = ids });
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
        public HttpResponseMessage CreateChildDoc(int id, string docEntryTypeAlias = null)
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

                DocRelation parentRelation = this.unitOfWork.DbContext.Set<DocRelation>()
                    .FirstOrDefault(e => e.DocId == id);
                newDoc.CreateDocRelation(id, parentRelation.RootDocId, userContext);

                //? parent/child classifications inheritance
                List<DocTypeClassification> docTypeClassifications = this.unitOfWork.DbContext.Set<DocTypeClassification>()
                    .Where(e => e.DocDirectionId == newDoc.DocDirectionId && e.DocTypeId == newDoc.DocTypeId)
                    .ToList();

                foreach (var docTypeClassification in docTypeClassifications)
                {
                    newDoc.CreateDocClassification(docTypeClassification.ClassificationId, userContext);
                }

                List<DocTypeUnitRole> docTypeUnitRoles = this.unitOfWork.DbContext.Set<DocTypeUnitRole>()
                    .Where(e => e.DocDirectionId == newDoc.DocDirectionId && e.DocTypeId == newDoc.DocTypeId)
                    .ToList();

                foreach (var docTypeUnitRole in docTypeUnitRoles)
                {
                    newDoc.CreateDocUnit(docTypeUnitRole.UnitId, docTypeUnitRole.DocTypeUnitRoleId, userContext);
                }

                this.unitOfWork.Save();

                this.docRepository.spSetDocUsers(newDoc.DocId);

                transaction.Commit();

                PreDocDO returnValue = new PreDocDO(newDoc);
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
            }
        }

        /// <summary>
        /// Преглед на документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetDoc(int id)
        {
            DateTime currentDate = DateTime.Now;

            UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>()
                .Include(e => e.User)
                .Include(e => e.Unit)
                .FirstOrDefault(e => e.UserId == this.userContext.UserId);

            List<DocUser> docUsers = this.docRepository.GetActiveDocUsersForDocByUnitId(id, unitUser);

            if (!docUsers.Any())
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            }

            var doc = this.docRepository.Find(id,
                e => e.DocStatus,
                e => e.DocEntryType,
                e => e.DocCasePartType,
                e => e.DocDirection,
                e => e.DocType);

            if (doc == null)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.NoContent);
            }

            //?
            //    DocUnitPermission readPermission = this.unitOfWork.Repo<DocUnitPermission>().GetByAlias("Read");
            //    if (!docUsers.Any(e => e.DocUnitPermissionId == readPermission.DocUnitPermissionId))
            //    {
            //        return ControllerContext.Request.CreateResponse(HttpStatusCode.Forbidden);
            //    }

            this.unitOfWork.DbContext.Set<DocFile>()
             .Include(e => e.DocFileType)
             .Include(e => e.DocFileKind)
             .Where(e => e.DocId == id)
             .ToList();

            //? redundant
            //this.unitOfWork.DbContext.Set<DocRelation>()
            // .Where(e => e.DocId == id)
            // .ToList();

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

            var returnValue = new DocDO(doc, unitUser);

            //docusers get permissions
            //returnValue.CanRead = true;
            //foreach (var item in docUsers)
            //{
            //    if (item.DocId == doc.DocId && item.DocUnitPermission.Alias == "Edit")
            //    {
            //        returnValue.CanEdit = true;
            //    }
            //    else if (item.DocId == doc.DocId && item.DocUnitPermission.Alias == "Register")
            //    {
            //        returnValue.CanRegister = true;
            //    }
            //    else if (item.DocId == doc.DocId && item.DocUnitPermission.Alias == "Management")
            //    {
            //        returnValue.CanManagement = true;
            //    }
            //    else if (item.DocId == doc.DocId && item.DocUnitPermission.Alias == "ESign")
            //    {
            //        returnValue.CanESign = true;
            //    }
            //    else if (item.DocId == doc.DocId && item.DocUnitPermission.Alias == "Finish")
            //    {
            //        returnValue.CanFinish = true;
            //    }
            //    else if (item.DocId == doc.DocId && item.DocUnitPermission.Alias == "Reverse")
            //    {
            //        returnValue.CanReverse = true;
            //    }
            //}

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
                //e => e.Doc.DocCasePartMovements.Select(dc => dc.User),
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

            //    if (Statics.AdministrativeBody == AdministrativeBody.DKH)
            //    {
            //        List<int> docIds = returnValue.DocRelations.Where(e => e.DocId.HasValue).Select(e => e.DocId.Value).ToList();
            //        //dkhApplications
            //        var dkhApplications = this.unitOfWork.Repo<DkhApplication>().Query()
            //            .Include(e => e.Unit)
            //            .Where(e => docIds.Contains(e.ApplicationDocId))
            //            .ToList();

            //        foreach (var item in dkhApplications)
            //        {
            //            returnValue.DocLinks.Add(new DocLinkDO
            //            {
            //                Id = item.DkhApplicationId,
            //                Url = "#/dkhApps/",
            //                Name = string.Format("Предложение от {0:dd.MM.yyyy} възложено на {1}", item.StartDate, item.Unit.Name)
            //            });
            //        }

            //        //dkhAgendas
            //        var dkhAgendas = this.unitOfWork.Repo<DkhAgenda>().Query()
            //            .Include(e => e.Unit)
            //            .Where(e => e.CaseDocId.HasValue && docIds.Contains(e.CaseDocId.Value))
            //            .ToList();

            //        foreach (var item in dkhAgendas)
            //        {
            //            returnValue.DocLinks.Add(new DocLinkDO
            //            {
            //                Id = item.DkhAgendaId,
            //                Url = "#/dkhAgendas/",
            //                Name = string.Format("Заседание от {0:dd.MM.yyyy} възложено на {1}", item.StartDate, item.Unit.Name)
            //            });
            //        }
            //    }

            //returnValue.SetupFlags();
            returnValue.Set();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
        }

        /// <summary>
        /// Редакция на документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <param name="doc">Нови данни на документ</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage UpdateDoc(int id, DocDO doc)
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
                        oldDoc.DeleteDocUnit(listDocUnits[i], userContext);
                    }
                }
                foreach (var du in allDocUnits.Where(e => !e.IsProcessed))
                {
                    oldDoc.CreateDocUnit(du.NomValueId, du.ForeignKeyId, userContext);
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
                        oldDoc.DeleteDocCorrespondent(listDocCorrespondents[i], userContext);
                    }
                }
                foreach (var du in doc.DocCorrespondents.Where(e => !e.IsProcessed))
                {
                    oldDoc.CreateDocCorrespondent(du.NomValueId, userContext);
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

                //List<DocFileDO> allDocFiles = doc.PublicDocFiles.Union(doc.PrivateDocFiles).ToList();
                List<DocFileDO> allDocFiles = doc.DocFiles;

                foreach (var file in allDocFiles.Where(e => !e.IsNew && e.IsDeleted && e.DocFileId.HasValue))
                {
                    oldDoc.DeleteDocFile(file.DocFileId.Value, userContext);
                    //? mark as deleted
                    //DocFile df = this.unitOfWork.Repo<DocFile>().Find(file.DocFileId.Value);
                    //df.IsActive = false;
                    //df.Name = string.Format("{0} (изтрит от {1})", df.Name, userContext.FullName);
                }

                foreach (var file in allDocFiles.Where(e => !e.IsNew && !e.IsDeleted && e.IsDirty && e.DocFileId.HasValue))
                {
                    oldDoc.UpdateDocFile(
                        file.DocFileId.Value,
                        file.DocFileKindId,
                        file.DocFileTypeId,
                        file.Name,
                        file.File.Name,
                        "",
                        file.File.Key,
                        userContext);
                }

                foreach (var file in allDocFiles.Where(e => e.IsNew && !e.IsDeleted))
                {
                    oldDoc.CreateDocFile(
                        file.DocFileKindId,
                        file.DocFileTypeId,
                        file.Name,
                        file.File.Name,
                        "",
                        file.File.Key,
                        false,
                        true,
                        userContext);
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

                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, true);
            }
        }

        /// <summary>
        /// Редакция на раздел на преписка, в който се намира документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <param name="docCasePartTypeId">Идентификатор на тип раздел</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage UpdateDocCasePartType(int id, string docVersion, int docCasePartTypeId)
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

                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateTechDoc(int id, string docVersion, DocDO doc)
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
                        oldDoc.DeleteDocUnit(listDocUnits[i], userContext);
                    }
                }
                foreach (var du in allDocUnits.Where(e => !e.IsProcessed))
                {
                    oldDoc.CreateDocUnit(du.NomValueId, du.ForeignKeyId, userContext);
                }

                #endregion

                oldDoc.DocTypeId= doc.DocTypeId;
                oldDoc.DocDirectionId = doc.DocDirectionId;

                this.unitOfWork.Save();

                this.docRepository.spSetDocUsers(oldDoc.DocId);

                transaction.Commit();

                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        [HttpPost]
        public HttpResponseMessage RegisterDoc(int id, string docVersion)
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

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Управление на документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <param name="docVersion">Версия на документ</param>
        /// <param name="closure">Автоматично приключване на всички подчиненни документи</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage SetNextStatus(
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
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        docRelations = docRelationDOs
                    });
            }

            this.unitOfWork.Save();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Анулиране на документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <param name="docVersion">Версия на документ</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage CancelDoc(int id, string docVersion)
        {
            DocStatus cancelDocStatus = this.unitOfWork.DbContext.Set<DocStatus>()
                .SingleOrDefault(e => e.Alias.ToLower() == "Canceled".ToLower());

            this.docRepository.CancelDoc(
                id,
                Helper.StringToVersion(docVersion),
                cancelDocStatus,
                this.userContext);

            this.unitOfWork.Save();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// Сторниране на управление на документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <param name="docVersion">Версия на документ</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage ReverseStatus(int id, string docVersion)
        {
            List<DocStatus> docStatuses = this.unitOfWork.DbContext.Set<DocStatus>().ToList();

            this.docRepository.ReverseDocStatus(
                id,
                Helper.StringToVersion(docVersion),
                docStatuses,
                this.userContext);

            this.unitOfWork.Save();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage CreateDocWorkflow(int id, string docVersion, DocWorkflowDO docWorkflow)
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

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteDocWorkflow(int id, int itemId, string docVersion)
        {
            Doc doc = this.docRepository.Find(id,
                e => e.DocWorkflows);

            doc.EnsureForProperVersion(Helper.StringToVersion(docVersion));

            doc.DeleteDocWorkflow(itemId, this.userContext);

            this.unitOfWork.Save();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage CreateDocElectronicServiceStage(
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

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        public HttpResponseMessage GetCurrentDocElectronicServiceStage(int id, string docVersion)
        {
            Doc doc = this.docRepository.Find(this.docRepository.GetCaseId(id),
                e => e.DocElectronicServiceStages);

            doc.EnsureForProperVersion(Helper.StringToVersion(docVersion));

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, doc.GetCurrentDocElectronicServiceStage());
        }

        [HttpPost]
        public HttpResponseMessage UpdateCurrentDocElectronicServiceStage(
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

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage EndCurrentDocElectronicServiceStage(
            int id,
            string docVersion,
            DocElectronicServiceStageDO docElectronicServiceStage)
        {
            Doc doc = this.docRepository.Find(this.docRepository.GetCaseId(id),
                e => e.DocElectronicServiceStages);

            doc.EnsureForProperVersion(Helper.StringToVersion(docVersion));

            doc.EndCurrentDocElectronicServiceStage(docElectronicServiceStage.EndingDate.Value, this.userContext);

            this.unitOfWork.Save();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteCurrentDocElectronicServiceStage(int id, string docVersion)
        {
            Doc doc = this.docRepository.Find(this.docRepository.GetCaseId(id),
                e => e.DocElectronicServiceStages);

            doc.EnsureForProperVersion(Helper.StringToVersion(docVersion));

            doc.ReverseDocElectronicServiceStage(doc.GetCurrentDocElectronicServiceStage(), this.userContext);

            this.unitOfWork.Save();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
