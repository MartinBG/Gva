using System.Collections.Generic;
using Common.Data;
using Gva.Api.Models.Views.Airport;
using Gva.Api.ModelsDO.Airports;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Airport
{
    public class AirportProjection : Projection<GvaViewAirport>
    {
        public AirportProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Airport")
        {
        }

        public override IEnumerable<GvaViewAirport> Execute(PartCollection parts)
        {
            var airportData = parts.Get<AirportDataDO>("airportData");

            if (airportData == null)
            {
                return new GvaViewAirport[] { };
            }

            return new[] { this.Create(airportData) };
        }

        private GvaViewAirport Create(PartVersion<AirportDataDO> airportData)
        {
            GvaViewAirport airport = new GvaViewAirport();

            airport.LotId = airportData.Part.Lot.LotId;
            airport.Name = airportData.Content.Name;
            airport.NameAlt = airportData.Content.NameAlt;
            airport.Place = airportData.Content.Place;
            airport.AirportTypeId = airportData.Content.AirportType.NomValueId;
            airport.ICAO = airportData.Content.Icao;
            airport.Runway = airportData.Content.Runway;
            airport.Course = airportData.Content.Course;
            airport.Excess = airportData.Content.Excess;
            airport.Concrete = airportData.Content.Concrete;

            return airport;
        }
    }
}
