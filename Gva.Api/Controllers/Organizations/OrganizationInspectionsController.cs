using System.Collections.Generic;
using System.Web.Http;
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
    [RoutePrefix("api/organizations/{lotId}/organizationInspections")]
    [Authorize]
    public class OrganizationInspectionsController : GvaCaseTypePartController<OrganizationInspectionDO>
    {
        private IApplicationRepository applicationRepository;
        private ILotRepository lotRepository;
        private IOrganizationRepository organizationRepository;

        public OrganizationInspectionsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            IFileRepository fileRepository,
            ILotEventDispatcher lotEventDispatcher,
            IOrganizationRepository organizationRepository,
            UserContext userContext)
            : base("organizationInspections", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.applicationRepository = applicationRepository;
            this.lotRepository = lotRepository;
            this.organizationRepository = organizationRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewInspection(int lotId, int? appId = null)
        {

            CaseDO caseDO = null;
            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);
                caseDO = new CaseDO()
                {
                    IsAdded = true,
                    Applications = new List<ApplicationNomDO>()
                    {
                        this.applicationRepository.GetInitApplication(appId)
                    }
                };
            } 
            OrganizationInspectionDO newInspection = new OrganizationInspectionDO();

            return Ok(new CaseTypePartDO<OrganizationInspectionDO>(newInspection, caseDO));
        }

        [Route("{inspectionPartIndex}/recommendations")]
        public IHttpActionResult GetInspectionRecommendations(int lotId, int inspectionPartIndex)
        {
            return Ok(this.organizationRepository.GetInspectionRecommendations(lotId, inspectionPartIndex));
        }
    }
}