using System.Web.Http;
using Common.Api.Repositories.NomRepository;
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
        private INomRepository nomRepository;

        public AircraftSmodsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            INomRepository nomRepository,
            UserContext userContext)
            : base("aircraftCertSmods", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewCertSmod()
        {
            AircraftCertSmodDO certificate = new AircraftCertSmodDO();
            certificate.Valid = this.nomRepository.GetNomValue("boolean", "yes");

            return Ok(new ApplicationPartVersionDO<AircraftCertSmodDO>(certificate));
        }
    }
}