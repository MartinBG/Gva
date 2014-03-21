using System;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;

namespace Gva.Api.Models
{
    public partial class GvaInventoryItem
    {
        public int InventoryItemId { get; set; }

        public int LotId { get; set; }

        public int PartId { get; set; }

        public int? CaseTypeId { get; set; }

        public string Filename { get; set; }

        public Guid? FileContentId { get; set; }

        public string DocumentType { get; set; }

        public string Name { get; set; }

        public string BookPageNumber { get; set; }

        public string PageIndex { get; set; }

        public int? PageCount { get; set; }

        public string Type { get; set; }

        public string Number { get; set; }

        public DateTime? Date { get; set; }

        public string Publisher { get; set; }

        public bool? Valid { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreationDate { get; set; }

        public string EditedBy { get; set; }

        public DateTime? EditedDate { get; set; }

        public virtual Lot Lot { get; set; }

        public virtual Part Part { get; set; }

        public virtual GvaCaseType CaseType { get; set; }
    }

    public class GvaInventoryItemMap : EntityTypeConfiguration<GvaInventoryItem>
    {
        public GvaInventoryItemMap()
        {
            // Primary Key
            this.HasKey(t => t.InventoryItemId);

            // Properties
            this.Property(t => t.Filename)
                .HasMaxLength(50);

            this.Property(t => t.DocumentType)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .IsRequired();

            this.Property(t => t.BookPageNumber)
                .HasMaxLength(50);

            this.Property(t => t.PageIndex)
                .HasMaxLength(50);

            this.Property(t => t.Type)
                .HasMaxLength(150);

            this.Property(t => t.Number)
                .HasMaxLength(50);

            this.Property(t => t.Publisher)
                .HasMaxLength(50);

            this.Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EditedBy)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GvaInventoryItems");
            this.Property(t => t.InventoryItemId).HasColumnName("GvaInventoryItemId");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartId).HasColumnName("LotPartId");
            this.Property(t => t.CaseTypeId).HasColumnName("GvaCaseTypeId");
            this.Property(t => t.Filename).HasColumnName("Filename");
            this.Property(t => t.FileContentId).HasColumnName("FileContentId");
            this.Property(t => t.DocumentType).HasColumnName("DocumentType");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.BookPageNumber).HasColumnName("BookPageNumber");
            this.Property(t => t.PageIndex).HasColumnName("PageIndex");
            this.Property(t => t.PageCount).HasColumnName("PageCount");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Number).HasColumnName("Number");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Publisher).HasColumnName("Publisher");
            this.Property(t => t.Valid).HasColumnName("Valid");
            this.Property(t => t.FromDate).HasColumnName("FromDate");
            this.Property(t => t.ToDate).HasColumnName("ToDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreationDate).HasColumnName("CreationDate");
            this.Property(t => t.EditedBy).HasColumnName("EditedBy");
            this.Property(t => t.EditedDate).HasColumnName("EditedDate");

            // Relationships
            this.HasRequired(t => t.Lot)
                .WithMany()
                .HasForeignKey(t => t.LotId);

            this.HasRequired(t => t.Part)
                .WithMany()
                .HasForeignKey(t => t.PartId);

            this.HasOptional(t => t.CaseType)
                .WithMany()
                .HasForeignKey(t => t.CaseTypeId);
        }
    }
}
