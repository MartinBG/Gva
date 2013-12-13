using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Gva.Api.Models
{
    public partial class GvaLotFile
    {
        public GvaLotFile()
        {
            this.GvaAppLotFiles = new List<GvaAppLotFile>();
        }

        public int GvaLotFileId { get; set; }
        public Nullable<int> LotPartId { get; set; }
        public Nullable<int> GvaFileId { get; set; }
        public Nullable<int> DocFileId { get; set; }
        public Nullable<int> GvaLotFileTypeId { get; set; }
        public string PageIndex { get; set; }
        public string PageNumber { get; set; }
        public bool IsActive { get; set; }
        public virtual Docs.Api.Models.DocFile DocFile { get; set; }
        public virtual ICollection<GvaAppLotFile> GvaAppLotFiles { get; set; }
        public virtual GvaFile GvaFile { get; set; }
        public virtual GvaLotFileType GvaLotFileType { get; set; }
        public virtual Regs.Api.Models.Part LotPart { get; set; }
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

            this.Property(t => t.PageNumber)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GvaLotFiles");
            this.Property(t => t.GvaLotFileId).HasColumnName("GvaLotFileId");
            this.Property(t => t.LotPartId).HasColumnName("LotPartId");
            this.Property(t => t.GvaFileId).HasColumnName("GvaFileId");
            this.Property(t => t.DocFileId).HasColumnName("DocFileId");
            this.Property(t => t.GvaLotFileTypeId).HasColumnName("GvaLotFileTypeId");
            this.Property(t => t.PageIndex).HasColumnName("PageIndex");
            this.Property(t => t.PageNumber).HasColumnName("PageNumber");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasOptional(t => t.DocFile)
                .WithMany()
                .HasForeignKey(d => d.DocFileId);
            this.HasOptional(t => t.GvaFile)
                .WithMany(t => t.GvaLotFiles)
                .HasForeignKey(d => d.GvaFileId);
            this.HasOptional(t => t.GvaLotFileType)
                .WithMany(t => t.GvaLotFiles)
                .HasForeignKey(d => d.GvaLotFileTypeId);
            this.HasOptional(t => t.LotPart)
                .WithMany()
                .HasForeignKey(d => d.LotPartId);

        }
    }
}
