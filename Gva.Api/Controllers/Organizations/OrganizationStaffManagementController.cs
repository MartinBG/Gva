using System.Collections.Generic;
using System.Web.Http;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Organizations
{
    [RoutePrefix("api/organizations/{lotId}/organizationStaffManagement")]
    [Authorize]
    public class OrganizationStaffManagementController : GvaApplicationPartController<OrganizationStaffManagementDO>
    {
        private IApplicationRepository applicationRepository;
        private ILotRepository lotRepository;

        public OrganizationStaffManagementController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            IOrganizationRepository organizationRepository)
            : base("organizationStaffManagement", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher)
        {
            this.applicationRepository = applicationRepository;
            this.lotRepository = lotRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewStaffManagement(int lotId, int? appId = null)
        {
            var applications = new List<ApplicationNomDO>();
            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);
                applications.Add(this.applicationRepository.GetInitApplication(appId));
            }

            OrganizationStaffManagementDO newStaffManagement = new OrganizationStaffManagementDO();

            return Ok(new ApplicationPartVersionDO<OrganizationStaffManagementDO>(newStaffManagement, applications));
        }
    }
}