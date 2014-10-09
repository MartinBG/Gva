using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.Models.Views.Organization;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Organizations
{
    [RoutePrefix("api/organizations/{lotId}/organizationRecommendations")]
    [Authorize]
    public class OrganizationRecommendationsController : GvaSimplePartController<OrganizationRecommendationDO>
    {
        private ILotRepository lotRepository;
        private IOrganizationRepository organizationRepository;
        private IUnitOfWork unitOfWork;

        public OrganizationRecommendationsController(
            IUnitOfWork unitOfWork,
            IOrganizationRepository organizationRepository,
            ILotRepository lotRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("organizationRecommendations", unitOfWork, lotRepository, lotEventDispatcher, userContext)
        {
            this.lotRepository = lotRepository;
            this.unitOfWork = unitOfWork;
            this.organizationRepository = organizationRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewRecommendation(int lotId)
        {
            OrganizationRecommendationDO newRecommendation = new OrganizationRecommendationDO();

            return Ok(new SimplePartDO<OrganizationRecommendationDO>(newRecommendation));
        }

        [Route("views")]
        public IHttpActionResult GetRecommendations(int lotId)
        {
            return Ok(this.organizationRepository.GetRecommendations(lotId));
        }
    }
}