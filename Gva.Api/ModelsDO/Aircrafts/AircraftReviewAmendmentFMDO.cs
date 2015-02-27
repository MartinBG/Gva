﻿using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftReviewAmendmentFMDO
    {
        [Required(ErrorMessage = "IssueDate is required.")]
        public DateTime? IssueDate { get; set; }

        [Required(ErrorMessage = "ValidToDate is required.")]
        public DateTime? ValidToDate { get; set; }

        public NomValue Organization { get; set; }

        public AircraftInspectorDO Inspector { get; set; }
    }
}
