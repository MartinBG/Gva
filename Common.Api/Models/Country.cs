using System.Data.Entity.ModelConfiguration;

namespace Common.Api.Models
{
    public partial class Country
    {
        public Country()
        {
        }

        public int CountryId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Alias { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public byte[] Version { get; set; }
    }

    public class CountryMap : EntityTypeConfiguration<Country>
    {
        public CountryMap()
        {
            // Primary Key
            this.HasKey(t => t.CountryId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Alias)
                .HasMaxLength(200);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Countries");
            this.Property(t => t.CountryId).HasColumnName("CountryId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
