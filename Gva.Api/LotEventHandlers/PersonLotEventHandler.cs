using System;
using System.Linq;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.Repositories;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers
{
    public class PersonLotEventHandler : ILotEventHandler
    {
        private static string[] parts = { "data", "employment", "licence", "rating" };

        private IPersonRepository personRepository;

        public PersonLotEventHandler(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        public void Handle(IEvent e)
        {
            CommitEvent commitEvent = e as CommitEvent;
            if (commitEvent == null)
            {
                return;
            }

            var commit = commitEvent.Commit;
            var lot = commitEvent.Lot;

            if (lot.Set.Alias != "Person" ||
                !commit.PartVersions.Any(pv => parts.Contains(pv.Part.SetPart.Alias)))
            {
                return;
            }

            var person = this.personRepository.GetPerson(lot.LotId);

            if (person == null)
            {
                person = new GvaPerson()
                {
                    Lot = lot
                };
                this.UpdatePersonData(person, commit);
                this.personRepository.AddPerson(person);
            }
            else
            {
                this.UpdatePersonData(person, commit);
                this.UpdatePersonLicences(person, commit);
                this.UpdatePersonEmployment(person, commit);
            }
        }

        private void UpdatePersonData(GvaPerson person, Commit commit)
        {
            var personDataPart = commit.ChangedPartVersions
                .SingleOrDefault(pv => pv.Part.SetPart.Alias == "data");

            if (personDataPart != null)
            {
                var personDataContent = personDataPart.Content.Value<JObject>("part");

                var personBirthDate = personDataContent.Value<DateTime>("dateOfBirth");
                DateTime now = DateTime.Today;
                int age = now.Year - personBirthDate.Year;
                if (now < personBirthDate.AddYears(age))
                {
                    age--;
                }

                person.Lin = personDataContent.Value<string>("lin");
                person.Uin = personDataContent.Value<string>("uin");
                person.Names = string.Format(
                    "{0} {1} {2}",
                    personDataContent.Value<string>("firstName"),
                    personDataContent.Value<string>("middleName"),
                    personDataContent.Value<string>("lastName"));
                person.Age = age;
            }
        }

        private void UpdatePersonLicences(GvaPerson person, Commit commit)
        {
            var personLicenceParts = commit.PartVersions
                .Where(pv => pv.Part.SetPart.Alias == "licence" && pv.PartOperation != PartOperation.Delete);

            person.Licences = string.Empty;
            foreach (var personLicencePart in personLicenceParts)
            {
                var personLicenceContent = personLicencePart.Content.Value<JObject>("part");
                person.Licences += string.Format(", {0}", personLicenceContent.Value<JObject>("licenceType").Value<string>("name"));
            }
        }

        private void UpdatePersonRatings(GvaPerson person, Commit commit)
        {
            var personRatingParts = commit.PartVersions
                .Where(pv => pv.Part.SetPart.Alias == "rating" && pv.PartOperation != PartOperation.Delete);

            person.Ratings = string.Empty;
            foreach (var personRatingPart in personRatingParts)
            {
                var personRatingContent = personRatingPart.Content.Value<JObject>("part");

                var retingType = personRatingContent.Value<JObject>("ratingType");
                if (retingType != null)
                {
                    person.Ratings += string.Format(", {0}", retingType.Value<string>("name"));
                }
            }
        }

        private void UpdatePersonEmployment(GvaPerson person, Commit commit)
        {
            var personEmploymentPart = commit.PartVersions
                .Where(pv => pv.Part.SetPart.Alias == "employment" && pv.PartOperation != PartOperation.Delete)
                .OrderByDescending(pv => pv.CreateDate)
                .FirstOrDefault();

            if (personEmploymentPart != null)
            {
                var personEmploymentContent = personEmploymentPart.Content.Value<JObject>("part");

                var organization = personEmploymentContent.Value<JObject>("organization");
                person.Organization = organization == null ? null : organization.Value<string>("name");
                person.Employment = personEmploymentContent.Value<JObject>("employmentCategory").Value<string>("name");
            }
        }
    }
}
