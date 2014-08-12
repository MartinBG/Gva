using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonDocumentIdDO
    {
        [Required(ErrorMessage = "DocumentType is required.")]
        public NomValue DocumentType { get; set; }

        [Required(ErrorMessage = "DocumentNumber is required.")]
        public string DocumentNumber { get; set; }

        [Required(ErrorMessage = "Valid is required.")]
        public NomValue Valid { get; set; }

        [Required(ErrorMessage = "DocumentDateValidFrom is required.")]
        public DateTime? DocumentDateValidFrom { get; set; }

        [Required(ErrorMessage = "DocumentDateValidTo is required.")]
        public DateTime? DocumentDateValidTo { get; set; }

        [Required(ErrorMessage = "DocumentPublisher is required.")]
        public string DocumentPublisher { get; set; }

        public string Notes { get; set; }
    }
}
