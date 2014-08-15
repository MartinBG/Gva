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
    [RoutePrefix("api/organizations/{lotId}/organizationAuditplans")]
    [Authorize]
    public class OrganizationAuditplansController : GvaApplicationPartController<OrganizationAuditplanDO>
    {
        public OrganizationAuditplansController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            IOrganizationRepository organizationRepository)
            : base("organizationAuditplans", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher)
        {
        }

        [Route("new")]
        public IHttpActionResult GetNewAuditplan(int lotId)
        {
            OrganizationAuditplanDO newAuditplan = new OrganizationAuditplanDO();

            return Ok(new ApplicationPartVersionDO<OrganizationAuditplanDO>(newAuditplan));
        }
    }
}