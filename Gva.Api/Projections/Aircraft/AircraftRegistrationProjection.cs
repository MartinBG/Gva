using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views.Aircraft;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Aircraft
{
    public class AircraftRegistrationProjection : Projection<GvaViewAircraftRegistration>
    {
        public AircraftRegistrationProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Aircraft")
        {
        }

        public override IEnumerable<GvaViewAircraftRegistration> Execute(PartCollection parts)
        {
            var registrations = parts.GetAll("aircraftCertRegistrationsFM");

            return registrations.Select(reg => this.Create(reg));
        }

        private GvaViewAircraftRegistration Create(PartVersion registration)
        {
            GvaViewAircraftRegistration reg = new GvaViewAircraftRegistration();

            // aircraftRegistrationFM
            reg.LotId = registration.Part.Lot.LotId;
            reg.PartIndex = registration.Part.Index;
            reg.CertRegisterId = registration.Content.Get<int>("register.nomValueId");
            reg.CertNumber = registration.Content.Get<int>("certNumber");
            reg.ActNumber = registration.Content.Get<int>("actNumber");
            reg.RegMark = registration.Content.Get<string>("regMark");

            return reg;
        }
    }
}
