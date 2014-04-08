using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;

namespace Gva.Api.Models
{
    public partial class GvaCaseType
    {
        public GvaCaseType()
        {
            this.GvaLotCases = new List<GvaLotCase>();
            this.GvaLotFiles = new List<GvaLotFile>();
        }

        public int GvaCaseTypeId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public int? LotSetId { get; set; }

        public virtual Set LotSet { get; set; }

        public virtual ICollection<GvaLotCase> GvaLotCases { get; set; }

        public virtual ICollection<GvaLotFile> GvaLotFiles { get; set; }

    }
    
    public class GvaCaseTypeMap : EntityTypeConfiguration<GvaCaseType>
    {
        public GvaCaseTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.GvaCaseTypeId);

            // Properties
            this.Property(t => t.GvaCaseTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .HasMaxLength(200);

            this.Property(t => t.Name)
                .HasMaxLength(200);

            this.Property(t => t.Alias)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("GvaCaseTypes");
            this.Property(t => t.GvaCaseTypeId).HasColumnName("GvaCaseTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.LotSetId).HasColumnName("LotSetId");

            // Relationships
            this.HasOptional(t => t.LotSet)
                .WithMany()
                .HasForeignKey(d => d.LotSetId);
        }
    }
}
