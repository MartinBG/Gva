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
    [RoutePrefix("api/organizations/{lotId}/organizationCertAirOperators")]
    [Authorize]
    public class OrganizationCertAirOperatorsController : GvaApplicationPartController<OrganizationCertAirOperatorDO>
    {
        public OrganizationCertAirOperatorsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            IOrganizationRepository organizationRepository)
            : base("organizationCertAirOperators", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher)
        {
        }

        [Route("new")]
        public IHttpActionResult GetNewCertAirOperator(int lotId)
        {
            return Ok(new ApplicationPartVersionDO<OrganizationCertAirOperatorDO>(new OrganizationCertAirOperatorDO()));
        }
    }
}