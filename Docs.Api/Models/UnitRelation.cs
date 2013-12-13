using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class UnitRelation
    {
        public int UnitRelationId { get; set; }
        public int UnitId { get; set; }
        public Nullable<int> ParentUnitId { get; set; }
        public int RootUnitId { get; set; }
        public byte[] Version { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual Unit Unit1 { get; set; }
        public virtual Unit Unit2 { get; set; }
    }

    public class UnitRelationMap : EntityTypeConfiguration<UnitRelation>
    {
        public UnitRelationMap()
        {
            // Primary Key
            this.HasKey(t => t.UnitRelationId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("UnitRelations");
            this.Property(t => t.UnitRelationId).HasColumnName("UnitRelationId");
            this.Property(t => t.UnitId).HasColumnName("UnitId");
            this.Property(t => t.ParentUnitId).HasColumnName("ParentUnitId");
            this.Property(t => t.RootUnitId).HasColumnName("RootUnitId");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Unit)
                .WithMany(t => t.UnitRelations)
                .HasForeignKey(d => d.UnitId);
            this.HasOptional(t => t.Unit1)
                .WithMany(t => t.UnitRelations1)
                .HasForeignKey(d => d.ParentUnitId);
            this.HasRequired(t => t.Unit2)
                .WithMany(t => t.UnitRelations2)
                .HasForeignKey(d => d.RootUnitId);

        }
    }
}
