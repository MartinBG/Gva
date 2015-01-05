using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonLangCertDO
    {
        public PersonLangCertDO() 
        {
            LangLevelEntries = new List<PersonLangLevelDO>();
        }

        public string DocumentNumber { get; set; }

        public int? DocumentPersonNumber { get; set; }

        [Required(ErrorMessage = "DocumentPublisher is required.")]
        public string DocumentPublisher { get; set; }

        [Required(ErrorMessage = "DocumentDateValidFrom is required.")]
        public DateTime? DocumentDateValidFrom { get; set; }

        public DateTime? DocumentDateValidTo { get; set; }

        public NomValue RatingType { get; set; }

        public NomValue AircraftTypeGroup { get; set; }

        public NomValue LicenceType { get; set; }

        public NomValue LocationIndicator { get; set; }

        public string Sector { get; set; }

        public NomValue RatingClass { get; set; }

        public NomValue Authorization { get; set; }

        [Required(ErrorMessage = "DocumentType is required.")]
        public NomValue DocumentType { get; set; }

        [Required(ErrorMessage = "DocumentRole is required.")]
        public NomValue DocumentRole { get; set; }

        public NomValue LangLevel { get; set; }

        public List<PersonLangLevelDO> LangLevelEntries { get; set; }

        [Required(ErrorMessage = "Valid is required.")]
        public NomValue Valid { get; set; }

        public string Notes { get; set; }
    }
}
