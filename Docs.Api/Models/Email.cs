using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class Email
    {
        public Email()
        {
            this.EmailAddressees = new List<EmailAddressee>();
            this.EmailAttachments = new List<EmailAttachment>();
        }

        public int EmailId { get; set; }
        public int EmailTypeId { get; set; }
        public int EmailStatusId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Nullable<System.DateTime> SentDate { get; set; }
        public byte[] Version { get; set; }
        public virtual ICollection<EmailAddressee> EmailAddressees { get; set; }
        public virtual ICollection<EmailAttachment> EmailAttachments { get; set; }
        public virtual EmailStatus EmailStatus { get; set; }
        public virtual EmailType EmailType { get; set; }
    }

    public class EmailMap : EntityTypeConfiguration<Email>
    {
        public EmailMap()
        {
            // Primary Key
            this.HasKey(t => t.EmailId);

            // Properties
            this.Property(t => t.Subject)
                .HasMaxLength(1000);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Emails");
            this.Property(t => t.EmailId).HasColumnName("EmailId");
            this.Property(t => t.EmailTypeId).HasColumnName("EmailTypeId");
            this.Property(t => t.EmailStatusId).HasColumnName("EmailStatusId");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.Body).HasColumnName("Body");
            this.Property(t => t.SentDate).HasColumnName("SentDate");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.EmailStatus)
                .WithMany(t => t.Emails)
                .HasForeignKey(d => d.EmailStatusId);
            this.HasRequired(t => t.EmailType)
                .WithMany(t => t.Emails)
                .HasForeignKey(d => d.EmailTypeId);

        }
    }
}
