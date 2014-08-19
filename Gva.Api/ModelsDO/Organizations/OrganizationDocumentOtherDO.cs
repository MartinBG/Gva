using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Organizations
{
    public class OrganizationDocumentOtherDO
    {
        public string DocumentNumber { get; set; }

        public string DocumentPersonNumber { get; set; }

        [Required(ErrorMessage = "DocumentPublisher is required.")]
        public string DocumentPublisher { get; set; }

        [Required(ErrorMessage = "DocumentDateValidFrom is required.")]
        public DateTime? DocumentDateValidFrom { get; set; }

        public DateTime? DocumentDateValidTo { get; set; }

        [Required(ErrorMessage = "DocumentType is required.")]
        public NomValue DocumentType { get; set; }

        [Required(ErrorMessage = "DocumentRole is required.")]
        public NomValue DocumentRole { get; set; }

        [Required(ErrorMessage = "Valid is required.")]
        public NomValue Valid { get; set; }

        public string Notes { get; set; }
    }
}
