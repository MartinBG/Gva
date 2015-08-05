using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.PersonTrainingRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/personDocumentTrainings")]
    [Authorize]
    public class PersonTrainingsController : GvaCaseTypePartController<PersonTrainingDO>
    {
        private INomRepository nomRepository;
        private IPersonTrainingRepository personTrainingRepository;
        private ICaseTypeRepository caseTypeRepository;
        private ILotRepository lotRepository;
        private IFileRepository fileRepository;
        private string path;

        public PersonTrainingsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            INomRepository nomRepository,
            ICaseTypeRepository caseTypeRepository,
            IPersonTrainingRepository personTrainingRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personDocumentTrainings", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
            this.personTrainingRepository = personTrainingRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
            this.path = "personDocumentTrainings";
        }

        [Route("new")]
        public IHttpActionResult GetNewTraining(int lotId, int? caseTypeId = null)
        {
            PersonTrainingDO newTraining = new PersonTrainingDO()
            {
                DocumentDateValidFrom = DateTime.Now,
                ValidId = this.nomRepository.GetNomValue("boolean", "yes").NomValueId
            };

            CaseDO caseDO = null;
            if (caseTypeId.HasValue)
            {
                GvaCaseType caseType = this.caseTypeRepository.GetCaseType(caseTypeId.Value);
                caseDO = new CaseDO()
                {
                    CaseType = new NomValue()
                    {
                        NomValueId = caseType.GvaCaseTypeId,
                        Name = caseType.Name,
                        Alias = caseType.Alias
                    }
                };
            }

            return Ok(new CaseTypePartDO<PersonTrainingDO>(newTraining, caseDO));
        }

        [Route("exams")]
        public IHttpActionResult GetExams(int lotId, int? caseTypeId = null)
        {
            int roleExamId = this.nomRepository.GetNomValue("documentRoles", "exam").NomValueId;
            var examViewDOs = this.personTrainingRepository.GetTrainings(lotId, caseTypeId, roleExamId);

            return Ok(examViewDOs);
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            var trainingViewDOs = this.personTrainingRepository.GetTrainings(lotId, caseTypeId);
            return Ok(trainingViewDOs);
        }

        [Route("{partIndex}/view")]
        public IHttpActionResult GetCheck(int lotId, int partIndex)
        {
            var trainingPartVersion = this.lotRepository.GetLotIndex(lotId).Index.GetPart<PersonCheckDO>(string.Format("{0}/{1}", this.path, partIndex));
            var lotFile = this.fileRepository.GetFileReference(trainingPartVersion.PartId, null);

            PersonTrainingViewDO training = new PersonTrainingViewDO()
            {
                Case = lotFile != null ? new CaseDO(lotFile) : null,
                PartIndex = trainingPartVersion.Part.Index,
                PartId = trainingPartVersion.PartId,
                DocumentDateValidFrom = trainingPartVersion.Content.DocumentDateValidFrom,
                DocumentDateValidTo = trainingPartVersion.Content.DocumentDateValidTo,
                AircraftTypeGroup = trainingPartVersion.Content.AircraftTypeGroupId.HasValue ? this.nomRepository.GetNomValue("aircraftTypeGroups", trainingPartVersion.Content.AircraftTypeGroupId.Value) : null,
                DocumentNumber = trainingPartVersion.Content.DocumentNumber,
                DocumentPublisher = trainingPartVersion.Content.DocumentPublisher,
                Notes = trainingPartVersion.Content.Notes,
                Valid = trainingPartVersion.Content.ValidId.HasValue ? this.nomRepository.GetNomValue("boolean", trainingPartVersion.Content.ValidId.Value) : null,
                DocumentType = trainingPartVersion.Content.DocumentTypeId.HasValue ? this.nomRepository.GetNomValue("documentTypes", trainingPartVersion.Content.DocumentTypeId.Value) : null,
                DocumentRole = trainingPartVersion.Content.DocumentRoleId.HasValue ? this.nomRepository.GetNomValue("documentRoles", trainingPartVersion.Content.DocumentRoleId.Value) : null,
                AircraftTypeCategory = trainingPartVersion.Content.AircraftTypeCategoryId.HasValue ? this.nomRepository.GetNomValue("aircraftClases66", trainingPartVersion.Content.AircraftTypeCategoryId.Value) : null,
                Authorization = trainingPartVersion.Content.AuthorizationId.HasValue ? this.nomRepository.GetNomValue("authorizations", trainingPartVersion.Content.AuthorizationId.Value) : null,
                RatingClass = trainingPartVersion.Content.RatingClassId.HasValue ? this.nomRepository.GetNomValue("ratingClasses", trainingPartVersion.Content.RatingClassId.Value) : null,
                LicenceType = trainingPartVersion.Content.LicenceTypeId.HasValue ? this.nomRepository.GetNomValue("licenceTypes", trainingPartVersion.Content.LicenceTypeId.Value) : null,
                LocationIndicator = trainingPartVersion.Content.LocationIndicatorId.HasValue ? this.nomRepository.GetNomValue("locationIndicators", trainingPartVersion.Content.LocationIndicatorId.Value) : null,
                Sector = trainingPartVersion.Content.Sector,
                DocumentPersonNumber = trainingPartVersion.Content.DocumentPersonNumber,
                RatingTypes = trainingPartVersion.Content.RatingTypes.Count > 0 ? this.nomRepository.GetNomValues("ratingTypes", trainingPartVersion.Content.RatingTypes.ToArray()).ToList() : null
            };

            return Ok(training);
        }
    }
}