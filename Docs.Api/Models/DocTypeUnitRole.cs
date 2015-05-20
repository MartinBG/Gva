using Docs.Api.Models.UnitModels;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocTypeUnitRole
    {
        public int DocTypeUnitRoleId { get; set; }

        public int DocTypeId { get; set; }

        public int DocDirectionId { get; set; }

        public int DocUnitRoleId { get; set; }

        public int UnitId { get; set; }

        public bool IsActive { get; set; }

        public byte[] Version { get; set; }

        public virtual DocDirection DocDirection { get; set; }

        public virtual DocType DocType { get; set; }

        public virtual DocUnitRole DocUnitRole { get; set; }

        public virtual Unit Unit { get; set; }
    }

    public class DocTypeUnitRoleMap : EntityTypeConfiguration<DocTypeUnitRole>
    {
        public DocTypeUnitRoleMap()
        {
            // Primary Key
            this.HasKey(t => t.DocTypeUnitRoleId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DocTypeUnitRoles");
            this.Property(t => t.DocTypeUnitRoleId).HasColumnName("DocTypeUnitRoleId");
            this.Property(t => t.DocTypeId).HasColumnName("DocTypeId");
            this.Property(t => t.DocDirectionId).HasColumnName("DocDirectionId");
            this.Property(t => t.DocUnitRoleId).HasColumnName("DocUnitRoleId");
            this.Property(t => t.UnitId).HasColumnName("UnitId");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.DocDirection)
                .WithMany(t => t.DocTypeUnitRoles)
                .HasForeignKey(d => d.DocDirectionId);
            this.HasRequired(t => t.DocType)
                .WithMany(t => t.DocTypeUnitRoles)
                .HasForeignKey(d => d.DocTypeId);
            this.HasRequired(t => t.DocUnitRole)
                .WithMany(t => t.DocTypeUnitRoles)
                .HasForeignKey(d => d.DocUnitRoleId);
            this.HasRequired(t => t.Unit)
                .WithMany()
                .HasForeignKey(d => d.UnitId);
        }
    }
}
