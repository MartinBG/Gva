﻿using System.Linq;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Common.Json;
using Common.Filters;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.InventoryRepository;
using Gva.Api.Repositories.PersonRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;
using System.Collections.Generic;
using Gva.Api.Repositories.ApplicationStageRepository;
using System;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons")]
    [Authorize]
    public class PersonsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IInventoryRepository inventoryRepository;
        private IPersonRepository personRepository;
        private IApplicationRepository applicationRepository;
        private IApplicationStageRepository applicationStageRepository;
        private ICaseTypeRepository caseTypeRepository;
        private ILotEventDispatcher lotEventDispatcher;

        public PersonsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IInventoryRepository inventoryRepository,
            IPersonRepository personRepository,
            IApplicationRepository applicationRepository,
            IApplicationStageRepository applicationStageRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.inventoryRepository = inventoryRepository;
            this.personRepository = personRepository;
            this.applicationRepository = applicationRepository;
            this.applicationStageRepository = applicationStageRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.lotEventDispatcher = lotEventDispatcher;
        }

        [Route("new")]
        public IHttpActionResult GetNewPerson(
            string firstName = null,
            string lastName = null,
            string uin = null,
            bool extendedVersion = true)
        {
            PersonDO newPerson = new PersonDO()
                {
                    PersonData = new PersonDataDO()
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Uin = uin
                    }
                };

            if (extendedVersion)
            {
                newPerson.PersonDocumentId = new PersonDocumentIdDO();
                newPerson.PersonAddress = new PersonAddressDO();
            }

            return Ok(newPerson);
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

            return Ok(persons.Select(p => new PersonViewDO(p)));
        }

        [Route("{lotId}")]
        public IHttpActionResult GetPerson(int lotId)
        {
            var person = this.personRepository.GetPerson(lotId);

            return Ok(new PersonViewDO(person));
        }

        [Route("")]
        [Validate]
        public IHttpActionResult PostPerson(PersonDO person)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();
                var newLot = this.lotRepository.CreateLot("Person", userContext);

                newLot.CreatePart("personData", JObject.FromObject(person.PersonData), userContext);
                this.caseTypeRepository.AddCaseTypes(newLot, person.PersonData.CaseTypes.Select(ct => ct.NomValueId));

                if (person.PersonDocumentId != null)
                {
                    var documentIdPart = newLot.CreatePart(
                        "personDocumentIds/*",
                        JObject.FromObject(person.PersonDocumentId),
                        userContext);
                }

                if (person.PersonAddress != null)
                {
                    newLot.CreatePart(
                        "personAddresses/*",
                        JObject.FromObject(person.PersonAddress),
                        userContext);
                }

                newLot.Commit(userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new { id = newLot.LotId });
            }
        }

        [Route(@"{lotId}/personInfo")]
        public IHttpActionResult GetPersonInfo(int lotId)
        {
            var person = this.lotRepository.GetLotIndex(lotId);

            var personDataPart = this.lotRepository.GetLotIndex(lotId).Index.GetPart("personData");
            var inspectorDataPart = this.lotRepository.GetLotIndex(lotId).Index.GetPart("inspectorData");

            return Ok(new PersonInfoDO(personDataPart, inspectorDataPart));
        }

        [Route(@"{lotId}/personInfo")]
        [Validate]
        public IHttpActionResult PostPersonInfo(int lotId, PersonInfoDO personInfo)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();
                var lot = this.lotRepository.GetLotIndex(lotId);

                this.caseTypeRepository.AddCaseTypes(
                    lot,
                    personInfo.PersonData.CaseTypes.Select(ct => ct.NomValueId));
                lot.UpdatePart("personData", JObject.FromObject(personInfo.PersonData), userContext);
                this.unitOfWork.Save();

                var caseTypes = this.caseTypeRepository.GetCaseTypesForLot(lotId);
                var inspectorDataPart = lot.Index.GetPart("inspectorData");

                if (caseTypes.Any(ct => ct.Alias == "inspector"))
                {
                    if (inspectorDataPart == null)
                    {
                        lot.CreatePart("inspectorData", JObject.FromObject(personInfo.InspectorData), userContext);
                    }
                    else
                    {
                        lot.UpdatePart("inspectorData", JObject.FromObject(personInfo.InspectorData), userContext);
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

        [Route("printableDocs")]
        public IHttpActionResult GetPrintableDocs(
            int? licenceType = null,
            int? licenceAction = null,
            int? lin = null,
            string uin = null,
            string names = null)
        {
            var docs = this.personRepository.GetPrintableDocs(licenceType, licenceAction, lin, uin, names);

            return Ok(docs.Select(d => new GvaViewPersonLicenceEditionDO(d)));
        }

        [Route("{lotId}/inventory")]
        public IHttpActionResult GetInventory(int lotId, int? caseTypeId = null)
        {
            var inventory = this.inventoryRepository.GetInventoryItemsForLot(lotId, caseTypeId);
            return Ok(inventory);
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

        [Route("nextLin")]
        public IHttpActionResult GetNextLin(int linTypeId)
        {
            return Ok(new
            {
                NextLin = this.personRepository.GetNextLin(linTypeId)
            });
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

        [HttpGet]
        [Route("stampedDocuments")]
        public IHttpActionResult GetStampedDocuments()
        {
            return Ok(this.personRepository.GetStampedDocuments());
        }

        [HttpPost]
        [Route("stampedDocuments")]
        public IHttpActionResult PostStampedDocuments(JArray stampedDocuments)
        {
            foreach (JObject document in stampedDocuments)
            {
                int applicationId = document.Get<int>("applicationId");
                string stageAlias = document.Get<string>("stageAlias");
                int stageId = this.unitOfWork.DbContext.Set<GvaStage>()
                    .Where(s => s.Alias.Equals(stageAlias))
                    .Single().GvaStageId;

                GvaApplicationStage lastStage = 
                    this.applicationStageRepository.GetApplicationStages(applicationId).Last();

                GvaApplicationStage applicationStage = new GvaApplicationStage() 
                { 
                    GvaStageId = stageId,
                    StartingDate = DateTime.Now,
                    Ordinal = lastStage.Ordinal + 1,
                    GvaApplicationId = applicationId
                };

                this.unitOfWork.DbContext.Set<GvaApplicationStage>().Add(applicationStage);

            }

            this.unitOfWork.Save();

            return Ok();
        }
    }
}