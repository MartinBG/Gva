using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Gva.Api.Models.Views.Aircraft;
using Gva.Api.ModelsDO.Aircrafts;
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
            var registrations = parts.GetAll<AircraftCertRegistrationFMDO>("aircraftCertRegistrationsFM");

            return registrations.Select(reg => this.Create(reg));
        }

        private GvaViewAircraftRegistration Create(PartVersion<AircraftCertRegistrationFMDO> registration)
        {
            GvaViewAircraftRegistration reg = new GvaViewAircraftRegistration();

            reg.LotId = registration.Part.Lot.LotId;
            reg.PartIndex = registration.Part.Index;
            reg.CertRegisterId = registration.Content.Register.NomValueId;
            reg.CertNumber = registration.Content.CertNumber;
            reg.ActNumber = registration.Content.ActNumber;
            reg.RegMark = registration.Content.RegMark;
            reg.PrintedExportCertFileId = registration.Content.Removal != null && registration.Content.Removal.Export != null ? registration.Content.Removal.Export.PrintedExportCertFileId : (int?)null;

            return reg;
        }
    }
}
