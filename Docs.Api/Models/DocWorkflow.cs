using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocWorkflow
    {
        public int DocWorkflowId { get; set; }
        public int DocId { get; set; }
        public int DocWorkflowActionId { get; set; }
        public System.DateTime EventDate { get; set; }
        public Nullable<bool> YesNo { get; set; }
        public int UserId { get; set; }
        public Nullable<int> ToUnitId { get; set; }
        public Nullable<int> PrincipalUnitId { get; set; }
        public string Note { get; set; }
        public byte[] Version { get; set; }
        public virtual Doc Doc { get; set; }
        public virtual DocWorkflowAction DocWorkflowAction { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual Unit Unit1 { get; set; }
        public virtual Common.Api.Models.User User { get; set; }
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
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.ToUnitId).HasColumnName("ToUnitId");
            this.Property(t => t.PrincipalUnitId).HasColumnName("PrincipalUnitId");
            this.Property(t => t.Note).HasColumnName("Note");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Doc)
                .WithMany(t => t.DocWorkflows)
                .HasForeignKey(d => d.DocId);
            this.HasRequired(t => t.DocWorkflowAction)
                .WithMany(t => t.DocWorkflows)
                .HasForeignKey(d => d.DocWorkflowActionId);
            this.HasOptional(t => t.Unit)
                .WithMany(t => t.DocWorkflows)
                .HasForeignKey(d => d.ToUnitId);
            this.HasOptional(t => t.Unit1)
                .WithMany(t => t.DocWorkflows1)
                .HasForeignKey(d => d.PrincipalUnitId);
            this.HasRequired(t => t.User)
                .WithMany()
                .HasForeignKey(d => d.UserId);

        }
    }
}
