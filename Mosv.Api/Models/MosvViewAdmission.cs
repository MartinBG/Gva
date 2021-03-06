﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Docs.Api.Models;
using Regs.Api.Models;
using Regs.Api.LotEvents;

namespace Mosv.Api.Models
{
    public partial class MosvViewAdmission : IProjectionView
    {
        public int LotId { get; set; }

        public int? ApplicationDocId { get; set; }

        public string IncomingLot { get; set; }

        public string IncomingNumber { get; set; }

        public DateTime? IncomingDate { get; set; }

        public string ApplicantType { get; set; }

        public string Applicant { get; set; }

        public virtual Doc ApplicationDoc { get; set; }

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
            this.Property(t => t.ApplicationDocId).HasColumnName("ApplicationDocId");
            this.Property(t => t.IncomingLot).HasColumnName("IncomingLot");
            this.Property(t => t.IncomingNumber).HasColumnName("IncomingNumber");
            this.Property(t => t.IncomingDate).HasColumnName("IncomingDate");
            this.Property(t => t.ApplicantType).HasColumnName("ApplicantType");
            this.Property(t => t.Applicant).HasColumnName("Applicant");

            // Relationships
            this.HasRequired(t => t.Lot)
                .WithOptional();
            this.HasOptional(t => t.ApplicationDoc)
                .WithMany()
                .HasForeignKey(t => t.ApplicationDocId);
        }
    }
}