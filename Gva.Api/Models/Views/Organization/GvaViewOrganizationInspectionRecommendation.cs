﻿using Regs.Api.LotEvents;
using System.Data.Entity.ModelConfiguration;

namespace Gva.Api.Models.Views.Organization
{
    public partial class GvaViewOrganizationInspectionRecommendation : IProjectionView
    {
        public int LotId { get; set; }

        public int InspectionPartIndex { get; set; }

        public int RecommendationPartIndex { get; set; }

        public virtual GvaViewOrganizationRecommendation Recommendation { get; set; }

        public virtual GvaViewOrganizationInspection Inspection { get; set; }
    }

    public class GvaViewOrganizationInspectionRecommendationMap : EntityTypeConfiguration<GvaViewOrganizationInspectionRecommendation>
    {
        public GvaViewOrganizationInspectionRecommendationMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.InspectionPartIndex, t.RecommendationPartIndex });

            // Table & Column Mappings
            this.ToTable("GvaViewOrganizationInspectionsRecommendations");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.RecommendationPartIndex).HasColumnName("RecommendationPartIndex");
            this.Property(t => t.InspectionPartIndex).HasColumnName("InspectionPartIndex");

            this.HasRequired(t => t.Recommendation)
                .WithMany(t => t.InspectionsRecommendations)
                .HasForeignKey(t => new { t.LotId, t.RecommendationPartIndex });

            this.HasRequired(t => t.Inspection)
                .WithMany(t => t.InspectionsRecommendations)
                .HasForeignKey(t => new { t.LotId, t.InspectionPartIndex });
        }
    }
}