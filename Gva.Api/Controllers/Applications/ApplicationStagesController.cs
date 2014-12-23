using System;
using System.Linq;
using System.Web.Http;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Applications;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.ApplicationStageRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Applications
{
    [RoutePrefix("api/apps/{appId}/stages")]
    [Authorize]
    public class ApplicationStagesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IApplicationStageRepository applicationStageRepository;
        private IApplicationRepository applicationRepository;
        private ILotRepository lotRepository;

        public ApplicationStagesController(
            IUnitOfWork unitOfWork,
            IApplicationStageRepository applicationStageRepository,
            IApplicationRepository applicationRepository,
            ILotRepository lotRepository)
        {
            this.unitOfWork = unitOfWork;
            this.applicationStageRepository = applicationStageRepository;
            this.applicationRepository = applicationRepository;
            this.lotRepository = lotRepository;
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetApplicationStages(int appId)
        {
            var applicationStages = this.applicationStageRepository.GetApplicationStages(appId);

            return Ok(applicationStages.Select(a => new ApplicationStageDO(a)));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult PostNewApplicationStage(int appId, ApplicationStageDO appStage)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var stageTermDate = this.applicationStageRepository.GetApplicationTermDate(appId, appStage.StageId);
                GvaStage gvaStage = this.unitOfWork.DbContext.Set<GvaStage>().Find(appStage.StageId);

                GvaApplicationStage stage = new GvaApplicationStage()
                {
                    GvaApplicationId = appId,
                    GvaStageId = gvaStage.GvaStageId,
                    StartingDate = appStage.Date,
                    InspectorLotId = appStage.InspectorId,
                    Ordinal = appStage.Ordinal,
                    Note = appStage.Note,
                    StageTermDate = stageTermDate
                };

                GvaApplicationStage applicationStage = this.unitOfWork.DbContext.Set<GvaApplicationStage>().Add(stage);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(applicationStage);
            }
        }

        [Route("{stageId}")]
        [HttpGet]
        public IHttpActionResult GetApplicationStage(int appId, int stageId)
        {
            return Ok(new ApplicationStageDO(this.applicationStageRepository.GetApplicationStage(appId, stageId)));
        }

        [Route("{stageId}")]
        [HttpPost]
        public IHttpActionResult PostApplicationStage(int appId, int stageId, ApplicationStageDO appStage)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                GvaApplicationStage applicationStage = this.unitOfWork.DbContext.Set<GvaApplicationStage>().Find(stageId);
                var stageTermDate = this.applicationStageRepository.GetApplicationTermDate(appId, appStage.StageId);

                if (applicationStage != null)
                {
                    applicationStage.GvaStageId = appStage.StageId;
                    applicationStage.StartingDate = appStage.Date;
                    applicationStage.InspectorLotId = appStage.InspectorId;
                    applicationStage.Ordinal = appStage.Ordinal;
                    applicationStage.Note = appStage.Note;
                    applicationStage.StageTermDate = stageTermDate;
                }

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(applicationStage);
            }
        }

        [Route("{stageId}")]
        [HttpDelete]
        public IHttpActionResult DeleteApplicationStage(int appId, int stageId)
        {
            var appStage = this.unitOfWork.DbContext.Set<GvaApplicationStage>()
                .Where(e => e.GvaAppStageId == stageId)
                .SingleOrDefault();

            var applicationStage = this.unitOfWork.DbContext.Set<GvaApplicationStage>()
                .Remove(appStage);

            this.unitOfWork.Save();

            return Ok(applicationStage);
        }
    }
}