using System;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;
using Regs.Api.Models;
using Regs.Api.LotEvents;

namespace Gva.Api.Models.Views
{
    public partial class GvaViewInventoryItem : IProjectionView
    {
        public int LotId { get; set; }

        public int PartId { get; set; }

        public int? ParentPartId { get; set; }

        public string SetPartAlias { get; set; }

        public string Name { get; set; }

        public int? TypeId { get; set; }

        public string Number { get; set; }

        public DateTime? Date { get; set; }

        public string Publisher { get; set; }

        public bool? Valid { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string Notes { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreationDate { get; set; }

        public string EditedBy { get; set; }

        public DateTime? EditedDate { get; set; }

        public virtual NomValue Type { get; set; }

        public virtual Lot Lot { get; set; }

        public virtual Part Part { get; set; }

        public virtual Part ParentPart { get; set; }
    }

    public class GvaViewInventoryItemMap : EntityTypeConfiguration<GvaViewInventoryItem>
    {
        public GvaViewInventoryItemMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.PartId });

            this.Property(t => t.SetPartAlias)
                .IsRequired()
                .HasMaxLength(100);

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
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartId).HasColumnName("LotPartId");
            this.Property(t => t.ParentPartId).HasColumnName("ParentLotPartId");
            this.Property(t => t.SetPartAlias).HasColumnName("SetPartAlias");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.TypeId).HasColumnName("TypeId");
            this.Property(t => t.Number).HasColumnName("Number");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Publisher).HasColumnName("Publisher");
            this.Property(t => t.Valid).HasColumnName("Valid");
            this.Property(t => t.FromDate).HasColumnName("FromDate");
            this.Property(t => t.ToDate).HasColumnName("ToDate");
            this.Property(t => t.Notes).HasColumnName("Notes");
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

            this.HasOptional(t => t.ParentPart)
                .WithMany()
                .HasForeignKey(t => t.ParentPartId);

            this.HasOptional(t => t.Type)
                .WithMany()
                .HasForeignKey(t => t.TypeId);
        }
    }
}
