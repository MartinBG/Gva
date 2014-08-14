using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftCertAirworthinessForm15MainDO
    {
        [Required(ErrorMessage = "AirworthinessReviewType is required.")]
        public NomValue AirworthinessReviewType { get; set; }

        [Required(ErrorMessage = "IssueDate is required.")]
        public DateTime? IssueDate { get; set; }

        [Required(ErrorMessage = "ValidToDate is required.")]
        public DateTime? ValidToDate { get; set; }

        [Required(ErrorMessage = "ApprovalNumber is required.")]
        public string ApprovalNumber { get; set; }

        [Required(ErrorMessage = "Inspector is required.")]
        public AircraftInspectorDO Inspector { get; set; }
    }
}
