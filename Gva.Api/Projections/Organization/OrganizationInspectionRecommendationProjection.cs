using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views.Organization;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Organization
{
    public class OrganizationInspectionRecommendationProjection : Projection<GvaViewOrganizationInspectionRecommendation>
    {
        public OrganizationInspectionRecommendationProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Organization")
        {
        }

        public override IEnumerable<GvaViewOrganizationInspectionRecommendation> Execute(PartCollection parts)
        {
            var recommendations = parts.GetAll("organizationRecommendation");
            List<GvaViewOrganizationInspectionRecommendation> inspectionsToRecommendation = new List<GvaViewOrganizationInspectionRecommendation>();
            foreach (var recommendation in recommendations)
            {
                foreach (int inspectionPartIndex in recommendation.Content.GetItems<int>("inspections"))
                {
                    inspectionsToRecommendation.Add(
                        new GvaViewOrganizationInspectionRecommendation()
                        {
                            LotId = recommendation.Part.Lot.LotId,
                            RecommendationPartIndex = recommendation.Part.Index,
                            InspectionPartIndex = inspectionPartIndex
                        });
                }
            }
            return inspectionsToRecommendation;
        }
    }
}
