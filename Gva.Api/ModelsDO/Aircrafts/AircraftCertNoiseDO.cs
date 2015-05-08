using System;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftCertNoiseDO
    {
        public string IssueNumber { get; set; }

        public DateTime? IssueDate { get; set; }

        public string Chapter { get; set; }

        public string Tcdsn { get; set; }

        public double? Flyover { get; set; }

        public double? Approach { get; set; }

        public double? Lateral { get; set; }

        public double? Overflight { get; set; }

        public double? Takeoff { get; set; }

        public string AdditionalModifications { get; set; }

        public string AdditionalModificationsAlt { get; set; }

        public string Remarks { get; set; }

        public int? PrintedFileId { get; set; }
    }
}
