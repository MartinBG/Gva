using System;
using Common.Data;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.AircraftView
{
    public class AircraftViewDataHandler : CommitEventHandler<GvaViewAircraft>
    {
        public AircraftViewDataHandler(IUnitOfWork unitOfWork)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Aircraft",
                setPartAlias: "aircraftData",
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId)
        {
        }

        public override void Fill(GvaViewAircraft aircraft, PartVersion part)
        {
            aircraft.Lot = part.Part.Lot;

            aircraft.ManSN = part.DynamicContent.manSN;
            aircraft.Model = part.DynamicContent.model;
            aircraft.ModelAlt = part.DynamicContent.modelAlt;
            aircraft.OutputDate = part.DynamicContent.outputDate;
            aircraft.ICAO = part.DynamicContent.icao;
            aircraft.AircraftCategory = part.DynamicContent.aircraftCategory.name;
            aircraft.AircraftProducer = part.DynamicContent.aircraftProducer.name;
            aircraft.Engine = part.DynamicContent.engine;
            aircraft.Propeller = part.DynamicContent.propeller;
            aircraft.ModifOrWingColor = part.DynamicContent.ModifOrWingColor;
        }

        public override void Clear(GvaViewAircraft aircraft)
        {
            throw new NotSupportedException();
        }
    }
}
