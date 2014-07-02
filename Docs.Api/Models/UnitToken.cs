using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class UnitToken
    {
        public int UnitId { get; set; }
        public string Token { get; set; }
        public string CreateToken { get; set; }
        public int DocUnitPermissionId { get; set; }
        public virtual DocUnitPermission DocUnitPermission { get; set; }
    }

    public class UnitTokenMap : EntityTypeConfiguration<UnitToken>
    {
        public UnitTokenMap()
        {
            // Primary Key
            this.HasKey(t => new { t.UnitId, t.Token, t.CreateToken, t.DocUnitPermissionId });

            // Properties
            this.Property(t => t.UnitId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Token)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.CreateToken)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.DocUnitPermissionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("UnitTokens");
            this.Property(t => t.UnitId).HasColumnName("UnitId");
            this.Property(t => t.Token).HasColumnName("Token");
            this.Property(t => t.CreateToken).HasColumnName("CreateToken");
            this.Property(t => t.DocUnitPermissionId).HasColumnName("DocUnitPermissionId");

            this.HasRequired(t => t.DocUnitPermission)
                .WithMany()
                .HasForeignKey(d => d.DocUnitPermissionId);
        }
    }
}
