﻿using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftCertAirworthinessReviewOtherDO
    {
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
