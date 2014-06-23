using System;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.ApplicationView
{
    public class ApplicationsViewPersonHandler : CommitEventHandler<GvaViewApplication>
    {
        public ApplicationsViewPersonHandler(IUnitOfWork unitOfWork)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Person",
                setPartAlias: "personApplication",
                viewMatcher: pv =>
                    v => v.PartId == pv.Part.PartId)
        {
        }

        public override void Fill(GvaViewApplication application, PartVersion partVersion)
        {
            application.LotId = partVersion.Part.Lot.LotId;
            application.Part = partVersion.Part;
            application.RequestDate = partVersion.Content.Get<DateTime?>("requestDate");
            application.DocumentNumber = partVersion.Content.Get<string>("documentNumber");
            application.ApplicationTypeName = partVersion.Content.Get<string>("applicationType.name");
            application.StatusName = partVersion.Content.Get<string>("applicationStatus.name");
        }

        public override void Clear(GvaViewApplication application)
        {
            throw new NotSupportedException();
        }
    }
}
