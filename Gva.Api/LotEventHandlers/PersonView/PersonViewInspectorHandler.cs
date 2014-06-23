using System;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.PersonView
{
    public class PersonViewInspectorHandler : CommitEventHandler<GvaViewPersonInspector>
    {
        public PersonViewInspectorHandler(IUnitOfWork unitOfWork)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Person",
                setPartAlias: "inspectorData",
                viewMatcher: pv => v => v.LotId == pv.Part.Lot.LotId)
        {
        }

        public override void Fill(GvaViewPersonInspector inspector, PartVersion part)
        {
            inspector.LotId = part.Part.Lot.LotId;
            inspector.ExaminerCode = part.Content.Get<string>("examinerCode");
            inspector.CaaName = part.Content.Get<string>("caa.name");
            inspector.StampNum = part.Content.Get<string>("stampNum");
            inspector.Valid = part.Content.Get<string>("valid.code") == "Y";
        }

        public override void Clear(GvaViewPersonInspector licence)
        {
            throw new NotSupportedException();
        }
    }
}
