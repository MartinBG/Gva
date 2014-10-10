using System.Collections.Generic;
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
    [RoutePrefix("api/organizations/{lotId}/organizationStaffManagement")]
    [Authorize]
    public class OrganizationStaffManagementController : GvaCaseTypePartController<OrganizationStaffManagementDO>
    {
        private IApplicationRepository applicationRepository;
        private ILotRepository lotRepository;
        private INomRepository nomRepository;
        private IFileRepository fileRepository;

        public OrganizationStaffManagementController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            IFileRepository fileRepository,
            IOrganizationRepository organizationRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("organizationStaffManagement", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
            this.nomRepository = nomRepository;
            this.fileRepository = fileRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewStaffManagement(int lotId, int? appId = null)
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

            OrganizationStaffManagementDO newStaffManagement = new OrganizationStaffManagementDO();
            newStaffManagement.Valid = this.nomRepository.GetNomValue("boolean", "yes");

            return Ok(new CaseTypePartDO<OrganizationStaffManagementDO>(newStaffManagement, caseDO));
        }
    }
}