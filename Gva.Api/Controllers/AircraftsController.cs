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
                var partVersion = this.lotRepository.GetLotIndex(lotId).Index.GetPart(path);

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
         Route(@"{lotId}/{*path:regex(^aircraftCertRegistrationsFM/\d+$)}")]
        public override IHttpActionResult GetPart(int lotId, string path)
        {
            return base.GetPart(lotId, path);
        }

        public RegistrationViewDO CreateRegistrationView(IEnumerable<PartVersion> registrations, IEnumerable<PartVersion> airworthinesses, int? regPartIndex)
        {
            var regs =
                (from r in registrations
                join aw in airworthinesses on r.Part.Index equals aw.Content.Get<int>("registration.nomValueId") into gaws
                from aw in gaws.DefaultIfEmpty()
                group aw by r into aws
                orderby aws.Key.Content.Get<int>("actNumber") descending
                select aws)
                .Select((aws, i) =>
                    new
                    {
                        Position = i,
                        Reg = aws.Key,
                        Aws = aws.Where(aw => aw != null).OrderByDescending(a => a.Content.Get<DateTime>("issueDate")).AsEnumerable()
                    })
                .ToList();

            var currentReg =
                //used to create a variable from this annonymous type
                new
                {
                    Position = default(int),
                    Reg = default(PartVersion),
                    Aws = Enumerable.Empty<PartVersion>()
                };

            if (regPartIndex.HasValue)
            {
                currentReg = regs.Where(r => r.Reg.Part.Index == regPartIndex).FirstOrDefault();
            }
            else
            {
                currentReg = regs.FirstOrDefault();
            }

            RegistrationViewDO regView = new RegistrationViewDO();
            regView.CurrentIndex = currentReg.Reg.Part.Index;

            var currentRegAw = regs
                .Where(r => r.Position >= currentReg.Position && r.Aws.Any())
                .Select(r =>
                    new
                    {
                        Aw = r.Aws.First(),
                        IsForCurrentReg = r == currentReg
                    })
                .FirstOrDefault();

            if (currentRegAw != null)
            {
                regView.AirworthinessIndex = currentRegAw.Aw.Part.Index;
                regView.HasAirworthiness = currentRegAw.IsForCurrentReg;
            }
            else
            {
                regView.AirworthinessIndex = null;
                regView.HasAirworthiness = false;
            }

            int position = currentReg.Position;

            regView.LastIndex = regs[0].Reg.Part.Index;
            regView.NextIndex = position > 0 ? regs[position - 1].Reg.Part.Index : (int?)null;
            regView.PrevIndex = position < regs.Count - 1 ? regs[position + 1].Reg.Part.Index : (int?)null;
            regView.FirstIndex = regs[regs.Count - 1].Reg.Part.Index;

            regView.LastReg = regs[0].Reg.Content;
            regView.FirstReg = regs[regs.Count - 1].Reg.Content;

            return regView;
        }

        [Route(@"{lotId}/aircraftCertRegistrationsFM/view")]
        [Route(@"{lotId}/aircraftCertRegistrationsFM/{regPartIndex}/view")]
        public IHttpActionResult GetRegistrationView(int lotId, int? regPartIndex = null)
        {
            var index = this.lotRepository.GetLotIndex(lotId).Index;
            var registrations = index.GetParts("aircraftCertRegistrationsFM");
            var airworthinesses = index.GetParts("aircraftCertAirworthinessesFM");
            if (registrations.Length > 0)
            {
                return Ok(CreateRegistrationView(registrations, airworthinesses, regPartIndex));
            }
            else
            {
                return Ok();
            }
        }

        [Route(@"{lotId}/aircraftCertRegistrationsFM/{regPartIndex}/debts")]
        public IHttpActionResult GetRegistrationDebts(int lotId, int regPartIndex)
        {
            var parts = this.lotRepository.GetLotIndex(lotId).Index.GetParts("aircraftDocumentDebtsFM").Where(e => e.Content.Get<int>("registration.nomValueId") == regPartIndex);

            return Ok(parts.Select(pv => new PartVersionDO(pv)));
        }

        [Route(@"{lotId}/{*path:regex(^aircraftDocumentOwners/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentDebtsFM/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentDebts/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentApplications/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^documentOccurrences/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRegistrations/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertAirworthinesses/\d+$)}")]
        public override IHttpActionResult GetFilePart(int lotId, string path, int? caseTypeId = null)
        {
            return base.GetFilePart(lotId, path, caseTypeId);
        }

        [Route(@"{lotId}/{*path:regex(^inspections/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertAirworthinessesFM/\d+$)}")]
        public override IHttpActionResult GetApplicationPart(int lotId, string path)
        {
            return base.GetApplicationPart(lotId, path);
        }

        [Route(@"{lotId}/{*path:regex(^aircraftData$)}")]
        public IHttpActionResult PostAircraftData(int lotId, string path, JObject content)
        {
            return base.PostPart(lotId, path, content);
        }

        [Route(@"{lotId}/{*path:regex(^aircraftParts$)}"),
         Route(@"{lotId}/{*path:regex(^maintenances$)}")]
        public override IHttpActionResult GetParts(int lotId, string path)
        {
            return base.GetParts(lotId, path);
        }

        [Route(@"{lotId}/{*path:regex(^aircraftCertRegistrationsFM$)}")]
        public IHttpActionResult GetRegistrations(int lotId, string path)
        {
            var parts = this.lotRepository.GetLotIndex(lotId).Index.GetParts(path).OrderByDescending(e => e.Content.Get<int>("actNumber"));

            return Ok(parts.Select(pv => new PartVersionDO(pv)));
        }

        [Route(@"{lotId}/{*path:regex(^aircraftDocumentOwners$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentDebtsFM$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentDebts$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftDocumentApplications$)}"),
         Route(@"{lotId}/{*path:regex(^documentOccurrences$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRegistrations$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertAirworthinesses$)}")]
        public override IHttpActionResult GetFileParts(int lotId, string path, int? caseTypeId = null)
        {
            return base.GetFileParts(lotId, path, caseTypeId);
        }

        [Route(@"{lotId}/{*path:regex(^inspections$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertAirworthinessesFM$)}")]
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
         Route(@"{lotId}/{*path:regex(^aircraftCertRegistrations$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRegistrationsFM$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertAirworthinesses$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertAirworthinessesFM$)}")]
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
         Route(@"{lotId}/{*path:regex(^aircraftCertRegistrations/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertRegistrationsFM/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertAirworthinesses/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertAirworthinessesFM/\d+$)}")]
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
         Route(@"{lotId}/{*path:regex(^aircraftCertRegistrations/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertAirworthinessesFM/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^aircraftCertAirworthinesses/\d+$)}")]
        public override IHttpActionResult DeletePart(int lotId, string path)
        {
            return base.DeletePart(lotId, path);
        }


        [Route(@"{lotId}/{*path:regex(^aircraftCertRegistrationsFM/\d+$)}")]
        public IHttpActionResult DeleteRegPart(int lotId, string path)
        {
            UserContext userContext = this.Request.GetUserContext();
            var lot = this.lotRepository.GetLotIndex(lotId);
            var partVersion = lot.DeletePart(path, userContext);
            this.fileRepository.DeleteFileReferences(partVersion);
            this.applicationRepository.DeleteApplicationRefs(partVersion);
            var registrations = this.lotRepository.GetLotIndex(lotId).Index.GetParts("aircraftCertRegistrationsFM").ToList();
            if (registrations.Count > 0)
            {
                var lastRegistration = registrations.FirstOrDefault();
                lastRegistration.Content.Property("isCurrent").Value = true;
                lot.UpdatePart("aircraftCertRegistrationsFM/" + lastRegistration.Part.Index , lastRegistration.Content, userContext);
            }
            lot.Commit(userContext, lotEventDispatcher);

            this.unitOfWork.Save();

            return Ok();
        }
    }
}