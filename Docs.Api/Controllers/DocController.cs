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

namespace Docs.Api.Controllers
{
    public class DocController : ApiController
    {
        private Common.Data.IUnitOfWork unitOfWork;
        private Docs.Api.Repositories.DocRepository.IDocRepository docRepository;
        private Common.Api.UserContext.UserContext userContext;

        public DocController(Common.Data.IUnitOfWork unitOfWork,
            Docs.Api.Repositories.DocRepository.IDocRepository docRepository,
            Common.Api.UserContext.IUserContextProvider userContextProvider)
        {
            this.unitOfWork = unitOfWork;
            this.docRepository = docRepository;
            this.userContext = userContextProvider.GetCurrentUserContext();
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
            UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == userContext.UserId);
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

        [HttpPost]
        public HttpResponseMessage CreateDoc(PreDocDO preDoc)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == userContext.UserId);
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
                        this.userContext);

                    int? rootDocId = null;
                    if (preDoc.ParentDocId.HasValue)
                    {
                        DocRelation parentDocRelation = this.unitOfWork.DbContext.Set<DocRelation>().FirstOrDefault(e => e.DocId == preDoc.ParentDocId.Value);
                        if (parentDocRelation != null)
                        {
                            rootDocId = parentDocRelation.RootDocId;
                        }
                    }
                    newDoc.CreateDocRelation(preDoc.ParentDocId, rootDocId, this.userContext);

                    //? parent/child classifications inheritance
                    List<DocTypeClassification> docTypeClassifications = this.unitOfWork.DbContext.Set<DocTypeClassification>()
                        .Where(e => e.DocDirectionId == newDoc.DocDirectionId && e.DocTypeId == newDoc.DocTypeId)
                        .ToList();

                    foreach (var docTypeClassification in docTypeClassifications)
                    {
                        newDoc.CreateDocClassification(docTypeClassification.ClassificationId, this.userContext);
                    }

                    List<DocTypeUnitRole> docTypeUnitRoles = this.unitOfWork.DbContext.Set<DocTypeUnitRole>()
                        .Where(e => e.DocDirectionId == newDoc.DocDirectionId && e.DocTypeId == newDoc.DocTypeId)
                        .ToList();

                    foreach (var docTypeUnitRole in docTypeUnitRoles)
                    {
                        newDoc.CreateDocUnit(docTypeUnitRole.UnitId, docTypeUnitRole.DocTypeUnitRoleId, this.userContext);
                    }

                    foreach (var correspondent in preDoc.Correspondents)
                    {
                        newDoc.CreateDocCorrespondent(correspondent, this.userContext);
                    }

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

        [HttpPost]
        public HttpResponseMessage CreateChildDoc(int id, string docEntryTypeAlias = null)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == userContext.UserId);
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
                    this.userContext);

                DocRelation parentRelation = this.unitOfWork.DbContext.Set<DocRelation>()
                    .FirstOrDefault(e => e.DocId == id);
                newDoc.CreateDocRelation(id, parentRelation.RootDocId, this.userContext);

                //? parent/child classifications inheritance
                List<DocTypeClassification> docTypeClassifications = this.unitOfWork.DbContext.Set<DocTypeClassification>()
                    .Where(e => e.DocDirectionId == newDoc.DocDirectionId && e.DocTypeId == newDoc.DocTypeId)
                    .ToList();

                foreach (var docTypeClassification in docTypeClassifications)
                {
                    newDoc.CreateDocClassification(docTypeClassification.ClassificationId, this.userContext);
                }

                List<DocTypeUnitRole> docTypeUnitRoles = this.unitOfWork.DbContext.Set<DocTypeUnitRole>()
                    .Where(e => e.DocDirectionId == newDoc.DocDirectionId && e.DocTypeId == newDoc.DocTypeId)
                    .ToList();

                foreach (var docTypeUnitRole in docTypeUnitRoles)
                {
                    newDoc.CreateDocUnit(docTypeUnitRole.UnitId, docTypeUnitRole.DocTypeUnitRoleId, this.userContext);
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

            UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == userContext.UserId);

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

            this.unitOfWork.DbContext.Set<DocRelation>()
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
                .Include(e => e.Unit)
                .Include(e => e.Unit1)
                .Where(e => e.DocId == id)
                .ToList();

            this.unitOfWork.DbContext.Set<DocClassification>()
             .Include(e => e.Classification)
             .Where(e => e.DocId == id)
             .ToList();

            var returnValue = new DocDO(doc, null, unitUser); //? systemUser

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

            //?
            //Doc caseDoc = this.docRepository.Find(caseDocId,
            //    e => e.DocType,
            //    e => e.DocElectronicServiceStages.Select(d => d.ElectronicServiceStage.ElectronicServiceStageExecutors.Select(ee => ee.Unit)));

            //if (caseDoc.DocType.IsElectronicService)
            //{
            //    foreach (var dess in caseDoc.DocElectronicServiceStages)
            //    {
            //        returnValue.DocElectronicServiceStages.Add(new DocElectronicServiceStageDO(dess));
            //    }
            //}

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

                //? check for permissions to update
                //?
                if (oldDoc == null)
                {
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = "Документът не може да бъде намерен." });
                }

                //?
                if (!oldDoc.Version.SequenceEqual(doc.Version))
                {
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = "Съществува нова версия на документа." });
                }

                oldDoc.DocSourceTypeId = doc.DocSourceTypeId;
                oldDoc.DocSubject = doc.DocSubject;
                oldDoc.DocBody = doc.DocBody;
                oldDoc.CorrRegNumber = doc.CorrRegNumber;
                oldDoc.CorrRegDate = doc.CorrRegDate;
                oldDoc.AssignmentTypeId = doc.AssignmentTypeId;
                oldDoc.AssignmentDate = doc.AssignmentDate;
                oldDoc.AssignmentDeadline = doc.AssignmentDeadline;
                oldDoc.ModifyDate = DateTime.Now;
                oldDoc.ModifyUserId = this.userContext.UserId;

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

                //List<DocFileDO> allDocFiles = doc.PublicDocFiles.Union(doc.PrivateDocFiles).ToList();
                List<DocFileDO> allDocFiles = doc.DocFiles;

                foreach (var file in allDocFiles.Where(e => !e.IsNew && e.IsDeleted && e.DocFileId.HasValue))
                {
                    oldDoc.DeleteDocFile(file.DocFileId.Value, this.userContext);
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
                        this.userContext);
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
                        this.userContext);
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

        /* DOC CONTROLLER

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="unitOfWork">Базов интерфейс за достъп до базата данни</param>
        /// <param name="userContextProvider">Интерфейс за достъп до потребителските данни</param>
        /// <param name="documentSerializer">Интерфейс за Xml сериализация на документи</param>
        //public DocController(IUnitOfWork unitOfWork, IUserContextProvider userContextProvider, IDocumentSerializer documentSerializer)
        //    : base(unitOfWork, userContextProvider, documentSerializer)
        //{
        //}

        #region PriorDoc

        /// <summary>
        /// Връща водещ документ по преписка
        /// </summary>
        /// <param name="caseUri">УРИ на преписка</param>
        /// <returns></returns>
        public HttpResponseMessage GetParentDoc(string caseUri)
        {
            //todo vinagi da se vryzva kym prepiska
            var parentDoc =
                  this.unitOfWork.Repo<Doc>().Query()
                  .Include(e => e.DocType)
                  .Include(e => e.DocRelations)
                  .SingleOrDefault(e => e.RegUri == caseUri.Trim());

            if (parentDoc != null)
            {
                DocRelationDO parentDocRelation = new DocRelationDO()
                {
                    RootDocId = parentDoc.DocRelations.FirstOrDefault().RootDocId, //presumption one doc can only be in one case
                    ParentDocId = parentDoc.DocId,
                    ParentRegUri = parentDoc.RegUri,
                    ParentDocTypeName = parentDoc.DocType.Name,
                    ParentSubject = parentDoc.DocSubject
                };

                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new
                {
                    parentDocRelation = parentDocRelation,
                    isCaseFound = true
                });
            }
            else
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new
                {
                    parentDocRelation = new DocRelationDO(),
                    isCaseFound = false
                });
            }
        }

        /// <summary>
        /// Генерира нов документ
        /// </summary>
        /// <param name="rootId">Идентификатор на водещ документ в преписка</param>
        /// <param name="parentId">Идентификатор на родителски документ</param>
        /// <returns></returns>
        public HttpResponseMessage GetFastPriorDoc(int? rootId, int? parentId)
        {
            UserContext userContext = this.userContextProvider.GetCurrentUserContext();

            User user = unitOfWork.Repo<User>()
                .Query()
                .Include(e => e.Unit)
                .SingleOrDefault(e => e.UserId == userContext.UserId);

            List<UnitClassification> unitClassifications = this.unitOfWork.Repo<Doc>().ExecSpGetUnitClassifications(user.UnitId.Value);

            ClassificationRole cr = this.unitOfWork.Repo<ClassificationRole>().Query()
                .FirstOrDefault(e => e.Alias.ToLower() == "Register".ToLower());

            bool hasRegister = unitClassifications.Any(e => e.ClassificationRoleId == cr.ClassificationRoleId);

            var returnValue = new PriorDocDO();

            string docCasePartTypeAlias = "public";
            if (parentId.HasValue)
            {
                docCasePartTypeAlias = "internal";

                var parentDoc = this.unitOfWork.Repo<Doc>().Find(parentId.Value,
                    d => d.DocType
                    );

                this.unitOfWork.Repo<DocRelation>().Query()
                    .Where(e => e.DocId == parentId.Value)
                    .ToList();

                DocRelationDO parentDocRelation = new DocRelationDO()
                {
                    RootDocId = parentDoc.DocRelations.FirstOrDefault().RootDocId, //presumption one doc can only be in one case
                    ParentDocId = parentId.Value,
                    ParentRegUri = parentDoc.RegUri,
                    ParentDocTypeName = parentDoc.DocType.Name,
                    ParentSubject = parentDoc.DocSubject
                };

                returnValue.ParentDocRelation = parentDocRelation;
                returnValue.FakeParentDocRelation = parentDocRelation;

                if (rootId.HasValue)
                {
                    var rootDoc = this.unitOfWork.Repo<Doc>().Find(rootId.Value);

                    this.unitOfWork.Repo<DocCorrespondent>().Query()
                        .Include(e => e.Correspondent.RegisterIndex)
                        .Include(e => e.Correspondent.CorrespondentType)
                    .Where(e => e.DocId == rootId.Value)
                    .ToList();

                    returnValue.DocCorrespondents.AddRange(rootDoc.DocCorrespondents.Select(e => new DocCorrespondentDO(e)).ToList());
                }
            }
            else
            {
                returnValue.ParentDocRelation = new DocRelationDO();
                returnValue.FakeParentDocRelation = new DocRelationDO();
            }

            DocCasePartType docCasePartType = this.unitOfWork.Repo<DocCasePartType>().GetByAlias(docCasePartTypeAlias);

            DocFormatType docFormatType = this.unitOfWork.Repo<DocFormatType>().GetByAlias("Electronic");

            DocDirection docDirection = this.unitOfWork.Repo<DocDirection>().GetByAlias("Incomming");

            DateTime currentDate = DateTime.Now;

            returnValue.RegDate = currentDate;
            returnValue.DocRootId = rootId;
            returnValue.DocParentId = parentId;
            returnValue.DocCasePartTypeId = docCasePartType.DocCasePartTypeId;
            returnValue.DocFormatTypeId = docFormatType.DocFormatTypeId;
            returnValue.DocDirectionId = docDirection.DocDirectionId;
            returnValue.DocElectronicServiceStage = new DocElectronicServiceStageDO();
            returnValue.DocElectronicServiceStage.StartingDate = currentDate;
            returnValue.DocSubject = "Подадено заявление";
            returnValue.IsCase = !parentId.HasValue;
            returnValue.IsRegistered = false;
            returnValue.IsSigned = false;
            returnValue.ToRegisterDoc = true;

            returnValue.DocFormatTypes = this.unitOfWork.Repo<DocFormatType>().Query()
                .Where(e => e.IsActive).ToList();
            returnValue.DocCasePartTypes = this.unitOfWork.Repo<DocCasePartType>().Query()
                .Where(e => e.IsActive).ToList();
            returnValue.DocDirections = this.unitOfWork.Repo<DocDirection>().Query()
                .Where(e => e.IsActive).ToList();

            returnValue.NumberOfDocs = 1;
            returnValue.IsVisibleRegisterCmd = hasRegister;

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
        }

        #endregion

        #region Docs

        /// <summary>
        /// Запис на нов документ
        /// </summary>
        /// <param name="prior">Данни на документ</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage PostNewDoc(PriorDocDO prior)
        {
            DocDO returnValue = PostNewDocInternal(prior);

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
        }

        /// <summary>
        /// Запис и регистрация на нови документи
        /// </summary>
        /// <param name="prior">Данни на документи</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage PostNewDocs(PriorDocDO prior)
        {
            try
            {
                using (TransactionScope transactionScope = this.unitOfWork.CreateTransactionScope())
                {
                    UserContext userContext = this.userContextProvider.GetCurrentUserContext();

                    DocDO returnValue = PostNewDocInternal(prior);

                    List<int> docIds = new List<int>();

                    for (var i = 0; i < prior.NumberOfDocs; i++)
                    {
                        Doc newDoc = PostDocInternal(returnValue, userContext);
                        this.unitOfWork.Repo<Doc>().RegisterDoc(this.unitOfWork, this.SystemUser.UserId, newDoc);
                        this.unitOfWork.Save();

                        //todo check if current user-unit is in docusers, if not transaction must fails

                        docIds.Add(newDoc.DocId);
                    }
                    transactionScope.Complete();

                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, Helper.GetStringFromIdList(docIds));
                }
            }
            catch (Exception ex)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }
        }

        /// <summary>
        /// Запис на нова резолюция
        /// </summary>
        /// <param name="id">Идентификатор на родителски документ</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage PostNewResolution(int id)
        {
            UserContext userContext = this.userContextProvider.GetCurrentUserContext();

            User user = unitOfWork.Repo<User>()
                .Query()
                .Include(e => e.Unit)
                .SingleOrDefault(e => e.UserId == userContext.UserId);

            DocDO resolution = GetNewResolutionInternal(id);
            resolution.DocSubject = "Резолюция";

            if (user.UnitId.HasValue)
            {
                DocUnitRole unitRole = this.unitOfWork.Repo<DocUnitRole>().Query()
                .SingleOrDefault(e => e.Alias.ToLower() == "From".ToLower());

                resolution.DocUnits.Add(new DocUnitDO
                {
                    UnitId = user.UnitId.Value,
                    DocUnitRoleId = unitRole.DocUnitRoleId
                });
            }

            Doc resolutionDoc = PostDocInternal(resolution, userContext);

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = "", docId = resolutionDoc.DocId });
        }

        /// <summary>
        /// Запис на нова задача
        /// </summary>
        /// <param name="id">Идентификатор на родителски документ</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage PostNewTask(int id)
        {
            UserContext userContext = this.userContextProvider.GetCurrentUserContext();

            User user = unitOfWork.Repo<User>()
                .Query()
                .Include(e => e.Unit)
                .SingleOrDefault(e => e.UserId == userContext.UserId);

            DocDO task = GetNewTaskInternal(id);
            task.DocSubject = "Задача";

            if (user.UnitId.HasValue)
            {
                DocUnitRole unitRole = this.unitOfWork.Repo<DocUnitRole>().Query()
                    .SingleOrDefault(e => e.Alias.ToLower() == "From".ToLower());

                task.DocUnits.Add(new DocUnitDO
                {
                    UnitId = user.UnitId.Value,
                    DocUnitRoleId = unitRole.DocUnitRoleId
                });
            }

            Doc taskDoc = PostDocInternal(task, userContext);

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = "", docId = taskDoc.DocId });
        }

        /// <summary>
        /// Запис на нова забележка
        /// </summary>
        /// <param name="id">Идентификатор на родителски документ</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage PostNewRemark(int id)
        {
            UserContext userContext = this.userContextProvider.GetCurrentUserContext();

            User user = unitOfWork.Repo<User>()
                .Query()
                .Include(e => e.Unit)
                .SingleOrDefault(e => e.UserId == userContext.UserId);

            DocDO remark = GetNewRemarkInternal(id);
            remark.DocSubject = "Забележка";

            if (user.UnitId.HasValue)
            {
                DocUnitRole unitRole = this.unitOfWork.Repo<DocUnitRole>().Query()
                .SingleOrDefault(e => e.Alias.ToLower() == "From".ToLower());

                remark.DocUnits.Add(new DocUnitDO
                {
                    UnitId = user.UnitId.Value,
                    DocUnitRoleId = unitRole.DocUnitRoleId
                });
            }

            Doc remarkDoc = PostDocInternal(remark, userContext);

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = "", docId = remarkDoc.DocId });
        }

        /// <summary>
        /// Търсене на документи
        /// </summary>
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
        public HttpResponseMessage GetNewPost(
            DateTime? fromDate,
            DateTime? toDate,
            string regUri,
            string docName,
            int? docTypeId,
            int? docStatusId,
            string corrs,
            string units,
            int limit,
            int offset
            )
        {
            UserContext userContext = this.userContextProvider.GetCurrentUserContext();

            User user = unitOfWork.Repo<User>()
                .Query()
                .Include(e => e.Unit)
                .SingleOrDefault(e => e.UserId == userContext.UserId);

            List<int> corrIds = Helper.GetIdListFromString(corrs);
            List<int> unitIds = Helper.GetIdListFromString(units);
            int totalCounts;

            DocStatus docStatusFinished = this.unitOfWork.Repo<DocStatus>().GetByAlias("finished");
            DocStatus docStatusCanceled = this.unitOfWork.Repo<DocStatus>().GetByAlias("canceled");

            var predicate = PredicateBuilder.True<Doc>()
                .And(e => e.DocType.IsElectronicService)
                .And(e => e.DocStatusId != docStatusFinished.DocStatusId
                        && e.DocStatusId != docStatusCanceled.DocStatusId)
                .And(e => e.DocRelations.Any(dr => dr.DocId == dr.RootDocId));


            if (fromDate.HasValue)
            {
                predicate = predicate.And(d => d.RegDate.HasValue && d.RegDate.Value >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                predicate = predicate.And(d => d.RegDate.HasValue && d.RegDate.Value <= toDate.Value);
            }

            if (!String.IsNullOrWhiteSpace(regUri))
            {
                predicate = predicate.And(d => d.RegUri.Contains(regUri));
            }

            if (!String.IsNullOrWhiteSpace(docName))
            {
                predicate = predicate.And(d => d.DocSubject.Contains(docName));
            }

            if (docTypeId.HasValue)
            {
                predicate = predicate.And(d => d.DocTypeId == docTypeId.Value);
            }

            if (docStatusId.HasValue)
            {
                predicate = predicate.And(d => d.DocStatusId == docStatusId.Value);
            }

            if (corrIds.Any())
            {
                predicate = predicate.And(d => d.DocCorrespondents.Any(dc => corrIds.Contains(dc.CorrespondentId)));
            }

            if (unitIds.Any())
            {
                predicate = predicate.And(d => d.DocUnits.Any(du => unitIds.Contains(du.UnitId)));
            }

            IQueryable<Doc> query = this.unitOfWork.Repo<DocRelation>().Query()
                .Join(this.unitOfWork.Repo<DocRelation>().Query(), dr => dr.RootDocId, dr2 => dr2.RootDocId, (dr, dr2) => new { OrgDocId = dr.DocId, DocId = dr2.DocId })
                .Join(this.unitOfWork.Repo<DocUser>().Query(), dr => dr.DocId, du => du.DocId, (dr, du) => new { OrgDocId = dr.OrgDocId, DocUser = du })
                .Where(du => du.DocUser.UnitId == user.UnitId && du.DocUser.IsActive)
                .Join(this.unitOfWork.Repo<Doc>().Query(), du => du.OrgDocId, d => d.DocId, (du, d) => d).Distinct()
                .Where(d => !d.DocUsers.Any(du => du.UnitId == user.UnitId && du.HasRead));

            query = query
                .Where(predicate)
                .OrderByDescending(e => e.RegDate)
                .Take(10000);

            totalCounts = query.Count();

            List<NPostListItemDO> returnValue = query
                .Skip(offset)
                .Take(limit)
                .Include(e => e.DocType)
                .Include(e => e.DocDirection)
                .Include(e => e.DocStatus)
                .Include(e => e.DocUnits)
                .Include(e => e.DocSourceType)
                .ToList()
                .Select(e => new NPostListItemDO(e, user))
                .ToList();

            List<int> loadedDocIds = returnValue.Where(e => e.DocId.HasValue).Select(e => e.DocId.Value).ToList();

            this.unitOfWork.Repo<DocUser>().Query()
                .Where(du => du.UnitId == user.UnitId && du.IsActive && loadedDocIds.Contains(du.DocId))
                .ToList();

            StringBuilder sb = new StringBuilder();

            if (totalCounts >= 10000)
            {
                sb.Append("Има повече от 10000 резултата, моля, въведете допълнителни филтри.");
            }

            DocEntryType resolution = this.unitOfWork.Repo<DocEntryType>().GetByAlias("Resolution");
            DocUnitRole inCharge = this.unitOfWork.Repo<DocUnitRole>().GetByAlias("InCharge");
            DocCasePartType casePartControl = this.unitOfWork.Repo<DocCasePartType>().GetByAlias("Control");

            foreach (var item in returnValue)
            {
                var docCorrespondents = this.unitOfWork.Repo<DocCorrespondent>().Query()
                    .Include(e => e.Correspondent.CorrespondentType)
                    .Where(e => e.DocId == item.DocId)
                    .ToList();

                item.DocCorrespondents.AddRange(docCorrespondents.Select(e => new DocCorrespondentDO(e)).ToList());

                DocRelation firstResolution = this.unitOfWork.Repo<DocRelation>().Query()
                    .Include(e => e.Doc.DocEntryType)
                    .Include(e => e.Doc.DocStatus)
                    .Include(e => e.Doc.DocCasePartType)
                    .Where(e => e.Doc.DocEntryTypeId == resolution.DocEntryTypeId
                        && e.RootDocId == item.DocId
                        && e.Doc.DocCasePartTypeId != casePartControl.DocCasePartTypeId)
                    .OrderBy(e => e.DocRelationId)
                    .FirstOrDefault();

                if (firstResolution != null)
                {
                    item.ResolutionStatusColor = "black";
                    item.ResolutionStatusName = firstResolution.Doc.DocStatus.Name;

                    DocUnit inChargeUnit = this.unitOfWork.Repo<DocUnit>().Query()
                        .Include(e => e.Unit)
                        .FirstOrDefault(e => e.DocId == firstResolution.DocId && e.DocUnitRoleId == inCharge.DocUnitRoleId);
                    if (inChargeUnit != null)
                    {
                        item.ResolutionInChargeUnitName = inChargeUnit.Unit.Name;
                    }
                    else
                    {
                        item.ResolutionStatusColor = "red";
                    }
                }
                else
                {
                    item.ResolutionStatusColor = "red";
                    item.ResolutionStatusName = "Липсва";
                }
            }

            List<DocCorrespondentDO> corrList = this.unitOfWork.Repo<Correspondent>().Query()
                .Include(e => e.CorrespondentType)
                .Include(e => e.RegisterIndex)
                .Where(e => corrIds.Contains(e.CorrespondentId))
                .ToList()
                .Select(e => new DocCorrespondentDO(e, isNew: true))
                .ToList();

            List<DocUnitDO> unitList = this.unitOfWork.Repo<Unit>().Query()
                .Where(e => unitIds.Contains(e.UnitId))
                .ToList()
                .Select(e => new DocUnitDO
                {
                    DocUnitId = -1,
                    DocId = -1,
                    UnitId = e.UnitId,
                    DocUnitRoleId = -1,
                    UnitName = e.Name,
                    DocUnitRoleAlias = ""
                })
                .ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new
            {
                documents = returnValue,
                documentCount = totalCounts,
                corrs = corrList,
                units = unitList,
                msg = sb.ToString()
            });
        }

        

        

        /// <summary>
        /// Запис на документ
        /// </summary>
        /// <param name="doc">Данни на документ</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage PostDoc(DocDO doc)
        {
            try
            {
                using (TransactionScope transactionScope = this.unitOfWork.CreateTransactionScope())
                {
                    UserContext userContext = this.userContextProvider.GetCurrentUserContext();

                    Doc newDoc = PostDocInternal(doc, userContext);
                    transactionScope.Complete();
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = "", docId = newDoc.DocId });
                }
            }
            catch (Exception ex)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = ex.Message });
            }
        }

        /// <summary>
        /// Изтриване на документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <returns></returns>
        [HttpDelete]
        public HttpResponseMessage DeleteDoc(int id)
        {
            try
            {
                using (TransactionScope transactionScope = this.unitOfWork.CreateTransactionScope())
                {
                    var doc = this.unitOfWork.Repo<Doc>().Find(id);

                    if (doc == null)
                    {
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
                    }

                    string result = this.unitOfWork.Repo<Doc>().ExecSpDeleteNotRegisteredDoc(String.Empty, doc.DocId);

                    if (!string.IsNullOrEmpty(result))
                    {
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = result });
                    }
                    else
                    {
                        transactionScope.Complete();
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = ex.Message });
            }
        }

        /// <summary>
        /// Маркира документ като прочетен
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage PostDocMarkAsRead(int id)
        {
            try
            {
                using (TransactionScope transactionScope = this.unitOfWork.CreateTransactionScope())
                {
                    UserContext userContext = this.userContextProvider.GetCurrentUserContext();

                    User user = this.unitOfWork.Repo<User>().Query()
                        .Include(e => e.Unit)
                        .SingleOrDefault(e => e.UserId == userContext.UserId);

                    var query = this.unitOfWork.Repo<DocRelation>().Query()
                        .Join(this.unitOfWork.Repo<DocRelation>().Query(), dr => dr.RootDocId, dr2 => dr2.RootDocId, (dr, dr2) => new { OrgDocId = dr.DocId, DocId = dr2.DocId })
                        .Join(this.unitOfWork.Repo<DocUser>().Query(), dr => dr.DocId, du => du.DocId, (dr, du) => new { OrgDocId = dr.OrgDocId, DocUser = du })
                        .Where(e => e.DocUser.UnitId == user.UnitId && e.OrgDocId == id)
                        .Join(this.unitOfWork.Repo<Doc>().Query(), du => du.OrgDocId, d => d.DocId, (du, d) => du.DocUser)
                        .Include(du => du.DocUnitPermission)
                        .Where(e => e.IsActive)
                        .Distinct();

                    if (!query.Any())
                    {
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.Forbidden);
                    }

                    List<DocUser> docUsers = query.ToList();

                    foreach (var item in docUsers.Where(e => e.DocId == id && e.UnitId == user.UnitId))
                    {
                        item.HasRead = true;
                    }

                    this.unitOfWork.Save();
                    transactionScope.Complete();

                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = "", docId = id });
                }
            }
            catch (Exception ex)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = ex.Message });
            }
        }

        /// <summary>
        /// Маркира документ като непрочетен
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage PostDocMarkAsUnread(int id)
        {
            try
            {
                using (TransactionScope transactionScope = this.unitOfWork.CreateTransactionScope())
                {
                    UserContext userContext = this.userContextProvider.GetCurrentUserContext();

                    User user = this.unitOfWork.Repo<User>().Query()
                        .Include(e => e.Unit)
                        .SingleOrDefault(e => e.UserId == userContext.UserId);

                    var query = this.unitOfWork.Repo<DocRelation>().Query()
                        .Join(this.unitOfWork.Repo<DocRelation>().Query(), dr => dr.RootDocId, dr2 => dr2.RootDocId, (dr, dr2) => new { OrgDocId = dr.DocId, DocId = dr2.DocId })
                        .Join(this.unitOfWork.Repo<DocUser>().Query(), dr => dr.DocId, du => du.DocId, (dr, du) => new { OrgDocId = dr.OrgDocId, DocUser = du })
                        .Where(e => e.DocUser.UnitId == user.UnitId && e.OrgDocId == id)
                        .Join(this.unitOfWork.Repo<Doc>().Query(), du => du.OrgDocId, d => d.DocId, (du, d) => du.DocUser)
                        .Include(du => du.DocUnitPermission)
                        .Where(e => e.IsActive)
                        .Distinct();

                    if (!query.Any())
                    {
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.Forbidden);
                    }

                    List<DocUser> docUsers = query.ToList();

                    foreach (var item in docUsers.Where(e => e.DocId == id && e.UnitId == user.UnitId))
                    {
                        item.HasRead = false;
                    }

                    this.unitOfWork.Save();
                    transactionScope.Complete();

                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = "", docId = id });
                }
            }
            catch (Exception ex)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = ex.Message });
            }
        }

        /// <summary>
        /// Прикачване на заявление към документ
        /// </summary>
        /// <param name="docId">Идентификатор на документ</param>
        /// <param name="docFileTypeId">Идентификатор на тип на документ</param>
        /// <param name="isSigned">Флаг за подписване на заявлението</param>
        /// <param name="doc">Данни на документа</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage PostCreateComplexFile(int docId, int docFileTypeId, bool isSigned, string docFileKindAlias, DocDO doc)
        {
            DocFileType docFileType = this.unitOfWork.Repo<DocFileType>().Find(docFileTypeId);

            DocFileKind docFileKind = this.unitOfWork.Repo<DocFileKind>().Query()
                .SingleOrDefault(e => e.Alias.ToLower() == docFileKindAlias.ToLower());

            int? parentDocId = null;
            int? rootDocId = null;
            //int? parentDocTypeId = null;

            var docRelation = this.unitOfWork.Repo<DocRelation>().GetByDocId(docId);
            parentDocId = docRelation.ParentDocId;
            rootDocId = docRelation.RootDocId;
            //todo wrong wrong wrong, there can be no parent doc
            //parentDocTypeId = this.unitOfWork.Repo<Doc>().Find(parentDocId.Value).DocTypeId;

            Tuple<string, byte[]> nameAndContent = GetDocFileNameAndBytesContent(docFileType, null, parentDocId, rootDocId);//TODO!!!
            Guid fileKey = this.abbcdnStorage.UploadFile(nameAndContent.Item2, nameAndContent.Item1);

            var returnValue = new DocFileDO
            {
                DocFileTypeId = docFileType.DocFileTypeId,
                DocFileTypeIsEditable = docFileType.IsEditable,
                DocFileTypeName = docFileType.Name,
                DocFileKindId = docFileKind.DocFileKindId,
                DocFileKindName = docFileKind.Name,
                DocFileKindAlias = docFileKind.Alias,
                Name = nameAndContent.Item1,
                DocFileName = nameAndContent.Item1,
                DocContentStorage = String.Empty,
                DocFileContentId = fileKey,
                IsPrimary = false, //todo
                IsSigned = isSigned,
                IsActive = true,
                IsNew = true
            };

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
        }

        #endregion

        #region DocManagement

        /// <summary>
        /// Управление на документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage PostNext(int id, bool? closure)
        {
            try
            {
                using (TransactionScope transactionScope = this.unitOfWork.CreateTransactionScope())
                {

                    var doc = this.unitOfWork.Repo<Doc>()
                        .Find(id,
                            e => e.DocEntryType,
                            e => e.DocFiles,
                            e => e.DocCorrespondents,
                            e => e.DocWorkflows,
                            e => e.DocStatus,
                            e => e.DocUnits);

                    if (doc == null)
                    {
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
                    }

                    DocStatus newDocStatus = null;

                    switch (doc.DocStatus.Alias)
                    {
                        case "Draft":
                            newDocStatus = this.unitOfWork.Repo<DocStatus>().GetByAlias("Prepared");
                            doc.DocStatusId = newDocStatus.DocStatusId;
                            doc.DocStatus = newDocStatus;
                            break;
                        case "Prepared":
                            newDocStatus = this.unitOfWork.Repo<DocStatus>().GetByAlias("Processed");
                            doc.DocStatusId = newDocStatus.DocStatusId;
                            doc.DocStatus = newDocStatus;

                            #region Sending ResolutionOrTaskAssigned mail

                            List<DocUnit> docUnits = new List<DocUnit>();

                            if (doc.DocEntryType.Alias == "Resolution" || doc.DocEntryType.Alias == "Task")
                            {
                                DocUnitRole docUnitRoleInCharge = this.unitOfWork.Repo<DocUnitRole>().GetByAlias("InCharge");
                                DocUnitRole docUnitRoleControlling = this.unitOfWork.Repo<DocUnitRole>().GetByAlias("Controlling");
                                docUnits.AddRange(doc.DocUnits.Where(du => du.DocUnitRoleId == docUnitRoleInCharge.DocUnitRoleId || du.DocUnitRoleId == docUnitRoleControlling.DocUnitRoleId).ToList());
                            }
                            else if (doc.DocEntryType.Alias == "Document")
                            {
                                DocUnitRole docUnitRoleTo = this.unitOfWork.Repo<DocUnitRole>().GetByAlias("To");
                                docUnits.AddRange(doc.DocUnits.Where(du => du.DocUnitRoleId == docUnitRoleTo.DocUnitRoleId).ToList());
                            }

                            if (docUnits.Count > 0)
                            {
                                AdministrativeEmailType taskAssignedEmailType = this.unitOfWork.Repo<AdministrativeEmailType>().GetByAlias("ResolutionOrTaskAssigned");
                                AdministrativeEmailStatus emailStatusNew = this.unitOfWork.Repo<AdministrativeEmailStatus>().GetByAlias("New");

                                foreach (var docUnit in docUnits)
                                {
                                    User toUser = this.unitOfWork.Repo<User>().GetByUnitId(docUnit.UnitId);
                                    if (!String.IsNullOrWhiteSpace(toUser.Email))
                                    {
                                        AdministrativeEmail email = new AdministrativeEmail();
                                        email.TypeId = taskAssignedEmailType.AdministrativeEmailTypeId;
                                        email.UserId = toUser.UserId;
                                        email.Param1 = String.Format(Request.RequestUri.OriginalString.Replace(Request.RequestUri.PathAndQuery, "/#/docs/{0}"), doc.DocId);
                                        email.StatusId = emailStatusNew.AdministrativeEmailStatusId;
                                        email.Subject = taskAssignedEmailType.Subject;
                                        email.Body = taskAssignedEmailType.Body.Replace("@@Param1", email.Param1);

                                        this.unitOfWork.Repo<AdministrativeEmail>().Add(email);
                                    }
                                }
                            }

                            #endregion

                            break;
                        case "Processed":
                            newDocStatus = this.unitOfWork.Repo<DocStatus>().GetByAlias("Finished");

                            if (doc.IsCase)
                            {
                                DocRelation dr = this.unitOfWork.Repo<DocRelation>().Query()
                                    .FirstOrDefault(e => e.DocId == id);

                                if (dr != null)
                                {
                                    int caseDocId = dr.RootDocId.Value;

                                    DocCasePartType dcpt = this.unitOfWork.Repo<DocCasePartType>().GetByAlias("Control");
                                    DocStatus cancelStatus = this.unitOfWork.Repo<DocStatus>().GetByAlias("Canceled");

                                    var docRelations = this.unitOfWork.Repo<DocRelation>().Query()
                                        .Include(e => e.Doc.DocCasePartType)
                                        .Include(e => e.Doc.DocCasePartMovements.Select(dc => dc.User))
                                        .Include(e => e.Doc.DocDirection)
                                        .Include(e => e.Doc.DocType)
                                        .Include(e => e.Doc.DocStatus)
                                        .Include(e => e.RootDoc)
                                        .Include(e => e.ParentDoc.DocType)
                                        .Where(e => e.RootDocId == caseDocId
                                            && e.Doc.DocId != id
                                            && e.Doc.DocCasePartTypeId != dcpt.DocCasePartTypeId
                                            && e.Doc.DocStatusId != newDocStatus.DocStatusId
                                            && e.Doc.DocStatusId != cancelStatus.DocStatusId)
                                        .ToList();

                                    if (docRelations.Any())
                                    {
                                        if (closure.HasValue && closure.Value)
                                        {
                                            foreach (var item in docRelations)
                                            {
                                                item.Doc.DocStatusId = newDocStatus.DocStatusId;
                                                item.Doc.DocStatus = newDocStatus;
                                            }
                                        }
                                        else
                                        {
                                            var docRelationDOs = docRelations.Select(e => new DocRelationDO(e)).ToList();

                                            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { needClosure = true, docRelations = docRelationDOs });
                                        }
                                    }
                                }
                            }

                            doc.DocStatusId = newDocStatus.DocStatusId;
                            doc.DocStatus = newDocStatus;
                            break;
                        case "Finished":
                            break;
                        default:
                            break;
                    };

                    this.unitOfWork.Save();
                    transactionScope.Complete();

                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = "", docId = id });
                }
            }
            catch (Exception ex)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = ex.Message });
            }
        }

        /// <summary>
        /// Отхвърляне на документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage PostCancel(int id)
        {
            try
            {
                using (TransactionScope transactionScope = this.unitOfWork.CreateTransactionScope())
                {

                    var doc = this.unitOfWork.Repo<Doc>().Find(id);

                    if (doc == null)
                    {
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
                    }

                    DocStatus newDocStatus = this.unitOfWork.Repo<DocStatus>().GetByAlias("Canceled");
                    doc.DocStatusId = newDocStatus.DocStatusId;
                    doc.DocStatus = newDocStatus;

                    this.unitOfWork.Save();
                    transactionScope.Complete();

                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = "", docId = id });
                }
            }
            catch (Exception ex)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = ex.Message });
            }
        }

        /// <summary>
        /// Сторниране на управление на документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage PostReverse(int id)
        {
            try
            {
                using (TransactionScope transactionScope = this.unitOfWork.CreateTransactionScope())
                {
                    var doc = this.unitOfWork.Repo<Doc>()
                        .Find(id,
                            e => e.DocEntryType,
                            e => e.DocFiles,
                            e => e.DocCorrespondents,
                            e => e.DocWorkflows,
                            e => e.DocStatus);

                    if (doc == null)
                    {
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
                    }

                    DocStatus newDocStatus = null;

                    switch (doc.DocStatus.Alias)
                    {
                        case "Draft":
                            break;
                        case "Prepared":
                            newDocStatus = this.unitOfWork.Repo<DocStatus>().GetByAlias("Draft");
                            doc.DocStatusId = newDocStatus.DocStatusId;
                            doc.DocStatus = newDocStatus;
                            break;
                        case "Processed":
                            newDocStatus = this.unitOfWork.Repo<DocStatus>().GetByAlias("Prepared");
                            doc.DocStatusId = newDocStatus.DocStatusId;
                            doc.DocStatus = newDocStatus;
                            break;
                        case "Finished":
                        case "Canceled":
                            newDocStatus = this.unitOfWork.Repo<DocStatus>().GetByAlias("Processed");
                            doc.DocStatusId = newDocStatus.DocStatusId;
                            doc.DocStatus = newDocStatus;
                            break;
                        default:
                            break;
                    };

                    this.unitOfWork.Save();
                    transactionScope.Complete();

                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = "", docId = id });
                }
            }
            catch (Exception ex)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = ex.Message });
            }
        }

        #endregion

        #region DocElectronicStages

        /// <summary>
        /// Нов етап на документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetNewDocElectronicServiceStage(int id)
        {
            int caseDocId = GetCaseId(id);

            var caseDoc = this.unitOfWork.Repo<Doc>()
                .Find(caseDocId, e => e.DocType);

            if (caseDoc.DocType.IsElectronicService)
            {

                var returnValue = new DocElectronicServiceStageDO
                {
                    DocId = caseDocId,
                    ElectronicServiceStageDocTypeId = caseDoc.DocType.DocTypeId,
                    StartingDate = DateTime.Now
                };

                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
            }
            else
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }
        }

        /// <summary>
        /// Връща текущ етап на документ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetCurrentDocElectronicServiceStage(int id)
        {
            int caseDocId = GetCaseId(id);

            var caseDoc = this.unitOfWork.Repo<Doc>()
                .Find(caseDocId,
                e => e.DocType,
                e => e.DocElectronicServiceStages.Select(d => d.ElectronicServiceStage));

            if (caseDoc == null)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
            }
            else if (!caseDoc.DocType.IsElectronicService)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }

            DocElectronicServiceStage currentStage = caseDoc.DocElectronicServiceStages.SingleOrDefault(e => e.IsCurrentStage);

            var returnValue = new DocElectronicServiceStageDO(currentStage);

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
        }

        /// <summary>
        /// Запис на нов етап на документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <param name="data">Данни на етап</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage PostDocElectronicServiceStage(int id, DocElectronicServiceStageDO data)
        {
            try
            {
                using (TransactionScope transactionScope = this.unitOfWork.CreateTransactionScope())
                {
                    int caseDocId = GetCaseId(id);

                    var caseDoc = this.unitOfWork.Repo<Doc>()
                        .Find(caseDocId,
                        e => e.DocType,
                        e => e.DocElectronicServiceStages,
                        e => e.DocCorrespondents,
                        e => e.DocCorrespondentContacts);

                    if (caseDoc == null)
                    {
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
                    }
                    else if (!caseDoc.DocType.IsElectronicService)
                    {
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.NotAcceptable);
                    }

                    DocElectronicServiceStage currentStage = caseDoc.DocElectronicServiceStages.SingleOrDefault(e => e.IsCurrentStage);
                    if (currentStage != null)
                    {
                        currentStage.IsCurrentStage = false;
                        if (!currentStage.EndingDate.HasValue)
                        {
                            currentStage.EndingDate = data.StartingDate.Value;
                        }
                    }

                    DocElectronicServiceStage newStage = new DocElectronicServiceStage();
                    newStage.DocId = caseDoc.DocId;
                    newStage.ElectronicServiceStageId = data.ElectronicServiceStageId.Value;
                    newStage.EndingDate = data.EndingDate;
                    newStage.ExpectedEndingDate = data.ExpectedEndingDate;
                    newStage.IsCurrentStage = true;
                    newStage.StartingDate = data.StartingDate.Value;

                    this.unitOfWork.Repo<DocElectronicServiceStage>().Add(newStage);

                    //Sending email
                    AdministrativeEmailType emailType = this.unitOfWork.Repo<AdministrativeEmailType>().GetByAlias("ElectronicServiceStageChanged");
                    AdministrativeEmailStatus emailStatus = this.unitOfWork.Repo<AdministrativeEmailStatus>().GetByAlias("New");

                    foreach (var docCorrespondent in caseDoc.DocCorrespondents)
                    {
                        Correspondent correspondent = this.unitOfWork.Repo<Correspondent>().Find(docCorrespondent.CorrespondentId, e => e.CorrespondentContacts);

                        if (correspondent != null && !String.IsNullOrWhiteSpace(correspondent.Email))
                        {
                            AdministrativeEmail email = new AdministrativeEmail();
                            email.TypeId = emailType.AdministrativeEmailTypeId;
                            email.CorrespondentId = correspondent.CorrespondentId;
                            email.Param1 = data.ElectronicServiceStageName;
                            email.StatusId = emailStatus.AdministrativeEmailStatusId;
                            email.Subject = emailType.Subject;
                            email.Body = emailType.Body.Replace("@@Param1", email.Param1);

                            this.unitOfWork.Repo<AdministrativeEmail>().Add(email);
                        }
                    }

                    this.unitOfWork.Save();
                    transactionScope.Complete();

                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, true);
                }
            }
            catch (Exception ex)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }
        }

        /// <summary>
        /// Рекдация на етап на документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <param name="data">Нови данни на етап</param>
        /// <returns></returns>
        [HttpPut]
        public HttpResponseMessage PutDocElectronicServiceStage(int id, DocElectronicServiceStageDO data)
        {
            try
            {
                using (TransactionScope transactionScope = this.unitOfWork.CreateTransactionScope())
                {
                    int caseDocId = GetCaseId(id);

                    var caseDoc = this.unitOfWork.Repo<Doc>()
                        .Find(caseDocId,
                        e => e.DocType,
                        e => e.DocElectronicServiceStages);

                    if (caseDoc == null)
                    {
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
                    }
                    else if (!caseDoc.DocType.IsElectronicService)
                    {
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.NotAcceptable);
                    }

                    DocElectronicServiceStage currentStage = caseDoc.DocElectronicServiceStages.SingleOrDefault(e => e.IsCurrentStage);
                    currentStage.ElectronicServiceStageId = data.ElectronicServiceStageId.Value;
                    currentStage.EndingDate = data.EndingDate;
                    currentStage.ExpectedEndingDate = data.ExpectedEndingDate;
                    currentStage.IsCurrentStage = true;
                    currentStage.StartingDate = data.StartingDate.Value;

                    this.unitOfWork.Save();

                    transactionScope.Complete();
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, true);
                }
            }
            catch (Exception ex)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }
        }

        /// <summary>
        /// Изтриване на етап на документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <returns></returns>
        [HttpDelete]
        public HttpResponseMessage DeleteDocElectronicServiceStage(int id)
        {
            try
            {
                using (TransactionScope transactionScope = this.unitOfWork.CreateTransactionScope())
                {
                    int caseDocId = GetCaseId(id);

                    var caseDoc = this.unitOfWork.Repo<Doc>()
                        .Find(caseDocId,
                        e => e.DocType,
                        e => e.DocElectronicServiceStages);

                    if (caseDoc == null)
                    {
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
                    }
                    else if (!caseDoc.DocType.IsElectronicService)
                    {
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.NotAcceptable);
                    }

                    if (caseDoc.DocElectronicServiceStages.Count > 1)
                    {
                        DocElectronicServiceStage currentStage = caseDoc.DocElectronicServiceStages.SingleOrDefault(e => e.IsCurrentStage);
                        DocElectronicServiceStage previousStage = caseDoc.DocElectronicServiceStages.Where(e => !e.IsCurrentStage).OrderByDescending(e => e.DocElectronicServiceStageId).FirstOrDefault();
                        if (currentStage != null && previousStage != null)
                        {
                            previousStage.IsCurrentStage = true;

                            this.unitOfWork.Repo<DocElectronicServiceStage>().Remove(currentStage);
                            this.unitOfWork.Save();
                        }
                    }

                    transactionScope.Complete();
                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = "", docId = id });
                }
            }
            catch (Exception ex)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = ex.Message });
            }
        }

        /// <summary>
        /// Обновяване на етап на документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <param name="data">Данни на етап</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage PostUpdateDocElectronicServiceStage(int id, DocElectronicServiceStageDO data)
        {
            int caseDocId = GetCaseId(id);

            var caseDoc = this.unitOfWork.Repo<Doc>()
                .Find(caseDocId,
                e => e.DocType,
                e => e.DocElectronicServiceStages.Select(d => d.ElectronicServiceStage));

            if (caseDoc == null)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var electronicServiceStage = this.unitOfWork.Repo<ElectronicServiceStage>().Query()
                .Include(e => e.ElectronicServiceStageExecutors.Select(ee => ee.Unit))
                .FirstOrDefault(e => e.ElectronicServiceStageId == data.ElectronicServiceStageId);

            StringBuilder sb = new StringBuilder();
            var executors = electronicServiceStage.ElectronicServiceStageExecutors.Where(e => e.IsActive).ToList();
            foreach (var executor in executors)
            {
                if (executor.Unit != null)
                {
                    sb.AppendLine(executor.Unit.Name);
                }
            }

            DateTime startingDate = data.StartingDate.HasValue ? data.StartingDate.Value : DateTime.Now.Date;
            int duration = electronicServiceStage.Duration.HasValue ? electronicServiceStage.Duration.Value : 0;
            DateTime expectedEndingDate = startingDate.AddDays(duration);

            if (!electronicServiceStage.IsDurationReset)
            {
                List<DocElectronicServiceStage> sameDocElectronicServiceStages = caseDoc.DocElectronicServiceStages.Where(e => e.ElectronicServiceStageId == electronicServiceStage.ElectronicServiceStageId).ToList();

                foreach (var item in sameDocElectronicServiceStages)
                {
                    if (item.EndingDate.HasValue)
                    {
                        TimeSpan timespan = item.EndingDate.Value.Subtract(item.StartingDate);
                        if (timespan.Ticks > 0)
                        {
                            expectedEndingDate.Subtract(timespan);
                            if (expectedEndingDate < startingDate)
                            {
                                expectedEndingDate = startingDate;
                                break;
                            }
                        }
                    }
                }
            }

            var result = new
            {
                electronicServiceStageId = electronicServiceStage.ElectronicServiceStageId,
                docTypeId = electronicServiceStage.DocTypeId,
                name = electronicServiceStage.Name,
                alias = electronicServiceStage.Alias,
                duration = electronicServiceStage.Duration,
                isFirstByDefault = electronicServiceStage.IsFirstByDefault,
                isDurationReset = electronicServiceStage.IsDurationReset,
                isLastStage = electronicServiceStage.IsLastStage,
                isActive = electronicServiceStage.IsActive,
                electronicServiceStageExecutors = sb.ToString(),
                expectedEndingDate = expectedEndingDate
            };

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, result);
        }

        #endregion

        #region DocWorkflows

        /// <summary>
        /// Генерира нов запис за ръководство на документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <param name="type">Тип на действие</param>
        /// <param name="action">Ключова дума на действие</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetNewDocWorkflow(int id, string type, string action)
        {
            UserContext userContext = this.userContextProvider.GetCurrentUserContext();

            User user = this.unitOfWork.Repo<User>().Query()
                .Include(e => e.Unit.UnitType)
                .SingleOrDefault(e => e.UserId == userContext.UserId);

            var docUnitRole = this.unitOfWork.Repo<DocUnitRole>().Query()
                .Single(e => e.Alias.ToLower() == type.ToLower());

            DocWorkflowAction wfAction = this.unitOfWork.Repo<DocWorkflowAction>().Query()
                .SingleOrDefault(e => e.Alias.ToLower() == action);

            var returnValue = new DocWorkflowDO
            {
                DocId = id,
                DocWorkflowActionId = wfAction.DocWorkflowActionId,
                DocWorkflowActionName = wfAction.Name,
                DocWorkflowActionAlias = wfAction.Alias,
                UserId = userContext.UserId,
                Username = userContext.FullName,
                UserUnitId = user.UnitId,
                UserUnitName = user.Unit != null ? user.Unit.Name : string.Empty,
                IsNew = true
            };

            if (!returnValue.IsRequest)
            {
                returnValue.CurrentDocUnit = new DocUnitDO
                {
                    UnitId = user.UnitId.Value,
                    UnitName = user.Unit.Name,
                    UnitTypeAlias = user.Unit.UnitType.Alias.ToLower(),
                    DocUnitRoleId = docUnitRole.DocUnitRoleId,
                    DocUnitRoleAlias = docUnitRole.Alias.ToLower(),
                    IsSelected = true,
                    IsNew = true
                };
            }
            else
            {
                returnValue.CurrentDocUnit = null;
            }

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
        }

        #endregion

        #region DocClassifications

        /// <summary>
        /// Генерира нова класификация на документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetNewDocClassification(int id)
        {
            UserContext userContext = this.userContextProvider.GetCurrentUserContext();

            User user = this.unitOfWork.Repo<User>().Query()
                .Include(e => e.Unit)
                .SingleOrDefault(e => e.UserId == userContext.UserId);

            var returnValue = new DocClassificationDO
            {
                DocId = id,
                ClassificationDate = DateTime.Now,
                ClassificationByUserId = user.UserId,
                IsNew = true
            };

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
        }

        #endregion

        #region DocFiles

        /// <summary>
        /// При преглед на заявление копира Xml стойността
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <param name="df">Данни на заявление</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage PostTempDocFileContent(int id, DocFileDO df)
        {
            try
            {
                using (TransactionScope transactionScope = this.unitOfWork.CreateTransactionScope())
                {
                    VisualizationMode mode = VisualizationMode.EditWithoutSignature;
                    if (df.VisualizationMode == "display")
                    {
                        mode = VisualizationMode.DisplayWithoutSignature;
                    }
                    else if (df.VisualizationMode == "edit")
                    {
                        if (df.IsSigned)
                        {
                            mode = VisualizationMode.EditWithSignature;
                        }
                    }

                    Ticket ticket = new Ticket();
                    ticket.TicketId = Guid.NewGuid();
                    ticket.DocFileId = df.DocFileId.Value;
                    ticket.OldKey = df.DocFileContentId;
                    ticket.VisualizationMode = (int)mode;

                    this.unitOfWork.Repo<Ticket>().Add(ticket);
                    this.unitOfWork.Save();
                    transactionScope.Complete();

                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = "", ticket = ticket });
                }
            }
            catch (Exception ex)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = ex.Message });
            }
        }

        #endregion

        #region DocSignature

        /// <summary>
        /// Електронно подписване на документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage PostDocSignature(int id)
        {
            try
            {
                using (TransactionScope transactionScope = this.unitOfWork.CreateTransactionScope())
                {
                    string result = "";

                    var doc = this.unitOfWork.Repo<Doc>()
                       .Find(id,
                            e => e.DocFiles,
                            e => e.DocCorrespondents,
                            e => e.DocWorkflows,
                            e => e.DocStatus);

                    if (doc == null)
                    {
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
                    }

                    DocWorkflowAction docWorkflowAction = unitOfWork.Repo<DocWorkflowAction>()
                        .Query()
                        .SingleOrDefault(e => e.Alias.ToLower() == "electronicsign");

                    DocWorkflow docWorkflow = new DocWorkflow();
                    docWorkflow.Doc = doc;
                    docWorkflow.DocWorkflowActionId = docWorkflowAction.DocWorkflowActionId;
                    docWorkflow.DocWorkflowAction = docWorkflowAction;
                    docWorkflow.EventDate = DateTime.Now;
                    docWorkflow.UserId = this.SystemUser.UserId;

                    doc.DocWorkflows.Add(docWorkflow);

                    doc.IsSigned = true;

                    var docFile = doc.DocFiles.FirstOrDefault(e => e.IsPrimary);
                    if (docFile != null)
                    {
                        docFile.IsSigned = true;

                        var fileInfo = this.abbcdnStorage.DownloadFile(docFile.DocFileContentId);

                        if (fileInfo != null)
                        {
                            byte[] signedContent = XmlSigner.AddSignatureToBytesContent(fileInfo.ContentBytes);
                            if (signedContent != null)
                            {
                                Guid fileKey = this.abbcdnStorage.UploadFile(signedContent, fileInfo.FileName);
                                docFile.DocFileContentId = fileKey;

                                this.unitOfWork.Save();
                            }
                            else
                            {
                                result = "Възникна грешка при подписването. Моля свържете се със системният администратор.";
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(result))
                    {
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = result });
                    }
                    else
                    {
                        transactionScope.Complete();
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = "", docId = id });
                    }
                }
            }
            catch (Exception ex)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = ex.Message });
            }
        }

        /// <summary>
        /// Премахване на ел. подпис на документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage PostUndoDocSignature(int id)
        {
            try
            {
                using (TransactionScope transactionScope = this.unitOfWork.CreateTransactionScope())
                {
                    string result = "";

                    var doc = this.unitOfWork.Repo<Doc>()
                       .Find(id,
                            e => e.DocFiles,
                            e => e.DocCorrespondents,
                            e => e.DocWorkflows,
                            e => e.DocStatus);

                    if (doc == null)
                    {
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
                    }

                    var docFile = doc.DocFiles.FirstOrDefault(e => e.IsPrimary);
                    if (docFile != null)
                    {
                        var fileInfo = this.abbcdnStorage.DownloadFile(docFile.DocFileContentId);

                        if (fileInfo != null)
                        {
                            byte[] unsignedContent = XmlSigner.RemoveSignatureFromBytesContent(fileInfo.ContentBytes);
                            if (unsignedContent != null)
                            {
                                Guid fileKey = this.abbcdnStorage.UploadFile(unsignedContent, fileInfo.FileName);
                                docFile.DocFileContentId = fileKey;

                                if (doc.DocWorkflows.Any())
                                {
                                    DocWorkflowAction docWorkflowAction = unitOfWork.Repo<DocWorkflowAction>()
                                        .Query()
                                        .SingleOrDefault(e => e.Alias.ToLower() == "ElectronicSign".ToLower());

                                    var workflow = doc.DocWorkflows.FirstOrDefault(e => e.DocWorkflowActionId == docWorkflowAction.DocWorkflowActionId);
                                    if (workflow != null)
                                    {
                                        this.unitOfWork.Repo<DocWorkflow>().Remove(workflow);
                                        doc.DocWorkflows.Remove(workflow);
                                    }
                                }

                                doc.IsSigned = false;
                                docFile.IsSigned = false;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(result))
                    {
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = result });
                    }
                    else
                    {
                        transactionScope.Complete();
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = "", docId = id });
                    }
                }
            }
            catch (Exception ex)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = ex.Message });
            }
        }

        #endregion

        #region DocRegister

        /// <summary>
        /// Регистриране на документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage PostDocRegistration(int id)
        {
            try
            {
                using (TransactionScope transactionScope = this.unitOfWork.CreateTransactionScope())
                {
                    var doc = this.unitOfWork.Repo<Doc>()
                        .Find(id,
                            e => e.DocFiles,
                            e => e.DocCorrespondents,
                            e => e.DocWorkflows,
                            e => e.DocStatus);

                    if (doc == null)
                    {
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
                    }

                    this.unitOfWork.Repo<Doc>().RegisterDoc(this.unitOfWork, this.SystemUser.UserId, doc);
                    AddDocumentUriToContent(id);

                    this.unitOfWork.Save();
                    transactionScope.Complete();

                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = "", docId = id });
                }
            }
            catch (Exception ex)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = ex.Message });
            }
        }

        /// <summary>
        /// Премахване на регистрация на документ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage PostUndoDocRegistration(int id)
        {
            try
            {
                using (TransactionScope transactionScope = this.unitOfWork.CreateTransactionScope())
                {
                    var doc = this.unitOfWork.Repo<Doc>()
                       .Find(id,
                           e => e.DocFiles,
                           e => e.DocCorrespondents,
                           e => e.DocWorkflows,
                           e => e.DocStatus);

                    if (doc == null)
                    {
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
                    }

                    this.unitOfWork.Repo<Doc>().UnregisterDoc(this.unitOfWork, doc);
                    RemoveDocumentUriFromContent(id);

                    this.unitOfWork.Save();
                    transactionScope.Complete();

                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = "", docId = id });
                }
            }
            catch (Exception ex)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = ex.Message });
            }
        }

        #endregion

        #region DocCasePartType

        /// <summary>
        /// Редакция на раздел на преписка, в който се намира документ
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <param name="docCasePartTypeId">Идентификатор на тип раздел</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage PostDocCasePartType(int id, int docCasePartTypeId)
        {
            try
            {
                using (TransactionScope transactionScope = this.unitOfWork.CreateTransactionScope())
                {
                    UserContext userContext = this.userContextProvider.GetCurrentUserContext();

                    var doc = this.unitOfWork.Repo<Doc>()
                        .Find(id,
                            e => e.DocFiles,
                            e => e.DocCorrespondents,
                            e => e.DocWorkflows,
                            e => e.DocStatus);

                    if (doc == null)
                    {
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
                    }

                    doc.DocCasePartTypeId = docCasePartTypeId;

                    DocCasePartMovement dcpm = new DocCasePartMovement();
                    dcpm.Doc = doc;
                    dcpm.DocCasePartTypeId = doc.DocCasePartTypeId;
                    dcpm.UserId = userContext.UserId;
                    dcpm.MovementDate = DateTime.Now;
                    this.unitOfWork.Repo<DocCasePartMovement>().Add(dcpm);

                    this.unitOfWork.Save();
                    this.unitOfWork.Repo<Doc>().ExecSpSetDocUsers(doc.DocId);
                    transactionScope.Complete();

                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, true);
                }
            }
            catch (Exception ex)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }
        }

        #endregion

        #region DocSendEmail

        //todo remake of emails

        /// <summary>
        /// Генерира нов имейл към кореспондент
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetDocSendEmail(int id)
        {
            try
            {
                using (TransactionScope transactionScope = this.unitOfWork.CreateTransactionScope())
                {
                    Doc doc = this.unitOfWork.Repo<Doc>()
                       .Find(id,
                            e => e.DocFiles,
                            e => e.DocCorrespondents.Select(dc => dc.Correspondent),
                            e => e.DocStatus);

                    if (doc == null)
                    {
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
                    }

                    DocSendEmailDO email = new DocSendEmailDO();
                    Correspondent correspondent = new Correspondent();

                    if (doc.DocCorrespondents.Any())
                    {
                        correspondent = doc.DocCorrespondents.FirstOrDefault().Correspondent;

                        email.EmailTo = correspondent.Email;
                        email.EmailToName = correspondent.DisplayName;
                        email.CorrespondentId = correspondent.CorrespondentId;
                    }
                    else
                    {
                        email.EmailTo = "";
                    }

                    AdministrativeEmailType emailType = this.unitOfWork.Repo<AdministrativeEmailType>().Query()
                        .SingleOrDefault(e => e.Alias.ToLower() == "CorrespondentEmail".ToLower());

                    email.TypeId = emailType.AdministrativeEmailTypeId;

                    email.Param1 = ConfigurationManager.AppSettings["PortalWebAddress"];
                    email.Param2 = string.Empty;
                    email.Param3 = string.Empty;
                    email.Param4 = string.Empty;
                    email.Param5 = string.Empty;

                    email.Subject = emailType.Subject.Replace("@@Param1", email.Param1)
                                .Replace("@@Param2", email.Param2)
                                .Replace("@@Param3", email.Param3)
                                .Replace("@@Param4", email.Param4)
                                .Replace("@@Param5", email.Param5);

                    email.Body = emailType.Body.Replace("@@Param1", email.Param1)
                                .Replace("@@Param2", email.Param2)
                                .Replace("@@Param3", email.Param3)
                                .Replace("@@Param4", email.Param4)
                                .Replace("@@Param5", email.Param5); ;

                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, email);
                }
            }
            catch (Exception ex)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = ex.Message });
            }
        }

        /// <summary>
        /// Запис на имейл към кореспондент
        /// </summary>
        /// <param name="id">Идентификатор на документ</param>
        /// <param name="mail">Данни на имейл</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage PostDocSendEmail(int id, DocSendEmailDO mail)
        {
            try
            {
                using (TransactionScope transactionScope = this.unitOfWork.CreateTransactionScope())
                {
                    Doc doc = this.unitOfWork.Repo<Doc>()
                       .Find(id,
                            e => e.DocFiles,
                            e => e.DocCorrespondents.Select(dc => dc.Correspondent),
                            e => e.DocStatus);

                    if (doc == null)
                    {
                        return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
                    }

                    AdministrativeEmail adminEmail = new AdministrativeEmail();
                    AdministrativeEmailStatus adminEmailStatus = this.unitOfWork.Repo<AdministrativeEmailStatus>().GetByAlias("New");
                    adminEmail.StatusId = adminEmailStatus.AdministrativeEmailStatusId;
                    adminEmail.TypeId = mail.TypeId.Value;
                    adminEmail.CorrespondentContactId = mail.CorrespondentContactId;
                    adminEmail.CorrespondentId = mail.CorrespondentId;
                    adminEmail.UserId = mail.UserId;
                    adminEmail.Param1 = mail.Param1;
                    adminEmail.Param2 = mail.Param2;
                    adminEmail.Param3 = mail.Param3;
                    adminEmail.Param4 = mail.Param4;
                    adminEmail.Param5 = mail.Param5;
                    adminEmail.Subject = mail.Subject;
                    adminEmail.Body = mail.Body;

                    this.unitOfWork.Repo<AdministrativeEmail>().Add(adminEmail);
                    this.unitOfWork.Save();

                    transactionScope.Complete();

                    return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = "", docId = id });
                }
            }
            catch (Exception ex)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { err = ex.Message });
            }
        }

        #endregion
        */

        /* BASE CONTROLLER
         *private User _systemUser;

        /// <summary>
        /// Достъп до системен потребител
        /// </summary>
        protected User SystemUser
        {
            get
            {
                if (_systemUser == null)
                {
                    _systemUser = this.unitOfWork.Repo<User>().Query().FirstOrDefault(e => e.Username == "systemUser");
                }

                return _systemUser;
            }
        }

        #region Protected methods

        /// <summary>
        /// Генерира нова документ от тип резолюция
        /// </summary>
        /// <param name="id">Идентификатор на родителския документ</param>
        /// <returns></returns>
        protected DocDO GetNewResolutionInternal(int id)
        {
            DateTime currentDate = DateTime.Now;

            DocRelation currentDocRelation = this.unitOfWork.Repo<DocRelation>().Query()
                .FirstOrDefault(e => e.DocId == id); //todo not working if document takes part in multiple cases

            DocRelationDO drDO = new DocRelationDO();
            drDO.RootDocId = currentDocRelation.RootDocId;
            drDO.ParentDocId = id;

            List<DocRelationDO> docRelations = new List<DocRelationDO>();
            docRelations.Add(drDO);

            DocEntryType entry = this.unitOfWork.Repo<DocEntryType>().GetByAlias("resolution");
            DocDirection direction = this.unitOfWork.Repo<DocDirection>().GetByAlias("internal");
            DocType type = this.unitOfWork.Repo<DocType>().GetByAlias("resolution");
            DocStatus status = this.unitOfWork.Repo<DocStatus>().GetByAlias("draft");
            AssignmentType assignmentType = this.unitOfWork.Repo<AssignmentType>().GetByAlias("withdeadline");
            DocCasePartType casePart = this.unitOfWork.Repo<DocCasePartType>().GetByAlias("internal");
            DocFormatType formatType = this.unitOfWork.Repo<DocFormatType>().GetByAlias("Electronic");

            var returnValue = new DocDO()
            {
                DocId = null,
                DocDirectionId = direction.DocDirectionId,
                DocDirectionAlias = direction.Alias,
                DocDirectionName = direction.Name,
                DocEntryTypeId = entry.DocEntryTypeId,
                DocEntryTypeAlias = entry.Alias,
                DocEntryTypeName = entry.Name,
                DocSubject = null,
                DocBody = null,
                DocStatusId = status.DocStatusId,
                DocStatusAlias = status.Alias,
                DocStatusName = status.Name,
                DocTypeId = type.DocTypeId,
                DocTypeAlias = type.Alias,
                DocTypeName = type.Name,
                DocFormatTypeId = formatType.DocFormatTypeId,
                DocCasePartTypeId = casePart.DocCasePartTypeId,
                DocCasePartTypeAlias = casePart.Alias,
                DocCasePartTypeName = casePart.Name,
                AssignmentDate = DateTime.Now,
                AssignmentTypeId = assignmentType.AssignmentTypeId,
                IsCase = false,
                IsRegistered = false,
                IsSigned = false,
                DocRegisterId = null,
                RegNumber = null,
                RegDate = currentDate,
                ExternalRegNumber = null,
                CorrRegNumber = null,
                CorrRegDate = null,
                IsActive = true,
                DocRelations = docRelations
            };

            returnValue.CanRead = true;
            returnValue.CanEdit = true;
            returnValue.CanRegister = true;
            returnValue.CanManagement = true;
            returnValue.CanESign = true;
            returnValue.CanFinish = true;
            returnValue.CanReverse = true;

            UserContext userContext = this.userContextProvider.GetCurrentUserContext();
            User user = this.unitOfWork.Repo<User>().Query()
                .Include(e => e.Unit)
                .SingleOrDefault(e => e.UserId == userContext.UserId);
            List<DocUnitRole> unitRoles = this.unitOfWork.Repo<DocUnitRole>().Query().ToList();
            List<DocTypeUnitRole> docTypeUnitRoles = this.unitOfWork.Repo<DocTypeUnitRole>().Query()
                .Include(e => e.DocUnitRole)
                .Include(e => e.Unit)
                .Where(e => e.DocTypeId == type.DocTypeId && e.DocDirectionId == direction.DocDirectionId).ToList();
            List<DocTypeClassification> docTypeClassifications = this.unitOfWork.Repo<DocTypeClassification>().Query()
                .Include(e => e.Classification)
                .Where(e => e.DocTypeId == type.DocTypeId && e.DocDirectionId == direction.DocDirectionId).ToList();

            returnValue.SetupAuto(user.Unit, unitRoles, docTypeUnitRoles, docTypeClassifications);
            returnValue.SetupFlags();

            return returnValue;
        }

        /// <summary>
        /// Генерира нова документ от тип задача
        /// </summary>
        /// <param name="id">Идентификатор на родителския документ</param>
        /// <returns></returns>
        protected DocDO GetNewTaskInternal(int id)
        {
            DateTime currentDate = DateTime.Now;

            DocRelation currentDocRelation = this.unitOfWork.Repo<DocRelation>().Query()
                .FirstOrDefault(e => e.DocId == id); //todo not working if document takes part in multiple cases

            DocRelationDO drDO = new DocRelationDO();
            drDO.RootDocId = currentDocRelation.RootDocId;
            drDO.ParentDocId = id;

            List<DocRelationDO> docRelations = new List<DocRelationDO>();
            docRelations.Add(drDO);

            DocEntryType entry = this.unitOfWork.Repo<DocEntryType>().GetByAlias("task");
            DocDirection direction = this.unitOfWork.Repo<DocDirection>().GetByAlias("internal");
            DocType type = this.unitOfWork.Repo<DocType>().GetByAlias("task");
            DocStatus status = this.unitOfWork.Repo<DocStatus>().GetByAlias("draft");
            AssignmentType assignmentType = this.unitOfWork.Repo<AssignmentType>().GetByAlias("withdeadline");
            DocCasePartType casePart = this.unitOfWork.Repo<DocCasePartType>().GetByAlias("internal");
            DocFormatType formatType = this.unitOfWork.Repo<DocFormatType>().GetByAlias("Electronic");

            var returnValue = new DocDO()
            {
                DocId = null,
                DocDirectionId = direction.DocDirectionId,
                DocDirectionAlias = direction.Alias,
                DocDirectionName = direction.Name,
                DocEntryTypeId = entry.DocEntryTypeId,
                DocEntryTypeAlias = entry.Alias,
                DocEntryTypeName = entry.Name,
                DocSubject = null,
                DocBody = null,
                DocStatusId = status.DocStatusId,
                DocStatusAlias = status.Alias,
                DocStatusName = status.Name,
                DocTypeId = type.DocTypeId,
                DocTypeAlias = type.Alias,
                DocTypeName = type.Name,
                DocFormatTypeId = formatType.DocFormatTypeId,
                DocCasePartTypeId = casePart.DocCasePartTypeId,
                DocCasePartTypeAlias = casePart.Alias,
                DocCasePartTypeName = casePart.Name,
                AssignmentTypeId = assignmentType.AssignmentTypeId,
                IsCase = false,
                IsRegistered = false,
                IsSigned = false,
                DocRegisterId = null,
                RegNumber = null,
                RegDate = currentDate,
                ExternalRegNumber = null,
                CorrRegNumber = null,
                CorrRegDate = null,
                IsActive = true,
                DocRelations = docRelations
            };

            returnValue.CanRead = true;
            returnValue.CanEdit = true;
            returnValue.CanRegister = true;
            returnValue.CanManagement = true;
            returnValue.CanESign = true;
            returnValue.CanFinish = true;
            returnValue.CanReverse = true;

            UserContext userContext = this.userContextProvider.GetCurrentUserContext();
            User user = this.unitOfWork.Repo<User>().Query()
                .Include(e => e.Unit)
                .SingleOrDefault(e => e.UserId == userContext.UserId);
            List<DocUnitRole> unitRoles = this.unitOfWork.Repo<DocUnitRole>().Query().ToList();
            List<DocTypeUnitRole> docTypeUnitRoles = this.unitOfWork.Repo<DocTypeUnitRole>().Query()
                .Include(e => e.DocUnitRole)
                .Include(e => e.Unit)
                .Where(e => e.DocTypeId == type.DocTypeId && e.DocDirectionId == direction.DocDirectionId).ToList();
            List<DocTypeClassification> docTypeClassifications = this.unitOfWork.Repo<DocTypeClassification>().Query()
                .Include(e => e.Classification)
                .Where(e => e.DocTypeId == type.DocTypeId && e.DocDirectionId == direction.DocDirectionId).ToList();

            returnValue.SetupAuto(user.Unit, unitRoles, docTypeUnitRoles, docTypeClassifications);
            returnValue.SetupFlags();

            return returnValue;
        }

        /// <summary>
        /// Генерира нов документ
        /// </summary>
        /// <param name="prior">Данни на документа</param>
        /// <returns></returns>
        protected DocDO PostNewDocInternal(PriorDocDO prior)
        {
            DocRelationDO drDO = new DocRelationDO();
            drDO.RootDocId = prior.DocRootId;
            drDO.ParentDocId = prior.DocParentId;

            if (!drDO.ParentDocId.HasValue)
            {
                drDO.RootDocId = prior.ParentDocRelation.RootDocId;
                drDO.ParentDocId = prior.ParentDocRelation.ParentDocId;
            }

            Doc parentDoc = null;
            if (drDO.ParentDocId.HasValue)
            {
                parentDoc = this.unitOfWork.Repo<Doc>().Find(drDO.ParentDocId.Value,
                    e => e.DocType);

                drDO.ParentRegUri = parentDoc.RegUri;
                drDO.ParentDocTypeName = parentDoc.DocType.Name;
                drDO.ParentSubject = parentDoc.DocSubject;
            }

            List<DocRelationDO> docRelations = new List<DocRelationDO>();
            if (drDO.RootDocId.HasValue)
            {
                var drDOs = this.unitOfWork.Repo<DocRelation>().Query()
                .Include(e => e.Doc.DocCasePartType)
                .Include(e => e.RootDoc)
                .Include(e => e.ParentDoc.DocType)
                .Where(e => e.RootDocId == drDO.RootDocId.Value)
                .ToList()
                .Select(e => new DocRelationDO(e))
                .ToList();

                if (drDOs.Any())
                {
                    docRelations.AddRange(drDOs);
                }
            }
            docRelations.Add(drDO);

            DocEntryType entry = this.unitOfWork.Repo<DocEntryType>().GetByAlias("document");
            DocStatus status = this.unitOfWork.Repo<DocStatus>().GetByAlias("draft");
            DocDirection direction = this.unitOfWork.Repo<DocDirection>().Query()
                .SingleOrDefault(e => e.DocDirectionId == prior.DocDirectionId.Value);
            DocType type = this.unitOfWork.Repo<DocType>().Query()
                .SingleOrDefault(e => e.DocTypeId == prior.DocTypeId.Value);
            DocCasePartType casePart = this.unitOfWork.Repo<DocCasePartType>().Query()
                .SingleOrDefault(e => e.DocCasePartTypeId == prior.DocCasePartTypeId);

            DocType docType = this.unitOfWork.Repo<DocType>().Find(prior.DocTypeId.Value);

            DocFileDO file = null;

            var returnValue = new DocDO()
            {
                DocId = null,
                DocDirectionId = direction.DocDirectionId,
                DocDirectionAlias = direction.Alias,
                DocDirectionName = direction.Name,
                DocEntryTypeId = entry.DocEntryTypeId,
                DocEntryTypeAlias = entry.Alias,
                DocEntryTypeName = entry.Name,
                DocSubject = prior.DocSubject,
                DocBody = null,
                DocStatusId = status.DocStatusId,
                DocStatusAlias = status.Alias,
                DocStatusName = status.Name,
                DocTypeId = type.DocTypeId,
                DocTypeAlias = type.Alias,
                DocTypeName = type.Name,
                DocFormatTypeId = prior.DocFormatTypeId,
                DocCasePartTypeId = casePart.DocCasePartTypeId,
                DocCasePartTypeAlias = casePart.Alias,
                DocCasePartTypeName = casePart.Name,
                DocTypeIsElectronicService = type.IsElectronicService,
                IsCase = prior.IsCase,
                IsRegistered = prior.IsRegistered,
                IsSigned = prior.IsSigned,
                DocRegisterId = null,
                RegNumber = null,
                RegDate = prior.RegDate,
                ExternalRegNumber = null,
                CorrRegNumber = null,
                CorrRegDate = null,
                IsActive = true,
                DocRelations = docRelations
            };

            returnValue.CanRead = true;
            returnValue.CanEdit = true;
            returnValue.CanRegister = true;
            returnValue.CanManagement = true;
            returnValue.CanESign = true;
            returnValue.CanFinish = true;
            returnValue.CanReverse = true;

            Correspondent correspondent = null;

            var priorDocCorrespondents = prior.DocCorrespondents.Where(e => !e.IsDeleted).ToList();
            if (priorDocCorrespondents.Count > 0)
            {
                returnValue.DocCorrespondents.AddRange(priorDocCorrespondents);
                correspondent = this.unitOfWork.Repo<Correspondent>().Find(priorDocCorrespondents[0].CorrespondentId, c => c.CorrespondentType);
            }

            if (docType.IsElectronicService)
            {
                DocFormatType formaType = this.unitOfWork.Repo<DocFormatType>().Find(returnValue.DocFormatTypeId.Value);

                if (!string.IsNullOrEmpty(docType.ElectronicServiceFileTypeUri))
                {
                    if (formaType.Alias.ToLower() != "Paper".ToLower())
                    {
                        DocFileType docFileType = this.unitOfWork.Repo<DocFileType>().GetByDocTypeUri(docType.ElectronicServiceFileTypeUri);
                        DocFileKind docFileKind = this.unitOfWork.Repo<DocFileKind>().Query()
                            .SingleOrDefault(e => e.Alias.ToLower() == "PublicAttachedFile".ToLower());

                        Tuple<string, byte[]> nameAndContent = GetDocFileNameAndBytesContent(docFileType, correspondent, prior.DocParentId, prior.DocRootId);
                        Guid fileKey = this.abbcdnStorage.UploadFile(nameAndContent.Item2, nameAndContent.Item1);

                        file = new DocFileDO
                        {
                            DocFileTypeId = docFileType.DocFileTypeId,
                            DocFileTypeIsEditable = docFileType.IsEditable,
                            DocFileTypeName = docFileType.Name,
                            DocFileKindId = docFileKind.DocFileKindId,
                            DocFileKindName = docFileKind.Name,
                            DocFileKindAlias = docFileKind.Alias,
                            Name = nameAndContent.Item1,
                            DocFileName = nameAndContent.Item1,
                            DocContentStorage = String.Empty,
                            DocFileContentId = fileKey,
                            IsPrimary = true,
                            IsSigned = false,
                            IsActive = true,
                            IsNew = true
                        };

                        returnValue.PublicDocFiles.Add(file);
                    }
                }

                if (formaType.Alias.ToLower() == "Paper".ToLower())
                {
                    DocSourceType manual = this.unitOfWork.Repo<DocSourceType>().GetByAlias("Manual");
                    returnValue.DocSourceTypeId = manual.DocSourceTypeId;
                }

                if (prior.DocElectronicServiceStage != null)
                {
                    returnValue.DocElectronicServiceStages.Add(prior.DocElectronicServiceStage);
                }
            }

            UserContext userContext = this.userContextProvider.GetCurrentUserContext();
            User user = this.unitOfWork.Repo<User>().Query()
                .Include(e => e.Unit)
                .SingleOrDefault(e => e.UserId == userContext.UserId);
            List<DocUnitRole> unitRoles = this.unitOfWork.Repo<DocUnitRole>().Query().ToList();
            List<DocTypeUnitRole> docTypeUnitRoles = this.unitOfWork.Repo<DocTypeUnitRole>().Query()
                .Include(e => e.DocUnitRole)
                .Include(e => e.Unit)
                .Where(e => e.DocTypeId == type.DocTypeId && e.DocDirectionId == direction.DocDirectionId).ToList();
            List<DocTypeClassification> docTypeClassifications = this.unitOfWork.Repo<DocTypeClassification>().Query()
                .Include(e => e.Classification)
                .Where(e => e.DocTypeId == type.DocTypeId && e.DocDirectionId == direction.DocDirectionId).ToList();

            returnValue.SetupAuto(user.Unit, unitRoles, docTypeUnitRoles, docTypeClassifications);
            returnValue.SetupFlags();

            return returnValue;
        }

        /// <summary>
        /// Генерира нова документ от тип забележка
        /// </summary>
        /// <param name="id">Идентификатор на родителския документ</param>
        /// <returns></returns>
        protected DocDO GetNewRemarkInternal(int id)
        {
            DateTime currentDate = DateTime.Now;

            DocRelation currentDocRelation = this.unitOfWork.Repo<DocRelation>().Query()
                .FirstOrDefault(e => e.DocId == id); //todo not working if document takes part in multiple cases

            DocRelationDO drDO = new DocRelationDO();
            drDO.RootDocId = currentDocRelation.RootDocId;
            drDO.ParentDocId = id;

            List<DocRelationDO> docRelations = new List<DocRelationDO>();
            docRelations.Add(drDO);

            DocEntryType entry = this.unitOfWork.Repo<DocEntryType>().GetByAlias("remark");
            DocDirection direction = this.unitOfWork.Repo<DocDirection>().GetByAlias("internal");
            DocType type = this.unitOfWork.Repo<DocType>().GetByAlias("remark");
            DocStatus status = this.unitOfWork.Repo<DocStatus>().GetByAlias("draft");
            AssignmentType assignmentType = this.unitOfWork.Repo<AssignmentType>().GetByAlias("withdeadline");
            DocCasePartType casePart = this.unitOfWork.Repo<DocCasePartType>().GetByAlias("internal");
            DocFormatType formatType = this.unitOfWork.Repo<DocFormatType>().GetByAlias("Electronic");

            var returnValue = new DocDO()
            {
                DocId = null,
                DocDirectionId = direction.DocDirectionId,
                DocDirectionAlias = direction.Alias,
                DocDirectionName = direction.Name,
                DocEntryTypeId = entry.DocEntryTypeId,
                DocEntryTypeAlias = entry.Alias,
                DocEntryTypeName = entry.Name,
                DocSubject = null,
                DocBody = null,
                DocStatusId = status.DocStatusId,
                DocStatusAlias = status.Alias,
                DocStatusName = status.Name,
                DocTypeId = type.DocTypeId,
                DocTypeAlias = type.Alias,
                DocTypeName = type.Name,
                DocFormatTypeId = formatType.DocFormatTypeId,
                DocCasePartTypeId = casePart.DocCasePartTypeId,
                DocCasePartTypeAlias = casePart.Alias,
                DocCasePartTypeName = casePart.Name,
                AssignmentDate = DateTime.Now,
                AssignmentTypeId = assignmentType.AssignmentTypeId,
                IsCase = false,
                IsRegistered = false,
                IsSigned = false,
                DocRegisterId = null,
                RegNumber = null,
                RegDate = currentDate,
                ExternalRegNumber = null,
                CorrRegNumber = null,
                CorrRegDate = null,
                IsActive = true,
                DocRelations = docRelations
            };

            returnValue.CanRead = true;
            returnValue.CanEdit = true;
            returnValue.CanRegister = true;
            returnValue.CanManagement = true;
            returnValue.CanESign = true;
            returnValue.CanFinish = true;
            returnValue.CanReverse = true;

            UserContext userContext = this.userContextProvider.GetCurrentUserContext();
            User user = this.unitOfWork.Repo<User>().Query()
                .Include(e => e.Unit)
                .SingleOrDefault(e => e.UserId == userContext.UserId);
            List<DocUnitRole> unitRoles = this.unitOfWork.Repo<DocUnitRole>().Query().ToList();
            List<DocTypeUnitRole> docTypeUnitRoles = this.unitOfWork.Repo<DocTypeUnitRole>().Query()
                .Include(e => e.DocUnitRole)
                .Include(e => e.Unit)
                .Where(e => e.DocTypeId == type.DocTypeId && e.DocDirectionId == direction.DocDirectionId).ToList();
            List<DocTypeClassification> docTypeClassifications = this.unitOfWork.Repo<DocTypeClassification>().Query()
                .Include(e => e.Classification)
                .Where(e => e.DocTypeId == type.DocTypeId && e.DocDirectionId == direction.DocDirectionId).ToList();

            returnValue.SetupAuto(user.Unit, unitRoles, docTypeUnitRoles, docTypeClassifications);
            returnValue.SetupFlags();

            return returnValue;
        }

        /// <summary>
        /// Записва нов документ
        /// </summary>
        /// <param name="doc">Данни на документа</param>
        /// <param name="userContext">Данни за текущия потребител</param>
        /// <returns></returns>
        protected Doc PostDocInternal(DocDO doc, UserContext userContext)
        {
            var newDoc = new Doc();

            newDoc.DocDirectionId = doc.DocDirectionId;
            newDoc.DocEntryTypeId = doc.DocEntryTypeId;
            newDoc.DocSourceTypeId = doc.DocSourceTypeId;
            newDoc.DocDestinationTypeId = doc.DocDestinationTypeId;
            newDoc.DocSubject = doc.DocSubject;
            newDoc.DocBody = doc.DocBody;
            newDoc.DocStatusId = doc.DocStatusId.Value;
            newDoc.DocTypeId = doc.DocTypeId;
            newDoc.DocCasePartTypeId = doc.DocCasePartTypeId;
            newDoc.DocRegisterId = doc.DocRegisterId;
            newDoc.RegUri = doc.RegUri;
            newDoc.RegIndex = doc.RegIndex;
            newDoc.RegNumber = doc.RegNumber;
            newDoc.RegDate = doc.RegDate;
            newDoc.ExternalRegNumber = doc.ExternalRegNumber;
            newDoc.CorrRegNumber = doc.CorrRegNumber;
            newDoc.CorrRegDate = doc.CorrRegDate;
            newDoc.AssignmentTypeId = doc.AssignmentTypeId;
            newDoc.AssignmentDate = doc.AssignmentDate;
            newDoc.AssignmentDeadline = doc.AssignmentDeadline;
            newDoc.IsCase = doc.IsCase;
            newDoc.IsRegistered = doc.IsRegistered;
            newDoc.IsSigned = doc.IsSigned;
            newDoc.IsActive = doc.IsActive;
            newDoc.ModifyDate = DateTime.Now;
            newDoc.ModifyUserId = userContext.UserId;

            DocEntryType docEntryType = this.unitOfWork.Repo<DocEntryType>().Query()
                .SingleOrDefault(e => e.Alias.ToLower() == "Document".ToLower());

            if (newDoc.IsCase && newDoc.DocEntryTypeId == docEntryType.DocEntryTypeId)
            {
                newDoc.AccessCode = GenerateAccessCode();
            }
            else
            {
                newDoc.AccessCode = null;
            }

            //doccasepartmovement
            DocCasePartMovement dcpm = new DocCasePartMovement();
            dcpm.Doc = newDoc;
            dcpm.DocCasePartTypeId = newDoc.DocCasePartTypeId;
            dcpm.UserId = userContext.UserId;
            dcpm.MovementDate = DateTime.Now;
            this.unitOfWork.Repo<DocCasePartMovement>().Add(dcpm);

            //doc relations
            DocRelationDO drDO = doc.DocRelations.Where(e => !e.DocId.HasValue).SingleOrDefault();
            if (drDO != null)
            {
                DocRelation newDocRelation = new DocRelation();
                newDocRelation.Doc = newDoc;
                newDocRelation.RootDoc = drDO.RootDocId.HasValue ? this.unitOfWork.Repo<Doc>().Find(drDO.RootDocId.Value) : newDoc;
                newDocRelation.ParentDoc = drDO.ParentDocId.HasValue ? this.unitOfWork.Repo<Doc>().Find(drDO.ParentDocId.Value) : null;
                this.unitOfWork.Repo<DocRelation>().Add(newDocRelation);
            }

            //doc electronic service stage
            var docESStage = doc.DocElectronicServiceStages.FirstOrDefault(e => e.ElectronicServiceStageId.HasValue); //maybe better check is if there is a parent docrelation
            if (docESStage != null)
            {
                DocElectronicServiceStage dess = new DocElectronicServiceStage();
                dess.Doc = newDoc;
                dess.ElectronicServiceStageId = docESStage.ElectronicServiceStageId.Value;
                dess.EndingDate = docESStage.EndingDate;
                dess.ExpectedEndingDate = docESStage.ExpectedEndingDate;
                dess.IsCurrentStage = true;
                dess.StartingDate = docESStage.StartingDate.Value;

                this.unitOfWork.Repo<DocElectronicServiceStage>().Add(dess);
            }

            //doccorrespondentcontacts
            foreach (var docCCon in doc.DocCorrespondentContacts.Where(e => !e.IsDeleted))
            {
                DocCorrespondentContact dcc = new DocCorrespondentContact();
                dcc.Doc = newDoc;
                dcc.CorrespondentContactId = docCCon.CorrespondentContactId;

                this.unitOfWork.Repo<DocCorrespondentContact>().Add(dcc);
            }

            //doccorrespondents
            foreach (var docCor in doc.DocCorrespondents.Where(e => !e.IsDeleted))
            {
                DocCorrespondent dc = new DocCorrespondent();
                dc.Doc = newDoc;
                dc.CorrespondentId = docCor.CorrespondentId;

                this.unitOfWork.Repo<DocCorrespondent>().Add(dc);
            }

            //docclassifications
            foreach (var docClassification in doc.DocClassifications.Where(e => !e.IsDeleted))
            {
                DocClassification dc = new DocClassification();
                dc.Doc = newDoc;
                dc.ClassificationId = docClassification.ClassificationId.Value;
                dc.ClassificationByUserId = docClassification.ClassificationByUserId.Value;
                dc.ClassificationDate = docClassification.ClassificationDate;
                dc.IsActive = docClassification.IsActive;

                this.unitOfWork.Repo<DocClassification>().Add(dc);
            }

            //docunits
            foreach (var docUnit in doc.DocUnits.Where(e => !e.IsDeleted))
            {
                DocUnit du = new DocUnit();
                du.Doc = newDoc;
                du.UnitId = docUnit.UnitId;
                du.DocUnitRoleId = docUnit.DocUnitRoleId;

                this.unitOfWork.Repo<DocUnit>().Add(du);
            }

            List<DocFileDO> docFiles = new List<DocFileDO>();
            docFiles.AddRange(doc.PublicDocFiles);
            docFiles.AddRange(doc.PrivateDocFiles);

            //files
            foreach (var file in docFiles.Where(e => e.IsNew))
            {
                DocFile ndf = new DocFile();
                ndf.DocFileContentId = file.DocFileContentId;
                ndf.DocContentStorage = file.DocContentStorage;
                ndf.Name = file.Name;
                ndf.DocFileName = file.DocFileName;
                ndf.DocFileTypeId = file.DocFileTypeId;
                ndf.DocFileKindId = file.DocFileKindId;
                ndf.Doc = newDoc;
                ndf.IsActive = file.IsActive;
                ndf.IsSigned = file.IsSigned;
                ndf.IsPrimary = file.IsPrimary;

                this.unitOfWork.Repo<DocFile>().Add(ndf);
            }

            this.unitOfWork.Repo<Doc>().Add(newDoc);
            this.unitOfWork.Save();
            this.unitOfWork.Repo<Doc>().ExecSpSetDocUsers(newDoc.DocId);

            return newDoc;
        }

        protected R_0009_000016.ElectronicServiceApplicant CreateElectronicServiceApplicant(Correspondent correspondent)
        {
            R_0009_000016.ElectronicServiceApplicant applicant = null;

            if (correspondent != null)
            {
                var recipient = new R_0009_000015.ElectronicServiceRecipient();

                if (correspondent.CorrespondentType.Alias == "BulgarianCitizen")
                {
                    var person = new R_0009_000008.PersonBasicData();

                    person.Identifier = new R_0009_000006.PersonIdentifier();
                    person.Identifier.EGN = correspondent.BgCitizenUIN;
                    person.Names = new R_0009_000005.PersonNames();
                    person.Names.First = correspondent.BgCitizenFirstName;
                    person.Names.Last = correspondent.BgCitizenLastName;

                    recipient.Person = person;
                }
                else if (correspondent.CorrespondentType.Alias == "Foreigner")
                {
                    var foreignPerson = new R_0009_000011.ForeignCitizenBasicData();

                    foreignPerson.Names = new R_0009_000007.ForeignCitizenNames();
                    foreignPerson.Names.FirstLatin = correspondent.ForeignerFirstName;
                    foreignPerson.Names.LastLatin = correspondent.ForeignerLastName;
                    foreignPerson.PlaceOfBirth = new R_0009_000009.ForeignCitizenPlaceOfBirth();
                    foreignPerson.PlaceOfBirth.SettlementName = correspondent.ForeignerSettlement;
                    foreignPerson.BirthDate = correspondent.ForeignerBirthDate;

                    if (correspondent.ForeignerCountryId.HasValue)
                    {
                        var country = this.unitOfWork.Repo<Country>().Find(correspondent.ForeignerCountryId.Value);

                        foreignPerson.PlaceOfBirth.CountryCode = country.Code;
                        foreignPerson.PlaceOfBirth.CountryName = country.Name;
                    }

                    recipient.ForeignPerson = foreignPerson;
                }
                else if (correspondent.CorrespondentType.Alias == "LegalEntity")
                {
                    var entity = new R_0009_000013.EntityBasicData();

                    entity.Name = correspondent.LegalEntityName;
                    entity.Identifier = correspondent.LegalEntityBulstat;

                    recipient.Entity = entity;
                }
                else if (correspondent.CorrespondentType.Alias == "ForeignLegalEntity")
                {
                    var foreignEntity = new R_0009_000014.ForeignEntityBasicData();

                    foreignEntity.ForeignEntityName = correspondent.FLegalEntityName;
                    foreignEntity.ForeignEntityRegister = correspondent.FLegalEntityRegisterName;
                    foreignEntity.ForeignEntityIdentifier = correspondent.FLegalEntityRegisterNumber;
                    foreignEntity.ForeignEntityOtherData = correspondent.FLegalEntityOtherData;

                    if (correspondent.ForeignerCountryId.HasValue)
                    {
                        var country = this.unitOfWork.Repo<Country>().Find(correspondent.ForeignerCountryId.Value);

                        foreignEntity.CountryISO3166TwoLetterCode = country.Code;
                        foreignEntity.CountryNameCyrillic = country.Name;
                    }

                    recipient.ForeignEntity = foreignEntity;
                }

                applicant = new R_0009_000016.ElectronicServiceApplicant();
                applicant.EmailAddress = correspondent.Email;
                applicant.RecipientGroupCollection = new R_0009_000016.RecipientGroupCollection();
                applicant.RecipientGroupCollection.Add(new R_0009_000016.RecipientGroup());
                applicant.RecipientGroupCollection[0].RecipientCollection = new R_0009_000016.ElectronicServiceRecipientCollection();
                applicant.RecipientGroupCollection[0].RecipientCollection.Add(recipient);
            }

            return applicant;
        }

        protected R_0009_000137.ElectronicServiceApplicantContactData CreateElectronicServiceApplicantContactData(Correspondent correspondent)
        {
            R_0009_000137.ElectronicServiceApplicantContactData applicantContactData = null;

            if (correspondent != null)
            {
                District district = null;
                Municipality municipality = null;
                Settlement settlement = null;

                if (correspondent.ContactDistrictId.HasValue)
                {
                    district = this.unitOfWork.Repo<District>().Find(correspondent.ContactDistrictId.Value);
                }

                if (correspondent.ContactMunicipalityId.HasValue)
                {
                    municipality = this.unitOfWork.Repo<Municipality>().Find(correspondent.ContactMunicipalityId.Value);
                }

                if (correspondent.ContactSettlementId.HasValue)
                {
                    settlement = this.unitOfWork.Repo<Settlement>().Find(correspondent.ContactSettlementId.Value);
                }

                applicantContactData = new R_0009_000137.ElectronicServiceApplicantContactData();
                applicantContactData.DistrictCode = district != null ? district.Code : null;
                applicantContactData.DistrictName = district != null ? district.Name : null;
                applicantContactData.MunicipalityCode = municipality != null ? municipality.Code : null;
                applicantContactData.MunicipalityName = municipality != null ? municipality.Name : null;
                applicantContactData.SettlementCode = settlement != null ? settlement.Code : null;
                applicantContactData.SettlementName = settlement != null ? settlement.Name : null;
                applicantContactData.AddressDescription = correspondent.ContactAddress;
                applicantContactData.PostCode = correspondent.ContactPostCode;
                applicantContactData.PostOfficeBox = correspondent.ContactPostOfficeBox;

                applicantContactData.PhoneNumbers = new R_0009_000137.PhoneNumbers();
                applicantContactData.PhoneNumbers.PhoneNumberCollection = new R_0009_000137.PhoneNumberCollection();
                if (!String.IsNullOrWhiteSpace(correspondent.ContactPhone))
                {
                    applicantContactData.PhoneNumbers.PhoneNumberCollection.Add(correspondent.ContactPhone);
                }

                applicantContactData.FaxNumbers = new R_0009_000137.FaxNumbers();
                applicantContactData.FaxNumbers.ElectronicServiceApplicantFaxNumberCollection = new R_0009_000137.ElectronicServiceApplicantFaxNumberCollection();
                if (!String.IsNullOrWhiteSpace(correspondent.ContactFax))
                {
                    applicantContactData.FaxNumbers.ElectronicServiceApplicantFaxNumberCollection.Add(correspondent.ContactFax);
                }
            }

            return applicantContactData;
        }

        /// <summary>
        /// Връща идентификатор на водещия документ по преписка
        /// </summary>
        /// <param name="id">Идентификатор на текущ документ</param>
        /// <returns></returns>
        protected int GetCaseId(int id)
        {
            var doc = this.unitOfWork.Repo<Doc>().Find(id, e => e.DocRelations);

            //presumption: doc can only be in one case and doc is already in the db
            return doc.DocRelations.FirstOrDefault().RootDocId.Value;
        }

        protected Tuple<string, byte[]> GetDocFileNameAndBytesContent(DocFileType docFileType, Correspondent correspondent, int? parentDocId, int? rootDocId)
        {
            byte[] bytesContent = null;
            string fileName = null;

            R_0009_000152.ElectronicAdministrativeServiceHeader header = new R_0009_000152.ElectronicAdministrativeServiceHeader();
            if (correspondent != null)
            {
                header.ElectronicServiceApplicant = CreateElectronicServiceApplicant(correspondent);
                header.ElectronicServiceApplicantContactData = CreateElectronicServiceApplicantContactData(correspondent);
            }

            //Bim applications
            if (docFileType.Alias == "R-1044")
            {
                fileName = "Документ УРИ0010-001044";
                var application = new R_1044.MeasuringEquipmentApprovalApplication();
                application.ElectronicAdministrativeServiceHeader = header;
                bytesContent = Converter.GetBytes(documentSerializer.XmlSerializeObjectToString(application));
            }
            else if (docFileType.Alias == "R-1090")
            {
                fileName = "Документ УРИ0010-001090";
                var application = new R_1090.MEVerificationApplication();
                application.ElectronicAdministrativeServiceHeader = header;
                bytesContent = Converter.GetBytes(documentSerializer.XmlSerializeObjectToString(application));
            }
            else if (docFileType.Alias == "R-1132")
            {
                fileName = "Документ УРИ0010-001132";
                var application = new R_1132.InstrumentalMetrologyExpertiseApplication();
                application.ElectronicAdministrativeServiceHeader = header;
                bytesContent = Converter.GetBytes(documentSerializer.XmlSerializeObjectToString(application));
            }
            else if (docFileType.Alias == "R-1184")
            {
                fileName = "Документ УРИ0010-001184";
                var application = new R_1184.InformationRegisterApprovedTypesMEApplication();
                application.ElectronicAdministrativeServiceHeader = header;
                bytesContent = Converter.GetBytes(documentSerializer.XmlSerializeObjectToString(application));
            }
            else if (docFileType.Alias == "R-1182")
            {
                fileName = "Документ УРИ0010-001182";
                var application = new R_1182.PlayingFacilityTypeApprovalApplication();
                application.ElectronicAdministrativeServiceHeader = header;
                bytesContent = Converter.GetBytes(documentSerializer.XmlSerializeObjectToString(application));
            }
            else if (docFileType.Alias == "R-1144")
            {
                fileName = "Документ УРИ0010-001144";
                var application = new R_1144.TypeExaminationFiscalDeviceApplication();
                application.ElectronicAdministrativeServiceHeader = header;
                bytesContent = Converter.GetBytes(documentSerializer.XmlSerializeObjectToString(application));
            }
            else if (docFileType.Alias == "R-1208")
            {
                fileName = "Документ УРИ0010-001208";
                var application = new R_1208.ConformityAssessmentNonAutomaticWeighingApplication();
                application.ElectronicAdministrativeServiceHeader = header;
                bytesContent = Converter.GetBytes(documentSerializer.XmlSerializeObjectToString(application));
            }
            else if (docFileType.Alias == "R-1192")
            {
                fileName = "Документ УРИ0010-001192";
                var application = new R_1192.ElectromagneticCompatibilityTestingApplication();
                application.ElectronicAdministrativeServiceHeader = header;
                bytesContent = Converter.GetBytes(documentSerializer.XmlSerializeObjectToString(application));
            }
            else if (docFileType.Alias == "R-1168")
            {
                fileName = "Документ УРИ0010-001168";
                var application = new R_1168.CalibrationInstrumentalComparingMaterialsApplication();
                application.ElectronicAdministrativeServiceHeader = header;
                bytesContent = Converter.GetBytes(documentSerializer.XmlSerializeObjectToString(application));
            }

            //Dkh applications
            else if (docFileType.Alias == "R-2010")
            {
                fileName = "Документ УРИ0010-002010";
                var application = new R_2010.DecisionConfirmPerformanceInvestmentApplication();
                application.ElectronicAdministrativeServiceHeader = header;
                bytesContent = Converter.GetBytes(documentSerializer.XmlSerializeObjectToString(application));
            }
            else if (docFileType.Alias == "R-2018")
            {
                fileName = "Документ УРИ0010-002018";
                var application = new R_2018.DuplicateCertificateIssuanceLicenseGamblingApplication();
                application.ElectronicAdministrativeServiceHeader = header;
                bytesContent = Converter.GetBytes(documentSerializer.XmlSerializeObjectToString(application));
            }
            else if (docFileType.Alias == "R-2020")
            {
                fileName = "Документ УРИ0010-002020";
                var application = new R_2020.IssuanceCertificatesTranscriptsRecordsDocsApplication();
                application.ElectronicAdministrativeServiceHeader = header;
                bytesContent = Converter.GetBytes(documentSerializer.XmlSerializeObjectToString(application));
            }

            //Answers
            else if (docFileType.Alias == "ReceiptNotAcknowledgedMessage")
            {
                fileName = "Документ УРИ0010-000001";

                var receiptMessage = new R_0009_000017.ReceiptNotAcknowledgedMessage();
                receiptMessage.MessageURI = new R_0009_000001.DocumentURI();
                receiptMessage.TransportType = "0006-000001";   //Чрез уеб базирано приложение;
                receiptMessage.Discrepancies = new R_0009_000017.Discrepancies();
                receiptMessage.Discrepancies.DiscrepancyCollection = new R_0009_000017.DiscrepancyCollection();
                receiptMessage.Discrepancies.DiscrepancyCollection.Add(ElectronicDocumentDiscrepancyTypeNomenclature.IncorrectFormat.Uri);  //Подаваното заявление не е в нормативно установения формат
                //receiptMessage.MessageURI.RegisterIndex = receiptDoc.RegIndex;
                //receiptMessage.MessageURI.SequenceNumber = receiptDoc.RegNumber.Value.ToString("D6");
                //receiptMessage.MessageURI.ReceiptOrSigningDate = receiptDoc.RegDate.Value;
                //receiptMessage.MessageCreationTime = receiptDoc.RegDate;

                if (parentDocId.HasValue && parentDocId.Value != 0)
                {
                    //TODO: Consider property values
                    Doc parentDoc = this.unitOfWork.Repo<Doc>().Find(parentDocId.Value, d => d.DocType);

                    if (parentDoc.DocType.IsElectronicService)
                    {
                        DocFile primary = this.unitOfWork.Repo<DocFile>().Query()
                            .Include(e => e.DocFileType)
                            .FirstOrDefault(e => e.DocId == parentDocId.Value && e.IsPrimary);

                        if (primary != null && !string.IsNullOrEmpty(primary.DocFileType.DocTypeUri))
                        {
                            var fileInfo = this.abbcdnStorage.DownloadFile(primary.DocFileContentId);

                            if (fileInfo != null)
                            {
                                RioService rioService = new RioService(fileInfo.ContentBytes);

                                receiptMessage.ElectronicServiceProvider = rioService.RioApplication.ElectronicServiceProviderBasicData;
                                receiptMessage.Applicant = rioService.ServiceHeader.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicant;   //TODO: Dali ne trqbva da se vzema ot header.ElectronicServiceApplicant?
                                receiptMessage.ElectronicServiceProvider = rioService.ServiceHeader.ElectronicServiceProviderBasicData;
                                receiptMessage.DocumentTypeURI = rioService.ServiceHeader.DocumentTypeURI;
                                receiptMessage.DocumentTypeName = rioService.ServiceHeader.DocumentTypeName;
                            }
                        }
                    }
                }

                bytesContent = Converter.GetBytes(documentSerializer.XmlSerializeObjectToString(receiptMessage));
            }
            else if (docFileType.Alias == "ReceiptAcknowledgedMessage")
            {
                fileName = "Документ УРИ0010-000002";

                UserContext userContext = this.userContextProvider.GetCurrentUserContext();

                var receiptMessage = new R_0009_000019.ReceiptAcknowledgedMessage();
                receiptMessage.DocumentTypeURI = new R_0009_000003.DocumentTypeURI();
                receiptMessage.TransportType = "0006-000001";   //Чрез уеб базирано приложение;
                receiptMessage.RegisteredBy = new R_0009_000019.RegisteredBy();
                receiptMessage.RegisteredBy.Officer = new R_0009_000019.Officer();
                receiptMessage.RegisteredBy.Officer.AISUserIdentifier = userContext.FullName;
                receiptMessage.RegisteredBy.AISURI = Statics.AisURI;

                if (parentDocId.HasValue && parentDocId.Value != 0)
                {
                    //TODO: Consider property values
                    Doc parentDoc = this.unitOfWork.Repo<Doc>().Find(parentDocId.Value, d => d.DocType);

                    if (parentDoc.DocType.IsElectronicService)
                    {
                        Doc rootDoc = this.unitOfWork.Repo<Doc>().Find(rootDocId.Value);

                        DocFile primary = this.unitOfWork.Repo<DocFile>().Query()
                            .Include(e => e.DocFileType)
                            .FirstOrDefault(e => e.DocId == parentDocId.Value && e.IsPrimary); //todo parentDocId or root

                        if (primary != null && !string.IsNullOrEmpty(primary.DocFileType.DocTypeUri))
                        {
                            var fileInfo = this.abbcdnStorage.DownloadFile(primary.DocFileContentId);

                            if (fileInfo != null)
                            {
                                RioService rioService = new RioService(fileInfo.ContentBytes);

                                string htmlFormat = @"<p>Номер на преписка: <b>{0}</b><br/>Код за достъп: <b>{1}</b><br/></p>";

                                receiptMessage.ElectronicServiceProvider = rioService.RioApplication.ElectronicServiceProviderBasicData;
                                receiptMessage.DocumentURI = new R_0009_000001.DocumentURI();
                                receiptMessage.DocumentURI.RegisterIndex = parentDoc.RegIndex;
                                receiptMessage.DocumentURI.SequenceNumber = parentDoc.RegNumber.Value.ToString("D6");
                                receiptMessage.DocumentURI.ReceiptOrSigningDate = parentDoc.RegDate.Value;
                                receiptMessage.Applicant = rioService.ServiceHeader.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicant;      //TODO: Dali ne trqbva da se vzema ot header.ElectronicServiceApplicant?
                                receiptMessage.ElectronicServiceProvider = rioService.ServiceHeader.ElectronicServiceProviderBasicData;
                                receiptMessage.DocumentTypeURI = rioService.ServiceHeader.DocumentTypeURI;
                                receiptMessage.DocumentTypeName = rioService.ServiceHeader.DocumentTypeName;
                                receiptMessage.CaseAccessIdentifier = String.Format(htmlFormat, rootDoc.RegUri, rootDoc.AccessCode);
                            }
                        }
                    }
                }

                bytesContent = Converter.GetBytes(documentSerializer.XmlSerializeObjectToString(receiptMessage));
            }
            else if (docFileType.Alias == "RemovingIrregularitiesInstructions")
            {
                fileName = "Документ УРИ0010-003010";
                var application = new R_3010.RemovingIrregularitiesInstructions();

                UserContext userContext = this.userContextProvider.GetCurrentUserContext();
                if (!String.IsNullOrWhiteSpace(userContext.FullName))
                {
                    var names = Helper.SplitNames(userContext.FullName);

                    application.Official = new R_3010.RemovingIrregularitiesInstructionsOfficial();
                    application.Official.PersonNames = new R_0009_000005.PersonNames();
                    application.Official.PersonNames.First = names.Item1;
                    application.Official.PersonNames.Middle = names.Item2;
                    application.Official.PersonNames.Last = names.Item3;
                }

                application.ElectronicServiceApplicant = header.ElectronicServiceApplicant;
                application.RemovingIrregularitiesInstructionsHeader = "Указания за отстраняване на нередовности";

                string administrativeBodyName = null;
                if (Statics.AdministrativeBody == AdministrativeBody.BIM)
                    administrativeBodyName = "Предстедател на Българския институт по метрология";
                else if (Statics.AdministrativeBody == AdministrativeBody.DKH)
                    administrativeBodyName = "Предстедател на Държавната комисия по хазарта";

                application.AdministrativeBodyName = administrativeBodyName;

                //TODO: Sega se set-va fiktivna stoinost, za da ne pokazva validatio msg, no trbqva da sled kato se registrira dokumenta i se podpi6e, da se otvarq xml content-a i da se edit-nat IrregularityDocumentURI i IrregularityDocumentReceiptOrSigningDate
                application.IrregularityDocumentReceiptOrSigningDate = DateTime.Now;
                application.IrregularityDocumentURI = new R_0009_000001.DocumentURI();
                application.IrregularityDocumentURI.ReceiptOrSigningDate = DateTime.Now;
                application.IrregularityDocumentURI.RegisterIndex = "9999";
                application.IrregularityDocumentURI.SequenceNumber = "999999";

                int removingIrregularitiesDeadline = 10;

                if (parentDocId.HasValue && parentDocId.Value != 0)
                {
                    //TODO: Consider property values
                    Doc parentDoc = this.unitOfWork.Repo<Doc>().Find(parentDocId.Value,
                        d => d.DocType,
                        d => d.DocFiles.Select(df => df.DocFileType));

                    DocFile primaryParentDocFile = parentDoc.DocFiles.FirstOrDefault(f => f.IsPrimary);

                    if (primaryParentDocFile != null && !string.IsNullOrEmpty(primaryParentDocFile.DocFileType.DocTypeUri))
                    {
                        if (parentDoc.DocType.RemoveIrregularitiesDeadline.HasValue)
                        {
                            removingIrregularitiesDeadline = parentDoc.DocType.RemoveIrregularitiesDeadline.Value;
                        }

                        if (parentDoc.DocType.IsElectronicService)
                        {
                            Doc rootDoc = this.unitOfWork.Repo<Doc>().Find(rootDocId.Value);

                            DocFile primary = this.unitOfWork.Repo<DocFile>().Query().FirstOrDefault(e => e.DocId == parentDocId.Value && e.IsPrimary); //todo parentDocId or root
                            var fileInfo = this.abbcdnStorage.DownloadFile(primary.DocFileContentId);

                            if (fileInfo != null)
                            {
                                RioService rioService = new RioService(fileInfo.ContentBytes);

                                application.ElectronicServiceProviderBasicData = rioService.RioApplication.ElectronicServiceProviderBasicData;
                            }

                            application.ApplicationDocumentReceiptOrSigningDate = primaryParentDocFile != null ? primaryParentDocFile.SignDate : null;
                            if (parentDoc != null && parentDoc.DocId != 0)
                            {
                                application.ApplicationDocumentURI = new R_0009_000001.DocumentURI();
                                application.ApplicationDocumentURI.RegisterIndex = parentDoc.RegIndex;
                                application.ApplicationDocumentURI.SequenceNumber = parentDoc.RegNumber.Value.ToString("D6");
                                application.ApplicationDocumentURI.ReceiptOrSigningDate = parentDoc.RegDate;
                            }
                            if (rootDoc != null && rootDoc.DocId != 0)
                            {
                                application.AISCaseURI = new R_0009_000073.AISCaseURI();
                                application.AISCaseURI.DocumentURI = new R_0009_000001.DocumentURI();
                                application.AISCaseURI.DocumentURI.RegisterIndex = rootDoc.RegIndex;
                                application.AISCaseURI.DocumentURI.SequenceNumber = rootDoc.RegNumber.Value.ToString("D6");
                                application.AISCaseURI.DocumentURI.ReceiptOrSigningDate = rootDoc.RegDate;
                            }
                        }
                    }
                }

                application.DeadlineCorrectionIrregularities = XmlConvert.ToString(new TimeSpan(removingIrregularitiesDeadline, 0, 0, 0));

                bytesContent = Converter.GetBytes(documentSerializer.XmlSerializeObjectToString(application));
            }

            return new Tuple<string, byte[]>(fileName, bytesContent);
        }

        protected void AddDocumentUriToContent(int docId)
        {
            Doc doc = this.unitOfWork.Repo<Doc>().Find(docId, d => d.DocType);

            R_0009_000001.DocumentURI documentUri = new R_0009_000001.DocumentURI();
            documentUri.RegisterIndex = doc.RegIndex;
            documentUri.SequenceNumber = doc.RegNumber.Value.ToString("D6");
            documentUri.ReceiptOrSigningDate = doc.RegDate.Value;

            SetDocumentUriToContent(docId, documentUri);
        }

        protected void RemoveDocumentUriFromContent(int docId)
        {
            SetDocumentUriToContent(docId, null);
        }

        protected void SetDocumentUriToContent(int docId, R_0009_000001.DocumentURI documentUri)
        {
            Doc doc = this.unitOfWork.Repo<Doc>().Find(docId,
                d => d.DocDirection,
                d => d.DocType,
                d => d.DocFiles.Select(df => df.DocFileType));

            if (doc.DocDirection.Alias.ToLower() == "Outgoing".ToLower())
            {
                foreach (var docFile in doc.DocFiles)
                {
                    if (docFile.DocFileType.HasEmbeddedUri)
                    {
                        var fileInfo = this.abbcdnStorage.DownloadFile(docFile.DocFileContentId);

                        RioService rioService = new RioService(fileInfo.ContentBytes);
                        rioService.SetDocumentUriToContent(documentUri);

                        Guid fileKey = this.abbcdnStorage.UploadFile(rioService.BytesContent, fileInfo.FileName);
                        docFile.DocFileContentId = fileKey;
                    }
                }
            }
        }

        protected string GenerateAccessCode()
        {
            CodeGenerator codeGenerator = new CodeGenerator();
            codeGenerator.Minimum = 10;
            codeGenerator.Maximum = 10;
            codeGenerator.ConsecutiveCharacters = true;
            codeGenerator.RepeatCharacters = true;

            while (true)
            {
                string code = codeGenerator.Generate();

                if (!this.unitOfWork.Repo<Doc>().CheckForExistingAccessCode(code))
                {
                    return code;
                }
            }
        }

        #endregion
         */
    }
}
