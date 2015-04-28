using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Gva.Api.Models.Views.Person;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Person
{
    public class PersonProjection : Projection<GvaViewPerson>
    {
        public PersonProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Person")
        {
        }

        public override IEnumerable<GvaViewPerson> Execute(PartCollection parts)
        {
            var personData = parts.Get<PersonDataDO>("personData");

            if (personData == null)
            {
                return new GvaViewPerson[] { };
            }

            var personEmployment = parts.GetAll<PersonEmploymentDO>("personDocumentEmployments")
                .Where(pv => pv.Content.Valid.Code == "Y")
                .FirstOrDefault();

            var personLicences = parts.GetAll<PersonLicenceDO>("licences")
                .Where(pv => pv.Content.Valid != null && pv.Content.Valid.Code == "Y");

            var personRatings = parts.GetAll<PersonRatingDO>("ratings");

            return new[] { this.Create(personData, personEmployment, personLicences, personRatings) };
        }

        private GvaViewPerson Create(
            PartVersion<PersonDataDO> personData,
            PartVersion<PersonEmploymentDO> personEmployment,
            IEnumerable<PartVersion<PersonLicenceDO>> personLicences,
            IEnumerable<PartVersion<PersonRatingDO>> personRatings)
        {
            GvaViewPerson person = new GvaViewPerson();

            person.CaseTypes = string.Join(", ", personData.Content.CaseTypes.Select(c => c.Alias));
            person.LotId = personData.Part.Lot.LotId;
            person.Lin = personData.Content.Lin;
            person.LinTypeId = personData.Content.LinType.NomValueId;
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
                person.EmploymentId = personEmployment.Content.EmploymentCategory != null ? personEmployment.Content.EmploymentCategory.NomValueId : (int?)null;
                person.OrganizationId = personEmployment.Content.Organization != null ? personEmployment.Content.Organization.NomValueId : (int?)null;
            }

            if (personLicences.Count() > 0)
            {
                person.Licences = string.Join(", ",
                (personLicences
                .Where(l => l.Content.LicenceType != null)
                .Select(l => l.Content.LicenceType.Code)
                .ToArray()));
            }

            List<string> ratings = new List<string>();
            foreach (var rating in personRatings)
            {
                if (rating.Content.AircraftTypeCategory != null)
                {
                    ratings.Add(rating.Content.AircraftTypeCategory.Code);
                }
                else { 
                    var ratingTypes = rating.Content.RatingTypes.Count() > 0 ? string.Join(", ", rating.Content.RatingTypes.Select(s => s.Code)) : null;
                    var ratingClass = rating.Content.RatingClass != null ? rating.Content.RatingClass.Code : null;
                    var authorization = rating.Content.Authorization != null ? rating.Content.Authorization.Code : null;

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
