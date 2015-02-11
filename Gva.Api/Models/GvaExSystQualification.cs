using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;
using System;
using Docs.Api.Models;

namespace Gva.Api.Models
{
    public partial class GvaExSystQualification
    {
        public GvaExSystQualification()
        {
            this.CertCampaigns = new List<GvaExSystCertCampaign>();
            this.CertPaths = new List<GvaExSystCertPath>();
            this.Exams = new List<GvaExSystExam>();
        }

        public string Name { get; set; }

        public string Code { get; set; }

        public virtual ICollection<GvaExSystCertCampaign> CertCampaigns { get; set; }

        public virtual ICollection<GvaExSystCertPath> CertPaths { get; set; }

        public virtual ICollection<GvaExSystExam> Exams { get; set; }
    }

    public class GvaExSystQualificationMap : EntityTypeConfiguration<GvaExSystQualification>
    {
        public GvaExSystQualificationMap()
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

            // Table & Column Mappings
            this.ToTable("GvaExSystQualifications");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Code).HasColumnName("Code");
        }
    }
}
