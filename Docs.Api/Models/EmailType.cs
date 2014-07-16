using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class EmailType
    {
        public EmailType()
        {
            this.Emails = new List<Email>();
        }

        public int EmailTypeId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public byte[] Version { get; set; }
        public virtual ICollection<Email> Emails { get; set; }
    }

    public class EmailTypeMap : EntityTypeConfiguration<EmailType>
    {
        public EmailTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.EmailTypeId);

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
            this.ToTable("EmailTypes");
            this.Property(t => t.EmailTypeId).HasColumnName("EmailTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.Body).HasColumnName("Body");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
