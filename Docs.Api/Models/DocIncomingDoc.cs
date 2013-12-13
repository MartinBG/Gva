using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocIncomingDoc
    {
        public int DocIncomingDocId { get; set; }
        public int DocId { get; set; }
        public int IncomingDocId { get; set; }
        public bool IsDocInitial { get; set; }
        public System.DateTime CreateDate { get; set; }
        public virtual Doc Doc { get; set; }
        public virtual IncomingDoc IncomingDoc { get; set; }
    }

    public class DocIncomingDocMap : EntityTypeConfiguration<DocIncomingDoc>
    {
        public DocIncomingDocMap()
        {
            // Primary Key
            this.HasKey(t => t.DocIncomingDocId);

            // Properties
            // Table & Column Mappings
            this.ToTable("DocIncomingDocs");
            this.Property(t => t.DocIncomingDocId).HasColumnName("DocIncomingDocId");
            this.Property(t => t.DocId).HasColumnName("DocId");
            this.Property(t => t.IncomingDocId).HasColumnName("IncomingDocId");
            this.Property(t => t.IsDocInitial).HasColumnName("IsDocInitial");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");

            // Relationships
            this.HasRequired(t => t.Doc)
                .WithMany(t => t.DocIncomingDocs)
                .HasForeignKey(d => d.DocId);
            this.HasRequired(t => t.IncomingDoc)
                .WithMany(t => t.DocIncomingDocs)
                .HasForeignKey(d => d.IncomingDocId);

        }
    }
}
