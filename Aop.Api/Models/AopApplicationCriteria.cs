using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Aop.Api.Models
{
    public partial class AopApplicationCriteria
    {
        public int AopApplicationCriteriaId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
    }

    public class AopApplicationCriteriaMap : EntityTypeConfiguration<AopApplicationCriteria>
    {
        public AopApplicationCriteriaMap()
        {
            // Primary Key
            this.HasKey(t => t.AopApplicationCriteriaId);

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
            this.ToTable("AopApplicationCriteria");
            this.Property(t => t.AopApplicationCriteriaId).HasColumnName("AopApplicationCriteriaId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
