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
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;
using Gva.Api.ModelsDO.Applications;
using Common.Api.Models;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/personDocumentApplications")]
    [Authorize]
    public class PersonApplicationsController : GvaCaseTypePartController<DocumentApplicationDO>
    {
        private string path;
        private IApplicationRepository applicationRepository;

        public PersonApplicationsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personDocumentApplications", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.path = "personDocumentApplications";
            this.applicationRepository = applicationRepository;
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            return Ok(this.applicationRepository.GetApplicationsForLot(lotId, this.path, caseTypeId));
        }
    }
}