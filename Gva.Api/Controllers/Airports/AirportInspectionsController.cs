using System;
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
    [RoutePrefix("api/airports/{lotId}/inspections")]
    [Authorize]
    public class AirportInspectionsController : GvaCaseTypePartController<AirportInspectionDO>
    {
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;
        private ICaseTypeRepository caseTypeRepository;

        public AirportInspectionsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            IFileRepository fileRepository,
            ILotEventDispatcher lotEventDispatcher,
            ICaseTypeRepository caseTypeRepository,
            UserContext userContext)
            : base("inspections", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
            this.caseTypeRepository = caseTypeRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewInspection(int lotId, int? appId = null)
        {
            GvaCaseType caseType = this.caseTypeRepository.GetCaseTypesForSet("airport").Single();

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

            AirportInspectionDO newInspection = new AirportInspectionDO()
            {
                StartDate = DateTime.Now
            };

            return Ok(new CaseTypePartDO<AirportInspectionDO>(newInspection, caseDO));
        }
    }
}