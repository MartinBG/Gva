using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;

namespace Gva.Api.Models.Views.Aircraft
{
    public partial class GvaViewAircraftCertAirworthiness
    {
        public int LotId { get; set; }

        public int PartIndex { get; set; }

        public int CertificateTypeId { get; set; }

        public int? PrintedFileId { get; set; }

        public virtual NomValue AirworthinessCertificateType { get; set; }

        public virtual GvaViewAircraft Aircraft { get; set; }
    }

    public class GvaViewAircraftCertAirworthinessMap : EntityTypeConfiguration<GvaViewAircraftCertAirworthiness>
    {
        public GvaViewAircraftCertAirworthinessMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.PartIndex });

            // Table & Column Mappings
            this.ToTable("GvaViewAircraftCertAirworthinesses");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartIndex).HasColumnName("PartIndex");
            this.Property(t => t.CertificateTypeId).HasColumnName("CertificateTypeId");
            this.Property(t => t.PrintedFileId).HasColumnName("PrintedFileId");

            // Relationships
            this.HasRequired(t => t.Aircraft)
                .WithMany()
                .HasForeignKey(t => t.LotId);

            this.HasRequired(t => t.AirworthinessCertificateType)
                .WithMany()
                .HasForeignKey(t => t.CertificateTypeId);
        }
    }
}
