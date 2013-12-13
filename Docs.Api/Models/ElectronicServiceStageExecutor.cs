using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class ElectronicServiceStageExecutor
    {
        public int ElectronicServiceStageExecutorId { get; set; }
        public int ElectronicServiceStageId { get; set; }
        public int UnitId { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual ElectronicServiceStage ElectronicServiceStage { get; set; }
        public virtual Unit Unit { get; set; }
    }

    public class ElectronicServiceStageExecutorMap : EntityTypeConfiguration<ElectronicServiceStageExecutor>
    {
        public ElectronicServiceStageExecutorMap()
        {
            // Primary Key
            this.HasKey(t => t.ElectronicServiceStageExecutorId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ElectronicServiceStageExecutors");
            this.Property(t => t.ElectronicServiceStageExecutorId).HasColumnName("ElectronicServiceStageExecutorId");
            this.Property(t => t.ElectronicServiceStageId).HasColumnName("ElectronicServiceStageId");
            this.Property(t => t.UnitId).HasColumnName("UnitId");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.ElectronicServiceStage)
                .WithMany(t => t.ElectronicServiceStageExecutors)
                .HasForeignKey(d => d.ElectronicServiceStageId);
            this.HasRequired(t => t.Unit)
                .WithMany(t => t.ElectronicServiceStageExecutors)
                .HasForeignKey(d => d.UnitId);

        }
    }
}
