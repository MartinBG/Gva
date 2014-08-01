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
    public class OrganizationRecommendationProjection : Projection<GvaViewOrganizationRecommendation>
    {
        public OrganizationRecommendationProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Organization")
        {
        }

        public override IEnumerable<GvaViewOrganizationRecommendation> Execute(PartCollection parts)
        {
            var recommendations = parts.GetAll("organizationRecommendation");

            return recommendations.Select(se => this.Create(se));
        }

        private GvaViewOrganizationRecommendation Create(PartVersion part)
        {
            GvaViewOrganizationRecommendation recommendation = new GvaViewOrganizationRecommendation();

            recommendation.LotId = part.Part.Lot.LotId;
            recommendation.PartIndex = part.Part.Index;
            recommendation.RecommendationPartName = part.Content.Get<string>("recommendationPart.name");
            recommendation.FormText = part.Content.Get<string>("formText");
            recommendation.FormDate = part.Content.Get<DateTime?>("formDate");

            return recommendation;
        }
    }
}
