using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;
using Regs.Api.Models;
using Gva.Api.ModelsDO.Persons;

namespace Gva.Api.Models.Views.Person
{
    public partial class GvaViewPersonRatingEdition
    {

        public int LotId { get; set; }

        public int PartId { get; set; }

        public int PartIndex { get; set; }

        public int RatingPartIndex { get; set; }

        public string RatingSubClasses { get; set; }

        public string Limitations { get; set; }

        public DateTime DocDateValidFrom { get; set; }

        public DateTime? DocDateValidTo { get; set; }

        public string Notes { get; set; }

        public string NotesAlt { get; set; }

        public int Index { get; set; }

        public virtual Part Part { get; set; }

        public virtual GvaViewPersonRating Rating { get; set; }
    }

    public class GvaViewPersonRatingEditionMap : EntityTypeConfiguration<GvaViewPersonRatingEdition>
    {
        public GvaViewPersonRatingEditionMap()
        {
            /// Primary Key
            this.HasKey(t => new { t.LotId, t.RatingPartIndex, t.PartIndex });

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PartIndex)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("GvaViewPersonRatingEditions");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartId).HasColumnName("LotPartId");
            this.Property(t => t.PartIndex).HasColumnName("PartIndex");
            this.Property(t => t.RatingPartIndex).HasColumnName("RatingPartIndex");
            this.Property(t => t.RatingSubClasses).HasColumnName("RatingSubClasses");
            this.Property(t => t.Limitations).HasColumnName("Limitations");
            this.Property(t => t.DocDateValidFrom).HasColumnName("DocDateValidFrom");
            this.Property(t => t.DocDateValidTo).HasColumnName("DocDateValidTo");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.NotesAlt).HasColumnName("NotesAlt");
            this.Property(t => t.Index).HasColumnName("Index");

            // Relationships
            this.HasRequired(t => t.Part)
                .WithMany()
                .HasForeignKey(t => t.PartId);
            this.HasRequired(t => t.Rating)
                .WithMany(d => d.Editions)
                .HasForeignKey(t => new { t.LotId, t.RatingPartIndex });
        }
    }
}
