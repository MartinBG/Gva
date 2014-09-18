using System.Collections.Generic;
using Common.Data;
using Gva.Api.Models.Views.Organization;
using Gva.Api.ModelsDO.Organizations;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Organization
{
    public class OrganizationProjection : Projection<GvaViewOrganization>
    {
        public OrganizationProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Organization")
        {
        }

        public override IEnumerable<GvaViewOrganization> Execute(PartCollection parts)
        {
            var organizationData = parts.Get<OrganizationDataDO>("organizationData");

            if (organizationData == null)
            {
                return new GvaViewOrganization[] { };
            }

            return new[] { this.Create(organizationData) };
        }

        private GvaViewOrganization Create(PartVersion<OrganizationDataDO> organizationData)
        {
            GvaViewOrganization organization = new GvaViewOrganization();

            organization.LotId = organizationData.Part.Lot.LotId;
            organization.Name = organizationData.Content.Name;
            organization.NameAlt = organizationData.Content.NameAlt;
            organization.Cao = organizationData.Content.Cao;
            organization.Valid = organizationData.Content.Valid.Code == "Y";
            organization.Uin = organizationData.Content.Uin;
            organization.OrganizationTypeId = organizationData.Content.OrganizationType.NomValueId;
            organization.DateValidTo = organizationData.Content.DateValidTo;
            organization.DateCaoValidTo = organizationData.Content.DateCaoValidTo;

            return organization;
        }
    }
}
