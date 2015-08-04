using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using Common.Data;
using Common.Linq;
using Docs.Api.Models;
using Gva.Api.Models;
using Gva.Api.Models.Views;
using Gva.Api.ModelsDO;
using System;
using Gva.Api.Models.Views.Person;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.Repositories.LotRepositories;
using Gva.Api.Repositories.FileRepository;
using Common.Api.Repositories.NomRepository;

namespace Gva.Api.Repositories.PersonTrainingRepository
{
    public class PersonTrainingRepository : IPersonTrainingRepository
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IFileRepository fileRepository;
        private INomRepository nomRepository;

        public PersonTrainingRepository(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            INomRepository nomRepository)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
            this.nomRepository = nomRepository;
        }

        public List<PersonTrainingViewDO> GetTrainings(int lotId, int? caseTypeId = null, int? roleId = null)
        {
            var trainings = this.lotRepository.GetLotIndex(lotId).Index.GetParts<PersonTrainingDO>("personDocumentTrainings")
                .Where(d => roleId.HasValue ? d.Content.DocumentRoleId == roleId.Value : true);

            List<PersonTrainingViewDO> trainingViewDOs = new List<PersonTrainingViewDO>();
            foreach (var trainingPartVersion in trainings)
            {
                var lotFile = this.fileRepository.GetFileReference(trainingPartVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFile != null)
                {
                    trainingViewDOs.Add(new PersonTrainingViewDO()
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
                    });
                }
            }

            return trainingViewDOs;
        }
    }
}
