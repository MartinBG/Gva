using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class IncomingDoc
    {
        public IncomingDoc()
        {
            this.DocIncomingDocs = new List<DocIncomingDoc>();
            this.IncomingDocFiles = new List<IncomingDocFile>();
        }

        public int IncomingDocId { get; set; }

        public Guid DocumentGuid { get; set; }

        public DateTime IncomingDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public int IncomingDocStatusId { get; set; }

        public byte[] Version { get; set; }

        public virtual ICollection<DocIncomingDoc> DocIncomingDocs { get; set; }

        public virtual ICollection<IncomingDocFile> IncomingDocFiles { get; set; }

        public virtual IncomingDocStatus IncomingDocStatus { get; set; }
    }

    public class IncomingDocMap : EntityTypeConfiguration<IncomingDoc>
    {
        public IncomingDocMap()
        {
            // Primary Key
            this.HasKey(t => t.IncomingDocId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("IncomingDocs");
            this.Property(t => t.IncomingDocId).HasColumnName("IncomingDocId");
            this.Property(t => t.DocumentGuid).HasColumnName("DocumentGuid");
            this.Property(t => t.IncomingDate).HasColumnName("IncomingDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.IncomingDocStatusId).HasColumnName("IncomingDocStatusId");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.IncomingDocStatus)
                .WithMany(t => t.IncomingDocs)
                .HasForeignKey(d => d.IncomingDocStatusId);
        }
    }
}
