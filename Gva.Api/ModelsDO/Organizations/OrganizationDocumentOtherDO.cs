using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Organizations
{
    public class OrganizationDocumentOtherDO
    {
        public string DocumentNumber { get; set; }

        public string DocumentPersonNumber { get; set; }

        public string DocumentPublisher { get; set; }

        public DateTime? DocumentDateValidFrom { get; set; }

        public DateTime? DocumentDateValidTo { get; set; }

        public int? DocumentTypeId { get; set; }

        public int? DocumentRoleId { get; set; }

        public int? ValidId { get; set; }

        public string Notes { get; set; }
    }
}
