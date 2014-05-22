using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Aop.Api.Models
{
    public partial class AopApplicationType
    {
        public int AopApplicationTypeId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
    }

    public class AopApplicationTypeMap : EntityTypeConfiguration<AopApplicationType>
    {
        public AopApplicationTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.AopApplicationTypeId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Alias)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("AopApplicationTypes");
            this.Property(t => t.AopApplicationTypeId).HasColumnName("AopApplicationTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
