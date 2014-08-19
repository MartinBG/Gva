using System;
using System.Collections.Generic;
using System.Web.Http;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Equipments;
using Gva.Api.Repositories.ApplicationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Equipments
{
    [RoutePrefix("api/equipments/{lotId}/inspections")]
    [Authorize]
    public class EquipmentInspectionsController : GvaApplicationPartController<EquipmentInspectionDO>
    {
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;

        public EquipmentInspectionsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher)
            : base("inspections", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher)
        {
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewInspection(int lotId, int? appId = null)
        {
            var applications = new List<ApplicationNomDO>();
            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);
                applications.Add(this.applicationRepository.GetInitApplication(appId));
            }

            EquipmentInspectionDO newInspection = new EquipmentInspectionDO()
            {
                StartDate = DateTime.Now
            };

            return Ok(new ApplicationPartVersionDO<EquipmentInspectionDO>(newInspection, applications));
        }
    }
}