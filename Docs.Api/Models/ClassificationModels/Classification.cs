using Docs.Api.Models.UnitModels;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models.ClassificationModels
{
    public partial class Classification
    {
        public Classification()
        {
            this.ClassificationRelations = new List<ClassificationRelation>();
            this.UnitClassifications = new List<UnitClassification>();
        }

        public int ClassificationId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public bool IsActive { get; set; }

        public byte[] Version { get; set; }

        public virtual ICollection<ClassificationRelation> ClassificationRelations { get; set; }

        public virtual ICollection<UnitClassification> UnitClassifications { get; set; }
    }

    public class ClassificationMap : EntityTypeConfiguration<Classification>
    {
        public ClassificationMap()
        {
            // Primary Key
            this.HasKey(t => t.ClassificationId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Alias)
                .HasMaxLength(200);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Classifications");
            this.Property(t => t.ClassificationId).HasColumnName("ClassificationId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
