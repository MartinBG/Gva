using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views.Organization;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Organization
{
    public class OrganizationInspectionProjection : Projection<GvaViewOrganizationInspection>
    {
        public OrganizationInspectionProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Organization")
        {
        }

        public override IEnumerable<GvaViewOrganizationInspection> Execute(PartCollection parts)
        {
            var inspections = parts.GetAll("organizationInspection");

            return inspections.Select(se => this.Create(se));
        }

        private GvaViewOrganizationInspection Create(PartVersion part)
        {
            GvaViewOrganizationInspection inspection = new GvaViewOrganizationInspection();

            inspection.LotId = part.Part.Lot.LotId;
            inspection.PartIndex = part.Part.Index;

            return inspection;
        }
    }
}
