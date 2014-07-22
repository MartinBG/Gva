using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views.Person;
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
            var personData = parts.Get("personData");

            if (personData == null)
            {
                return new GvaViewPerson[] { };
            }

            var personEmployment = parts.GetAll("personDocumentEmployments")
                .Where(pv => pv.Content.Get<string>("valid.code") == "Y")
                .FirstOrDefault();

            var personLicences = parts.GetAll("licences")
                .Where(pv => pv.Content.Get<string>("valid.code") == "Y");

            var personRatings = parts.GetAll("ratings")
                .Where(pv => pv.Content.Get("ratingType") != null);

            return new[] { this.Create(personData, personEmployment, personLicences, personRatings) };
        }

        private GvaViewPerson Create(PartVersion personData, PartVersion personEmployment, IEnumerable<PartVersion> personLicences, IEnumerable<PartVersion> personRatings)
        {
            GvaViewPerson person = new GvaViewPerson();

            person.LotId = personData.Part.Lot.LotId;
            person.Lin = personData.Content.Get<string>("lin");
            person.LinTypeId = personData.Content.Get<int>("linType.nomValueId");
            person.Uin = personData.Content.Get<string>("uin");
            person.Names = string.Format(
                "{0} {1} {2}",
                personData.Content.Get<string>("firstName"),
                personData.Content.Get<string>("middleName"),
                personData.Content.Get<string>("lastName"));
            person.BirtDate = personData.Content.Get<DateTime>("dateOfBirth");

            if (personEmployment != null)
            {
                person.EmploymentId = personEmployment.Content.Get<int>("employmentCategory.nomValueId");
                person.OrganizationId = personEmployment.Content.Get<int?>("organization.nomValueId");
            }

            if (personLicences.Count() > 0)
            {
                person.Licences = string.Join(", ",
                (personLicences
                .Select(l => l.Content.Get<string>("licenceType.code"))
                .ToArray()));
            }

            if (personRatings.Count() > 0)
            {
                person.Ratings = string.Join(", ",
                (personRatings
                .Select(l => l.Content.Get<string>("ratingType.name"))
                .ToArray()));
            }

            return person;
        }
    }
}
