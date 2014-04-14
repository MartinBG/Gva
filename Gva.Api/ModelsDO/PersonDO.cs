using System;
using System.Linq;
using System.Collections.Generic;
using Gva.Api.Models;
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
            this.LinType = personData.LinType;
            this.Uin = personData.Uin;
            this.Names = personData.Names;
            this.BirtDate = personData.BirtDate;
            this.Organization = personData.Organization;
            this.Employment = personData.Employment;
            this.Licences = personLicences == null ?
                new List<GvaViewPersonLicence>() :
                personLicences;
            this.Ratings = personRatings == null ?
                new List<GvaViewPersonRating>() :
                personRatings;
        }

        public int Id { get; set; }

        public string Lin { get; set; }

        public string LinType { get; set; }

        public string Uin { get; set; }

        public string Names { get; set; }

        public DateTime BirtDate { get; set; }

        public IEnumerable<GvaViewPersonLicence> Licences { get; set; }

        public IEnumerable<GvaViewPersonRating> Ratings { get; set; }

        public string Organization { get; set; }

        public string Employment { get; set; }
    }
}