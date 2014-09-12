using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;
using Common.Sequence;

namespace Regs.Api.Models
{
    public partial class PartUser
    {
        public PartUser()
        {
        }

        public int PartId { get; set; }

        public int UserId { get; set; }

        public int ClassificationPermissionId { get; set; }

        public Part Part { get; set; }
    }

    public class PartUserMap : EntityTypeConfiguration<PartUser>
    {
        public PartUserMap()
        {
            // Primary Key
            this.HasKey(t => new { t.PartId, t.UserId, t.ClassificationPermissionId });

            // Properties
            this.Property(t => t.PartId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ClassificationPermissionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("vwLotPartUsers");
            this.Property(t => t.PartId).HasColumnName("LotPartId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.ClassificationPermissionId).HasColumnName("ClassificationPermissionId");

            // Relationships
            this.HasRequired(t => t.Part)
                .WithMany(t => t.PartUsers)
                .HasForeignKey(d => d.PartId);
        }
    }
}
