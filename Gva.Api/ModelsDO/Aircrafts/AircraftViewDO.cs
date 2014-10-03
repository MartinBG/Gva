using System;
using Gva.Api.Models.Views.Aircraft;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftViewDO
    {
        public AircraftViewDO()
        {
        }

        public AircraftViewDO(GvaViewAircraft aircraftData)
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
            this.EngineAlt = aircraftData.EngineAlt;
            this.PropellerAlt = aircraftData.PropellerAlt;
            this.ModifOrWingColorAlt = aircraftData.ModifOrWingColorAlt;
            this.Mark = aircraftData.Mark;
            this.ActNumber = aircraftData.ActNumber;
            this.CertNumber = aircraftData.CertNumber;
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

        public string EngineAlt { get; set; }

        public string PropellerAlt { get; set; }

        public string ModifOrWingColorAlt { get; set; }

        public string Mark { get; set; }

        public int? ActNumber { get; set; }

        public int? CertNumber { get; set; }
    }
}