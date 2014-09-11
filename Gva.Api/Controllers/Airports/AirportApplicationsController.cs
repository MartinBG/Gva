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

namespace Gva.Api.Controllers.Airports
{
    [RoutePrefix("api/airports/{lotId}/airportDocumentApplications")]
    [Authorize]
    public class AirportApplicationsController : GvaFilePartController<DocumentApplicationDO>
    {
        private string path;
        private IUnitOfWork unitOfWork;
        private IFileRepository fileRepository;
        private IApplicationRepository applicationRepository;
        private ILotRepository lotRepository;
        private ILotEventDispatcher lotEventDispatcher;
        private UserContext userContext;

        public AirportApplicationsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("airportDocumentApplications", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.path = "airportDocumentApplications";
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

            return Ok(new FilePartVersionDO<DocumentApplicationDO>(newApplication));
        }

        public override IHttpActionResult PostNewPart(int lotId, FilePartVersionDO<DocumentApplicationDO> application)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);

                PartVersion partVersion = lot.CreatePart(
                    path + "/*",
                    JObject.FromObject(application.Part),
                    this.userContext);

                this.fileRepository.AddFileReferences(partVersion, application.Files);

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
                var partVersion = lot.DeletePart(string.Format("{0}/{1}", this.path, partIndex), this.userContext);

                this.fileRepository.DeleteFileReferences(partVersion);
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