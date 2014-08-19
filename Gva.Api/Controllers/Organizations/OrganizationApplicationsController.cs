using System;
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

namespace Gva.Api.Controllers.Organizations
{
    [RoutePrefix("api/organizations/{lotId}/organizationDocumentApplications")]
    [Authorize]
    public class OrganizationApplicationsController : GvaFilePartController<DocumentApplicationDO>
    {
        private string path;
        private IUnitOfWork unitOfWork;
        private IFileRepository fileRepository;
        private IApplicationRepository applicationRepository;
        private ILotRepository lotRepository;
        private ILotEventDispatcher lotEventDispatcher;

        public OrganizationApplicationsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher)
            : base("organizationDocumentApplications", unitOfWork, lotRepository, fileRepository, lotEventDispatcher)
        {
            this.path = "organizationDocumentApplications";
            this.unitOfWork = unitOfWork;
            this.fileRepository = fileRepository;
            this.applicationRepository = applicationRepository;
            this.lotRepository = lotRepository;
            this.lotEventDispatcher = lotEventDispatcher;
        }

        [Route("new")]
        public IHttpActionResult GetNewApplication(int lotId)
        {
            DocumentApplicationDO newApplication = new DocumentApplicationDO()
            {
                DocumentDate = DateTime.Now
            };

            return Ok(new FilePartVersionDO<DocumentApplicationDO>(newApplication));
        }

        public override IHttpActionResult PostNewPart(int lotId, FilePartVersionDO<DocumentApplicationDO> application)
        {
            UserContext userContext = this.Request.GetUserContext();
            var lot = this.lotRepository.GetLotIndex(lotId);

            PartVersion partVersion = lot.CreatePart(
                path + "/*",
                JObject.FromObject(application.Part),
                userContext);

            this.fileRepository.AddFileReferences(partVersion, application.Files);

            lot.Commit(userContext, this.lotEventDispatcher);

            GvaApplication gvaApplication = new GvaApplication()
            {
                LotId = lot.LotId,
                GvaAppLotPartId = partVersion.Part.PartId
            };

            this.applicationRepository.AddGvaApplication(gvaApplication);

            this.unitOfWork.Save();

            return Ok();
        }

        public override IHttpActionResult DeletePart(int lotId, int partIndex)
        {
            UserContext userContext = this.Request.GetUserContext();
            var lot = this.lotRepository.GetLotIndex(lotId);
            var partVersion = lot.DeletePart(string.Format("{0}/{1}", this.path, partIndex), userContext);

            this.fileRepository.DeleteFileReferences(partVersion);
            this.applicationRepository.DeleteGvaApplication(partVersion.PartId);

            lot.Commit(userContext, lotEventDispatcher);

            this.unitOfWork.Save();

            return Ok();
        }
    }
}