using System;
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
    [RoutePrefix("api/aircrafts/{lotId}/aircraftCertRadios")]
    [Authorize]
    public class AircraftRadiosController : GvaApplicationPartController<AircraftCertRadioDO>
    {
        public AircraftRadiosController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("aircraftCertRadios", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher, userContext)
        {
        }

        [Route("new")]
        public IHttpActionResult GetNewCertRadio()
        {
            AircraftCertRadioDO newCertRadio = new AircraftCertRadioDO()
            {
                IssueDate = DateTime.Now
            };

            return Ok(new ApplicationPartVersionDO<AircraftCertRadioDO>(newCertRadio));
        }
    }
}