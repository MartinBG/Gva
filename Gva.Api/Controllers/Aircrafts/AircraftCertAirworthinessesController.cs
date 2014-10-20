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
    [RoutePrefix("api/aircrafts/{lotId}/aircraftCertAirworthinesses")]
    [Authorize]
    public class AircraftCertAirworthinessesController : GvaCaseTypePartController<AircraftCertAirworthinessDO>
    {
        private IFileRepository fileRepository;
        private INomRepository nomRepository;
        private ICaseTypeRepository caseTypeRepository;
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;

        public AircraftCertAirworthinessesController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            INomRepository nomRepository,
            ICaseTypeRepository caseTypeRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("aircraftCertAirworthinesses", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
            this.lotRepository = lotRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.fileRepository = fileRepository;
            this.applicationRepository = applicationRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewCertAirworthiness(int lotId, int? appId = null)
        {
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

            return Ok(new CaseTypePartDO<AircraftCertAirworthinessDO>(new AircraftCertAirworthinessDO(), caseDO));
        }
    }
}