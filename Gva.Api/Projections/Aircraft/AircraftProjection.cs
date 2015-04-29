using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Gva.Api.Models.Views.Aircraft;
using Gva.Api.ModelsDO.Aircrafts;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Aircraft
{
    public class AircraftProjection : Projection<GvaViewAircraft>
    {
        public AircraftProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Aircraft")
        {
        }

        public override IEnumerable<GvaViewAircraft> Execute(PartCollection parts)
        {
            var aircraftData = parts.Get<AircraftDataDO>("aircraftData");

            if (aircraftData == null)
            {
                return new GvaViewAircraft[] { };
            }

            var lastRegistration = parts.GetAll<AircraftCertRegistrationFMDO>("aircraftCertRegistrationsFM")
                .OrderByDescending(r => r.CreateDate)
                .FirstOrDefault();

            return new[] { this.Create(aircraftData, lastRegistration) };
        }

        private GvaViewAircraft Create(PartVersion<AircraftDataDO> aircraftData, PartVersion<AircraftCertRegistrationFMDO> lastRegistration)
        {
            GvaViewAircraft aircraft = new GvaViewAircraft();

            // aircraftData
            aircraft.LotId = aircraftData.Part.Lot.LotId;
            aircraft.ManSN = aircraftData.Content.ManSN;
            aircraft.Model = aircraftData.Content.Model;
            aircraft.ModelAlt = aircraftData.Content.ModelAlt;
            aircraft.OutputDate = aircraftData.Content.OutputDate;
            aircraft.ICAO = aircraftData.Content.ICAO;
            aircraft.AirCategoryId = aircraftData.Content.AirCategory == null ? (int?)null : aircraftData.Content.AirCategory.NomValueId;
            aircraft.AircraftProducerId = aircraftData.Content.AircraftProducer == null ? (int?)null : aircraftData.Content.AircraftProducer.NomValueId;
            aircraft.Engine = aircraftData.Content.Engine;
            aircraft.Propeller = aircraftData.Content.Propeller;
            aircraft.ModifOrWingColor = aircraftData.Content.ModifOrWingColor;
            aircraft.EngineAlt = aircraftData.Content.EngineAlt;
            aircraft.PropellerAlt = aircraftData.Content.PropellerAlt;
            aircraft.ModifOrWingColorAlt = aircraftData.Content.ModifOrWingColorAlt;

            // lastRegistration
            if (lastRegistration != null)
            {
                aircraft.ActNumber = lastRegistration.Content.ActNumber;
            }

            return aircraft;
        }
    }
}
