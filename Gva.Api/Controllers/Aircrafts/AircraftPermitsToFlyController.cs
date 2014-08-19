using System;
using System.Web.Http;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.Repositories.ApplicationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Aircrafts
{
    [RoutePrefix("api/aircrafts/{lotId}/aircraftCertPermitsToFly")]
    [Authorize]
    public class AircraftPermitsToFlyController : GvaApplicationPartController<AircraftCertPermitToFlyDO>
    {
        public AircraftPermitsToFlyController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher)
            : base("aircraftCertPermitsToFly", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher) { }

        [Route("new")]
        public IHttpActionResult GetNewCertPermitToFly()
        {
            AircraftCertPermitToFlyDO newCertPermitToFly = new AircraftCertPermitToFlyDO()
            {
                IssueDate = DateTime.Now
            };

            return Ok(new ApplicationPartVersionDO<AircraftCertPermitToFlyDO>(newCertPermitToFly));
        }
    }
}