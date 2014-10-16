using System;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Aircrafts
{
    [RoutePrefix("api/aircrafts/{lotId}/maintenances")]
    [Authorize]
    public class AircraftMaintenanceController : GvaCaseTypePartController<AircraftMaintenanceDO>
    {
        private ICaseTypeRepository caseTypeRepository;

        public AircraftMaintenanceController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("maintenances", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.caseTypeRepository = caseTypeRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewMaintenance()
        {
            var caseType = this.caseTypeRepository.GetCaseTypesForSet("Aircraft").Single();
            var caseDO = new CaseDO()
            {
                CaseType = new NomValue()
                {
                    NomValueId = caseType.GvaCaseTypeId,
                    Name = caseType.Name,
                    Alias = caseType.Alias
                },
                IsAdded = true
            };

            AircraftMaintenanceDO newMaintenance = new AircraftMaintenanceDO()
            {
                FromDate = DateTime.Now
            };

            return Ok(new CaseTypePartDO<AircraftMaintenanceDO>(newMaintenance, caseDO));
        }
    }
}