using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Gva.Api.Models.Views.Organization;
using Gva.Api.ModelsDO.Organizations;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Organization
{
    public class OrganizationRecommendationProjection : Projection<GvaViewOrganizationRecommendation>
    {
        public OrganizationRecommendationProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Organization")
        {
        }

        public override IEnumerable<GvaViewOrganizationRecommendation> Execute(PartCollection parts)
        {
            var recommendations = parts.GetAll<OrganizationRecommendationDO>("organizationRecommendation");

            return recommendations.Select(se => this.Create(se));
        }

        private GvaViewOrganizationRecommendation Create(PartVersion<OrganizationRecommendationDO> part)
        {
            GvaViewOrganizationRecommendation recommendation = new GvaViewOrganizationRecommendation();

            recommendation.LotId = part.Part.Lot.LotId;
            recommendation.PartIndex = part.Part.Index;
            recommendation.AuditPartName = part.Content.AuditPart == null ? null : part.Content.AuditPart.Name;
            recommendation.FormText = part.Content.FormText;
            recommendation.FormDate = part.Content.FormDate;

            return recommendation;
        }
    }
}
