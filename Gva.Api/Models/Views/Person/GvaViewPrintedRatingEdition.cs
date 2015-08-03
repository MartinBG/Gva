using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;
using Regs.Api.Models;
using Regs.Api.LotEvents;
using Gva.Api.Models.Views.Person;

namespace Gva.Api.Models.Views.Person
{
    public partial class GvaViewPrintedRatingEdition : IProjectionView
    {
        public int LotId { get; set; }

        public int RatingPartIndex { get; set; }

        public int RatingEditionPartIndex { get; set; }

        public int LicencePartIndex { get; set; }

        public int LicenceEditionPartIndex { get; set; }

        public int PrintedFileId { get; set; }

        public virtual GvaViewPersonLicenceEdition LicenceEdition { get; set; }
    }

    public class GvaViewPrintedRatingEditionMap : EntityTypeConfiguration<GvaViewPrintedRatingEdition>
    {
        public GvaViewPrintedRatingEditionMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.RatingPartIndex, t.RatingEditionPartIndex, t.LicencePartIndex, t.LicenceEditionPartIndex });

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RatingPartIndex)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.RatingEditionPartIndex)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.LicencePartIndex)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.LicenceEditionPartIndex)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PrintedFileId)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("GvaViewPrintedRatingEditions");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.RatingPartIndex).HasColumnName("RatingPartIndex");
            this.Property(t => t.RatingEditionPartIndex).HasColumnName("RatingEditionPartIndex");
            this.Property(t => t.LicencePartIndex).HasColumnName("LicencePartIndex");
            this.Property(t => t.LicenceEditionPartIndex).HasColumnName("LicenceEditionPartIndex");
            this.Property(t => t.PrintedFileId).HasColumnName("PrintedFileId");

            this.HasRequired(t => t.LicenceEdition)
                .WithMany(t => t.PrintedRatingEditions)
                .HasForeignKey(d => new { d.LotId, d.LicencePartIndex, d.LicenceEditionPartIndex });
        }
    }
}
