using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;
using System.Collections.Generic;

namespace Gva.Api.Models
{
    public partial class GvaViewPerson
    {
        public int LotId { get; set; }

        public string Lin { get; set; }

        public string LinType { get; set; }

        public string Uin { get; set; }

        public string Names { get; set; }

        public DateTime BirtDate { get; set; }

        public string Organization { get; set; }

        public string Employment { get; set; }

        public virtual Lot Lot { get; set; }

        public virtual ICollection<GvaViewPersonLicence> Licences { get; set; }

        public virtual ICollection<GvaViewPersonRating> Ratings { get; set; }
    }

    public class GvaViewPersonMap : EntityTypeConfiguration<GvaViewPerson>
    {
        public GvaViewPersonMap()
        {
            // Primary Key
            this.HasKey(t => t.LotId);

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Lin)
                .HasMaxLength(50);

            this.Property(t => t.LinType)
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
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.Lin).HasColumnName("Lin");
            this.Property(t => t.LinType).HasColumnName("LinType");
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
