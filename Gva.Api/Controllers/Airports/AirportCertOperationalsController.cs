using System.Web.Http;
using Common.Api.UserContext;
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
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("airportCertOperationals", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher, userContext)
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