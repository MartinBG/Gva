using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;

namespace Gva.Api.Models
{
    public partial class GvaViewAircraftRegistration
    {
        public int LotPartId { get; set; }

        public int LotId { get; set; }

        public int CertRegisterId { get; set; }

        public int CertNumber { get; set; }

        public int ActNumber { get; set; }

        public string RegMark { get; set; }

        public virtual Lot Lot { get; set; }

        public virtual Part Part { get; set; }
    }

    public class GvaViewAircraftRegistrationMap : EntityTypeConfiguration<GvaViewAircraftRegistration>
    {
        public GvaViewAircraftRegistrationMap()
        {
            // Primary Key
            this.HasKey(t => t.LotPartId);

            // Properties
            this.Property(t => t.LotPartId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("GvaViewAircraftRegistrations");
            this.Property(t => t.LotPartId).HasColumnName("LotPartId");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.CertRegisterId).HasColumnName("CertRegisterId");
            this.Property(t => t.CertNumber).HasColumnName("CertNumber");
            this.Property(t => t.ActNumber).HasColumnName("ActNumber");
            this.Property(t => t.RegMark).HasColumnName("RegMark");

            // Relationships
            this.HasRequired(t => t.Lot)
                .WithMany()
                .HasForeignKey(t => t.LotId);

            this.HasRequired(t => t.Part)
                .WithOptional();
        }
    }
}
