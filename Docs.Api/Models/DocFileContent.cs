using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocFileContent
    {
        public int DocFileContentId { get; set; }
        public System.Guid Key { get; set; }
        public string Hash { get; set; }
        public int Size { get; set; }
        public byte[] Content { get; set; }
    }

    public class DocFileContentMap : EntityTypeConfiguration<DocFileContent>
    {
        public DocFileContentMap()
        {
            // Primary Key
            this.HasKey(t => t.DocFileContentId);

            // Properties
            this.Property(t => t.Hash)
                .IsRequired()
                .HasMaxLength(40);

            this.Property(t => t.Content)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("DocFileContents");
            this.Property(t => t.DocFileContentId).HasColumnName("DocFileContentId");
            this.Property(t => t.Key).HasColumnName("Key");
            this.Property(t => t.Hash).HasColumnName("Hash");
            this.Property(t => t.Size).HasColumnName("Size");
            this.Property(t => t.Content).HasColumnName("Content");
        }
    }
}
