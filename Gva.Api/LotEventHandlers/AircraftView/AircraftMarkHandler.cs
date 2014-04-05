using System;
using Common.Data;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.AircraftView
{
    public class AircraftMarkHandler : CommitEventHandler<GvaViewAircraft>
    {
        public AircraftMarkHandler(IUnitOfWork unitOfWork)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Aircraft",
                setPartAlias: "aircraftMark",
                partMatcher: pv => pv.DynamicContent.mark != null,
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId,
                isPrincipalHandler: false)
        {
        }

        public override void Fill(GvaViewAircraft aircraft, PartVersion part)
        {
            aircraft.Mark = part.DynamicContent.mark;
        }

        public override void Clear(GvaViewAircraft aircraft)
        {
            throw new NotSupportedException();
        }
    }
}
