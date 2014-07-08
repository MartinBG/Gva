using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Common.Api.Models
{
    public partial class ClassificationPermission
    {
        public int ClassificationPermissionId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public bool IsActive { get; set; }

        public byte[] Version { get; set; }
    }

    public class ClassificationPermissionMap : EntityTypeConfiguration<ClassificationPermission>
    {
        public ClassificationPermissionMap()
        {
            // Primary Key
            this.HasKey(t => t.ClassificationPermissionId);

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
            this.ToTable("ClassificationPermissions");
            this.Property(t => t.ClassificationPermissionId).HasColumnName("ClassificationPermissionId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
