using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocCasePartMovement
    {
        public int DocCasePartMovementId { get; set; }

        public int DocId { get; set; }

        public int DocCasePartTypeId { get; set; }

        public DateTime MovementDate { get; set; }

        public int UserId { get; set; }

        public byte[] Version { get; set; }

        public virtual DocCasePartType DocCasePartType { get; set; }

        public virtual Doc Doc { get; set; }

        public virtual Common.Api.Models.User User { get; set; }
    }

    public class DocCasePartMovementMap : EntityTypeConfiguration<DocCasePartMovement>
    {
        public DocCasePartMovementMap()
        {
            // Primary Key
            this.HasKey(t => t.DocCasePartMovementId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DocCasePartMovements");
            this.Property(t => t.DocCasePartMovementId).HasColumnName("DocCasePartMovementId");
            this.Property(t => t.DocId).HasColumnName("DocId");
            this.Property(t => t.DocCasePartTypeId).HasColumnName("DocCasePartTypeId");
            this.Property(t => t.MovementDate).HasColumnName("MovementDate");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.DocCasePartType)
                .WithMany(t => t.DocCasePartMovements)
                .HasForeignKey(d => d.DocCasePartTypeId);
            this.HasRequired(t => t.Doc)
                .WithMany(t => t.DocCasePartMovements)
                .HasForeignKey(d => d.DocId)
                .WillCascadeOnDelete();
            this.HasRequired(t => t.User)
                .WithMany()
                .HasForeignKey(d => d.UserId);

        }
    }
}
