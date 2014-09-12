using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.Repositories.ApplicationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Aircrafts
{
    [RoutePrefix("api/aircrafts/{lotId}/aircraftCertSmods")]
    [Authorize]
    public class AircraftSmodsController : GvaApplicationPartController<AircraftCertSmodDO>
    {
        public AircraftSmodsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("aircraftCertSmods", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher, userContext)
        {
        }

        [Route("new")]
        public IHttpActionResult GetNewCertSmod()
        {
            return Ok(new ApplicationPartVersionDO<AircraftCertSmodDO>(new AircraftCertSmodDO()));
        }
    }
}