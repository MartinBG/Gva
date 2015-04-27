using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;
using Regs.Api.Models;
using Regs.Api.LotEvents;

namespace Gva.Api.Models.Views.Organization
{
    public partial class GvaViewOrganizationApproval : IProjectionView
    {
        public int LotId { get; set; }

        public int PartId { get; set; }

        public int PartIndex { get; set; }

        public string DocumentNumber { get; set; }

        public int? ApprovalTypeId { get; set; }

        public int? ApprovalStateId { get; set; }

        public virtual NomValue ApprovalType { get; set; }

        public virtual NomValue ApprovalState { get; set; }

        public virtual GvaViewOrganization Organization { get; set; }

        public virtual List<GvaViewOrganizationAmendment> Amendments { get; set; }
    }

    public class GvaViewOrganizationApprovalMap : EntityTypeConfiguration<GvaViewOrganizationApproval>
    {
        public GvaViewOrganizationApprovalMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.PartIndex });

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PartIndex)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DocumentNumber)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GvaViewOrganizationApprovals");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartIndex).HasColumnName("PartIndex");
            this.Property(t => t.PartId).HasColumnName("PartId");
            this.Property(t => t.DocumentNumber).HasColumnName("DocumentNumber");
            this.Property(t => t.ApprovalTypeId).HasColumnName("ApprovalTypeId");
            this.Property(t => t.ApprovalStateId).HasColumnName("ApprovalStateId");

            // Relationships
            this.HasOptional(t => t.ApprovalState)
                .WithMany()
                .HasForeignKey(t => t.ApprovalStateId);
            this.HasOptional(t => t.ApprovalType)
                .WithMany()
                .HasForeignKey(t => t.ApprovalTypeId);
        }
    }
}
