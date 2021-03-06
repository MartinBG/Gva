﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using Common.Api.Models;
using Common.Sequence;
using Newtonsoft.Json.Linq;

namespace Regs.Api.Models
{
    public partial class Commit
    {
        public static Sequence CommitSequence = new Sequence("commitSequence");

        public Commit()
        {
            this.CommitVersions = new List<CommitVersion>();
        }

        public int CommitId { get; set; }

        public int LotId { get; set; }

        public int? ParentCommitId { get; set; }

        public int CommiterId { get; set; }

        public DateTime CommitDate { get; set; }

        public bool IsIndex { get; set; }

        public bool IsLoaded { get; set; }

        public virtual Commit ParentCommit { get; set; }

        public virtual Lot Lot { get; set; }

        public virtual User Commiter { get; set; }

        public virtual ICollection<CommitVersion> CommitVersions { get; set; }

        public virtual ICollection<PartVersion> ChangedPartVersions { get; set; }

        public void EnsureIsLoaded()
        {
            if (!this.IsLoaded)
            {
                throw new InvalidOperationException(string.Format("Commit with id {0} has not been loaded.", this.CommitId));
            }
        }

        public PartCollection Parts
        {
            get
            {
                var parts = this.CommitVersions
                    .Select(cv => cv.PartVersion)
                    .Where(pv => pv.PartOperation != PartOperation.Delete);

                return new PartCollection(parts);
            }
        }

        public PartCollection OldParts
        {
            get
            {
                var parts = this.CommitVersions
                    .Select(cv => cv.PartVersion.OriginalCommit == this ? cv.OldPartVersion : cv.PartVersion)
                    .Where(pv => pv != null);

                return new PartCollection(parts);
            }
        }

        public PartVersion<T> GetPart<T>(string path)
            where T : class
        {
            return this.Parts.Get<T>(path);
        }

        public PartVersion<T>[] GetParts<T>(string pathSpec)
            where T : class
        {
            return this.Parts.GetAll<T>(pathSpec);
        }
    }

    public class CommitMap : EntityTypeConfiguration<Commit>
    {
        public CommitMap()
        {
            // Primary Key
            this.HasKey(t => t.CommitId);

            // Properties
            this.Property(t => t.CommitId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("LotCommits");
            this.Property(t => t.CommitId).HasColumnName("LotCommitId");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.ParentCommitId).HasColumnName("ParentLotCommitId");
            this.Property(t => t.CommiterId).HasColumnName("CommiterId");
            this.Property(t => t.CommitDate).HasColumnName("CommitDate");
            this.Property(t => t.IsIndex).HasColumnName("IsIndex");

            // Relationships
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

            // Local-only properties
            this.Ignore(t => t.IsLoaded);
            this.Ignore(t => t.Parts);
            this.Ignore(t => t.OldParts);
        }
    }
}
