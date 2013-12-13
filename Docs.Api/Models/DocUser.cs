using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocUser
    {
        public int DocId { get; set; }
        public int UnitId { get; set; }
        public int DocUnitPermissionId { get; set; }
        public bool HasRead { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> ActivateDate { get; set; }
        public Nullable<System.DateTime> DeactivateDate { get; set; }
        public virtual Doc Doc { get; set; }
        public virtual DocUnitPermission DocUnitPermission { get; set; }
        public virtual Unit Unit { get; set; }
    }

    public class DocUserMap : EntityTypeConfiguration<DocUser>
    {
        public DocUserMap()
        {
            // Primary Key
            this.HasKey(t => new { t.DocId, t.UnitId, t.DocUnitPermissionId });

            // Properties
            this.Property(t => t.DocId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UnitId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DocUnitPermissionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DocUsers");
            this.Property(t => t.DocId).HasColumnName("DocId");
            this.Property(t => t.UnitId).HasColumnName("UnitId");
            this.Property(t => t.DocUnitPermissionId).HasColumnName("DocUnitPermissionId");
            this.Property(t => t.HasRead).HasColumnName("HasRead");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.ActivateDate).HasColumnName("ActivateDate");
            this.Property(t => t.DeactivateDate).HasColumnName("DeactivateDate");

            // Relationships
            this.HasRequired(t => t.Doc)
                .WithMany(t => t.DocUsers)
                .HasForeignKey(d => d.DocId);
            this.HasRequired(t => t.DocUnitPermission)
                .WithMany(t => t.DocUsers)
                .HasForeignKey(d => d.DocUnitPermissionId);
            this.HasRequired(t => t.Unit)
                .WithMany(t => t.DocUsers)
                .HasForeignKey(d => d.UnitId);

        }
    }
}
