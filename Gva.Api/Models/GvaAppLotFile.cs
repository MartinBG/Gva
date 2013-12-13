using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gva.Api.Models
{
    public partial class GvaAppLotFile
    {
        public int GvaApplicationId { get; set; }
        public int GvaLotFileId { get; set; }
        public Nullable<int> DocFileId { get; set; }
        public virtual Docs.Api.Models.DocFile DocFile { get; set; }
        public virtual GvaApplication GvaApplication { get; set; }
        public virtual GvaLotFile GvaLotFile { get; set; }
    }

    public class GvaAppLotFileMap : EntityTypeConfiguration<GvaAppLotFile>
    {
        public GvaAppLotFileMap()
        {
            // Primary Key
            this.HasKey(t => new { t.GvaApplicationId, t.GvaLotFileId });

            // Properties
            this.Property(t => t.GvaApplicationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.GvaLotFileId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("GvaAppLotFiles");
            this.Property(t => t.GvaApplicationId).HasColumnName("GvaApplicationId");
            this.Property(t => t.GvaLotFileId).HasColumnName("GvaLotFileId");
            this.Property(t => t.DocFileId).HasColumnName("DocFileId");

            // Relationships
            this.HasOptional(t => t.DocFile)
                .WithMany()
                .HasForeignKey(d => d.DocFileId);
            this.HasRequired(t => t.GvaApplication)
                .WithMany(t => t.GvaAppLotFiles)
                .HasForeignKey(d => d.GvaApplicationId);
            this.HasRequired(t => t.GvaLotFile)
                .WithMany(t => t.GvaAppLotFiles)
                .HasForeignKey(d => d.GvaLotFileId);

        }
    }
}
