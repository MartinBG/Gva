using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class UnitUser
    {
        public int UnitUserId { get; set; }

        public int UserId { get; set; }

        public int UnitId { get; set; }

        public bool IsActive { get; set; }

        public byte[] Version { get; set; }

        public virtual Unit Unit { get; set; }

        public virtual Common.Api.Models.User User { get; set; }
    }

    public class UnitUserMap : EntityTypeConfiguration<UnitUser>
    {
        public UnitUserMap()
        {
            // Primary Key
            this.HasKey(t => t.UnitUserId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("UnitUsers");
            this.Property(t => t.UnitUserId).HasColumnName("UnitUserId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.UnitId).HasColumnName("UnitId");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Unit)
                .WithMany(t => t.UnitUsers)
                .HasForeignKey(d => d.UnitId);
            this.HasRequired(t => t.User)
                .WithMany()
                .HasForeignKey(d => d.UserId);

        }
    }
}
