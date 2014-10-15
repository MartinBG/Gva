using System.Web.Http;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.Repositories.ApplicationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;
using System;
using Common.Api.UserContext;

namespace Gva.Api.Controllers.Aircrafts
{
    [RoutePrefix("api/aircrafts/{lotId}/maintenances")]
    [Authorize]
    public class AircraftMaintenanceController : GvaSimplePartController<AircraftMaintenanceDO>
    {
        public AircraftMaintenanceController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("maintenances", unitOfWork, lotRepository, lotEventDispatcher, userContext)
        {
        }

        [Route("new")]
        public IHttpActionResult GetNewMaintenance()
        {
            AircraftMaintenanceDO newMaintenance = new AircraftMaintenanceDO()
            {
                FromDate = DateTime.Now
            };

            return Ok(new SimplePartDO<AircraftMaintenanceDO>(newMaintenance));
        }
    }
}