using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;
using Regs.Api.Models;

namespace Gva.Api.Models.Views.Person
{
    public partial class GvaLicenceEdition
    {
        public int LotId { get; set; }

        public int LicencePartId { get; set; }

        public int EditionPartId { get; set; }

        public int EditionIndex { get; set; }

        public int LicenceTypeId { get; set; }

        public string StampNumber { get; set; }

        public DateTime DateValidFrom { get; set; }

        public DateTime FirstDocDateValidFrom { get; set; }

        public DateTime? DateValidTo { get; set; }

        public int LicenceActionId { get; set; }

        public int? LicenceNumber { get; set; }

        public int LicencePartIndex { get; set; }

        public int EditionPartIndex { get; set; }

        public bool Valid { get; set; }

        public bool IsLastEdition { get; set; }

        public string LicenceTypeCaCode { get; set; }

        public string PublisherCode { get; set; }

        public string ForeignLicenceNumber { get; set; }

        public string ForeignPublisher { get; set; }

        public string Notes { get; set; }

        public string Inspector { get; set; }

        public string StatusChange { get; set; }

        public string Limitations { get; set; }

        public int? GvaLotFileId { get; set; }

        public int? GvaApplicationId { get; set; }

        public int? ApplicationPartId { get; set; }

        public int? GvaStageId { get; set; }

        public int? OfficiallyReissuedStageId { get; set; }

        public virtual GvaViewPerson Person { get; set; }

        public virtual Part LicencePart { get; set; }

        public virtual Part EditionPart { get; set; }

        public virtual NomValue LicenceType { get; set; }

        public virtual NomValue LicenceAction { get; set; }

        public virtual GvaLotFile LotFile { get; set; }

        public virtual GvaApplication GvaApplication { get; set; }

        public virtual GvaViewApplication Application { get; set; }

        public virtual GvaApplicationStage Stage { get; set; }
    }

    public class GvaLicenceEditionMap : EntityTypeConfiguration<GvaLicenceEdition>
    {
        public GvaLicenceEditionMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.LicencePartId, t.EditionPartId });

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.LicencePartId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.EditionPartId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("vwGvaLicenceEditions");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.LicencePartId).HasColumnName("LicencePartId");
            this.Property(t => t.EditionPartId).HasColumnName("EditionPartId");
            this.Property(t => t.EditionIndex).HasColumnName("EditionIndex");
            this.Property(t => t.LicenceTypeId).HasColumnName("LicenceTypeId");
            this.Property(t => t.StampNumber).HasColumnName("StampNumber");
            this.Property(t => t.FirstDocDateValidFrom).HasColumnName("FirstDocDateValidFrom");
            this.Property(t => t.DateValidFrom).HasColumnName("DateValidFrom");
            this.Property(t => t.DateValidTo).HasColumnName("DateValidTo");
            this.Property(t => t.LicenceActionId).HasColumnName("LicenceActionId");
            this.Property(t => t.LicenceNumber).HasColumnName("LicenceNumber");
            this.Property(t => t.LicencePartIndex).HasColumnName("LicencePartIndex");
            this.Property(t => t.EditionPartIndex).HasColumnName("PartIndex");
            this.Property(t => t.IsLastEdition).HasColumnName("IsLastEdition");
            this.Property(t => t.Valid).HasColumnName("Valid");
            this.Property(t => t.LicenceTypeCaCode).HasColumnName("LicenceTypeCaCode");
            this.Property(t => t.PublisherCode).HasColumnName("PublisherCode");
            this.Property(t => t.ForeignLicenceNumber).HasColumnName("ForeignLicenceNumber");
            this.Property(t => t.ForeignPublisher).HasColumnName("ForeignPublisher");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.Inspector).HasColumnName("Inspector");
            this.Property(t => t.StatusChange).HasColumnName("StatusChange");
            this.Property(t => t.Limitations).HasColumnName("Limitations");
            this.Property(t => t.GvaLotFileId).HasColumnName("GvaLotFileId");
            this.Property(t => t.GvaApplicationId).HasColumnName("GvaApplicationId");
            this.Property(t => t.ApplicationPartId).HasColumnName("ApplicationPartId");
            this.Property(t => t.GvaStageId).HasColumnName("GvaStageId");
            this.Property(t => t.OfficiallyReissuedStageId).HasColumnName("OfficiallyReissuedStageId");

            // Relationships
            this.HasRequired(t => t.Person)
                .WithMany()
                .HasForeignKey(t => t.LotId);
            this.HasRequired(t => t.LicencePart)
                .WithMany()
                .HasForeignKey(t => t.LicencePartId);
            this.HasRequired(t => t.EditionPart)
                .WithMany()
                .HasForeignKey(t => t.EditionPartId);
            this.HasRequired(t => t.LicenceType)
                .WithMany()
                .HasForeignKey(t => t.LicenceTypeId);
            this.HasRequired(t => t.LicenceAction)
                .WithMany()
                .HasForeignKey(t => t.LicenceActionId);
            this.HasOptional(t => t.LotFile)
                .WithMany()
                .HasForeignKey(t => t.GvaLotFileId);
            this.HasOptional(t => t.GvaApplication)
                .WithMany()
                .HasForeignKey(t => t.GvaApplicationId);
            this.HasOptional(t => t.Application)
                .WithMany()
                .HasForeignKey(t => new { t.LotId, t.ApplicationPartId });
            this.HasOptional(t => t.Stage)
                .WithMany()
                .HasForeignKey(t => t.GvaStageId);
        }
    }
}
