using System;
using System.Collections.Generic;
using System.Web.Http;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;
using Gva.Api.ModelsDO.Common;
using Newtonsoft.Json.Linq;
using Common.Api.UserContext;
using Regs.Api.Models;
using Gva.Api.Models;

namespace Gva.Api.Controllers.Aircrafts
{
    [RoutePrefix("api/aircrafts/{lotId}/aircraftDocumentApplications")]
    [Authorize]
    public class AircraftDocumentApplicationsController : GvaFilePartController<DocumentApplicationDO>
    {
        private string path;
        private ILotRepository lotRepository;
        private IFileRepository fileRepository;
        private IApplicationRepository applicationRepository;
        private IUnitOfWork unitOfWork;
        private ILotEventDispatcher lotEventDispatcher;

        public AircraftDocumentApplicationsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher)
            : base("aircraftDocumentApplications", unitOfWork, lotRepository, fileRepository, lotEventDispatcher)
        {
            this.path = "aircraftDocumentApplications";
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
            this.applicationRepository = applicationRepository;
            this.unitOfWork = unitOfWork;
            this.lotEventDispatcher = lotEventDispatcher;
        }

        [Route("new")]
        public IHttpActionResult GetNewDocumentApplication(int lotId)
        {
            return Ok(new FilePartVersionDO<DocumentApplicationDO>(new DocumentApplicationDO()));
        }

        public override IHttpActionResult PostNewPart(int lotId, FilePartVersionDO<DocumentApplicationDO> application)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();
                var lot = this.lotRepository.GetLotIndex(lotId);

                PartVersion partVersion = lot.CreatePart(path + "/*", JObject.FromObject(application.Part), userContext);

                this.fileRepository.AddFileReferences(partVersion, application.Files);

                lot.Commit(userContext, lotEventDispatcher);

                GvaApplication gvaApplication = new GvaApplication()
                {
                    Lot = lot,
                    GvaAppLotPart = partVersion.Part
                };

                applicationRepository.AddGvaApplication(gvaApplication);

                transaction.Commit();
            }

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