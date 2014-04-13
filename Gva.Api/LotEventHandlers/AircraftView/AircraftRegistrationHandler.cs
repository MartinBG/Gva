using System;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Gva.Api.Repositories.AircraftRepository;

namespace Gva.Api.LotEventHandlers.AircraftView
{
    public class AircraftRegistrationHandler : CommitEventHandler<GvaViewAircraftRegistration>
    {
        private IAircraftRegistrationAwRepository aircraftRegistrationAwRepository;

        public AircraftRegistrationHandler(IUnitOfWork unitOfWork,
            IAircraftRegistrationAwRepository aircraftRegistrationAwRepository)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Aircraft",
                setPartAlias: "aircraftRegistrationFM",
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId && v.LotPartId == pv.Part.PartId)
        {
            this.aircraftRegistrationAwRepository = aircraftRegistrationAwRepository;
        }

        public override void Fill(GvaViewAircraftRegistration reg, PartVersion part)
        {
            reg.Lot = part.Part.Lot;
            reg.Part = part.Part;
            reg.CertNumber = part.Content.Get<string>("certNumber");
            var aw = this.aircraftRegistrationAwRepository.GetLastAw(part.Part.Lot.LotId);
            if (aw != null)
            {
                reg.CertAirworthinessId = aw.LotPartId;
            }
        }

        public override void Clear(GvaViewAircraftRegistration registration)
        {
            throw new NotSupportedException();
        }
    }
}
