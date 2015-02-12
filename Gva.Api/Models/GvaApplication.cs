using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using Gva.Api.Models.Views;
using Regs.Api.Models;

namespace Gva.Api.Models
{
    public partial class GvaApplication
    {
        public GvaApplication()
        {
            this.GvaAppLotFiles = new List<GvaAppLotFile>();
        }

        public int GvaApplicationId { get; set; }

        public Nullable<int> DocId { get; set; }

        public int LotId { get; set; }

        public Nullable<int> GvaAppLotPartId { get; set; }

        public virtual Docs.Api.Models.Doc Doc { get; set; }

        public virtual Regs.Api.Models.Lot Lot { get; set; }

        public virtual Part GvaAppLotPart { get; set; }

        public virtual ICollection<GvaAppLotFile> GvaAppLotFiles { get; set; }
    }

    public class GvaApplicationMap : EntityTypeConfiguration<GvaApplication>
    {
        public GvaApplicationMap()
        {
            // Primary Key
            this.HasKey(t => t.GvaApplicationId);

            // Properties
            // Table & Column Mappings
            this.ToTable("GvaApplications");
            this.Property(t => t.GvaApplicationId).HasColumnName("GvaApplicationId");
            this.Property(t => t.DocId).HasColumnName("DocId");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.GvaAppLotPartId).HasColumnName("GvaAppLotPartId");

            // Relationships
            this.HasOptional(t => t.Doc)
                .WithMany()
                .HasForeignKey(d => d.DocId);
            this.HasRequired(t => t.Lot)
                .WithMany()
                .HasForeignKey(d => d.LotId);
            this.HasOptional(t => t.GvaAppLotPart)
                .WithMany()
                .HasForeignKey(d => d.GvaAppLotPartId);
        }
    }
}
