using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Common.Api.Models
{
    public partial class Municipality
    {
        public Municipality()
        {
            this.Settlements = new List<Settlement>();
        }

        public int MunicipalityId { get; set; }

        public int DistrictId { get; set; }

        public string Code { get; set; }

        public string Code2 { get; set; }

        public string MainSettlementCode { get; set; }

        public string Category { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public byte[] Version { get; set; }

        public virtual District District { get; set; }

        public virtual ICollection<Settlement> Settlements { get; set; }
    }

    public class MunicipalityMap : EntityTypeConfiguration<Municipality>
    {
        public MunicipalityMap()
        {
            // Primary Key
            this.HasKey(t => t.MunicipalityId);

            // Properties
            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Code2)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.MainSettlementCode)
                .HasMaxLength(10);

            this.Property(t => t.Category)
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Alias)
                .HasMaxLength(200);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Municipalities");
            this.Property(t => t.MunicipalityId).HasColumnName("MunicipalityId");
            this.Property(t => t.DistrictId).HasColumnName("DistrictId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Code2).HasColumnName("Code2");
            this.Property(t => t.MainSettlementCode).HasColumnName("MainSettlementCode");
            this.Property(t => t.Category).HasColumnName("Category");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.District)
                .WithMany(t => t.Municipalities)
                .HasForeignKey(d => d.DistrictId);
        }
    }
}
