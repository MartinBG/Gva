using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonEducationDO
    {
        [Required(ErrorMessage = "DocumentNumber is required.")]
        public string DocumentNumber { get; set; }

        [Required(ErrorMessage = "CompletionDate is required.")]
        public DateTime? CompletionDate { get; set; }

        [Required(ErrorMessage = "Graduation is required.")]
        public int? GraduationId { get; set; }

        [Required(ErrorMessage = "School is required.")]
        public int? SchoolId { get; set; }

        [Required(ErrorMessage = "Speciality is required.")]
        public string Speciality { get; set; }

        public string Notes { get; set; }
    }
}
