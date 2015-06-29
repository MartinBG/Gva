using System;
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
    [RoutePrefix("api/aircrafts/{lotId}/aircraftCertNoises")]
    [Authorize]
    public class AircraftNoisesController : GvaCaseTypePartController<AircraftCertNoiseDO>
    {
        private ICaseTypeRepository caseTypeRepository;
        private IApplicationRepository applicationRepository;
        private ILotRepository lotRepository;
        private IAircraftRepository aircraftRepository;

        public AircraftNoisesController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ICaseTypeRepository caseTypeRepository,
            IAircraftRepository aircraftRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("aircraftCertNoises", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext) 
        {
            this.caseTypeRepository = caseTypeRepository;
            this.applicationRepository = applicationRepository;
            this.lotRepository = lotRepository;
            this.aircraftRepository = aircraftRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewCertNoise(int lotId, int? appId = null)
        {
            int? lastNumberPerForm45 = this.aircraftRepository.GetLastNumberPerForm(formPrefix: 45);

            AircraftCertNoiseDO newCertNoise = new AircraftCertNoiseDO()
            {
                IssueDate = DateTime.Now,
                IssueNumber = string.Format("45-{0:0000}", (lastNumberPerForm45.HasValue ? lastNumberPerForm45 + 1 : 0))
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
                caseDO.Applications.Add(this.applicationRepository.GetNomApplication(appId.Value));
            }

            return Ok(new CaseTypePartDO<AircraftCertNoiseDO>(newCertNoise, caseDO));
        }
    }
}