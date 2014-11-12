using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gva.Api.Models.Views.Person
{
    public partial class GvaViewPersonDocument
    {
        public int LotId { get; set; }

        public int PartIndex { get; set; }

        public string DocumentNumber { get; set; }

        public int? DocumentPersonNumber { get; set; }

        public virtual GvaViewPerson Person { get; set; }
    }

    public class GvaViewPersonDocumentMap : EntityTypeConfiguration<GvaViewPersonDocument>
    {
        public GvaViewPersonDocumentMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.PartIndex, t.DocumentNumber });

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DocumentNumber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.DocumentPersonNumber)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable("GvaViewPersonDocuments");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartIndex).HasColumnName("PartIndex");
            this.Property(t => t.DocumentNumber).HasColumnName("DocumentNumber");
            this.Property(t => t.DocumentPersonNumber).HasColumnName("DocumentPersonNumber");

            // Relationships
            this.HasRequired(t => t.Person)
                .WithMany(d => d.PersonDocuments)
                .HasForeignKey(t => t.LotId);
        }
    }
}
