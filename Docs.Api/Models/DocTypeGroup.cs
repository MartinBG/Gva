using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocTypeGroup
    {
        public DocTypeGroup()
        {
            this.DocTypes = new List<DocType>();
        }

        public int DocTypeGroupId { get; set; }
        public string Name { get; set; }
        public bool IsElectronicService { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual ICollection<DocType> DocTypes { get; set; }
    }

    public class DocTypeGroupMap : EntityTypeConfiguration<DocTypeGroup>
    {
        public DocTypeGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.DocTypeGroupId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DocTypeGroups");
            this.Property(t => t.DocTypeGroupId).HasColumnName("DocTypeGroupId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsElectronicService).HasColumnName("IsElectronicService");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
