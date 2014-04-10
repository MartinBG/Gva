using System;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.OrganizationView
{
    public class OrganizationViewDataHandler : CommitEventHandler<GvaViewOrganization>
    {
        public OrganizationViewDataHandler(IUnitOfWork unitOfWork)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Organization",
                setPartAlias: "organizationData",
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId)
        {
        }

        public override void Fill(GvaViewOrganization organization, PartVersion part)
        {
            organization.Lot = part.Part.Lot;

            organization.Name = part.Content.Get<string>("name");
            organization.CAO = part.Content.Get<string>("CAO");
            organization.Valid = part.Content.Get<string>("valid.name");
            organization.Uin = part.Content.Get<string>("uin");
            organization.OrganizationType = part.Content.Get<string>("organizationType.name");
            organization.DateValidTo = part.Content.Get<DateTime?>("dateValidTo");
            organization.DateCAOValidTo = part.Content.Get<DateTime?>("dateCAOValidTo");
        }

        public override void Clear(GvaViewOrganization organization)
        {
            throw new NotSupportedException();
        }
    }
}
