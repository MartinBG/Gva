using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class AdministrativeEmailType
    {
        public AdministrativeEmailType()
        {
            this.AdministrativeEmails = new List<AdministrativeEmail>();
        }

        public int AdministrativeEmailTypeId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public byte[] Version { get; set; }

        public virtual ICollection<AdministrativeEmail> AdministrativeEmails { get; set; }
    }

    public class AdministrativeEmailTypeMap : EntityTypeConfiguration<AdministrativeEmailType>
    {
        public AdministrativeEmailTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.AdministrativeEmailTypeId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Alias)
                .HasMaxLength(200);

            this.Property(t => t.Subject)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("AdministrativeEmailTypes");
            this.Property(t => t.AdministrativeEmailTypeId).HasColumnName("AdministrativeEmailTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.Body).HasColumnName("Body");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
