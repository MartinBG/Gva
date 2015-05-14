using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftCertAirworthinessDO
    {
        public NomValue AirworthinessCertificateType { get; set; }

        public NomValue Registration { get; set; }

        public string StampNumber { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime? IssueDate { get; set; }

        public DateTime? ValidFromDate { get; set; }

        public DateTime? ValidToDate { get; set; }

        public NomValue Organization { get; set; }

        public AircraftInspectorDO Inspector { get; set; }
    }
}
