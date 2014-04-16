using System;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocFile
    {
        public DocFile()
        {
        }

        public int DocFileId { get; set; }

        public int DocId { get; set; }

        public int DocFileTypeId { get; set; }

        public int DocFileKindId { get; set; }

        public Nullable<int> DocFileOriginTypeId { get; set; }

        public string Name { get; set; }

        public string DocFileName { get; set; }

        public string DocContentStorage { get; set; }

        public Guid DocFileContentId { get; set; }

        public DateTime? SignDate { get; set; }

        public bool IsPrimary { get; set; }

        public bool IsSigned { get; set; }

        public bool IsActive { get; set; }

        public byte[] Version { get; set; }

        public virtual DocFileKind DocFileKind { get; set; }

        public virtual DocFileType DocFileType { get; set; }

        public virtual DocFileOriginType DocFileOriginType { get; set; }

        public virtual Doc Doc { get; set; }
    }

    public class DocFileMap : EntityTypeConfiguration<DocFile>
    {
        public DocFileMap()
        {
            // Primary Key
            this.HasKey(t => t.DocFileId);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(1000);

            this.Property(t => t.DocFileName)
                .HasMaxLength(1000);

            this.Property(t => t.DocContentStorage)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DocFiles");
            this.Property(t => t.DocFileId).HasColumnName("DocFileId");
            this.Property(t => t.DocId).HasColumnName("DocId");
            this.Property(t => t.DocFileTypeId).HasColumnName("DocFileTypeId");
            this.Property(t => t.DocFileKindId).HasColumnName("DocFileKindId");
            this.Property(t => t.DocFileOriginTypeId).HasColumnName("DocFileOriginTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.DocFileName).HasColumnName("DocFileName");
            this.Property(t => t.DocContentStorage).HasColumnName("DocContentStorage");
            this.Property(t => t.DocFileContentId).HasColumnName("DocFileContentId");
            this.Property(t => t.SignDate).HasColumnName("SignDate");
            this.Property(t => t.IsPrimary).HasColumnName("IsPrimary");
            this.Property(t => t.IsSigned).HasColumnName("IsSigned");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.DocFileKind)
                .WithMany(t => t.DocFiles)
                .HasForeignKey(d => d.DocFileKindId);
            this.HasOptional(t => t.DocFileOriginType)
                .WithMany(t => t.DocFiles)
                .HasForeignKey(d => d.DocFileOriginTypeId);
            this.HasRequired(t => t.DocFileType)
                .WithMany(t => t.DocFiles)
                .HasForeignKey(d => d.DocFileTypeId);
            this.HasRequired(t => t.Doc)
                .WithMany(t => t.DocFiles)
                .HasForeignKey(d => d.DocId)
                .WillCascadeOnDelete();
        }
    }
}
