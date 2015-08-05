using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using Gva.Api.ModelsDO.Persons;

namespace Gva.ManageLotsTool.OldDOs
{
    public class PersonCheckDO_Old
    {
        public PersonCheckDO_Old()
        {
            Reports = new List<RelatedReportDO>();
            RatingTypes = new List<NomValue>();
        }

        public NomValue AircraftTypeCategory { get; set; }

        public NomValue AircraftTypeGroup { get; set; }

        public List<NomValue> RatingTypes { get; set; }

        public NomValue LocationIndicator { get; set; }

        public string Sector { get; set; }

        public string DocumentNumber { get; set; }

        public int? DocumentPersonNumber { get; set; }

        [Required(ErrorMessage = "DocumentType is required.")]
        public NomValue DocumentType { get; set; }

        [Required(ErrorMessage = "DocumentRole is required.")]
        public NomValue DocumentRole { get; set; }

        public string DocumentPublisher { get; set; }

        [Required(ErrorMessage = "DocumentDateValidFrom is required.")]
        public DateTime? DocumentDateValidFrom { get; set; }

        public DateTime? DocumentDateValidTo { get; set; }

        public NomValue RatingClass { get; set; }

        public NomValue Authorization { get; set; }

        public NomValue PersonCheckRatingValue { get; set; }

        public NomValue LicenceType { get; set; }

        [Required(ErrorMessage = "Valid is required.")]
        public NomValue Valid { get; set; }

        public string Notes { get; set; }

        public List<RelatedReportDO> Reports { get; set; }
    }
}
