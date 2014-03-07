using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Gva.Api.Models
{
    public partial class GvaFile
    {
        public GvaFile()
        {
            this.GvaLotFiles = new List<GvaLotFile>();
        }

        public int GvaFileId { get; set; }

        public string Filename { get; set; }

        public Guid FileContentId { get; set; }

        public virtual ICollection<GvaLotFile> GvaLotFiles { get; set; }
    }

    public class GvaFileMap : EntityTypeConfiguration<GvaFile>
    {
        public GvaFileMap()
        {
            // Primary Key
            this.HasKey(t => t.GvaFileId);

            // Properties
            this.Property(t => t.Filename)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GvaFiles");
            this.Property(t => t.GvaFileId).HasColumnName("GvaFileId");
            this.Property(t => t.Filename).HasColumnName("Filename");
            this.Property(t => t.FileContentId).HasColumnName("FileContentId");
        }
    }
}
