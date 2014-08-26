using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gva.Api.Models.Views.Person;

namespace Gva.Api.Models.Views.Organization
{
    public partial class GvaViewOrganizationRecommendation
    {
        public int LotId { get; set; }

        public int PartIndex { get; set; }

        public string AuditPartName { get; set; }

        public string FormText { get; set; }

        public DateTime? FormDate { get; set; }

        public virtual GvaViewOrganization Organization { get; set; }

        public virtual ICollection<GvaViewOrganizationInspectionRecommendation> InspectionsRecommendations { get; set; }
    }

    public class GvaViewOrganizationRecommendationMap : EntityTypeConfiguration<GvaViewOrganizationRecommendation>
    {
        public GvaViewOrganizationRecommendationMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.PartIndex });

            // Properties
            this.Property(t => t.PartIndex)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.AuditPartName)
                .HasMaxLength(150);
            this.Property(t => t.FormText)
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("GvaViewOrganizationRecommendations");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartIndex).HasColumnName("PartIndex");
            this.Property(t => t.AuditPartName).HasColumnName("AuditPartName");
            this.Property(t => t.FormText).HasColumnName("FormText");
            this.Property(t => t.FormDate).HasColumnName("FormDate");

            // Relationships
            this.HasRequired(t => t.Organization)
                .WithMany(t => t.Recommendations)
                .HasForeignKey(t => t.LotId);

        }
    }
}
