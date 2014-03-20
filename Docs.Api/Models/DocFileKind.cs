using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocFileKind
    {
        public DocFileKind()
        {
            this.DocFiles = new List<DocFile>();
        }

        public int DocFileKindId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public bool IsActive { get; set; }

        public byte[] Version { get; set; }

        public virtual ICollection<DocFile> DocFiles { get; set; }
    }

    public class DocFileKindMap : EntityTypeConfiguration<DocFileKind>
    {
        public DocFileKindMap()
        {
            // Primary Key
            this.HasKey(t => t.DocFileKindId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Alias)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DocFileKinds");
            this.Property(t => t.DocFileKindId).HasColumnName("DocFileKindId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
