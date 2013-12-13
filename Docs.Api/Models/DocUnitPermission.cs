using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocUnitPermission
    {
        public DocUnitPermission()
        {
            this.DocUsers = new List<DocUser>();
        }

        public int DocUnitPermissionId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual ICollection<DocUser> DocUsers { get; set; }
    }

    public class DocUnitPermissionMap : EntityTypeConfiguration<DocUnitPermission>
    {
        public DocUnitPermissionMap()
        {
            // Primary Key
            this.HasKey(t => t.DocUnitPermissionId);

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
            this.ToTable("DocUnitPermissions");
            this.Property(t => t.DocUnitPermissionId).HasColumnName("DocUnitPermissionId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
