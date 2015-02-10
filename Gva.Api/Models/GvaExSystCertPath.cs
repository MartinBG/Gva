using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;
using System;
using Docs.Api.Models;

namespace Gva.Api.Models
{
    public partial class GvaExSystCertPath
    {
        public string Name { get; set; }

        public int Code { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        public string QualificationCode { get; set; }

        public string TestCode { get; set; }

        public virtual GvaExSystQualification Qualification { get; set; }

        public virtual GvaExSystTest Test { get; set; }
    }

    public class GvaExSystCertPathMap : EntityTypeConfiguration<GvaExSystCertPath>
    {
        public GvaExSystCertPathMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Code, t.QualificationCode, t.TestCode });

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Code)
               .IsRequired();

            this.Property(t => t.QualificationCode)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.TestCode)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("GvaExSystCertPaths");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.ValidFrom).HasColumnName("ValidFrom");
            this.Property(t => t.ValidTo).HasColumnName("ValidTo");
            this.Property(t => t.QualificationCode).HasColumnName("QualificationCode");
            this.Property(t => t.TestCode).HasColumnName("TestCode");

            // Relationships
            this.HasRequired(t => t.Qualification)
                .WithMany(t => t.CertPaths)
                .HasForeignKey(d => d.QualificationCode);

            this.HasRequired(t => t.Test)
                .WithMany(t => t.CertPaths)
                .HasForeignKey(d => new { d.TestCode, d.QualificationCode });

        }
    }
}
