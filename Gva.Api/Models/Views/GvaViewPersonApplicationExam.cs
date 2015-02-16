using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;
using System;
using Docs.Api.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Gva.Api.Models.Views.Person;

namespace Gva.Api.Models.Views
{
    public partial class GvaViewPersonApplicationExam
    {
        public int LotId { get; set; }

        public int AppPartId { get; set; }

        public string CertCampCode { get; set; }

        public string CertCampName { get; set; }

        public string ExamCode { get; set; }

        public string ExamName { get; set; }

        public DateTime ExamDate { get; set; }

        public virtual GvaViewApplication Application { get; set; }

        public virtual GvaViewPerson Person { get; set; }
    }

    public class GvaViewPersonApplicationExamMap : EntityTypeConfiguration<GvaViewPersonApplicationExam>
    {
        public GvaViewPersonApplicationExamMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.AppPartId, t.ExamCode, t.ExamDate });

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.AppPartId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CertCampCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CertCampName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ExamCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ExamName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GvaViewPersonApplicationExams");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.AppPartId).HasColumnName("AppPartId");
            this.Property(t => t.CertCampCode).HasColumnName("CertCampCode");
            this.Property(t => t.CertCampName).HasColumnName("CertCampName");
            this.Property(t => t.ExamCode).HasColumnName("ExamCode");
            this.Property(t => t.ExamName).HasColumnName("ExamName");
            this.Property(t => t.ExamDate).HasColumnName("ExamDate");

            // Relationships
            this.HasRequired(t => t.Application)
                .WithMany(t => t.ApplicationExams)
                .HasForeignKey(d => new { d.LotId, d.AppPartId });
            this.HasRequired(t => t.Person)
                .WithMany(t => t.ApplicationExams)
                .HasForeignKey(d => d.LotId);

        }
    }
}
