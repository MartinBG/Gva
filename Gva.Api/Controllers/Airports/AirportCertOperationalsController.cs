using System.Web.Http;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Airports;
using Gva.Api.Repositories.ApplicationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Airports
{
    [RoutePrefix("api/airports/{lotId}/airportCertOperationals")]
    [Authorize]
    public class AirportCertOperationalsController : GvaApplicationPartController<AirportCertOperationalDO>
    {
        public AirportCertOperationalsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher)
            : base("airportCertOperationals", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher)
        {
        }

        [Route("new")]
        public IHttpActionResult GetNewCertOperational(int lotId)
        {
            AirportCertOperationalDO newCertOperational = new AirportCertOperationalDO();

            return Ok(new ApplicationPartVersionDO<AirportCertOperationalDO>(newCertOperational));
        }
    }
}