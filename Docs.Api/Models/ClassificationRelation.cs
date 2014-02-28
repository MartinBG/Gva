using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class ClassificationRelation
    {
        public int ClassificationRelationId { get; set; }

        public int ClassificationId { get; set; }

        public int? ParentClassificationId { get; set; }

        public int? RootClassificationId { get; set; }

        public byte[] Version { get; set; }

        public virtual Classification Classification { get; set; }

        public virtual Classification Classification1 { get; set; }

        public virtual Classification Classification2 { get; set; }
    }

    public class ClassificationRelationMap : EntityTypeConfiguration<ClassificationRelation>
    {
        public ClassificationRelationMap()
        {
            // Primary Key
            this.HasKey(t => t.ClassificationRelationId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ClassificationRelations");
            this.Property(t => t.ClassificationRelationId).HasColumnName("ClassificationRelationId");
            this.Property(t => t.ClassificationId).HasColumnName("ClassificationId");
            this.Property(t => t.ParentClassificationId).HasColumnName("ParentClassificationId");
            this.Property(t => t.RootClassificationId).HasColumnName("RootClassificationId");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Classification)
                .WithMany(t => t.ClassificationRelations)
                .HasForeignKey(d => d.ClassificationId);
            this.HasOptional(t => t.Classification1)
                .WithMany(t => t.ClassificationRelations1)
                .HasForeignKey(d => d.ParentClassificationId);
            this.HasOptional(t => t.Classification2)
                .WithMany(t => t.ClassificationRelations2)
                .HasForeignKey(d => d.RootClassificationId);
        }
    }
}
