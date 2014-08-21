using System.Web.Http;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;
using System.Collections.Generic;

namespace Gva.Api.Controllers.Organizations
{
    [RoutePrefix("api/organizations/{lotId}/organizationApprovals")]
    [Authorize]
    public class OrganizationApprovalsController : GvaApplicationPartController<OrganizationApprovalDO>
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;
        private ILotEventDispatcher lotEventDispatcher;

        public OrganizationApprovalsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            IOrganizationRepository organizationRepository)
            : base("organizationApprovals", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
            this.lotEventDispatcher = lotEventDispatcher;
        }

        [Route("new")]
        public IHttpActionResult GetNewApproval(int lotId, int? appId = null)
        {
            var applications = new List<ApplicationNomDO>();
            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);
                applications.Add(this.applicationRepository.GetInitApplication(appId));
            }

            OrganizationApprovalDO newApproval = new OrganizationApprovalDO()
            {
                Amendments = new List<AmendmentDO>
                {
                    new AmendmentDO()
                    {
                        Applications = applications
                    }
                }
            };

            return Ok(new ApplicationPartVersionDO<OrganizationApprovalDO>(newApproval));
        }

        [Route("{partIndex}/newAmendment")]
        public IHttpActionResult GetNewApprovalAmendment(int lotId, int partIndex, int? appId = null)
        {
            var applications = new List<ApplicationNomDO>();
            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);
                applications.Add(this.applicationRepository.GetInitApplication(appId));
            }

            AmendmentDO newApprovalAmendment = new AmendmentDO()
            {
                Applications = applications
            };

            return Ok(newApprovalAmendment);
        }
    }
}