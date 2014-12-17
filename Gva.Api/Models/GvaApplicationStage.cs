using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;
using Gva.Api.Models.Views.Person;

namespace Gva.Api.Models
{
    public partial class GvaApplicationStage
    {
        public int GvaAppStageId { get; set; }

        public int GvaApplicationId { get; set; }

        public int GvaStageId { get; set; }

        public DateTime StartingDate { get; set; }

        public int? InspectorLotId { get; set; }

        public int Ordinal { get; set; }

        public string Note { get; set; }

        public DateTime? StageTermDate { get; set; }

        public virtual GvaApplication GvaApplication { get; set; }

        public virtual GvaStage GvaStage { get; set; }

        public virtual GvaViewPersonInspector Inspector { get; set; }
    }

    public class GvaApplicationStageMap : EntityTypeConfiguration<GvaApplicationStage>
    {
        public GvaApplicationStageMap()
        {
            // Primary Key
            this.HasKey(t => t.GvaAppStageId);

            // Properties
            this.Property(t => t.GvaAppStageId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("GvaAppStages");
            this.Property(t => t.GvaAppStageId).HasColumnName("GvaAppStageId");
            this.Property(t => t.GvaApplicationId).HasColumnName("GvaApplicationId");
            this.Property(t => t.GvaStageId).HasColumnName("GvaStageId");
            this.Property(t => t.StartingDate).HasColumnName("StartingDate");
            this.Property(t => t.StageTermDate).HasColumnName("StageTermDate");
            this.Property(t => t.InspectorLotId).HasColumnName("InspectorLotId");
            this.Property(t => t.Ordinal).HasColumnName("Ordinal");
            this.Property(t => t.Note).HasColumnName("Note");

            // Relationships ??
            this.HasRequired(t => t.GvaApplication)
                .WithMany()
                .HasForeignKey(d => d.GvaApplicationId);

            this.HasRequired(t => t.GvaStage)
                .WithMany()
                .HasForeignKey(d => d.GvaStageId);

            this.HasOptional(t => t.Inspector)
                .WithMany()
                .HasForeignKey(d => d.InspectorLotId);
        }
    }
}
