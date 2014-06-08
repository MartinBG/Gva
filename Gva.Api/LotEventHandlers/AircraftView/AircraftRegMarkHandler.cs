using System;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Gva.Api.Repositories.AircraftRepository;

namespace Gva.Api.LotEventHandlers.AircraftView
{
    public class AircraftRegMarkHandler : CommitEventHandler<GvaViewAircraftRegMark>
    {
        public AircraftRegMarkHandler(IUnitOfWork unitOfWork)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Aircraft",
                setPartAlias: "aircraftRegistrationFM",
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId && v.LotPartId == pv.Part.PartId)
        {
        }

        public override void Fill(GvaViewAircraftRegMark reg, PartVersion part)
        {
            reg.Lot = part.Part.Lot;
            reg.Part = part.Part;
            reg.RegMark = part.Content.Get<string>("regMark");
        }

        public override void Clear(GvaViewAircraftRegMark registration)
        {
            throw new NotSupportedException();
        }
    }
}
