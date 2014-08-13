using System;
using System.ComponentModel.DataAnnotations;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftCertPermitToFlyDO
    {
        [Required(ErrorMessage = "IssuePlace is required.")]
        public string IssuePlace { get; set; }

        [Required(ErrorMessage = "IssueDate is required.")]
        public DateTime IssueDate { get; set; }

        [Required(ErrorMessage = "ValidToDate is required.")]
        public DateTime? ValidToDate { get; set; }

        public string Purpose { get; set; }

        public string PurposeAlt { get; set; }

        public string PointFrom { get; set; }

        public string PointFromAlt { get; set; }

        public string PointTo { get; set; }

        public string PointToAlt { get; set; }

        public string PlanStops { get; set; }

        public string PlanStopsAlt { get; set; }

        public string Notes { get; set; }

        public string NotesAlt { get; set; }

        public string Crew { get; set; }

        public string CrewAlt { get; set; }
    }
}
