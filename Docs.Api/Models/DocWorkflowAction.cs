using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocWorkflowAction
    {
        public DocWorkflowAction()
        {
            this.DocWorkflows = new List<DocWorkflow>();
        }

        public int DocWorkflowActionId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual ICollection<DocWorkflow> DocWorkflows { get; set; }
    }

    public class DocWorkflowActionMap : EntityTypeConfiguration<DocWorkflowAction>
    {
        public DocWorkflowActionMap()
        {
            // Primary Key
            this.HasKey(t => t.DocWorkflowActionId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Alias)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DocWorkflowActions");
            this.Property(t => t.DocWorkflowActionId).HasColumnName("DocWorkflowActionId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
