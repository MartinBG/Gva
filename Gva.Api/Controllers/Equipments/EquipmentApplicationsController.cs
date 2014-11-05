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
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Equipments
{
    [RoutePrefix("api/equipments/{lotId}/equipmentDocumentApplications")]
    [Authorize]
    public class EquipmentApplicationsController : GvaCaseTypePartController<DocumentApplicationDO>
    {
        private string path;
        private IApplicationRepository applicationRepository;

        public EquipmentApplicationsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("equipmentDocumentApplications", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.path = "equipmentDocumentApplications";
            this.applicationRepository = applicationRepository;
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            return Ok(this.applicationRepository.GetApplicationsForLot(lotId, this.path, caseTypeId));
        }
    }
}