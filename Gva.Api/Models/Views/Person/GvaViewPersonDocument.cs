using Regs.Api.LotEvents;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;
using Regs.Api.Models;

namespace Gva.Api.Models.Views.Person
{
    public partial class GvaViewPersonDocument : IProjectionView
    {
        public int LotId { get; set; }

        public int PartId { get; set; }

        public int? ParentPartId { get; set; }

        public string SetPartAlias { get; set; }

        public string DocumentNumber { get; set; }

        public int? DocumentPersonNumber { get; set; }

        public int? TypeId { get; set; }

        public int? RoleId { get; set; }

        public string Publisher { get; set; }

        public string Limitations { get; set; }

        public int? MedClassId { get; set; }

        public DateTime? Date { get; set; }

        public bool? Valid { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string Notes { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreationDate { get; set; }

        public string EditedBy { get; set; }

        public DateTime? EditedDate { get; set; }

        public virtual NomValue Type { get; set; }

        public virtual NomValue Role { get; set; }

        public virtual GvaViewPerson Person { get; set; }

        public virtual Lot Lot { get; set; }

        public virtual Part Part { get; set; }

        public virtual Part ParentPart { get; set; }
    }

    public class GvaViewPersonDocumentMap : EntityTypeConfiguration<GvaViewPersonDocument>
    {
        public GvaViewPersonDocumentMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.PartId });

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PartId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SetPartAlias)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.DocumentNumber)
                .HasMaxLength(50);

            this.Property(t => t.Publisher)
                .HasMaxLength(150);

            this.Property(t => t.Limitations)
                .HasMaxLength(150);

            this.Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EditedBy)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GvaViewPersonDocuments");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartId).HasColumnName("LotPartId");
            this.Property(t => t.ParentPartId).HasColumnName("ParentLotPartId");
            this.Property(t => t.SetPartAlias).HasColumnName("SetPartAlias");
            this.Property(t => t.DocumentNumber).HasColumnName("DocumentNumber");
            this.Property(t => t.DocumentPersonNumber).HasColumnName("DocumentPersonNumber");
            this.Property(t => t.TypeId).HasColumnName("TypeId");
            this.Property(t => t.RoleId).HasColumnName("RoleId");
            this.Property(t => t.Publisher).HasColumnName("Publisher");
            this.Property(t => t.Limitations).HasColumnName("Limitations");
            this.Property(t => t.MedClassId).HasColumnName("MedClassId");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Publisher).HasColumnName("Publisher");
            this.Property(t => t.Valid).HasColumnName("Valid");
            this.Property(t => t.FromDate).HasColumnName("FromDate");
            this.Property(t => t.ToDate).HasColumnName("ToDate");
            this.Property(t => t.Notes).HasColumnName("Notes");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreationDate).HasColumnName("CreationDate");
            this.Property(t => t.EditedBy).HasColumnName("EditedBy");
            this.Property(t => t.EditedDate).HasColumnName("EditedDate");

            // Relationships
            this.HasRequired(t => t.Person)
                .WithMany(d => d.PersonDocuments)
                .HasForeignKey(t => t.LotId);

            this.HasRequired(t => t.Part)
                .WithMany()
                .HasForeignKey(t => t.PartId);

            this.HasOptional(t => t.ParentPart)
                .WithMany()
                .HasForeignKey(t => t.ParentPartId);

            this.HasOptional(t => t.Type)
                .WithMany()
                .HasForeignKey(t => t.TypeId);

            this.HasOptional(t => t.Role)
                .WithMany()
                .HasForeignKey(t => t.RoleId);
        }
    }
}
