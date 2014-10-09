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
    [RoutePrefix("api/organizations/{lotId}/organizationCertGroundServiceOperators")]
    [Authorize]
    public class OrganizationCertGroundServiceOperatorsController : GvaSimplePartController<OrganizationCertGroundServiceOperatorDO>
    {
        private INomRepository nomRepository;

        public OrganizationCertGroundServiceOperatorsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IOrganizationRepository organizationRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("organizationCertGroundServiceOperators", unitOfWork, lotRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewCertGroundServiceOperator(int lotId)
        {
            OrganizationCertGroundServiceOperatorDO certificate = new OrganizationCertGroundServiceOperatorDO();
            certificate.Valid = this.nomRepository.GetNomValue("boolean", "yes");

            return Ok(new SimplePartDO<OrganizationCertGroundServiceOperatorDO>(certificate));
        }
    }
}