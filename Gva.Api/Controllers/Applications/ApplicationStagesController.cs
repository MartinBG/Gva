using System;
using System.Linq;
using System.Web.Http;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Applications;
using Gva.Api.Repositories.ApplicationStageRepository;
using Newtonsoft.Json.Linq;

namespace Gva.Api.Controllers.Applications
{
    [RoutePrefix("api/apps/{appId}/stages")]
    [Authorize]
    public class ApplicationStagesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IApplicationStageRepository applicationStageRepository;

        public ApplicationStagesController(
            IUnitOfWork unitOfWork,
            IApplicationStageRepository applicationStageRepository)
        {
            this.unitOfWork = unitOfWork;
            this.applicationStageRepository = applicationStageRepository;
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
        public IHttpActionResult PostNewApplicationStage(int appId, JObject appStage)
        {
            GvaApplicationStage stage = new GvaApplicationStage()
            {
                GvaApplicationId = appId,
                GvaStageId = appStage.Get<int>("stageId"),
                StartingDate = appStage.Get<DateTime>("date"),
                InspectorLotId = appStage.Get<int?>("inspectorId"),
                Ordinal = appStage.Get<int>("ordinal"),
                Note = appStage.Get<string>("note")
            };

            var applicationStage = this.unitOfWork.DbContext.Set<GvaApplicationStage>().Add(stage);

            this.unitOfWork.Save();

            return Ok(applicationStage);
        }

        [Route("{stageId}")]
        [HttpGet]
        public IHttpActionResult GetApplicationStage(int appId, int stageId)
        {
            return Ok(new ApplicationStageDO(this.applicationStageRepository.GetApplicationStage(appId, stageId)));
        }

        [Route("{stageId}")]
        [HttpPost]
        public IHttpActionResult PostApplicationStage(int appId, int stageId, JObject appStage)
        {
            var applicationStage = this.unitOfWork.DbContext.Set<GvaApplicationStage>().Find(stageId);
            if (applicationStage != null)
            {
                applicationStage.GvaStageId = appStage.Get<int>("stageId");
                applicationStage.StartingDate = appStage.Get<DateTime>("date");
                applicationStage.InspectorLotId = appStage.Get<int?>("inspectorId");
                applicationStage.Ordinal = appStage.Get<int>("ordinal");
                applicationStage.Note = appStage.Get<string>("note");
            }

            this.unitOfWork.Save();

            return Ok(applicationStage);
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