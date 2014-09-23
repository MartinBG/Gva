using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonDocumentExamDO
    {
        public string DocumentNumber { get; set; }

        public NomValue Valid { get; set; }

        [Required(ErrorMessage = "DocumentDateValidFrom is required.")]
        public DateTime? DocumentDateValidFrom { get; set; }

        public DateTime? DocumentDateValidTo { get; set; }

        [Required(ErrorMessage = "DocumentType is required.")]
        public NomValue DocumentType { get; set; }

        public string DocumentPublisher { get; set; }

        public string Notes { get; set; }
    }
}
