using Aop.Api.Models;
using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace Aop.Api.Models
{
    public partial class AopApp
    {
        public int AopApplicationId { get; set; }
        public Nullable<int> DocId { get; set; }
        public Nullable<int> AopEmployerId { get; set; }
        public string Email { get; set; }
        public Nullable<int> STAopApplicationTypeId { get; set; }
        public Nullable<int> STObjectId { get; set; }
        public string STSubject { get; set; }
        public Nullable<int> STCriteriaId { get; set; }
        public string STValue { get; set; }
        public string STRemark { get; set; }
        public Nullable<bool> STIsMilitary { get; set; }
        public Nullable<int> STNoteTypeId { get; set; }
        public Nullable<int> STDocId { get; set; }
        public Nullable<int> STChecklistId { get; set; }
        public Nullable<int> STChecklistStatusId { get; set; }
        public Nullable<int> STNoteId { get; set; }
        public Nullable<int> NDAopApplicationTypeId { get; set; }
        public Nullable<int> NDObjectId { get; set; }
        public string NDSubject { get; set; }
        public Nullable<int> NDCriteriaId { get; set; }
        public string NDValue { get; set; }
        public Nullable<bool> NDIsMilitary { get; set; }
        public string NDROPIdNum { get; set; }
        public string NDROPUnqNum { get; set; }
        public Nullable<System.DateTime> NDROPDate { get; set; }
        public Nullable<int> NDProcedureStatusId { get; set; }
        public string NDRefusalReason { get; set; }
        public string NDAppeal { get; set; }
        public string NDRemark { get; set; }
        public Nullable<int> NDDocId { get; set; }
        public Nullable<int> NDChecklistId { get; set; }
        public Nullable<int> NDChecklistStatusId { get; set; }
        public Nullable<int> NDReportId { get; set; }
        public byte[] Version { get; set; }
        public virtual AopApplicationCriteria STCriteria { get; set; }
        public virtual AopApplicationCriteria NDCriteria { get; set; }
        public virtual AopApplicationObject STObject { get; set; }
        public virtual AopApplicationObject NDObject { get; set; }
        public virtual AopChecklistStatus STAopChecklistStatus { get; set; }
        public virtual AopChecklistStatus NDAopChecklistStatus { get; set; }
        public virtual AopEmployer AopEmployer { get; set; }
        public virtual AopProcedureStatus NDAopProcedureStatus { get; set; }
        public virtual Doc CaseDoc { get; set; }
        public virtual Doc STDoc { get; set; }
        public virtual Doc STChecklist { get; set; }
        public virtual Doc STNote { get; set; }
        public virtual Doc NDDoc { get; set; }
        public virtual Doc NDChecklist { get; set; }
        public virtual Doc NDReport { get; set; }

        public void EnsureForProperVersion(byte[] version)
        {
            if (!this.Version.SequenceEqual(version))
            {
                throw new OptimisticConcurrencyException("Doc has been modified.");
            }
        }
    }

    public class AopApplicationMap : EntityTypeConfiguration<AopApp>
    {
        public AopApplicationMap()
        {
            // Primary Key
            this.HasKey(t => t.AopApplicationId);

            // Properties
            this.Property(t => t.Email)
                .HasMaxLength(100);

            this.Property(t => t.STValue)
                .HasMaxLength(500);

            this.Property(t => t.NDValue)
                .HasMaxLength(500);

            this.Property(t => t.NDROPIdNum)
                .HasMaxLength(50);

            this.Property(t => t.NDROPUnqNum)
                .HasMaxLength(50);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("AopApplications");
            this.Property(t => t.AopApplicationId).HasColumnName("AopApplicationId");
            this.Property(t => t.DocId).HasColumnName("DocId");
            this.Property(t => t.AopEmployerId).HasColumnName("AopEmployerId");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.STAopApplicationTypeId).HasColumnName("STAopApplicationTypeId");
            this.Property(t => t.STObjectId).HasColumnName("STObjectId");
            this.Property(t => t.STSubject).HasColumnName("STSubject");
            this.Property(t => t.STCriteriaId).HasColumnName("STCriteriaId");
            this.Property(t => t.STValue).HasColumnName("STValue");
            this.Property(t => t.STRemark).HasColumnName("STRemark");
            this.Property(t => t.STIsMilitary).HasColumnName("STIsMilitary");
            this.Property(t => t.STNoteTypeId).HasColumnName("STNoteTypeId");
            this.Property(t => t.STDocId).HasColumnName("STDocId");
            this.Property(t => t.STChecklistId).HasColumnName("STChecklistId");
            this.Property(t => t.STChecklistStatusId).HasColumnName("STChecklistStatusId");
            this.Property(t => t.STNoteId).HasColumnName("STNoteId");
            this.Property(t => t.NDAopApplicationTypeId).HasColumnName("NDAopApplicationTypeId");
            this.Property(t => t.NDObjectId).HasColumnName("NDObjectId");
            this.Property(t => t.NDSubject).HasColumnName("NDSubject");
            this.Property(t => t.NDCriteriaId).HasColumnName("NDCriteriaId");
            this.Property(t => t.NDValue).HasColumnName("NDValue");
            this.Property(t => t.NDIsMilitary).HasColumnName("NDIsMilitary");
            this.Property(t => t.NDROPIdNum).HasColumnName("NDROPIdNum");
            this.Property(t => t.NDROPUnqNum).HasColumnName("NDROPUnqNum");
            this.Property(t => t.NDROPDate).HasColumnName("NDROPDate");
            this.Property(t => t.NDProcedureStatusId).HasColumnName("NDProcedureStatusId");
            this.Property(t => t.NDRefusalReason).HasColumnName("NDRefusalReason");
            this.Property(t => t.NDAppeal).HasColumnName("NDAppeal");
            this.Property(t => t.NDRemark).HasColumnName("NDRemark");
            this.Property(t => t.NDDocId).HasColumnName("NDDocId");
            this.Property(t => t.NDChecklistId).HasColumnName("NDChecklistId");
            this.Property(t => t.NDChecklistStatusId).HasColumnName("NDChecklistStatusId");
            this.Property(t => t.NDReportId).HasColumnName("NDReportId");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasOptional(t => t.STCriteria)
                .WithMany()
                .HasForeignKey(d => d.STCriteriaId);
            this.HasOptional(t => t.NDCriteria)
                .WithMany()
                .HasForeignKey(d => d.NDCriteriaId);
            this.HasOptional(t => t.STObject)
                .WithMany()
                .HasForeignKey(d => d.STObjectId);
            this.HasOptional(t => t.NDObject)
                .WithMany()
                .HasForeignKey(d => d.NDObjectId);
            this.HasOptional(t => t.STAopChecklistStatus)
                .WithMany()
                .HasForeignKey(d => d.STChecklistStatusId);
            this.HasOptional(t => t.NDAopChecklistStatus)
                .WithMany()
                .HasForeignKey(d => d.NDChecklistStatusId);
            this.HasOptional(t => t.AopEmployer)
                .WithMany()
                .HasForeignKey(d => d.AopEmployerId);
            this.HasOptional(t => t.NDAopProcedureStatus)
                .WithMany()
                .HasForeignKey(d => d.NDProcedureStatusId);
            this.HasOptional(t => t.CaseDoc)
                .WithMany()
                .HasForeignKey(d => d.DocId);
            this.HasOptional(t => t.STDoc)
                .WithMany()
                .HasForeignKey(d => d.STDocId);
            this.HasOptional(t => t.STChecklist)
                .WithMany()
                .HasForeignKey(d => d.STChecklistId);
            this.HasOptional(t => t.STNote)
                .WithMany()
                .HasForeignKey(d => d.STNoteId);
            this.HasOptional(t => t.NDDoc)
                .WithMany()
                .HasForeignKey(d => d.NDDocId);
            this.HasOptional(t => t.NDChecklist)
                .WithMany()
                .HasForeignKey(d => d.NDChecklistId);
            this.HasOptional(t => t.NDReport)
                .WithMany()
                .HasForeignKey(d => d.NDReportId);

        }
    }
}
