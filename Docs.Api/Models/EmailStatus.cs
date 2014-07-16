using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class EmailStatus
    {
        public EmailStatus()
        {
            this.Emails = new List<Email>();
        }

        public int EmailStatusId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public byte[] Version { get; set; }
        public virtual ICollection<Email> Emails { get; set; }
    }

    public class EmailStatusMap : EntityTypeConfiguration<EmailStatus>
    {
        public EmailStatusMap()
        {
            // Primary Key
            this.HasKey(t => t.EmailStatusId);

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
            this.ToTable("EmailStatuses");
            this.Property(t => t.EmailStatusId).HasColumnName("EmailStatusId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
