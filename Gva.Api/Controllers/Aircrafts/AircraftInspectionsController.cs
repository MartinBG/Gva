using System.Web.Http;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.Repositories.ApplicationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;
using System.Collections.Generic;
using Common.Api.UserContext;

namespace Gva.Api.Controllers.Aircrafts
{
    [RoutePrefix("api/aircrafts/{lotId}/inspections")]
    [Authorize]
    public class AircraftInspectionsController : GvaApplicationPartController<AircraftInspectionDO>
    {
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;

        public AircraftInspectionsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("inspections", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher, userContext)
        {
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewInspection(int lotId, int? appId = null)
        {
            var applications = new List<ApplicationNomDO>();
            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);
                applications.Add(this.applicationRepository.GetInitApplication(appId));
            }

            return Ok(new ApplicationPartVersionDO<AircraftInspectionDO>(new AircraftInspectionDO(), applications));
        }
    }
}