using System;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Organizations
{
    [RoutePrefix("api/organizations/{lotId}/organizationDocumentOthers")]
    [Authorize]
    public class OrganizationDocumentOthersController : GvaCaseTypePartController<OrganizationDocumentOtherDO>
    {
        public OrganizationDocumentOthersController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("organizationDocumentOthers", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        { }

        [Route("new")]
        public IHttpActionResult GetNewDocumentOther(int lotId)
        {
            OrganizationDocumentOtherDO newDocumentOther = new OrganizationDocumentOtherDO()
            {
                DocumentDateValidFrom = DateTime.Now
            };

            return Ok(new CaseTypePartDO<OrganizationDocumentOtherDO>(newDocumentOther));
        }
    }
}