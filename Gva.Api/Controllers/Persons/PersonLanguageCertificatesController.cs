using System;
using System.Linq;
using System.Collections.Generic;
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
using Gva.Api.Repositories.PersonLangCertRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/personDocumentLangCertificates")]
    [Authorize]
    public class PersonLanguageCertificatesController : GvaCaseTypePartController<PersonLangCertDO>
    {
        private INomRepository nomRepository;
        private ICaseTypeRepository caseTypeRepository;
        private IPersonLangCertRepository personLangCertRepository;
        private ILotRepository lotRepository;
        private IFileRepository fileRepository;

        public PersonLanguageCertificatesController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            INomRepository nomRepository,
            ICaseTypeRepository caseTypeRepository,
            IPersonLangCertRepository personLangCertRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personDocumentLangCertificates", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.lotRepository = lotRepository;
            this.personLangCertRepository = personLangCertRepository;
            this.fileRepository = fileRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewLangCert(int lotId, int? caseTypeId = null)
        {
            PersonLangCertDO newLangCert = new PersonLangCertDO()
            {
                DocumentDateValidFrom = DateTime.Now,
                ValidId = this.nomRepository.GetNomValue("boolean", "yes").NomValueId,
                LangLevelEntries = new List<PersonLangLevelDO>()
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

            return Ok(new CaseTypePartDO<PersonLangCertDO>(newLangCert, caseDO));
        }

        [Route("newLangLevel")]
        public IHttpActionResult GetNewLangLevel(int lotId)
        {
            PersonLangLevelDO langLevel = new PersonLangLevelDO()
            {
                ChangeDate = DateTime.Now
            };

            return Ok(langLevel);
        }

        [Route("byValidity")]
        public IHttpActionResult GetLangCertsByValidity(int lotId, int? caseTypeId = null, bool? valid = true)
        {
            var langCertViewDOs = this.personLangCertRepository.GetLangCerts(lotId, caseTypeId, valid);
            return Ok(langCertViewDOs);
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            var langCertViewDOs = this.personLangCertRepository.GetLangCerts(lotId, caseTypeId);
            return Ok(langCertViewDOs);
        }

        [Route("{partIndex}/langLevelHistory")]
        public IHttpActionResult GetLangLevelHistory(int lotId, int partIndex)
        {
            var partVersion = this.lotRepository.GetLotIndex(lotId).Index.GetPart<PersonLangCertDO>(string.Format("personDocumentLangCertificates/{0}", partIndex));
            List<PersonLangLevelViewDO> langLevelHistoryDOs = partVersion.Content.LangLevelEntries
                .Select(l => new PersonLangLevelViewDO()
                {
                    ChangeDate = l.ChangeDate,
                    LangLevel = l.LangLevelId.HasValue ? this.nomRepository.GetNomValue("langLevels", l.LangLevelId.Value) : null
                })
                .ToList();

            return Ok(langLevelHistoryDOs);
        }

        [Route("{partIndex}/view")]
        public IHttpActionResult GetLangCert(int lotId, int partIndex)
        {
            var partVersion = this.lotRepository.GetLotIndex(lotId).Index.GetPart<PersonLangCertDO>(string.Format("personDocumentLangCertificates/{0}", partIndex));
            var lotFile = this.fileRepository.GetFileReference(partVersion.PartId, null);

            PersonLangCertViewDO cert = new PersonLangCertViewDO()
            {
                Case = lotFile != null ? new CaseDO(lotFile) : null,
                PartIndex = partVersion.Part.Index,
                PartId = partVersion.PartId,
                DocumentDateValidFrom = partVersion.Content.DocumentDateValidFrom,
                DocumentDateValidTo = partVersion.Content.DocumentDateValidTo,
                AircraftTypeGroup = partVersion.Content.AircraftTypeGroupId.HasValue ? this.nomRepository.GetNomValue("aircraftTypeGroups", partVersion.Content.AircraftTypeGroupId.Value) : null,
                DocumentNumber = partVersion.Content.DocumentNumber,
                DocumentPublisher = partVersion.Content.DocumentPublisher,
                Notes = partVersion.Content.Notes,
                Valid = partVersion.Content.ValidId.HasValue ? this.nomRepository.GetNomValue("boolean", partVersion.Content.ValidId.Value) : null,
                DocumentType = partVersion.Content.DocumentTypeId.HasValue ? this.nomRepository.GetNomValue("documentTypes", partVersion.Content.DocumentTypeId.Value) : null,
                DocumentRole = partVersion.Content.DocumentRoleId.HasValue ? this.nomRepository.GetNomValue("documentRoles", partVersion.Content.DocumentRoleId.Value) : null,
                AircraftTypeCategory = partVersion.Content.AircraftTypeCategoryId.HasValue ? this.nomRepository.GetNomValue("aircraftClases66", partVersion.Content.AircraftTypeCategoryId.Value) : null,
                Authorization = partVersion.Content.AuthorizationId.HasValue ? this.nomRepository.GetNomValue("authorizations", partVersion.Content.AuthorizationId.Value) : null,
                RatingClass = partVersion.Content.RatingClassId.HasValue ? this.nomRepository.GetNomValue("ratingClasses", partVersion.Content.RatingClassId.Value) : null,
                LicenceType = partVersion.Content.LicenceTypeId.HasValue ? this.nomRepository.GetNomValue("licenceTypes", partVersion.Content.LicenceTypeId.Value) : null,
                LocationIndicator = partVersion.Content.LocationIndicatorId.HasValue ? this.nomRepository.GetNomValue("locationIndicators", partVersion.Content.LocationIndicatorId.Value) : null,
                Sector = partVersion.Content.Sector,
                DocumentPersonNumber = partVersion.Content.DocumentPersonNumber,
                RatingTypes = partVersion.Content.RatingTypes.Count > 0 ? this.nomRepository.GetNomValues("ratingTypes", partVersion.Content.RatingTypes.ToArray()).ToList() : null,
                LangLevel = partVersion.Content.LangLevelId.HasValue ? this.nomRepository.GetNomValue("langLevels", partVersion.Content.LangLevelId.Value) : null,
                LangLevelEntries = partVersion.Content.LangLevelEntries.Count > 0 ?
                    partVersion.Content.LangLevelEntries
                    .Select(l =>
                        new PersonLangLevelViewDO()
                        {
                            ChangeDate = l.ChangeDate,
                            LangLevel = l.LangLevelId.HasValue ? this.nomRepository.GetNomValue("langLevels", l.LangLevelId.Value) : null
                        }).ToList() : null
            };

            return Ok(cert);
        }
    }
}