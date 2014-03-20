using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Common.Api.Models
{
    public partial class User
    {
        public User()
        {
            this.Roles = new List<Role>();
        }

        public int UserId { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public bool HasPassword { get; set; }

        public string Fullname { get; set; }

        public string Notes { get; set; }

        public string CertificateThumbprint { get; set; }

        public bool IsActive { get; set; }

        public byte[] Version { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }

    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.UserId);

            // Properties
            this.Property(t => t.Username)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.PasswordHash)
                .HasMaxLength(200);

            this.Property(t => t.PasswordSalt)
                .HasMaxLength(200);

            this.Property(t => t.Fullname)
                .HasMaxLength(200);

            this.Property(t => t.CertificateThumbprint)
                .HasMaxLength(200);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Users");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Username).HasColumnName("Username");
            this.Property(t => t.PasswordHash).HasColumnName("PasswordHash");
            this.Property(t => t.PasswordSalt).HasColumnName("PasswordSalt");
            this.Property(t => t.HasPassword).HasColumnName("HasPassword");
            this.Property(t => t.Fullname).HasColumnName("Fullname");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.CertificateThumbprint).HasColumnName("CertificateThumbprint");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
