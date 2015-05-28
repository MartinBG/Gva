using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftCertRadioDO
    {
        public AircraftCertRadioDO()
        {
            this.Entries = new List<AircraftCertRadioEntryDO>();
        }

        public int AslNumber { get; set; }

        public DateTime? IssueDate { get; set; }

        public AircraftInspectorDO Inspector { get; set; }

        public List<AircraftCertRadioEntryDO> Entries { get; set; }
    }
}