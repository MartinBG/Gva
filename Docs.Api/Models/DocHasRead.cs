using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocHasRead
    {
        public int DocHasReadId { get; set; }

        public int DocId { get; set; }

        public int UnitId { get; set; }

        public bool HasRead { get; set; }

        public DateTime ModifyDate { get; set; }

        public int ModifyUserId { get; set; }

        public byte[] Version { get; set; }

        public virtual Doc Doc { get; set; }

        public virtual Common.Api.Models.Unit Unit { get; set; }

        public virtual Common.Api.Models.User User { get; set; }
    }

    public class DocHasReadMap : EntityTypeConfiguration<DocHasRead>
    {
        public DocHasReadMap()
        {
            // Primary Key
            this.HasKey(t => t.DocHasReadId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DocHasReads");
            this.Property(t => t.DocHasReadId).HasColumnName("DocHasReadId");
            this.Property(t => t.DocId).HasColumnName("DocId");
            this.Property(t => t.UnitId).HasColumnName("UnitId");
            this.Property(t => t.HasRead).HasColumnName("HasRead");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.ModifyUserId).HasColumnName("ModifyUserId");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Doc)
                .WithMany(t => t.DocHasReads)
                .HasForeignKey(d => d.DocId)
                .WillCascadeOnDelete();
            this.HasRequired(t => t.Unit)
                .WithMany()
                .HasForeignKey(d => d.UnitId)
                .WillCascadeOnDelete();
            this.HasRequired(t => t.User)
                .WithMany()
                .HasForeignKey(d => d.ModifyUserId);

        }
    }
}
