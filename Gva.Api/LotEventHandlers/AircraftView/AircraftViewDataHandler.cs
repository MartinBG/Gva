using System;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.AircraftView
{
    public class AircraftViewDataHandler : CommitEventHandler<GvaViewAircraft>
    {
        public AircraftViewDataHandler(IUnitOfWork unitOfWork)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Aircraft",
                setPartAlias: "aircraftData",
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId)
        {
        }

        public override void Fill(GvaViewAircraft aircraft, PartVersion part)
        {
            aircraft.Lot = part.Part.Lot;

            aircraft.ManSN = part.Content.Get<string>("manSN");
            aircraft.Model = part.Content.Get<string>("model");
            aircraft.ModelAlt = part.Content.Get<string>("modelAlt");
            aircraft.OutputDate = part.Content.Get<DateTime?>("outputDate");
            aircraft.ICAO = part.Content.Get<string>("icao");
            aircraft.AircraftCategory = part.Content.Get<string>("aircraftCategory.name");
            aircraft.AircraftProducer = part.Content.Get<string>("aircraftProducer.name");
            aircraft.Engine = part.Content.Get<string>("engine");
            aircraft.Propeller = part.Content.Get<string>("propeller");
            aircraft.ModifOrWingColor = part.Content.Get<string>("ModifOrWingColor");
        }

        public override void Clear(GvaViewAircraft aircraft)
        {
            throw new NotSupportedException();
        }
    }
}
