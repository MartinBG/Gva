using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;
using Regs.Api.Models;
using Gva.Api.ModelsDO.Persons;
using System.Collections.Generic;

namespace Gva.Api.Models.Views.Person
{
    public partial class GvaViewPersonRating
    {

        public int LotId { get; set; }

        public int PartId { get; set; }

        public int PartIndex { get; set; }

        public int? RatingTypeId { get; set; }

        public int? RatingLevelId { get; set; }

        public int? RatingClassId { get; set; }

        public int? AircraftTypeGroupId { get; set; }

        public int? AuthorizationId { get; set; }

        public int? LocationIndicatorId { get; set; }

        public int? AircraftTypeCategoryId { get; set; }

        public int? CaaId { get; set; }

        public string Sector { get; set; }

        public virtual NomValue LocationIndicator { get; set; }

        public virtual NomValue RatingType { get; set; }

        public virtual NomValue RatingLevel { get; set; }

        public virtual NomValue RatingClass { get; set; }

        public virtual NomValue AircraftTypeGroup { get; set; }

        public virtual NomValue Authorization { get; set; }

        public virtual NomValue AircraftTypeCategory { get; set; }

        public virtual NomValue Caa { get; set; }

        public virtual GvaViewPerson Person { get; set; }

        public virtual Part Part { get; set; }

        public virtual List<GvaViewPersonRatingEdition> Editions { get; set; }

    }

    public class GvaViewPersonRatingMap : EntityTypeConfiguration<GvaViewPersonRating>
    {
        public GvaViewPersonRatingMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.PartIndex });

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PartId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Sector)
                .IsOptional()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("GvaViewPersonRatings");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartId).HasColumnName("LotPartId");
            this.Property(t => t.PartIndex).HasColumnName("PartIndex");
            this.Property(t => t.RatingTypeId).HasColumnName("RatingTypeId");
            this.Property(t => t.RatingLevelId).HasColumnName("RatingLevelId");
            this.Property(t => t.RatingClassId).HasColumnName("RatingClassId");
            this.Property(t => t.AircraftTypeGroupId).HasColumnName("AircraftTypeGroupId");
            this.Property(t => t.AuthorizationId).HasColumnName("AuthorizationId");
            this.Property(t => t.LocationIndicatorId).HasColumnName("LocationIndicatorId");
            this.Property(t => t.AircraftTypeCategoryId).HasColumnName("AircraftTypeCategoryId");
            this.Property(t => t.CaaId).HasColumnName("CaaId");
            this.Property(t => t.Sector).HasColumnName("Sector");

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
            this.HasOptional(t => t.LocationIndicator)
                .WithMany()
                .HasForeignKey(t => t.LocationIndicatorId);
            this.HasOptional(t => t.Caa)
                .WithMany()
                .HasForeignKey(t => t.CaaId);
            this.HasOptional(t => t.AircraftTypeCategory)
                .WithMany()
                .HasForeignKey(t => t.AircraftTypeCategoryId);

        }
    }
}
