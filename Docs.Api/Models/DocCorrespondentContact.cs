using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocCorrespondentContact
    {
        public int DocCorrespondentContactId { get; set; }

        public int DocId { get; set; }

        public int CorrespondentContactId { get; set; }

        public byte[] Version { get; set; }

        public virtual CorrespondentContact CorrespondentContact { get; set; }

        public virtual Doc Doc { get; set; }
    }

    public class DocCorrespondentContactMap : EntityTypeConfiguration<DocCorrespondentContact>
    {
        public DocCorrespondentContactMap()
        {
            // Primary Key
            this.HasKey(t => t.DocCorrespondentContactId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DocCorrespondentContacts");
            this.Property(t => t.DocCorrespondentContactId).HasColumnName("DocCorrespondentContactId");
            this.Property(t => t.DocId).HasColumnName("DocId");
            this.Property(t => t.CorrespondentContactId).HasColumnName("CorrespondentContactId");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.CorrespondentContact)
                .WithMany(t => t.DocCorrespondentContacts)
                .HasForeignKey(d => d.CorrespondentContactId);
            this.HasRequired(t => t.Doc)
                .WithMany(t => t.DocCorrespondentContacts)
                .HasForeignKey(d => d.DocId);
        }
    }
}
