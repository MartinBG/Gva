using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Gva.Api.Models.Views.Aircraft;
using Regs.Api.LotEvents;

namespace Gva.Api.Models.Views.Aircarft
{
    public partial class GvaViewAircraftPrintedCertNoise : IProjectionView
    {
        public int LotId { get; set; }

        public int NoisePartIndex { get; set; }

        public int PrintedFileId { get; set; }

        public virtual GvaViewAircraft Aircraft { get; set; }
    }

    public class GvaViewAircraftPrintedCertNoiseMap : EntityTypeConfiguration<GvaViewAircraftPrintedCertNoise>
    {
        public GvaViewAircraftPrintedCertNoiseMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.NoisePartIndex });

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NoisePartIndex)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("GvaViewAircraftPrintedCertNoises");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.NoisePartIndex).HasColumnName("NoisePartIndex");
            this.Property(t => t.PrintedFileId).HasColumnName("PrintedFileId");

            this.HasRequired(t => t.Aircraft)
                .WithMany()
                .HasForeignKey(d => d.LotId);
        }
    }
}
