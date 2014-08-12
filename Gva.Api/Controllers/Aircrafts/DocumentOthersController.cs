using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Common.Json;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.FileRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;
using Gva.Api.ModelsDO.Aircrafts;

namespace Gva.Api.Controllers.Aircrafts
{
    [RoutePrefix("api/aircrafts/{lotId}/aircraftDocumentOthers")]
    [Authorize]
    public class DocumentOthersController : GvaFilePartController<AircraftDocumentOtherDO>
    {
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;

        public DocumentOthersController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher)
            : base("aircraftDocumentOthers", unitOfWork, lotRepository, fileRepository, lotEventDispatcher)
        {
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewDocumentOther(int lotId, int? appId = null)
        {
            AircraftDocumentOtherDO newDocumentOther = new AircraftDocumentOtherDO()
            {
                DocumentDateValidFrom = DateTime.Now
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

            return Ok(new FilePartVersionDO<AircraftDocumentOtherDO>(newDocumentOther, files));
        }
    }
}