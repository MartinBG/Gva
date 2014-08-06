using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Text.RegularExpressions;
using Regs.Api.Models;

namespace Gva.Api.Models
{
    public partial class GvaLotFile
    {
        public GvaLotFile()
        {
            this.GvaAppLotFiles = new List<GvaAppLotFile>();
        }

        public int GvaLotFileId { get; set; }

        public int LotPartId { get; set; }

        public int? GvaFileId { get; set; }

        public int? DocFileId { get; set; }

        public int GvaCaseTypeId { get; set; }

        public string PageIndex { get; set; }

        public int? PageNumber { get; set; }

        public virtual Docs.Api.Models.DocFile DocFile { get; set; }

        public virtual GvaCaseType GvaCaseType { get; set; }

        public virtual ICollection<GvaAppLotFile> GvaAppLotFiles { get; set; }

        public virtual GvaFile GvaFile { get; set; }

        public virtual Part LotPart { get; set; }
    }

    public class GvaLotFileMap : EntityTypeConfiguration<GvaLotFile>
    {
        public GvaLotFileMap()
        {
            // Primary Key
            this.HasKey(t => t.GvaLotFileId);

            // Properties
            this.Property(t => t.PageIndex)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GvaLotFiles");
            this.Property(t => t.GvaLotFileId).HasColumnName("GvaLotFileId");
            this.Property(t => t.LotPartId).HasColumnName("LotPartId");
            this.Property(t => t.GvaFileId).HasColumnName("GvaFileId");
            this.Property(t => t.DocFileId).HasColumnName("DocFileId");
            this.Property(t => t.GvaCaseTypeId).HasColumnName("GvaCaseTypeId");
            this.Property(t => t.PageIndex).HasColumnName("PageIndex");
            this.Property(t => t.PageNumber).HasColumnName("PageNumber");

            // Relationships
            this.HasOptional(t => t.DocFile)
                .WithMany()
                .HasForeignKey(d => d.DocFileId);
            this.HasOptional(t => t.GvaFile)
                .WithMany(t => t.GvaLotFiles)
                .HasForeignKey(d => d.GvaFileId);
            this.HasRequired(t => t.LotPart)
                .WithMany()
                .HasForeignKey(d => d.LotPartId);
            this.HasRequired(t => t.GvaCaseType)
                .WithMany(t => t.GvaLotFiles)
                .HasForeignKey(d => d.GvaCaseTypeId);
        }
    }
}
