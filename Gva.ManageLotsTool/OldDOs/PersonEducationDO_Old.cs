using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.ManageLotsTool.OldDOs
{
    public class PersonEducationDO_Old
    {
        [Required(ErrorMessage = "DocumentNumber is required.")]
        public string DocumentNumber { get; set; }

        [Required(ErrorMessage = "CompletionDate is required.")]
        public DateTime? CompletionDate { get; set; }

        [Required(ErrorMessage = "Graduation is required.")]
        public NomValue Graduation { get; set; }

        [Required(ErrorMessage = "School is required.")]
        public NomValue School { get; set; }

        [Required(ErrorMessage = "Speciality is required.")]
        public string Speciality { get; set; }

        public string Notes { get; set; }
    }
}
