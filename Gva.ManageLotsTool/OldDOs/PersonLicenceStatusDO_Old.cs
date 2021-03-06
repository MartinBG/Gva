﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.ManageLotsTool.OldDOs
{
    public class PersonLicenceStatusDO_Old
    {
        [Required(ErrorMessage = "Valid is required!")]
        public NomValue Valid { get; set; }

        [Required(ErrorMessage = "ChangeReason is required!")]
        public NomValue ChangeReason { get; set; }

        [Required(ErrorMessage = "ChangeDate is required!")]
        public DateTime? ChangeDate { get; set; }

        public NomValue Inspector { get; set; }

        public string Notes { get; set; }
    }
}
