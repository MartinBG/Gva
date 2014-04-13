using System;
using System.Linq;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.AircraftView
{
    public class AircraftRegistrationNewAwHandler : CommitEventHandler<GvaViewAircraftRegistration>
    {
        public AircraftRegistrationNewAwHandler(IUnitOfWork unitOfWork)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Aircraft",
                setPartAlias: "aircraftAirworthinessFM",
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId && v.LotPartId == pv.Content.Get<int>("registration.nomValueId"),
                isPrincipal: false)
        {
        }

        public override void Fill(GvaViewAircraftRegistration reg, PartVersion part)
        {
            reg.CertAirworthinessId = part.PartId;
        }

        public override void Clear(GvaViewAircraftRegistration reg)
        {
            throw new NotSupportedException();
        }
    }
}
