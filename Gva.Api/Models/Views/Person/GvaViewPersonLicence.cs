using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;
using Regs.Api.Models;

namespace Gva.Api.Models.Views.Person
{
    public partial class GvaViewPersonLicence
    {
        public int LotId { get; set; }

        public int PartId { get; set; }

        public int PartIndex { get; set; }

        public int LicenceTypeId { get; set; }

        public int? LicenceNumber { get; set; }

        public bool Valid { get; set; }

        public string LicenceTypeCaCode { get; set; }

        public string PublisherCode { get; set; }

        public string ForeignLicenceNumber { get; set; }

        public string ForeignPublisher { get; set; }

        public string StatusChange { get; set; }

        public virtual GvaViewPerson Person { get; set; }

        public virtual Part Part { get; set; }

        public virtual NomValue LicenceType { get; set; }

        public virtual List<GvaViewPersonLicenceEdition> Editions { get; set; }

    }

    public class GvaViewPersonLicenceMap : EntityTypeConfiguration<GvaViewPersonLicence>
    {
        public GvaViewPersonLicenceMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.PartIndex });

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PartId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.LicenceTypeCaCode)
                .HasMaxLength(50);

            this.Property(t => t.PublisherCode)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GvaViewPersonLicences");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartIndex).HasColumnName("PartIndex");
            this.Property(t => t.PartId).HasColumnName("PartId");
            this.Property(t => t.LicenceTypeId).HasColumnName("LicenceTypeId");
            this.Property(t => t.LicenceNumber).HasColumnName("LicenceNumber");
            this.Property(t => t.Valid).HasColumnName("Valid");
            this.Property(t => t.LicenceTypeCaCode).HasColumnName("LicenceTypeCaCode");
            this.Property(t => t.PublisherCode).HasColumnName("PublisherCode");
            this.Property(t => t.ForeignLicenceNumber).HasColumnName("ForeignLicenceNumber");
            this.Property(t => t.ForeignPublisher).HasColumnName("ForeignPublisher");
            this.Property(t => t.StatusChange).HasColumnName("StatusChange");

            // Relationships
            this.HasRequired(t => t.Person)
                .WithMany(d => d.PersonLicences)
                .HasForeignKey(t => t.LotId);
            this.HasRequired(t => t.Part)
                .WithMany()
                .HasForeignKey(t => t.PartId);
            this.HasRequired(t => t.LicenceType)
                .WithMany()
                .HasForeignKey(t => t.LicenceTypeId);
        }
    }
}
