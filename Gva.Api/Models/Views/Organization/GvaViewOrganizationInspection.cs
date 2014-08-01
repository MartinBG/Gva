using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gva.Api.Models.Views.Person;

namespace Gva.Api.Models.Views.Organization
{
    public partial class GvaViewOrganizationInspection
    {
        public int LotId { get; set; }

        public int InspectionPartIndex { get; set; }

        public virtual GvaViewOrganization Organization { get; set; }

        public virtual ICollection<GvaViewOrganizationInspectionRecommendation> InspectionsRecommendations { get; set; }

    }

    public class GvaViewOrganizationInspectionMap : EntityTypeConfiguration<GvaViewOrganizationInspection>
    {
        public GvaViewOrganizationInspectionMap()
        {
            // Primary Key
            this.HasKey(t => t.InspectionPartIndex);

            // Properties
            this.Property(t => t.InspectionPartIndex)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);


            // Table & Column Mappings
            this.ToTable("GvaViewOrganizationInspections");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.InspectionPartIndex).HasColumnName("InspectionPartIndex");

            this.HasRequired(t => t.Organization)
            .WithMany(t => t.RInspections)
            .HasForeignKey(t => t.LotId);
        }
    }
}
