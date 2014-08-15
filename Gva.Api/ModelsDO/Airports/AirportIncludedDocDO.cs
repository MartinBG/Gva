using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Airports
{
    public class AirportIncludedDocDO
    {
        [Required(ErrorMessage = "PartIndex is required.")]
        public int? PartIndex { get; set; }

        [Required(ErrorMessage = "SetPartAlias is required.")]
        public string SetPartAlias { get; set; }

        public NomValue Inspector { get; set; }

        public DateTime? ApprovalDate { get; set; }
    }
}
