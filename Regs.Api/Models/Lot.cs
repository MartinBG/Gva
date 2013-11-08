using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regs.Api.Models
{
    public partial class Lot
    {
        public Lot()
        {
            this.Commits = new List<Commit>();
            this.Parts = new List<Part>();
        }

        public int LotId { get; set; }
        public int SetId { get; set; }
        public int NextIndex { get; set; }

        public virtual ICollection<Commit> Commits { get; set; }
        public virtual ICollection<Part> Parts { get; set; }
        public virtual Set Set { get; set; }
    }

    public class LotMap : EntityTypeConfiguration<Lot>
    {
        public LotMap()
        {
            // Primary Key
            this.HasKey(t => t.LotId);

            // Table & Column Mappings
            this.ToTable("Lots");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.SetId).HasColumnName("LotSetId");
            this.Property(t => t.NextIndex).HasColumnName("NextIndex");

            // Relationships
            this.HasRequired(t => t.Set)
                .WithMany(t => t.Lots)
                .HasForeignKey(d => d.SetId);
        }
    }
}
