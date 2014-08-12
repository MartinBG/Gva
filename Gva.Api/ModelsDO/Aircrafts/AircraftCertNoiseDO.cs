using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using System.Collections.Generic;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftCertNoiseDO
    {
        public string IssueNumber { get; set; }

        public DateTime? IssueDate { get; set; }

        public string Chapter { get; set; }

        public string Tcds { get; set; }

        public string Tcdsn { get; set; }

        public double? Flyover { get; set; }

        public double? Approach { get; set; }

        public double? Lateral { get; set; }

        public double? Overflight { get; set; }

        public double? Takeoff { get; set; }

        public string AdditionalModification { get; set; }

        public List<ApplicationNomDO> Applications { get; set; }

    }
}
