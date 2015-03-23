﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftCertAirworthinessFMDO
    {
        public AircraftCertAirworthinessFMDO()
        {
            this.Reviews = new List<AircraftCertAirworthinessReviewDO>();
        }

        public NomValue AirworthinessCertificateType { get; set; }

        public NomValue Registration { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime? IssueDate { get; set; }

        public DateTime? ValidToDate { get; set; }

        public List<AircraftCertAirworthinessReviewDO> Reviews { get; set; }
    }
}
