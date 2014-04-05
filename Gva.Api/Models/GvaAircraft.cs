using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;
using System;

namespace Gva.Api.Models
{
    public partial class GvaAircraft
    {
        public int GvaAircraftLotId { get; set; }

        public string ManSN { get; set; }

        public string Model { get; set; }

        public string ModelAlt { get; set; }

        public string ICAO { get; set; }

        public string AircraftCategory { get; set; }

        public string AircraftProducer { get; set; }

        public string Engine { get; set; }

        public string Propeller { get; set; }

        public string ModifOrWingColor { get; set; }

        public DateTime? OutputDate { get; set; }

        public virtual Lot Lot { get; set; }
    }

    public class GvaAircraftMap : EntityTypeConfiguration<GvaAircraft>
    {
        public GvaAircraftMap()
        {
            // Primary Key
            this.HasKey(t => t.GvaAircraftLotId);

            // Properties
            this.Property(t => t.GvaAircraftLotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ManSN)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ICAO)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("GvaAircrafts");
            this.Property(t => t.GvaAircraftLotId).HasColumnName("GvaAircraftLotId");
            this.Property(t => t.ManSN).HasColumnName("ManSN");
            this.Property(t => t.Model).HasColumnName("Model");
            this.Property(t => t.ModelAlt).HasColumnName("ModelAlt");
            this.Property(t => t.OutputDate).HasColumnName("OutputDate");
            this.Property(t => t.ICAO).HasColumnName("ICAO");
            this.Property(t => t.AircraftCategory).HasColumnName("AircraftCategory");
            this.Property(t => t.AircraftProducer).HasColumnName("AircraftProducer");
            this.Property(t => t.Engine).HasColumnName("Engine");
            this.Property(t => t.Propeller).HasColumnName("Propeller");
            this.Property(t => t.ModifOrWingColor).HasColumnName("ModifOrWingColor");

            // Relationships
            this.HasRequired(t => t.Lot)
                .WithOptional();
        }
    }
}
