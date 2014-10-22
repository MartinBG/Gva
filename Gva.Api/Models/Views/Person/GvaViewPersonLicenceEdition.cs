using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;
using Regs.Api.Models;

namespace Gva.Api.Models.Views.Person
{
    public partial class GvaViewPersonLicenceEdition
    {
        public int LotId { get; set; }

        public int PartId { get; set; }

        public int PartIndex { get; set; }

        public int LicencePartIndex { get; set; }

        public int Index { get; set; }

        public string StampNumber { get; set; }

        public DateTime FirstDocDateValidFrom { get; set; }

        public DateTime DateValidFrom { get; set; }

        public DateTime? DateValidTo { get; set; }

        public int LicenceActionId { get; set; }

        public bool IsLastEdition { get; set; }

        public string Notes { get; set; }

        public string Inspector { get; set; }

        public string Limitations { get; set; }

        public virtual Part Part { get; set; }

        public virtual NomValue LicenceAction { get; set; }

        public virtual GvaViewPersonLicence Licence { get; set; }

    }

    public class GvaViewPersonLicenceEditionMap : EntityTypeConfiguration<GvaViewPersonLicenceEdition>
    {
        public GvaViewPersonLicenceEditionMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.LicencePartIndex, t.PartIndex });

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PartId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.StampNumber)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GvaViewPersonLicenceEditions");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartIndex).HasColumnName("PartIndex");
            this.Property(t => t.PartId).HasColumnName("PartId");
            this.Property(t => t.StampNumber).HasColumnName("StampNumber");
            this.Property(t => t.FirstDocDateValidFrom).HasColumnName("FirstDocDateValidFrom");
            this.Property(t => t.IsLastEdition).HasColumnName("IsLastEdition");
            this.Property(t => t.DateValidFrom).HasColumnName("DateValidFrom");
            this.Property(t => t.DateValidTo).HasColumnName("DateValidTo");
            this.Property(t => t.LicenceActionId).HasColumnName("LicenceActionId");
            this.Property(t => t.LicencePartIndex).HasColumnName("LicencePartIndex");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.Inspector).HasColumnName("Inspector");
            this.Property(t => t.Limitations).HasColumnName("Limitations");

            // Relationships
            this.HasRequired(t => t.Part)
                .WithMany()
                .HasForeignKey(t => t.PartId);
            this.HasRequired(t => t.LicenceAction)
                .WithMany()
                .HasForeignKey(t => t.LicenceActionId);
            this.HasRequired(t => t.Licence)
                .WithMany(d => d.Editions)
                .HasForeignKey(t => new { t.LotId, t.LicencePartIndex });
        }
    }
}
