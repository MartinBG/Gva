using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using Gva.Api.ModelsDO.Common;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftDocumentOccurenceDO
    {
        [Required(ErrorMessage = "AircraftOccurrenceClass is required.")]
        public NomValue AircraftOccurrenceClass { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public NomValue Country { get; set; }

        [Required(ErrorMessage = "LocalDate is required.")]
        public DateTime? LocalDate { get; set; }

        [Required(ErrorMessage = "LocalTime is required.")]
        public int? LocalTime { get; set; }

        [Required(ErrorMessage = "Area is required.")]
        public string Area { get; set; }

        public string Notes { get; set; }

        public string OccurrenceNotes { get; set; }

    }
}
