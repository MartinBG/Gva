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

namespace Gva.Api.Repositories.PersonLangCertRepository
{
    public class PersonLangCertRepository : IPersonLangCertRepository
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IFileRepository fileRepository;
        private INomRepository nomRepository;

        public PersonLangCertRepository(
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

        public List<PersonLangCertViewDO> GetLangCerts(int lotId, int? caseTypeId = null, bool? valid = null)
        {
            var langCerts = this.lotRepository.GetLotIndex(lotId).Index.GetParts<PersonLangCertDO>("personDocumentLangCertificates").ToList();
            if (valid.HasValue && valid.Value == true)
            {
                langCerts = langCerts
                    .Where(e => !e.Content.DocumentDateValidTo.HasValue || DateTime.Compare(e.Content.DocumentDateValidTo.Value, DateTime.Now) >= 0)
                    .ToList();
            }

            List<PersonLangCertViewDO> langCertViewDOs = new List<PersonLangCertViewDO>();
            foreach (var langCertPartVersion in langCerts)
            {
                var lotFile = this.fileRepository.GetFileReference(langCertPartVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFile != null)
                {
                    langCertViewDOs.Add(new PersonLangCertViewDO()
                    {
                        Case = lotFile != null ? new CaseDO(lotFile) : null,
                        PartIndex = langCertPartVersion.Part.Index,
                        PartId = langCertPartVersion.PartId,
                        DocumentDateValidFrom = langCertPartVersion.Content.DocumentDateValidFrom,
                        DocumentDateValidTo = langCertPartVersion.Content.DocumentDateValidTo,
                        AircraftTypeGroup = langCertPartVersion.Content.AircraftTypeGroupId.HasValue ? this.nomRepository.GetNomValue("aircraftTypeGroups", langCertPartVersion.Content.AircraftTypeGroupId.Value) : null,
                        DocumentNumber = langCertPartVersion.Content.DocumentNumber,
                        DocumentPublisher = langCertPartVersion.Content.DocumentPublisher,
                        Notes = langCertPartVersion.Content.Notes,
                        Valid = langCertPartVersion.Content.ValidId.HasValue ? this.nomRepository.GetNomValue("boolean", langCertPartVersion.Content.ValidId.Value) : null,
                        DocumentType = langCertPartVersion.Content.DocumentTypeId.HasValue ? this.nomRepository.GetNomValue("documentTypes", langCertPartVersion.Content.DocumentTypeId.Value) : null,
                        DocumentRole = langCertPartVersion.Content.DocumentRoleId.HasValue ? this.nomRepository.GetNomValue("documentRoles", langCertPartVersion.Content.DocumentRoleId.Value) : null,
                        AircraftTypeCategory = langCertPartVersion.Content.AircraftTypeCategoryId.HasValue ? this.nomRepository.GetNomValue("aircraftClases66", langCertPartVersion.Content.AircraftTypeCategoryId.Value) : null,
                        Authorization = langCertPartVersion.Content.AuthorizationId.HasValue ? this.nomRepository.GetNomValue("authorizations", langCertPartVersion.Content.AuthorizationId.Value) : null,
                        RatingClass = langCertPartVersion.Content.RatingClassId.HasValue ? this.nomRepository.GetNomValue("ratingClasses", langCertPartVersion.Content.RatingClassId.Value) : null,
                        LicenceType = langCertPartVersion.Content.LicenceTypeId.HasValue ? this.nomRepository.GetNomValue("licenceTypes", langCertPartVersion.Content.LicenceTypeId.Value) : null,
                        LocationIndicator = langCertPartVersion.Content.LocationIndicatorId.HasValue ? this.nomRepository.GetNomValue("locationIndicators", langCertPartVersion.Content.LocationIndicatorId.Value) : null,
                        Sector = langCertPartVersion.Content.Sector,
                        DocumentPersonNumber = langCertPartVersion.Content.DocumentPersonNumber,
                        RatingTypes = langCertPartVersion.Content.RatingTypes.Count > 0 ? this.nomRepository.GetNomValues("ratingTypes", langCertPartVersion.Content.RatingTypes.ToArray()).ToList() : null,
                        LangLevel = langCertPartVersion.Content.LangLevelId.HasValue ? this.nomRepository.GetNomValue("langLevels", langCertPartVersion.Content.LangLevelId.Value) : null,
                        LangLevelEntries = langCertPartVersion.Content.LangLevelEntries.Count > 0 ?
                            langCertPartVersion.Content.LangLevelEntries
                            .Select(l =>
                                new PersonLangLevelViewDO()
                                {
                                    ChangeDate = l.ChangeDate,
                                    LangLevel = l.LangLevelId.HasValue ? this.nomRepository.GetNomValue("langLevels", l.LangLevelId.Value) : null
                                }).ToList() : null
                    });
                }
            }
            return langCertViewDOs;
        }
    }
}
