using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocDestinationType
    {
        public DocDestinationType()
        {
            this.Docs = new List<Doc>();
        }

        public int DocDestinationTypeId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public bool IsActive { get; set; }

        public byte[] Version { get; set; }

        public virtual ICollection<Doc> Docs { get; set; }
    }

    public class DocDestinationTypeMap : EntityTypeConfiguration<DocDestinationType>
    {
        public DocDestinationTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.DocDestinationTypeId);

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
            this.ToTable("DocDestinationTypes");
            this.Property(t => t.DocDestinationTypeId).HasColumnName("DocDestinationTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
