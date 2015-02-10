using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;
using System;
using Docs.Api.Models;
using Gva.Api.Models.Views.Person;

namespace Gva.Api.Models
{
    public partial class GvaExSystExaminee
    {
        public int GvaExSystExamineeId { get; set; }

        public string Uin { get; set; }

        public int? Lin { get; set; }

        public string TestCode { get; set; }

        public DateTime EndTime { get; set; }

        public string TotalScore { get; set; }

        public string ResultStatus { get; set; }

        public string CertCampCode { get; set; }

        public int LotId { get; set; }

        public virtual GvaExSystCertCampaign CertCampaign { get; set; }

        public virtual GvaViewPerson Person { get; set; }
    }

    public class GvaExSystExamineeMap : EntityTypeConfiguration<GvaExSystExaminee>
    {
        public GvaExSystExamineeMap()
        {
            // Primary Key
            this.HasKey(t => t.GvaExSystExamineeId);

            // Properties
            this.Property(t => t.Uin)
                .HasMaxLength(50);

            this.Property(t => t.TotalScore)
                .HasMaxLength(10);

            this.Property(t => t.TestCode)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.ResultStatus)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CertCampCode)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("GvaExSystExaminees");
            this.Property(t => t.GvaExSystExamineeId).HasColumnName("GvaExSystExamineeId");
            this.Property(t => t.Uin).HasColumnName("Uin");
            this.Property(t => t.Lin).HasColumnName("Lin");
            this.Property(t => t.TestCode).HasColumnName("TestCode");
            this.Property(t => t.EndTime).HasColumnName("EndTime");
            this.Property(t => t.TotalScore).HasColumnName("TotalScore");
            this.Property(t => t.ResultStatus).HasColumnName("ResultStatus");
            this.Property(t => t.CertCampCode).HasColumnName("CertCampCode");
            this.Property(t => t.LotId).HasColumnName("LotId");

            // Relationships
            this.HasOptional(t => t.CertCampaign)
                .WithMany(t => t.Examinees)
                .HasForeignKey(d => d.CertCampCode);

            this.HasRequired(t => t.Person)
                .WithMany(t => t.Examinees)
                .HasForeignKey(t => t.LotId);

        }
    }
}
