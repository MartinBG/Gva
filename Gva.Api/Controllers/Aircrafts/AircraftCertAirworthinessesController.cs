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
    [RoutePrefix("api/aircrafts/{lotId}/aircraftCertAirworthinesses")]
    [Authorize]
    public class AircraftCertAirworthinessesController : GvaFilePartController<AircraftCertAirworthinessDO>
    {
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;

        public AircraftCertAirworthinessesController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("aircraftCertAirworthinesses", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewCertAirworthiness(int lotId)
        {
            return Ok(new FilePartVersionDO<AircraftCertAirworthinessDO>(new AircraftCertAirworthinessDO()));
        }
    }
}