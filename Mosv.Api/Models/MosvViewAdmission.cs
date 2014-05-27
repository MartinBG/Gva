using Regs.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Mosv.Api.Models
{
    public partial class MosvViewAdmission
    {
        public int LotId { get; set; }

        public string IncomingLot { get; set; }

        public string IncomingNumber { get; set; }

        public DateTime? IncomingDate { get; set; }

        public string ApplicantType { get; set; }

        public string Applicant { get; set; }

        public virtual Lot Lot { get; set; }
    }

    public class MosvViewAdmissionMap : EntityTypeConfiguration<MosvViewAdmission>
    {
        public MosvViewAdmissionMap()
        {
            // Primary Key
            this.HasKey(t => t.LotId);

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("MosvViewAdmissions");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.IncomingLot).HasColumnName("IncomingLot");
            this.Property(t => t.IncomingNumber).HasColumnName("IncomingNumber");
            this.Property(t => t.IncomingDate).HasColumnName("IncomingDate");
            this.Property(t => t.ApplicantType).HasColumnName("ApplicantType");
            this.Property(t => t.Applicant).HasColumnName("Applicant");

            // Relationships
            this.HasRequired(t => t.Lot)
                .WithOptional();
        }
    }
}