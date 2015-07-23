using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.ManageLotsTool.OldDOs
{
    public class PersonAddressDO_Old
    {
        [Required(ErrorMessage = "AddressType is required.")]
        public NomValue AddressType { get; set; }

        [Required(ErrorMessage = "Valid is required.")]
        public NomValue Valid { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "AddressAlt is required.")]
        public string AddressAlt { get; set; }

        [Required(ErrorMessage = "Settlement is required.")]
        public NomValue Settlement { get; set; }

        public string PostalCode { get; set; }

        public string Phone { get; set; }
    }
}
