using System.Linq;
using System.Web.Http;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.Repositories.ApplicationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Gva.Api.Models;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Common.Api.Models;

namespace Gva.Api.Controllers.Aircrafts
{
    [RoutePrefix("api/aircrafts/{lotId}/aircraftCertAirworthinessesFM")]
    [Authorize]
    public class AircraftCertAirworthinessesFMController : GvaCaseTypePartController<AircraftCertAirworthinessFMDO>
    {
        private IFileRepository fileRepository;
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
            this.fileRepository = fileRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewCertAirworthinessFM(int lotId, int? appId = null)
        {
            AircraftCertAirworthinessFMDO newCertAirworthinessFM = new AircraftCertAirworthinessFMDO()
            {
                AirworthinessCertificateType = nomRepository.GetNomValue("airworthinessCertificateTypes", "f25")
            };

            GvaCaseType caseType = this.caseTypeRepository.GetCaseTypesForSet("aircraft").Single();
            CaseDO caseDO = new CaseDO()
            {
                CaseType = new NomValue()
                {
                    NomValueId = caseType.GvaCaseTypeId,
                    Name = caseType.Name,
                    Alias = caseType.Alias
                },
                BookPageNumber = this.fileRepository.GetNextBPN(lotId, caseType.GvaCaseTypeId).ToString()
            };

            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);

                caseDO.Applications.Add(this.applicationRepository.GetInitApplication(appId));
            }

            var airworthinessFMPartVersion = new CaseTypePartDO<AircraftCertAirworthinessFMDO>(newCertAirworthinessFM, caseDO);

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