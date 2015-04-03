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
using Common.Filters;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.Models.Views.Aircraft;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/aircrafts")]
    [Authorize]
    public class AircraftsController : ApiController
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
        private UserContext userContext;

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
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
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
            this.userContext = userContext;
        }

        [Route("")]
        public IHttpActionResult GetAircrafts(string mark = null, string manSN = null, string modelAlt = null, string icao = null, string airCategory = null, string aircraftProducer = null, bool exact = false)
        {
            var aircrafts = this.aircraftRepository.GetAircrafts(mark: mark, manSN: manSN, modelAlt: modelAlt, icao: icao, airCategory: airCategory, aircraftProducer: aircraftProducer, exact: exact);

            return Ok(aircrafts.Select(a => new AircraftViewDO(a)));
        }

        [Route("registrations")]
        public IHttpActionResult GetAircraftsRegistrations(string regMark = null, int? registerId = null, int? certNumber = null, int? actNumber = null)
        {
            var aircrafts = this.aircraftRegistrationRepository
                .GetAircraftsRegistrations(regMark: regMark, registerId: registerId, certNumber: certNumber, actNumber: actNumber)
                .Select(a => new AircraftRegistrationDO(a))
                .ToList();

            var invalidActNumbers = this.aircraftRepository.GetInvalidActNumbers().
                Select(a => new AircraftRegistrationDO(a))
                .ToList();

            return Ok(aircrafts.Union(invalidActNumbers).OrderBy(i => i.ActNumber).ToList());
        }

        [Route("invalidActNumbers")]
        public IHttpActionResult GetInvalidActNumbers()
        {
            return Ok(this.aircraftRepository.GetInvalidActNumbers());
        }

        [HttpPost]
        [Route("devalidateActNumber")]
        public IHttpActionResult DevalidateActNumber(ActNumberDO actNumberEntry)
        {
            return Ok(new
            {
                devalidated = this.aircraftRepository.DevalidateActNumber(actNumberEntry.ActNumber, actNumberEntry.Reason)
            });
        }

        [Route("{lotId}")]
        public IHttpActionResult GetAircraft(int lotId)
        {
            var aircraft = this.aircraftRepository.GetAircraft(lotId);

            return Ok(new AircraftViewDO(aircraft));
        }

        [Route("")]
        [Validate]
        public IHttpActionResult PostAircraft(AircraftDO aircraft)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var newLot = this.lotRepository.CreateLot("Aircraft");

                var partVersion = newLot.CreatePart("aircraftData", aircraft.AircraftData, this.userContext);

                int aircraftCaseTypeId = this.caseTypeRepository.GetCaseTypesForSet("Aircraft").Single().GvaCaseTypeId;
                this.caseTypeRepository.AddCaseTypes(newLot, new int[] { aircraftCaseTypeId });
                newLot.Commit(this.userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

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
        public IHttpActionResult CheckMSN(string msn, int? aircraftId = null)
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
        public IHttpActionResult GetNextActNumber(int? registerId = null, string registerAlias = null)
        {
            int? lastActNumber = this.aircraftRegistrationRepository.GetLastActNumber(registerId, registerAlias);

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
        public IHttpActionResult GetAircraftData(int lotId)
        {
            var aircraft = this.lotRepository.GetLotIndex(lotId);

            var aircraftDataPart = this.lotRepository.GetLotIndex(lotId).Index.GetPart<AircraftDataDO>("aircraftData");

            return Ok(aircraftDataPart.Content);
        }

        [Route(@"{lotId}/{*path:regex(^aircraftData$)}")]
        [Validate]
        public IHttpActionResult PostAircraftData(int lotId, AircraftDataDO aircraftData)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);
                PartVersion<AircraftDataDO> partVersion = lot.UpdatePart("aircraftData", aircraftData, this.userContext);

                lot.Commit(this.userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok();
            }
        }

        [Route("new")]
        public IHttpActionResult GetNewAircraft()
        {
            return Ok(new AircraftDO());
        }
    }
}