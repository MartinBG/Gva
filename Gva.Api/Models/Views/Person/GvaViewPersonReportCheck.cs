using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;
using Regs.Api.Models;

namespace Gva.Api.Models.Views.Person
{
    public partial class GvaViewPersonReportCheck
    {
        public int CheckLotId { get; set; }

        public int CheckPartIndex { get; set; }

        public int ReportLotId { get; set; }

        public int ReportPartIndex { get; set; }

        public int GvaViewPersonReportsChecksId { get; set; }

        public virtual GvaViewPersonCheck Check { get; set; }

        public virtual GvaViewPersonReport Report { get; set; }

    }

    public class GvaViewPersonReportCheckMap : EntityTypeConfiguration<GvaViewPersonReportCheck>
    {
        public GvaViewPersonReportCheckMap()
        {
            // Primary Key
            this.HasKey(t => new { t.GvaViewPersonReportsChecksId, t.CheckLotId, t.ReportLotId, t.CheckPartIndex, t.ReportPartIndex });

            this.Property(r => r.GvaViewPersonReportsChecksId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("GvaViewPersonReportsChecks");
            this.Property(t => t.CheckLotId).HasColumnName("CheckLotId");
            this.Property(t => t.CheckPartIndex).HasColumnName("CheckPartIndex");
            this.Property(t => t.ReportLotId).HasColumnName("ReportLotId");
            this.Property(t => t.ReportPartIndex).HasColumnName("ReportPartIndex");

            //Relationships
            this.HasRequired(t => t.Check)
                .WithMany(t => t.ReportsChecks)
                .HasForeignKey(t => new { t.CheckLotId, t.CheckPartIndex });

            this.HasRequired(t => t.Report)
                .WithMany(t => t.ReportsChecks)
                .HasForeignKey(t => new { t.ReportLotId, t.ReportPartIndex });

        }
    }
}
