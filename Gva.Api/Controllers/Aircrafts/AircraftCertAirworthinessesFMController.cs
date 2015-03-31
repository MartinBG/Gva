using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Aircrafts
{
    [RoutePrefix("api/aircrafts/{lotId}/aircraftCertAirworthinessesFM")]
    [Authorize]
    public class AircraftCertAirworthinessesFMController : GvaCaseTypePartController<AircraftCertAirworthinessFMDO>
    {
        private INomRepository nomRepository;
        private ICaseTypeRepository caseTypeRepository;
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;

        public AircraftCertAirworthinessesFMController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ICaseTypeRepository caseTypeRepository,
            IFileRepository fileRepository,
            ILotEventDispatcher lotEventDispatcher,
            INomRepository nomRepository,
            UserContext userContext)
            : base("aircraftCertAirworthinessesFM", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
            this.lotRepository = lotRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.applicationRepository = applicationRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewCertAirworthinessFM(int lotId, int? appId = null)
        {
            AircraftCertAirworthinessFMDO newCertAirworthinessFM = new AircraftCertAirworthinessFMDO()
            {
                AirworthinessCertificateType = this.nomRepository.GetNomValue("airworthinessCertificateTypes", "f25")
            };

            GvaCaseType caseType = this.caseTypeRepository.GetCaseTypesForSet("aircraft").Single();
            CaseDO caseDO = new CaseDO()
            {
                CaseType = new NomValue()
                {
                    NomValueId = caseType.GvaCaseTypeId,
                    Name = caseType.Name,
                    Alias = caseType.Alias
                }
            };

            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);

                caseDO.Applications.Add(this.applicationRepository.GetInitApplication(appId));
            }

            var airworthinessFMPartVersion = new CaseTypePartDO<AircraftCertAirworthinessFMDO>(newCertAirworthinessFM, caseDO);

            var review = new AircraftCertAirworthinessReviewDO()
            {
                Inspector = new AircraftInspectorDO()
            };

            return Ok(new
                {
                    airworthinessFMPartVersion,
                    review
                });
        }
    }
}