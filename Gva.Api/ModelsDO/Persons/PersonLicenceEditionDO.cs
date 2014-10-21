using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using Common.Filters;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonLicenceEditionDO
    {
        public PersonLicenceEditionDO()
        {
            this.Limitations = new List<NomValue>();
            this.IncludedRatings = new List<int>();
            this.IncludedExams = new List<int>();
            this.IncludedTrainings = new List<int>();
            this.IncludedLangCerts = new List<int>();
            this.IncludedChecks = new List<int>();
            this.IncludedMedicals = new List<int>();
            this.IncludedLicences = new List<int>();
        }

        public int? LicencePartIndex { get; set; }

        public int Index { get; set; }

        public NomValue Inspector { get; set; }

        [Required(ErrorMessage = "DocumentDateValidFrom is required!")]
        public DateTime? DocumentDateValidFrom { get; set; }

        public DateTime? DocumentDateValidTo { get; set; }

        [Required(ErrorMessage = "LicenceAction is required!")]
        public NomValue LicenceAction { get; set; }

        public PersonLicenceAmlLimitationsDO AmlLimitations { get; set; }

        public List<NomValue> Limitations { get; set; }

        public string Notes { get; set; }

        public string NotesAlt { get; set; }

        public string StampNumber { get; set; }

        public List<int> IncludedRatings { get; set; }

        public List<int> IncludedExams { get; set; }

        public List<int> IncludedTrainings { get; set; }

        public List<int> IncludedLangCerts { get; set; }

        public List<int> IncludedChecks { get; set; }

        public List<int> IncludedMedicals { get; set; }

        public List<int> IncludedLicences { get; set; }
    }
}
