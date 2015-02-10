using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;
using System;
using Docs.Api.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Gva.Api.Models.Views.Person;

namespace Gva.Api.Models.Vew
{
    public partial class GvaViewPersonQualification
    {
        public int LotId { get; set; }

        public int ApplicationPartIndex { get; set; }

        public string QualificationCodes { get; set; }

        public virtual GvaViewPerson Person { get; set; }
    }

    public class GvaViewPersonQualificationMap : EntityTypeConfiguration<GvaViewPersonQualification>
    {
        public GvaViewPersonQualificationMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.ApplicationPartIndex });

            // Table & Column Mappings
            this.ToTable("GvaViewPersonQualifications");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.ApplicationPartIndex).HasColumnName("ApplicationPartIndex");
            this.Property(t => t.QualificationCodes).HasColumnName("QualificationCodes");

            // Relationships
            this.HasRequired(t => t.Person)
                .WithMany(t => t.Qualifications)
                .HasForeignKey(d => d.LotId);

        }
    }
}
