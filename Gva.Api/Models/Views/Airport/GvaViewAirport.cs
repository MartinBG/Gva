using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;
using Regs.Api.Models;
using Regs.Api.LotEvents;

namespace Gva.Api.Models.Views.Airport
{
    public partial class GvaViewAirport : IProjectionView
    {
        public int LotId { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string Place { get; set; }

        public int AirportTypeId { get; set; }

        public string ICAO { get; set; }

        public string Runway { get; set; }

        public string Course { get; set; }

        public string Excess { get; set; }

        public string Concrete { get; set; }

        public virtual Lot Lot { get; set; }

        public virtual NomValue AirportType { get; set; }
    }

    public class GvaViewAirportMap : EntityTypeConfiguration<GvaViewAirport>
    {
        public GvaViewAirportMap()
        {
            // Primary Key
            this.HasKey(t => t.LotId);

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.NameAlt)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GvaViewAirports");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.Place).HasColumnName("Place");
            this.Property(t => t.AirportTypeId).HasColumnName("AirportTypeId");
            this.Property(t => t.ICAO).HasColumnName("ICAO");
            this.Property(t => t.Runway).HasColumnName("Runway");
            this.Property(t => t.Course).HasColumnName("Course");
            this.Property(t => t.Excess).HasColumnName("Excess");
            this.Property(t => t.Concrete).HasColumnName("Concrete");

            // Relationships
            this.HasRequired(t => t.Lot)
                .WithOptional();
            this.HasRequired(t => t.AirportType)
                .WithMany()
                .HasForeignKey(t => t.AirportTypeId);
        }
    }
}
