using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Gva.Api.Models.Views.Aircarft;
using Gva.Api.Models.Views.Aircraft;
using Gva.Api.ModelsDO.Aircrafts;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Aircraft
{
    public class AircraftPrintedCertNoiseProjection : Projection<GvaViewAircraftPrintedCertNoise>
    {
        public AircraftPrintedCertNoiseProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Aircraft")
        {
        }

        public override IEnumerable<GvaViewAircraftPrintedCertNoise> Execute(PartCollection parts)
        {
            var noises = parts.GetAll<AircraftCertNoiseDO>("aircraftCertNoises").Where(c => c.Content.PrintedFileId.HasValue);

            return noises.Select(noise => this.Create(noise));
        }

        private GvaViewAircraftPrintedCertNoise Create(PartVersion<AircraftCertNoiseDO> noisePart)
        {
            GvaViewAircraftPrintedCertNoise noise = new GvaViewAircraftPrintedCertNoise();

            noise.LotId = noisePart.Part.Lot.LotId;
            noise.NoisePartIndex = noisePart.Part.Index;
            noise.PrintedFileId = noisePart.Content.PrintedFileId.Value;

            return noise;
        }
    }
}
