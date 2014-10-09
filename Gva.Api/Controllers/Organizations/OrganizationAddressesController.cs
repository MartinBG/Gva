using System.Web.Http;
using Common.Api.Repositories.NomRepository;
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
    [RoutePrefix("api/organizations/{lotId}/organizationAddresses")]
    [Authorize]
    public class OrganizationAddressesController : GvaSimplePartController<OrganizationAddressDO>
    {
        private INomRepository nomRepository;

        public OrganizationAddressesController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IOrganizationRepository organizationRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("organizationAddresses", unitOfWork, lotRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewAddress(int lotId)
        {
            OrganizationAddressDO newAddress = new OrganizationAddressDO();
            newAddress.Valid = this.nomRepository.GetNomValue("boolean", "yes");

            return Ok(new SimplePartDO<OrganizationAddressDO>(newAddress));
        }
    }
}