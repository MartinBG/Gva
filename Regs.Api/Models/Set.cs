﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Infrastructure;

namespace Regs.Api.Models
{
    public partial class Set
    {
        public Set()
        {
            this.Lots = new List<Lot>();
            this.SetParts = new List<SetPart>();
        }

        public int SetId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }

        public virtual ICollection<Lot> Lots { get; set; }
        public virtual ICollection<SetPart> SetParts { get; set; }

        public Lot AddLot(UserContext userContext)
        {
            Commit index = new Commit
            {
                CommiterId = userContext.UserId,
                CommitDate = DateTime.Now,
                IsIndex = true
            };

            Lot lot = new Lot
            {
                NextIndex = 0,
                Set = this
            };
            lot.Commits.Add(index);
            this.Lots.Add(lot);

            return lot;
        }
    }

    public class SetMap : EntityTypeConfiguration<Set>
    {
        public SetMap()
        {
            // Primary Key
            this.HasKey(t => t.SetId);

            // Properties
            this.Property(t => t.SetId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Alias)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("LotSets");
            this.Property(t => t.SetId).HasColumnName("LotSetId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
        }
    }
}