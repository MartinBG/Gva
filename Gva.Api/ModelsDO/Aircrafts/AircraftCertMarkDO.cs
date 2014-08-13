using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftCertMarkDO
    {
        [Required(ErrorMessage = "LtrInNumber is required.")]
        public string LtrInNumber { get; set; }

        [Required(ErrorMessage = "LtrInDate is required.")]
        public DateTime? LtrInDate { get; set; }

        [Required(ErrorMessage = "LtrCaaNumber is required.")]
        public string LtrCaaNumber { get; set; }

        [Required(ErrorMessage = "LtrCaaDate is required.")]
        public DateTime? LtrCaaDate { get; set; }

        [Required(ErrorMessage = "Mark is required.")]
        public string Mark { get; set; }

        [Required(ErrorMessage = "Valid is required.")]
        public NomValue Valid { get; set; }
    }
}
