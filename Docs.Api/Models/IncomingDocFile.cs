using System;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class IncomingDocFile
    {
        public int IncomingDocFileId { get; set; }

        public int IncomingDocId { get; set; }

        public int DocFileTypeId { get; set; }

        public string Name { get; set; }

        public string DocFileName { get; set; }

        public string DocContentStorage { get; set; }

        public Guid? DocFileContentId { get; set; }

        public byte[] Version { get; set; }

        public virtual DocFileType DocFileType { get; set; }

        public virtual IncomingDoc IncomingDoc { get; set; }
    }

    public class IncomingDocFileMap : EntityTypeConfiguration<IncomingDocFile>
    {
        public IncomingDocFileMap()
        {
            // Primary Key
            this.HasKey(t => t.IncomingDocFileId);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(1000);

            this.Property(t => t.DocFileName)
                .IsRequired()
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
            this.ToTable("IncomingDocFiles");
            this.Property(t => t.IncomingDocFileId).HasColumnName("IncomingDocFileId");
            this.Property(t => t.IncomingDocId).HasColumnName("IncomingDocId");
            this.Property(t => t.DocFileTypeId).HasColumnName("DocFileTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.DocFileName).HasColumnName("DocFileName");
            this.Property(t => t.DocContentStorage).HasColumnName("DocContentStorage");
            this.Property(t => t.DocFileContentId).HasColumnName("DocFileContentId");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.DocFileType)
                .WithMany(t => t.IncomingDocFiles)
                .HasForeignKey(d => d.DocFileTypeId);
            this.HasRequired(t => t.IncomingDoc)
                .WithMany(t => t.IncomingDocFiles)
                .HasForeignKey(d => d.IncomingDocId);
        }
    }
}
