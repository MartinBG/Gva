using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gva.Api.Models
{
    public partial class GvaApplicationSearch
    {
        public int LotPartId { get; set; }
        public Nullable<System.DateTime> RequestDate { get; set; }
        public string DocumentNumber { get; set; }
        public string ApplicationTypeName { get; set; }
        public string StatusName { get; set; }
        public virtual Regs.Api.Models.Part LotPart { get; set; }
    }

    public class GvaApplicationSearchMap : EntityTypeConfiguration<GvaApplicationSearch>
    {
        public GvaApplicationSearchMap()
        {
            // Primary Key
            this.HasKey(t => t.LotPartId);

            // Properties
            this.Property(t => t.LotPartId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DocumentNumber)
                .HasMaxLength(20);

            this.Property(t => t.ApplicationTypeName)
                .HasMaxLength(500);

            this.Property(t => t.StatusName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GvaApplicationSearches");
            this.Property(t => t.LotPartId).HasColumnName("LotPartId");
            this.Property(t => t.RequestDate).HasColumnName("RequestDate");
            this.Property(t => t.DocumentNumber).HasColumnName("DocumentNumber");
            this.Property(t => t.ApplicationTypeName).HasColumnName("ApplicationTypeName");
            this.Property(t => t.StatusName).HasColumnName("StatusName");

            // Relationships
            this.HasRequired(t => t.LotPart)
                .WithOptional();

        }
    }
}
