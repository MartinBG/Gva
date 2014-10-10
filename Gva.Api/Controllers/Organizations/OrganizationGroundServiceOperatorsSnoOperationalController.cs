using System.Web.Http;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Organizations
{
    [RoutePrefix("api/organizations/{lotId}/organizationGroundServiceOperatorsSnoOperational")]
    [Authorize]
    public class OrganizationGroundServiceOperatorsSnoOperationalController : GvaCaseTypePartController<OrganizationGroundServiceOperatorsSnoOperationalDO>
    {
        private INomRepository nomRepository;

        public OrganizationGroundServiceOperatorsSnoOperationalController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IOrganizationRepository organizationRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("organizationGroundServiceOperatorsSnoOperational", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewGroundServiceOperatorsSnoOperational(int lotId)
        {
            OrganizationGroundServiceOperatorsSnoOperationalDO certificate = new OrganizationGroundServiceOperatorsSnoOperationalDO();
            certificate.Valid = this.nomRepository.GetNomValue("boolean", "yes");

            return Ok(new CaseTypePartDO<OrganizationGroundServiceOperatorsSnoOperationalDO>(certificate));
        }
    }
}