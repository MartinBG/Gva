using System;
using Common.Data;
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

            airport.Name = part.DynamicContent.name;
            airport.NameAlt = part.DynamicContent.nameAlt;
            airport.Place = part.DynamicContent.place;
            airport.AirportType = part.DynamicContent.airportType.name;
            airport.ICAO = part.DynamicContent.icao;
            airport.Runway = part.DynamicContent.runway;
            airport.Course = part.DynamicContent.course;
            airport.Excess = part.DynamicContent.excess;
            airport.Concrete = part.DynamicContent.concrete;
        }

        public override void Clear(GvaViewAirport airport)
        {
            throw new NotSupportedException();
        }
    }
}
