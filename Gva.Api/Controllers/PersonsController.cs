using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;
using AutoMapper;
using Common.Api.Models;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.Mappers.Resolvers;
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
            ILotEventDispatcher lotEventDispatcher,
            FileResolver fileResolver)
            : base(lotRepository, fileRepository, unitOfWork, lotEventDispatcher, fileResolver)
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
                UserContext userContext = this.Request.GetUserContext();
                var newLot = this.lotRepository.GetSet("Person").CreateLot(userContext);

                dynamic personData = person.Value<JObject>("personData");
                newLot.CreatePart("personData", personData, userContext);
                this.caseTypeRepository.AddCaseTypes(newLot, personData.Value<JArray>("caseTypes"));

                var documentIdPart = newLot.CreatePart("personDocumentIds/*", person.Value<JObject>("personDocumentId"), userContext);
                this.fileRepository.AddFileReferences(documentIdPart, null);

                newLot.CreatePart("personAddresses/*", person.Value<JObject>("personAddress"), userContext);

                newLot.Commit(userContext, lotEventDispatcher);

                transaction.Commit();
            }

            this.unitOfWork.Save();

            return Ok();
        }

        [Route("{lotId}/inventory")]
        public IHttpActionResult GetInventory(int lotId, int? caseTypeId = null)
        {
            var inventoryItems = this.inventoryRepository.GetInventoryItemsForLot(lotId);

            List<Tuple<GvaInventoryItem, GvaLotFile>> inventory;
            if (caseTypeId.HasValue)
            {
                var lotFiles = this.fileRepository.GetFileReferencesForLot(lotId, caseTypeId.Value);

                inventory = inventoryItems
                    .Join(
                        lotFiles,
                        i => i.PartId,
                        f => f.LotPartId,
                        (i, f) => Tuple.Create(i, f))
                    .ToList();
            }
            else
            {
                inventory = new List<Tuple<GvaInventoryItem, GvaLotFile>>();
                foreach (var inventoryItem in inventoryItems)
                {
                    var lotFiles = this.fileRepository.GetFileReferences(inventoryItem.PartId, null);

                    if (lotFiles.Length == 0)
                    {
                        inventory.Add(Tuple.Create<GvaInventoryItem, GvaLotFile>(inventoryItem, null));
                    }

                    foreach (var lotFile in lotFiles)
                    {
                        inventory.Add(Tuple.Create(inventoryItem, lotFile));
                    }
                }
            }

            return Ok(Mapper.Map<List<InventoryItemDO>>(inventory));
        }

        [Route("{lotId}/applications")]
        public IHttpActionResult GetApplications(int lotId, string term = null)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);

            var applications = Mapper.Map<GvaApplication[], ApplicationNomDO[]>(this.applicationRepository.GetNomApplications(lotId));

            if (!string.IsNullOrWhiteSpace(term))
            {
                term = term.ToLower();
                applications = applications.Where(n => n.ApplicationName.ToLower().Contains(term)).ToArray();
            }

            return Ok(applications);
        }

        [Route("~/api/nomenclatures/personCaseTypes")]
        public IHttpActionResult GetCaseTypes(int? lotId = null, string term = null, int? id = null)
        {
            if (id.HasValue)
            {
                var caseType = this.caseTypeRepository.GetCaseType(id.Value);
                return Ok(Mapper.Map<GvaCaseType, NomValue>(caseType));
            }

            IEnumerable<GvaCaseType> caseTypes;
            if (lotId.HasValue)
            {
                caseTypes = this.caseTypeRepository.GetCaseTypesForLot(lotId.Value);
            }
            else
            {
                var set = this.lotRepository.GetSet("Person");
                caseTypes = this.caseTypeRepository.GetCaseTypesForSet(set.SetId);
            }

            if (!string.IsNullOrWhiteSpace(term))
            {
                term = term.ToLower();
                caseTypes = caseTypes.Where(ct => ct.Name.ToLower().Contains(term));
            }

            return Ok(Mapper.Map<IEnumerable<GvaCaseType>, IEnumerable<NomValue>>(caseTypes));
        }

        [Route(@"{lotId}/{*path:regex(^personAddresses/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personData$)}"),
         Route(@"{lotId}/{*path:regex(^personFlyingExperiences/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^licences/\d+/editions/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^ratings/\d+/editions/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personStatuses/\d+$)}")]
        public IHttpActionResult GetPart(int lotId, string path)
        {
            return base.GetPart(lotId, path);
        }

        [Route(@"{lotId}/{*path:regex(^ratings/\d+$)}")]
        public IHttpActionResult GetRatingPart(int lotId, string path)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);

            var part = lot.GetPart(path);
            var firstEdition = lot.GetParts(path + "/editions").FirstOrDefault();

            return Ok(Mapper.Map<RatingPartVersionDO>(Tuple.Create<PartVersion, PartVersion, PartVersion>(part, firstEdition, null)));
        }

        [Route(@"{lotId}/{*path:regex(^personDocumentIds/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentChecks/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentEducations/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentEmployments/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentMedicals/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentTheoreticalexams/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentTrainings/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentOthers/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentApplications/\d+$)}")]
        public IHttpActionResult GetFilePart(int lotId, string path, int? caseTypeId = null)
        {
            return base.GetFilePart(lotId, path, caseTypeId);
        }

        [Route(@"{lotId}/{*path:regex(^personAddresses$)}"),
         Route(@"{lotId}/{*path:regex(^personFlyingExperiences$)}"),
         Route(@"{lotId}/{*path:regex(^licences/\d+/editions$)}"),
         Route(@"{lotId}/{*path:regex(^ratings/\d+/editions$)}"),
         Route(@"{lotId}/{*path:regex(^personStatuses$)}")]
        public IHttpActionResult GetParts(int lotId, string path)
        {
            return base.GetParts(lotId, path);
        }

        //Route(@"{lotId}/{path:regex(^licences$)}"),
        [Route(@"{lotId}/{path:regex(^ratings$)}")]
        public IHttpActionResult GetRatingParts(int lotId, string path)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            var parts = lot.GetParts(path);

            var result = new List<RatingPartVersionDO>();
            foreach (var part in parts)
            {
                var partEditions = lot.GetParts(part.Part.Path + "/editions");

                result.Add(Mapper.Map<RatingPartVersionDO>(
                    Tuple.Create(
                        part,
                        partEditions.FirstOrDefault(),
                        partEditions.LastOrDefault())));
            }

            return Ok(result);
        }

        [Route(@"{lotId}/{path:regex(^personDocumentIds$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentChecks$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentEducations$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentEmployments$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentMedicals$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentTheoreticalexams$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentTrainings$)}"),
         Route(@"{lotId}/{path:regex(^personDocumentOthers$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentApplications$)}")]
        public IHttpActionResult GetFileParts(int lotId, string path, int? caseTypeId = null)
        {
            return base.GetFileParts(lotId, path, caseTypeId);
        }

        [Route(@"{lotId}/{*path:regex(^personAddresses$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentChecks$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentEducations$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentEmployments$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentIds$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentMedicals$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentTheoreticalexams$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentTrainings$)}"),
         Route(@"{lotId}/{*path:regex(^personFlyingExperiences$)}"),
         Route(@"{lotId}/{*path:regex(^licences/\d+/editions$)}"),
         Route(@"{lotId}/{*path:regex(^ratings/\d+/editions$)}"),
         Route(@"{lotId}/{*path:regex(^personStatuses$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentOthers$)}")]
        public IHttpActionResult PostNewPart(int lotId, string path, JObject content)
        {
            return base.PostNewPart(lotId, path, content);
        }

        //[Route(@"{lotId}/{path:regex(^licences$)}")]
        [Route(@"{lotId}/{path:regex(^ratings$)}")]
        public IHttpActionResult PostNewRating(int lotId, string path, dynamic content)
        {
            UserContext userContext = this.Request.GetUserContext();
            var lot = this.lotRepository.GetLotIndex(lotId);

            PartVersion partVersion = lot.CreatePart(path + "/*", content.rating.part, userContext);

            lot.CreatePart(
                string.Format("{0}/{1}/editions/*", path, partVersion.Part.Index),
                content.ratingEdition.part,
                userContext);

            lot.Commit(userContext, lotEventDispatcher);
            this.unitOfWork.Save();

            return Ok();
        }

        [Route(@"{lotId}/{*path:regex(^personAddresses/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentChecks/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentEducations/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentEmployments/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentIds/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentMedicals/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentTheoreticalexams\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentTrainings/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personFlyingExperiences/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^licences/\d+/editions/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^ratings/\d+/editions/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personStatuses/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentOthers/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentApplications/\d+$)}")]
        public IHttpActionResult PostPart(int lotId, string path, JObject content)
        {
            return base.PostPart(lotId, path, content);
        }

        [Route(@"{lotId}/{*path:regex(^personData$)}")]
        public IHttpActionResult PostPersonData(int lotId, string path, JObject content)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            this.caseTypeRepository.AddCaseTypes(lot, (content as dynamic).part.caseTypes);

            return base.PostPart(lotId, path, content);
        }

        [Route(@"{lotId}/{*path:regex(^personAddresses/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentChecks/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentEducations/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentEmployments/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentIds/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentMedicals/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentTheoreticalexams\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentTrainings/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personFlyingExperiences/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personStatuses/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^personDocumentOthers/\d+$)}")]
        public IHttpActionResult DeletePart(int lotId, string path)
        {
            return base.DeletePart(lotId, path);
        }

        [Route(@"{lotId}/{*path:regex(^licences/\d+/editions/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^ratings/\d+/editions/\d+$)}")]
        public IHttpActionResult DeleteEdition(int lotId, string path)
        {
            UserContext userContext = this.Request.GetUserContext();
            var lot = this.lotRepository.GetLotIndex(lotId);
            lot.DeletePart(path, userContext, true);

            if (lot.GetParts(Regex.Match(path, @".+/\d+/[^/\d]+").Value).Count() == 0)
            {
                lot.DeletePart(Regex.Match(path, @"[^/\d]+/\d+").Value, userContext);
            }

            lot.Commit(userContext, lotEventDispatcher);

            this.unitOfWork.Save();

            return Ok();
        }

        [Route(@"{lotId}/{*path:regex(^personDocumentApplications$)}")]
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