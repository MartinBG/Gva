using Docs.Api.Models.ClassificationModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models.UnitModels
{
    public partial class UnitToken
    {
        public int UnitId { get; set; }
        public string Token { get; set; }
        public string CreateToken { get; set; }
        public int ClassificationPermissionId { get; set; }
        public virtual ClassificationPermission ClassificationPermission { get; set; }
    }

    public class UnitTokenMap : EntityTypeConfiguration<UnitToken>
    {
        public UnitTokenMap()
        {
            // Primary Key
            this.HasKey(t => new { t.UnitId, t.Token, t.CreateToken, t.ClassificationPermissionId });

            // Properties
            this.Property(t => t.UnitId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Token)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.CreateToken)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.ClassificationPermissionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("UnitTokens");
            this.Property(t => t.UnitId).HasColumnName("UnitId");
            this.Property(t => t.Token).HasColumnName("Token");
            this.Property(t => t.CreateToken).HasColumnName("CreateToken");
            this.Property(t => t.ClassificationPermissionId).HasColumnName("ClassificationPermissionId");

            this.HasRequired(t => t.ClassificationPermission)
                .WithMany()
                .HasForeignKey(d => d.ClassificationPermissionId);
        }
    }
}
