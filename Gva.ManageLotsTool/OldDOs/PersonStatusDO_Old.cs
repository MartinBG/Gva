using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.ManageLotsTool.OldDOs
{
    public class PersonStatusDO_Old
    {
        [Required(ErrorMessage = "PersonStatusType is required.")]
        public NomValue PersonStatusType { get; set; }

        public string DocumentNumber { get; set; }

        [Required(ErrorMessage = "DocumentDateValidFrom is required.")]
        public DateTime? DocumentDateValidFrom { get; set; }

        public DateTime? DocumentDateValidTo { get; set; }

        public string Notes { get; set; }
    }
}
