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
        public int? ValidId { get; set; }

        [Required(ErrorMessage = "Organization is required.")]
        public int? OrganizationId { get; set; }

        [Required(ErrorMessage = "EmploymentCategory is required.")]
        public int? EmploymentCategoryId { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public int? CountryId { get; set; }

        public string Notes { get; set; }
    }
}
