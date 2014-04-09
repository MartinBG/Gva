﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.UserContext;
using Common.Data;
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
        private IFileRepository fileRepository;
        private IApplicationRepository applicationRepository;
        private ICaseTypeRepository caseTypeRepository;
        private ILotEventDispatcher lotEventDispatcher;

        public AircraftsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IInventoryRepository inventoryRepository,
            IAircraftRepository aircraftRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher)
            : base(lotRepository, fileRepository, unitOfWork, lotEventDispatcher)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.inventoryRepository = inventoryRepository;
            this.aircraftRepository = aircraftRepository;
            this.fileRepository = fileRepository;
            this.applicationRepository = applicationRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.lotEventDispatcher = lotEventDispatcher;
        }

        [Route("")]
        public IHttpActionResult GetAircrafts(string manSN = null, string model = null, string icao = null, bool exact = false)
        {
            var aircrafts = this.aircraftRepository.GetAircrafts(manSN, model, icao, exact);

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
                var newLot = this.lotRepository.GetSet("Aircraft").CreateLot(userContext);

                dynamic aircraftData = aircraft.Value<JObject>("aircraftData");
                newLot.CreatePart("aircraftData", aircraftData, userContext);
                this.caseTypeRepository.AddCaseTypes(newLot, aircraftData.Value<JArray>("caseTypes"));

                newLot.Commit(userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new { id = newLot.LotId });
            }
        }

        [Route("{lotId}/inventory")]
        public IHttpActionResult GetInventory(int lotId, int? caseTypeId = null)
        {
            this.lotRepository.GetLotIndex(lotId);
            var inventoryItems = this.inventoryRepository.GetInventoryItemsForLot(lotId);

            List<InventoryItemDO> inventory;
            if (caseTypeId.HasValue)
            {
                var lotFiles = this.fileRepository.GetFileReferencesForLot(lotId, caseTypeId.Value);

                inventory = inventoryItems
                    .Join(
                        lotFiles,
                        i => i.PartId,
                        f => f.LotPartId,
                        (i, f) => new InventoryItemDO(i, f))
                    .ToList();
            }
            else
            {
                inventory = new List<InventoryItemDO>();
                foreach (var inventoryItem in inventoryItems)
                {
                    var lotFiles = this.fileRepository.GetFileReferences(inventoryItem.PartId, null);

                    if (lotFiles.Length == 0)
                    {
                        inventory.Add(new InventoryItemDO(inventoryItem, null));
                    }

                    foreach (var lotFile in lotFiles)
                    {
                        inventory.Add(new InventoryItemDO(inventoryItem, lotFile));
                    }
                }
            }

            return Ok(inventory);
        }


        [Route("{lotId}/applications")]
        public IHttpActionResult GetApplications(int lotId, string term = null)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);

            var applications = this.applicationRepository.GetNomApplications(lotId).Select(a => new ApplicationNomDO(a));

            if (!string.IsNullOrWhiteSpace(term))
            {
                term = term.ToLower();
                applications = applications.Where(n => n.ApplicationName.ToLower().Contains(term)).ToArray();
            }

            return Ok(applications);
        }

        [Route(@"{lotId}/{*path:regex(^aircraftDocumentApplications$)}")]
        public IHttpActionResult PostNewApplication(int lotId, string path, dynamic content)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();
                var lot = this.lotRepository.GetLotIndex(lotId);

                PartVersion partVersion = lot.CreatePart(path + "/*", content.part, userContext);

                this.fileRepository.AddFileReferences(partVersion, content.files);

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

        [Route(@"{lotId}/{*path:regex(^aircraftDocumentApplications/\d+$)}")]
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

        [Route(@"{lotId}/{*path:regex(^aircraftData$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftParts/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^maintenances/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRegistrations/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRegistrationsFM/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertAirworthinesses/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertAirworthinessesFM/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertMarks/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertSmods/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertPermitsToFly/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertNoises/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertNoisesFM/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRadios/\d+$)}")]
        public override IHttpActionResult GetPart(int lotId, string path)
        {
            return base.GetPart(lotId, path);
        }
        [Route(@"{lotId}/{*path:regex(^aircraftCertRegistrations/current/\d*$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRegistrationsFM/current/\d*$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRegistrationsFM/current$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRegistrations/current$)}")]
        public override IHttpActionResult GetRegPart(int lotId)
        {
            return base.GetRegPart(lotId);
        }

        [Route(@"{lotId}/{*path:regex(^aircraftDocumentOwners/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentDebtsFM/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentDebts/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentApplications/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentOthers/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^documentOccurrences/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^inspections/\d+$)}")]
        public override IHttpActionResult GetFilePart(int lotId, string path, int? caseTypeId = null)
        {
            return base.GetFilePart(lotId, path, caseTypeId);
        }

        [Route(@"{lotId}/{*path:regex(^aircraftData$)}")]
        public IHttpActionResult PostAircraftData(int lotId, string path, JObject content)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            //this.caseTypeRepository.AddCaseTypes(lot, (content as dynamic).part.caseTypes);

            return base.PostPart(lotId, path, content);
        }

        [Route(@"{lotId}/{*path:regex(^aircraftParts$)}"),
         Route(@"{lotId}/{*path:regex(^maintenances$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRegistrations$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRegistrationsFM$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertAirworthinesses$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertAirworthinessesFM$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertMarks$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertSmods$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertPermitsToFly$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertNoises$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertNoisesFM$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRadios$)}")]
        public override IHttpActionResult GetParts(int lotId, string path)
        {
            return base.GetParts(lotId, path);
        }

        [Route(@"{lotId}/{*path:regex(^aircraftDocumentOwners$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentDebtsFM$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentDebts$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentApplications$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentOthers$)}"),
         Route(@"{lotId}/{*path:regex(^documentOccurrences$)}"),
         Route(@"{lotId}/{*path:regex(^inspections$)}")]
        public override IHttpActionResult GetFileParts(int lotId, string path, int? caseTypeId = null)
        {
            return base.GetFileParts(lotId, path, caseTypeId);
        }


        [Route(@"{lotId}/{*path:regex(^aircraftDocumentOwners$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftParts$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentDebtsFM$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentDebts$)}"),
         Route(@"{lotId}/{*path:regex(^maintenances$)}"),
         Route(@"{lotId}/{*path:regex(^documentOccurrences$)}"),
         Route(@"{lotId}/{*path:regex(^inspections$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentOthers$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRegistrations$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRegistrationsFM$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertAirworthinesses$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertAirworthinessesFM$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertMarks$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertSmods$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertPermitsToFly$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertNoises$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertNoisesFM$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRadios$)}")]
        public IHttpActionResult PostNewPart(int lotId, string path, JObject content)
        {
            return base.PostNewPart(lotId, path, content);
        }

        [Route(@"{lotId}/{*path:regex(^aircraftDocumentOwners/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftParts/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentDebtsFM/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentDebts/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^maintenances/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^documentOccurrences/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^inspections/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentApplications/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentOthers/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRegistrations/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRegistrationsFM/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertAirworthinesses/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertAirworthinessesFM/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertMarks/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertSmods/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertPermitsToFly/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertNoises/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertNoisesFM/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRadios/\d+$)}")]
        public IHttpActionResult PostPart(int lotId, string path, JObject content)
        {
            return base.PostPart(lotId, path, content);
        }

        [Route(@"{lotId}/{*path:regex(^aircraftDocumentOwners/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftParts/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentDebtsFM/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentDebts/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^maintenances/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^documentOccurrences/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^inspections/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentOthers/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRegistrations/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRegistrationsFM/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertAirworthinesses/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertAirworthinessesFM/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertMarks/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertSmods/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertPermitsToFly/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertNoises/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertNoisesFM/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRadios/\d+$)}")]
        public override IHttpActionResult DeletePart(int lotId, string path)
        {
            return base.DeletePart(lotId, path);
        }
    }
}