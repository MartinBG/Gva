using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Api.Models;
using Gva.Api.Models.Views.Aircraft;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Models.Views.SModeCode
{
    public partial class GvaViewSModeCode : IProjectionView
    {
        public int LotId { get; set; }

        public int TypeId { get; set; }

        public string CodeHex { get; set; }

        public string Note { get; set; }

        public int? AircraftId { get; set; }

        public string Applicant { get; set; }

        public string TheirNumber { get; set; }

        public DateTime? TheirDate { get; set; }

        public string CaaNumber { get; set; }

        public DateTime? CaaDate { get; set; }

        public virtual GvaViewAircraft Aircraft { get; set; }

        public virtual Lot Lot { get; set; }

        public virtual NomValue Type { get; set; }
    }

    public class GvaViewSModeCodeMap : EntityTypeConfiguration<GvaViewSModeCode>
    {
        public GvaViewSModeCodeMap()
        {
            // Primary Key
            this.HasKey(t => t.LotId);

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CodeHex)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TheirNumber)
               .IsOptional()
               .HasMaxLength(50);

            this.Property(t => t.CaaNumber)
               .IsOptional()
               .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GvaViewSModeCodes");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.TypeId).HasColumnName("TypeId");
            this.Property(t => t.CodeHex).HasColumnName("CodeHex");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.AircraftId).HasColumnName("AircraftId");
            this.Property(t => t.Applicant).HasColumnName("Applicant");
            this.Property(t => t.TheirNumber).HasColumnName("TheirNumber");
            this.Property(t => t.TheirDate).HasColumnName("TheirDate");
            this.Property(t => t.CaaNumber).HasColumnName("CaaNumber");
            this.Property(t => t.CaaDate).HasColumnName("CaaDate");

            // Relationships
            this.HasRequired(t => t.Lot)
                .WithOptional();
            this.HasRequired(t => t.Type)
                .WithMany()
                .HasForeignKey(d => d.TypeId);
            this.HasRequired(t => t.Aircraft)
                .WithMany()
                .HasForeignKey(d => d.AircraftId);
        }
    }
}
