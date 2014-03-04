using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocRelation
    {
        public int DocRelationId { get; set; }
        public int DocId { get; set; }
        public Nullable<int> ParentDocId { get; set; }
        public Nullable<int> RootDocId { get; set; }
        public byte[] Version { get; set; }
        public virtual Doc Doc { get; set; }
        public virtual Doc ParentDoc { get; set; }
        public virtual Doc RootDoc { get; set; }
    }

    public class DocRelationMap : EntityTypeConfiguration<DocRelation>
    {
        public DocRelationMap()
        {
            // Primary Key
            this.HasKey(t => t.DocRelationId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DocRelations");
            this.Property(t => t.DocRelationId).HasColumnName("DocRelationId");
            this.Property(t => t.DocId).HasColumnName("DocId");
            this.Property(t => t.ParentDocId).HasColumnName("ParentDocId");
            this.Property(t => t.RootDocId).HasColumnName("RootDocId");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Doc)
                .WithMany(t => t.DocRelations)
                .HasForeignKey(d => d.DocId);
            this.HasOptional(t => t.ParentDoc)
                .WithMany(t => t.DocRelations1)
                .HasForeignKey(d => d.ParentDocId);
            this.HasOptional(t => t.RootDoc)
                .WithMany(t => t.DocRelations2)
                .HasForeignKey(d => d.RootDocId);

        }
    }
}
