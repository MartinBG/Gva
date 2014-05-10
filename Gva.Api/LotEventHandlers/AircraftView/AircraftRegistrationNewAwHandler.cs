using System;
using System.Linq;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Gva.Api.Repositories.AircraftRepository;

namespace Gva.Api.LotEventHandlers.AircraftView
{
    public class AircraftRegistrationNewAwHandler : CommitEventHandler<GvaViewAircraftRegistration>
    {
        private IAircraftRegistrationAwRepository aircraftRegistrationAwRepository;

        public AircraftRegistrationNewAwHandler(IUnitOfWork unitOfWork,
            IAircraftRegistrationAwRepository aircraftRegistrationAwRepository)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Aircraft",
                setPartAlias: "aircraftAirworthinessFM",
                viewMatcher: pv =>
                {
                    var registrationPartIndex = pv.Content.Get<int>("registration.nomValueId");
                    return v => v.LotId == pv.Part.Lot.LotId && v.Part.Index == registrationPartIndex;
                },
                isPrincipal: false)
        {
            this.aircraftRegistrationAwRepository = aircraftRegistrationAwRepository;
        }

        public override void Fill(GvaViewAircraftRegistration reg, PartVersion part)
        {
            reg.CertAirworthinessId = part.Part.Index;
        }

        public override void Clear(GvaViewAircraftRegistration reg)
        {
            var aw = this.aircraftRegistrationAwRepository.GetLastAw(reg.Lot.LotId);

            reg.CertAirworthinessId = aw.LotPartId;
        }
    }
}
