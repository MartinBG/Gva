using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Regs.Api.Models
{
    public partial class CommitVersion
    {
        public int CommitId { get; set; }

        public int PartVersionId { get; set; }

        public int? OldPartVersionId { get; set; }

        public virtual Commit Commit { get; set; }

        public virtual PartVersion PartVersion { get; set; }

        public virtual PartVersion OldPartVersion { get; set; }
    }

    public class CommitVersionMap : EntityTypeConfiguration<CommitVersion>
    {
        public CommitVersionMap()
        {
            // Primary Key
            this.HasKey(t => new { t.CommitId, t.PartVersionId });

            // Properties
            this.Property(t => t.CommitId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PartVersionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("LotCommitVersions");
            this.Property(t => t.CommitId).HasColumnName("LotCommitId");
            this.Property(t => t.PartVersionId).HasColumnName("LotPartVersionId");
            this.Property(t => t.OldPartVersionId).HasColumnName("OldLotPartVersionId");

            // Relationships
            this.HasRequired(t => t.Commit)
                .WithMany(t => t.CommitVersions)
                .HasForeignKey(d => d.CommitId);

            this.HasRequired(t => t.PartVersion)
                .WithMany()
                .HasForeignKey(d => d.PartVersionId);

            this.HasOptional(t => t.OldPartVersion)
                .WithMany()
                .HasForeignKey(t => t.OldPartVersionId);
        }
    }
}
