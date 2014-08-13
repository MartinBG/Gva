using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonEmploymentDO
    {
        [Required(ErrorMessage = "Hiredate is required.")]
        public DateTime? Hiredate { get; set; }

        [Required(ErrorMessage = "Valid is required.")]
        public NomValue Valid { get; set; }

        public NomValue Organization { get; set; }

        [Required(ErrorMessage = "EmploymentCategory is required.")]
        public NomValue EmploymentCategory { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public NomValue Country { get; set; }

        public string Notes { get; set; }
    }
}
