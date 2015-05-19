using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;
using Regs.Api.Models;
using Regs.Api.LotEvents;

namespace Gva.Api.Models.Views.Organization
{
    public partial class GvaViewOrganization : IProjectionView
    {
        public int LotId { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string Cao { get; set; }

        public string Uin { get; set; }

        public bool Valid { get; set; }

        public int OrganizationTypeId { get; set; }

        public DateTime? DateValidTo { get; set; }

        public DateTime? DateCaoValidTo { get; set; }

        public virtual Lot Lot { get; set; }

        public virtual NomValue OrganizationType { get; set; }

        public virtual ICollection<GvaViewOrganizationRecommendation> Recommendations { get; set; }

        public virtual ICollection<GvaViewOrganizationInspection> Inspections { get; set; }

        public virtual ICollection<GvaLotCase> GvaLotCases { get; set; }

        public virtual ICollection<GvaViewOrganizationApproval> OrganizationApprovals { get; set; }
    }

    public class GvaViewOrganizationMap : EntityTypeConfiguration<GvaViewOrganization>
    {
        public GvaViewOrganizationMap()
        {
            // Primary Key
            this.HasKey(t => t.LotId);

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.NameAlt)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.Cao)
                .HasMaxLength(50);

            this.Property(t => t.Uin)
            .HasMaxLength(50);

            this.Property(t => t.OrganizationTypeId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("GvaViewOrganizations");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.Uin).HasColumnName("Uin");
            this.Property(t => t.Cao).HasColumnName("Cao");
            this.Property(t => t.Valid).HasColumnName("Valid");
            this.Property(t => t.OrganizationTypeId).HasColumnName("OrganizationTypeId");
            this.Property(t => t.DateValidTo).HasColumnName("DateValidTo");
            this.Property(t => t.DateCaoValidTo).HasColumnName("DateCaoValidTo");

            // Relationships
            this.HasRequired(t => t.Lot)
                .WithOptional();
            this.HasRequired(t => t.OrganizationType)
                .WithMany()
                .HasForeignKey(t => t.OrganizationTypeId);
        }
    }
}
