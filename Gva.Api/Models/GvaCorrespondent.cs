using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;
using System;
using Docs.Api.Models;

namespace Gva.Api.Models
{
    public partial class GvaCorrespondent
    {
        public int GvaCorrespondentId { get; set; }
        public int LotId { get; set; }
        public int CorrespondentId { get; set; }
        public bool IsActive { get; set; }
        public virtual Correspondent Correspondent { get; set; }
        public virtual Lot Lot { get; set; }
    }

    public class GvaCorrespondentMap : EntityTypeConfiguration<GvaCorrespondent>
    {
        public GvaCorrespondentMap()
        {
            // Primary Key
            this.HasKey(t => t.GvaCorrespondentId);

            // Properties
            // Table & Column Mappings
            this.ToTable("GvaCorrespondents");
            this.Property(t => t.GvaCorrespondentId).HasColumnName("GvaCorrespondentId");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.CorrespondentId).HasColumnName("CorrespondentId");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.Correspondent)
                .WithMany()
                .HasForeignKey(d => d.CorrespondentId);
            this.HasRequired(t => t.Lot)
                .WithMany()
                .HasForeignKey(d => d.LotId);

        }
    }
}
