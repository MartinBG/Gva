using System;
using System.Linq;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Gva.Api.Repositories.AircraftRepository;
using System.Collections.Generic;

namespace Gva.Api.LotEventHandlers.AircraftView
{
    public class AircraftRegMarkProjection : Projection<GvaViewAircraftRegMark>
    {
        public AircraftRegMarkProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Aircraft")
        {
        }

        public override IEnumerable<GvaViewAircraftRegMark> Execute(PartCollection parts)
        {
            var registrations = parts.GetAll("aircraftCertRegistrationsFM");

            return registrations.Select(reg => Create(reg));
        }

        private GvaViewAircraftRegMark Create(PartVersion registration)
        {
            GvaViewAircraftRegMark reg = new GvaViewAircraftRegMark();

            // aircraftRegistrationFM
            reg.LotId = registration.Part.Lot.LotId;
            reg.PartIndex = registration.Part.Index;
            reg.RegMark = registration.Content.Get<string>("regMark");

            return reg;
        }
    }
}
