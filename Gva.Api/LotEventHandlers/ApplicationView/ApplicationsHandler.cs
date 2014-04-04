using System;
using Common.Data;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.ApplicationView
{
    public class ApplicationsHandler : CommitEventHandler<GvaViewApplication>
    {
        public ApplicationsHandler(IUnitOfWork unitOfWork)
            : base(
                unitOfWork: unitOfWork,
                setPartAlias: "application",
                viewMatcher: pv =>
                    v => v.PartId == pv.Part.PartId)
        {
        }

        public override void Fill(GvaViewApplication application, PartVersion partVersion)
        {
            application.Part = partVersion.Part;
            application.RequestDate = partVersion.DynamicContent.requestDate;
            application.DocumentNumber = partVersion.DynamicContent.documentNumber;
            application.ApplicationTypeName = partVersion.DynamicContent.applicationType.name;
            application.StatusName = partVersion.DynamicContent.applicationStatus == null ? null : partVersion.DynamicContent.applicationStatus.name;
        }

        public override void Clear(GvaViewApplication application)
        {
            throw new NotSupportedException();
        }
    }
}
