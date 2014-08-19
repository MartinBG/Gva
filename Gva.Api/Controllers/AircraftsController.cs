using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.UserContext;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.InventoryRepository;
using Gva.Api.Repositories.AircraftRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/aircrafts")]
    [Authorize]
    public class AircraftsController : GvaLotsController
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IInventoryRepository inventoryRepository;
        private IAircraftRepository aircraftRepository;
        private IAircraftRegistrationRepository aircraftRegistrationRepository;
        private IAircraftRegMarkRepository aircraftRegMarkRepository;
        private IFileRepository fileRepository;
        private IApplicationRepository applicationRepository;
        private ICaseTypeRepository caseTypeRepository;
        private ILotEventDispatcher lotEventDispatcher;

        public AircraftsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IInventoryRepository inventoryRepository,
            IAircraftRepository aircraftRepository,
            IAircraftRegistrationRepository aircraftRegistrationRepository,
            IAircraftRegMarkRepository aircraftRegMarkRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher)
            : base(applicationRepository, lotRepository, fileRepository, unitOfWork, lotEventDispatcher)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.inventoryRepository = inventoryRepository;
            this.aircraftRepository = aircraftRepository;
            this.aircraftRegistrationRepository = aircraftRegistrationRepository;
            this.aircraftRegMarkRepository = aircraftRegMarkRepository;
            this.fileRepository = fileRepository;
            this.applicationRepository = applicationRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.lotEventDispatcher = lotEventDispatcher;
        }

        [Route("")]
        public IHttpActionResult GetAircrafts(string mark = null, string manSN = null, string model = null, string icao = null, string airCategory = null, string aircraftProducer = null, bool exact = false)
        {
            var aircrafts = this.aircraftRepository.GetAircrafts(mark: mark, manSN: manSN, model: model, icao: icao, airCategory: airCategory, aircraftProducer: aircraftProducer, exact: exact);

            return Ok(aircrafts.Select(a => new AircraftDO(a)));
        }

        [Route("{lotId}")]
        public IHttpActionResult GetAircraft(int lotId)
        {
            var aircraft = this.aircraftRepository.GetAircraft(lotId);

            return Ok(new AircraftDO(aircraft));
        }

        [Route("")]
        public IHttpActionResult PostAircraft(JObject aircraft)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();
                var newLot = this.lotRepository.CreateLot("Aircraft", userContext);

                newLot.CreatePart("aircraftData", aircraft.Get<JObject>("aircraftData"), userContext);
                int aircraftCaseTypeId = this.caseTypeRepository.GetCaseTypesForSet("Aircraft").Single().GvaCaseTypeId;
                this.caseTypeRepository.AddCaseTypes(newLot, new int[] { aircraftCaseTypeId });
                newLot.Commit(userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new { id = newLot.LotId });
            }
        }

        [Route("{lotId}/inventory")]
        public IHttpActionResult GetInventory(int lotId, int? caseTypeId = null)
        {
            var inventory = this.inventoryRepository.GetInventoryItemsForLot(lotId, caseTypeId);
            return Ok(inventory);
        }

        [HttpGet]
        [Route("checkMSN")]
        public IHttpActionResult checkMSN(string msn, int? aircraftId = null)
        {
            bool isValid = this.aircraftRepository.IsUniqueMSN(msn, aircraftId);

            return Ok(new
            {
                IsValid = isValid
            });
        }

        [HttpGet]
        [Route("checkRegMark")]
        public IHttpActionResult CheckRegMark(int lotId, string regMark = null)
        {
            bool isValid = this.aircraftRegMarkRepository.RegMarkIsValid(lotId, regMark);

            return Ok(new
            {
                IsValid = isValid
            });
        }

        [Route("getNextActNumber")]
        public IHttpActionResult GetNextActNumber(int registerId)
        {
            int? lastActNumber = this.aircraftRegistrationRepository.GetLastActNumber(registerId);

            return Ok(new
            {
                ActNumber =  (lastActNumber ?? 0) + 1
            });
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

        [Route(@"{lotId}/{*path:regex(^aircraftData$)}")]
        public override IHttpActionResult GetPart(int lotId, string path)
        {
            return base.GetPart(lotId, path);
        }

        [Route(@"{lotId}/{*path:regex(^aircraftData$)}")]
        public IHttpActionResult PostAircraftData(int lotId, string path, JObject content)
        {
            return base.PostPart(lotId, path, content);
        }
    }
}