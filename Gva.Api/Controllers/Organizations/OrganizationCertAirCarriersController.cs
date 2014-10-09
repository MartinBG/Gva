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
    [RoutePrefix("api/organizations/{lotId}/organizationCertAirCarriers")]
    [Authorize]
    public class OrganizationCertAirCarriersController : GvaSimplePartController<OrganizationCertAirCarrierDO>
    {
        private INomRepository nomRepository;

        public OrganizationCertAirCarriersController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IOrganizationRepository organizationRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("organizationCertAirCarriers", unitOfWork, lotRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewCertAirCarrier(int lotId)
        {
            OrganizationCertAirCarrierDO certificate = new OrganizationCertAirCarrierDO();
            certificate.Valid = this.nomRepository.GetNomValue("boolean", "yes");

            return Ok(new SimplePartDO<OrganizationCertAirCarrierDO>(certificate));
        }
    }
}