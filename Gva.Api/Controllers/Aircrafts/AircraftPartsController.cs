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
    [RoutePrefix("api/aircrafts/{lotId}/aircraftParts")]
    [Authorize]
    public class AircraftPartsController : GvaSimplePartController<AircraftPartDO>
    {
        public AircraftPartsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("aircraftParts", unitOfWork, lotRepository, lotEventDispatcher, userContext)
        {
        }

        [Route("new")]
        public IHttpActionResult GetNewPart()
        {
            return Ok(new SimplePartDO<AircraftPartDO>(new AircraftPartDO()));
        }
    }
}