using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Organizations
{
    public class OrganizationApprovedAircraftsDO
    {
        [Required(ErrorMessage = "AircraftTypeGroup is required.")]
        public NomValue AircraftTypeGroup { get; set; }

        [Required(ErrorMessage = "DateApproved is required.")]
        public DateTime? DateApproved { get; set; }

        [Required(ErrorMessage = "Inspector is required.")]
        public NomValue Inspector { get; set; }

        [Required(ErrorMessage = "Valid is required.")]
        public NomValue Valid { get; set; }

        public string Notes { get; set; }
    }
}
