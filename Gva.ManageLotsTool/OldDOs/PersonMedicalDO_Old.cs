using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.ManageLotsTool.OldDOs
{
    public class PersonMedicalDO_Old
    {
        public PersonMedicalDO_Old()
        {
            this.Limitations = new List<NomValue>();
        }

        [Required(ErrorMessage = "DocumentNumberPrefix is required.")]
        public string DocumentNumberPrefix { get; set; }

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
