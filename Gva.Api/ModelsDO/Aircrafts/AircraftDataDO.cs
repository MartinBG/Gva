using Common.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftDataDO
    {
        [Required(ErrorMessage = "AircraftProducer is required.")]
        public NomValue AircraftProducer { get; set; }

        [Required(ErrorMessage = "AirCategory is required.")]
        public NomValue AirCategory { get; set; }

        public string ICAO { get; set; }

        [Required(ErrorMessage = "ManSN is required.")]
        public string ManSN { get; set; }

        [Required(ErrorMessage = "Model is required.")]
        public string Model { get; set; }

        [Required(ErrorMessage = "ModelAlt is required.")]
        public string ModelAlt { get; set; }

        [Required(ErrorMessage = "OutputDate is required.")]
        public DateTime? OutputDate { get; set; }

        public string Engine { get; set; }

        public string EngineAlt { get; set; }

        public string DocRoom { get; set; }

        public string Propeller { get; set; }

        public string PropellerAlt { get; set; }

        public string Seats { get; set; }

        public string ModifOrWingColor { get; set; }

        public string ModifOrWingColorAlt { get; set; }

        public int? MaxMassT { get; set; }

        public int? MaxMassL { get; set; }
    }
}
