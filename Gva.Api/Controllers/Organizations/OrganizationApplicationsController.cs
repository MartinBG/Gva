using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Common;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;
using Common.Api.Models;

namespace Gva.Api.Controllers.Organizations
{
    [RoutePrefix("api/organizations/{lotId}/organizationDocumentApplications")]
    [Authorize]
    public class OrganizationApplicationsController : GvaCaseTypePartController<DocumentApplicationDO>
    {
        private string path;
        private IApplicationRepository applicationRepository;

        public OrganizationApplicationsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("organizationDocumentApplications", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.path = "organizationDocumentApplications";
            this.applicationRepository = applicationRepository;
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            return Ok(this.applicationRepository.GetApplicationsForLot(lotId, this.path, caseTypeId));
        }
    }
}