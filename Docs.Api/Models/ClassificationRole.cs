using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class ClassificationRole
    {
        public ClassificationRole()
        {
            this.UnitClassifications = new List<UnitClassification>();
        }

        public int ClassificationRoleId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual ICollection<UnitClassification> UnitClassifications { get; set; }
    }

    public class ClassificationRoleMap : EntityTypeConfiguration<ClassificationRole>
    {
        public ClassificationRoleMap()
        {
            // Primary Key
            this.HasKey(t => t.ClassificationRoleId);

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
            this.ToTable("ClassificationRoles");
            this.Property(t => t.ClassificationRoleId).HasColumnName("ClassificationRoleId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
