using Regs.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Mosv.Api.Models
{
    public partial class MosvViewSignal
    {
        public int LotId { get; set; }

        public int? ApplicationDocId { get; set; }

        public string IncomingLot { get; set; }

        public string IncomingNumber { get; set; }

        public DateTime? IncomingDate { get; set; }

        public string Applicant { get; set; }

        public string Institution { get; set; }

        public string Violation { get; set; }

        public virtual Docs.Api.Models.Doc ApplicationDoc { get; set; }

        public virtual Lot Lot { get; set; }
    }

    public class MosvViewSignalMap : EntityTypeConfiguration<MosvViewSignal>
    {
        public MosvViewSignalMap()
        {
            // Primary Key
            this.HasKey(t => t.LotId);

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("MosvViewSignals");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.ApplicationDocId).HasColumnName("ApplicationDocId");
            this.Property(t => t.IncomingLot).HasColumnName("IncomingLot");
            this.Property(t => t.IncomingNumber).HasColumnName("IncomingNumber");
            this.Property(t => t.IncomingDate).HasColumnName("IncomingDate");
            this.Property(t => t.Applicant).HasColumnName("Applicant");
            this.Property(t => t.Institution).HasColumnName("Institution");
            this.Property(t => t.Violation).HasColumnName("Violation");

            // Relationships
            this.HasRequired(t => t.Lot)
                .WithOptional();
            this.HasOptional(t => t.ApplicationDoc)
                .WithMany()
                .HasForeignKey(t => t.ApplicationDocId);
        }
    }
}
