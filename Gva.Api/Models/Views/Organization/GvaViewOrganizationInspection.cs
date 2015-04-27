using Regs.Api.LotEvents;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gva.Api.Models.Views.Organization
{
    public partial class GvaViewOrganizationInspection : IProjectionView
    {
        public int LotId { get; set; }

        public int PartIndex { get; set; }

        public virtual GvaViewOrganization Organization { get; set; }

        public virtual ICollection<GvaViewOrganizationInspectionRecommendation> InspectionsRecommendations { get; set; }

    }

    public class GvaViewOrganizationInspectionMap : EntityTypeConfiguration<GvaViewOrganizationInspection>
    {
        public GvaViewOrganizationInspectionMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.PartIndex });

            // Properties
            this.Property(t => t.PartIndex)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);


            // Table & Column Mappings
            this.ToTable("GvaViewOrganizationInspections");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartIndex).HasColumnName("PartIndex");

            this.HasRequired(t => t.Organization)
                .WithMany(t => t.Inspections)
                .HasForeignKey(t => t.LotId);
        }
    }
}
