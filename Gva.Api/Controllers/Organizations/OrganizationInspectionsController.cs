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
    [RoutePrefix("api/organizations/{lotId}/organizationInspections")]
    [Authorize]
    public class OrganizationInspectionsController : GvaApplicationPartController<OrganizationInspectionDO>
    {
        private IApplicationRepository applicationRepository;
        private ILotRepository lotRepository;
        private IOrganizationRepository organizationRepository;

        public OrganizationInspectionsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            IOrganizationRepository organizationRepository)
            : base("organizationInspections", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher)
        {
            this.applicationRepository = applicationRepository;
            this.lotRepository = lotRepository;
            this.organizationRepository = organizationRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewInspection(int lotId, int? appId = null)
        {
            var applications = new List<ApplicationNomDO>();
            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);
                applications.Add(this.applicationRepository.GetInitApplication(appId));
            }

            OrganizationInspectionDO newInspection = new OrganizationInspectionDO();

            return Ok(new ApplicationPartVersionDO<OrganizationInspectionDO>(newInspection, applications));
        }

        [Route("{inspectionPartIndex}/recommendations")]
        public IHttpActionResult GetInspectionRecommendations(int lotId, int inspectionPartIndex)
        {
            return Ok(this.organizationRepository.GetInspectionRecommendations(lotId, inspectionPartIndex));
        }
    }
}