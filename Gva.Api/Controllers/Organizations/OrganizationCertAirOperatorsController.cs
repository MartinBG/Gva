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
    [RoutePrefix("api/organizations/{lotId}/organizationCertAirOperators")]
    [Authorize]
    public class OrganizationCertAirOperatorsController : GvaApplicationPartController<OrganizationCertAirOperatorDO>
    {
        private INomRepository nomRepository;

        public OrganizationCertAirOperatorsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            IOrganizationRepository organizationRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("organizationCertAirOperators", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewCertAirOperator(int lotId)
        {
            OrganizationCertAirOperatorDO certificate = new OrganizationCertAirOperatorDO();
            certificate.Valid = this.nomRepository.GetNomValue("boolean", "yes");

            return Ok(new ApplicationPartVersionDO<OrganizationCertAirOperatorDO>(certificate));
        }
    }
}