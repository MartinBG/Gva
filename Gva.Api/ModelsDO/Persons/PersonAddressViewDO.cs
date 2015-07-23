using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonAddressViewDO
    {
        public int PartIndex { get; set; }

        public NomValue AddressType { get; set; }

        public NomValue Valid { get; set; }

        public string Address { get; set; }

        public string AddressAlt { get; set; }

        public NomValue Settlement { get; set; }

        public string PostalCode { get; set; }

        public string Phone { get; set; }
    }
}
