﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;
using Common.Sequence;

namespace Regs.Api.Models
{
    public partial class Part
    {
        public static Sequence PartSequence = new Sequence("partSequence");

        public Part()
        {
            this.PartVersions = new List<PartVersion>();
            this.PartUsers = new List<PartUser>();
        }

        public int PartId { get; set; }

        public int SetPartId { get; set; }

        public int LotId { get; set; }

        public string Path { get; set; }

        public int Index { get; set; }

        public int CreatorId { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual User Creator { get; set; }

        public virtual Lot Lot { get; set; }

        public virtual SetPart SetPart { get; set; }

        public virtual ICollection<PartVersion> PartVersions { get; set; }

        public virtual ICollection<PartUser> PartUsers { get; set; }

        public bool Matches(string path)
        {
            return this.Path.StartsWith(path);
        }
    }

    public class PartMap : EntityTypeConfiguration<Part>
    {
        public PartMap()
        {
            // Primary Key
            this.HasKey(t => t.PartId);

            // Properties
            this.Property(t => t.PartId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Path)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("LotParts");
            this.Property(t => t.PartId).HasColumnName("LotPartId");
            this.Property(t => t.SetPartId).HasColumnName("LotSetPartId");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.Path).HasColumnName("Path");
            this.Property(t => t.Index).HasColumnName("Index");
            this.Property(t => t.CreatorId).HasColumnName("CreatorId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");

            // Relationships
            this.HasRequired(t => t.Lot)
                .WithMany(t => t.Parts)
                .HasForeignKey(d => d.LotId)
                .WillCascadeOnDelete();

            this.HasRequired(t => t.SetPart)
                .WithMany()
                .HasForeignKey(d => d.SetPartId);

            this.HasRequired(t => t.Creator)
                .WithMany()
                .HasForeignKey(d => d.CreatorId);
        }
    }
}
