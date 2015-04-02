using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;
using Regs.Api.Models;

namespace Gva.Api.Models
{
    public partial class GvaInvalidActNumber
    {
        public int ActNumber { get; set; }

        public string Reason { get; set; }

        public int RegisterId { get; set; }

        public virtual NomValue Register { get; set; }
    }

    public class GvaInvalidActNumberMap : EntityTypeConfiguration<GvaInvalidActNumber>
    {
        public GvaInvalidActNumberMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ActNumber, t.RegisterId });

            // Properties
            this.Property(t => t.ActNumber)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Reason)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("GvaInvalidActNumbers");
            this.Property(t => t.ActNumber).HasColumnName("ActNumber");
            this.Property(t => t.Reason).HasColumnName("Reason");
            this.Property(t => t.RegisterId).HasColumnName("RegisterId");

            this.HasRequired(t => t.Register)
                .WithMany()
                .HasForeignKey(t => t.RegisterId);
        }
    }
}
