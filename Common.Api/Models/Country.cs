using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Common.Api.Models
{
    public partial class Country
    {
        public Country()
        {
        }

        public int CountryId { get; set; }
        public string Name { get; set; }
        public string NumericCode { get; set; }
        public string Alpha3Code { get; set; }
        public string Alpha2Code { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
    }

    public class CountryMap : EntityTypeConfiguration<Country>
    {
        public CountryMap()
        {
            // Primary Key
            this.HasKey(t => t.CountryId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.NumericCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Alpha3Code)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Alpha2Code)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Alias)
                .HasMaxLength(200);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Countries");
            this.Property(t => t.CountryId).HasColumnName("CountryId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NumericCode).HasColumnName("NumericCode");
            this.Property(t => t.Alpha3Code).HasColumnName("Alpha3Code");
            this.Property(t => t.Alpha2Code).HasColumnName("Alpha2Code");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
