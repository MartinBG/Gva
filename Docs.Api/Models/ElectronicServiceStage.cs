using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class ElectronicServiceStage
    {
        public ElectronicServiceStage()
        {
            this.DocElectronicServiceStages = new List<DocElectronicServiceStage>();
            this.ElectronicServiceStageExecutors = new List<ElectronicServiceStageExecutor>();
        }

        public int ElectronicServiceStageId { get; set; }

        public int DocTypeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Alias { get; set; }

        public int? Duration { get; set; }

        public bool IsDurationReset { get; set; }

        public bool IsFirstByDefault { get; set; }

        public bool IsLastStage { get; set; }

        public bool IsActive { get; set; }

        public byte[] Version { get; set; }

        public virtual ICollection<DocElectronicServiceStage> DocElectronicServiceStages { get; set; }

        public virtual DocType DocType { get; set; }

        public virtual ICollection<ElectronicServiceStageExecutor> ElectronicServiceStageExecutors { get; set; }
    }

    public class ElectronicServiceStageMap : EntityTypeConfiguration<ElectronicServiceStage>
    {
        public ElectronicServiceStageMap()
        {
            // Primary Key
            this.HasKey(t => t.ElectronicServiceStageId);

            // Properties
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
            this.ToTable("ElectronicServiceStages");
            this.Property(t => t.ElectronicServiceStageId).HasColumnName("ElectronicServiceStageId");
            this.Property(t => t.DocTypeId).HasColumnName("DocTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.Duration).HasColumnName("Duration");
            this.Property(t => t.IsDurationReset).HasColumnName("IsDurationReset");
            this.Property(t => t.IsFirstByDefault).HasColumnName("IsFirstByDefault");
            this.Property(t => t.IsLastStage).HasColumnName("IsLastStage");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.DocType)
                .WithMany(t => t.ElectronicServiceStages)
                .HasForeignKey(d => d.DocTypeId);
        }
    }
}
