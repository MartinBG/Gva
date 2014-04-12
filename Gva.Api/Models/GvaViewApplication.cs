using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;

namespace Gva.Api.Models
{
    public partial class GvaViewApplication
    {
        public int PartId { get; set; }

        public int? LotId { get; set; }

        public DateTime? RequestDate { get; set; }

        public string DocumentNumber { get; set; }

        public string ApplicationTypeName { get; set; }

        public string StatusName { get; set; }

        public virtual Lot Lot { get; set; }

        public virtual Part Part { get; set; }
    }

    public class GvaViewApplicationMap : EntityTypeConfiguration<GvaViewApplication>
    {
        public GvaViewApplicationMap()
        {
            // Primary Key
            this.HasKey(t => t.PartId);

            // Properties
            this.Property(t => t.PartId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DocumentNumber)
                .HasMaxLength(50);

            this.Property(t => t.ApplicationTypeName)
                .HasMaxLength(500);

            this.Property(t => t.StatusName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GvaViewApplications");
            this.Property(t => t.PartId).HasColumnName("LotPartId");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.RequestDate).HasColumnName("RequestDate");
            this.Property(t => t.DocumentNumber).HasColumnName("DocumentNumber");
            this.Property(t => t.ApplicationTypeName).HasColumnName("ApplicationTypeName");
            this.Property(t => t.StatusName).HasColumnName("StatusName");

            // Relationships
            this.HasOptional(t => t.Lot)
                .WithMany()
                .HasForeignKey(t => t.LotId);

            this.HasRequired(t => t.Part)
                .WithOptional();
        }
    }
}
