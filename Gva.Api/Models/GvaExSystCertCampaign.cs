using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;
using System;
using Docs.Api.Models;

namespace Gva.Api.Models
{
    public partial class GvaExSystCertCampaign
    {
        public GvaExSystCertCampaign()
        {
            this.Examinees = new List<GvaExSystExaminee>();
        }

        public string Name { get; set; }

        public string Code { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        public string QualificationCode { get; set; }

        public virtual GvaExSystQualification Qualification { get; set; }

        public virtual ICollection<GvaExSystExaminee> Examinees { get; set; }
    }

    public class GvaExSystCertCampaignMap : EntityTypeConfiguration<GvaExSystCertCampaign>
    {
        public GvaExSystCertCampaignMap()
        {
            // Primary Key
            this.HasKey(t => t.Code);

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
            this.ToTable("GvaExSystCertCampaigns");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.ValidFrom).HasColumnName("ValidFrom");
            this.Property(t => t.ValidTo).HasColumnName("ValidTo");
            this.Property(t => t.QualificationCode).HasColumnName("QualificationCode");

            // Relationships
            this.HasRequired(t => t.Qualification)
                .WithMany(t => t.CertCampaigns)
                .HasForeignKey(d => d.QualificationCode);

        }
    }
}
