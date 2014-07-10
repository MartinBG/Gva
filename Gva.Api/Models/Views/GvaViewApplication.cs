using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;
using Regs.Api.Models;

namespace Gva.Api.Models.Views
{
    public partial class GvaViewApplication
    {
        public int LotId { get; set; }

        public int PartId { get; set; }

        public DateTime? RequestDate { get; set; }

        public string DocumentNumber { get; set; }

        public int ApplicationTypeId { get; set; }

        public virtual NomValue ApplicationType { get; set; }

        public virtual Lot Lot { get; set; }

        public virtual Part Part { get; set; }
    }

    public class GvaViewApplicationMap : EntityTypeConfiguration<GvaViewApplication>
    {
        public GvaViewApplicationMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.PartId });

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PartId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DocumentNumber)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("GvaViewApplications");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartId).HasColumnName("LotPartId");
            this.Property(t => t.RequestDate).HasColumnName("RequestDate");
            this.Property(t => t.DocumentNumber).HasColumnName("DocumentNumber");
            this.Property(t => t.ApplicationTypeId).HasColumnName("ApplicationTypeId");

            // Relationships
            this.HasRequired(t => t.Lot)
                .WithMany()
                .HasForeignKey(t => t.LotId);

            this.HasRequired(t => t.Part)
                .WithMany()
                .HasForeignKey(t => t.PartId);

            this.HasRequired(t => t.ApplicationType)
                .WithMany()
                .HasForeignKey(t => t.ApplicationTypeId);
        }
    }
}