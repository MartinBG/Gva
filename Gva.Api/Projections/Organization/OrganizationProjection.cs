using System;
using System.Collections.Generic;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views.Organization;
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
            var organizationData = parts.Get("organizationData");

            if (organizationData == null)
            {
                return new GvaViewOrganization[] { };
            }

            return new[] { this.Create(organizationData) };
        }

        private GvaViewOrganization Create(PartVersion organizationData)
        {
            GvaViewOrganization organization = new GvaViewOrganization();

            organization.LotId = organizationData.Part.Lot.LotId;
            organization.Name = organizationData.Content.Get<string>("name");
            organization.NameAlt = organizationData.Content.Get<string>("nameAlt");
            organization.CAO = organizationData.Content.Get<string>("CAO");
            organization.Valid = organizationData.Content.Get<string>("valid.code") == "Y";
            organization.Uin = organizationData.Content.Get<string>("uin");
            organization.OrganizationTypeId = organizationData.Content.Get<int>("organizationType.nomValueId");
            organization.DateValidTo = organizationData.Content.Get<DateTime?>("dateValidTo");
            organization.DateCAOValidTo = organizationData.Content.Get<DateTime?>("dateCAOValidTo");

            return organization;
        }
    }
}
