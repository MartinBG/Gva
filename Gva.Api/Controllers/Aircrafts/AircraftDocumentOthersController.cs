using System;
using System.Collections.Generic;
using System.Web.Http;
using Common.Api.Repositories.NomRepository;
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
    [RoutePrefix("api/aircrafts/{lotId}/aircraftDocumentOthers")]
    [Authorize]
    public class AircraftDocumentOthersController : GvaFilePartController<AircraftDocumentOtherDO>
    {
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;
        private INomRepository nomRepository;

        public AircraftDocumentOthersController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("aircraftDocumentOthers", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
            this.nomRepository = nomRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewDocumentOther(int lotId, int? appId = null)
        {
            AircraftDocumentOtherDO newDocumentOther = new AircraftDocumentOtherDO()
            {
                DocumentDateValidFrom = DateTime.Now
            };

            newDocumentOther.Valid = this.nomRepository.GetNomValue("boolean", "yes");

            var cases = new List<CaseDO>();
            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);
                cases.Add(new CaseDO()
                {
                    IsAdded = true,
                    Applications = new List<ApplicationNomDO>()
                    {
                        this.applicationRepository.GetInitApplication(appId)
                    }
                });
            }

            return Ok(new FilePartVersionDO<AircraftDocumentOtherDO>(newDocumentOther, cases));
        }
    }
}