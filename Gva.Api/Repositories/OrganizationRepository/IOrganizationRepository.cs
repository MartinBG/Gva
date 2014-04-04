using System.Collections.Generic;
using Gva.Api.Models;
using System;

namespace Gva.Api.Repositories.OrganizationRepository
{
    public interface IOrganizationRepository
    {
        IEnumerable<GvaViewOrganizationData> GetOrganizations(
            string name,
            string CAO,
            string uin,
            DateTime? dateValidTo,
            DateTime? dateCAOValidTo,
            int offset,
            int? limit);

        GvaViewOrganizationData GetOrganization(int organizationId);

        void AddOrganization(GvaViewOrganizationData organization);
    }
}