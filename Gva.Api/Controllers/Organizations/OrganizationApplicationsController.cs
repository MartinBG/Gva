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
    public class OrganizationApplicationsController : GvaCaseTypePartController<DocumentApplicationDO>
    {
        private string path;
        private IUnitOfWork unitOfWork;
        private IFileRepository fileRepository;
        private IApplicationRepository applicationRepository;
        private ILotRepository lotRepository;
        private ILotEventDispatcher lotEventDispatcher;
        private UserContext userContext;

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
            this.unitOfWork = unitOfWork;
            this.fileRepository = fileRepository;
            this.applicationRepository = applicationRepository;
            this.lotRepository = lotRepository;
            this.lotEventDispatcher = lotEventDispatcher;
            this.userContext = userContext;
        }

        [Route("new")]
        public IHttpActionResult GetNewApplication(int lotId)
        {
            DocumentApplicationDO newApplication = new DocumentApplicationDO()
            {
                DocumentDate = DateTime.Now
            };

            return Ok(new CaseTypePartDO<DocumentApplicationDO>(newApplication));
        }

        public override IHttpActionResult PostNewPart(int lotId, CaseTypePartDO<DocumentApplicationDO> application)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);

                var partVersion = lot.CreatePart(path + "/*", application.Part, this.userContext);

                this.fileRepository.AddFileReference(partVersion.Part, application.Case);

                lot.Commit(this.userContext, this.lotEventDispatcher);

                GvaApplication gvaApplication = new GvaApplication()
                {
                    LotId = lot.LotId,
                    GvaAppLotPartId = partVersion.Part.PartId
                };

                this.applicationRepository.AddGvaApplication(gvaApplication);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok();
            }
        }

        public override IHttpActionResult DeletePart(int lotId, int partIndex)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);
                var partVersion = lot.DeletePart<DocumentApplicationDO>(string.Format("{0}/{1}", this.path, partIndex), this.userContext);

                this.fileRepository.DeleteFileReferences(partVersion.Part);
                this.applicationRepository.DeleteGvaApplication(partVersion.PartId);

                lot.Commit(this.userContext, lotEventDispatcher);

                this.unitOfWork.Save();
                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok();
            }
        }
    }
}