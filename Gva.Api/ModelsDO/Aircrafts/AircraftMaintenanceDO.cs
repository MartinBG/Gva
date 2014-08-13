using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftMaintenanceDO
    {
        [Required(ErrorMessage = "Lim145limitation is required.")]
        public NomValue Lim145limitation { get; set; }

        public NomValue Person { get; set; }

        public NomValue Organization { get; set; }

        [Required(ErrorMessage = "FromDate is required.")]
        public DateTime FromDate { get; set; }

        [Required(ErrorMessage = "ToDate is required.")]
        public DateTime? ToDate { get; set; }

        public string Notes { get; set; }
    }
}
