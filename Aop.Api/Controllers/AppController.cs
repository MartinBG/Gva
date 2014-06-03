using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using Aop.Api.DataObjects;
using Aop.Api.Models;
using Aop.Api.Repositories.Aop;
using Aop.Api.WordTemplates;
using Common.Api.Repositories.UserRepository;
using Common.Api.UserContext;
using Common.Blob;
using Common.Data;
using Common.Utils;
using Common.WordTemplates;
using Docs.Api.DataObjects;
using Docs.Api.Models;
using Docs.Api.Repositories.DocRepository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Aop.Api.Controllers
{
    [RoutePrefix("api/aop/apps")]
    public class AppController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAppRepository appRepository;
        private IDocRepository docRepository;
        private UserContext userContext;
        private IUserRepository userRepository;
        private IDataGenerator dataGenerator;

        public AppController(IUnitOfWork unitOfWork,
            IAppRepository appRepository,
            IDocRepository docRepository,
            IUserRepository userRepository,
            IDataGenerator dataGenerator)
        {
            this.unitOfWork = unitOfWork;
            this.appRepository = appRepository;
            this.docRepository = docRepository;
            this.userRepository = userRepository;
            this.dataGenerator = dataGenerator;
        }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            try
            {
                this.userContext = this.Request.GetUserContext();
            }
            catch { }
        }

        [Route("new")]
        [HttpPost]
        public IHttpActionResult PostNewApp()
        {
            AopApp app = appRepository.CreateNewAopApp(this.userContext);

            this.unitOfWork.Save();

            return Ok(new
            {
                aopApplicationId = app.AopApplicationId
            });
        }


        [Route("")]
        [HttpGet]
        public IHttpActionResult GetApps(
            int limit = 10,
            int offset = 0,
            string displayName = null,
            string correspondentEmail = null
            )
        {
            //? hot fix: load fist 1000 corrs, so the paging with datatable will work
            limit = 1000;
            offset = 0;

            int totalCount = 0;

            var returnValue =
                this.appRepository.GetApps(
                //displayName,
                //correspondentEmail,
                    limit,
                    offset,
                    out totalCount)
                .Select(e => new AppListItemDO(e))
                .ToList();

            foreach (var item in returnValue)
            {
                if (item.STDocId.HasValue)
                {
                    var dr = unitOfWork.DbContext.Set<DocRelation>()
                         .Include(e => e.Doc.DocCasePartType)
                         .Include(e => e.Doc.DocCasePartMovements.Select(dc => dc.User))
                         .Include(e => e.Doc.DocDirection)
                         .Include(e => e.Doc.DocType)
                         .Include(e => e.Doc.DocStatus)
                         .FirstOrDefault(e => e.DocId == item.STDocId.Value);

                    item.STDocRelation = new DocRelationDO(dr);
                }

                if (item.NDDocId.HasValue)
                {
                    var dr = unitOfWork.DbContext.Set<DocRelation>()
                         .Include(e => e.Doc.DocCasePartType)
                         .Include(e => e.Doc.DocCasePartMovements.Select(dc => dc.User))
                         .Include(e => e.Doc.DocDirection)
                         .Include(e => e.Doc.DocType)
                         .Include(e => e.Doc.DocStatus)
                         .FirstOrDefault(e => e.DocId == item.NDDocId.Value);

                    item.NDDocRelation = new DocRelationDO(dr);
                }
            }

            return Ok(new
            {
                applications = returnValue,
                applicationCount = totalCount
            });
        }

        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult GetApp(int id)
        {
            AopApp app = this.appRepository.Find(id,
                e => e.AopEmployer);

            if (app == null)
            {
                return NotFound();
            }

            AppDO returnValue = new AppDO(app);

            #region DocRelations

            //ST
            if (app.STDocId.HasValue)
            {
                var dr = unitOfWork.DbContext.Set<DocRelation>()
                     .Include(e => e.Doc.DocCasePartType)
                     .Include(e => e.Doc.DocCasePartMovements.Select(dc => dc.User))
                     .Include(e => e.Doc.DocDirection)
                     .Include(e => e.Doc.DocType)
                     .Include(e => e.Doc.DocStatus)
                     .FirstOrDefault(e => e.DocId == app.STDocId.Value);

                returnValue.STDocRelation = new DocRelationDO(dr);
            }

            if (app.STChecklistId.HasValue)
            {
                var dr = unitOfWork.DbContext.Set<DocRelation>()
                     .Include(e => e.Doc.DocCasePartType)
                     .Include(e => e.Doc.DocCasePartMovements.Select(dc => dc.User))
                     .Include(e => e.Doc.DocDirection)
                     .Include(e => e.Doc.DocType)
                     .Include(e => e.Doc.DocStatus)
                     .FirstOrDefault(e => e.DocId == app.STChecklistId.Value);

                returnValue.STChecklistRelation = new DocRelationDO(dr);
            }

            if (app.STNoteId.HasValue)
            {
                var dr = unitOfWork.DbContext.Set<DocRelation>()
                     .Include(e => e.Doc.DocCasePartType)
                     .Include(e => e.Doc.DocCasePartMovements.Select(dc => dc.User))
                     .Include(e => e.Doc.DocDirection)
                     .Include(e => e.Doc.DocType)
                     .Include(e => e.Doc.DocStatus)
                     .FirstOrDefault(e => e.DocId == app.STNoteId.Value);

                returnValue.STNoteRelation = new DocRelationDO(dr);
            }

            //ND
            if (app.NDDocId.HasValue)
            {
                var dr = unitOfWork.DbContext.Set<DocRelation>()
                     .Include(e => e.Doc.DocCasePartType)
                     .Include(e => e.Doc.DocCasePartMovements.Select(dc => dc.User))
                     .Include(e => e.Doc.DocDirection)
                     .Include(e => e.Doc.DocType)
                     .Include(e => e.Doc.DocStatus)
                     .FirstOrDefault(e => e.DocId == app.NDDocId.Value);

                returnValue.NDDocRelation = new DocRelationDO(dr);
            }

            if (app.NDChecklistId.HasValue)
            {
                var dr = unitOfWork.DbContext.Set<DocRelation>()
                     .Include(e => e.Doc.DocCasePartType)
                     .Include(e => e.Doc.DocCasePartMovements.Select(dc => dc.User))
                     .Include(e => e.Doc.DocDirection)
                     .Include(e => e.Doc.DocType)
                     .Include(e => e.Doc.DocStatus)
                     .FirstOrDefault(e => e.DocId == app.NDChecklistId.Value);

                returnValue.NDChecklistRelation = new DocRelationDO(dr);
            }

            if (app.NDReportId.HasValue)
            {
                var dr = unitOfWork.DbContext.Set<DocRelation>()
                     .Include(e => e.Doc.DocCasePartType)
                     .Include(e => e.Doc.DocCasePartMovements.Select(dc => dc.User))
                     .Include(e => e.Doc.DocDirection)
                     .Include(e => e.Doc.DocType)
                     .Include(e => e.Doc.DocStatus)
                     .FirstOrDefault(e => e.DocId == app.NDReportId.Value);

                returnValue.NDReportRelation = new DocRelationDO(dr);
            }

            #endregion

            return Ok(returnValue);
        }

        [Route("{id}")]
        [HttpPost]
        public IHttpActionResult UpdateApp(int id, AppDO app)
        {
            var oldApp = this.appRepository.Find(id);

            oldApp.EnsureForProperVersion(app.Version);

            oldApp.AopEmployerId = app.AopEmployerId;
            oldApp.Email = app.Email;

            //I
            oldApp.STAopApplicationTypeId = app.STAopApplicationTypeId;
            oldApp.STObjectId = app.STObjectId;
            oldApp.STSubject = app.STSubject;
            oldApp.STCriteriaId = app.STCriteriaId;
            oldApp.STValue = app.STValue;
            oldApp.STRemark = app.STRemark;
            oldApp.STIsMilitary = app.STIsMilitary;
            oldApp.STNoteTypeId = app.STNoteTypeId;

            oldApp.STDocId = app.STDocId;
            oldApp.STChecklistId = app.STChecklistId;
            oldApp.STChecklistStatusId = app.STChecklistStatusId;
            oldApp.STNoteId = app.STNoteId;

            //II
            oldApp.NDAopApplicationTypeId = app.NDAopApplicationTypeId;
            oldApp.NDObjectId = app.NDObjectId;
            oldApp.NDSubject = app.NDSubject;
            oldApp.NDCriteriaId = app.NDCriteriaId;
            oldApp.NDValue = app.NDValue;
            oldApp.NDIsMilitary = app.NDIsMilitary;
            oldApp.NDROPIdNum = app.NDROPIdNum;
            oldApp.NDROPUnqNum = app.NDROPUnqNum;
            oldApp.NDROPDate = app.NDROPDate;
            oldApp.NDProcedureStatusId = app.NDProcedureStatusId;
            oldApp.NDRefusalReason = app.NDRefusalReason;
            oldApp.NDAppeal = app.NDAppeal;
            oldApp.NDRemark = app.NDRemark;

            oldApp.NDDocId = app.NDDocId;
            oldApp.NDChecklistId = app.NDChecklistId;
            oldApp.NDChecklistStatusId = app.NDChecklistStatusId;
            oldApp.NDReportId = app.NDReportId;

            //aop set oldapp.docid if not set

            this.unitOfWork.Save();

            return Ok(new
            {
                err = "",
                aopApplicationId = oldApp.AopApplicationId
            });
        }

        [Route("{id}/checklist")]
        [HttpPost]
        public IHttpActionResult CreateChildChecklist(int id, string identifier, string action)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                bool copy = false, correct = false;
                int docId;
                int checklistId = 0;

                AopApp app = this.unitOfWork.DbContext.Set<AopApp>().Find(id);

                if (identifier == "st")
                {
                    if (app.STDocId.HasValue)
                    {
                        docId = app.STDocId.Value;
                    }
                    else
                    {
                        throw new Exception("STDocId missing.");
                    }

                    if (action == "copy")
                    {
                        copy = true;

                        if (!app.STChecklistId.HasValue)
                        {
                            throw new Exception();
                        }

                        checklistId = app.STChecklistId.Value;
                    }
                    else if (action == "correct")
                    {
                        correct = true;

                        if (!app.STChecklistId.HasValue)
                        {
                            throw new Exception();
                        }

                        checklistId = app.STChecklistId.Value;
                    }
                }
                else if (identifier == "nd")
                {
                    if (app.NDDocId.HasValue)
                    {
                        docId = app.NDDocId.Value;
                    }
                    else
                    {
                        throw new Exception("NDDocId missing.");
                    }

                    if (action == "copy")
                    {
                        copy = true;

                        if (!app.NDChecklistId.HasValue)
                        {
                            throw new Exception();
                        }

                        checklistId = app.NDChecklistId.Value;
                    }
                    else if (action == "correct")
                    {
                        correct = true;

                        if (!app.NDChecklistId.HasValue)
                        {
                            throw new Exception();
                        }

                        checklistId = app.NDChecklistId.Value;
                    }
                }
                else
                {
                    throw new Exception("Identifier missing.");
                }

                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);
                DocEntryType documentEntryType = this.unitOfWork.DbContext.Set<DocEntryType>().SingleOrDefault(e => e.Alias == "Document");

                if (correct)
                {
                    documentEntryType = this.unitOfWork.DbContext.Set<DocEntryType>().SingleOrDefault(e => e.Alias == "Resolution");
                }

                DocDirection internalDocDirection = this.unitOfWork.DbContext.Set<DocDirection>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "Internal".ToLower());
                DocCasePartType internalDocCasePartType = this.unitOfWork.DbContext.Set<DocCasePartType>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "Internal".ToLower());
                DocFormatType paperDocFormatType = this.unitOfWork.DbContext.Set<DocFormatType>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "Paper".ToLower());
                DocStatus draftStatus = this.unitOfWork.DbContext.Set<DocStatus>().SingleOrDefault(e => e.Alias == "Draft");

                DocType docType = this.unitOfWork.DbContext.Set<DocType>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "EditableDocumentFile".ToLower());
                string docSubject = "Чеклист";

                if (identifier == "st")
                {
                    docSubject = "Чеклист за първи етап";
                }
                else
                {
                    docSubject = "Чеклист за втори етап";
                }

                if (correct)
                {
                    docType = this.unitOfWork.DbContext.Set<DocType>().SingleOrDefault(e => e.Alias == "Resolution");
                    docSubject = "Поправка на " + docSubject.ToLower();
                }
                else if (copy)
                {
                    docSubject = "Копие на " + docSubject.ToLower();
                }

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

                DocRelation parentDocRelation = this.unitOfWork.DbContext.Set<DocRelation>().FirstOrDefault(e => e.DocId == docId);

                if (copy || correct)
                {
                    if (parentDocRelation.ParentDocId.HasValue)
                    {
                        parentDocRelation = this.unitOfWork.DbContext.Set<DocRelation>().FirstOrDefault(e => e.DocId == parentDocRelation.ParentDocId.Value);
                    }
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

                List<DocUnitRole> docUnitRoles = this.unitOfWork.DbContext.Set<DocUnitRole>().ToList();

                List<DocTypeUnitRole> docTypeUnitRoles = new List<DocTypeUnitRole>();

                //from
                docTypeUnitRoles.Add(new DocTypeUnitRole
                {
                    DocTypeId = docType.DocTypeId,
                    DocDirectionId = internalDocDirection.DocDirectionId,
                    DocUnitRoleId = docUnitRoles.SingleOrDefault(e => e.Alias == "From").DocUnitRoleId,
                    UnitId = unitUser.UnitId,
                    IsActive = true
                });

                DocUnitRole importedBy = docUnitRoles.SingleOrDefault(e => e.Alias == "ImportedBy");

                if (correct)
                {
                    List<DocUnit> checklistImporters = this.unitOfWork.DbContext.Set<DocUnit>()
                        .Where(e => e.DocId == checklistId && e.DocUnitRoleId == importedBy.DocUnitRoleId)
                        .ToList();

                    foreach (var item in checklistImporters)
                    {
                        //InCharge
                        docTypeUnitRoles.Add(new DocTypeUnitRole
                        {
                            DocTypeId = docType.DocTypeId,
                            DocDirectionId = internalDocDirection.DocDirectionId,
                            DocUnitRoleId = docUnitRoles.SingleOrDefault(e => e.Alias == "InCharge").DocUnitRoleId,
                            UnitId = item.UnitId,
                            IsActive = true
                        });
                    }

                    AssignmentType noDeadline = this.unitOfWork.DbContext.Set<AssignmentType>()
                        .SingleOrDefault(e => e.Alias == "WithoutDeadline");

                    newDoc.AssignmentTypeId = noDeadline.AssignmentTypeId;
                    newDoc.AssignmentDate = DateTime.Now;
                }

                newDoc.CreateDocProperties(
                    parentDocRelation,
                    null,
                    docTypeClassifications,
                    electronicServiceStage,
                    docTypeUnitRoles,
                    importedBy,
                    unitUser,
                    null,
                    null,
                    this.userContext);

                Guid key = Guid.NewGuid();

                string username = userRepository.GetUser(unitUser.UserId).Username;
                string emptyChecklist = JsonConvert.SerializeObject(new
                {
                    version = "1",
                    author = username,
                    createDate = DateTime.Now
                }).ToString();

                string contentStr = string.Format("[{0}]", emptyChecklist);
                if (copy || correct)
                {
                    DocFile editable = this.unitOfWork.DbContext.Set<DocFile>()
                        .FirstOrDefault(e => e.DocId == checklistId && e.DocFileOriginType.Alias == "EditableFile");

                    if (editable != null)
                    {
                        List<System.Data.SqlClient.SqlParameter> sp = new List<System.Data.SqlClient.SqlParameter>();
                        sp.Add(new System.Data.SqlClient.SqlParameter("@key", editable.DocFileContentId));

                        byte[] contentToBeCopied = this.docRepository.SqlQuery<byte[]>(@"SELECT [Content] FROM [dbo].[Blobs] WHERE [Key] = @key", sp).FirstOrDefault();
                        
                        string contentToString = System.Text.Encoding.UTF8.GetString(contentToBeCopied);

                        JArray editableFiles = JsonConvert.DeserializeObject<JArray>(contentToString);
                        dynamic lastChecklist = editableFiles[editableFiles.Count - 1];
                        lastChecklist.version = int.Parse(Convert.ToString(lastChecklist.version)) + 1;
                        lastChecklist.author = username;
                        lastChecklist.createDate = DateTime.Now;

                        if (copy)
                        {
                            editableFiles.Add(lastChecklist);
                        }
                        else if (correct)
                        {
                            editableFiles[editableFiles.Count - 1] = lastChecklist;
                        }

                        contentStr = JsonConvert.SerializeObject(editableFiles).ToString();
                    }
                }

                byte[] content = System.Text.Encoding.UTF8.GetBytes(contentStr);

                System.Security.Cryptography.SHA1 sha1 = new System.Security.Cryptography.SHA1Managed();
                sha1.ComputeHash(content);

                List<System.Data.SqlClient.SqlParameter> sqlParams = new List<System.Data.SqlClient.SqlParameter>();
                sqlParams.Add(new System.Data.SqlClient.SqlParameter("@key", key));
                //sqlParams.Add(new System.Data.SqlClient.SqlParameter("@hash", BitConverter.ToString(sha1.Hash).Replace("-", string.Empty)));
                sqlParams.Add(new System.Data.SqlClient.SqlParameter("@hash", Guid.NewGuid().ToString().Replace("-", string.Empty))); //to bypass hash unique constraint in db
                sqlParams.Add(new System.Data.SqlClient.SqlParameter("@size", content.LongCount()));
                sqlParams.Add(new System.Data.SqlClient.SqlParameter("@content", content));

                this.docRepository.SqlQuery<decimal>(
@"
INSERT INTO [dbo].[Blobs] ([Key], [Hash], [Size], [Content], [IsDeleted]) 
    VALUES (@key, @hash, @size , @content, 0);

SELECT SCOPE_IDENTITY();
", sqlParams).FirstOrDefault();

                DocFileKind privateKind = this.unitOfWork.DbContext.Set<DocFileKind>()
                    .FirstOrDefault(e => e.Alias == "PrivateAttachedFile");
                DocFileType unknownType = this.unitOfWork.DbContext.Set<DocFileType>()
                    .FirstOrDefault(e => e.Alias == "UnknownBinary");
                DocFileOriginType editableFile = this.unitOfWork.DbContext.Set<DocFileOriginType>()
                    .FirstOrDefault(e => e.Alias == "EditableFile");

                newDoc.CreateDocFile(
                    privateKind.DocFileKindId,
                    unknownType.DocFileTypeId,
                    editableFile.DocFileOriginTypeId,
                    "Checklist",
                    "Checklist",
                    "",
                    key,
                    this.userContext);

                this.unitOfWork.Save();

                if (identifier == "st")
                {
                    app.STChecklistId = newDoc.DocId;
                }
                else if (identifier == "nd")
                {
                    app.NDChecklistId = newDoc.DocId;
                }

                this.docRepository.spSetDocUsers(newDoc.DocId);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new
                {
                    docId = newDoc.DocId
                });
            }
        }

        //[Route("{id}/note")]
        //[HttpPost]
        //public IHttpActionResult CreateChildNote(int id)
        //{
        //    using (var transaction = this.unitOfWork.BeginTransaction())
        //    {
        //        int docId;

        //        AopApp app = this.unitOfWork.DbContext.Set<AopApp>().Find(id);

        //        if (app.STDocId.HasValue)
        //        {
        //            docId = app.STDocId.Value;
        //        }
        //        else
        //        {
        //            throw new Exception("STDocId missing.");
        //        }

        //        UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);
        //        DocEntryType documentEntryType = this.unitOfWork.DbContext.Set<DocEntryType>().SingleOrDefault(e => e.Alias == "Document");

        //        DocDirection internalDocDirection = this.unitOfWork.DbContext.Set<DocDirection>()
        //            .SingleOrDefault(e => e.Alias.ToLower() == "Internal".ToLower());
        //        DocCasePartType internalDocCasePartType = this.unitOfWork.DbContext.Set<DocCasePartType>()
        //            .SingleOrDefault(e => e.Alias.ToLower() == "Internal".ToLower());
        //        DocFormatType paperDocFormatType = this.unitOfWork.DbContext.Set<DocFormatType>()
        //            .SingleOrDefault(e => e.Alias.ToLower() == "Paper".ToLower());
        //        DocStatus draftStatus = this.unitOfWork.DbContext.Set<DocStatus>().SingleOrDefault(e => e.Alias == "Draft");

        //        DocType docType = this.unitOfWork.DbContext.Set<DocType>()
        //            .SingleOrDefault(e => e.Alias.ToLower() == "Note".ToLower());
        //        string docSubject = "Генерирано становище";

        //        Doc newDoc = this.docRepository.CreateDoc(
        //            internalDocDirection.DocDirectionId,
        //            documentEntryType.DocEntryTypeId,
        //            draftStatus.DocStatusId,
        //            docSubject,
        //            internalDocCasePartType.DocCasePartTypeId,
        //            null,
        //            null,
        //            docType.DocTypeId,
        //            paperDocFormatType.DocFormatTypeId,
        //            null,
        //            userContext);

        //        DocRelation parentDocRelation = this.unitOfWork.DbContext.Set<DocRelation>().FirstOrDefault(e => e.DocId == docId);

        //        ElectronicServiceStage electronicServiceStage = null;
        //        if (parentDocRelation == null)
        //        {
        //            electronicServiceStage = this.unitOfWork.DbContext.Set<ElectronicServiceStage>()
        //                .SingleOrDefault(e => e.DocTypeId == newDoc.DocTypeId && e.IsFirstByDefault);
        //        }

        //        List<DocTypeClassification> docTypeClassifications = this.unitOfWork.DbContext.Set<DocTypeClassification>()
        //            .Where(e => e.DocDirectionId == newDoc.DocDirectionId && e.DocTypeId == newDoc.DocTypeId)
        //            .ToList();

        //        List<DocUnitRole> docUnitRoles = this.unitOfWork.DbContext.Set<DocUnitRole>().ToList();

        //        List<DocTypeUnitRole> docTypeUnitRoles = new List<DocTypeUnitRole>();

        //        //from
        //        docTypeUnitRoles.Add(new DocTypeUnitRole
        //        {
        //            DocTypeId = docType.DocTypeId,
        //            DocDirectionId = internalDocDirection.DocDirectionId,
        //            DocUnitRoleId = docUnitRoles.SingleOrDefault(e => e.Alias == "From").DocUnitRoleId,
        //            UnitId = unitUser.UnitId,
        //            IsActive = true
        //        });

        //        DocUnitRole importedBy = docUnitRoles.SingleOrDefault(e => e.Alias == "ImportedBy");

        //        newDoc.CreateDocProperties(
        //            parentDocRelation,
        //            null,
        //            docTypeClassifications,
        //            electronicServiceStage,
        //            docTypeUnitRoles,
        //            importedBy,
        //            unitUser,
        //            null,
        //            null,
        //            this.userContext);

        //        this.unitOfWork.Save();

        //        #region DummyFileContent

        //        DocFileKind docFileKind = this.unitOfWork.DbContext.Set<DocFileKind>().Single(e => e.Alias == "PublicAttachedFile");
        //        DocFileOriginType docFileOriginType = this.unitOfWork.DbContext.Set<DocFileOriginType>().Single(e => e.Alias == "AttachedFile");

        //        var fileKey = CreateDummyDocFileContent();

        //        DocFile docFile = new DocFile();
        //        docFile.Doc = newDoc;
        //        docFile.DocContentStorage = String.Empty;
        //        docFile.DocFileContentId = fileKey;
        //        docFile.DocFileTypeId = this.unitOfWork.DbContext.Set<DocFileType>().Single(e => e.Alias == "DOC").DocFileTypeId;
        //        docFile.DocFileKindId = docFileKind.DocFileKindId;
        //        docFile.Name = "Становище";
        //        docFile.DocFileName = "Note.doc";
        //        docFile.DocFileOriginTypeId = docFileOriginType.DocFileOriginTypeId;
        //        docFile.IsPrimary = true;
        //        docFile.IsSigned = false;
        //        docFile.IsActive = true;

        //        this.unitOfWork.DbContext.Set<DocFile>().Add(docFile);
        //        this.unitOfWork.Save();

        //        #endregion

        //        app.STNoteId = newDoc.DocId;

        //        this.docRepository.spSetDocUsers(newDoc.DocId);

        //        this.unitOfWork.Save();

        //        transaction.Commit();

        //        return Ok(new
        //        {
        //            docId = newDoc.DocId
        //        });
        //    }
        //}

        [Route("{id}/note")]
        public HttpResponseMessage GetCreateChildNote(int id)
        {
            DocFile docFile = this.unitOfWork.DbContext.Set<DocFile>()
                .FirstOrDefault(df => df.DocFileOriginTypeId.HasValue && df.DocFileOriginType.Alias == "EditableFile");

            List<System.Data.SqlClient.SqlParameter> sqlParams = new List<System.Data.SqlClient.SqlParameter>();
            sqlParams.Add(new System.Data.SqlClient.SqlParameter("@key", docFile.DocFileContentId));

            byte[] content = this.docRepository.SqlQuery<byte[]>(@"SELECT [Content] FROM [dbo].[Blobs] WHERE [Key] = @key", sqlParams).FirstOrDefault();

            JObject note = JObject.Parse(System.Text.Encoding.UTF8.GetString(content));
            JObject json = this.dataGenerator.Generate(note);

            string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\Aop.Web.App\word_templates\stanovishte_template.docx");

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new PushStreamContent(
                (outputStream, httpContent, transportContext) =>
                {
                    using (outputStream)
                    {
                        using (FileStream template = File.Open(templatePath, FileMode.Open, FileAccess.ReadWrite))
                        using (var memoryStream = new MemoryStream())
                        {
                            template.CopyTo(memoryStream);

                            WordTemplateTransformer tt = new WordTemplateTransformer(memoryStream);
                            tt.Transform(json);

                            memoryStream.Position = 0;
                            memoryStream.CopyTo(outputStream);
                        }
                    }
                });
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "note"
                };

            return result;
        }

        //[Route("{id}/report")]
        //[HttpPost]
        //public IHttpActionResult CreateChildReport(int id)
        //{
        //    using (var transaction = this.unitOfWork.BeginTransaction())
        //    {
        //        int docId;

        //        AopApp app = this.unitOfWork.DbContext.Set<AopApp>().Find(id);

        //        if (app.NDDocId.HasValue)
        //        {
        //            docId = app.NDDocId.Value;
        //        }
        //        else
        //        {
        //            throw new Exception("NDDocId missing.");
        //        }

        //        UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);
        //        DocEntryType documentEntryType = this.unitOfWork.DbContext.Set<DocEntryType>().SingleOrDefault(e => e.Alias == "Document");

        //        DocDirection internalDocDirection = this.unitOfWork.DbContext.Set<DocDirection>()
        //            .SingleOrDefault(e => e.Alias.ToLower() == "Internal".ToLower());
        //        DocCasePartType internalDocCasePartType = this.unitOfWork.DbContext.Set<DocCasePartType>()
        //            .SingleOrDefault(e => e.Alias.ToLower() == "Internal".ToLower());
        //        DocFormatType paperDocFormatType = this.unitOfWork.DbContext.Set<DocFormatType>()
        //            .SingleOrDefault(e => e.Alias.ToLower() == "Paper".ToLower());
        //        DocStatus draftStatus = this.unitOfWork.DbContext.Set<DocStatus>().SingleOrDefault(e => e.Alias == "Draft");

        //        DocType docType = this.unitOfWork.DbContext.Set<DocType>()
        //            .SingleOrDefault(e => e.Alias.ToLower() == "Report".ToLower());
        //        string docSubject = "Генериран доклад";

        //        Doc newDoc = this.docRepository.CreateDoc(
        //            internalDocDirection.DocDirectionId,
        //            documentEntryType.DocEntryTypeId,
        //            draftStatus.DocStatusId,
        //            docSubject,
        //            internalDocCasePartType.DocCasePartTypeId,
        //            null,
        //            null,
        //            docType.DocTypeId,
        //            paperDocFormatType.DocFormatTypeId,
        //            null,
        //            userContext);

        //        DocRelation parentDocRelation = this.unitOfWork.DbContext.Set<DocRelation>().FirstOrDefault(e => e.DocId == docId);

        //        ElectronicServiceStage electronicServiceStage = null;
        //        if (parentDocRelation == null)
        //        {
        //            electronicServiceStage = this.unitOfWork.DbContext.Set<ElectronicServiceStage>()
        //                .SingleOrDefault(e => e.DocTypeId == newDoc.DocTypeId && e.IsFirstByDefault);
        //        }

        //        List<DocTypeClassification> docTypeClassifications = this.unitOfWork.DbContext.Set<DocTypeClassification>()
        //            .Where(e => e.DocDirectionId == newDoc.DocDirectionId && e.DocTypeId == newDoc.DocTypeId)
        //            .ToList();

        //        List<DocUnitRole> docUnitRoles = this.unitOfWork.DbContext.Set<DocUnitRole>().ToList();

        //        List<DocTypeUnitRole> docTypeUnitRoles = new List<DocTypeUnitRole>();

        //        //from
        //        docTypeUnitRoles.Add(new DocTypeUnitRole
        //        {
        //            DocTypeId = docType.DocTypeId,
        //            DocDirectionId = internalDocDirection.DocDirectionId,
        //            DocUnitRoleId = docUnitRoles.SingleOrDefault(e => e.Alias == "From").DocUnitRoleId,
        //            UnitId = unitUser.UnitId,
        //            IsActive = true
        //        });

        //        DocUnitRole importedBy = docUnitRoles.SingleOrDefault(e => e.Alias == "ImportedBy");

        //        newDoc.CreateDocProperties(
        //            parentDocRelation,
        //            null,
        //            docTypeClassifications,
        //            electronicServiceStage,
        //            docTypeUnitRoles,
        //            importedBy,
        //            unitUser,
        //            null,
        //            null,
        //            this.userContext);

        //        this.unitOfWork.Save();

        //        #region DummyFileContent

        //        DocFileKind docFileKind = this.unitOfWork.DbContext.Set<DocFileKind>().Single(e => e.Alias == "PublicAttachedFile");
        //        DocFileOriginType docFileOriginType = this.unitOfWork.DbContext.Set<DocFileOriginType>().Single(e => e.Alias == "AttachedFile");

        //        var fileKey = CreateDummyDocFileContent();

        //        DocFile docFile = new DocFile();
        //        docFile.Doc = newDoc;
        //        docFile.DocContentStorage = String.Empty;
        //        docFile.DocFileContentId = fileKey;
        //        docFile.DocFileTypeId = this.unitOfWork.DbContext.Set<DocFileType>().Single(e => e.Alias == "DOC").DocFileTypeId;
        //        docFile.DocFileKindId = docFileKind.DocFileKindId;
        //        docFile.Name = "Доклад";
        //        docFile.DocFileName = "Report.doc";
        //        docFile.DocFileOriginTypeId = docFileOriginType.DocFileOriginTypeId;
        //        docFile.IsPrimary = true;
        //        docFile.IsSigned = false;
        //        docFile.IsActive = true;

        //        this.unitOfWork.DbContext.Set<DocFile>().Add(docFile);
        //        this.unitOfWork.Save();

        //        #endregion

        //        app.NDReportId = newDoc.DocId;

        //        this.docRepository.spSetDocUsers(newDoc.DocId);

        //        this.unitOfWork.Save();

        //        transaction.Commit();

        //        return Ok(new
        //        {
        //            docId = newDoc.DocId
        //        });
        //    }
        //}

        [Route("{id}/report")]
        public HttpResponseMessage GetCreateChildReport(int id)
        {
            DocFile docFile = this.unitOfWork.DbContext.Set<DocFile>()
                .FirstOrDefault(df => df.DocFileOriginTypeId.HasValue && df.DocFileOriginType.Alias == "EditableFile");

            List<System.Data.SqlClient.SqlParameter> sqlParams = new List<System.Data.SqlClient.SqlParameter>();
            sqlParams.Add(new System.Data.SqlClient.SqlParameter("@key", docFile.DocFileContentId));

            byte[] content = this.docRepository.SqlQuery<byte[]>(@"SELECT [Content] FROM [dbo].[Blobs] WHERE [Key] = @key", sqlParams).FirstOrDefault();

            JObject note = JObject.Parse(System.Text.Encoding.UTF8.GetString(content));
            JObject json = this.dataGenerator.Generate(note);

            string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\Aop.Web.App\word_templates\doklad_template.docx");

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new PushStreamContent(
                (outputStream, httpContent, transportContext) =>
                {
                    using (outputStream)
                    {
                        using (FileStream template = File.Open(templatePath, FileMode.Open, FileAccess.ReadWrite))
                        using (var memoryStream = new MemoryStream())
                        {
                            template.CopyTo(memoryStream);

                            WordTemplateTransformer tt = new WordTemplateTransformer(memoryStream);
                            tt.Transform(json);

                            memoryStream.Position = 0;
                            memoryStream.CopyTo(outputStream);
                        }
                    }
                });
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "report"
                };

            return result;
        }

        private Guid CreateDummyDocFileContent()
        {
            StringBuilder builder = new StringBuilder("ГЕНЕРИРАН ФАЙЛ");

            byte[] content = Utf8Utils.GetBytes(builder.ToString());

            Guid fileKey = WriteToBlob(content);

            return fileKey;
        }

        private Guid WriteToBlob(byte[] content)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
            {
                connection.Open();
                using (var blobWriter = new BlobWriter(connection))
                using (var stream = blobWriter.OpenStream())
                {
                    stream.Write(content, 0, content.Length);
                    return blobWriter.GetBlobKey();
                }
            }
        }
    }
}
