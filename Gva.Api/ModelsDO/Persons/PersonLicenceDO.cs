using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using System.Collections.Generic;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonLicenceDO
    {
        public bool isFcl { get; set; }

        [Required(ErrorMessage = "LicenceType is required.")]
        public NomValue LicenceType { get; set; }

        public int? LicenceNumber { get; set; }

        public string ForeignLicenceNumber { get; set; }

        public NomValue ForeignPublisher { get; set; }

        public NomValue Employment { get; set; }

        [Required(ErrorMessage = "Publisher is required.")]
        public NomValue Publisher { get; set; }

        public NomValue Valid { get; set; }

        public List<PersonLicenceStatusDO> Statuses { get; set; }
    }
}
