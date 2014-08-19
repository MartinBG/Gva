using Gva.Api.Models.Views.Airport;

namespace Gva.Api.ModelsDO.Airports
{
    public class AirportViewDO
    {
        public AirportViewDO(GvaViewAirport airportData)
        {
            this.Id = airportData.LotId;
            this.Name = airportData.Name;
            this.NameAlt = airportData.NameAlt;
            this.AirportType = airportData.AirportType.Name;
            this.Place = airportData.Place;
            this.ICAO = airportData.ICAO;
            this.Runway = airportData.Runway;
            this.Course = airportData.Course;
            this.Excess = airportData.Excess;
            this.Concrete = airportData.Concrete;
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string Place { get; set; }

        public string AirportType { get; set; }

        public string ICAO { get; set; }

        public string Runway { get; set; }

        public string Course { get; set; }

        public string Excess { get; set; }

        public string Concrete { get; set; }
    }
}