using System.Web.Http;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.Repositories.ApplicationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;
using System;

namespace Gva.Api.Controllers.Aircrafts
{
    [RoutePrefix("api/aircrafts/{lotId}/maintenances")]
    [Authorize]
    public class AircraftMaintenanceController : GvaApplicationPartController<AircraftMaintenanceDO>
    {
        public AircraftMaintenanceController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher)
            : base("maintenances", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher) { }

        [Route("new")]
        public IHttpActionResult GetNewMaintenance()
        {
            AircraftMaintenanceDO newMaintenance = new AircraftMaintenanceDO()
            {
                FromDate = DateTime.Now
            };

            return Ok(new ApplicationPartVersionDO<AircraftMaintenanceDO>(newMaintenance));
        }
    }
}