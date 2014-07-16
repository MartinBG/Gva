using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class EmailAddresseeType
    {
        public EmailAddresseeType()
        {
            this.EmailAddressees = new List<EmailAddressee>();
        }

        public int EmailAddresseeTypeId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public byte[] Version { get; set; }
        public virtual ICollection<EmailAddressee> EmailAddressees { get; set; }
    }

    public class EmailAddresseeTypeMap : EntityTypeConfiguration<EmailAddresseeType>
    {
        public EmailAddresseeTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.EmailAddresseeTypeId);

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
            this.ToTable("EmailAddresseeTypes");
            this.Property(t => t.EmailAddresseeTypeId).HasColumnName("EmailAddresseeTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
