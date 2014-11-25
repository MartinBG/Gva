using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Web.Helpers;
using Newtonsoft.Json;
using System;

namespace Common.Api.Models
{
    public class User
    {
        public User()
        {
            this.Roles = new List<Role>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public DateTime? ResignationDate { get; set; }
        public string Notes { get; set; }
        public string CertificateThumbprint { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool HasPassword { get; set; }
        public byte[] Version { get; set; }
        public virtual ICollection<Role> Roles { get; set; }

        //server only
        [JsonIgnore]
        public string PasswordHash { get; set; }
        [JsonIgnore]
        public string PasswordSalt { get; set; }

        //client only
        public string Password { get; set; }

        public void SetPassword(string password)
        {
            if (password == null)
            {
                this.PasswordSalt = null;
                this.PasswordHash = null;
            }
            else
            {
                this.PasswordSalt = Crypto.GenerateSalt();
                this.PasswordHash = Crypto.HashPassword(password + this.PasswordSalt);
            }
        }

        public bool VerifyPassword(string password)
        {
            return Crypto.VerifyHashedPassword(this.PasswordHash, password + this.PasswordSalt);
        }

        public void ChangePassword(string oldPassword, string newPassword)
        {
            if (this.VerifyPassword(oldPassword))
            {
                this.SetPassword(newPassword);
            }
            else
            {
                throw new Exception("Wrong password provided");
            }
        }
    }

    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.UserId);

            // Properties
            this.Property(t => t.UserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.Email)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Users");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Username).HasColumnName("Username");
            this.Property(t => t.PasswordHash).HasColumnName("PasswordHash");
            this.Property(t => t.PasswordSalt).HasColumnName("PasswordSalt");
            this.Property(t => t.HasPassword).HasColumnName("HasPassword");
            this.Property(t => t.Fullname).HasColumnName("Fullname");
            this.Property(t => t.AppointmentDate).HasColumnName("AppointmentDate");
            this.Property(t => t.ResignationDate).HasColumnName("ResignationDate");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.CertificateThumbprint).HasColumnName("CertificateThumbprint");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");

            this.Ignore(u => u.Password);
        }
    }
}
