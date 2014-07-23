using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;
using Gva.Api.Models.Views.Organization;
using Regs.Api.Models;

namespace Gva.Api.Models.Views.Person
{
    public partial class GvaViewPerson
    {
        public int LotId { get; set; }

        public string Lin { get; set; }

        public int LinTypeId { get; set; }

        public string Uin { get; set; }

        public string Names { get; set; }

        public DateTime BirtDate { get; set; }

        public int? OrganizationId { get; set; }

        public int? EmploymentId { get; set; }

        public string Licences { get; set; }

        public string Ratings { get; set; }

        public virtual Lot Lot { get; set; }

        public virtual NomValue LinType { get; set; }

        public virtual GvaViewOrganization Organization { get; set; }

        public virtual NomValue Employment { get; set; }

        public virtual GvaViewPersonInspector Inspector { get; set; }

        public virtual ICollection<GvaViewOrganizationExaminer> OrganizationExaminers { get; set; }

        public virtual ICollection<GvaLotCase> GvaLotCases { get; set; }

    }

    public class GvaViewPersonMap : EntityTypeConfiguration<GvaViewPerson>
    {
        public GvaViewPersonMap()
        {
            // Primary Key
            this.HasKey(t => t.LotId);

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Lin)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LinTypeId)
                .IsRequired();

            this.Property(t => t.Uin)
                .HasMaxLength(50);

            this.Property(t => t.Names)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("GvaViewPersons");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.Lin).HasColumnName("Lin");
            this.Property(t => t.LinTypeId).HasColumnName("LinTypeId");
            this.Property(t => t.Uin).HasColumnName("Uin");
            this.Property(t => t.Names).HasColumnName("Names");
            this.Property(t => t.Licences).HasColumnName("LicenceCodes");
            this.Property(t => t.Ratings).HasColumnName("RatingCodes");
            this.Property(t => t.BirtDate).HasColumnName("BirtDate");
            this.Property(t => t.OrganizationId).HasColumnName("OrganizationId");
            this.Property(t => t.EmploymentId).HasColumnName("EmploymentId");

            // Relationships
            this.HasRequired(t => t.Lot)
                .WithOptional();
            this.HasRequired(t => t.LinType)
                .WithMany()
                .HasForeignKey(t => t.LinTypeId);
            this.HasOptional(t => t.Organization)
                .WithMany()
                .HasForeignKey(t => t.OrganizationId);
            this.HasOptional(t => t.Employment)
                .WithMany()
                .HasForeignKey(t => t.EmploymentId);
            this.HasMany(t => t.GvaLotCases)
                .WithOptional()
                .HasForeignKey(t => t.LotId);
        }
    }
}
