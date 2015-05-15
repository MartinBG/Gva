using Regs.Api.LotEvents;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gva.Api.Models.Views.Person
{
    public partial class GvaViewPersonDocument : IProjectionView
    {
        public int LotId { get; set; }

        public int PartIndex { get; set; }

        public string DocumentNumber { get; set; }

        public int? DocumentPersonNumber { get; set; }

        public int? TypeId { get; set; }

        public int? RoleId { get; set; }

        public string Publisher { get; set; }

        public string Limitations { get; set; }

        public DateTime? DateValidFrom { get; set; }

        public virtual GvaViewPerson Person { get; set; }
    }

    public class GvaViewPersonDocumentMap : EntityTypeConfiguration<GvaViewPersonDocument>
    {
        public GvaViewPersonDocumentMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.PartIndex });

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PartIndex)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DocumentNumber)
                .HasMaxLength(50);

            this.Property(t => t.Publisher)
                .HasMaxLength(150);

            this.Property(t => t.Limitations)
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("GvaViewPersonDocuments");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartIndex).HasColumnName("PartIndex");
            this.Property(t => t.DocumentNumber).HasColumnName("DocumentNumber");
            this.Property(t => t.DocumentPersonNumber).HasColumnName("DocumentPersonNumber");
            this.Property(t => t.TypeId).HasColumnName("TypeId");
            this.Property(t => t.RoleId).HasColumnName("RoleId");
            this.Property(t => t.Publisher).HasColumnName("Publisher");
            this.Property(t => t.Limitations).HasColumnName("Limitations");
            this.Property(t => t.DateValidFrom).HasColumnName("DateValidFrom");

            // Relationships
            this.HasRequired(t => t.Person)
                .WithMany(d => d.PersonDocuments)
                .HasForeignKey(t => t.LotId);
        }
    }
}
