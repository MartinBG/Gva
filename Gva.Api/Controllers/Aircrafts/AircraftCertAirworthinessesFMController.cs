using System.Web.Http;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.Repositories.ApplicationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;

namespace Gva.Api.Controllers.Aircrafts
{
    [RoutePrefix("api/aircrafts/{lotId}/aircraftCertAirworthinessesFM")]
    [Authorize]
    public class AircraftCertAirworthinessesFMController : GvaApplicationPartController<AircraftCertAirworthinessFMDO>
    {
        private INomRepository nomRepository;

        public AircraftCertAirworthinessesFMController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            INomRepository nomRepository,
            UserContext userContext)
            : base("aircraftCertAirworthinessesFM", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewCertAirworthinessFM()
        {
            AircraftCertAirworthinessFMDO newCertAirworthinessFM = new AircraftCertAirworthinessFMDO()
            {
                AirworthinessCertificateType = nomRepository.GetNomValue("airworthinessCertificateTypes", "f25")
            };

            var airworthinessFMPartVersion = new ApplicationPartVersionDO<AircraftCertAirworthinessFMDO>(newCertAirworthinessFM);

            var reviewForm15 = new AircraftCertAirworthinessForm15MainDO()
            {
                AirworthinessReviewType = nomRepository.GetNomValue("airworthinessReviewTypes", "15a"),
                Inspector = new AircraftInspectorDO()
            };

            var reviewOther = new AircraftCertAirworthinessReviewOtherDO()
            {
                Inspector = new AircraftInspectorDO()
            };

            return Ok(new
                {
                    airworthinessFMPartVersion,
                    reviewForm15,
                    reviewOther
                });
        }
    }
}