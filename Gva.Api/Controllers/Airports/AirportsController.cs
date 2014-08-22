﻿using System.Linq;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Common.Filters;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Airports;
using Gva.Api.Repositories.AirportRepository;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.InventoryRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Airports
{
    [RoutePrefix("api/airports")]
    [Authorize]
    public class AirportsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IInventoryRepository inventoryRepository;
        private IAirportRepository airportRepository;
        private IApplicationRepository applicationRepository;
        private ICaseTypeRepository caseTypeRepository;
        private ILotEventDispatcher lotEventDispatcher;

        public AirportsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IInventoryRepository inventoryRepository,
            IAirportRepository airportRepository,
            IApplicationRepository applicationRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.inventoryRepository = inventoryRepository;
            this.airportRepository = airportRepository;
            this.applicationRepository = applicationRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.lotEventDispatcher = lotEventDispatcher;
        }

        [Route("new")]
        public IHttpActionResult GetNewAirport()
        {
            AirportDataDO airportData = new AirportDataDO();

            return Ok(airportData);
        }

        [Route("")]
        public IHttpActionResult GetAirports(string name = null, string icao = null, bool exact = false)
        {
            var airports = this.airportRepository.GetAirports(name, icao, exact);

            return Ok(airports.Select(a => new AirportViewDO(a)));
        }

        [Route("{lotId}")]
        public IHttpActionResult GetAirport(int lotId)
        {
            var airport = this.airportRepository.GetAirport(lotId);

            return Ok(new AirportViewDO(airport));
        }

        [Route("")]
        [Validate]
        public IHttpActionResult PostAirport(AirportDataDO airportData)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();
                var newLot = this.lotRepository.CreateLot("Airport", userContext);

                newLot.CreatePart("airportData", JObject.FromObject(airportData), userContext);
                int airportCaseTypeId = this.caseTypeRepository.GetCaseTypesForSet("Airport").Single().GvaCaseTypeId;
                this.caseTypeRepository.AddCaseTypes(newLot, new int[] { airportCaseTypeId });

                newLot.Commit(userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new { id = newLot.LotId });
            }
        }

        [Route(@"{lotId}/airportData")]
        public IHttpActionResult GetAirportData(int lotId)
        {
            var airportData = this.lotRepository.GetLotIndex(lotId).Index.GetPart("airportData");

            return Ok(airportData.Content.ToObject<AirportDataDO>());
        }

        [Route(@"{lotId}/airportData")]
        [Validate]
        public IHttpActionResult PostAirportData(int lotId, AirportDataDO airportData)
        {
            UserContext userContext = this.Request.GetUserContext();
            var lot = this.lotRepository.GetLotIndex(lotId);
            lot.UpdatePart("airportData", JObject.FromObject(airportData), userContext);

            lot.Commit(userContext, this.lotEventDispatcher);

            this.unitOfWork.Save();

            return Ok();
        }

        [Route("{lotId}/inventory")]
        public IHttpActionResult GetInventory(int lotId, [FromUri] string[] documentTypes = null, int? caseTypeId = null)
        {
            var inventory = this.inventoryRepository.GetInventoryItemsForLot(lotId, caseTypeId);

            if (documentTypes.Length > 0)
            {
                inventory = inventory.Where(item => documentTypes.Contains(item.SetPartAlias)).ToList();
            }

            return Ok(inventory);
        }

        [Route("{lotId}/applications/{appId}")]
        public IHttpActionResult GetApplication(int lotId, int appId)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);

            GvaApplication gvaNomApp = this.applicationRepository.GetNomApplication(appId);
            if (gvaNomApp != null)
            {
                return Ok(new ApplicationNomDO(gvaNomApp));
            }

            return Ok();
        }
    }
}