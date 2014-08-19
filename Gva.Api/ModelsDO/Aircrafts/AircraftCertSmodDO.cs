using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftCertSmodDO
    {
        [Required(ErrorMessage = "LtrInNumber is required.")]
        public string LtrInNumber { get; set; }

        [Required(ErrorMessage = "LtrInDate is required.")]
        public DateTime? LtrInDate { get; set; }

        [Required(ErrorMessage = "LtrCaaNumber is required.")]
        public string LtrCaaNumber { get; set; }

        [Required(ErrorMessage = "ErrorMessage is required.")]
        public DateTime? LtrCaaDate { get; set; }

        public string CaaTo { get; set; }

        public string CaaJob { get; set; }

        public string CaaToAddress { get; set; }

        [Required(ErrorMessage = "Scode is required.")]
        public string Scode { get; set; }

        [Required(ErrorMessage = "Valid is required.")]
        public NomValue Valid { get; set; }
    }
}
