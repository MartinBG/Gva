using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.NomRepository;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.Models.Views.Person;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.CaseTypeRepository;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Person
{
    public class PersonProjection : Projection<GvaViewPerson>
    {
        private INomRepository nomRepository;
        private ICaseTypeRepository caseTypeRepository;
        private string setAlias;

        public PersonProjection(
            IUnitOfWork unitOfWork,
            INomRepository nomRepository,
            ICaseTypeRepository caseTypeRepository)
            : base(unitOfWork, "Person")
        {
            this.nomRepository = nomRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.setAlias = "Person";
        }

        public override IEnumerable<GvaViewPerson> Execute(PartCollection parts)
        {
            var personData = parts.Get<PersonDataDO>("personData");
            IEnumerable<GvaCaseType> caseTypesForSet = this.caseTypeRepository.GetCaseTypesForSet(this.setAlias);
            if (personData == null)
            {
                return new GvaViewPerson[] { };
            }

            int validTrueId = this.nomRepository.GetNomValue("boolean", "yes").NomValueId;
            var personEmployment = parts.GetAll<PersonEmploymentDO>("personDocumentEmployments")
                .Where(pv => pv.Content.ValidId == validTrueId)
                .FirstOrDefault();

            var personLicences = parts.GetAll<PersonLicenceDO>("licences")
                .Where(pv => pv.Content.ValidId.HasValue && this.nomRepository.GetNomValue(pv.Content.ValidId.Value).Code == "Y");

            var personRatings = parts.GetAll<PersonRatingDO>("ratings");

            return new[] { this.Create(personData, personEmployment, personLicences, personRatings, caseTypesForSet) };
        }

        private GvaViewPerson Create(
            PartVersion<PersonDataDO> personData,
            PartVersion<PersonEmploymentDO> personEmployment,
            IEnumerable<PartVersion<PersonLicenceDO>> personLicences,
            IEnumerable<PartVersion<PersonRatingDO>> personRatings,
            IEnumerable<GvaCaseType> caseTypesForSet)
        {
            GvaViewPerson person = new GvaViewPerson();

            person.CaseTypes = string.Join(", ",
                caseTypesForSet
                .Where(c => personData.Content.CaseTypes.Contains(c.GvaCaseTypeId))
                .Select(c => c.Alias));

            person.LotId = personData.Part.Lot.LotId;
            person.Lin = personData.Content.Lin;
            person.LinTypeId = personData.Content.LinTypeId.Value;
            person.Uin = personData.Content.Uin;
            person.Names = string.Format(
                "{0} {1} {2}",
                personData.Content.FirstName,
                personData.Content.MiddleName,
                personData.Content.LastName);
            person.NamesAlt = string.Format(
               "{0} {1} {2}",
               personData.Content.FirstNameAlt,
               personData.Content.MiddleNameAlt,
               personData.Content.LastNameAlt);
            person.BirtDate = personData.Content.DateOfBirth.Value;

            if (personEmployment != null)
            {
                person.EmploymentId = personEmployment.Content.EmploymentCategoryId;
                person.OrganizationId = personEmployment.Content.OrganizationId;
            }

            if (personLicences.Count() > 0)
            {
                person.Licences = string.Join(", ",
                (personLicences
                .Where(l => l.Content.LicenceTypeId.HasValue)
                .Select(l => this.nomRepository.GetNomValue(l.Content.LicenceTypeId.Value).Code)
                .ToArray()));
            }

            List<string> ratings = new List<string>();
            foreach (var rating in personRatings)
            {
                if (rating.Content.AircraftTypeCategoryId.HasValue)
                {
                    ratings.Add(this.nomRepository.GetNomValue("aircraftClases66", rating.Content.AircraftTypeCategoryId.Value).Code);
                }
                else {
                    var ratingTypes = rating.Content.RatingTypes.Count() > 0 ? string.Join(", ", this.nomRepository.GetNomValues("ratingTypes", rating.Content.RatingTypes.ToArray()).Select(s => s.Code)) : null;
                    var ratingClass = rating.Content.RatingClassId.HasValue ? this.nomRepository.GetNomValue("ratingClasses", rating.Content.RatingClassId.Value).Code : string.Empty;
                    var authorization = rating.Content.AuthorizationId.HasValue ? this.nomRepository.GetNomValue("authorizations", rating.Content.AuthorizationId.Value).Code : null;

                    var ratingData = (ratingTypes ?? ratingClass) + (authorization != null ? "/" + authorization : string.Empty);
                    if (!string.IsNullOrEmpty(ratingData))
                    {
                        ratings.Add(ratingData);
                    }
                }
            }
            if (ratings.Count() > 0)
            {
                person.Ratings = string.Join(", ", ratings);
            }

            return person;
        }
    }
}
