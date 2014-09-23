﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;
using Regs.Api.Models;
using Gva.Api.ModelsDO.Persons;

namespace Gva.Api.Models.Views.Person
{
    public partial class GvaViewPersonRating
    {

        public int LotId { get; set; }

        public int PartId { get; set; }

        public int EditionIndex { get; set; }

        public int RatingPartIndex { get; set; }

        public int EditionPartIndex { get; set; }

        public int? RatingTypeId { get; set; }

        public int? RatingLevelId { get; set; }

        public int? RatingClassId { get; set; }

        public int? AircraftTypeGroupId { get; set; }

        public int? AuthorizationId { get; set; }

        public string RatingSubClasses { get; set; }

        public string Limitations { get; set; }

        public DateTime LastDocDateValidFrom { get; set; }

        public DateTime LastDocDateValidTo { get; set; }

        public DateTime FirstDocDateValidFrom { get; set; }

        public string Notes { get; set; }

        public string NotesAlt { get; set; }

        public virtual GvaViewPerson Person { get; set; }

        public virtual Part Part { get; set; }

        public virtual NomValue RatingType { get; set; }

        public virtual NomValue RatingLevel { get; set; }

        public virtual NomValue RatingClass { get; set; }

        public virtual NomValue AircraftTypeGroup { get; set; }

        public virtual NomValue Authorization { get; set; }

    }

    public class GvaViewPersonRatingMap : EntityTypeConfiguration<GvaViewPersonRating>
    {
        public GvaViewPersonRatingMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.PartId });

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PartId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("GvaViewPersonRatings");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartId).HasColumnName("LotPartId");
            this.Property(t => t.EditionIndex).HasColumnName("EditionIndex");
            this.Property(t => t.RatingPartIndex).HasColumnName("RatingPartIndex");
            this.Property(t => t.EditionPartIndex).HasColumnName("EditionPartIndex");
            this.Property(t => t.RatingTypeId).HasColumnName("RatingTypeId");
            this.Property(t => t.RatingLevelId).HasColumnName("RatingLevelId");
            this.Property(t => t.RatingClassId).HasColumnName("RatingClassId");
            this.Property(t => t.AircraftTypeGroupId).HasColumnName("AircraftTypeGroupId");
            this.Property(t => t.AuthorizationId).HasColumnName("AuthorizationId");
            this.Property(t => t.RatingSubClasses).HasColumnName("RatingSubClasses");
            this.Property(t => t.Limitations).HasColumnName("Limitations");
            this.Property(t => t.LastDocDateValidFrom).HasColumnName("LastDocDateValidFrom");
            this.Property(t => t.LastDocDateValidTo).HasColumnName("LastDocDateValidTo");
            this.Property(t => t.FirstDocDateValidFrom).HasColumnName("FirstDocDateValidFrom");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.NotesAlt).HasColumnName("NotesAlt");

            // Relationships
            this.HasRequired(t => t.Person)
                .WithMany(d => d.PersonRatings)
                .HasForeignKey(t => t.LotId);
            this.HasRequired(t => t.Part)
                .WithMany()
                .HasForeignKey(t => t.PartId);
            this.HasOptional(t => t.RatingType)
                .WithMany()
                .HasForeignKey(t => t.RatingTypeId);
            this.HasOptional(t => t.RatingLevel)
                .WithMany()
                .HasForeignKey(t => t.RatingLevelId);
            this.HasOptional(t => t.RatingClass)
                .WithMany()
                .HasForeignKey(t => t.RatingClassId);
            this.HasOptional(t => t.AircraftTypeGroup)
                .WithMany()
                .HasForeignKey(t => t.AircraftTypeGroupId);
            this.HasOptional(t => t.Authorization)
                .WithMany()
                .HasForeignKey(t => t.AuthorizationId);
        }
    }
}