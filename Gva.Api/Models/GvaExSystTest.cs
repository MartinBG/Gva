using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;
using System;
using Docs.Api.Models;

namespace Gva.Api.Models
{
    public partial class GvaExSystTest
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string QualificationCode { get; set; }

        public virtual ICollection<GvaExSystCertPath> CertPaths { get; set; }

        public virtual GvaExSystQualification Qualification { get; set; }
    }

    public class GvaExSystTestMap : EntityTypeConfiguration<GvaExSystTest>
    {
        public GvaExSystTestMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Code, t.QualificationCode });

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.QualificationCode)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("GvaExSystTests");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.QualificationCode).HasColumnName("QualificationCode");

            // Relationships
            this.HasRequired(t => t.Qualification)
                .WithMany(t => t.Tests)
                .HasForeignKey(d => d.QualificationCode);

        }
    }
}
