using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocType
    {
        public DocType()
        {
            this.Docs = new List<Doc>();
            this.DocTypeClassifications = new List<DocTypeClassification>();
            this.DocTypeUnitRoles = new List<DocTypeUnitRole>();
            this.ElectronicServiceStages = new List<ElectronicServiceStage>();
        }

        public int DocTypeId { get; set; }

        public int DocTypeGroupId { get; set; }

        public int? PrimaryRegisterIndexId { get; set; }

        public int? SecondaryRegisterIndexId { get; set; }

        public string Name { get; set; }

        public string ApplicationName { get; set; }

        public string Alias { get; set; }

        public bool IsElectronicService { get; set; }

        public string ElectronicServiceFileTypeUri { get; set; }

        public string ElectronicServiceTypeApplication { get; set; }

        public string ElectronicServiceProvider { get; set; }

        public int? ExecutionDeadline { get; set; }

        public int? RemoveIrregularitiesDeadline { get; set; }

        public bool IsActive { get; set; }

        public byte[] Version { get; set; }

        public virtual ICollection<Doc> Docs { get; set; }

        public virtual ICollection<DocTypeClassification> DocTypeClassifications { get; set; }

        public virtual DocTypeGroup DocTypeGroup { get; set; }

        public virtual RegisterIndex RegisterIndex { get; set; }

        public virtual RegisterIndex RegisterIndex1 { get; set; }

        public virtual ICollection<DocTypeUnitRole> DocTypeUnitRoles { get; set; }

        public virtual ICollection<ElectronicServiceStage> ElectronicServiceStages { get; set; }
    }

    public class DocTypeMap : EntityTypeConfiguration<DocType>
    {
        public DocTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.DocTypeId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.ApplicationName)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.Alias)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.ElectronicServiceFileTypeUri)
                .HasMaxLength(200);

            this.Property(t => t.ElectronicServiceTypeApplication)
                .HasMaxLength(200);

            this.Property(t => t.ElectronicServiceProvider)
                .HasMaxLength(200);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DocTypes");
            this.Property(t => t.DocTypeId).HasColumnName("DocTypeId");
            this.Property(t => t.DocTypeGroupId).HasColumnName("DocTypeGroupId");
            this.Property(t => t.PrimaryRegisterIndexId).HasColumnName("PrimaryRegisterIndexId");
            this.Property(t => t.SecondaryRegisterIndexId).HasColumnName("SecondaryRegisterIndexId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ApplicationName).HasColumnName("ApplicationName");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.IsElectronicService).HasColumnName("IsElectronicService");
            this.Property(t => t.ElectronicServiceFileTypeUri).HasColumnName("ElectronicServiceFileTypeUri");
            this.Property(t => t.ElectronicServiceTypeApplication).HasColumnName("ElectronicServiceTypeApplication");
            this.Property(t => t.ElectronicServiceProvider).HasColumnName("ElectronicServiceProvider");
            this.Property(t => t.ExecutionDeadline).HasColumnName("ExecutionDeadline");
            this.Property(t => t.RemoveIrregularitiesDeadline).HasColumnName("RemoveIrregularitiesDeadline");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.DocTypeGroup)
                .WithMany(t => t.DocTypes)
                .HasForeignKey(d => d.DocTypeGroupId);
            this.HasOptional(t => t.RegisterIndex)
                .WithMany(t => t.DocTypes)
                .HasForeignKey(d => d.PrimaryRegisterIndexId);
            this.HasOptional(t => t.RegisterIndex1)
                .WithMany(t => t.DocTypes1)
                .HasForeignKey(d => d.SecondaryRegisterIndexId);
        }
    }
}
