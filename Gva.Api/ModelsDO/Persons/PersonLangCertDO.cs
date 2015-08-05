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
            RatingTypes = new List<int>();
        }

        public string DocumentNumber { get; set; }

        public int? DocumentPersonNumber { get; set; }

        [Required(ErrorMessage = "DocumentPublisher is required.")]
        public string DocumentPublisher { get; set; }

        [Required(ErrorMessage = "DocumentDateValidFrom is required.")]
        public DateTime? DocumentDateValidFrom { get; set; }

        public DateTime? DocumentDateValidTo { get; set; }

        public List<int> RatingTypes { get; set; }

        public int? AircraftTypeCategoryId { get; set; }

        public int? AircraftTypeGroupId { get; set; }

        public int? LicenceTypeId { get; set; }

        public int? LocationIndicatorId { get; set; }

        public string Sector { get; set; }

        public int? RatingClassId { get; set; }

        public int? AuthorizationId { get; set; }

        [Required(ErrorMessage = "DocumentType is required.")]
        public int? DocumentTypeId { get; set; }

        [Required(ErrorMessage = "DocumentRole is required.")]
        public int? DocumentRoleId { get; set; }

        public int? LangLevelId { get; set; }

        public List<PersonLangLevelDO> LangLevelEntries { get; set; }

        [Required(ErrorMessage = "Valid is required.")]
        public int? ValidId { get; set; }

        public string Notes { get; set; }
    }
}
