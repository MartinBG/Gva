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
        public IHttpActionResult GetAircrafts(string mark = null, string manSN = null, string model = null, string airCategory = null, string aircraftProducer = null, bool exact = false)
        {
            var aircrafts = this.aircraftRepository.GetAircrafts(mark: mark, manSN: manSN, model: model, airCategory: airCategory, aircraftProducer: aircraftProducer, exact: exact);

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

                newLot.CreatePart("aircraftData", aircraft.Get<JObject>("aircraftData"), userContext);
                this.caseTypeRepository.AddCaseTypes(newLot, aircraft.GetItems<JObject>("aircraftData.caseTypes"));

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

        [Route("getNextCertNumber")]
        public IHttpActionResult GetNextCertNumber(int registerId, int? currentCertNumber = null)
        {
            int? lastCertNumber = this.aircraftRegistrationRepository.GetLastCertNumber(registerId);

            int nextCertNumber = Math.Max(lastCertNumber ?? 0, currentCertNumber ?? 0) + 1;

            return Ok(new
            {
                CertNumber =  nextCertNumber
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

        [Route(@"{lotId}/{*path:regex(^aircraftDocumentApplications$)}")]
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
         Route(@"{lotId}/{*path:regex(^aircraftCertAirworthinessesFM/\d+$)}")]
        public override IHttpActionResult GetPart(int lotId, string path)
        {
            return base.GetPart(lotId, path);
        }


        public RegistrationsDO GetRegistrationsData(IEnumerable<PartVersion> registrations, IEnumerable<GvaViewAircraftRegistration> registrationsView, int? lotInd = null, int? lotId = null)
        {
            RegistrationsDO regsData = new RegistrationsDO();
            var regs = registrations.OrderByDescending(r => r.Content.Get<int>("actNumber")).ToArray();
            int regIndex;
            int? regPartId;
            if (lotInd.HasValue)
            {
                var currentReg = regs.Where(e => e.Part.Index == lotInd).FirstOrDefault();
                regsData.CurrentIndex = currentReg.Part.Index;
                regIndex = Array.IndexOf(regs, currentReg);
                regPartId = currentReg.PartId;
            }
            else
            {
                var currentReg = regs.FirstOrDefault();
                regsData.CurrentIndex = currentReg.Part.Index;
                regIndex = Array.IndexOf(regs, currentReg);
                regPartId = currentReg.PartId;
            }
            regsData.AirworthinessIndex = registrationsView.Where(e => e.LotPartId == regPartId).FirstOrDefault().CertAirworthinessId;
            if (regsData.AirworthinessIndex != null)
            {
                regsData.HasAirworthiness = true;
            }
            else
            {
                regsData.HasAirworthiness = false;
                var regsWithAirworthiness = registrationsView.Where(e => e.LotId == lotId && e.CertAirworthinessId.HasValue).ToList();
                if (regsWithAirworthiness.Count > 0)
                {
                    regsData.AirworthinessIndex = regsWithAirworthiness.FirstOrDefault().CertAirworthinessId;
                }
            }
            regsData.LastIndex = regs[0].Part.Index;
            regsData.NextIndex = regIndex > 0 ? regs[regIndex - 1].Part.Index : (int?)null;
            regsData.PrevIndex = regIndex < regs.Length - 1 ? regs[regIndex + 1].Part.Index : (int?)null;
            regsData.FirstIndex = regs[regs.Length - 1].Part.Index;
            regsData.LastReg = regs[0].Content;
            regsData.FirstReg = regs[regs.Length - 1].Content;

            return regsData;
        }
        [Route(@"{lotId}/{*path:regex(^aircraftCertRegistrationsCurrent$)}")]
        public IHttpActionResult GetRegistrationsData(int lotId)
        {
            var registrations = this.lotRepository.GetLotIndex(lotId).GetParts("aircraftCertRegistrationsFM");
            if (registrations.Length > 0)
            {
                var registrationsView = this.aircraftRegistrationRepository.GetRegistrations(lotId);

                return Ok(GetRegistrationsData(registrations, registrationsView, null, lotId));
            }
            else
            {
                return Ok(new { notRegistered = true });
            }
        }

        [Route(@"{lotId}/{path:regex(^aircraftCertRegistrationsCurrent$)}/{lotInd}")]
        public IHttpActionResult GetRegistrationsData(int lotId, int? lotInd = null)
        {
            var registrations = this.lotRepository.GetLotIndex(lotId).GetParts("aircraftCertRegistrationsFM");
            if (registrations.Length > 0)
            {
                var registrationsView = this.aircraftRegistrationRepository.GetRegistrations(lotId);
                return Ok(GetRegistrationsData(registrations, registrationsView, lotInd, lotId));
            }
            else
            {
                return Ok(new { notRegistered = true });
            }
        }

        [Route(@"{lotId}/{*path:regex(^aircraftDocumentOwners/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentDebtsFM/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentDebts/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentApplications/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentOthers/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^documentOccurrences/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRegistrations/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRegistrationsFM/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertAirworthinesses/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertMarks/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertSmods/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertPermitsToFly/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertNoises/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRadios/\d+$)}")]
        public override IHttpActionResult GetFilePart(int lotId, string path, int? caseTypeId = null)
        {
            return base.GetFilePart(lotId, path, caseTypeId);
        }

        [Route(@"{lotId}/{*path:regex(^inspections/\d+$)}")]
        public override IHttpActionResult GetApplicationPart(int lotId, string path)
        {
            return base.GetApplicationPart(lotId, path);
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
         Route(@"{lotId}/{*path:regex(^aircraftCertAirworthinessesFM$)}")]
        public override IHttpActionResult GetParts(int lotId, string path)
        {
            return base.GetParts(lotId, path);
        }

        [Route(@"{lotId}/{*path:regex(^aircraftCertRegistrationsFM$)}")]
        public IHttpActionResult GetRegistrations(int lotId, string path)
        {
            var parts = this.lotRepository.GetLotIndex(lotId).GetParts(path).OrderByDescending(e => e.Content.Get<int>("actNumber"));

            return Ok(parts.Select(pv => new PartVersionDO(pv)));
        }

        [Route(@"{lotId}/{path:regex(^aircraftCurrentDocumentDebtsFM$)}/{regId}")]
        public IHttpActionResult GetRegistrations(int lotId, int regId)
        {
            var parts = this.lotRepository.GetLotIndex(lotId).GetParts("aircraftDocumentDebtsFM").Where(e => e.Content.Get<int>("registration.nomValueId") == regId);

            return Ok(parts.Select(pv => new PartVersionDO(pv)));
        }

        [Route(@"{lotId}/{*path:regex(^aircraftDocumentOwners$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentDebtsFM$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentDebts$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentApplications$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentOthers$)}"),
         Route(@"{lotId}/{*path:regex(^documentOccurrences$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRegistrations$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertAirworthinesses$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertMarks$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertSmods$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertPermitsToFly$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertNoises$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRadios$)}")]
        public override IHttpActionResult GetFileParts(int lotId, string path, int? caseTypeId = null)
        {
            return base.GetFileParts(lotId, path, caseTypeId);
        }

        [Route(@"{lotId}/{*path:regex(^inspections$)}")]
        public override IHttpActionResult GetApplicationParts(int lotId, string path)
        {
            return base.GetApplicationParts(lotId, path);
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
         Route(@"{lotId}/{*path:regex(^aircraftCertRadios$)}")]
        public override IHttpActionResult PostNewPart(int lotId, string path, JObject content)
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
         Route(@"{lotId}/{*path:regex(^aircraftCertRadios/\d+$)}")]
        public override IHttpActionResult PostPart(int lotId, string path, JObject content)
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
         Route(@"{lotId}/{*path:regex(^aircraftCertMarks/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertSmods/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertPermitsToFly/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertNoises/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRadios/\d+$)}")]
        public override IHttpActionResult DeletePart(int lotId, string path)
        {
            return base.DeletePart(lotId, path);
        }

    }
}