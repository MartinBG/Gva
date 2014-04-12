using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;
using System;

namespace Gva.Api.Models
{
    public partial class GvaViewAircraftAw
    {
        public int LotPartId { get; set; }

        public int LotId { get; set; }

        public int RegId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime ValidFromDate { get; set; }

        public DateTime ValidToDate { get; set; }

        public string Inspector { get; set; }

        public DateTime? EASA15IssueDate { get; set; }

        public DateTime? EASA15IssueValidToDate { get; set; }

        public virtual Lot Lot { get; set; }
        
        public virtual Part Part { get; set; }
    }

    public class GvaViewAircraftAwMap : EntityTypeConfiguration<GvaViewAircraftAw>
    {
        public GvaViewAircraftAwMap()
        {
            // Primary Key
            this.HasKey(t => t.LotPartId);

            // Properties
            this.Property(t => t.LotPartId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("GvaViewAircraftAws");
            this.Property(t => t.LotPartId).HasColumnName("LotPartId");
            this.Property(t => t.RegId).HasColumnName("RegId");
            this.Property(t => t.IssueDate).HasColumnName("IssueDate");
            this.Property(t => t.ValidFromDate).HasColumnName("ValidFromDate");
            this.Property(t => t.ValidToDate).HasColumnName("ValidToDate");
            this.Property(t => t.Inspector).HasColumnName("Inspector");
            this.Property(t => t.EASA15IssueDate).HasColumnName("EASA15IssueDate");
            this.Property(t => t.EASA15IssueValidToDate).HasColumnName("EASA15IssueValidToDate");

            // Relationships
            this.HasRequired(t => t.Lot)
                .WithMany()
                .HasForeignKey(t => t.LotId);

            this.HasRequired(t => t.Part)
                .WithOptional();
        }
    }
}
