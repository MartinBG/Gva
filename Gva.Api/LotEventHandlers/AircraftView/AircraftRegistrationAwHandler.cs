using System;
using System.Linq;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.AircraftView
{
    public class AircraftRegistrationAwHandler : CommitEventHandler<GvaViewAircraftAw>
    {
        public AircraftRegistrationAwHandler(IUnitOfWork unitOfWork)
            : base(
                unitOfWork: unitOfWork,
                setPartAlias: "aircraftAirworthinessFM",
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId && v.LotPartId == pv.Part.PartId)
        {
        }

        public override void Fill(GvaViewAircraftAw aw, PartVersion part)
        {
            aw.Part = part.Part;
            aw.Lot = part.Part.Lot;
            aw.RegId = part.Content.Get<int>("registration.nomValueId");
            aw.IssueDate = part.Content.Get<DateTime>("issueDate");
            aw.ValidFromDate = part.Content.Get<DateTime>("validFromDate");
            aw.ValidToDate = part.Content.Get<DateTime>("validToDate");
            aw.Inspector = part.Content.Get<string>("inspector.name");
            aw.EASA15IssueDate = part.Content.Get<DateTime?>("EASA15IssueDate");
            aw.EASA15IssueValidToDate = part.Content.Get<DateTime?>("EASA15IssueValidToDate");
        }

        public override void Clear(GvaViewAircraftAw aw)
        {
            throw new NotSupportedException();
        }
    }
}
