using Common.Api.Models;
using Regs.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gva.Api.Models
{
    public partial class    GvaViewAircraft
    {
        public int LotId { get; set; }

        public string ManSN { get; set; }

        public string Model { get; set; }

        public string ModelAlt { get; set; }

        public string ICAO { get; set; }

        public int? AirCategoryId { get; set; }

        public int? AircraftProducerId { get; set; }

        public string Engine { get; set; }

        public string Propeller { get; set; }

        public string ModifOrWingColor { get; set; }

        public string Mark { get; set; }

        public DateTime? OutputDate { get; set; }

        public virtual Lot Lot { get; set; }

        public virtual NomValue AirCategory { get; set; }

        public virtual NomValue AircraftProducer { get; set; }
    }

    public class GvaViewAircraftMap : EntityTypeConfiguration<GvaViewAircraft>
    {
        public GvaViewAircraftMap()
        {
            // Primary Key
            this.HasKey(t => t.LotId);

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ManSN)
                .IsRequired()
                .HasMaxLength(50);


            // Table & Column Mappings
            this.ToTable("GvaViewAircrafts");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.ManSN).HasColumnName("ManSN");
            this.Property(t => t.Model).HasColumnName("Model");
            this.Property(t => t.ModelAlt).HasColumnName("ModelAlt");
            this.Property(t => t.OutputDate).HasColumnName("OutputDate");
            this.Property(t => t.ICAO).HasColumnName("ICAO");
            this.Property(t => t.AirCategoryId).HasColumnName("AirCategoryId");
            this.Property(t => t.AircraftProducerId).HasColumnName("AircraftProducerId");
            this.Property(t => t.Engine).HasColumnName("Engine");
            this.Property(t => t.Propeller).HasColumnName("Propeller");
            this.Property(t => t.ModifOrWingColor).HasColumnName("ModifOrWingColor");
            this.Property(t => t.Mark).HasColumnName("Mark");

            // Relationships
            this.HasRequired(t => t.Lot)
                .WithOptional();
            this.HasOptional(t => t.AirCategory)
                .WithMany()
                .HasForeignKey(t => t.AirCategoryId);
            this.HasOptional(t => t.AircraftProducer)
                .WithMany()
                .HasForeignKey(t => t.AircraftProducerId);
        }
    }
}
