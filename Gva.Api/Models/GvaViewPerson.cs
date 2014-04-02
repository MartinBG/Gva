using System.Collections.Generic;

namespace Gva.Api.Models
{
    public class GvaViewPerson
    {
        public GvaViewPersonData Data { get; set; }

        public IEnumerable<GvaViewPersonRating> Ratings { get; set; }

        public IEnumerable<GvaViewPersonLicence> Licences { get; set; }
    }
}
