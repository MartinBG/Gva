using System.Linq;
using System.Web.Http;
using Common.Api.Models;
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
    [RoutePrefix("api/aircrafts/{lotId}/aircraftDocumentDebtsFM")]
    [Authorize]
    public class AircraftDocumentDebtsFMController : GvaCaseTypePartController<AircraftDocumentDebtFMDO>
    {
        private ICaseTypeRepository caseTypeRepository;
        private IFileRepository fileRepository;
        private IApplicationRepository applicationRepository;
        private ILotRepository lotRepository;
        private IAircraftDocumentDebtFMRepository aircraftDocumentDebtFMRepository;

        public AircraftDocumentDebtsFMController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher,
            IAircraftDocumentDebtFMRepository aircraftDocumentDebtFMRepository,
            UserContext userContext)
            : base("aircraftDocumentDebtsFM", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.caseTypeRepository = caseTypeRepository;
            this.fileRepository = fileRepository;
            this.applicationRepository = applicationRepository;
            this.lotRepository = lotRepository;
            this.aircraftDocumentDebtFMRepository = aircraftDocumentDebtFMRepository;
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            var documentDebts = this.aircraftDocumentDebtFMRepository.GetDocumentDebts(lotId: lotId, caseTypeId: caseTypeId);

            return Ok(documentDebts);
        }

        [Route("new")]
        public IHttpActionResult GetNewDocumentDebtFM (int lotId, int? appId = null)
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

            return Ok(new CaseTypePartDO<AircraftDocumentDebtFMDO>(new AircraftDocumentDebtFMDO(), caseDO));
        }
    }
}