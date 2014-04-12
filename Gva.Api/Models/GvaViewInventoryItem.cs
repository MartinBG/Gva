using System;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;

namespace Gva.Api.Models
{
    public partial class GvaViewInventoryItem
    {
        public int InventoryItemId { get; set; }

        public int LotId { get; set; }

        public int PartId { get; set; }

        public string SetPartAlias { get; set; }

        public string Name { get; set; }

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
    }

    public class GvaViewInventoryItemMap : EntityTypeConfiguration<GvaViewInventoryItem>
    {
        public GvaViewInventoryItemMap()
        {
            // Primary Key
            this.HasKey(t => t.InventoryItemId);

            this.Property(t => t.SetPartAlias)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .IsRequired();

            this.Property(t => t.Number)
                .HasMaxLength(100);

            this.Property(t => t.Publisher)
                .HasMaxLength(150);

            this.Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EditedBy)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GvaViewInventoryItems");
            this.Property(t => t.InventoryItemId).HasColumnName("GvaViewInvItemId");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartId).HasColumnName("LotPartId");
            this.Property(t => t.SetPartAlias).HasColumnName("SetPartAlias");
            this.Property(t => t.Name).HasColumnName("Name");
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
        }
    }
}
