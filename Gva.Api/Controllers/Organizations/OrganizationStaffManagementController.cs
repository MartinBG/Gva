using System.Web.Http;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Organizations
{
    [RoutePrefix("api/organizations/{lotId}/organizationStaffManagement")]
    [Authorize]
    public class OrganizationStaffManagementController : GvaCaseTypePartController<OrganizationStaffManagementDO>
    {
        private INomRepository nomRepository;

        public OrganizationStaffManagementController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("organizationStaffManagement", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewStaffManagement(int lotId)
        {
            OrganizationStaffManagementDO newStaffManagement = new OrganizationStaffManagementDO();
            newStaffManagement.Valid = this.nomRepository.GetNomValue("boolean", "yes");

            return Ok(new CaseTypePartDO<OrganizationStaffManagementDO>(newStaffManagement));
        }
    }
}