using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class IrregularityType
    {
        public int IrregularityTypeId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string Description { get; set; }

        public byte[] Version { get; set; }
    }

    public class IrregularityTypeMap : EntityTypeConfiguration<IrregularityType>
    {
        public IrregularityTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.IrregularityTypeId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Alias)
                .HasMaxLength(200);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("IrregularityTypes");
            this.Property(t => t.IrregularityTypeId).HasColumnName("IrregularityTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
