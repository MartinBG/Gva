﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Common.Filters;
using Common.Json;
using Gva.Api.Models;
using Gva.Api.Models.Views.Person;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Common;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.ApplicationStageRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.InventoryRepository;
using Gva.Api.Repositories.PersonRepository;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

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
        private INomRepository nomRepository;
        private IFileRepository fileRepository;
        private ILotEventDispatcher lotEventDispatcher;
        private UserContext userContext;

        public PersonsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IInventoryRepository inventoryRepository,
            IPersonRepository personRepository,
            IApplicationRepository applicationRepository,
            IApplicationStageRepository applicationStageRepository,
            ICaseTypeRepository caseTypeRepository,
            INomRepository nomRepository,
            IFileRepository fileRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.inventoryRepository = inventoryRepository;
            this.personRepository = personRepository;
            this.applicationRepository = applicationRepository;
            this.applicationStageRepository = applicationStageRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.nomRepository = nomRepository;
            this.fileRepository = fileRepository;
            this.lotEventDispatcher = lotEventDispatcher;
            this.userContext = userContext;
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
                NomValue ValidTrue = this.nomRepository.GetNomValue("boolean", "yes");
                newPerson.PersonDocumentId = new CaseTypesPartDO<PersonDocumentIdDO>(new PersonDocumentIdDO()
                {
                    Valid = ValidTrue
                }, new List<CaseDO>());
                newPerson.PersonAddress = new PersonAddressDO()
                {
                    Valid = ValidTrue
                };
            }

            newPerson.PersonData.Country = this.nomRepository.GetNomValue("countries", "BG");

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
            var persons = this.personRepository.GetPersons(lin, linType, uin, caseType, names, licences, ratings, organization, isInspector, isExaminer, exact, 0, null);

            return Ok(persons.Select(p => new PersonViewDO(p)));
        }

        [Route("{lotId}")]
        public IHttpActionResult GetPerson(int lotId)
        {
            var person = this.personRepository.GetPerson(lotId);

            var hasExamSystData = false;
            var examSystDataPartVersion = this.lotRepository.GetLotIndex(lotId).Index.GetPart<PersonExamSystDataDO>("personExamSystData");
            if (examSystDataPartVersion != null)
            {
                hasExamSystData = true;
            }

            return Ok(new PersonViewDO(person, hasExamSystData));
        }

        [Route("")]
        [Validate]
        public IHttpActionResult PostPerson(PersonDO person)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var newLot = this.lotRepository.CreateLot("Person");

                var personDataPart = newLot.CreatePart("personData", person.PersonData, this.userContext);
                this.caseTypeRepository.AddCaseTypes(newLot, person.PersonData.CaseTypes.Select(ct => ct.NomValueId));

                PartVersion<PersonDocumentIdDO> documentIdPart = null;
                if (person.PersonDocumentId != null)
                {
                    documentIdPart = newLot.CreatePart("personDocumentIds/*", person.PersonDocumentId.Part, this.userContext);
                }

                PartVersion<PersonAddressDO> personAddressPart = null;
                if (person.PersonAddress != null)
                {
                    personAddressPart = newLot.CreatePart("personAddresses/*", person.PersonAddress, this.userContext);
                    var cases = this.caseTypeRepository.GetCaseTypesForSet("person")
                        .Select(ct => new CaseDO()
                        {
                            CaseType = new NomValue()
                            {
                                NomValueId = ct.GvaCaseTypeId,
                                Name = ct.Name,
                                Alias = ct.Alias
                            },
                            IsAdded = true
                        });

                    this.fileRepository.AddFileReferences(personAddressPart.Part, cases);
                }

                PartVersion<InspectorDataDO> inspectorDataPart = null;
                if (person.InspectorData != null)
                {
                    inspectorDataPart = newLot.CreatePart("inspectorData", person.InspectorData, this.userContext);
                }

                PartVersion<ExaminerDataDO> examinerDataPart = null;
                if (person.ExaminerData != null)
                {
                    examinerDataPart = newLot.CreatePart("examinerData", person.ExaminerData, this.userContext);
                }

                newLot.Commit(this.userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(personDataPart.PartId);

                if (documentIdPart != null)
                {
                    this.lotRepository.ExecSpSetLotPartTokens(documentIdPart.PartId);
                }

                if (personAddressPart != null)
                {
                    this.lotRepository.ExecSpSetLotPartTokens(personAddressPart.PartId);
                }

                if (inspectorDataPart != null)
                {
                    this.lotRepository.ExecSpSetLotPartTokens(inspectorDataPart.PartId);
                }

                if (examinerDataPart != null)
                {
                    this.lotRepository.ExecSpSetLotPartTokens(examinerDataPart.PartId);
                }

                transaction.Commit();

                return Ok(new { id = newLot.LotId });
            }
        }

        [Route(@"{lotId}/personInfo")]
        public IHttpActionResult GetPersonInfo(int lotId)
        {
            var person = this.lotRepository.GetLotIndex(lotId);

            var personDataPart = this.lotRepository.GetLotIndex(lotId).Index.GetPart<PersonDataDO>("personData");
            var inspectorDataPart = this.lotRepository.GetLotIndex(lotId).Index.GetPart<InspectorDataDO>("inspectorData");
            var examinerDataPart = this.lotRepository.GetLotIndex(lotId).Index.GetPart<ExaminerDataDO>("examinerData");

            return Ok(new PersonInfoDO(personDataPart, inspectorDataPart, examinerDataPart));
        }

        [Route(@"{lotId}/personInfo")]
        [Validate]
        public IHttpActionResult PostPersonInfo(int lotId, PersonInfoDO personInfo)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);

                this.caseTypeRepository.AddCaseTypes(lot, personInfo.PersonData.CaseTypes.Select(ct => ct.NomValueId));

                var personDataPart = lot.UpdatePart("personData", personInfo.PersonData, this.userContext);

                this.unitOfWork.Save();

                var caseTypes = this.caseTypeRepository.GetCaseTypesForLot(lotId);

                var inspectorDataPart = lot.Index.GetPart<InspectorDataDO>("inspectorData");
                PartVersion<InspectorDataDO> changedInspectorDataPart = null;
                if (caseTypes.Any(ct => ct.Alias == "inspector"))
                {
                    if (inspectorDataPart == null)
                    {
                        changedInspectorDataPart = lot.CreatePart("inspectorData", personInfo.InspectorData, this.userContext);
                    }
                    else
                    {
                        changedInspectorDataPart = lot.UpdatePart("inspectorData", personInfo.InspectorData, this.userContext);
                    }
                }
                else if (inspectorDataPart != null)
                {
                    changedInspectorDataPart = lot.DeletePart<InspectorDataDO>("inspectorData", this.userContext);
                }

                var examinerDataPart = lot.Index.GetPart<ExaminerDataDO>("examinerData");
                PartVersion<ExaminerDataDO> changedExaminerDataPart = null;
                if (caseTypes.Any(ct => ct.Alias == "staffExaminer"))
                {
                    if (examinerDataPart == null)
                    {
                        changedExaminerDataPart = lot.CreatePart("examinerData", personInfo.ExaminerData, this.userContext);
                    }
                    else
                    {
                        changedExaminerDataPart = lot.UpdatePart("examinerData", personInfo.ExaminerData, this.userContext);
                    }
                }
                else if (examinerDataPart != null)
                {
                    changedExaminerDataPart = lot.DeletePart<ExaminerDataDO>("examinerData", this.userContext);
                }

                lot.Commit(this.userContext, this.lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(personDataPart.PartId);

                if (changedInspectorDataPart != null)
                {
                    this.lotRepository.ExecSpSetLotPartTokens(changedInspectorDataPart.PartId);
                }

                if (changedExaminerDataPart != null)
                {
                    this.lotRepository.ExecSpSetLotPartTokens(changedExaminerDataPart.PartId);
                }

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
        [Route("isUniqueDocNumber")]
        public IHttpActionResult IsUniqueDocNumber(string documentNumber, int? documentPersonNumber = null, int? partIndex = null)
        {
            bool isUnique = this.personRepository.IsUniqueDocNumber(documentNumber, documentPersonNumber, partIndex);

            if (!isUnique)
            {
                var lastDocumentWithThisNumber = this.unitOfWork.DbContext.Set<GvaViewPersonDocument>()
                    .Where(d => d.DocumentNumber == documentNumber)
                    .OrderByDescending(d => d.DocumentPersonNumber)
                    .FirstOrDefault();

                return Ok(new
                {
                    isUnique = false,
                    lastExistingGroupNumber = lastDocumentWithThisNumber.DocumentPersonNumber
                });
            }
            else
            {
                return Ok(new
                {
                    isUnique = true
                });
            }
        }

        [Route("stampedDocuments")]
        public IHttpActionResult GetStampedDocuments(
            string uin = null,
            string names = null,
            string stampNumber = null,
            int? lin = null,
            int? licenceNumber = null)
        {
            var docs = this.personRepository.GetStampedDocuments(uin, names, stampNumber, lin, licenceNumber);

            return Ok(docs.Select(d => new GvaViewPersonLicenceEditionDO(d)));
        }

        [HttpPost]
        [Route("stampedDocuments")]
        public IHttpActionResult PostStampedDocuments(List<AplicationStageDO> stampedDocuments)
        {
            foreach (AplicationStageDO document in stampedDocuments)
            {
                var applicationStages = this.applicationStageRepository.GetApplicationStages(document.ApplicationId);
                int lastStageOrdinal = applicationStages.Last().Ordinal;

                var application = this.applicationRepository.GetApplicationById(document.ApplicationId);
                int? documentDuration = application.ApplicationType.TextContent.Get<int?>("duration");

                List<string> newStagesAliases = this.unitOfWork.DbContext.Set<GvaStage>()
                        .Where(s => document.StageAliases.Contains(s.Alias))
                        .OrderBy(s => s.GvaStageId)
                        .Select(s => s.Alias)
                        .ToList();

                foreach (string stageAlias in newStagesAliases)
                {
                    var stage = this.unitOfWork.DbContext.Set<GvaStage>()
                        .Where(s => s.Alias.Equals(stageAlias))
                        .Single();

                    lastStageOrdinal++;
                    var stageTermDate = this.applicationStageRepository.GetApplicationTermDate(document.ApplicationId, stage.GvaStageId);

                    GvaApplicationStage applicationStage = new GvaApplicationStage()
                    {
                        GvaStageId = stage.GvaStageId,
                        StartingDate = DateTime.Now,
                        Ordinal = lastStageOrdinal,
                        GvaApplicationId = document.ApplicationId,
                        StageTermDate = stageTermDate
                    };

                    this.unitOfWork.DbContext.Set<GvaApplicationStage>().Add(applicationStage);
                }

            }

            this.unitOfWork.Save();

            return Ok();
        }

        [HttpGet]
        [Route("isFclLicence")]
        public IHttpActionResult IsFclLicence(int licenceTypeId)
        {
            return Ok(
                new
                {
                    isFcl = this.personRepository.IsFclLicence(licenceTypeId)
                });
        }

        [HttpGet]
        [Route("getChecksForReport")]
        public IHttpActionResult GetChecksForReport([FromUri] List<int> checks = null)
        {
            return Ok(this.personRepository.GetChecksForReport(checks));
        }
    }
}