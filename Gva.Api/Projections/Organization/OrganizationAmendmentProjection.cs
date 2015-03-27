using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Common.Api.Repositories.NomRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Models.Views.Organization;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Models;
using Gva.Api.ModelsDO;

namespace Gva.Api.Projections.Organization
{
    public class OrganizationAmendmentProjection : Projection<GvaViewOrganizationAmendment>
    {
        public OrganizationAmendmentProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Organization")
        {
        }

        public override IEnumerable<GvaViewOrganizationAmendment> Execute(PartCollection parts)
        {
            var amendments = parts.GetAll<OrganizationAmendmentDO>("approvalAmendments");

            return amendments.Select(a => this.Create(a));
        }

        private GvaViewOrganizationAmendment Create(PartVersion<OrganizationAmendmentDO> organizationAmendment)
        {
            GvaViewOrganizationAmendment amendment = new GvaViewOrganizationAmendment();
            amendment.LotId = organizationAmendment.Part.Lot.LotId;
            amendment.PartIndex = organizationAmendment.Part.Index;
            amendment.PartId = organizationAmendment.Part.PartId;
            amendment.DocumentNumber = organizationAmendment.Content.DocumentNumber;
            amendment.ChangeNum = organizationAmendment.Content.ChangeNum;
            amendment.DocumentDateIssue = organizationAmendment.Content.DocumentDateIssue;
            amendment.ApprovalPartIndex = organizationAmendment.Content.ApprovalPartIndex.Value;
            amendment.Index = organizationAmendment.Content.Index;

            return amendment;
        }
    }
}
