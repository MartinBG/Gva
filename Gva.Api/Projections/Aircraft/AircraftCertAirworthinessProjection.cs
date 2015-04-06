using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Gva.Api.Models.Views.Aircraft;
using Gva.Api.ModelsDO.Aircrafts;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Aircraft
{
    public class AircraftCertAirworthinessProjection : Projection<GvaViewAircraftCertAirworthiness>
    {
        public AircraftCertAirworthinessProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Aircraft")
        {
        }

        public override IEnumerable<GvaViewAircraftCertAirworthiness> Execute(PartCollection parts)
        {
            var airworthinesses = parts.GetAll<GvaViewAircraftCertAirworthiness>("aircraftCertAirworthinessesFM");

            return airworthinesses.Select(cert => this.Create(cert));
        }

        private GvaViewAircraftCertAirworthiness Create(PartVersion<GvaViewAircraftCertAirworthiness> airworthiness)
        {
            GvaViewAircraftCertAirworthiness cert = new GvaViewAircraftCertAirworthiness();

            cert.LotId = airworthiness.Part.Lot.LotId;
            cert.PartIndex = airworthiness.Part.Index;
            cert.CertificateTypeId = airworthiness.Content.AirworthinessCertificateType.NomValueId;
            cert.PrintedFileId = airworthiness.Content.PrintedFileId;

            return cert;
        }
    }
}
