using System;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.AircraftView
{
    public class AircraftRegistrationHandler : CommitEventHandler<GvaViewAircraftRegistration>
    {
        public AircraftRegistrationHandler(IUnitOfWork unitOfWork)
            : base(
                unitOfWork: unitOfWork,
                setPartAlias: "aircraftRegistrationFM",
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId && v.LotPartId == pv.Part.PartId)
        {
        }

        public override void Fill(GvaViewAircraftRegistration reg, PartVersion part)
        {
            reg.Lot = part.Part.Lot;
            reg.Part = part.Part;
            reg.CertNumber = part.Content.Get<string>("certNumber");
        }

        public override void Clear(GvaViewAircraftRegistration registration)
        {
            throw new NotSupportedException();
        }
    }
}
