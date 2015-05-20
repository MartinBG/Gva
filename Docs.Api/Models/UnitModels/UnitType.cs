using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models.UnitModels
{
    public partial class UnitType
    {
        public UnitType()
        {
            this.Units = new List<Unit>();
        }

        public int UnitTypeId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public bool IsActive { get; set; }

        public byte[] Version { get; set; }

        public virtual ICollection<Unit> Units { get; set; }
    }

    public class UnitTypeMap : EntityTypeConfiguration<UnitType>
    {
        public UnitTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.UnitTypeId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Alias)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("UnitTypes");
            this.Property(t => t.UnitTypeId).HasColumnName("UnitTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
