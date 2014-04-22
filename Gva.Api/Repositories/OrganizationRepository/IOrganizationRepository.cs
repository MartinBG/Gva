using System.Collections.Generic;
using Gva.Api.Models;
using System;

namespace Gva.Api.Repositories.OrganizationRepository
{
    public interface IOrganizationRepository
    {
        IEnumerable<GvaViewOrganization> GetOrganizations(
            string name,
            int? caseTypeId,
            string CAO,
            string uin,
            DateTime? dateValidTo,
            DateTime? dateCAOValidTo,
            bool exact = false,
            int offset = 0,
            int? limit = null);

        GvaViewOrganization GetOrganization(int organizationId);
    }
}