using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocCorrespondent
    {
        public int DocCorrespondentId { get; set; }

        public int DocId { get; set; }

        public int CorrespondentId { get; set; }

        public byte[] Version { get; set; }

        public virtual Correspondent Correspondent { get; set; }

        public virtual Doc Doc { get; set; }
    }

    public class DocCorrespondentMap : EntityTypeConfiguration<DocCorrespondent>
    {
        public DocCorrespondentMap()
        {
            // Primary Key
            this.HasKey(t => t.DocCorrespondentId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DocCorrespondents");
            this.Property(t => t.DocCorrespondentId).HasColumnName("DocCorrespondentId");
            this.Property(t => t.DocId).HasColumnName("DocId");
            this.Property(t => t.CorrespondentId).HasColumnName("CorrespondentId");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Correspondent)
                .WithMany(t => t.DocCorrespondents)
                .HasForeignKey(d => d.CorrespondentId);
            this.HasRequired(t => t.Doc)
                .WithMany(t => t.DocCorrespondents)
                .HasForeignKey(d => d.DocId)
                .WillCascadeOnDelete();
        }
    }
}
