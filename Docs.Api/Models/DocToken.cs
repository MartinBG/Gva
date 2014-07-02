using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocToken
    {
        public int DocId { get; set; }
        public string Token { get; set; }
        public string CreateToken { get; set; }
    }

    public class DocTokenMap : EntityTypeConfiguration<DocToken>
    {
        public DocTokenMap()
        {
            // Primary Key
            this.HasKey(t => new { t.DocId, t.Token, t.CreateToken });

            // Properties
            this.Property(t => t.DocId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Token)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.CreateToken)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("DocTokens");
            this.Property(t => t.DocId).HasColumnName("DocId");
            this.Property(t => t.Token).HasColumnName("Token");
            this.Property(t => t.CreateToken).HasColumnName("CreateToken");
        }
    }
}
