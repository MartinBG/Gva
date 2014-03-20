using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class IncomingDocStatus
    {
        public IncomingDocStatus()
        {
            this.IncomingDocs = new List<IncomingDoc>();
        }

        public int IncomingDocStatusId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public byte[] Version { get; set; }

        public virtual ICollection<IncomingDoc> IncomingDocs { get; set; }
    }

    public class IncomingDocStatusMap : EntityTypeConfiguration<IncomingDocStatus>
    {
        public IncomingDocStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.IncomingDocStatusId);

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
            this.ToTable("IncomingDocStatuses");
            this.Property(t => t.IncomingDocStatusId).HasColumnName("IncomingDocStatusId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
