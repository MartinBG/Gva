using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;
using Regs.Api.Models;
using Regs.Api.LotEvents;

namespace Gva.Api.Models.Views.Equipment
{
    public partial class GvaViewEquipment : IProjectionView
    {
        public int LotId { get; set; }

        public string Name { get; set; }

        public int EquipmentTypeId { get; set; }

        public int EquipmentProducerId { get; set; }

        public string ManPlace { get; set; }

        public DateTime ManDate { get; set; }

        public string Place { get; set; }

        public DateTime? OperationalDate { get; set; }

        public string Note { get; set; }

        public virtual Lot Lot { get; set; }

        public virtual NomValue EquipmentType { get; set; }

        public virtual NomValue EquipmentProducer { get; set; }
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

            this.Property(t => t.EquipmentTypeId)
                .IsRequired();

            this.Property(t => t.EquipmentProducerId)
                .IsRequired();

            this.Property(t => t.ManPlace)
                .IsRequired();

            this.Property(t => t.ManDate)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("GvaViewEquipments");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.EquipmentTypeId).HasColumnName("EquipmentTypeId");
            this.Property(t => t.EquipmentProducerId).HasColumnName("EquipmentProducerId");
            this.Property(t => t.ManPlace).HasColumnName("ManPlace");
            this.Property(t => t.Place).HasColumnName("Place");
            this.Property(t => t.OperationalDate).HasColumnName("OperationalDate");
            this.Property(t => t.Note).HasColumnName("Note");

            // Relationships
            this.HasRequired(t => t.Lot)
                .WithOptional();
            this.HasRequired(t => t.EquipmentType)
                .WithMany()
                .HasForeignKey(t => t.EquipmentTypeId);
            this.HasRequired(t => t.EquipmentProducer)
                .WithMany()
                .HasForeignKey(t => t.EquipmentProducerId);
        }
    }
}
