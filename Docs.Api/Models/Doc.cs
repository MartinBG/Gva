using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class Doc
    {
        public Doc()
        {
            this.DocClassifications = new List<DocClassification>();
            this.DocCorrespondentContacts = new List<DocCorrespondentContact>();
            this.DocCorrespondents = new List<DocCorrespondent>();
            this.DocElectronicServiceStages = new List<DocElectronicServiceStage>();
            this.DocFiles = new List<DocFile>();
            this.DocIncomingDocs = new List<DocIncomingDoc>();
            this.DocRelations = new List<DocRelation>();
            this.DocRelations1 = new List<DocRelation>();
            this.DocRelations2 = new List<DocRelation>();
            this.DocUnits = new List<DocUnit>();
            this.DocUsers = new List<DocUser>();
            this.DocWorkflows = new List<DocWorkflow>();
        }

        public int DocId { get; set; }
        public int DocDirectionId { get; set; }
        public int DocEntryTypeId { get; set; }
        public Nullable<int> DocCasePartTypeId { get; set; }
        public string DocSubject { get; set; }
        public string DocBody { get; set; }
        public int DocStatusId { get; set; }
        public Nullable<int> DocSourceTypeId { get; set; }
        public Nullable<int> DocDestinationTypeId { get; set; }
        public Nullable<int> DocTypeId { get; set; }
        public Nullable<int> DocFormatTypeId { get; set; }
        public Nullable<int> DocRegisterId { get; set; }
        public string RegUri { get; set; }
        public string RegIndex { get; set; }
        public Nullable<int> RegNumber { get; set; }
        public Nullable<System.DateTime> RegDate { get; set; }
        public string ExternalRegNumber { get; set; }
        public string CorrRegNumber { get; set; }
        public Nullable<System.DateTime> CorrRegDate { get; set; }
        public string AccessCode { get; set; }
        public Nullable<int> AssignmentTypeId { get; set; }
        public Nullable<System.DateTime> AssignmentDate { get; set; }
        public Nullable<System.DateTime> AssignmentDeadline { get; set; }
        public bool IsCase { get; set; }
        public bool IsRegistered { get; set; }
        public bool IsSigned { get; set; }
        public Nullable<System.Guid> LockObjectId { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<int> ModifyUserId { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual AssignmentType AssignmentType { get; set; }
        public virtual DocCasePartType DocCasePartType { get; set; }
        public virtual ICollection<DocClassification> DocClassifications { get; set; }
        public virtual ICollection<DocCorrespondentContact> DocCorrespondentContacts { get; set; }
        public virtual ICollection<DocCorrespondent> DocCorrespondents { get; set; }
        public virtual DocDestinationType DocDestinationType { get; set; }
        public virtual DocDirection DocDirection { get; set; }
        public virtual ICollection<DocElectronicServiceStage> DocElectronicServiceStages { get; set; }
        public virtual DocEntryType DocEntryType { get; set; }
        public virtual ICollection<DocFile> DocFiles { get; set; }
        public virtual DocFormatType DocFormatType { get; set; }
        public virtual ICollection<DocIncomingDoc> DocIncomingDocs { get; set; }
        public virtual DocRegister DocRegister { get; set; }
        public virtual ICollection<DocRelation> DocRelations { get; set; }
        public virtual ICollection<DocRelation> DocRelations1 { get; set; }
        public virtual ICollection<DocRelation> DocRelations2 { get; set; }
        public virtual DocSourceType DocSourceType { get; set; }
        public virtual DocStatus DocStatus { get; set; }
        public virtual DocType DocType { get; set; }
        public virtual Common.Api.Models.User User { get; set; }
        public virtual ICollection<DocUnit> DocUnits { get; set; }
        public virtual ICollection<DocUser> DocUsers { get; set; }
        public virtual ICollection<DocWorkflow> DocWorkflows { get; set; }
    }

    public class DocMap : EntityTypeConfiguration<Doc>
    {
        public DocMap()
        {
            // Primary Key
            this.HasKey(t => t.DocId);

            // Properties
            this.Property(t => t.DocSubject)
                .IsRequired();

            this.Property(t => t.RegUri)
                .HasMaxLength(200);

            this.Property(t => t.RegIndex)
                .HasMaxLength(200);

            this.Property(t => t.ExternalRegNumber)
                .HasMaxLength(200);

            this.Property(t => t.CorrRegNumber)
                .HasMaxLength(200);

            this.Property(t => t.AccessCode)
                .HasMaxLength(50);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Docs");
            this.Property(t => t.DocId).HasColumnName("DocId");
            this.Property(t => t.DocDirectionId).HasColumnName("DocDirectionId");
            this.Property(t => t.DocEntryTypeId).HasColumnName("DocEntryTypeId");
            this.Property(t => t.DocCasePartTypeId).HasColumnName("DocCasePartTypeId");
            this.Property(t => t.DocSubject).HasColumnName("DocSubject");
            this.Property(t => t.DocBody).HasColumnName("DocBody");
            this.Property(t => t.DocStatusId).HasColumnName("DocStatusId");
            this.Property(t => t.DocSourceTypeId).HasColumnName("DocSourceTypeId");
            this.Property(t => t.DocDestinationTypeId).HasColumnName("DocDestinationTypeId");
            this.Property(t => t.DocTypeId).HasColumnName("DocTypeId");
            this.Property(t => t.DocFormatTypeId).HasColumnName("DocFormatTypeId");
            this.Property(t => t.DocRegisterId).HasColumnName("DocRegisterId");
            this.Property(t => t.RegUri).HasColumnName("RegUri");
            this.Property(t => t.RegIndex).HasColumnName("RegIndex");
            this.Property(t => t.RegNumber).HasColumnName("RegNumber");
            this.Property(t => t.RegDate).HasColumnName("RegDate");
            this.Property(t => t.ExternalRegNumber).HasColumnName("ExternalRegNumber");
            this.Property(t => t.CorrRegNumber).HasColumnName("CorrRegNumber");
            this.Property(t => t.CorrRegDate).HasColumnName("CorrRegDate");
            this.Property(t => t.AccessCode).HasColumnName("AccessCode");
            this.Property(t => t.AssignmentTypeId).HasColumnName("AssignmentTypeId");
            this.Property(t => t.AssignmentDate).HasColumnName("AssignmentDate");
            this.Property(t => t.AssignmentDeadline).HasColumnName("AssignmentDeadline");
            this.Property(t => t.IsCase).HasColumnName("IsCase");
            this.Property(t => t.IsRegistered).HasColumnName("IsRegistered");
            this.Property(t => t.IsSigned).HasColumnName("IsSigned");
            this.Property(t => t.LockObjectId).HasColumnName("LockObjectId");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.ModifyUserId).HasColumnName("ModifyUserId");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasOptional(t => t.AssignmentType)
                .WithMany(t => t.Docs)
                .HasForeignKey(d => d.AssignmentTypeId);
            this.HasOptional(t => t.DocCasePartType)
                .WithMany(t => t.Docs)
                .HasForeignKey(d => d.DocCasePartTypeId);
            this.HasOptional(t => t.DocDestinationType)
                .WithMany(t => t.Docs)
                .HasForeignKey(d => d.DocDestinationTypeId);
            this.HasRequired(t => t.DocDirection)
                .WithMany(t => t.Docs)
                .HasForeignKey(d => d.DocDirectionId);
            this.HasRequired(t => t.DocEntryType)
                .WithMany(t => t.Docs)
                .HasForeignKey(d => d.DocEntryTypeId);
            this.HasOptional(t => t.DocFormatType)
                .WithMany(t => t.Docs)
                .HasForeignKey(d => d.DocFormatTypeId);
            this.HasOptional(t => t.DocRegister)
                .WithMany(t => t.Docs)
                .HasForeignKey(d => d.DocRegisterId);
            this.HasOptional(t => t.DocSourceType)
                .WithMany(t => t.Docs)
                .HasForeignKey(d => d.DocSourceTypeId);
            this.HasRequired(t => t.DocStatus)
                .WithMany(t => t.Docs)
                .HasForeignKey(d => d.DocStatusId);
            this.HasOptional(t => t.DocType)
                .WithMany(t => t.Docs)
                .HasForeignKey(d => d.DocTypeId);
            this.HasOptional(t => t.User)
                .WithMany()
                .HasForeignKey(d => d.ModifyUserId);

        }
    }
}
