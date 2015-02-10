using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;
using System;
using Docs.Api.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Gva.Api.Models.Views.Person;

namespace Gva.Api.Models.Views
{
    public partial class GvaViewPersonApplicationTest
    {
        public int LotId { get; set; }

        public int GvaApplicationId { get; set; }

        public string CertCampCode { get; set; }

        public string CertCampName { get; set; }

        public string TestCode { get; set; }

        public string TestName { get; set; }

        public DateTime TestDate { get; set; }

        public virtual GvaApplication Application { get; set; }

        public virtual GvaViewPerson Person { get; set; }
    }

    public class GvaViewPersonApplicationTestMap : EntityTypeConfiguration<GvaViewPersonApplicationTest>
    {
        public GvaViewPersonApplicationTestMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.GvaApplicationId, t.TestCode });

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.GvaApplicationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CertCampCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CertCampName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TestCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TestName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GvaViewPersonApplicationTests");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.GvaApplicationId).HasColumnName("GvaApplicationId");
            this.Property(t => t.CertCampCode).HasColumnName("CertCampCode");
            this.Property(t => t.CertCampName).HasColumnName("CertCampName");
            this.Property(t => t.TestCode).HasColumnName("TestCode");
            this.Property(t => t.TestName).HasColumnName("TestName");
            this.Property(t => t.TestDate).HasColumnName("TestDate");

            // Relationships
            this.HasRequired(t => t.Application)
                .WithMany(t => t.ApplicationTests)
                .HasForeignKey(d => d.GvaApplicationId);
            this.HasRequired(t => t.Person)
                .WithMany(t => t.ApplicationTests)
                .HasForeignKey(d => d.LotId);

        }
    }
}
