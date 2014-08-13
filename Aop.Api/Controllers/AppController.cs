using Aop.Api.DataObjects;
using Aop.Api.Models;
using Aop.Api.Repositories.Aop;
using Aop.Api.Utils;
using Aop.Api.WordTemplates;
using Common.Api.Models;
using Common.Api.Repositories.UserRepository;
using Common.Api.UserContext;
using Common.Blob;
using Common.Data;
using Common.Extensions;
using Common.Utils;
using Docs.Api.DataObjects;
using Docs.Api.Models;
using Docs.Api.Repositories.DocRepository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Aop.Api.Controllers
{
    [RoutePrefix("api/aop/apps")]
    [Authorize]
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
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);

                AopApp app = appRepository.CreateNewAopApp(unitUser.UnitId, this.userContext);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new
                {
                    aopApplicationId = app.AopApplicationId
                });
            }
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

            var returnValue = this.appRepository.GetApps(limit, offset, out totalCount)
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
                e => e.CreateUnit,
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

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteApp(int id)
        {
            this.appRepository.DeteleAopApp(id);

            this.unitOfWork.Save();

            return Ok();
        }

        [Route("docs")]
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
            string corrs = null,
            string units = null,
            string ds = null,
            int? isChosen = null
            )
        {
            this.userContext = this.Request.GetUserContext();

            //? hot fix: load fist 1000 docs, so the paging with datatable will work
            limit = 1000;
            offset = 0;

            UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);
            ClassificationPermission readPermission = this.unitOfWork.DbContext.Set<ClassificationPermission>().SingleOrDefault(e => e.Alias == "Read");
            DocSourceType docSourceType = this.unitOfWork.DbContext.Set<DocSourceType>().SingleOrDefault(e => e.Alias == "Internet");
            DocCasePartType docCasePartType = this.unitOfWork.DbContext.Set<DocCasePartType>().SingleOrDefault(e => e.Alias == "Control");
            List<DocStatus> docStatuses = this.unitOfWork.DbContext.Set<DocStatus>().Where(e => e.IsActive).ToList();
            List<DocUnitRole> docUnitRoles = this.unitOfWork.DbContext.Set<DocUnitRole>()
                .Where(e => e.Alias.ToLower() == "incharge" || e.Alias.ToLower() == "controlling" || e.Alias.ToLower() == "to")
                .ToList();

            int totalCount = 0;
            List<Doc> docs = new List<Doc>();

            docs = this.docRepository.GetDocs(
                      fromDate,
                      toDate,
                      regUri,
                      docName,
                      docTypeId,
                      docStatusId,
                      hideRead,
                      null,
                      corrs,
                      units,
                      ds,
                      limit,
                      offset,
                      docCasePartType,
                      readPermission,
                      unitUser,
                      out totalCount);

            List<int> loadedDocIds = docs.Select(e => e.DocId).ToList();

            var docHasReads = this.unitOfWork.DbContext.Set<DocHasRead>()
                .Where(e => e.UnitId == unitUser.UnitId && loadedDocIds.Contains(e.DocId)).ToList();

            List<DocListItemDO> returnValue = docs.Select(e => new DocListItemDO(e, unitUser)).ToList();

            //? hot fix: load fist 1000 docs, so the paging with datatable will work
            //? gonna fail miserably with more docs
            foreach (var item in returnValue)
            {
                var docCorrespondents = this.unitOfWork.DbContext.Set<DocCorrespondent>()
                    .Include(e => e.Correspondent.CorrespondentType)
                    .Where(e => e.DocId == item.DocId)
                    .ToList();

                item.DocCorrespondents.AddRange(docCorrespondents.Select(e => new DocCorrespondentDO(e)).ToList());
            }

            //? hot fix: load fist 1000 docs, so the paging with datatable will work
            //? gonna fail miserably with more docs
            if (isChosen == 1) //true
            {
                List<AopApp> allAopApps = this.unitOfWork.DbContext.Set<AopApp>().ToList();
                List<int> aopAppDocIds =
                    allAopApps.Where(e => e.STDocId.HasValue).Select(e => e.STDocId.Value)
                    .Union(allAopApps.Where(e => e.STChecklistId.HasValue).Select(e => e.STChecklistId.Value))
                    .Union(allAopApps.Where(e => e.STNoteId.HasValue).Select(e => e.STNoteId.Value))
                    .Union(allAopApps.Where(e => e.NDDocId.HasValue).Select(e => e.NDDocId.Value))
                    .Union(allAopApps.Where(e => e.NDChecklistId.HasValue).Select(e => e.NDChecklistId.Value))
                    .Union(allAopApps.Where(e => e.NDReportId.HasValue).Select(e => e.NDReportId.Value))
                    .ToList();

                for (int i = 0; i < returnValue.Count; i++)
                {
                    if (returnValue[i].DocId.HasValue &&
                        aopAppDocIds.Contains(returnValue[i].DocId.Value))
                    {
                        returnValue.RemoveAt(i);
                    }
                }
            }

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

        [Route("docs/{id}")]
        [HttpGet]
        public IHttpActionResult GetAopAppForDoc(int id)
        {
            int? aopAppId = null;

            AopApp app = this.unitOfWork.DbContext.Set<AopApp>()
                .FirstOrDefault(e => e.STDocId == id || e.STChecklistId == id || e.STNoteId == id
                    || e.NDDocId == id || e.NDChecklistId == id || e.NDReportId == id);

            if (app != null)
            {
                aopAppId = app.AopApplicationId;
            }

            return Ok(new
            {
                aopApplicationId = aopAppId
            });
        }

        [Route("{id}/fed/first")]
        [HttpPost]
        public IHttpActionResult ReadFedForFirstStage(int id)
        {
            AopApp app = this.appRepository.Find(id);

            if (app.STDocId.HasValue)
            {
                Doc doc = this.docRepository.Find(app.STDocId.Value,
                    e => e.DocFiles);

                var fedFile = doc.DocFiles.FirstOrDefault(e => e.DocFileName.EndsWith(".fed"));

                if (fedFile != null)
                {
                    byte[] fedContent;

                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
                    {
                        connection.Open();

                        using (MemoryStream m1 = new MemoryStream())
                        using (var blobStream = new BlobReadStream(connection, "dbo", "Blobs", "Content", "Key", fedFile.DocFileContentId))
                        {
                            blobStream.CopyTo(m1);
                            fedContent = m1.ToArray();
                        }
                    }

                    List<NOMv5.nom> noms = FedHelper.GetFedDocumentNomenclatures();
                    List<NUTv5.item> nuts = FedHelper.GetFedDocumentNuts();

                    FEDv5.document fedDoc = XmlSerializerUtils.XmlDeserializeFromBytes<FEDv5.document>(fedContent);

                    FedExtractor.noms = noms;

                    //ST properties
                    string procType = FedExtractor.GetProcedureTypeShort(fedDoc);
                    string obj = FedExtractor.GetObject(fedDoc);
                    string criteria = FedExtractor.GetOffersCriteriaOnly(fedDoc);
                    string subject = FedExtractor.GetSubject(fedDoc);
                    string val = FedExtractor.GetPredictedValue(fedDoc);

                    var stAopApplicationType = this.unitOfWork.DbContext.Set<AopApplicationType>()
                        .FirstOrDefault(e => e.Name.ToLower().StartsWith(procType.ToLower()));
                    if (stAopApplicationType != null)
                    {
                        app.STAopApplicationTypeId = stAopApplicationType.AopApplicationTypeId;
                    }

                    var stObject = this.unitOfWork.DbContext.Set<AopApplicationObject>()
                        .FirstOrDefault(e => e.Name.ToLower().StartsWith(obj.ToLower()));
                    if (stObject != null)
                    {
                        app.STObjectId = stObject.AopApplicationObjectId;
                    }

                    var stCriteria = this.unitOfWork.DbContext.Set<AopApplicationCriteria>()
                        .FirstOrDefault(e => e.Name.ToLower().StartsWith(criteria.ToLower()));
                    if (stCriteria != null)
                    {
                        app.STCriteriaId = stCriteria.AopApplicationCriteriaId;
                    }

                    app.STSubject = subject;
                    app.STValue = val;

                    //възложител
                    string aopEmpUic = FedExtractor.GetEIK(fedDoc);

                    var aopEmp = this.unitOfWork.DbContext.Set<AopEmployer>()
                        .FirstOrDefault(e => e.Uic == aopEmpUic);

                    if (aopEmp != null)
                    {
                        app.AopEmployerId = aopEmp.AopEmployerId;
                    }
                    else
                    {
                        string aopEmpName = FedExtractor.GetContractor(fedDoc);
                        string aopEmpLotNum = FedExtractor.GetContractorBatch(fedDoc);

                        AopEmployerType unknownAopEmpType = this.appRepository.GetAopEmployerTypeByAlias("Unknown");

                        AopEmployer emp = appRepository.CreateAopEmployer(aopEmpName, aopEmpLotNum, aopEmpUic, unknownAopEmpType.AopEmployerTypeId);

                        this.unitOfWork.DbContext.Set<AopEmployer>().Add(emp);

                        this.unitOfWork.Save();

                        app.AopEmployerId = emp.AopEmployerId;
                    }

                    this.unitOfWork.Save();
                }
            }

            return Ok(new
            {
                err = "",
                aopApplicationId = app.AopApplicationId
            });
        }

        [Route("{id}/fed/second")]
        [HttpPost]
        public IHttpActionResult ReadFedForSecondStage(int id)
        {
            AopApp app = this.appRepository.Find(id);

            if (app.STDocId.HasValue)
            {
                Doc doc = this.docRepository.Find(app.STDocId.Value,
                    e => e.DocFiles);

                var fedFile = doc.DocFiles.FirstOrDefault(e => e.DocFileName.EndsWith(".fed"));

                if (fedFile != null)
                {
                    byte[] fedContent;

                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
                    {
                        connection.Open();

                        using (MemoryStream m1 = new MemoryStream())
                        using (var blobStream = new BlobReadStream(connection, "dbo", "Blobs", "Content", "Key", fedFile.DocFileContentId))
                        {
                            blobStream.CopyTo(m1);
                            fedContent = m1.ToArray();
                        }
                    }

                    List<NOMv5.nom> noms = FedHelper.GetFedDocumentNomenclatures();
                    List<NUTv5.item> nuts = FedHelper.GetFedDocumentNuts();

                    FEDv5.document fedDoc = XmlSerializerUtils.XmlDeserializeFromBytes<FEDv5.document>(fedContent);

                    FedExtractor.noms = noms;

                    //ND properties
                    string procType = FedExtractor.GetProcedureTypeShort(fedDoc);
                    string obj = FedExtractor.GetObject(fedDoc);
                    string criteria = FedExtractor.GetOffersCriteriaOnly(fedDoc);
                    string subject = FedExtractor.GetSubject(fedDoc);
                    string val = FedExtractor.GetPredictedValue(fedDoc);

                    var ndAopApplicationType = this.unitOfWork.DbContext.Set<AopApplicationType>()
                        .FirstOrDefault(e => e.Name.ToLower().StartsWith(procType.ToLower()));
                    if (ndAopApplicationType != null)
                    {
                        app.NDAopApplicationTypeId = ndAopApplicationType.AopApplicationTypeId;
                    }

                    var ndObject = this.unitOfWork.DbContext.Set<AopApplicationObject>()
                        .FirstOrDefault(e => e.Name.ToLower().StartsWith(obj.ToLower()));
                    if (ndObject != null)
                    {
                        app.NDObjectId = ndObject.AopApplicationObjectId;
                    }

                    var ndCriteria = this.unitOfWork.DbContext.Set<AopApplicationCriteria>()
                        .FirstOrDefault(e => e.Name.ToLower().StartsWith(criteria.ToLower()));
                    if (ndCriteria != null)
                    {
                        app.NDCriteriaId = ndCriteria.AopApplicationCriteriaId;
                    }

                    app.NDSubject = subject;
                    app.NDValue = val;

                    //възложител
                    string aopEmpUic = FedExtractor.GetEIK(fedDoc);

                    var aopEmp = this.unitOfWork.DbContext.Set<AopEmployer>()
                        .FirstOrDefault(e => e.Uic == aopEmpUic);

                    if (aopEmp != null && !app.AopEmployerId.HasValue)
                    {
                        app.AopEmployerId = aopEmp.AopEmployerId;
                    }
                    else
                    {
                        string aopEmpName = FedExtractor.GetContractor(fedDoc);
                        string aopEmpLotNum = FedExtractor.GetContractorBatch(fedDoc);

                        AopEmployerType unknownAopEmpType = this.appRepository.GetAopEmployerTypeByAlias("Unknown");

                        AopEmployer emp = appRepository.CreateAopEmployer(aopEmpName, aopEmpLotNum, aopEmpUic, unknownAopEmpType.AopEmployerTypeId);

                        this.unitOfWork.DbContext.Set<AopEmployer>().Add(emp);

                        this.unitOfWork.Save();

                        app.AopEmployerId = emp.AopEmployerId;
                    }

                    this.unitOfWork.Save();
                }
            }

            return Ok(new
            {
                err = "",
                aopApplicationId = app.AopApplicationId
            });
        }
    }
}
