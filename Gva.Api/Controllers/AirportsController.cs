﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.Repositories.AirportRepository;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.InventoryRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/airports")]
    [Authorize]
    public class AirportsController : GvaLotsController
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IInventoryRepository inventoryRepository;
        private IAirportRepository airportRepository;
        private IFileRepository fileRepository;
        private IApplicationRepository applicationRepository;
        private ICaseTypeRepository caseTypeRepository;
        private ILotEventDispatcher lotEventDispatcher;

        public AirportsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IInventoryRepository inventoryRepository,
            IAirportRepository airportRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher)
            : base(applicationRepository, lotRepository, fileRepository, unitOfWork, lotEventDispatcher)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.inventoryRepository = inventoryRepository;
            this.airportRepository = airportRepository;
            this.fileRepository = fileRepository;
            this.applicationRepository = applicationRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.lotEventDispatcher = lotEventDispatcher;
        }

        [Route("")]
        public IHttpActionResult GetAirports(string name = null, string icao = null, bool exact = false)
        {
            var airports = this.airportRepository.GetAirports(name, icao, exact);

            return Ok(airports.Select(a => new AirportDO(a)));
        }

        [Route("{lotId}")]
        public IHttpActionResult GetAirport(int lotId)
        {
            var airport = this.airportRepository.GetAirport(lotId);

            return Ok(new AirportDO(airport));
        }

        [Route("")]
        public IHttpActionResult PostAirport(JObject airport)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();
                var newLot = this.lotRepository.CreateLot("Airport", userContext);

                newLot.CreatePart("airportData", airport.Get<JObject>("airportData"), userContext);
                this.caseTypeRepository.AddCaseTypes(newLot, airport.GetItems<JObject>("airportData.caseTypes"));

                newLot.Commit(userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new { id = newLot.LotId });
            }
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

        [Route(@"{lotId}/{*path:regex(^airportDocumentApplications$)}")]
        public IHttpActionResult PostNewApplication(int lotId, string path, JObject content)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();
                var lot = this.lotRepository.GetLotIndex(lotId);

                PartVersion partVersion = lot.CreatePart(path + "/*", content.Get<JObject>("part"), userContext);

                this.fileRepository.AddFileReferences(partVersion, content.GetItems<FileDO>("files"));

                lot.Commit(userContext, lotEventDispatcher);

                GvaApplication application = new GvaApplication()
                {
                    Lot = lot,
                    GvaAppLotPart = partVersion.Part
                };

                applicationRepository.AddGvaApplication(application);

                transaction.Commit();
            }

            this.unitOfWork.Save();

            return Ok();
        }

        [Route(@"{lotId}/{*path:regex(^airportDocumentApplications/\d+$)}")]
        public IHttpActionResult DeleteApplication(int lotId, string path)
        {
            IHttpActionResult result;

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var partVersion = this.lotRepository.GetLotIndex(lotId).GetPart(path);

                applicationRepository.DeleteGvaApplication(partVersion.Part.PartId);

                result = base.DeletePart(lotId, path);

                transaction.Commit();
            }

            this.unitOfWork.Save();

            return result;
        }

        [Route(@"{lotId}/{*path:regex(^airportData$)}"),
         Route(@"{lotId}/{*path:regex(^airportCertOperationals/\d+$)}")]
        public override IHttpActionResult GetPart(int lotId, string path)
        {
            return base.GetPart(lotId, path);
        }

        [Route(@"{lotId}/{*path:regex(^airportDocumentOwners/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^airportDocumentApplications/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^airportDocumentOthers/\d+$)}")]
        public override IHttpActionResult GetFilePart(int lotId, string path, int? caseTypeId = null)
        {
            return base.GetFilePart(lotId, path, caseTypeId);
        }

        [Route(@"{lotId}/{*path:regex(^inspections/\d+$)}")]
        public override IHttpActionResult GetApplicationPart(int lotId, string path)
        {
            return base.GetApplicationPart(lotId, path);
        }

        [Route(@"{lotId}/{*path:regex(^airportData$)}")]
        public IHttpActionResult PostAirportData(int lotId, string path, JObject content)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            //this.caseTypeRepository.AddCaseTypes(lot, (content as dynamic).part.caseTypes);

            return base.PostPart(lotId, path, content);
        }

        [Route(@"{lotId}/{*path:regex(^airportCertOperationals$)}")]
        public override IHttpActionResult GetParts(int lotId, string path)
        {
            return base.GetParts(lotId, path);
        }

        [Route(@"{lotId}/{*path:regex(^airportDocumentOwners$)}"),
         Route(@"{lotId}/{*path:regex(^airportDocumentApplications$)}"),
         Route(@"{lotId}/{*path:regex(^airportDocumentOthers$)}")]
        public override IHttpActionResult GetFileParts(int lotId, string path, int? caseTypeId = null)
        {
            return base.GetFileParts(lotId, path, caseTypeId);
        }

        [Route(@"{lotId}/{*path:regex(^inspections$)}")]
        public override IHttpActionResult GetApplicationParts(int lotId, string path)
        {
            return base.GetApplicationParts(lotId, path);
        }

        [Route(@"{lotId}/{*path:regex(^airportDocumentOwners$)}"),
         Route(@"{lotId}/{*path:regex(^airportDocumentOthers$)}"),
         Route(@"{lotId}/{*path:regex(^inspections$)}"),
         Route(@"{lotId}/{*path:regex(^airportCertOperationals$)}")]
        public override IHttpActionResult PostNewPart(int lotId, string path, JObject content)
        {
            return base.PostNewPart(lotId, path, content);
        }

        [Route(@"{lotId}/{*path:regex(^airportDocumentOwners/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^airportDocumentApplications/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^airportDocumentOthers/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^inspections/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^airportCertOperationals/\d+$)}")]
        public override IHttpActionResult PostPart(int lotId, string path, JObject content)
        {
            return base.PostPart(lotId, path, content);
        }

        [Route(@"{lotId}/{*path:regex(^airportDocumentOwners/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^airportDocumentOthers/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^inspections/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^airportCertOperationals/\d+$)}")]
        public override IHttpActionResult DeletePart(int lotId, string path)
        {
            return base.DeletePart(lotId, path);
        }
    }
}