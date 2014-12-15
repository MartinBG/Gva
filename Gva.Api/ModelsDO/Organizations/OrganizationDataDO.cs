using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using Common.ValidationAttributes;

namespace Gva.Api.ModelsDO.Organizations
{
    public class OrganizationDataDO
    {

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "NameAlt is required.")]
        public string NameAlt { get; set; }

        public string Code { get; set; }

        public string Uin { get; set; }

        [MinimumLength(1, ErrorMessage = "CaseTypes are required.")]
        public List<NomValue> CaseTypes { get; set; }

        public string Cao { get; set; }

        public DateTime? DateCaoFirstIssue { get; set; }

        public DateTime? DateCaoLastIssue { get; set; }

        public DateTime? DateCaoValidTo { get; set; }

        public DateTime? DateValidTo { get; set; }

        [Required(ErrorMessage = "Valid is required.")]
        public NomValue Valid { get; set; }

        public string Icao { get; set; }

        public string Iata { get; set; }

        public string Sita { get; set; }

        [Required(ErrorMessage = "OrganizationType is required.")]
        public NomValue OrganizationType { get; set; }

        [Required(ErrorMessage = "OrganizationKind is required.")]
        public NomValue OrganizationKind { get; set; }

        public string DocRoom { get; set; }

        public string Phones { get; set; }

        public string WebSite { get; set; }

        public string Notes { get; set; }
    }
}
