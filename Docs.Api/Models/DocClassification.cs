using Docs.Api.Models.ClassificationModels;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocClassification
    {
        public int DocClassificationId { get; set; }

        public int DocId { get; set; }

        public int ClassificationId { get; set; }

        public int ClassificationByUserId { get; set; }

        public System.DateTime ClassificationDate { get; set; }

        public bool IsInherited { get; set; }

        public bool IsActive { get; set; }

        public byte[] Version { get; set; }

        public virtual Classification Classification { get; set; }

        public virtual Doc Doc { get; set; }
    }

    public class DocClassificationMap : EntityTypeConfiguration<DocClassification>
    {
        public DocClassificationMap()
        {
            // Primary Key
            this.HasKey(t => t.DocClassificationId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DocClassifications");
            this.Property(t => t.DocClassificationId).HasColumnName("DocClassificationId");
            this.Property(t => t.DocId).HasColumnName("DocId");
            this.Property(t => t.ClassificationId).HasColumnName("ClassificationId");
            this.Property(t => t.ClassificationByUserId).HasColumnName("ClassificationByUserId");
            this.Property(t => t.ClassificationDate).HasColumnName("ClassificationDate");
            this.Property(t => t.IsInherited).HasColumnName("IsInherited");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Classification)
                .WithMany()
                .HasForeignKey(d => d.ClassificationId);
            this.HasRequired(t => t.Doc)
                .WithMany(t => t.DocClassifications)
                .HasForeignKey(d => d.DocId)
                .WillCascadeOnDelete();
        }
    }
}
