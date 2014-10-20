using System.Web.Http;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using System.Linq;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;
using Gva.Api.Models;
using Common.Api.Models;

namespace Gva.Api.Controllers.Aircrafts
{
    [RoutePrefix("api/aircrafts/{lotId}/aircraftCertSmods")]
    [Authorize]
    public class AircraftSmodsController : GvaCaseTypePartController<AircraftCertSmodDO>
    {
        private INomRepository nomRepository;
        private ICaseTypeRepository caseTypeRepository;
        private IFileRepository fileRepository;
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;

        public AircraftSmodsController(
            ICaseTypeRepository caseTypeRepository,
            IApplicationRepository applicationRepository,
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            ILotEventDispatcher lotEventDispatcher,
            INomRepository nomRepository,
            UserContext userContext)
            : base("aircraftCertSmods", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.fileRepository = fileRepository;
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewCertSmod(int lotId, int? appId = null)
        {
            AircraftCertSmodDO certificate = new AircraftCertSmodDO();
            certificate.Valid = this.nomRepository.GetNomValue("boolean", "yes");
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

            return Ok(new CaseTypePartDO<AircraftCertSmodDO>(certificate, caseDO));
        }
    }
}