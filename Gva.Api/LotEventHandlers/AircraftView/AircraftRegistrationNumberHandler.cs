using System;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.AircraftView
{
    public class AircraftRegistrationNumberHandler : CommitEventHandler<GvaViewAircraft>
    {
        public AircraftRegistrationNumberHandler(IUnitOfWork unitOfWork)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Aircraft",
                setPartAlias: "aircraftRegistrationFM",
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId,
                isPrincipal: false)
        {
        }

        public override void Fill(GvaViewAircraft aircraft, PartVersion part)
        {
            aircraft.Mark = part.Content.Get<string>("certNumber");
        }

        public override void Clear(GvaViewAircraft aircraft)
        {
            aircraft.Mark = null;
        }
    }
}
