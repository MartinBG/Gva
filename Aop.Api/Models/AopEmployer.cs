using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Aop.Api.Models
{
    public partial class AopEmployer
    {
        public int AopEmployerId { get; set; }
        public string Name { get; set; }
        public string LotNum { get; set; }
        public int AopEmployerTypeId { get; set; }
        public byte[] Version { get; set; }
        public virtual AopEmployerType AopEmployerType { get; set; }
    }

    public class AopEmployerMap : EntityTypeConfiguration<AopEmployer>
    {
        public AopEmployerMap()
        {
            // Primary Key
            this.HasKey(t => t.AopEmployerId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.LotNum)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("AopEmployers");
            this.Property(t => t.AopEmployerId).HasColumnName("AopEmployerId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.LotNum).HasColumnName("LotNum");
            this.Property(t => t.AopEmployerTypeId).HasColumnName("AopEmployerTypeId");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.AopEmployerType)
                .WithMany(t => t.AopEmployers)
                .HasForeignKey(d => d.AopEmployerTypeId);

        }
    }
}