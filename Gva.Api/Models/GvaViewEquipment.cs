using Regs.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gva.Api.Models
{
    public partial class GvaViewEquipment
    {
        public int LotId { get; set; }

        public string Name { get; set; }

        public string EquipmentType { get; set; }

        public string EquipmentProducer { get; set; }

        public string ManPlace { get; set; }

        public DateTime ManDate { get; set; }

        public string Place { get; set; }

        public DateTime? OperationalDate { get; set; }

        public string Note { get; set; }

        public virtual Lot Lot { get; set; }
    }

    public class GvaViewEquipmentMap : EntityTypeConfiguration<GvaViewEquipment>
    {
        public GvaViewEquipmentMap()
        {
            // Primary Key
            this.HasKey(t => t.LotId);

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EquipmentType)
                .IsRequired();

            this.Property(t => t.EquipmentProducer)
                .IsRequired();

            this.Property(t => t.ManPlace)
                .IsRequired();

            this.Property(t => t.ManDate)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("GvaViewEquipments");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.EquipmentType).HasColumnName("EquipmentType");
            this.Property(t => t.EquipmentProducer).HasColumnName("EquipmentProducer");
            this.Property(t => t.ManPlace).HasColumnName("ManPlace");
            this.Property(t => t.Place).HasColumnName("Place");
            this.Property(t => t.OperationalDate).HasColumnName("OperationalDate");
            this.Property(t => t.Note).HasColumnName("Note");

            // Relationships
            this.HasRequired(t => t.Lot)
                .WithOptional();
        }
    }
}
