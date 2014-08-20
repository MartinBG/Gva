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
    [RoutePrefix("api/organizations/{lotId}/organizationCertGroundServiceOperators")]
    [Authorize]
    public class OrganizationCertGroundServiceOperatorsController : GvaApplicationPartController<OrganizationCertGroundServiceOperatorDO>
    {
        public OrganizationCertGroundServiceOperatorsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            IOrganizationRepository organizationRepository)
            : base("organizationCertGroundServiceOperators", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher)
        {
        }

        [Route("new")]
        public IHttpActionResult GetNewCertGroundServiceOperator(int lotId)
        {
            return Ok(new ApplicationPartVersionDO<OrganizationCertGroundServiceOperatorDO>(new OrganizationCertGroundServiceOperatorDO()));
        }
    }
}