using System;
using System.Collections.Generic;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Aircrafts
{
    [RoutePrefix("api/aircrafts/{lotId}/aircraftDocumentOwners")]
    [Authorize]
    public class AircraftDocumentOwnersController : GvaFilePartController<AircraftDocumentOwnerDO>
    {
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;

        public AircraftDocumentOwnersController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("aircraftDocumentOwners", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewDocumentOwner(int lotId, int? appId = null)
        {
            AircraftDocumentOwnerDO newDocumentOwner = new AircraftDocumentOwnerDO()
            {
                FromDate = DateTime.Now
            };

            var files = new List<FileDO>();
            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);
                files.Add(new FileDO()
                {
                    IsAdded = true,
                    Applications = new List<ApplicationNomDO>()
                    {
                        this.applicationRepository.GetInitApplication(appId)
                    }
                });
            }

            return Ok(new FilePartVersionDO<AircraftDocumentOwnerDO>(newDocumentOwner, files));
        }
    }
}