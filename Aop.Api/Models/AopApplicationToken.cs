using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Aop.Api.Models
{
    public partial class AopApplicationToken
    {
        public int AopApplicationId { get; set; }
        public string Token { get; set; }
        public string CreateToken { get; set; }
    }

    public class AopApplicationTokenMap : EntityTypeConfiguration<AopApplicationToken>
    {
        public AopApplicationTokenMap()
        {
            // Primary Key
            this.HasKey(t => new { t.AopApplicationId, t.Token, t.CreateToken });

            // Properties
            this.Property(t => t.AopApplicationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Token)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.CreateToken)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("AopApplicationTokens");
            this.Property(t => t.AopApplicationId).HasColumnName("AopApplicationId");
            this.Property(t => t.Token).HasColumnName("Token");
            this.Property(t => t.CreateToken).HasColumnName("CreateToken");
        }
    }
}
