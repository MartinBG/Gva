using System.Collections.Generic;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views.Airport;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.Airport
{
    public class AirportProjection : Projection<GvaViewAirport>
    {
        public AirportProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Airport")
        {
        }

        public override IEnumerable<GvaViewAirport> Execute(PartCollection parts)
        {
            var airportData = parts.Get("airportData");

            if (airportData == null)
            {
                return new GvaViewAirport[] { };
            }

            return new[] { this.Create(airportData) };
        }

        private GvaViewAirport Create(PartVersion airportData)
        {
            GvaViewAirport airport = new GvaViewAirport();

            airport.LotId = airportData.Part.Lot.LotId;
            airport.Name = airportData.Content.Get<string>("name");
            airport.NameAlt = airportData.Content.Get<string>("nameAlt");
            airport.Place = airportData.Content.Get<string>("place");
            airport.AirportTypeId = airportData.Content.Get<int>("airportType.nomValueId");
            airport.ICAO = airportData.Content.Get<string>("icao");
            airport.Runway = airportData.Content.Get<string>("runway");
            airport.Course = airportData.Content.Get<string>("course");
            airport.Excess = airportData.Content.Get<string>("excess");
            airport.Concrete = airportData.Content.Get<string>("concrete");

            return airport;
        }
    }
}
