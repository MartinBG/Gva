using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.InventoryRepository;
using Gva.Api.Repositories.PersonRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/persons")]
    [Authorize]
    public class PersonsController : GvaLotsController
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IInventoryRepository inventoryRepository;
        private IPersonRepository personRepository;
        private IFileRepository fileRepository;
        private IApplicationRepository applicationRepository;
        private ICaseTypeRepository caseTypeRepository;
        private ILotEventDispatcher lotEventDispatcher;

        public PersonsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IInventoryRepository inventoryRepository,
            IPersonRepository personRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher)
            : base(applicationRepository, lotRepository, fileRepository, unitOfWork, lotEventDispatcher)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.inventoryRepository = inventoryRepository;
            this.personRepository = personRepository;
            this.fileRepository = fileRepository;
            this.applicationRepository = applicationRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.lotEventDispatcher = lotEventDispatcher;
        }

        [Route("")]
        public IHttpActionResult GetPersons(
            string lin = null,
            string linType = null,
            string uin = null,
            string names = null,
            string licences = null,
            string ratings = null,
            string organization = null,
            string caseTypeAlias = null,
            bool exact = false)
        {
            var persons = this.personRepository.GetPersons(lin, linType, uin, names, licences, ratings, organization, caseTypeAlias, exact);

            return Ok(persons.Select(p => new PersonDO(p)));
        }

        [Route("{lotId}")]
        public IHttpActionResult GetPerson(int lotId)
        {
            var person = this.personRepository.GetPerson(lotId);

            return Ok(new PersonDO(person));
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

        [Route("")]
        public IHttpActionResult PostPerson(JObject person)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();
                var newLot = this.lotRepository.GetSet("Person").CreateLot(userContext);

                newLot.CreatePart("personData", person.Get<JObject>("personData"), userContext);
                this.caseTypeRepository.AddCaseTypes(newLot, person.GetItems<JObject>("personData.caseTypes"));

                var personDocumentId = person.Get<JObject>("personDocumentId");
                if (personDocumentId != null)
                {
                    var documentIdPart = newLot.CreatePart("personDocumentIds/*", personDocumentId, userContext);
                    this.fileRepository.AddFileReferences(documentIdPart, null);
                }

                var personAddress = person.Get<JObject>("personAddress");
                if (personAddress != null)
                {
                    newLot.CreatePart("personAddresses/*", personAddress, userContext);
                }

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

        [Route(@"{lotId}/{*path:regex(^personAddresses/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personData$)}"),
         Route(@"{lotId}/{*path:regex(^personFlyingExperiences/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personStatuses/\d+$)}")]
        public override IHttpActionResult GetPart(int lotId, string path)
        {
            return base.GetPart(lotId, path);
        }

        [Route(@"{lotId}/{*path:regex(^personDocumentIds/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentChecks/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentEducations/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentEmployments/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentMedicals/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentExams/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentTrainings/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentOthers/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentApplications/\d+$)}")]
        public override IHttpActionResult GetFilePart(int lotId, string path, int? caseTypeId = null)
        {
            return base.GetFilePart(lotId, path, caseTypeId);
        }

        [Route(@"{lotId}/{*path:regex(^licences/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^ratings/\d+$)}")]
        public override IHttpActionResult GetApplicationPart(int lotId, string path)
        {
            return base.GetApplicationPart(lotId, path);
        }

        [Route(@"{lotId}/{*path:regex(^licences$)}"),
        Route(@"{lotId}/{*path:regex(^ratings$)}")]
        public override IHttpActionResult GetApplicationParts(int lotId, string path)
        {
            return base.GetApplicationParts(lotId, path);
        }

        [Route(@"{lotId}/{*path:regex(^licences$)}"),
        Route(@"{lotId}/{*path:regex(^ratings$)}")]
        public IHttpActionResult PostNewApplicationPart(int lotId, string path, JObject content)
        {
            UserContext userContext = this.Request.GetUserContext();
            var lot = this.lotRepository.GetLotIndex(lotId);

            PartVersion partVersion = lot.CreatePart(path + "/*", content.Get<JObject>("part"), userContext);

            this.fileRepository.AddFileReferences(partVersion, content.GetItems<FileDO>("files"));

            this.applicationRepository.AddApplicationRefs(partVersion, content.GetItems<ApplicationNomDO>("part.editions[0].applications"));

            lot.Commit(userContext, lotEventDispatcher);

            this.unitOfWork.Save();

            return Ok(new { partIndex = partVersion.Part.Index });
        }

        [Route(@"{lotId}/{*path:regex(^licences/\d+$)}"),
        Route(@"{lotId}/{*path:regex(^ratings/\d+$)}")]
        public IHttpActionResult PostApplicationPart(int lotId, string path, JObject content)
        {
            UserContext userContext = this.Request.GetUserContext();
            var lot = this.lotRepository.GetLotIndex(lotId);
            PartVersion partVersion = lot.UpdatePart(path, content.Get<JObject>("part"), userContext);

            this.fileRepository.AddFileReferences(partVersion, content.GetItems<FileDO>("files"));

            var editions = content.GetItems<JObject>("part.editions");

            foreach (var edition in editions)
            {
                this.applicationRepository.AddApplicationRefs(partVersion, edition.GetItems<ApplicationNomDO>("applications"));
            }

            lot.Commit(userContext, lotEventDispatcher);

            this.unitOfWork.Save();

            return Ok();
        }

        [Route(@"{lotId}/{*path:regex(^personAddresses$)}"),
         Route(@"{lotId}/{*path:regex(^personFlyingExperiences$)}"),
         Route(@"{lotId}/{*path:regex(^personStatuses$)}")]
        public override IHttpActionResult GetParts(int lotId, string path)
        {
            return base.GetParts(lotId, path);
        }

        [Route(@"{lotId}/{path:regex(^personDocumentIds$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentChecks$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentEducations$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentEmployments$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentMedicals$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentExams$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentTrainings$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentOthers$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentApplications$)}")]
        public override IHttpActionResult GetFileParts(int lotId, string path, int? caseTypeId = null)
        {
            return base.GetFileParts(lotId, path, caseTypeId);
        }


        [Route("nextLin")]
        public IHttpActionResult GetNextLin(string linType = null)
        {
            var persons = this.personRepository.GetPersons(linType: linType);

            int nextLin;
            if (persons.Count() != 0)
            {
                int lastLin = persons.OrderBy(p => int.Parse(p.Lin)).Select(p => int.Parse(p.Lin)).Last();
                nextLin = ++lastLin;
            }
            else
            {
                var lins = new Dictionary<string, int>()
                {
                    { "pilots", 10001 },
                    { "flyingCrew", 20001 },
                    { "crewStaff", 30001 },
                    { "HeadFlights", 40001 },
                    { "AirlineEngineers", 50001 },
                    { "dispatchers", 60001 },
                    { "paratroopers", 70001 },
                    { "engineersRVD", 80001 },
                    { "deltaplaner", 90001 }
                };
                nextLin = lins[linType];
            }

            return Ok(new
            {
                NextLin = nextLin
            });
        }

        [Route("{lotId}/lastLicenceNumber")]
        public IHttpActionResult GetLastLicenceNumber(int lotId, string licenceType)
        {

            PartVersion[] licences = this.lotRepository.GetLotIndex(lotId).GetParts("licences");

            string licenceNumber = licences.Where(l => l.Content.Get("licenceType.code").ToString() == licenceType)
                .OrderBy(l => l.Part.PartId)
                .Where(l => l.Content.Get("licenceNumber") != null)
                .Select(l => l.Content.Get("licenceNumber").ToString()).FirstOrDefault();

            return Ok(new JObject(new JProperty("number", licenceNumber)));
        }

        [Route(@"{lotId}/{*path:regex(^personAddresses$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentChecks$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentEducations$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentEmployments$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentIds$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentMedicals$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentExams$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentTrainings$)}"),
         Route(@"{lotId}/{*path:regex(^personFlyingExperiences$)}"),
         Route(@"{lotId}/{*path:regex(^personStatuses$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentOthers$)}")]
        public override IHttpActionResult PostNewPart(int lotId, string path, JObject content)
        {
            return base.PostNewPart(lotId, path, content);
        }

        [Route(@"{lotId}/{*path:regex(^personAddresses/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentChecks/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentEducations/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentEmployments/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentIds/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentMedicals/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentExams\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentTrainings/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personFlyingExperiences/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personStatuses/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentOthers/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentApplications/\d+$)}")]
        public override IHttpActionResult PostPart(int lotId, string path, JObject content)
        {
            return base.PostPart(lotId, path, content);
        }

        [Route(@"{lotId}/{*path:regex(^personData$)}")]
        public IHttpActionResult PostPersonData(int lotId, string path, JObject content)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            this.caseTypeRepository.AddCaseTypes(lot, content.GetItems<JObject>("part.caseTypes"));

            return base.PostPart(lotId, path, content);
        }

        [Route(@"{lotId}/{*path:regex(^personAddresses/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentChecks/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentEducations/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentEmployments/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentIds/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentMedicals/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentExams\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentTrainings/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personFlyingExperiences/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^licences/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^ratings/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personStatuses/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentOthers/\d+$)}")]
        public override IHttpActionResult DeletePart(int lotId, string path)
        {
            return base.DeletePart(lotId, path);
        }

        [Route(@"{lotId}/{*path:regex(^personDocumentApplications$)}")]
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

        [Route(@"{lotId}/{*path:regex(^personDocumentApplications/\d+$)}")]
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
    }
}