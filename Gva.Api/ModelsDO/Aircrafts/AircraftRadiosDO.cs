using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftRadiosDO
    {
        public NomValue AircraftRadioType { get; set; }

        public string Count { get; set; }

        public string Producer { get; set; }

        public string Model { get; set; }

        public string Power { get; set; }

        public string Class { get; set; }

        public string Bandwidth { get; set; }
    }
}
