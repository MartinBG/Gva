using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftPartDO
    {
        [Required(ErrorMessage = "AircraftPart is required.")]
        public NomValue AircraftPart { get; set; }

        [Required(ErrorMessage = "PartProducer is required.")]
        public NomValue PartProducer { get; set; }

        [Required(ErrorMessage = "Model is required.")]
        public string Model { get; set; }

        public string ModelAlt { get; set; }

        public string Sn { get; set; }

        [Required(ErrorMessage = "Count is required.")]
        public int? Count { get; set; }

        [Required(ErrorMessage = "AircraftPartStatus is required.")]
        public NomValue AircraftPartStatus { get; set; }

        public DateTime? ManDate { get; set; }

        public string ManPlace { get; set; }

        public string Description { get; set; }

    }
}
