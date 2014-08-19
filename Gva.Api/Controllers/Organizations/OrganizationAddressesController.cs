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
    [RoutePrefix("api/organizations/{lotId}/organizationAddresses")]
    [Authorize]
    public class OrganizationAddressesController : GvaApplicationPartController<OrganizationAddressDO>
    {
        public OrganizationAddressesController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            IOrganizationRepository organizationRepository)
            : base("organizationAddresses", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher)
        {
        }

        [Route("new")]
        public IHttpActionResult GetNewAddress(int lotId)
        {
            OrganizationAddressDO newAddress = new OrganizationAddressDO();

            return Ok(new ApplicationPartVersionDO<OrganizationAddressDO>(newAddress));
        }
    }
}