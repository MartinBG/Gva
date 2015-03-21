using Common.Api.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.Models.ClassificationModels
{
    public partial class RoleClassification
    {
        public int RoleClassificationId { get; set; }
        public int RoleId { get; set; }
        public int ClassificationId { get; set; }
        public int ClassificationPermissionId { get; set; }
        public byte[] Version { get; set; }
        public virtual ClassificationPermission ClassificationPermission { get; set; }
        public virtual Classification Classification { get; set; }
        public virtual Role Role { get; set; }
    }

    public class RoleClassificationMap : EntityTypeConfiguration<RoleClassification>
    {
        public RoleClassificationMap()
        {
            // Primary Key
            this.HasKey(t => t.RoleClassificationId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("RoleClassifications");
            this.Property(t => t.RoleClassificationId).HasColumnName("RoleClassificationId");
            this.Property(t => t.RoleId).HasColumnName("RoleId");
            this.Property(t => t.ClassificationId).HasColumnName("ClassificationId");
            this.Property(t => t.ClassificationPermissionId).HasColumnName("ClassificationPermissionId");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.ClassificationPermission)
                .WithMany()
                .HasForeignKey(d => d.ClassificationPermissionId);
            this.HasRequired(t => t.Classification)
                .WithMany()
                .HasForeignKey(d => d.ClassificationId);
            this.HasRequired(t => t.Role)
                .WithMany()
                .HasForeignKey(d => d.RoleId)
                .WillCascadeOnDelete();

        }
    }
}
