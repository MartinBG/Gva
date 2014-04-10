using System;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.AirportView
{
    public class AirportDataHandler : CommitEventHandler<GvaViewAirport>
    {
        public AirportDataHandler(IUnitOfWork unitOfWork)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Airport",
                setPartAlias: "airportData",
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId)
        {
        }

        public override void Fill(GvaViewAirport airport, PartVersion part)
        {
            airport.Lot = part.Part.Lot;

            airport.Name = part.Content.Get<string>("name");
            airport.NameAlt = part.Content.Get<string>("nameAlt");
            airport.Place = part.Content.Get<string>("place");
            airport.AirportType = part.Content.Get<string>("airportType.name");
            airport.ICAO = part.Content.Get<string>("icao");
            airport.Runway = part.Content.Get<string>("runway");
            airport.Course = part.Content.Get<string>("course");
            airport.Excess = part.Content.Get<string>("excess");
            airport.Concrete = part.Content.Get<string>("concrete");
        }

        public override void Clear(GvaViewAirport airport)
        {
            throw new NotSupportedException();
        }
    }
}
