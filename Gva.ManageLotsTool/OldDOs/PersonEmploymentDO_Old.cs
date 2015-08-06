using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.ManageLotsTool.OldDOs
{
    public class PersonEmploymentDO_Old
    {
        [Required(ErrorMessage = "Hiredate is required.")]
        public DateTime? Hiredate { get; set; }

        [Required(ErrorMessage = "Valid is required.")]
        public NomValue Valid { get; set; }

        [Required(ErrorMessage = "Organization is required.")]
        public NomValue Organization { get; set; }

        [Required(ErrorMessage = "EmploymentCategory is required.")]
        public NomValue EmploymentCategory { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public NomValue Country { get; set; }

        public string Notes { get; set; }
    }
}
