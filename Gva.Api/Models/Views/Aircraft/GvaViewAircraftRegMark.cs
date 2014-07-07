using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;

namespace Gva.Api.Models
{
    public partial class GvaViewAircraftRegMark
    {
        public int LotId { get; set; }

        public int PartIndex { get; set; }

        public string RegMark { get; set; }

        public virtual GvaViewAircraft Aircraft { get; set; }
    }

    public class GvaViewAircraftRegMarkMap : EntityTypeConfiguration<GvaViewAircraftRegMark>
    {
        public GvaViewAircraftRegMarkMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.PartIndex });

            // Table & Column Mappings
            this.ToTable("GvaViewAircraftRegMarks");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartIndex).HasColumnName("PartIndex");
            this.Property(t => t.RegMark).HasColumnName("RegMark");

            // Relationships
            this.HasRequired(t => t.Aircraft)
                .WithMany()
                .HasForeignKey(t => t.LotId);
        }
    }
}
