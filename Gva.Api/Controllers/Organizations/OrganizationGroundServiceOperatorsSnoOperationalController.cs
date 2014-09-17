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
    [RoutePrefix("api/organizations/{lotId}/organizationGroundServiceOperatorsSnoOperational")]
    [Authorize]
    public class OrganizationGroundServiceOperatorsSnoOperationalController : GvaApplicationPartController<OrganizationGroundServiceOperatorsSnoOperationalDO>
    {
        private INomRepository nomRepository;

        public OrganizationGroundServiceOperatorsSnoOperationalController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            IOrganizationRepository organizationRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("organizationGroundServiceOperatorsSnoOperational", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewGroundServiceOperatorsSnoOperational(int lotId)
        {
            OrganizationGroundServiceOperatorsSnoOperationalDO certificate = new OrganizationGroundServiceOperatorsSnoOperationalDO();
            certificate.Valid = this.nomRepository.GetNomValue("boolean", "yes");

            return Ok(new ApplicationPartVersionDO<OrganizationGroundServiceOperatorsSnoOperationalDO>(certificate));
        }
    }
}