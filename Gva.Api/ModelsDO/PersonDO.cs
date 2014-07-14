using System;
using System.Collections.Generic;
using System.Linq;
using Gva.Api.Models;
using Gva.Api.Models.Views.Person;

namespace Gva.Api.ModelsDO
{
    public class PersonDO
    {
        public PersonDO(GvaViewPerson person)
            : this(person, person.Licences, person.Ratings)
        {
        }

        public PersonDO(
            GvaViewPerson personData,
            IEnumerable<GvaViewPersonLicence> personLicences = null,
            IEnumerable<GvaViewPersonRating> personRatings = null)
        {
            this.Id = personData.LotId;
            this.Lin = personData.Lin;
            this.LinType = personData.LinType.Code;
            this.Uin = personData.Uin;
            this.Names = personData.Names;
            this.Age = this.GetAge(personData.BirtDate.Date);
            this.Organization = personData.Organization == null ? null : personData.Organization.Name;
            this.Employment = personData.Employment == null ? null : personData.Employment.Name;
            this.Licences = string.Join(", ",
                (personLicences ?? new List<GvaViewPersonLicence>()).Select(l => l.LicenceType.Code).ToArray());
            this.Ratings = string.Join(", ",
                (personRatings ?? new List<GvaViewPersonRating>()).Select(l => l.RatingType.Name).ToArray());
        }

        public int Id { get; set; }

        public string Lin { get; set; }

        public string LinType { get; set; }

        public string Uin { get; set; }

        public string Names { get; set; }

        public int Age { get; set; }

        public string Licences { get; set; }

        public string Ratings { get; set; }

        public string Organization { get; set; }

        public string Employment { get; set; }

        private int GetAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}