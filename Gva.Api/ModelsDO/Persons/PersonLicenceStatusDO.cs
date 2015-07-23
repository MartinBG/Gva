using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonLicenceStatusDO
    {
        [Required(ErrorMessage = "Valid is required!")]
        public int? ValidId { get; set; }

        [Required(ErrorMessage = "ChangeReason is required!")]
        public int? ChangeReasonId { get; set; }

        [Required(ErrorMessage = "ChangeDate is required!")]
        public DateTime? ChangeDate { get; set; }

        public int? InspectorId { get; set; }

        public string Notes { get; set; }
    }
}
