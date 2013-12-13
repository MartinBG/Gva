using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Common.Api.Models
{
    public partial class Settlement
    {
        public Settlement()
        {
        }

        public int SettlementId { get; set; }
        public int MunicipalityId { get; set; }
        public int DistrictId { get; set; }
        public string Code { get; set; }
        public string MunicipalityCode { get; set; }
        public string DistrictCode { get; set; }
        public string MunicipalityCode2 { get; set; }
        public string DistrictCode2 { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public string SettlementName { get; set; }
        public string TypeCode { get; set; }
        public string MayoraltyCode { get; set; }
        public string Category { get; set; }
        public string Altitude { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public bool IsDistrict { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual District District { get; set; }
        public virtual Municipality Municipality { get; set; }
    }

    public class SettlementMap : EntityTypeConfiguration<Settlement>
    {
        public SettlementMap()
        {
            // Primary Key
            this.HasKey(t => t.SettlementId);

            // Properties
            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.MunicipalityCode)
                .HasMaxLength(10);

            this.Property(t => t.DistrictCode)
                .HasMaxLength(10);

            this.Property(t => t.MunicipalityCode2)
                .HasMaxLength(10);

            this.Property(t => t.DistrictCode2)
                .HasMaxLength(10);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.TypeName)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.SettlementName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.TypeCode)
                .HasMaxLength(50);

            this.Property(t => t.MayoraltyCode)
                .HasMaxLength(50);

            this.Property(t => t.Category)
                .HasMaxLength(50);

            this.Property(t => t.Altitude)
                .HasMaxLength(50);

            this.Property(t => t.Alias)
                .HasMaxLength(200);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Settlements");
            this.Property(t => t.SettlementId).HasColumnName("SettlementId");
            this.Property(t => t.MunicipalityId).HasColumnName("MunicipalityId");
            this.Property(t => t.DistrictId).HasColumnName("DistrictId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.MunicipalityCode).HasColumnName("MunicipalityCode");
            this.Property(t => t.DistrictCode).HasColumnName("DistrictCode");
            this.Property(t => t.MunicipalityCode2).HasColumnName("MunicipalityCode2");
            this.Property(t => t.DistrictCode2).HasColumnName("DistrictCode2");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.TypeName).HasColumnName("TypeName");
            this.Property(t => t.SettlementName).HasColumnName("SettlementName");
            this.Property(t => t.TypeCode).HasColumnName("TypeCode");
            this.Property(t => t.MayoraltyCode).HasColumnName("MayoraltyCode");
            this.Property(t => t.Category).HasColumnName("Category");
            this.Property(t => t.Altitude).HasColumnName("Altitude");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsDistrict).HasColumnName("IsDistrict");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.District)
                .WithMany(t => t.Settlements)
                .HasForeignKey(d => d.DistrictId);
            this.HasRequired(t => t.Municipality)
                .WithMany(t => t.Settlements)
                .HasForeignKey(d => d.MunicipalityId);

        }
    }
}
