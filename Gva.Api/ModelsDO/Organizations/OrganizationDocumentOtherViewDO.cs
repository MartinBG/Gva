using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Organizations
{
    public class OrganizationDocumentOtherViewDO
    {
        public CaseDO Case { get; set; }

        public int PartIndex { get; set; }

        public int PartId { get; set; }

        public string DocumentNumber { get; set; }

        public string DocumentPersonNumber { get; set; }

        public string DocumentPublisher { get; set; }

        public DateTime? DocumentDateValidFrom { get; set; }

        public DateTime? DocumentDateValidTo { get; set; }

        public NomValue DocumentType { get; set; }

        public NomValue DocumentRole { get; set; }

        public NomValue Valid { get; set; }

        public string Notes { get; set; }
    }
}
