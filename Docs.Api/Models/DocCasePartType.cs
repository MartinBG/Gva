using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocCasePartType
    {
        public DocCasePartType()
        {
            this.Docs = new List<Doc>();
        }

        public int DocCasePartTypeId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual ICollection<Doc> Docs { get; set; }
    }

    public class DocCasePartTypeMap : EntityTypeConfiguration<DocCasePartType>
    {
        public DocCasePartTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.DocCasePartTypeId);

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
            this.ToTable("DocCasePartTypes");
            this.Property(t => t.DocCasePartTypeId).HasColumnName("DocCasePartTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
