using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonLicenceEditionDO
    {
        public PersonLicenceEditionDO()
        {
            this.Limitations = new List<NomValue>();
            this.Applications = new List<ApplicationNomDO>();
            this.IncludedRatings = new List<int>();
            this.IncludedTrainings = new List<int>();
            this.IncludedChecks = new List<int>();
            this.IncludedMedicals = new List<int>();
            this.IncludedLicences = new List<int>();
        }

        public int? Index { get; set; }

        public NomValue Inspector { get; set; }

        [Required(ErrorMessage = "DocumentDateValidFrom is required!")]
        public DateTime? DocumentDateValidFrom { get; set; }

        [Required(ErrorMessage = "DocumentDateValidTo is required!")]
        public DateTime? DocumentDateValidTo { get; set; }

        [Required(ErrorMessage = "LicenceAction is required!")]
        public NomValue LicenceAction { get; set; }

        public PersonLicenceAmlLimitationsDO AmlLimitations { get; set; }

        public List<NomValue> Limitations { get; set; }

        public string Notes { get; set; }

        public string NotesAlt { get; set; }

        public List<ApplicationNomDO> Applications { get; set; }

        public List<int> IncludedRatings { get; set; }

        public List<int> IncludedTrainings { get; set; }

        public List<int> IncludedChecks { get; set; }

        public List<int> IncludedMedicals { get; set; }

        public List<int> IncludedLicences { get; set; }
    }
}
