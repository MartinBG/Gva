using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;
using Regs.Api.Models;
using Regs.Api.LotEvents;

namespace Gva.Api.Models.Views.Person
{
    public partial class GvaViewPersonReportCheck : IProjectionView
    {
        public int LotId { get; set; }

        public int PartIndex { get; set; }

        public int CheckLotId { get; set; }

        public int CheckPartIndex { get; set; }

        public virtual GvaViewPersonReport Report { get; set; }
    }

    public class GvaViewPersonReportCheckMap : EntityTypeConfiguration<GvaViewPersonReportCheck>
    {
        public GvaViewPersonReportCheckMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.PartIndex, t.CheckLotId, t.CheckPartIndex });

            // Table & Column Mappings
            this.ToTable("GvaViewPersonReportsChecks");
            this.Property(t => t.LotId).HasColumnName("ReportLotId");
            this.Property(t => t.PartIndex).HasColumnName("ReportPartIndex");
            this.Property(t => t.CheckLotId).HasColumnName("CheckLotId");
            this.Property(t => t.CheckPartIndex).HasColumnName("CheckPartIndex");

            //Relationships
            this.HasRequired(t => t.Report)
                .WithMany(t => t.ReportsChecks)
                .HasForeignKey(d => new { d.LotId, d.PartIndex });
        }
    }
}
