using System;
using System.Linq;
using System.Collections.Generic;
using Gva.Api.Models;
namespace Gva.Api.ModelsDO
{
    public class AircraftDO
    {
        public AircraftDO(
            GvaViewAircraft aircraftData)
        {
            this.Id = aircraftData.LotId;
            this.ManSN = aircraftData.ManSN;
            this.Model = aircraftData.Model;
            this.ModelAlt = aircraftData.ModelAlt;
            this.OutputDate = aircraftData.OutputDate;
            this.ICAO = aircraftData.ICAO;
            this.AircraftCategory = aircraftData.AircraftCategory;
            this.AircraftProducer = aircraftData.AircraftProducer;
            this.Engine = aircraftData.Engine;
            this.Propeller = aircraftData.Propeller;
            this.ModifOrWingColor = aircraftData.ModifOrWingColor;
        }
        public int Id { get; set; }

        public string ManSN { get; set; }

        public string Model { get; set; }

        public string ModelAlt { get; set; }

        public DateTime? OutputDate { get; set; }

        public string ICAO { get; set; }

        public string AircraftCategory { get; set; }

        public string AircraftProducer { get; set; }

        public string Engine { get; set; }

        public string Propeller { get; set; }

        public string ModifOrWingColor { get; set; }

        public string Mark { get; set; }
    }
}