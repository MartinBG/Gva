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

            var personRatings = parts.GetAll<PersonRatingDO>("ratings")
                .Where(pv => pv.Content.RatingType != null);

            return new[] { this.Create(personData, personEmployment, personLicences, personRatings) };
        }

        private GvaViewPerson Create(
            PartVersion<PersonDataDO> personData,
            PartVersion<PersonEmploymentDO> personEmployment,
            IEnumerable<PartVersion<PersonLicenceDO>> personLicences,
            IEnumerable<PartVersion<PersonRatingDO>> personRatings)
        {
            GvaViewPerson person = new GvaViewPerson();

            person.LotId = personData.Part.Lot.LotId;
            person.Lin = personData.Content.Lin;
            person.LinTypeId = personData.Content.LinType.NomValueId;
            person.Uin = personData.Content.Uin;
            person.Names = string.Format(
                "{0} {1} {2}",
                personData.Content.FirstName,
                personData.Content.MiddleName,
                personData.Content.LastName);
            person.BirtDate = personData.Content.DateOfBirth.Value;

            if (personEmployment != null)
            {
                person.EmploymentId = personEmployment.Content.EmploymentCategory.NomValueId;
                person.OrganizationId = personEmployment.Content.Organization.NomValueId;
            }

            if (personLicences.Count() > 0)
            {
                person.Licences = string.Join(", ",
                (personLicences
                .Select(l => l.Content.LicenceType.Code)
                .ToArray()));
            }

            if (personRatings.Count() > 0)
            {
                person.Ratings = string.Join(", ",
                (personRatings
                .Select(l => l.Content.RatingType.Name)
                .ToArray()));
            }

            return person;
        }
    }
}
