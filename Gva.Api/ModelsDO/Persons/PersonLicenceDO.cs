using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using System.Collections.Generic;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonLicenceDO
    {
        [Required(ErrorMessage = "LicenceType is required.")]
        public int? LicenceTypeId { get; set; }

        public int? LicenceNumber { get; set; }

        public string ForeignLicenceNumber { get; set; }

        public int? ForeignPublisherId { get; set; }

        public int? EmploymentId { get; set; }

        [Required(ErrorMessage = "Publisher is required.")]
        public int? PublisherId { get; set; }

        public int? ValidId { get; set; }

        public List<PersonLicenceStatusDO> Statuses { get; set; }
    }
}
