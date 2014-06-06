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
            reg.CertRegisterId = part.Content.Get<int>("register.nomValueId");
            reg.CertNumber = part.Content.Get<int>("certNumber");
            reg.ActNumber = part.Content.Get<int>("actNumber");
            reg.RegMark = part.Content.Get<string>("regMark");
        }

        public override void Clear(GvaViewAircraftRegistration registration)
        {
            throw new NotSupportedException();
        }
    }
}
