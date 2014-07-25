using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;

namespace Gva.Api.Models
{
    public partial class GvaStage
    {
        public int GvaStageId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

    }

    public class GvaStageMap : EntityTypeConfiguration<GvaStage>
    {
        public GvaStageMap()
        {
            // Primary Key
            this.HasKey(t => t.GvaStageId);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(200);

            this.Property(t => t.Alias)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("GvaStages");
            this.Property(t => t.GvaStageId).HasColumnName("GvaStageId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
        }
    }
}
