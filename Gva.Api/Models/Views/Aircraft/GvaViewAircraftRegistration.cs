using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;
using Regs.Api.LotEvents;

namespace Gva.Api.Models.Views.Aircraft
{
    public partial class GvaViewAircraftRegistration : IProjectionView
    {
        public int LotId { get; set; }

        public int PartIndex { get; set; }

        public int CertRegisterId { get; set; }

        public int? CertNumber { get; set; }

        public int? ActNumber { get; set; }

        public int? PrintedExportCertFileId { get; set; }

        public int? PrintedRegCertFileId { get; set; }

        public string RegMark { get; set; }

        public virtual GvaViewAircraft Aircraft { get; set; }

        public virtual NomValue Register { get; set; }
    }

    public class GvaViewAircraftRegistrationMap : EntityTypeConfiguration<GvaViewAircraftRegistration>
    {
        public GvaViewAircraftRegistrationMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.PartIndex });

            // Table & Column Mappings
            this.ToTable("GvaViewAircraftRegistrations");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartIndex).HasColumnName("PartIndex");
            this.Property(t => t.CertRegisterId).HasColumnName("CertRegisterId");
            this.Property(t => t.CertNumber).HasColumnName("CertNumber");
            this.Property(t => t.ActNumber).HasColumnName("ActNumber");
            this.Property(t => t.RegMark).HasColumnName("RegMark");
            this.Property(t => t.PrintedExportCertFileId).HasColumnName("PrintedExportCertFileId");
            this.Property(t => t.PrintedRegCertFileId).HasColumnName("PrintedRegCertFileId");

            // Relationships
            this.HasRequired(t => t.Aircraft)
                .WithMany()
                .HasForeignKey(t => t.LotId);

            this.HasRequired(t => t.Register)
                .WithMany()
                .HasForeignKey(t => t.CertRegisterId);
        }
    }
}
