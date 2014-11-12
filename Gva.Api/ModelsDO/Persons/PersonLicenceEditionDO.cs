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
            this.IncludedRatings = new List<IncludedRatingDO>();
            this.IncludedExams = new List<IncludedDocumentDO>();
            this.IncludedTrainings = new List<IncludedDocumentDO>();
            this.IncludedLangCerts = new List<IncludedDocumentDO>();
            this.IncludedChecks = new List<IncludedDocumentDO>();
            this.IncludedMedicals = new List<IncludedDocumentDO>();
            this.IncludedLicences = new List<IncludedDocumentDO>();
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

        public List<IncludedRatingDO> IncludedRatings { get; set; }

        public List<IncludedDocumentDO> IncludedExams { get; set; }

        public List<IncludedDocumentDO> IncludedTrainings { get; set; }

        public List<IncludedDocumentDO> IncludedLangCerts { get; set; }

        public List<IncludedDocumentDO> IncludedChecks { get; set; }

        public List<IncludedDocumentDO> IncludedMedicals { get; set; }

        public List<IncludedDocumentDO> IncludedLicences { get; set; }
    }
}
