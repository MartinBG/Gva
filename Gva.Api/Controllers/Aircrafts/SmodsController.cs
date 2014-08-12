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
    [RoutePrefix("api/aircrafts/{lotId}/aircraftCertSmods")]
    [Authorize]
    public class SmodsController : GvaApplicationPartController<AircraftCertSmodDO>
    {
        public SmodsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher)
            : base("aircraftCertSmods", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher) { }

        [Route("new")]
        public IHttpActionResult GetNewCertSmod()
        {
            return Ok(new ApplicationPartVersionDO<AircraftCertSmodDO>(new AircraftCertSmodDO()));
        }
    }
}