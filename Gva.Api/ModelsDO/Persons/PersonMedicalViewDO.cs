using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonMedicalViewDO
    {
        public PersonMedicalViewDO()
        {
            this.Limitations = new List<NomValue>();
        }

        public int PartIndex { get; set; }

        public int PartId { get; set; }

        public CaseDO Case { get; set; }

        public string DocumentNumberPrefix { get; set; }

        public string DocumentNumber { get; set; }

        public string DocumentNumberSuffix { get; set; }

        [Required(ErrorMessage = "MedClass is required.")]
        public NomValue MedClass { get; set; }

        public DateTime? DocumentDateValidFrom { get; set; }

        public DateTime? DocumentDateValidTo { get; set; }

        public IEnumerable<NomValue> Limitations { get; set; }

        public NomValue DocumentPublisherId { get; set; }

        public string Notes { get; set; }
    }
}
