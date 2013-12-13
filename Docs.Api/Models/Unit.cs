using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class Unit
    {
        public Unit()
        {
            this.DocTypeUnitRoles = new List<DocTypeUnitRole>();
            this.DocUnits = new List<DocUnit>();
            this.DocUsers = new List<DocUser>();
            this.DocWorkflows = new List<DocWorkflow>();
            this.DocWorkflows1 = new List<DocWorkflow>();
            this.ElectronicServiceStageExecutors = new List<ElectronicServiceStageExecutor>();
            this.UnitClassifications = new List<UnitClassification>();
            this.UnitRelations = new List<UnitRelation>();
            this.UnitRelations1 = new List<UnitRelation>();
            this.UnitRelations2 = new List<UnitRelation>();
        }

        public int UnitId { get; set; }
        public string Name { get; set; }
        public int UnitTypeId { get; set; }
        public bool InheritParentClassification { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual ICollection<DocTypeUnitRole> DocTypeUnitRoles { get; set; }
        public virtual ICollection<DocUnit> DocUnits { get; set; }
        public virtual ICollection<DocUser> DocUsers { get; set; }
        public virtual ICollection<DocWorkflow> DocWorkflows { get; set; }
        public virtual ICollection<DocWorkflow> DocWorkflows1 { get; set; }
        public virtual ICollection<ElectronicServiceStageExecutor> ElectronicServiceStageExecutors { get; set; }
        public virtual ICollection<UnitClassification> UnitClassifications { get; set; }
        public virtual ICollection<UnitRelation> UnitRelations { get; set; }
        public virtual ICollection<UnitRelation> UnitRelations1 { get; set; }
        public virtual ICollection<UnitRelation> UnitRelations2 { get; set; }
        public virtual UnitType UnitType { get; set; }
    }

    public class UnitMap : EntityTypeConfiguration<Unit>
    {
        public UnitMap()
        {
            // Primary Key
            this.HasKey(t => t.UnitId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Units");
            this.Property(t => t.UnitId).HasColumnName("UnitId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.UnitTypeId).HasColumnName("UnitTypeId");
            this.Property(t => t.InheritParentClassification).HasColumnName("InheritParentClassification");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.UnitType)
                .WithMany(t => t.Units)
                .HasForeignKey(d => d.UnitTypeId);

        }
    }
}
