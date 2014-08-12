using System.Linq;
using System.Web.Http;
using Common.Api.Repositories.NomRepository;
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
        private INomRepository nomRepository;

        public PersonsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IInventoryRepository inventoryRepository,
            IPersonRepository personRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher,
            INomRepository nomRepository)
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
            this.nomRepository = nomRepository;
        }

        [Route("")]
        public IHttpActionResult GetPersons(
            int? lin = null,
            string linType = null,
            string uin = null,
            int? caseType = null,
            string names = null,
            string licences = null,
            string ratings = null,
            string organization = null,
            bool isInspector = false,
            bool isExaminer = false,
            bool exact = false)
        {
            var persons = this.personRepository.GetPersons(lin, linType, uin, caseType, names, licences, ratings, organization, isInspector, isExaminer, exact, 0, 1000);

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
            if (gvaNomApp != null && gvaNomApp.GvaAppLotPart != null)
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
                var newLot = this.lotRepository.CreateLot("Person", userContext);

                newLot.CreatePart("personData", person.Get<JObject>("personData"), userContext);
                this.caseTypeRepository.AddCaseTypes(newLot, person.GetItems<JObject>("personData.caseTypes").Select(ct => ct.Get<int>("nomValueId")));

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
            var inventory = this.inventoryRepository.GetInventoryItemsForLot(lotId, caseTypeId);
            return Ok(inventory);
        }

        [Route(@"{lotId}/personInfo")]
        public IHttpActionResult GetPersonInfo(int lotId)
        {
            var person = this.lotRepository.GetLotIndex(lotId);

            var personDataPart = this.lotRepository.GetLotIndex(lotId).Index.GetPart("personData");
            var inspectorDataPart = this.lotRepository.GetLotIndex(lotId).Index.GetPart("inspectorData");

            return Ok(new PersonInfoDO(personDataPart, inspectorDataPart));
        }

        [Route(@"{lotId}/{*path:regex(^personDocumentIds/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentEducations/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentEmployments/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentExams/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentOthers/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentApplications/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personExams/\d+$)}")]
        public override IHttpActionResult GetFilePart(int lotId, string path, int? caseTypeId = null)
        {
            return base.GetFilePart(lotId, path, caseTypeId);
        }

        [Route(@"{lotId}/{path:regex(^personDocumentIds$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentEducations$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentEmployments$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentExams$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentOthers$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentApplications$)}"),
         Route(@"{lotId}/{*path:regex(^personExams$)}")]
        public override IHttpActionResult GetFileParts(int lotId, string path, int? caseTypeId = null)
        {
            return base.GetFileParts(lotId, path, caseTypeId);
        }


        [Route("nextLin")]
        public IHttpActionResult GetNextLin(int linTypeId)
        {
            return Ok(new
            {
                NextLin = this.personRepository.GetNextLin(linTypeId)
            });
        }

        [Route(@"{lotId}/{*path:regex(^personDocumentEducations$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentEmployments$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentIds$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentExams$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentOthers$)}"),
         Route(@"{lotId}/{*path:regex(^personExams$)}")]
        public override IHttpActionResult PostNewPart(int lotId, string path, JObject content)
        {
            return base.PostNewPart(lotId, path, content);
        }

        [Route(@"{lotId}/{*path:regex(^personDocumentEducations/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentEmployments/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentIds/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentExams/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentOthers/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentApplications/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personExams/\d+$)}")]
        public override IHttpActionResult PostPart(int lotId, string path, JObject content)
        {
            return base.PostPart(lotId, path, content);
        }

        [Route(@"{lotId}/personInfo")]
        public IHttpActionResult PostPersonInfo(int lotId, JObject content)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();
                var lot = this.lotRepository.GetLotIndex(lotId);

                this.caseTypeRepository.AddCaseTypes(lot, content.GetItems<JObject>("personData.part.caseTypes").Select(ct => ct.Get<int>("nomValueId")));
                lot.UpdatePart("personData", content.Get<JObject>("personData.part"), userContext);
                this.unitOfWork.Save();

                var caseTypes = this.caseTypeRepository.GetCaseTypesForLot(lotId);
                var inspectorDataPart = lot.Index.GetPart("inspectorData");

                if (caseTypes.Any(ct => ct.Alias == "inspector"))
                {
                    if (inspectorDataPart == null)
                    {
                        lot.CreatePart("inspectorData", content.Get<JObject>("inspectorData.part"), userContext);
                    }
                    else
                    {
                        lot.UpdatePart("inspectorData", content.Get<JObject>("inspectorData.part"), userContext);
                    }
                }
                else if (inspectorDataPart != null)
                {
                    lot.DeletePart("inspectorData", userContext);
                }

                lot.Commit(userContext, this.lotEventDispatcher);
                this.unitOfWork.Save();

                transaction.Commit();
            }

            return Ok();
        }

        [Route(@"{lotId}/{*path:regex(^personDocumentEducations/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentEmployments/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentIds/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentExams/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentOthers/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personExams/\d+$)}")]
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
                var partVersion = this.lotRepository.GetLotIndex(lotId).Index.GetPart(path);

                applicationRepository.DeleteGvaApplication(partVersion.Part.PartId);

                result = base.DeletePart(lotId, path);

                transaction.Commit();
            }

            this.unitOfWork.Save();

            return result;
        }

        [HttpGet]
        [Route("isUniqueUin")]
        public IHttpActionResult IsUniqueUin(string uin, int? personId = null)
        {
            return Ok(new
            {
                isUnique = this.personRepository.IsUniqueUin(uin, personId)
            });
        }
    }
}