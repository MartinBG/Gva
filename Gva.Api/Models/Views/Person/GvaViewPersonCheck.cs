using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;
using Regs.Api.Models;

namespace Gva.Api.Models.Views.Person
{
    public partial class GvaViewPersonCheck
    {
        public int LotId { get; set; }

        public int PartId { get; set; }

        public int PartIndex { get; set; }

        public string DocumentNumber { get; set; }

        public int? DocumentTypeId { get; set; }

        public int? DocumentRoleId { get; set; }

        public string RatingTypes { get; set; }

        public int? RatingClassId { get; set; }

        public int? AuthorizationId { get; set; }

        public string Sector { get; set; }

        public int? LicenceTypeId { get; set; }

        public int? ValidId { get; set; }

        public int? PersonCheckRatingValueId { get; set; }

        public DateTime DocumentDateValidFrom { get; set; }

        public DateTime DocumentDateValidTo { get; set; }

        public string Publisher { get; set; }

        public virtual NomValue DocumentType { get; set; }

        public virtual NomValue DocumentRole { get; set; }

        public virtual NomValue RatingClass { get; set; }

        public virtual NomValue Authorization { get; set; }

        public virtual NomValue LicenceType { get; set; }

        public virtual NomValue Valid { get; set; }

        public virtual NomValue PersonCheckRatingValue { get; set; }

        public virtual GvaViewPerson Person { get; set; }

    }

    public class GvaViewPersonCheckMap : EntityTypeConfiguration<GvaViewPersonCheck>
    {
        public GvaViewPersonCheckMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.PartIndex });

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PartIndex)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Publisher)
                .HasMaxLength(100);

            this.Property(t => t.DocumentNumber)
                .HasMaxLength(50);

            this.Property(t => t.Sector)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GvaViewPersonChecks");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartId).HasColumnName("PartId");
            this.Property(t => t.PartIndex).HasColumnName("PartIndex");
            this.Property(t => t.DocumentNumber).HasColumnName("DocumentNumber");
            this.Property(t => t.DocumentTypeId).HasColumnName("DocumentTypeId");
            this.Property(t => t.DocumentRoleId).HasColumnName("DocumentRoleId");
            this.Property(t => t.RatingTypes).HasColumnName("RatingTypes");
            this.Property(t => t.RatingClassId).HasColumnName("RatingClassId");
            this.Property(t => t.AuthorizationId).HasColumnName("AuthorizationId");
            this.Property(t => t.Sector).HasColumnName("Sector");
            this.Property(t => t.LicenceTypeId).HasColumnName("LicenceTypeId");
            this.Property(t => t.ValidId).HasColumnName("ValidId");
            this.Property(t => t.PersonCheckRatingValueId).HasColumnName("PersonCheckRatingValueId");
            this.Property(t => t.DocumentDateValidFrom).HasColumnName("DocumentDateValidFrom");
            this.Property(t => t.DocumentDateValidTo).HasColumnName("DocumentDateValidTo");

            //Relationships
            this.HasRequired(t => t.Person)
                .WithMany(t => t.Checks)
                .HasForeignKey(t => t.LotId);

            this.HasOptional(t => t.DocumentType)
                .WithMany()
                .HasForeignKey(t => t.DocumentTypeId);
            this.HasOptional(t => t.DocumentRole)
                .WithMany()
                .HasForeignKey(t => t.DocumentRoleId);
            this.HasOptional(t => t.RatingClass)
                .WithMany()
                .HasForeignKey(t => t.RatingClassId);
            this.HasOptional(t => t.Authorization)
                 .WithMany()
                 .HasForeignKey(t => t.AuthorizationId);
            this.HasOptional(t => t.LicenceType)
                 .WithMany()
                 .HasForeignKey(t => t.LicenceTypeId);
            this.HasOptional(t => t.Valid)
                 .WithMany()
                 .HasForeignKey(t => t.ValidId);
            this.HasOptional(t => t.PersonCheckRatingValue)
                 .WithMany()
                 .HasForeignKey(t => t.PersonCheckRatingValueId);

        }
    }
}
