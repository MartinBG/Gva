using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocDirection
    {
        public DocDirection()
        {
            this.Docs = new List<Doc>();
            this.DocTypeClassifications = new List<DocTypeClassification>();
            this.DocTypeUnitRoles = new List<DocTypeUnitRole>();
        }

        public int DocDirectionId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public bool IsActive { get; set; }

        public byte[] Version { get; set; }

        public virtual ICollection<Doc> Docs { get; set; }

        public virtual ICollection<DocTypeClassification> DocTypeClassifications { get; set; }

        public virtual ICollection<DocTypeUnitRole> DocTypeUnitRoles { get; set; }
    }

    public class DocDirectionMap : EntityTypeConfiguration<DocDirection>
    {
        public DocDirectionMap()
        {
            // Primary Key
            this.HasKey(t => t.DocDirectionId);

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
            this.ToTable("DocDirections");
            this.Property(t => t.DocDirectionId).HasColumnName("DocDirectionId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
