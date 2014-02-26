using Regs.Api.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gva.Api.Models
{
    public partial class GvaPerson
    {
        public int GvaPersonLotId { get; set; }
        public string Lin { get; set; }
        public string Uin { get; set; }
        public string Names { get; set; }
        public int Age { get; set; }
        public string Licences { get; set; }
        public string Ratings { get; set; }
        public string Organization { get; set; }
        public string Employment { get; set; }

        public virtual Lot Lot { get; set; }
    }

    public class GvaPersonMap : EntityTypeConfiguration<GvaPerson>
    {
        public GvaPersonMap()
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

            // Table & Column Mappings
            this.ToTable("GvaPersons");
            this.Property(t => t.GvaPersonLotId).HasColumnName("GvaPersonLotId");
            this.Property(t => t.Lin).HasColumnName("Lin");
            this.Property(t => t.Uin).HasColumnName("Uin");
            this.Property(t => t.Names).HasColumnName("Names");
            this.Property(t => t.Age).HasColumnName("Age");
            this.Property(t => t.Licences).HasColumnName("Licences");
            this.Property(t => t.Ratings).HasColumnName("Ratings");
            this.Property(t => t.Organization).HasColumnName("Organization");
            this.Property(t => t.Employment).HasColumnName("Employment");

            // Relationships
            this.HasRequired(t => t.Lot)
                .WithOptional();
        }
    }
}
