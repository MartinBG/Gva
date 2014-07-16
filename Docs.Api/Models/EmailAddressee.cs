using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class EmailAddressee
    {
        public int EmailAddresseeId { get; set; }
        public int EmailId { get; set; }
        public int EmailAddresseeTypeId { get; set; }
        public string Address { get; set; }
        public byte[] Version { get; set; }
        public virtual EmailAddresseeType EmailAddresseeType { get; set; }
        public virtual Email Email { get; set; }
    }

    public class EmailAddresseeMap : EntityTypeConfiguration<EmailAddressee>
    {
        public EmailAddresseeMap()
        {
            // Primary Key
            this.HasKey(t => t.EmailAddresseeId);

            // Properties
            this.Property(t => t.Address)
                .IsRequired()
                .HasMaxLength(1000);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("EmailAddressees");
            this.Property(t => t.EmailAddresseeId).HasColumnName("EmailAddresseeId");
            this.Property(t => t.EmailId).HasColumnName("EmailId");
            this.Property(t => t.EmailAddresseeTypeId).HasColumnName("EmailAddresseeTypeId");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.EmailAddresseeType)
                .WithMany(t => t.EmailAddressees)
                .HasForeignKey(d => d.EmailAddresseeTypeId);
            this.HasRequired(t => t.Email)
                .WithMany(t => t.EmailAddressees)
                .HasForeignKey(d => d.EmailId);

        }
    }
}
