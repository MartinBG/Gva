using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftCertAirworthinessDO
    {
        [Required(ErrorMessage = "AircraftCertificateType is required.")]
        public NomValue AircraftCertificateType { get; set; }

        [Required(ErrorMessage = "RefNumber is required.")]
        public string RefNumber { get; set; }

        [Required(ErrorMessage = "IssueDate is required.")]
        public DateTime? IssueDate { get; set; }

        [Required(ErrorMessage = "ValidToDate is required.")]
        public DateTime? ValidToDate { get; set; }

    }
}
