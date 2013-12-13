using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocTypeClassification
    {
        public int DocTypeClassificationId { get; set; }
        public int DocTypeId { get; set; }
        public int DocDirectionId { get; set; }
        public int ClassificationId { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual Classification Classification { get; set; }
        public virtual DocDirection DocDirection { get; set; }
        public virtual DocType DocType { get; set; }
    }

    public class DocTypeClassificationMap : EntityTypeConfiguration<DocTypeClassification>
    {
        public DocTypeClassificationMap()
        {
            // Primary Key
            this.HasKey(t => t.DocTypeClassificationId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DocTypeClassifications");
            this.Property(t => t.DocTypeClassificationId).HasColumnName("DocTypeClassificationId");
            this.Property(t => t.DocTypeId).HasColumnName("DocTypeId");
            this.Property(t => t.DocDirectionId).HasColumnName("DocDirectionId");
            this.Property(t => t.ClassificationId).HasColumnName("ClassificationId");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Classification)
                .WithMany(t => t.DocTypeClassifications)
                .HasForeignKey(d => d.ClassificationId);
            this.HasRequired(t => t.DocDirection)
                .WithMany(t => t.DocTypeClassifications)
                .HasForeignKey(d => d.DocDirectionId);
            this.HasRequired(t => t.DocType)
                .WithMany(t => t.DocTypeClassifications)
                .HasForeignKey(d => d.DocTypeId);

        }
    }
}
