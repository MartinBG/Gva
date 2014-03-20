using System;
using System.Linq;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.Repositories.PersonRepository;
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
                dynamic personDataContent = personDataPart.Content;

                var personBirthDate = Convert.ToDateTime(personDataContent.dateOfBirth);
                DateTime now = DateTime.Today;
                int age = now.Year - personBirthDate.Year;
                if (now < personBirthDate.AddYears(age))
                {
                    age--;
                }

                person.Lin = personDataContent.lin;
                person.Uin = personDataContent.uin;
                person.Names = string.Format(
                    "{0} {1} {2}",
                    personDataContent.firstName,
                    personDataContent.middleName,
                    personDataContent.lastName);
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
                dynamic personLicenceContent = personLicencePart.Content;
                person.Licences += string.Format(", {0}", personLicenceContent.licenceType.name);
            }
        }

        private void UpdatePersonRatings(GvaPerson person, Commit commit)
        {
            var personRatingParts = commit.PartVersions
                .Where(pv => pv.Part.SetPart.Alias == "rating" && pv.PartOperation != PartOperation.Delete);

            person.Ratings = string.Empty;
            foreach (var personRatingPart in personRatingParts)
            {
                dynamic personRatingContent = personRatingPart.Content;

                var retingType = personRatingContent.ratingType;
                if (retingType != null)
                {
                    person.Ratings += string.Format(", {0}", retingType.name);
                }
            }
        }

        private void UpdatePersonEmployment(GvaPerson person, Commit commit)
        {
            var personEmploymentPart = commit.PartVersions
                .Where(pv => pv.Part.SetPart.Alias == "employment" && pv.PartOperation != PartOperation.Delete)
                .OrderByDescending(pv => pv.CreateDate)
                .FirstOrDefault(pv => (pv.Content as dynamic).valid.code == "Y");

            if (personEmploymentPart != null)
            {
                dynamic personEmploymentContent = personEmploymentPart.Content;

                var organization = personEmploymentContent.organization;
                person.Organization = organization == null ? null : organization.name;
                person.Employment = personEmploymentContent.employmentCategory.name;
            }
        }
    }
}
