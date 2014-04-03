using System.Collections.Generic;
using Gva.Api.Models;
using System;

namespace Gva.Api.Repositories.OrganizationRepository
{
    public interface IOrganizationRepository
    {
        IEnumerable<GvaViewOrganization> GetOrganizations(
            string name,
            string CAO,
            string uin,
            DateTime? dateValidTo,
            DateTime? dateCAOValidTo,
            int offset,
            int? limit);

        GvaViewOrganization GetOrganization(int organizationId);

        void AddOrganization(GvaViewOrganization organization);
    }
}