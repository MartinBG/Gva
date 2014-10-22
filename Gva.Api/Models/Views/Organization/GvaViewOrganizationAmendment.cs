using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;
using Regs.Api.Models;

namespace Gva.Api.Models.Views.Organization
{
    public partial class GvaViewOrganizationAmendment
    {
        public int LotId { get; set; }

        public int PartId { get; set; }

        public int PartIndex { get; set; }

        public string DocumentNumber { get; set; }

        public int ApprovalPartIndex { get; set; }

        public DateTime? DocumentDateIssue { get; set; }

        public int? ChangeNum { get; set; }

        public int Index { get; set; }

        public virtual GvaViewOrganization Organization { get; set; }

        public virtual GvaViewOrganizationApproval Approval { get; set; }

        public virtual Part Part { get; set; }
    }

    public class GvaViewOrganizationAmendmentMap : EntityTypeConfiguration<GvaViewOrganizationAmendment>
    {
        public GvaViewOrganizationAmendmentMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.ApprovalPartIndex, t.PartIndex });

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PartIndex)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DocumentNumber)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GvaViewOrganizationAmendments");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartId).HasColumnName("LotPartId");
            this.Property(t => t.PartIndex).HasColumnName("PartIndex");
            this.Property(t => t.DocumentNumber).HasColumnName("DocumentNumber");
            this.Property(t => t.ApprovalPartIndex).HasColumnName("ApprovalPartIndex");
            this.Property(t => t.ChangeNum).HasColumnName("ChangeNum");
            this.Property(t => t.DocumentDateIssue).HasColumnName("DocumentDateIssue");
            this.Property(t => t.Index).HasColumnName("Index");
            
            // Relationships
            this.HasRequired(t => t.Approval)
                .WithMany(d => d.Amendments)
                .HasForeignKey(t => new { t.LotId, t.ApprovalPartIndex });
            this.HasRequired(t => t.Part)
                .WithMany()
                .HasForeignKey(t => t.PartId);
        }
    }
}
