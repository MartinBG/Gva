using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftCertRadioDO
    {
        [Required(ErrorMessage = "CertNumber is required.")]
        public string CertNumber { get; set; }

        [Required(ErrorMessage = "IssueDate is required.")]
        public DateTime IssueDate { get; set; }

        [Required(ErrorMessage = "ValidToDate is required.")]
        public DateTime? ValidToDate { get; set; }

        [Required(ErrorMessage = "Valid is required.")]
        public NomValue Valid { get; set; }

        public AircraftRadiosDO Radios { get; set; }
    }
}
