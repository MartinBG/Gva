using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocUnit
    {
        public int DocUnitId { get; set; }

        public int DocId { get; set; }

        public int UnitId { get; set; }

        public int DocUnitRoleId { get; set; }

        public byte[] Version { get; set; }

        public virtual Doc Doc { get; set; }

        public virtual DocUnitRole DocUnitRole { get; set; }

        public virtual Unit Unit { get; set; }
    }

    public class DocUnitMap : EntityTypeConfiguration<DocUnit>
    {
        public DocUnitMap()
        {
            // Primary Key
            this.HasKey(t => t.DocUnitId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DocUnits");
            this.Property(t => t.DocUnitId).HasColumnName("DocUnitId");
            this.Property(t => t.DocId).HasColumnName("DocId");
            this.Property(t => t.UnitId).HasColumnName("UnitId");
            this.Property(t => t.DocUnitRoleId).HasColumnName("DocUnitRoleId");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Doc)
                .WithMany(t => t.DocUnits)
                .HasForeignKey(d => d.DocId);
            this.HasRequired(t => t.DocUnitRole)
                .WithMany(t => t.DocUnits)
                .HasForeignKey(d => d.DocUnitRoleId);
            this.HasRequired(t => t.Unit)
                .WithMany(t => t.DocUnits)
                .HasForeignKey(d => d.UnitId);
        }
    }
}
