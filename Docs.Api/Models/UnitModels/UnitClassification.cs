using Docs.Api.Models.ClassificationModels;
using Docs.Api.Models.UnitModels;
using System;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models.UnitModels
{
    public partial class UnitClassification : IEquatable<UnitClassification>
    {
        public int UnitClassificationId { get; set; }

        public int UnitId { get; set; }

        public int ClassificationId { get; set; }

        public int ClassificationPermissionId { get; set; }

        public byte[] Version { get; set; }

        public virtual ClassificationPermission ClassificationPermission { get; set; }

        public virtual Classification Classification { get; set; }

        public virtual Unit Unit { get; set; }

        public bool Equals(UnitClassification other)
        {
            if (other == null) return false;
            return (ClassificationId.Equals(other.ClassificationId)
                && ClassificationPermissionId.Equals(other.ClassificationPermissionId));
        }
    }

    public class UnitClassificationMap : EntityTypeConfiguration<UnitClassification>
    {
        public UnitClassificationMap()
        {
            // Primary Key
            this.HasKey(t => t.UnitClassificationId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("UnitClassifications");
            this.Property(t => t.UnitClassificationId).HasColumnName("UnitClassificationId");
            this.Property(t => t.UnitId).HasColumnName("UnitId");
            this.Property(t => t.ClassificationId).HasColumnName("ClassificationId");
            this.Property(t => t.ClassificationPermissionId).HasColumnName("ClassificationPermissionId");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.ClassificationPermission)
                .WithMany()
                .HasForeignKey(d => d.ClassificationPermissionId);
            this.HasRequired(t => t.Classification)
                .WithMany(t => t.UnitClassifications)
                .HasForeignKey(d => d.ClassificationId);
            this.HasRequired(t => t.Unit)
                .WithMany(t => t.UnitClassifications)
                .HasForeignKey(d => d.UnitId);
        }
    }
}
