using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.AircraftRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/personFlyingExperiences")]
    [Authorize]
    public class PersonFlyingExperiencesController : GvaCaseTypePartController<PersonFlyingExperienceDO>
    {
        private ILotRepository lotRepository;
        private INomRepository nomRepository;
        private IFileRepository fileRepository;
        private IOrganizationRepository organizationRepository;
        private IAircraftRepository aircraftRepository;
        private string path;

        public PersonFlyingExperiencesController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            INomRepository nomRepository,
            IOrganizationRepository organizationRepository,
            IAircraftRepository aircraftRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personFlyingExperiences", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.path = "personFlyingExperiences";
            this.lotRepository = lotRepository;
            this.nomRepository = nomRepository;
            this.fileRepository = fileRepository;
            this.organizationRepository = organizationRepository;
            this.aircraftRepository = aircraftRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewFlyingExperience(int lotId)
        {
            PersonFlyingExperienceDO newFlyingExperience = new PersonFlyingExperienceDO()
            {
                DocumentDate = DateTime.Now
            };

            return Ok(new CaseTypePartDO<PersonFlyingExperienceDO>(newFlyingExperience));
        }

        [Route("sumAllFlightHours")]
        public IHttpActionResult GetSumOFAllFlightHours(int lotId, int locationIndicatorId, string sector, int? partIndex = null)
        {
            var allFlyingExperiences = this.lotRepository.GetLotIndex(lotId).Index.GetParts<PersonFlyingExperienceDO>(this.path).ToList();
            if (partIndex.HasValue)
            {
                allFlyingExperiences = allFlyingExperiences.Where(e => e.Part.Index != partIndex).ToList();
            }

            var total = allFlyingExperiences
                .Where(f => f.Content.TotalDoc.HasValue && 
                    DateTime.Compare(f.CreateDate, DateTime.Now) < 0 &&
                    f.Content.LocationIndicatorId == locationIndicatorId &&
                    f.Content.Sector.ToLower().Contains(sector.ToLower()))
                    .Select(s => s.Content.TotalDoc).Sum() ?? 0;

            return Ok(new
            {
                total = total
            });
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            var flyingExperiences = this.lotRepository.GetLotIndex(lotId).Index.GetParts<PersonFlyingExperienceDO>(this.path);

            List<PersonFlyingExperienceViewDO> flyingExpViewDOs = new List<PersonFlyingExperienceViewDO>();
            foreach (var flyingExperienceVersion in flyingExperiences)
            {
                var lotFile = this.fileRepository.GetFileReference(flyingExperienceVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFile != null)
                {
                    NomValue aircraft = null;
                    NomValue organization = null;
                    if (flyingExperienceVersion.Content.AircraftId.HasValue)
                    {
                        var ac = this.aircraftRepository.GetAircraft(flyingExperienceVersion.Content.AircraftId.Value);
                        aircraft = new NomValue()
                        {
                            NomValueId = ac.Item1.LotId,
                            Name = ac.Item1.Model,
                            NameAlt = ac.Item1.ModelAlt
                        };
                    }

                    if (flyingExperienceVersion.Content.OrganizationId.HasValue)
                    {
                        var org = this.organizationRepository.GetOrganization(flyingExperienceVersion.Content.OrganizationId.Value);
                        organization = new NomValue()
                        {
                            NomValueId = org.LotId,
                            Name = org.Name,
                            NameAlt = org.NameAlt
                        };
                    }

                    flyingExpViewDOs.Add(new PersonFlyingExperienceViewDO()
                    {
                        Case = lotFile != null ? new CaseDO(lotFile) : null,
                        PartIndex = flyingExperienceVersion.Part.Index,
                        LocationIndicator = flyingExperienceVersion.Content.LocationIndicatorId.HasValue ? this.nomRepository.GetNomValue("locationIndicators", flyingExperienceVersion.Content.LocationIndicatorId.Value) : null,
                        NightLandings = flyingExperienceVersion.Content.NightLandings,
                        Notes = flyingExperienceVersion.Content.Notes,
                        TotalDoc = flyingExperienceVersion.Content.TotalDoc,
                        Total = flyingExperienceVersion.Content.Total,
                        TotalLastMonths = flyingExperienceVersion.Content.TotalLastMonths,
                        DayLandings = flyingExperienceVersion.Content.DayLandings,
                        DayVFR = flyingExperienceVersion.Content.DayVFR,
                        DayIFR = flyingExperienceVersion.Content.DayIFR,
                        NightIFR = flyingExperienceVersion.Content.NightIFR,
                        NightVFR = flyingExperienceVersion.Content.NightIFR,
                        DocumentDate = flyingExperienceVersion.Content.DocumentDate,
                        LicenceType = flyingExperienceVersion.Content.LicenceTypeId.HasValue ? this.nomRepository.GetNomValue("licenceTypes", flyingExperienceVersion.Content.LicenceTypeId.Value) : null,
                        RatingClass = flyingExperienceVersion.Content.RatingClassId.HasValue ? this.nomRepository.GetNomValue("ratingClasses", flyingExperienceVersion.Content.RatingClassId.Value) : null,
                        RatingTypes = flyingExperienceVersion.Content.RatingTypes.Count > 0 ? this.nomRepository.GetNomValues("ratingTypes", flyingExperienceVersion.Content.RatingTypes.ToArray()).ToList() : null,
                        ExperienceMeasure = flyingExperienceVersion.Content.ExperienceMeasureId.HasValue ? this.nomRepository.GetNomValue("experienceMeasures", flyingExperienceVersion.Content.ExperienceMeasureId.Value) : null,
                        ExperienceRole = flyingExperienceVersion.Content.ExperienceRoleId.HasValue ? this.nomRepository.GetNomValue("experienceRoles", flyingExperienceVersion.Content.ExperienceRoleId.Value) : null,
                        Sector = flyingExperienceVersion.Content.Sector,
                        Authorization = flyingExperienceVersion.Content.AuthorizationId.HasValue ? this.nomRepository.GetNomValue("authorizations", flyingExperienceVersion.Content.AuthorizationId.Value) : null,
                        Aircraft = aircraft,
                        Organization = organization
                    });
                }
            }
            return Ok(flyingExpViewDOs);
        }
    }
}