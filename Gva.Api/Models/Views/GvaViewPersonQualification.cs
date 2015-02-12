using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;
using System;
using Docs.Api.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Gva.Api.Models.Views.Person;
using Common.Api.Models;

namespace Gva.Api.Models.Views
{
    public partial class GvaViewPersonQualification
    {
        public int LotId { get; set; }

        public string QualificationCode { get; set; }

        public string QualificationName { get; set; }

        public string LicenceTypeCode { get; set; }

        public string State { get; set; }

        public string StateMethod { get; set; }

        public virtual GvaViewPerson Person { get; set; }
    }

    public class GvaViewPersonQualificationMap : EntityTypeConfiguration<GvaViewPersonQualification>
    {
        public GvaViewPersonQualificationMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.QualificationCode });

            // Table & Column Mappings
            this.ToTable("GvaViewPersonQualifications");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.QualificationCode).HasColumnName("QualificationCode");
            this.Property(t => t.QualificationName).HasColumnName("QualificationName");
            this.Property(t => t.LicenceTypeCode).HasColumnName("LicenceTypeCode");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.StateMethod).HasColumnName("StateMethod");

            // Relationships
            this.HasRequired(t => t.Person)
                .WithMany(t => t.Qualifications)
                .HasForeignKey(d => d.LotId);
        }
    }
}
