﻿using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftCertAirworthinessReviewOtherDO
    {
        public DateTime? IssueDate { get; set; }

        public DateTime? ValidToDate { get; set; }

        public AircraftInspectorDO Inspector { get; set; }
    }
}
