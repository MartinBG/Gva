using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftDocumentOtherDO
    {
        public string DocumentNumber { get; set; }

        public DateTime DocumentDateValidFrom { get; set; }

        public DateTime? DocumentDateValidTo { get; set; }

        [Required(ErrorMessage = "DocumentPublisher is required.")]
        public string DocumentPublisher { get; set; }

        [Required(ErrorMessage = "OtherDocumentType is required.")]
        public NomValue OtherDocumentType { get; set; }

        [Required(ErrorMessage = "OtherDocumentRole is required.")]
        public NomValue OtherDocumentRole { get; set; }

        public NomValue Valid { get; set; }

        public string Notes { get; set; }
    }
}
