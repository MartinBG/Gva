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
    [RoutePrefix("api/aircrafts/{lotId}/aircraftCertNoises")]
    [Authorize]
    public class AircraftNoisesController : GvaApplicationPartController<AircraftCertNoiseDO>
    {
        public AircraftNoisesController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher)
            : base("aircraftCertNoises", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher) { }

        [Route("new")]
        public IHttpActionResult GetNewCertNoise()
        {
            AircraftCertNoiseDO newCertNoise = new AircraftCertNoiseDO()
            {
                IssueDate = DateTime.Now
            };

            return Ok(new ApplicationPartVersionDO<AircraftCertNoiseDO>(newCertNoise));
        }
    }
}