using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class CorrespondentContact
    {
        public CorrespondentContact()
        {
            this.DocCorrespondentContacts = new List<DocCorrespondentContact>();
        }

        public int CorrespondentContactId { get; set; }
        public int CorrespondentId { get; set; }
        public string Name { get; set; }
        public string UIN { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual Correspondent Correspondent { get; set; }
        public virtual ICollection<DocCorrespondentContact> DocCorrespondentContacts { get; set; }
    }

    public class CorrespondentContactMap : EntityTypeConfiguration<CorrespondentContact>
    {
        public CorrespondentContactMap()
        {
            // Primary Key
            this.HasKey(t => t.CorrespondentContactId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.UIN)
                .HasMaxLength(50);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("CorrespondentContacts");
            this.Property(t => t.CorrespondentContactId).HasColumnName("CorrespondentContactId");
            this.Property(t => t.CorrespondentId).HasColumnName("CorrespondentId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.UIN).HasColumnName("UIN");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Correspondent)
                .WithMany(t => t.CorrespondentContacts)
                .HasForeignKey(d => d.CorrespondentId);

        }
    }
}
