using Aop.Api.DataObjects;
using Aop.Api.Models;
using Aop.Api.Repositories.Aop;
using Aop.Api.WordTemplates;
using Common.Api.Repositories.UserRepository;
using Common.Api.UserContext;
using Common.Data;
using Docs.Api.DataObjects;
using Docs.Api.Models;
using Docs.Api.Repositories.DocRepository;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;

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
    }
}
