using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Common.Models;

namespace Regs.Api.Models
{
    public partial class PartVersion
    {
        public PartVersion()
        {
            this.Commits = new List<Commit>();
        }

        public int PartVersionId { get; set; }
        public int PartId { get; set; }
        public int TextBlobId { get; set; }
        public int OriginalCommitId { get; set; }
        public int CreatorId { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Commit OriginalCommit { get; set; }
        public virtual PartOperation PartOperation { get; set; }
        public virtual Part Part { get; set; }
        public virtual TextBlob TextBlob { get; set; }
        public virtual ICollection<Commit> Commits { get; set; }
        public virtual User User { get; set; }
    }

    public class PartVersionMap : EntityTypeConfiguration<PartVersion>
    {
        public PartVersionMap()
        {
            // Primary Key
            this.HasKey(t => t.PartVersionId);

            // Table & Column Mappings
            this.ToTable("LotPartVersions");
            this.Property(t => t.PartVersionId).HasColumnName("LotPartVersionId");
            this.Property(t => t.PartId).HasColumnName("LotPartId");
            this.Property(t => t.TextBlobId).HasColumnName("TextBlobId");
            this.Property(t => t.OriginalCommitId).HasColumnName("OriginalCommitId");
            this.Property(t => t.PartOperation).HasColumnName("LotPartOperationId");
            this.Property(t => t.CreatorId).HasColumnName("CreatorId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");

            // Relationships
            this.HasRequired(t => t.OriginalCommit)
                .WithMany()
                .HasForeignKey(d => d.OriginalCommitId);

            this.HasRequired(t => t.Part)
                .WithMany(t => t.PartVersions)
                .HasForeignKey(d => d.PartId)
                .WillCascadeOnDelete();

            this.HasRequired(t => t.TextBlob)
                .WithMany()
                .HasForeignKey(d => d.TextBlobId);

            this.HasRequired(t => t.User)
                .WithMany()
                .HasForeignKey(d => d.CreatorId);
        }
    }
}
