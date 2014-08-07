using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonMedicalDO
    {
        public PersonMedicalDO()
        {
            this.Limitations = new List<NomValue>();
        }

        [Required(ErrorMessage = "DocumentNumberPrefix is required.")]
        public string DocumentNumberPrefix { get; set; }

        [Required(ErrorMessage = "DocumentNumber is required.")]
        public string DocumentNumber { get; set; }

        public string DocumentNumberSuffix { get; set; }

        [Required(ErrorMessage = "MedClass is required.")]
        public NomValue MedClass { get; set; }

        [Required(ErrorMessage = "DocumentDateValidFrom is required.")]
        public DateTime? DocumentDateValidFrom { get; set; }

        [Required(ErrorMessage = "DocumentDateValidTo is required.")]
        public DateTime? DocumentDateValidTo { get; set; }

        public List<NomValue> Limitations { get; set; }

        [Required(ErrorMessage = "DocumentPublisher is required.")]
        public NomValue DocumentPublisher { get; set; }

        public string Notes { get; set; }
    }
}
