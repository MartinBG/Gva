using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonCheckDO
    {
        public NomValue AircraftTypeGroup { get; set; }

        public NomValue RatingType { get; set; }

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
    }
}
