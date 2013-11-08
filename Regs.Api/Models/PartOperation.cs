using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regs.Api.Models
{
    public partial class PartOperation
    {
        public int PartOperationId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
    }

    public class PartOperationMap : EntityTypeConfiguration<PartOperation>
    {
        public PartOperationMap()
        {
            // Primary Key
            this.HasKey(t => t.PartOperationId);

            // Properties
            this.Property(t => t.PartOperationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Alias)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("LotPartOperations");
            this.Property(t => t.PartOperationId).HasColumnName("LotPartOperationId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
        }
    }
}
