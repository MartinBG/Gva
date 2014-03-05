using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Common.Api.Repositories.UserRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.Repositories.PersonRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/persons")]
    public class PersonsController : GvaLotsController
    {
        private UserContext userContext;
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IUserRepository userRepository;
        private IPersonRepository personRepository;

        public PersonsController(
            IUserContextProvider userContextProvider,
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IUserRepository userRepository,
            IPersonRepository personRepository)
            : base(lotRepository, userContextProvider, unitOfWork)
        {
            this.userContext = userContextProvider.GetCurrentUserContext();
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.userRepository = userRepository;
            this.personRepository = personRepository;
        }

        [Route("")]
        public HttpResponseMessage GetPersons(string lin = null, string uin = null, string names = null, string licences = null, string ratings = null, string organization = null, bool exact = false)
        {
            var persons = this.personRepository.GetPersons(lin, uin, names, licences, ratings, organization, exact);

            return ControllerContext.Request.CreateResponse(
                HttpStatusCode.OK,
                Mapper.Map<IEnumerable<GvaPerson>, IEnumerable<PersonDO>>(persons));
        }

        [Route("{lotId}")]
        public HttpResponseMessage GetPerson(int lotId)
        {
            var person = this.personRepository.GetPerson(lotId);
            return ControllerContext.Request.CreateResponse(
                HttpStatusCode.OK,
                Mapper.Map<GvaPerson, PersonDO>(person));
        }

        [Route("")]
        public HttpResponseMessage PostPerson(JObject person)
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

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage GetInventory(int lotId)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);

            IEnumerable<PartVersion> partVersions = new List<PartVersion>()
                .Concat(lot.GetParts("personDocumentEducations"))
                .Concat(lot.GetParts("personDocumentIds"))
                .Concat(lot.GetParts("personDocumentTrainings"))
                .Concat(lot.GetParts("personDocumentMedicals"))
                .Concat(lot.GetParts("personDocumentChecks"));

            IList<InventoryItemDO> inventory = new List<InventoryItemDO>();
            foreach (var partVersion in partVersions)
            {
                JObject content = partVersion.Content.Value<JObject>("part");
                JObject file = partVersion.Content.Value<JObject>("file");
                JToken valid;
                JToken publisher;

                Commit commit = lot.Commits.FirstOrDefault(c => c.IsIndex == true);
                PartVersion firstPartVersion = null;
                while (firstPartVersion == null)
                {
                    PartVersion currentPartVersion = lot.GetPart(partVersion.Part.Path, commit.CommitId);
                    if (currentPartVersion.PartOperation == PartOperation.Add)
                    {
                        firstPartVersion = currentPartVersion;
                    }
                    else
                    {
                        commit = this.lotRepository.LoadCommit(commit.ParentCommitId);
                    }
                }

                var partAlias = partVersion.Part.SetPart.Alias;
                InventoryItemDO inventoryItem = new InventoryItemDO()
                {
                    DocumentType = partAlias,
                    PartIndex = partVersion.Part.Index.Value,
                    BookPageNumber = content.Value<string>("bookPageNumber"),
                    Number = content.Value<string>("documentNumber"),
                    Date = content.Value<DateTime?>("documentDateValidFrom") ?? content.Value<DateTime?>("completionDate"),
                    Publisher = content.TryGetValue("documentPublisher", out publisher) ?
                        (publisher.Type == JTokenType.Object ? publisher.Value<string>("name") : publisher.ToString()) :
                        content.Value<JObject>("school").Value<string>("name"),
                    Valid = content.TryGetValue("valid", out valid) ? valid.Value<string>("name") : null,
                    FromDate = content.Value<DateTime?>("documentDateValidFrom"),
                    ToDate = content.Value<DateTime?>("documentDateValidTo"),
                    PageCount = content.Value<int?>("pageCount"),
                    CreatedBy = this.userRepository.GetUser(firstPartVersion.CreatorId).Fullname,
                    CreationDate = firstPartVersion.CreateDate
                };

                if (partVersion.PartOperation == PartOperation.Update)
                {
                    inventoryItem.EditedDate = partVersion.CreateDate;
                    inventoryItem.EditedBy = this.userRepository.GetUser(partVersion.CreatorId).Fullname;
                }

                if (partAlias == "education")
                {
                    inventoryItem.Name = "Образование";
                    inventoryItem.Type = content.Value<JObject>("graduation").Value<string>("name");
                }
                else if (partAlias == "documentId")
                {
                    inventoryItem.Name = "Документ за самоличност";
                    inventoryItem.Type = content.Value<JObject>("personDocumentIdType").Value<string>("name");
                }
                else if (partAlias == "training")
                {
                    inventoryItem.Name = content.Value<JObject>("personOtherDocumentRole").Value<string>("name");
                    inventoryItem.Type = content.Value<JObject>("personOtherDocumentType").Value<string>("name");
                }
                else if (partAlias == "medical")
                {
                    inventoryItem.Name = "Медицинско свидетелство";
                    inventoryItem.Number = string.Format(
                        "{0}-{1}-{2}-{3}",
                        content.Value<string>("documentNumberPrefix"),
                        content.Value<string>("documentNumber"),
                        lot.GetPart("personData").Content.Value<string>("lin"),
                        content.Value<string>("documentNumberSuffix"));
                }
                else if (partAlias == "check")
                {
                    inventoryItem.Name = content.Value<JObject>("personCheckDocumentRole").Value<string>("name");
                    inventoryItem.Type = content.Value<JObject>("personCheckDocumentType").Value<string>("name");
                }

                if (file != null)
                {
                    inventoryItem.File = new FileDO()
                    {
                        Key = file.Value<string>("key"),
                        Name = file.Value<string>("name")
                    };
                }

                inventory.Add(inventoryItem);
            }

            return ControllerContext.Request.CreateResponse(
                HttpStatusCode.OK,
                inventory);
        }

        [Route(@"{lotId}/{path:regex(^personAddresses/\d+$)}"),
         Route(@"{lotId}/{path:regex(^personData$)}"),
         Route(@"{lotId}/{path:regex(^personFlyingExperiences/\d+$)}"),
         Route(@"{lotId}/{path:regex(^licence$)}"),
         Route(@"{lotId}/{path:regex(^rating$)}"),
         Route(@"{lotId}/{path:regex(^personStatuses/\d+$)}")]
        public HttpResponseMessage GetPart(int lotId, string path)
        {
            return base.GetPart(lotId, path);
        }

        //public HttpResponseMessage GetFilePart(int lotId, string path)
        //{
        //}

        [Route(@"{lotId}/{path:regex(^personAddresses$)}"),
         Route(@"{lotId}/{path:regex(^personFlyingExperiences$)}"),
         Route(@"{lotId}/{path:regex(^licence$)}"),
         Route(@"{lotId}/{path:regex(^rating$)}"),
         Route(@"{lotId}/{path:regex(^personStatuses$)}")]
        public HttpResponseMessage GetParts(int lotId, string path)
        {
            var parts = this.lotRepository.GetLotIndex(lotId).GetParts(path);

            return ControllerContext.Request.CreateResponse(
                HttpStatusCode.OK,
                Mapper.Map<PartVersion[], PartVersionDO[]>(parts));
        }

        //public HttpResponseMessage GetFileParts(int lotId, string path)
        //{
        //}

        [Route(@"{lotId}/{path:regex(^personAddresses$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentChecks$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentEducations$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentEmployments$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentIds$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentMedicals$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentTrainings$)}"),
         Route(@"{lotId}/{path:regex(^personFlyingExperiences$)}"),
         Route(@"{lotId}/{path:regex(^licence$)}"),
         Route(@"{lotId}/{path:regex(^rating$)}"),
         Route(@"{lotId}/{path:regex(^personStatuses$)}")]
        public HttpResponseMessage PostNewPart(int lotId, string path, JObject content)
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
         Route(@"{lotId}/{path:regex(^personDocumentTrainings/\d+$)}"),
         Route(@"{lotId}/{path:regex(^personFlyingExperiences/\d+$)}"),
         Route(@"{lotId}/{path:regex(^licence$)}"),
         Route(@"{lotId}/{path:regex(^rating$)}"),
         Route(@"{lotId}/{path:regex(^personStatuses/\d+$)}")]
        public HttpResponseMessage PostPart(int lotId, string path, JObject content)
        {
            return base.PostPart(lotId, path, content);
        }

        [Route(@"{lotId}/{path:regex(^personAddresses/\d+$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentChecks/\d+$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentEducations/\d+$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentEmployments/\d+$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentIds/\d+$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentMedicals/\d+$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentTrainings/\d+$)}"),
         Route(@"{lotId}/{path:regex(^personFlyingExperiences/\d+$)}"),
         Route(@"{lotId}/{path:regex(^licence$)}"),
         Route(@"{lotId}/{path:regex(^rating$)}"),
         Route(@"{lotId}/{path:regex(^personStatuses/\d+$)}")]
        public HttpResponseMessage DeletePart(int lotId, string path)
        {
            return base.DeletePart(lotId, path);
        }
    }
}