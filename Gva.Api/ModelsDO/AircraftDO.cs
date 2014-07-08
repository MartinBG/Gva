using System;
using Gva.Api.Models.Views.Aircraft;

namespace Gva.Api.ModelsDO
{
    public class AircraftDO
    {
        public AircraftDO()
        {
        }

        public AircraftDO(GvaViewAircraft aircraftData)
        {
            this.Id = aircraftData.LotId;
            this.ManSN = aircraftData.ManSN;
            this.Model = aircraftData.Model;
            this.ModelAlt = aircraftData.ModelAlt;
            this.OutputDate = aircraftData.OutputDate;
            this.ICAO = aircraftData.ICAO;
            this.AirCategory = aircraftData.AirCategory != null ? aircraftData.AirCategory.Name : null;
            this.AircraftProducer = aircraftData.AircraftProducer != null ?aircraftData.AircraftProducer.Name : null;
            this.Engine = aircraftData.Engine;
            this.Propeller = aircraftData.Propeller;
            this.ModifOrWingColor = aircraftData.ModifOrWingColor;
            this.Mark = aircraftData.Mark;
        }

        public int Id { get; set; }

        public string ManSN { get; set; }

        public string Model { get; set; }

        public string ModelAlt { get; set; }

        public DateTime? OutputDate { get; set; }

        public string ICAO { get; set; }

        public string AirCategory { get; set; }

        public string AircraftProducer { get; set; }

        public string Engine { get; set; }

        public string Propeller { get; set; }

        public string ModifOrWingColor { get; set; }

        public string Mark { get; set; }
    }
}