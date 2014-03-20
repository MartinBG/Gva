using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class AdministrativeEmailStatus
    {
        public AdministrativeEmailStatus()
        {
            this.AdministrativeEmails = new List<AdministrativeEmail>();
        }

        public int AdministrativeEmailStatusId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public byte[] Version { get; set; }

        public virtual ICollection<AdministrativeEmail> AdministrativeEmails { get; set; }
    }

    public class AdministrativeEmailStatusMap : EntityTypeConfiguration<AdministrativeEmailStatus>
    {
        public AdministrativeEmailStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.AdministrativeEmailStatusId);

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
            this.ToTable("AdministrativeEmailStatuses");
            this.Property(t => t.AdministrativeEmailStatusId).HasColumnName("AdministrativeEmailStatusId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
