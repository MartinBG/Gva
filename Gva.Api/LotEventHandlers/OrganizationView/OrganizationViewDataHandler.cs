using System;
using Common.Data;
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

            organization.Name = part.DynamicContent.name;
            organization.CAO = part.DynamicContent.CAO;
            organization.Valid = part.DynamicContent.valid.name;
            organization.Uin = part.DynamicContent.uin;
            organization.OrganizationType = part.DynamicContent.organizationType.name;
            organization.DateValidTo = Convert.ToDateTime(part.DynamicContent.dateValidTo);
            organization.DateCAOValidTo = Convert.ToDateTime(part.DynamicContent.dateCAOValidTo);

        }

        public override void Clear(GvaViewOrganization organization)
        {
            throw new NotSupportedException();
        }
    }
}
