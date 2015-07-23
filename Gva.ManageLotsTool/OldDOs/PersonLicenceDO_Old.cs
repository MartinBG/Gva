using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using System.Collections.Generic;

namespace Gva.ManageLotsTool.OldDOs
{
    public class PersonLicenceDO_Old
    {
        [Required(ErrorMessage = "LicenceType is required.")]
        public NomValue LicenceType { get; set; }

        public int? LicenceNumber { get; set; }

        public string ForeignLicenceNumber { get; set; }

        public NomValue ForeignPublisher { get; set; }

        public NomValue Employment { get; set; }

        [Required(ErrorMessage = "Publisher is required.")]
        public NomValue Publisher { get; set; }

        public NomValue Valid { get; set; }

        public List<PersonLicenceStatusDO_Old> Statuses { get; set; }
    }
}
