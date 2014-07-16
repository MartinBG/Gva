using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class EmailAttachment
    {
        public int EmailAttachmentId { get; set; }
        public int EmailId { get; set; }
        public string Name { get; set; }
        public System.Guid ContentId { get; set; }
        public byte[] Version { get; set; }
        public virtual Email Email { get; set; }
    }

    public class EmailAttachmentMap : EntityTypeConfiguration<EmailAttachment>
    {
        public EmailAttachmentMap()
        {
            // Primary Key
            this.HasKey(t => t.EmailAttachmentId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.Name)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("EmailAttachments");
            this.Property(t => t.EmailAttachmentId).HasColumnName("EmailAttachmentId");
            this.Property(t => t.EmailId).HasColumnName("EmailId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ContentId).HasColumnName("ContentId");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Email)
                .WithMany(t => t.EmailAttachments)
                .HasForeignKey(d => d.EmailId);

        }
    }
}
