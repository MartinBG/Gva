using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views.Person;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Models.Views.Organization;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.Models;

namespace Gva.Api.Projections.Person
{
    public class OrganizationApprovalProjection : Projection<GvaViewOrganizationApproval>
    {
        private IUnitOfWork unitOfWork;
        public OrganizationApprovalProjection(
            IUnitOfWork unitOfWork)
            : base(unitOfWork, "Organization")
        {
            this.unitOfWork = unitOfWork;
        }

        public override IEnumerable<GvaViewOrganizationApproval> Execute(PartCollection parts)
        {
            var approvals = parts.GetAll<OrganizationApprovalDO>("approvals");

            return approvals.Select(a => this.Create(a));
        }

        private GvaViewOrganizationApproval Create(PartVersion<OrganizationApprovalDO> organizationApproval)
        {
            GvaViewOrganizationApproval approval = new GvaViewOrganizationApproval();

            approval.LotId = organizationApproval.Part.Lot.LotId;
            approval.PartIndex = organizationApproval.Part.Index;
            approval.DocumentNumber = organizationApproval.Content.DocumentNumber;
            approval.PartId = organizationApproval.Part.PartId;
            approval.ApprovalTypeId = organizationApproval.Content.ApprovalType != null ? organizationApproval.Content.ApprovalType.NomValueId : (int?)null;
            approval.ApprovalStateId = organizationApproval.Content.ApprovalState != null ? organizationApproval.Content.ApprovalState.NomValueId : (int?)null;

            return approval;
        }
    }
}
