using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocFileType
    {
        public DocFileType()
        {
            this.DocFiles = new List<DocFile>();
            this.IncomingDocFiles = new List<IncomingDocFile>();
        }

        public int DocFileTypeId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string DocTypeUri { get; set; }
        public bool HasEmbeddedUri { get; set; }
        public string MimeType { get; set; }
        public string Extention { get; set; }
        public bool IsEditable { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual ICollection<DocFile> DocFiles { get; set; }
        public virtual ICollection<IncomingDocFile> IncomingDocFiles { get; set; }
    }

    public class DocFileTypeMap : EntityTypeConfiguration<DocFileType>
    {
        public DocFileTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.DocFileTypeId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Alias)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.DocTypeUri)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.MimeType)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Extention)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DocFileTypes");
            this.Property(t => t.DocFileTypeId).HasColumnName("DocFileTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.DocTypeUri).HasColumnName("DocTypeUri");
            this.Property(t => t.HasEmbeddedUri).HasColumnName("HasEmbeddedUri");
            this.Property(t => t.MimeType).HasColumnName("MimeType");
            this.Property(t => t.Extention).HasColumnName("Extention");
            this.Property(t => t.IsEditable).HasColumnName("IsEditable");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
