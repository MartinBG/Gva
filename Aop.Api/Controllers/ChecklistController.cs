using Aop.Api.Models;
using Common.Api.UserContext;
using Common.Blob;
using Common.WordTemplates;
using Docs.Api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Http;

namespace Aop.Api.Controllers
{
    [Authorize]
    public class ChecklistController : ApiController
    {
        private Common.Data.IUnitOfWork unitOfWork;
        private Aop.Api.Repositories.Aop.IAppRepository appRepository;
        private Docs.Api.Repositories.DocRepository.IDocRepository docRepository;
        private Aop.Api.WordTemplates.IDataGenerator dataGenerator;

        private UserContext userContext;

        public ChecklistController(Common.Data.IUnitOfWork unitOfWork,
            Aop.Api.Repositories.Aop.IAppRepository appRepository,
            Docs.Api.Repositories.DocRepository.IDocRepository docRepository,
            Aop.Api.WordTemplates.IDataGenerator dataGenerator)
        {
            this.unitOfWork = unitOfWork;
            this.appRepository = appRepository;
            this.docRepository = docRepository;
            this.dataGenerator = dataGenerator;
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

        [Route("api/aop/editableFiles/checklist")]
        [HttpGet]
        public IHttpActionResult LoadChecklist(int id)
        {
            var doc = this.docRepository.Find(id,
               e => e.DocFiles.Select(df => df.DocFileOriginType));

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

            string contentToString = System.Text.Encoding.UTF8.GetString(content);

            AopApp aopApp = this.unitOfWork.DbContext.Set<AopApp>().FirstOrDefault(e => e.STChecklistId == id || e.NDChecklistId == id);

            return Ok(new
            {
                aopApplicationId = aopApp.AopApplicationId,
                content = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(contentToString)
            });
        }

        [Route("api/aop/apps/{id}/checklist")]
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

                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().Include(e => e.Unit).FirstOrDefault(e => e.UserId == this.userContext.UserId);
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

                List<DocClassification> parentDocClassifications = this.unitOfWork.DbContext.Set<DocClassification>()
                        .Where(e => e.DocId == parentDocRelation.DocId &&
                            e.IsActive &&
                            e.IsInherited)
                        .ToList();

                newDoc.CreateDocProperties(
                    parentDocRelation,
                    null,
                    null,
                    parentDocClassifications,
                    electronicServiceStage, //? should there be a stage
                    docTypeUnitRoles,
                    importedBy,
                    unitUser,
                    null,
                    null,
                    this.userContext);

                Guid blobKey;

                string emptyChecklist = JsonConvert.SerializeObject(new
                {
                    version = "1",
                    author = unitUser.Unit.Name,
                    createDate = DateTime.Now
                }).ToString();

                string contentStr = string.Format("{{ versions: [{0}] }}", emptyChecklist);

                if (copy || correct)
                {
                    DocFile editable = this.unitOfWork.DbContext.Set<DocFile>().FirstOrDefault(e => e.DocId == checklistId && e.DocFileOriginType.Alias == "EditableFile");

                    if (editable != null)
                    {
                        byte[] contentToBeCopied;

                        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
                        {
                            connection.Open();

                            using (MemoryStream m1 = new MemoryStream())
                            using (var blobStream = new BlobReadStream(connection, "dbo", "Blobs", "Content", "Key", editable.DocFileContentId))
                            {
                                blobStream.CopyTo(m1);
                                contentToBeCopied = m1.ToArray();
                            }
                        }

                        string contentToString = System.Text.Encoding.UTF8.GetString(contentToBeCopied);

                        JObject checklistObj = JsonConvert.DeserializeObject<JObject>(contentToString);
                        JArray checklistVersions = checklistObj["versions"] as JArray;
                        dynamic newChecklistVersion = checklistVersions.First().DeepClone();
                        newChecklistVersion.version = int.Parse(Convert.ToString(newChecklistVersion.version)) + 1;
                        newChecklistVersion.author = unitUser.Unit.Name;
                        newChecklistVersion.createDate = DateTime.Now;

                        checklistVersions.Insert(0, newChecklistVersion);

                        contentStr = JsonConvert.SerializeObject(checklistObj);
                    }
                }

                byte[] content = System.Text.Encoding.UTF8.GetBytes(contentStr);

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
                {
                    connection.Open();
                    using (var blobWriter = new BlobWriter(connection))
                    using (var stream = blobWriter.OpenStream())
                    {
                        stream.Write(content, 0, content.Length);
                        blobKey = blobWriter.GetBlobKey();
                    }
                }

                DocFileKind privateKind = this.unitOfWork.DbContext.Set<DocFileKind>()
                    .FirstOrDefault(e => e.Alias == "PrivateAttachedFile");
                DocFileType unknownType = this.unitOfWork.DbContext.Set<DocFileType>()
                    .FirstOrDefault(e => e.Alias == "Checklist");
                DocFileOriginType editableFile = this.unitOfWork.DbContext.Set<DocFileOriginType>()
                    .FirstOrDefault(e => e.Alias == "EditableFile");

                newDoc.CreateDocFile(
                    privateKind.DocFileKindId,
                    unknownType.DocFileTypeId,
                    editableFile.DocFileOriginTypeId,
                    "Checklist",
                    "Checklist",
                    "",
                    blobKey,
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

                this.docRepository.ExecSpSetDocTokens(docId: newDoc.DocId);
                this.docRepository.ExecSpSetDocUnitTokens(docId: newDoc.DocId);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new
                {
                    docId = newDoc.DocId
                });
            }
        }

        [Route("api/aop/apps/{id}/note")]
        [HttpPost]
        public IHttpActionResult CreateChildNote(int id)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                int docId, checklistId;

                AopApp app = this.unitOfWork.DbContext.Set<AopApp>().Find(id);

                if (app.STDocId.HasValue && app.STChecklistId.HasValue)
                {
                    docId = app.STDocId.Value;
                    checklistId = app.STChecklistId.Value;
                }
                else
                {
                    throw new Exception("STDocId OR STChecklistId missing.");
                }

                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);
                DocEntryType documentEntryType = this.unitOfWork.DbContext.Set<DocEntryType>().SingleOrDefault(e => e.Alias == "Document");

                DocDirection internalDocDirection = this.unitOfWork.DbContext.Set<DocDirection>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "Internal".ToLower());
                DocCasePartType internalDocCasePartType = this.unitOfWork.DbContext.Set<DocCasePartType>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "Internal".ToLower());
                DocFormatType paperDocFormatType = this.unitOfWork.DbContext.Set<DocFormatType>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "Paper".ToLower());
                DocStatus draftStatus = this.unitOfWork.DbContext.Set<DocStatus>().SingleOrDefault(e => e.Alias == "Draft");

                DocType docType = this.unitOfWork.DbContext.Set<DocType>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "Note".ToLower());
                string docSubject = "Генерирано становище";

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

                ElectronicServiceStage electronicServiceStage = null;
                if (parentDocRelation == null)
                {
                    electronicServiceStage = this.unitOfWork.DbContext.Set<ElectronicServiceStage>()
                        .SingleOrDefault(e => e.DocTypeId == newDoc.DocTypeId && e.IsFirstByDefault);
                }

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

                List<DocClassification> parentDocClassifications = this.unitOfWork.DbContext.Set<DocClassification>()
                        .Where(e => e.DocId == parentDocRelation.DocId &&
                            e.IsActive &&
                            e.IsInherited)
                        .ToList();

                newDoc.CreateDocProperties(
                    parentDocRelation,
                    null,
                    null,
                    parentDocClassifications,
                    electronicServiceStage, //? should there be a stage
                    docTypeUnitRoles,
                    importedBy,
                    unitUser,
                    null,
                    null,
                    this.userContext);

                this.unitOfWork.Save();

                #region FileContent

                DocFile docFile = this.unitOfWork.DbContext.Set<DocFile>()
                    .FirstOrDefault(e => e.DocId == checklistId && e.DocFileOriginTypeId.HasValue && e.DocFileOriginType.Alias == "EditableFile");
                DocFileType docFileType = this.unitOfWork.DbContext.Set<DocFileType>().SingleOrDefault(e => e.Alias == "DOCX");
                DocFileKind docFileKind = this.unitOfWork.DbContext.Set<DocFileKind>().Single(e => e.Alias == "PublicAttachedFile");
                DocFileOriginType docFileOriginType = this.unitOfWork.DbContext.Set<DocFileOriginType>().Single(e => e.Alias == "AttachedFile");

                Guid blobKey;

                byte[] content;

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
                {
                    connection.Open();

                    using (MemoryStream m1 = new MemoryStream())
                    using (var blobStream = new BlobReadStream(connection, "dbo", "Blobs", "Content", "Key", docFile.DocFileContentId))
                    {
                        blobStream.CopyTo(m1);
                        content = m1.ToArray();
                    }

                    string contentToString = System.Text.Encoding.UTF8.GetString(content);
                    JObject checklistObj = JsonConvert.DeserializeObject<JObject>(contentToString);
                    JArray checklistVersions = checklistObj["versions"] as JArray;
                    JObject latest = checklistVersions.First() as JObject;

                    DocUnit checklistMadeBy = this.unitOfWork.DbContext.Set<DocUnit>()
                        .Include(e => e.Unit.UnitRelations.Select(u => u.ParentUnit))
                        .Include(e => e.DocUnitRole)
                        .FirstOrDefault(e => e.DocId == checklistId && (e.DocUnitRole.Alias == "ImportedBy" || e.DocUnitRole.Alias == "MadeBy"));

                    DocWorkflow checklistDiscuss = this.unitOfWork.DbContext.Set<DocWorkflow>()
                        .Include(e => e.PrincipalUnit.UnitRelations.Select(u => u.ParentUnit))
                        .FirstOrDefault(e => e.DocId == checklistId && e.DocWorkflowAction.Alias == "Discuss");

                    JObject json = this.dataGenerator.GenerateNote(latest,
                        date: DateTime.Now.ToString("dd.MM.yyyy"),
                        madeByName: checklistMadeBy != null ? checklistMadeBy.Unit.Name : string.Empty,
                        madeByPosition: checklistMadeBy != null ? checklistMadeBy.Unit.UnitRelations.First().ParentUnit.Name : string.Empty,
                        coordinatorName: checklistDiscuss != null ? checklistDiscuss.PrincipalUnit.Name : string.Empty,
                        coordinatorPosition: checklistDiscuss != null ? checklistDiscuss.PrincipalUnit.UnitRelations.First().ParentUnit.Name : string.Empty
                        );

                    string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App\word_templates\stanovishte_template.docx");

                    using (FileStream template = File.Open(templatePath, FileMode.Open, FileAccess.ReadWrite))
                    using (var memoryStream = new MemoryStream())
                    using (var blobWriter = new BlobWriter(connection))
                    using (var stream = blobWriter.OpenStream())
                    {
                        template.CopyTo(memoryStream);

                        WordTemplateTransformer tt = new WordTemplateTransformer(memoryStream);
                        tt.Transform(json);

                        memoryStream.Position = 0;
                        memoryStream.CopyTo(stream);

                        blobKey = blobWriter.GetBlobKey();
                    }
                }

                newDoc.CreateDocFile(
                    docFileKind.DocFileKindId,
                    docFileType.DocFileTypeId,
                    docFileOriginType.DocFileOriginTypeId,
                    "Note.docx",
                    "Note.docx",
                    "",
                    blobKey,
                    this.userContext);

                #endregion

                app.STNoteId = newDoc.DocId;

                this.docRepository.ExecSpSetDocTokens(docId: newDoc.DocId);
                this.docRepository.ExecSpSetDocUnitTokens(docId: newDoc.DocId);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new
                {
                    docId = newDoc.DocId
                });
            }
        }

        [Route("api/aop/apps/{id}/report")]
        [HttpPost]
        public IHttpActionResult CreateChildReport(int id)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                int docId, checklistId;

                AopApp app = this.unitOfWork.DbContext.Set<AopApp>().Find(id);

                if (app.NDDocId.HasValue && app.NDChecklistId.HasValue)
                {
                    docId = app.NDDocId.Value;
                    checklistId = app.NDChecklistId.Value;
                }
                else
                {
                    throw new Exception("NDDocId OR NDChecklistId missing.");
                }

                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);
                DocEntryType documentEntryType = this.unitOfWork.DbContext.Set<DocEntryType>().SingleOrDefault(e => e.Alias == "Document");

                DocDirection internalDocDirection = this.unitOfWork.DbContext.Set<DocDirection>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "Internal".ToLower());
                DocCasePartType internalDocCasePartType = this.unitOfWork.DbContext.Set<DocCasePartType>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "Internal".ToLower());
                DocFormatType paperDocFormatType = this.unitOfWork.DbContext.Set<DocFormatType>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "Paper".ToLower());
                DocStatus draftStatus = this.unitOfWork.DbContext.Set<DocStatus>().SingleOrDefault(e => e.Alias == "Draft");

                DocType docType = this.unitOfWork.DbContext.Set<DocType>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "Report".ToLower());
                string docSubject = "Генериран доклад";

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

                ElectronicServiceStage electronicServiceStage = null;
                if (parentDocRelation == null)
                {
                    electronicServiceStage = this.unitOfWork.DbContext.Set<ElectronicServiceStage>()
                        .SingleOrDefault(e => e.DocTypeId == newDoc.DocTypeId && e.IsFirstByDefault);
                }

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

                List<DocClassification> parentDocClassifications = this.unitOfWork.DbContext.Set<DocClassification>()
                       .Where(e => e.DocId == parentDocRelation.DocId &&
                           e.IsActive &&
                           e.IsInherited)
                       .ToList();

                newDoc.CreateDocProperties(
                    parentDocRelation,
                    null,
                    null,
                    parentDocClassifications,
                    electronicServiceStage,
                    docTypeUnitRoles,
                    importedBy,
                    unitUser,
                    null,
                    null,
                    this.userContext);

                this.unitOfWork.Save();

                #region FileContent

                DocFile docFile = this.unitOfWork.DbContext.Set<DocFile>()
                    .FirstOrDefault(e => e.DocId == checklistId && e.DocFileOriginTypeId.HasValue && e.DocFileOriginType.Alias == "EditableFile");
                DocFileType docFileType = this.unitOfWork.DbContext.Set<DocFileType>().SingleOrDefault(e => e.Alias == "DOCX");
                DocFileKind docFileKind = this.unitOfWork.DbContext.Set<DocFileKind>().Single(e => e.Alias == "PublicAttachedFile");
                DocFileOriginType docFileOriginType = this.unitOfWork.DbContext.Set<DocFileOriginType>().Single(e => e.Alias == "AttachedFile");

                Guid blobKey;

                byte[] content;

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
                {
                    connection.Open();

                    using (MemoryStream m1 = new MemoryStream())
                    using (var blobStream = new BlobReadStream(connection, "dbo", "Blobs", "Content", "Key", docFile.DocFileContentId))
                    {
                        blobStream.CopyTo(m1);
                        content = m1.ToArray();
                    }

                    string contentToString = System.Text.Encoding.UTF8.GetString(content);
                    JObject checklistObj = JsonConvert.DeserializeObject<JObject>(contentToString);
                    JArray checklistVersions = checklistObj["versions"] as JArray;
                    JObject lastest = checklistVersions.First() as JObject;

                    DocUnit checklistMadeBy = this.unitOfWork.DbContext.Set<DocUnit>()
                        .Include(e => e.Unit.UnitRelations.Select(u => u.ParentUnit))
                        .Include(e => e.DocUnitRole)
                        .FirstOrDefault(e => e.DocId == checklistId && (e.DocUnitRole.Alias == "ImportedBy" || e.DocUnitRole.Alias == "MadeBy"));

                    DocWorkflow checklistDiscuss = this.unitOfWork.DbContext.Set<DocWorkflow>()
                        .Include(e => e.PrincipalUnit.UnitRelations.Select(u => u.ParentUnit))
                        .FirstOrDefault(e => e.DocId == checklistId && e.DocWorkflowAction.Alias == "Discuss");

                    JObject json = this.dataGenerator.GenerateReport(lastest,
                        date: DateTime.Now.ToString("dd.MM.yyyy"),
                        madeByName: checklistMadeBy != null ? checklistMadeBy.Unit.Name : string.Empty,
                        madeByPosition: checklistMadeBy != null ? checklistMadeBy.Unit.UnitRelations.First().ParentUnit.Name : string.Empty,
                        coordinatorName: checklistDiscuss != null ? checklistDiscuss.PrincipalUnit.Name : string.Empty,
                        coordinatorPosition: checklistDiscuss != null ? checklistDiscuss.PrincipalUnit.UnitRelations.First().ParentUnit.Name : string.Empty
                        );

                    string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App\word_templates\doklad_template.docx");

                    using (FileStream template = File.Open(templatePath, FileMode.Open, FileAccess.ReadWrite))
                    using (var memoryStream = new MemoryStream())
                    using (var blobWriter = new BlobWriter(connection))
                    using (var stream = blobWriter.OpenStream())
                    {
                        template.CopyTo(memoryStream);

                        WordTemplateTransformer tt = new WordTemplateTransformer(memoryStream);
                        tt.Transform(json);

                        memoryStream.Position = 0;
                        memoryStream.CopyTo(stream);

                        blobKey = blobWriter.GetBlobKey();
                    }
                }

                newDoc.CreateDocFile(
                    docFileKind.DocFileKindId,
                    docFileType.DocFileTypeId,
                    docFileOriginType.DocFileOriginTypeId,
                    "Report.docx",
                    "Report.docx",
                    "",
                    blobKey,
                    this.userContext);

                #endregion

                app.NDReportId = newDoc.DocId;

                this.docRepository.ExecSpSetDocTokens(docId: newDoc.DocId);
                this.docRepository.ExecSpSetDocUnitTokens(docId: newDoc.DocId);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new
                {
                    docId = newDoc.DocId
                });
            }
        }
    }
}