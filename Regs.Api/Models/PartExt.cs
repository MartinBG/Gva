using System;
using System.Data.Entity.ModelConfiguration;

namespace Regs.Api.Models
{
    public partial class PartExt
    {
        public int PartId { get; set; }
        public int? IndexPartVersionId { get; set; }
        public int CommitedPartVersionId { get; set; }
        public int FirstPartVersionId { get; set; }

        public virtual Part Part { get; set; }
        public virtual PartVersion CommitedPartVersion { get; set; }
        public virtual PartVersion FirstPartVersion { get; set; }
        public virtual PartVersion IndexPartVersion { get; set; }
    }

    public class PartExtMap : EntityTypeConfiguration<PartExt>
    {
        public PartExtMap()
        {
            // Primary Key
            this.HasKey(t => t.PartId);

            // Table & Column Mappings
            this.ToTable("LotPartExts");
            this.Property(t => t.PartId).HasColumnName("LotPartId");
            this.Property(t => t.IndexPartVersionId).HasColumnName("IndexLotPartVersionId");
            this.Property(t => t.CommitedPartVersionId).HasColumnName("CommitedLotPartVersionId");
            this.Property(t => t.FirstPartVersionId).HasColumnName("FirstLotPartVersionId");

            // Relationships
            this.HasRequired(t => t.Part)
                .WithOptional();

            this.HasRequired(t => t.CommitedPartVersion)
                .WithMany()
                .HasForeignKey(d => d.CommitedPartVersionId);

            this.HasRequired(t => t.FirstPartVersion)
                .WithMany()
                .HasForeignKey(d => d.FirstPartVersionId);

            this.HasOptional(t => t.IndexPartVersion)
                .WithMany()
                .HasForeignKey(d => d.IndexPartVersionId);
        }
    }
}
