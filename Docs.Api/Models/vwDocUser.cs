using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class vwDocUser
    {
        public int DocId { get; set; }
        public int UnitId { get; set; }
        public int DocUnitPermissionId { get; set; }
        public Nullable<long> C_c1 { get; set; }
        public virtual Doc Doc { get; set; }
        public virtual DocUnitPermission DocUnitPermission { get; set; }
    }

    public class vwDocUserMap : EntityTypeConfiguration<vwDocUser>
    {
        public vwDocUserMap()
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
            this.ToTable("vwDocUsers");
            this.Property(t => t.DocId).HasColumnName("DocId");
            this.Property(t => t.UnitId).HasColumnName("UnitId");
            this.Property(t => t.DocUnitPermissionId).HasColumnName("DocUnitPermissionId");
            this.Property(t => t.C_c1).HasColumnName("_c1");

            this.HasRequired(t => t.Doc)
                .WithMany(t => t.vwDocUsers)
                .HasForeignKey(d => d.DocId);
            this.HasRequired(t => t.DocUnitPermission)
                .WithMany()
                .HasForeignKey(d => d.DocUnitPermissionId);
        }
    }
}
