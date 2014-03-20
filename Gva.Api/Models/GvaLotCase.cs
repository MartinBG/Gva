using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;

namespace Gva.Api.Models
{
    public partial class GvaLotCase
    {
        public int GvaLotCaseId { get; set; }

        public int GvaCaseTypeId { get; set; }

        public int LotId { get; set; }

        public virtual GvaCaseType GvaCaseType { get; set; }

        public virtual Lot Lot { get; set; }
    }

    public class GvaLotCaseMap : EntityTypeConfiguration<GvaLotCase>
    {
        public GvaLotCaseMap()
        {
            // Primary Key
            this.HasKey(t => t.GvaLotCaseId);

            // Properties
            this.Property(t => t.GvaLotCaseId);

            // Table & Column Mappings
            this.ToTable("GvaLotCases");
            this.Property(t => t.GvaLotCaseId).HasColumnName("GvaLotCaseId");
            this.Property(t => t.GvaCaseTypeId).HasColumnName("GvaCaseTypeId");
            this.Property(t => t.LotId).HasColumnName("LotId");

            // Relationships
            this.HasRequired(t => t.GvaCaseType)
                .WithMany(t => t.GvaLotCases)
                .HasForeignKey(d => d.GvaCaseTypeId);
            this.HasRequired(t => t.Lot)
                .WithMany()
                .HasForeignKey(d => d.LotId);

        }
    }
}
