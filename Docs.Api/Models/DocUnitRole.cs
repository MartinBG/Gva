using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocUnitRole
    {
        public DocUnitRole()
        {
            this.DocTypeUnitRoles = new List<DocTypeUnitRole>();
            this.DocUnits = new List<DocUnit>();
        }

        public int DocUnitRoleId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public bool IsActive { get; set; }

        public byte[] Version { get; set; }

        public virtual ICollection<DocTypeUnitRole> DocTypeUnitRoles { get; set; }

        public virtual ICollection<DocUnit> DocUnits { get; set; }
    }

    public class DocUnitRoleMap : EntityTypeConfiguration<DocUnitRole>
    {
        public DocUnitRoleMap()
        {
            // Primary Key
            this.HasKey(t => t.DocUnitRoleId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Alias)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DocUnitRoles");
            this.Property(t => t.DocUnitRoleId).HasColumnName("DocUnitRoleId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
