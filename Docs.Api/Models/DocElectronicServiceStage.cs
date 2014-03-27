using System;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocElectronicServiceStage
    {
        public int DocElectronicServiceStageId { get; set; }

        public int DocId { get; set; }

        public int ElectronicServiceStageId { get; set; }

        public DateTime StartingDate { get; set; }

        public DateTime? ExpectedEndingDate { get; set; }

        public DateTime? EndingDate { get; set; }

        public bool IsCurrentStage { get; set; }

        public byte[] Version { get; set; }

        public virtual Doc Doc { get; set; }

        public virtual ElectronicServiceStage ElectronicServiceStage { get; set; }
    }

    public class DocElectronicServiceStageMap : EntityTypeConfiguration<DocElectronicServiceStage>
    {
        public DocElectronicServiceStageMap()
        {
            // Primary Key
            this.HasKey(t => t.DocElectronicServiceStageId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DocElectronicServiceStages");
            this.Property(t => t.DocElectronicServiceStageId).HasColumnName("DocElectronicServiceStageId");
            this.Property(t => t.DocId).HasColumnName("DocId");
            this.Property(t => t.ElectronicServiceStageId).HasColumnName("ElectronicServiceStageId");
            this.Property(t => t.StartingDate).HasColumnName("StartingDate");
            this.Property(t => t.ExpectedEndingDate).HasColumnName("ExpectedEndingDate");
            this.Property(t => t.EndingDate).HasColumnName("EndingDate");
            this.Property(t => t.IsCurrentStage).HasColumnName("IsCurrentStage");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Doc)
                .WithMany(t => t.DocElectronicServiceStages)
                .HasForeignKey(d => d.DocId)
                .WillCascadeOnDelete();
            this.HasRequired(t => t.ElectronicServiceStage)
                .WithMany(t => t.DocElectronicServiceStages)
                .HasForeignKey(d => d.ElectronicServiceStageId);
        }
    }
}
