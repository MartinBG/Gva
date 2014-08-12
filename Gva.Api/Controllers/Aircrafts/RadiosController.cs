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
    [RoutePrefix("api/aircrafts/{lotId}/aircraftCertRadios")]
    [Authorize]
    public class RadiosController : GvaApplicationPartController<AircraftCertRadioDO>
    {
        public RadiosController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher)
            : base("aircraftCertRadios", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher) { }

        [Route("new")]
        public IHttpActionResult GetNewCertRadio()
        {
            AircraftCertRadioDO newCertRadio = new AircraftCertRadioDO()
            {
                IssueDate = DateTime.Now
            };

            return Ok(new ApplicationPartVersionDO<AircraftCertRadioDO>(newCertRadio));
        }
    }
}