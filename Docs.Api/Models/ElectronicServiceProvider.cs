using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class ElectronicServiceProvider
    {
        public int ElectronicServiceProviderId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Bulstat { get; set; }

        public string Alias { get; set; }

        public string EndPointAddress { get; set; }

        public bool IsActive { get; set; }

        public byte[] Version { get; set; }
    }

    public class ElectronicServiceProviderMap : EntityTypeConfiguration<ElectronicServiceProvider>
    {
        public ElectronicServiceProviderMap()
        {
            // Primary Key
            this.HasKey(t => t.ElectronicServiceProviderId);

            // Properties
            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Bulstat)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Alias)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.EndPointAddress)
                .HasMaxLength(500);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ElectronicServiceProviders");
            this.Property(t => t.ElectronicServiceProviderId).HasColumnName("ElectronicServiceProviderId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Bulstat).HasColumnName("Bulstat");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.EndPointAddress).HasColumnName("EndPointAddress");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
