using System;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocWorkflow
    {
        public int DocWorkflowId { get; set; }

        public int DocId { get; set; }

        public int DocWorkflowActionId { get; set; }

        public DateTime EventDate { get; set; }

        public bool? YesNo { get; set; }

        public int UnitUserId { get; set; }

        public int? ToUnitId { get; set; }

        public int? PrincipalUnitId { get; set; }

        public string Note { get; set; }

        public byte[] Version { get; set; }

        public virtual Doc Doc { get; set; }

        public virtual DocWorkflowAction DocWorkflowAction { get; set; }

        public virtual Common.Api.Models.Unit ToUnit { get; set; }

        public virtual Common.Api.Models.Unit PrincipalUnit { get; set; }

        public virtual Common.Api.Models.UnitUser UnitUser { get; set; }
    }

    public class DocWorkflowMap : EntityTypeConfiguration<DocWorkflow>
    {
        public DocWorkflowMap()
        {
            // Primary Key
            this.HasKey(t => t.DocWorkflowId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DocWorkflows");
            this.Property(t => t.DocWorkflowId).HasColumnName("DocWorkflowId");
            this.Property(t => t.DocId).HasColumnName("DocId");
            this.Property(t => t.DocWorkflowActionId).HasColumnName("DocWorkflowActionId");
            this.Property(t => t.EventDate).HasColumnName("EventDate");
            this.Property(t => t.YesNo).HasColumnName("YesNo");
            this.Property(t => t.UnitUserId).HasColumnName("UnitUserId");
            this.Property(t => t.ToUnitId).HasColumnName("ToUnitId");
            this.Property(t => t.PrincipalUnitId).HasColumnName("PrincipalUnitId");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Doc)
                .WithMany(t => t.DocWorkflows)
                .HasForeignKey(d => d.DocId)
                .WillCascadeOnDelete();
            this.HasRequired(t => t.DocWorkflowAction)
                .WithMany(t => t.DocWorkflows)
                .HasForeignKey(d => d.DocWorkflowActionId);
            this.HasOptional(t => t.ToUnit)
                .WithMany()
                .HasForeignKey(d => d.ToUnitId);
            this.HasOptional(t => t.PrincipalUnit)
                .WithMany()
                .HasForeignKey(d => d.PrincipalUnitId);
            this.HasRequired(t => t.UnitUser)
                .WithMany()
                .HasForeignKey(d => d.UnitUserId);
        }
    }
}
