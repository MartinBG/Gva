using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.Repositories.InventoryRepository;
using Gva.Api.Repositories.PersonRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/persons")]
    public class PersonsController : GvaLotsController
    {
        private UserContext userContext;
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IInventoryRepository inventoryRepository;
        private IPersonRepository personRepository;

        public PersonsController(
            IUserContextProvider userContextProvider,
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IInventoryRepository inventoryRepository,
            IPersonRepository personRepository)
            : base(lotRepository, userContextProvider, unitOfWork)
        {
            this.userContext = userContextProvider.GetCurrentUserContext();
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.inventoryRepository = inventoryRepository;
            this.personRepository = personRepository;
        }

        [Route("")]
        public IHttpActionResult GetPersons(string lin = null, string uin = null, string names = null, string licences = null, string ratings = null, string organization = null, bool exact = false)
        {
            var persons = this.personRepository.GetPersons(lin, uin, names, licences, ratings, organization, exact);

            return Ok(Mapper.Map<IEnumerable<GvaPerson>, IEnumerable<PersonDO>>(persons));
        }

        [Route("{lotId}")]
        public IHttpActionResult GetPerson(int lotId)
        {
            var person = this.personRepository.GetPerson(lotId);
            return Ok(Mapper.Map<GvaPerson, PersonDO>(person));
        }

        [Route("")]
        public IHttpActionResult PostPerson(JObject person)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var newLot = this.lotRepository.GetSet("Person").CreateLot(this.userContext);

                newLot.CreatePart("personData", person.Value<JObject>("personData"), this.userContext);

                newLot.CreatePart("personDocumentIds/*", person.Value<JObject>("personDocumentId"), this.userContext);


                newLot.CreatePart("personAddresses/*", person.Value<JObject>("personAddress"), this.userContext);

                newLot.Commit(this.userContext);

                transaction.Commit();
            }

            this.unitOfWork.Save();

            return Ok();
        }

        [Route("{lotId}/inventory")]
        public IHttpActionResult GetInventory(int lotId)
        {
            var inventory = this.inventoryRepository.GetInventoryItemsForLot(lotId);

            return Ok(Mapper.Map<IEnumerable<GvaInventoryItem>, IEnumerable<InventoryItemDO>>(inventory));
        }

        [Route(@"{lotId}/{path:regex(^personAddresses/\d+$)}"),
         Route(@"{lotId}/{path:regex(^personData$)}"),
         Route(@"{lotId}/{path:regex(^personFlyingExperiences/\d+$)}"),
         Route(@"{lotId}/{path:regex(^licences/\d+$)}"),
         Route(@"{lotId}/{path:regex(^licences/\d+/editions/\d+$)}"),
         Route(@"{lotId}/{path:regex(^ratings/\d$)}"),
         Route(@"{lotId}/{path:regex(^ratings/\d/editions/\d$)}"),
         Route(@"{lotId}/{path:regex(^personStatuses/\d+$)}")]
        public IHttpActionResult GetPart(int lotId, string path)
        {
            return base.GetPart(lotId, path);
        }

        //public IHttpActionResult GetFilePart(int lotId, string path)
        //{
        //}

        [Route(@"{lotId}/{path:regex(^personAddresses$)}"),
         Route(@"{lotId}/{path:regex(^personFlyingExperiences$)}"),
         Route(@"{lotId}/{path:regex(^licences$)}"),
         Route(@"{lotId}/{path:regex(^licences/\d+/editions$)}"),
         Route(@"{lotId}/{path:regex(^ratings$)}"),
         Route(@"{lotId}/{path:regex(^ratings/\d/editions$)}"),
         Route(@"{lotId}/{path:regex(^personStatuses$)}")]
        public IHttpActionResult GetParts(int lotId, string path)
        {
            return base.GetParts(lotId, path);
        }

        //public IHttpActionResult GetFileParts(int lotId, string path)
        //{
        //}

        [Route(@"{lotId}/{path:regex(^personAddresses$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentChecks$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentEducations$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentEmployments$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentIds$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentMedicals$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentTheoreticalexams$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentTrainings$)}"),
         Route(@"{lotId}/{path:regex(^personFlyingExperiences$)}"),
         Route(@"{lotId}/{path:regex(^licences$)}"),
         Route(@"{lotId}/{path:regex(^licences/\d+/editions$)}"),
         Route(@"{lotId}/{path:regex(^ratings$)}"),
         Route(@"{lotId}/{path:regex(^ratings/\d/editions$)}"),
         Route(@"{lotId}/{path:regex(^personStatuses$)}")]
        public IHttpActionResult PostNewPart(int lotId, string path, JObject content)
        {
            return base.PostNewPart(lotId, path, content);
        }

        [Route(@"{lotId}/{path:regex(^personAddresses/\d+$)}"),
         Route(@"{lotId}/{path:regex(^personData$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentChecks/\d+$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentEducations/\d+$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentEmployments/\d+$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentIds/\d+$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentMedicals/\d+$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentTheoreticalexams\d+$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentTrainings/\d+$)}"),
         Route(@"{lotId}/{path:regex(^personFlyingExperiences/\d+$)}"),
         Route(@"{lotId}/{path:regex(^licences/\d+/editions/\d+$)}"),
         Route(@"{lotId}/{path:regex(^ratings/\d/editions/\d$)}"),
         Route(@"{lotId}/{path:regex(^personStatuses/\d+$)}")]
        public IHttpActionResult PostPart(int lotId, string path, JObject content)
        {
            return base.PostPart(lotId, path, content);
        }

        [Route(@"{lotId}/{path:regex(^personAddresses/\d+$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentChecks/\d+$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentEducations/\d+$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentEmployments/\d+$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentIds/\d+$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentMedicals/\d+$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentTheoreticalexams\d+$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentTrainings/\d+$)}"),
         Route(@"{lotId}/{path:regex(^personFlyingExperiences/\d+$)}"),
         Route(@"{lotId}/{path:regex(^licences/\d+/editions/\d+$)}"),
         Route(@"{lotId}/{path:regex(^ratings/\d/editions/\d$)}"),
         Route(@"{lotId}/{path:regex(^personStatuses/\d+$)}")]
        public IHttpActionResult DeletePart(int lotId, string path)
        {
            return base.DeletePart(lotId, path);
        }
    }
}