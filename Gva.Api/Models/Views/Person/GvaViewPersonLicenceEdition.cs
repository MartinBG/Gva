using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;
using Regs.Api.Models;
using Gva.Api.ModelsDO.Persons;

namespace Gva.Api.Models.Views.Person
{
    public partial class GvaViewPersonLicenceEdition
    {

        public int LotId { get; set; }

        public int PartId { get; set; }

        public int EditionIndex { get; set; }

        public int LicenceTypeId { get; set; }

        public string StampNumber { get; set; }

        public DateTime DateValidFrom { get; set; }

        public DateTime? DateValidTo { get; set; }

        public int LicenceActionId { get; set; }

        public int LicenceNumber { get; set; }

        public bool IsLastEdition { get; set; }

        public int? GvaApplicationId { get; set; }

        public string ApplicationName { get; set; }

        public int? ApplicationPartIndex { get; set; }

        public int LicencePartIndex { get; set; }

        public int EditionPartIndex { get; set; }

        public DateTime FirstDocDateValidFrom { get; set; }

        public bool Valid { get; set; }

        public virtual GvaViewPerson Person { get; set; }

        public virtual Part Part { get; set; }

        public virtual NomValue LicenceType { get; set; }

        public virtual NomValue LicenceAction { get; set; }

        public virtual GvaApplication Application { get; set; }

    }

    public class GvaViewPersonLicenceEditionMap : EntityTypeConfiguration<GvaViewPersonLicenceEdition>
    {
        public GvaViewPersonLicenceEditionMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.PartId, t.EditionIndex });

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PartId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.EditionIndex)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.StampNumber)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GvaViewPersonLicenceEditions");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartId).HasColumnName("LotPartId");
            this.Property(t => t.EditionIndex).HasColumnName("EditionIndex");
            this.Property(t => t.LicenceTypeId).HasColumnName("LicenceTypeId");
            this.Property(t => t.StampNumber).HasColumnName("StampNumber");
            this.Property(t => t.DateValidFrom).HasColumnName("DateValidFrom");
            this.Property(t => t.DateValidTo).HasColumnName("DateValidTo");
            this.Property(t => t.LicenceActionId).HasColumnName("LicenceActionId");
            this.Property(t => t.LicenceNumber).HasColumnName("LicenceNumber");
            this.Property(t => t.IsLastEdition).HasColumnName("IsLastEdition");
            this.Property(t => t.GvaApplicationId).HasColumnName("GvaApplicationId");
            this.Property(t => t.ApplicationName).HasColumnName("ApplicationName");
            this.Property(t => t.ApplicationPartIndex).HasColumnName("ApplicationPartIndex");
            this.Property(t => t.LicencePartIndex).HasColumnName("LicencePartIndex");
            this.Property(t => t.EditionPartIndex).HasColumnName("EditionPartIndex");
            this.Property(t => t.FirstDocDateValidFrom).HasColumnName("FirstDocDateValidFrom");
            this.Property(t => t.Valid).HasColumnName("Valid");
            
            // Relationships
            this.HasRequired(t => t.Person)
                .WithMany(d => d.PersonLicenceEditions)
                .HasForeignKey(t => t.LotId);
            this.HasRequired(t => t.Part)
                .WithMany()
                .HasForeignKey(t => t.PartId);
            this.HasRequired(t => t.LicenceType)
                .WithMany()
                .HasForeignKey(t => t.LicenceTypeId);
            this.HasRequired(t => t.LicenceAction)
                .WithMany()
                .HasForeignKey(t => t.LicenceActionId);
            this.HasOptional(t => t.Application)
                .WithMany(d => d.PersonLicenceEditions)
                .HasForeignKey(t => t.GvaApplicationId);
        }
    }
}
