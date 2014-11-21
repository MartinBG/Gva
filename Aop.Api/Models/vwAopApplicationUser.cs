using Common.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Aop.Api.Models
{
    public partial class vwAopApplicationUser
    {
        public int AopApplicationId { get; set; }
        public int UnitId { get; set; }
        public int ClassificationPermissionId { get; set; }
        public Nullable<long> C_c1 { get; set; }
        public virtual AopApp AopApp { get; set; }
        public virtual ClassificationPermission ClassificationPermission { get; set; }
    }

    public class vwAopApplicationUserMap : EntityTypeConfiguration<vwAopApplicationUser>
    {
        public vwAopApplicationUserMap()
        {
            // Primary Key
            this.HasKey(t => new { t.AopApplicationId, t.UnitId, t.ClassificationPermissionId });

            // Properties
            this.Property(t => t.AopApplicationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UnitId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ClassificationPermissionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("vwAopApplicationUsers");
            this.Property(t => t.AopApplicationId).HasColumnName("AopApplicationId");
            this.Property(t => t.UnitId).HasColumnName("UnitId");
            this.Property(t => t.ClassificationPermissionId).HasColumnName("ClassificationPermissionId");
            this.Property(t => t.C_c1).HasColumnName("_c1");

            this.HasRequired(t => t.AopApp)
                .WithMany(t => t.vwAopApplicationUsers)
                .HasForeignKey(d => d.AopApplicationId);
            this.HasRequired(t => t.ClassificationPermission)
                .WithMany()
                .HasForeignKey(d => d.ClassificationPermissionId);
        }
    }
}
