﻿using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Common
{
    public class DisparityDO
    {
        [Required(ErrorMessage = "DetailCode is required.")]
        public string DetailCode { get; set; }

        [Required(ErrorMessage = "RefNumber is required.")]
        public string RefNumber { get; set; }

        [Required(ErrorMessage = "DisparityLevel is required.")]
        public NomValue DisparityLevel { get; set; }

        public string RectifyAction { get; set; }

        public string ClosureDocument { get; set; }

        public DateTime? RemovalDate { get; set; }

        public DateTime? ClosureDate { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
    }
}
