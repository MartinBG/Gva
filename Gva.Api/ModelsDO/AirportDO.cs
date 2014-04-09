using System;
using System.Linq;
using System.Collections.Generic;
using Gva.Api.Models;
namespace Gva.Api.ModelsDO
{
    public class AirportDO
    {
        public AirportDO(
            GvaViewAirport airportData)
        {
            this.Id = airportData.LotId;
            this.Name = airportData.Name;
            this.AirportType = airportData.AirportType;
            this.Place = airportData.Place;
            this.ICAO = airportData.ICAO;
            this.Runway = airportData.Runway;
            this.Course = airportData.Course;
            this.Excess = airportData.Excess;
            this.Concrete = airportData.Concrete;
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Place { get; set; }

        public string AirportType { get; set; }

        public string ICAO { get; set; }

        public string Runway { get; set; }

        public string Course { get; set; }

        public string Excess { get; set; }

        public string Concrete { get; set; }
    }
}