﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Common.Models;

namespace Regs.Api.Models
{
    public partial class Commit
    {
        public Commit()
        {
            this.PartVersions = new List<PartVersion>();
        }

        public int CommitId { get; set; }
        public int LotId { get; set; }
        public int? ParentCommitId { get; set; }
        public int CommiterId { get; set; }
        public DateTime CommitDate { get; set; }
        public bool IsIndex { get; set; }

        public virtual Commit ParentCommit { get; set; }
        public virtual Lot Lot { get; set; }
        public virtual User Commiter { get; set; }
        public virtual ICollection<PartVersion> PartVersions { get; set; }
    }

    public class CommitMap : EntityTypeConfiguration<Commit>
    {
        public CommitMap()
        {
            // Primary Key
            this.HasKey(t => t.CommitId);

            // Table & Column Mappings
            this.ToTable("LotCommits");
            this.Property(t => t.CommitId).HasColumnName("LotCommitId");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.ParentCommitId).HasColumnName("ParentLotCommitId");
            this.Property(t => t.CommiterId).HasColumnName("CommiterId");
            this.Property(t => t.CommitDate).HasColumnName("CommitDate");
            this.Property(t => t.IsIndex).HasColumnName("IsIndex");

            // Relationships
            this.HasMany(t => t.PartVersions)
                .WithMany(t => t.Commits)
                .Map(m =>
                {
                    m.ToTable("LotCommitVersions");
                    m.MapLeftKey("LotCommitId");
                    m.MapRightKey("LotPartVersionId");
                });

            this.HasOptional(t => t.ParentCommit)
                .WithMany()
                .HasForeignKey(d => d.ParentCommitId);

            this.HasRequired(t => t.Lot)
                .WithMany(t => t.Commits)
                .HasForeignKey(d => d.LotId)
                .WillCascadeOnDelete();

            this.HasRequired(t => t.Commiter)
                .WithMany()
                .HasForeignKey(d => d.CommiterId);
        }
    }
}