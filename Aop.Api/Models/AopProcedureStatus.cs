using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Aop.Api.Models
{
    public partial class AopProcedureStatus
    {
        public int AopProcedureStatusId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
    }

    public class AopProcedureStatusMap : EntityTypeConfiguration<AopProcedureStatus>
    {
        public AopProcedureStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.AopProcedureStatusId);

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
            this.ToTable("AopProcedureStatuses");
            this.Property(t => t.AopProcedureStatusId).HasColumnName("AopProcedureStatusId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
