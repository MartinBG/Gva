using System;
using System.Collections.Generic;
using Gva.Api.Models.Views.Organization;

namespace Gva.Api.Repositories.OrganizationRepository
{
    public interface IOrganizationRepository
    {
        IEnumerable<GvaViewOrganization> GetOrganizations(
            string name,
            int? caseTypeId,
            string cao,
            string uin,
            DateTime? dateValidTo,
            DateTime? dateCaoValidTo,
            bool exact = false,
            int offset = 0,
            int? limit = null);

        GvaViewOrganization GetOrganization(int organizationId);

        IEnumerable<GvaViewOrganizationRecommendation> GetInspectionRecommendations(int lotId, int inspectionPartIndex);
    }
}