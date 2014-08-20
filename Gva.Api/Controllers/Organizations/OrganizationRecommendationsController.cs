using System.Collections.Generic;
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
    [RoutePrefix("api/organizations/{lotId}/organizationRecommendations")]
    [Authorize]
    public class OrganizationRecommendationsController : GvaApplicationPartController<OrganizationRecommendationDO>
    {
        private IApplicationRepository applicationRepository;
        private ILotRepository lotRepository;
        private IOrganizationRepository organizationRepository;

        public OrganizationRecommendationsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            IOrganizationRepository organizationRepository)
            : base("organizationRecommendations", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher)
        {
            this.applicationRepository = applicationRepository;
            this.lotRepository = lotRepository;
            this.organizationRepository = organizationRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewRecommendation(int lotId, int? appId = null)
        {
            var applications = new List<ApplicationNomDO>();
            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);
                applications.Add(this.applicationRepository.GetInitApplication(appId));
            }

            OrganizationRecommendationDO newRecommendation = new OrganizationRecommendationDO();

            return Ok(new ApplicationPartVersionDO<OrganizationRecommendationDO>(newRecommendation, applications));
        }
    }
}