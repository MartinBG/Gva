using Regs.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Mosv.Api.Models
{
    public partial class MosvElectronicServiceProvider
    {
        public int MosvElectronicServiceProviderId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Alias { get; set; }

        public bool IsActive { get; set; }

        public byte[] Version { get; set; }
    }

    public class MosvElectronicServiceProviderMap : EntityTypeConfiguration<MosvElectronicServiceProvider>
    {
        public MosvElectronicServiceProviderMap()
        {
            // Primary Key
            this.HasKey(t => t.MosvElectronicServiceProviderId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Alias)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("MosvElectronicServiceProviders");
            this.Property(t => t.MosvElectronicServiceProviderId).HasColumnName("MosvElectronicServiceProviderId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
