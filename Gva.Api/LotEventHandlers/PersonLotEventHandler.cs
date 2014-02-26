using Common.Data;
using Gva.Api.Models;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using System;
using System.Linq;

namespace Gva.Api.LotEventHandlers
{
    public class PersonLotEventHandler : ILotEventHandler
    {
        private static string[] parts = { "data", "employment", "licence", "rating" };

        private IUnitOfWork unitOfWork;

        public PersonLotEventHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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

            var person = this.unitOfWork.DbContext.Set<GvaPerson>()
                .SingleOrDefault(p => p.GvaPersonLotId == lot.LotId);

            if (person == null)
            {
                person = new GvaPerson();
                this.UpdatePersonData(person, commit);
                this.unitOfWork.DbContext.Set<GvaPerson>().Add(person);
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
            var personDataPart = commit.PartVersions
                .SingleOrDefault(pv => pv.Part.SetPart.Alias == "data" && pv.OriginalCommit == commit);

            if (personDataPart != null)
            {
                var personDataContent = JObject.Parse(personDataPart.TextContent);

                var personBirthDate = Convert.ToDateTime(personDataContent.Value<string>("dateOfBirth"));
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
            var personLicencePart = commit.PartVersions
                .SingleOrDefault(pv => pv.Part.SetPart.Alias == "licence" && pv.OriginalCommit == commit);

            if (personLicencePart != null)
            {
                var personLicenceContent = JObject.Parse(personLicencePart.TextContent);
                person.Licences = person.Licences ?? string.Empty;
                person.Licences += string.Format(", {0}", personLicenceContent.Value<JObject>("licenceType").Value<string>("name"));
            }
        }

        private void UpdatePersonRatings(GvaPerson person, Commit commit)
        {
            var personRatingPart = commit.PartVersions
                .SingleOrDefault(pv => pv.Part.SetPart.Alias == "rating" && pv.OriginalCommit == commit);

            if (personRatingPart != null)
            {
                var personRatingContent = JObject.Parse(personRatingPart.TextContent);
                var retingType = personRatingContent.Value<JObject>("ratingType");
                if (retingType != null)
                {
                    person.Ratings = person.Ratings ?? string.Empty;
                    person.Ratings += string.Format(", {0}", retingType.Value<string>("name"));
                }
            }
        }

        private void UpdatePersonEmployment(GvaPerson person, Commit commit)
        {
            var personEmploymentPart = commit.PartVersions
                .SingleOrDefault(pv => pv.Part.SetPart.Alias == "employment" && pv.OriginalCommit == commit);

            if (personEmploymentPart != null)
            {
                var personEmploymentContent = JObject.Parse(personEmploymentPart.TextContent);
                var organization = personEmploymentContent.Value<JObject>("organization");
                person.Organization = organization == null ? null : organization.Value<string>("name");
                person.Employment = personEmploymentContent.Value<JObject>("employmentCategory").Value<string>("name");
            }
        }
    }
}
