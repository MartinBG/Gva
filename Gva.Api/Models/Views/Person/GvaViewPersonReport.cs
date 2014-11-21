using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;
using Regs.Api.Models;

namespace Gva.Api.Models.Views.Person
{
    public partial class GvaViewPersonReport
    {
        public int LotId { get; set; }

        public int PartIndex { get; set; }

        public string ReportNumber { get; set; }

        public DateTime? Date { get; set; }

        public string Publisher { get; set; }

        public virtual GvaViewPerson Person { get; set; }

        public virtual ICollection<GvaViewPersonReportCheck> ReportsChecks { get; set; }

    }

    public class GvaViewPersonReportMap : EntityTypeConfiguration<GvaViewPersonReport>
    {
        public GvaViewPersonReportMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.PartIndex });

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PartIndex)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Publisher)
                .HasMaxLength(100);

            this.Property(t => t.ReportNumber)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GvaViewPersonReports");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartIndex).HasColumnName("PartIndex");
            this.Property(t => t.Publisher).HasColumnName("Publisher");
            this.Property(t => t.ReportNumber).HasColumnName("ReportNumber");
            this.Property(t => t.Date).HasColumnName("Date");

            //Relationships
            this.HasRequired(t => t.Person)
                .WithMany(t => t.Reports)
                .HasForeignKey(t => t.LotId);

        }
    }
}
