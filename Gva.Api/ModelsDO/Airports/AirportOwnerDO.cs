using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Airports
{
    public class AirportOwnerDO
    {
        [Required(ErrorMessage = "AirportRelation is required.")]
        public NomValue AirportRelation { get; set; }

        public NomValue Person { get; set; }

        public NomValue Organization { get; set; }

        [Required(ErrorMessage = "DocumentNumber is required.")]
        public string DocumentNumber { get; set; }

        [Required(ErrorMessage = "DocumentDate is required.")]
        public DateTime? DocumentDate { get; set; }

        [Required(ErrorMessage = "FromDate is required.")]
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string ReasonTerminate { get; set; }

        public string Notes { get; set; }
    }
}
