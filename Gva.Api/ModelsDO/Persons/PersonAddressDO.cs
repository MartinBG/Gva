using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonAddressDO
    {
        public int? AddressTypeId { get; set; }

        public int? ValidId { get; set; }

        public string Address { get; set; }

        public string AddressAlt { get; set; }

        public int? SettlementId { get; set; }

        public string PostalCode { get; set; }

        public string Phone { get; set; }
    }
}
