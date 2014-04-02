using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;

namespace Gva.Api.Models
{
    public partial class GvaViewPersonData
    {
        public int GvaPersonLotId { get; set; }

        public string Lin { get; set; }

        public string Uin { get; set; }

        public string Names { get; set; }

        public DateTime BirtDate { get; set; }

        public string Organization { get; set; }

        public string Employment { get; set; }

        public virtual Lot Lot { get; set; }
    }

    public class GvaViewPersonMap : EntityTypeConfiguration<GvaViewPersonData>
    {
        public GvaViewPersonMap()
        {
            // Primary Key
            this.HasKey(t => t.GvaPersonLotId);

            // Properties
            this.Property(t => t.GvaPersonLotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Lin)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Uin)
                .HasMaxLength(50);

            this.Property(t => t.Names)
                .IsRequired();

            this.Property(t => t.Organization)
                .HasMaxLength(50);

            this.Property(t => t.Employment)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GvaViewPersons");
            this.Property(t => t.GvaPersonLotId).HasColumnName("GvaPersonLotId");
            this.Property(t => t.Lin).HasColumnName("Lin");
            this.Property(t => t.Uin).HasColumnName("Uin");
            this.Property(t => t.Names).HasColumnName("Names");
            this.Property(t => t.BirtDate).HasColumnName("BirtDate");
            this.Property(t => t.Organization).HasColumnName("Organization");
            this.Property(t => t.Employment).HasColumnName("Employment");

            // Relationships
            this.HasRequired(t => t.Lot)
                .WithOptional();
        }
    }
}
