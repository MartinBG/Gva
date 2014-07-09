using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;

namespace Gva.Api.Models.Views.Person
{
    public partial class GvaViewPersonRating
    {
        public int LotId { get; set; }

        public int PartIndex { get; set; }

        public int RatingTypeId { get; set; }

        public virtual NomValue RatingType { get; set; }

        public virtual GvaViewPerson Person { get; set; }
    }

    public class GvaViewPersonRatingMap : EntityTypeConfiguration<GvaViewPersonRating>
    {
        public GvaViewPersonRatingMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.PartIndex });

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(t => t.PartIndex)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("GvaViewPersonRatings");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartIndex).HasColumnName("PartIndex");
            this.Property(t => t.RatingTypeId).HasColumnName("RatingTypeId");

            // Relationships
            this.HasRequired(t => t.Person)
                .WithMany(t => t.Ratings)
                .HasForeignKey(d => d.LotId);
            this.HasRequired(t => t.RatingType)
                .WithMany()
                .HasForeignKey(t => t.RatingTypeId);
        }
    }
}
