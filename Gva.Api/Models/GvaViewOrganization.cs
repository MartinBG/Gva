﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;
using System;
using Common.Api.Models;

namespace Gva.Api.Models
{
    public partial class GvaViewOrganizationData
    {
        public int GvaOrganizationLotId { get; set; }

        public string Name { get; set; }

        public string CAO { get; set; }

        public string Uin { get; set; }

        public string Valid { get; set; }

        public string OrganizationType { get; set; }

        public DateTime? DateValidTo { get; set; }

        public DateTime? DateCAOValidTo { get; set; }

        public virtual Lot Lot { get; set; }
    }

    public class GvaViewOrganizationMap : EntityTypeConfiguration<GvaViewOrganizationData>
    {
        public GvaViewOrganizationMap()
        {
            // Primary Key
            this.HasKey(t => t.GvaOrganizationLotId);

            // Properties
            this.Property(t => t.GvaOrganizationLotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.CAO)
                .HasMaxLength(50);

            this.Property(t => t.Uin)
            .HasMaxLength(50);

            this.Property(t => t.Valid)
               .IsRequired();

            this.Property(t => t.OrganizationType)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("GvaViewOrganizations");
            this.Property(t => t.GvaOrganizationLotId).HasColumnName("GvaOrganizationLotId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Uin).HasColumnName("Uin");
            this.Property(t => t.CAO).HasColumnName("CAO");
            this.Property(t => t.Valid).HasColumnName("Valid");
            this.Property(t => t.OrganizationType).HasColumnName("OrganizationType");
            this.Property(t => t.DateValidTo).HasColumnName("DateValidTo");
            this.Property(t => t.DateCAOValidTo).HasColumnName("DateCAOValidTo");

            // Relationships
            this.HasRequired(t => t.Lot)
                .WithOptional();
        }
    }
}