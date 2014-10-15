using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Airports;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Airports
{
    [RoutePrefix("api/airports/{lotId}/airportDocumentOwners")]
    [Authorize]
    public class AirportOwnersController : GvaCaseTypePartController<AirportOwnerDO>
    {
        private IApplicationRepository applicationRepository;
        private ILotRepository lotRepository;
        private IFileRepository fileRepository;
        private ICaseTypeRepository caseTypeRepository;

        public AirportOwnersController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("airportDocumentOwners", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.applicationRepository = applicationRepository;
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
            this.caseTypeRepository = caseTypeRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewOwner(int lotId, int? appId = null)
        {
            GvaCaseType caseType = this.caseTypeRepository.GetCaseTypesForSet("airport").Single();
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

            AirportOwnerDO newOwner = new AirportOwnerDO();

            return Ok(new CaseTypePartDO<AirportOwnerDO>(newOwner, caseDO));
        }
    }
}