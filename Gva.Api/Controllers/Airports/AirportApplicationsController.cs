using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Common;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Airports
{
    [RoutePrefix("api/airports/{lotId}/airportDocumentApplications")]
    [Authorize]
    public class AirportApplicationsController : GvaCaseTypePartController<DocumentApplicationDO>
    {
        private string path;
        private IApplicationRepository applicationRepository;

        public AirportApplicationsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("airportDocumentApplications", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.path = "airportDocumentApplications";
            this.applicationRepository = applicationRepository;
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            return Ok(this.applicationRepository.GetApplicationsForLot(lotId, this.path, caseTypeId));
        }
    }

}