using System.Collections.Generic;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Organizations
{
    [RoutePrefix("api/organizations/{lotId}/organizationStaffExaminers")]
    [Authorize]
    public class OrganizationStaffExaminersController : GvaApplicationPartController<OrganizationStaffExaminerDO>
    {
        public OrganizationStaffExaminersController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            IOrganizationRepository organizationRepository,
            UserContext userContext)
            : base("organizationStaffExaminers", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher, userContext)
        {
        }

        [Route("new")]
        public IHttpActionResult GetNewStaffExaminer(int lotId)
        {
            OrganizationStaffExaminerDO newStaffExaminer = new OrganizationStaffExaminerDO();

            return Ok(new ApplicationPartVersionDO<OrganizationStaffExaminerDO>(newStaffExaminer));
        }
    }
}