using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.Models.Views.Person;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/personDocumentChecks")]
    [Authorize]
    public class PersonChecksController : GvaCaseTypePartController<PersonCheckDO>
    {
        private INomRepository nomRepository;
        private IUnitOfWork unitOfWork;
        private ICaseTypeRepository caseTypeRepository;
        private IFileRepository fileRepository;
        private ILotRepository lotRepository;
        private string path;

        public PersonChecksController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            INomRepository nomRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personDocumentChecks", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
            this.unitOfWork = unitOfWork;
            this.caseTypeRepository = caseTypeRepository;
            this.fileRepository = fileRepository;
            this.lotRepository = lotRepository;
            this.path = "personDocumentChecks";
        }

        [Route("new")]
        public IHttpActionResult GetNewCheck(int lotId, int? caseTypeId = null)
        {
            PersonCheckDO newCheck = new PersonCheckDO()
            {
                DocumentDateValidFrom = DateTime.Now,
                ValidId = this.nomRepository.GetNomValue("boolean", "yes").NomValueId,
                Reports = new List<RelatedReportDO>()
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

            return Ok(new CaseTypePartDO<PersonCheckDO>(newCheck, caseDO));
        }

        [Route("{partIndex}/report")]
        public IHttpActionResult GetReport(int lotId, int partIndex)
        {
            var report = (from r in this.unitOfWork.DbContext.Set<GvaViewPersonReport>().Include(r => r.Person)
                      join rc in this.unitOfWork.DbContext.Set<GvaViewPersonReportCheck>() on
                        new { LotId = r.LotId, PartIndex = r.PartIndex }
                        equals
                        new { LotId = rc.LotId, PartIndex = rc.PartIndex }
                          where rc.CheckPartIndex == partIndex && rc.CheckLotId == lotId
                             select new {
                                PartIndex = r.PartIndex,
                                LotId = r.LotId,
                                Date = r.Date,
                                DocumentNumber = r.DocumentNumber,
                                Person = r.Person
                              })
                              .SingleOrDefault();

            RelatedReportDO result = null;
            if(report != null)
            {
                result = new RelatedReportDO()
                {
                    PartIndex = report.PartIndex,
                    LotId = report.LotId,
                    Date = report.Date,
                    DocumentNumber = report.DocumentNumber,
                    Publisher = string.Format("{0} {1}", report.Person.Lin, report.Person.Names)
                };
            }

            return Ok(new
            {
                result = result
            });
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            var checks = this.lotRepository.GetLotIndex(lotId).Index.GetParts<PersonCheckDO>(this.path);

            List<PersonCheckViewDO> checkViewDOs = new List<PersonCheckViewDO>();
            foreach (var checkPartVersion in checks)
            {
                var lotFile = this.fileRepository.GetFileReference(checkPartVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFile != null)
                {
                    checkViewDOs.Add(new PersonCheckViewDO()
                    {
                        Case = lotFile != null ? new CaseDO(lotFile) : null,
                        PartIndex = checkPartVersion.Part.Index,
                        PartId = checkPartVersion.PartId,
                        DocumentDateValidFrom = checkPartVersion.Content.DocumentDateValidFrom,
                        DocumentDateValidTo = checkPartVersion.Content.DocumentDateValidTo,
                        AircraftTypeGroup = checkPartVersion.Content.AircraftTypeGroupId.HasValue ? this.nomRepository.GetNomValue("aircraftTypeGroups", checkPartVersion.Content.AircraftTypeGroupId.Value) : null,
                        DocumentNumber = checkPartVersion.Content.DocumentNumber,
                        DocumentPublisher = checkPartVersion.Content.DocumentPublisher,
                        Notes = checkPartVersion.Content.Notes,
                        Valid = checkPartVersion.Content.ValidId.HasValue ? this.nomRepository.GetNomValue("boolean", checkPartVersion.Content.ValidId.Value) : null,
                        DocumentType = checkPartVersion.Content.DocumentTypeId.HasValue ? this.nomRepository.GetNomValue("documentTypes", checkPartVersion.Content.DocumentTypeId.Value) : null,
                        DocumentRole = checkPartVersion.Content.DocumentRoleId.HasValue ? this.nomRepository.GetNomValue("documentRoles", checkPartVersion.Content.DocumentRoleId.Value) : null,
                        AircraftTypeCategory = checkPartVersion.Content.AircraftTypeCategoryId.HasValue ? this.nomRepository.GetNomValue("aircraftClases66", checkPartVersion.Content.AircraftTypeCategoryId.Value) : null,
                        Authorization = checkPartVersion.Content.AuthorizationId.HasValue ? this.nomRepository.GetNomValue("authorizations", checkPartVersion.Content.AuthorizationId.Value) : null,
                        RatingClass = checkPartVersion.Content.RatingClassId.HasValue ? this.nomRepository.GetNomValue("ratingClasses", checkPartVersion.Content.RatingClassId.Value) : null,
                        LicenceType = checkPartVersion.Content.LicenceTypeId.HasValue ? this.nomRepository.GetNomValue("licenceTypes", checkPartVersion.Content.LicenceTypeId.Value) : null,
                        LocationIndicator = checkPartVersion.Content.LocationIndicatorId.HasValue ? this.nomRepository.GetNomValue("locationIndicators", checkPartVersion.Content.LocationIndicatorId.Value) : null,
                        Sector = checkPartVersion.Content.Sector,
                        DocumentPersonNumber = checkPartVersion.Content.DocumentPersonNumber,
                        RatingTypes = checkPartVersion.Content.RatingTypes.Count > 0 ? this.nomRepository.GetNomValues("ratingTypes", checkPartVersion.Content.RatingTypes.ToArray()).ToList() : null,
                        Reports = checkPartVersion.Content.Reports,
                        PersonCheckRatingValue = checkPartVersion.Content.PersonCheckRatingValueId.HasValue ? this.nomRepository.GetNomValue("personCheckRatingValues", checkPartVersion.Content.PersonCheckRatingValueId.Value) : null
                    });
                }
            }
            return Ok(checkViewDOs);
        }
    }
}