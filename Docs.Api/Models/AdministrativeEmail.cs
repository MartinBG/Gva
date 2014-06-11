using System;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class AdministrativeEmail
    {
        public int AdministrativeEmailId { get; set; }

        public int TypeId { get; set; }

        public int? UserId { get; set; }

        public int? CorrespondentId { get; set; }

        public int? CorrespondentContactId { get; set; }

        public string Param1 { get; set; }

        public string Param2 { get; set; }

        public string Param3 { get; set; }

        public string Param4 { get; set; }

        public string Param5 { get; set; }

        public int StatusId { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime? SentDate { get; set; }

        public byte[] Version { get; set; }

        public virtual AdministrativeEmailStatus AdministrativeEmailStatus { get; set; }

        public virtual AdministrativeEmailType AdministrativeEmailType { get; set; }
    }

    public class AdministrativeEmailMap : EntityTypeConfiguration<AdministrativeEmail>
    {
        public AdministrativeEmailMap()
        {
            // Primary Key
            this.HasKey(t => t.AdministrativeEmailId);

            // Properties
            this.Property(t => t.Param1)
                .HasMaxLength(500);

            this.Property(t => t.Param2)
                .HasMaxLength(500);

            this.Property(t => t.Param3)
                .HasMaxLength(500);

            this.Property(t => t.Param4)
                .HasMaxLength(500);

            this.Property(t => t.Param5)
                .HasMaxLength(500);

            this.Property(t => t.Subject)
                .HasMaxLength(1000);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("AdministrativeEmails");
            this.Property(t => t.AdministrativeEmailId).HasColumnName("AdministrativeEmailId");
            this.Property(t => t.TypeId).HasColumnName("TypeId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.CorrespondentId).HasColumnName("CorrespondentId");
            this.Property(t => t.CorrespondentContactId).HasColumnName("CorrespondentContactId");
            this.Property(t => t.Param1).HasColumnName("Param1");
            this.Property(t => t.Param2).HasColumnName("Param2");
            this.Property(t => t.Param3).HasColumnName("Param3");
            this.Property(t => t.Param4).HasColumnName("Param4");
            this.Property(t => t.Param5).HasColumnName("Param5");
            this.Property(t => t.StatusId).HasColumnName("StatusId");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.Body).HasColumnName("Body");
            this.Property(t => t.SentDate).HasColumnName("SentDate");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.AdministrativeEmailStatus)
                .WithMany(t => t.AdministrativeEmails)
                .HasForeignKey(d => d.StatusId);
            this.HasRequired(t => t.AdministrativeEmailType)
                .WithMany(t => t.AdministrativeEmails)
                .HasForeignKey(d => d.TypeId);
        }
    }
}
