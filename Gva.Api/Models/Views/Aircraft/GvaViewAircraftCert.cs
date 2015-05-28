using Regs.Api.LotEvents;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gva.Api.Models.Views.Aircraft
{
    public partial class GvaViewAircraftCert : IProjectionView
    {
        public int LotId { get; set; }

        public int PartIndex { get; set; }

        public string DocumentNumber { get; set; }

        public string FormName { get; set; }

        public int? FormNumberPrefix { get; set; }

        public int? ParsedNumberWithoutPrefix { get; set; }

        public virtual GvaViewAircraft Aircraft { get; set; }
    }

    public class GvaViewAircraftCertMap : EntityTypeConfiguration<GvaViewAircraftCert>
    {
        public GvaViewAircraftCertMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.PartIndex });

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PartIndex)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DocumentNumber)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.FormName)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("GvaViewAircraftCerts");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartIndex).HasColumnName("PartIndex");
            this.Property(t => t.DocumentNumber).HasColumnName("DocumentNumber");
            this.Property(t => t.FormName).HasColumnName("FormName");
            this.Property(t => t.FormNumberPrefix).HasColumnName("FormNumberPrefix");
            this.Property(t => t.ParsedNumberWithoutPrefix).HasColumnName("ParsedNumberWithoutPrefix");

            // Relationships
            this.HasRequired(t => t.Aircraft)
                .WithMany()
                .HasForeignKey(t => t.LotId);
        }
    }
}
