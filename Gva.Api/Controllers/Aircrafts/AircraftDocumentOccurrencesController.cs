using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
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
    [RoutePrefix("api/aircrafts/{lotId}/documentOccurrences")]
    [Authorize]
    public class AircraftDocumentOccurrencesController : GvaCaseTypePartController<AircraftDocumentOccurenceDO>
    {
        private ICaseTypeRepository caseTypeRepository;
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;

        public AircraftDocumentOccurrencesController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("documentOccurrences", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.caseTypeRepository = caseTypeRepository;
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewDocumentOccurrence (int lotId, int? appId = null)
        {
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
                caseDO.IsAdded = true;
                caseDO.Applications = new List<ApplicationNomDO>()
                {
                    this.applicationRepository.GetNomApplication(appId.Value)
                };
            }

            return Ok(new CaseTypePartDO<AircraftDocumentOccurenceDO>(new AircraftDocumentOccurenceDO(), caseDO));
        }
    }
}