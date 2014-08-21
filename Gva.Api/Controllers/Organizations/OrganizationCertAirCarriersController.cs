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
    [RoutePrefix("api/organizations/{lotId}/organizationCertAirCarriers")]
    [Authorize]
    public class OrganizationCertAirCarriersController : GvaApplicationPartController<OrganizationCertAirCarrierDO>
    {
        public OrganizationCertAirCarriersController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            IOrganizationRepository organizationRepository)
            : base("organizationCertAirCarriers", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher)
        {
        }

        [Route("new")]
        public IHttpActionResult GetNewCertAirCarrier(int lotId)
        {
            return Ok(new ApplicationPartVersionDO<OrganizationCertAirCarrierDO>(new OrganizationCertAirCarrierDO()));
        }
    }
}