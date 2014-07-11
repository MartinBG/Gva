using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views.Aircraft;
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
            var aircraftData = parts.Get("aircraftData");

            if (aircraftData == null)
            {
                return new GvaViewAircraft[] { };
            }

            var activeRegistration = parts.GetAll("aircraftCertRegistrationsFM")
                .Where(pv => pv.Content.Get<bool>("isActive"))
                .SingleOrDefault();

            return new[] { this.Create(aircraftData, activeRegistration) };
        }

        private GvaViewAircraft Create(PartVersion aircraftData, PartVersion activeRegistration)
        {
            GvaViewAircraft aircraft = new GvaViewAircraft();

            // aircraftData
            aircraft.LotId = aircraftData.Part.Lot.LotId;
            aircraft.ManSN = aircraftData.Content.Get<string>("manSN");
            aircraft.Model = aircraftData.Content.Get<string>("model");
            aircraft.ModelAlt = aircraftData.Content.Get<string>("modelAlt");
            aircraft.OutputDate = aircraftData.Content.Get<DateTime?>("outputDate");
            aircraft.ICAO = aircraftData.Content.Get<string>("icao");
            aircraft.AirCategoryId = aircraftData.Content.Get<int?>("airCategory.nomValueId");
            aircraft.AircraftProducerId = aircraftData.Content.Get<int?>("aircraftProducer.nomValueId");
            aircraft.Engine = aircraftData.Content.Get<string>("engine");
            aircraft.Propeller = aircraftData.Content.Get<string>("propeller");
            aircraft.ModifOrWingColor = aircraftData.Content.Get<string>("ModifOrWingColor");

            // currentRegistration
            if (activeRegistration != null)
            {
                aircraft.Mark = activeRegistration.Content.Get<string>("regMark");
            }

            return aircraft;
        }
    }
}
