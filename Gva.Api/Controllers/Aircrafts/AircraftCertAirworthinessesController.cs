using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.Repositories.AircraftRepository;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Aircrafts
{
    [RoutePrefix("api/aircrafts/{lotId}/aircraftCertAirworthinessesFM")]
    [Authorize]
    public class AircraftCertAirworthinessesController : GvaCaseTypePartController<AircraftCertAirworthinessDO>
    {
        private INomRepository nomRepository;
        private ICaseTypeRepository caseTypeRepository;
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;
        private IAircraftRegistrationRepository aircraftRegistrationRepository;
        private IAircraftRepository aircraftRepository;

        public AircraftCertAirworthinessesController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ICaseTypeRepository caseTypeRepository,
            IFileRepository fileRepository,
            ILotEventDispatcher lotEventDispatcher,
            INomRepository nomRepository,
            IAircraftRegistrationRepository aircraftRegistrationRepository,
            IAircraftRepository aircraftRepository,
            UserContext userContext)
            : base("aircraftCertAirworthinessesFM", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
            this.lotRepository = lotRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.applicationRepository = applicationRepository;
            this.aircraftRegistrationRepository = aircraftRegistrationRepository;
            this.aircraftRepository = aircraftRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewCertAirworthinessFM(int lotId, int? appId = null)
        {
            var lastReg = this.aircraftRegistrationRepository.GetAircraftRegistrationNoms(lotId).FirstOrDefault();
            int? lastNumberPerForm25 = this.aircraftRepository.GetLastNumberPerForm(formPrefix: 25);
            AircraftCertAirworthinessDO newCertAirworthinessFM = new AircraftCertAirworthinessDO()
            {
                AirworthinessCertificateType = this.nomRepository.GetNomValue("airworthinessCertificateTypes", "f25"),
                Registration = lastReg,
                DocumentNumber = string.Format("25-{0:0000}", (lastNumberPerForm25.HasValue ? lastNumberPerForm25 + 1 : 0))
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

            return Ok(new CaseTypePartDO<AircraftCertAirworthinessDO>(newCertAirworthinessFM, caseDO));
        }
    }
}