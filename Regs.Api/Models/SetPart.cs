using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Regs.Api.Models
{
    public partial class SetPart
    {
        public SetPart()
        {
            this.Parts = new List<Part>();
        }

        public int SetPartId { get; set; }

        public int SetId { get; set; }

        public string Alias { get; set; }

        public string PathRegex { get; set; }

        public string Schema { get; set; }

        public virtual ICollection<Part> Parts { get; set; }

        public virtual Set Set { get; set; }
    }

    public class SetPartMap : EntityTypeConfiguration<SetPart>
    {
        public SetPartMap()
        {
            // Primary Key
            this.HasKey(t => t.SetPartId);

            // Properties
            this.Property(t => t.SetPartId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Alias)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PathRegex)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Schema)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("LotSetParts");
            this.Property(t => t.SetPartId).HasColumnName("LotSetPartId");
            this.Property(t => t.SetId).HasColumnName("LotSetId");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.PathRegex).HasColumnName("PathRegex");
            this.Property(t => t.Schema).HasColumnName("Schema");

            // Relationships
            this.HasRequired(t => t.Set)
                .WithMany(t => t.SetParts)
                .HasForeignKey(d => d.SetId);
        }
    }
}
