using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;

namespace Gva.Api.Models
{
    public partial class GvaViewPersonRating
    {
        public int PartId { get; set; }

        public int? LotId { get; set; }

        public string RatingType { get; set; }

        public virtual Lot Lot { get; set; }

        public virtual Part Part { get; set; }

        public virtual GvaViewPerson Person { get; set; }
    }

    public class GvaViewPersonRatingMap : EntityTypeConfiguration<GvaViewPersonRating>
    {
        public GvaViewPersonRatingMap()
        {
            // Primary Key
            this.HasKey(t => t.PartId);

            // Properties
            this.Property(t => t.PartId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("GvaViewPersonRatings");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartId).HasColumnName("LotPartId");
            this.Property(t => t.RatingType).HasColumnName("RatingType");

            // Relationships
            this.HasOptional(t => t.Lot)
                .WithMany()
                .HasForeignKey(t => t.LotId);

            this.HasRequired(t => t.Part)
                .WithOptional();

            // Hack mapping to workaround joins
            this.HasOptional(t => t.Person)
                .WithMany(t => t.Ratings)
                .HasForeignKey(d => d.LotId);
        }
    }
}
