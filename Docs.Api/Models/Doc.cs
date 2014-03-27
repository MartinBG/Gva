using Common.Api.UserContext;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

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

        public int? DocCasePartTypeId { get; set; }

        public string DocSubject { get; set; }

        public string DocBody { get; set; }

        public int DocStatusId { get; set; }

        public int? DocSourceTypeId { get; set; }

        public int? DocDestinationTypeId { get; set; }

        public int? DocTypeId { get; set; }

        public int? DocFormatTypeId { get; set; }

        public int? DocRegisterId { get; set; }

        public string RegUri { get; set; }

        public string RegIndex { get; set; }

        public int? RegNumber { get; set; }

        public DateTime? RegDate { get; set; }

        public string ExternalRegNumber { get; set; }

        public string CorrRegNumber { get; set; }

        public DateTime? CorrRegDate { get; set; }

        public string AccessCode { get; set; }

        public int? AssignmentTypeId { get; set; }

        public DateTime? AssignmentDate { get; set; }

        public DateTime? AssignmentDeadline { get; set; }

        public bool IsCase { get; set; }

        public bool IsRegistered { get; set; }

        public bool IsSigned { get; set; }

        public Guid? LockObjectId { get; set; }

        public DateTime? ModifyDate { get; set; }

        public int? ModifyUserId { get; set; }

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

        #region DocCorrespondents

        public DocCorrespondent CreateDocCorrespondent(int correspondentId, UserContext userContext)
        {
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = userContext.UserId;

            DocCorrespondent docCorrespondent = new DocCorrespondent
            {
                CorrespondentId = correspondentId
            };

            this.DocCorrespondents.Add(docCorrespondent);

            return docCorrespondent;
        }

        public DocCorrespondent CreateDocCorrespondent(Correspondent correspondent, UserContext userContext)
        {
            return this.CreateDocCorrespondent(correspondent.CorrespondentId, userContext);
        }

        public void DeleteDocCorrespondent(DocCorrespondent docCorrespondent, UserContext userContext)
        {
            bool result = this.DocCorrespondents.Remove(docCorrespondent);

            if (result)
            {
                this.ModifyDate = DateTime.Now;
                this.ModifyUserId = userContext.UserId;
            }
            else
            {
                throw new Exception("The docCorrespondent to be removed is not contained in the doc.");
            }

        }

        public void DeleteDocCorrespondent(int docCorrespondentId, UserContext userContext)
        {
            DocCorrespondent docCorrespondent = this.DocCorrespondents.FirstOrDefault(e => e.DocCorrespondentId == docCorrespondentId);

            if (docCorrespondent != null)
            {
                this.DeleteDocCorrespondent(docCorrespondent, userContext);
            }
            else
            {
                throw new Exception(string.Format("No docCorrespondent with ID = {0} found.", docCorrespondentId));
            }
        }

        #endregion

        #region DocRelations

        public DocRelation CreateDocRelation(int? parentDocId, int? rootDocId, UserContext userContext)
        {
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = userContext.UserId;

            DocRelation docRelation = new DocRelation
            {
                ParentDocId = parentDocId,
                RootDocId = rootDocId
            };

            if (!docRelation.RootDocId.HasValue)
            {
                docRelation.RootDoc = this;
                this.IsCase = true;
            }
            else
            {
                this.IsCase = this.DocId == docRelation.RootDocId.Value;
            }

            this.DocRelations.Add(docRelation);

            return docRelation;
        }

        public void UpdateDocRelation(int docRelationId, int? parentDocId, int? rootDocId, UserContext userContext)
        {
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = userContext.UserId;

            DocRelation docRelation = this.DocRelations.FirstOrDefault(e => e.DocRelationId == docRelationId);

            if (docRelation != null)
            {
                docRelation.ParentDocId = parentDocId;
                docRelation.RootDocId = rootDocId;

                if (!docRelation.RootDocId.HasValue)
                {
                    docRelation.RootDoc = this;
                    this.IsCase = true; //? DocEntryTypeId dependant
                }
                else
                {
                    this.IsCase = this.DocId == docRelation.RootDocId.Value;
                }
            }
            else
            {
                throw new Exception(string.Format("No docRelation contact with ID = {0} found.", docRelationId));
            }
        }

        public void DeleteDocRelation(DocRelation docRelation, UserContext userContext)
        {
            bool result = this.DocRelations.Remove(docRelation);

            if (result)
            {
                this.ModifyDate = DateTime.Now;
                this.ModifyUserId = userContext.UserId;

                if (!this.DocRelations.Any())
                {
                    throw new Exception("Document can not exist with no docRelations.");
                }
            }
            else
            {
                throw new Exception("The docRelation to be removed is not contained in the doc.");
            }
        }

        public void DeleteDocRelation(int docRelationId, UserContext userContext)
        {
            DocRelation docRelation = this.DocRelations.FirstOrDefault(e => e.DocRelationId == docRelationId);

            if (docRelation != null)
            {
                this.DeleteDocRelation(docRelation, userContext);
            }
            else
            {
                throw new Exception(string.Format("No docRelation with ID = {0} found.", docRelationId));
            }
        }

        #endregion

        #region DocClassifications

        public DocClassification CreateDocClassification(int classificationId, UserContext userContext)
        {
            DateTime currentDate = DateTime.Now;

            this.ModifyDate = currentDate;
            this.ModifyUserId = userContext.UserId;

            DocClassification docClassification = new DocClassification
            {
                ClassificationByUserId = userContext.UserId,
                ClassificationDate = currentDate,
                ClassificationId = classificationId,
                IsActive = true
            };

            this.DocClassifications.Add(docClassification);

            return docClassification;
        }

        public DocClassification CreateDocClassification(Classification classification, UserContext userContext)
        {
            return this.CreateDocClassification(classification.ClassificationId, userContext);
        }

        public void DisableDocClassification(DocClassification docClassification, UserContext userContext)
        {
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = userContext.UserId;

            docClassification.IsActive = false;
        }

        public void DisableDocClassification(int docClassificationId, UserContext userContext)
        {
            DocClassification docClassification = this.DocClassifications.FirstOrDefault(e => e.DocClassificationId == docClassificationId);

            if (docClassification != null)
            {
                this.DisableDocClassification(docClassification, userContext);
            }
            else
            {
                throw new Exception(string.Format("No docClassification with ID = {0} found.", docClassificationId));
            }
        }

        public void DeleteDocClassification(DocClassification docClassification, UserContext userContext)
        {
            bool result = this.DocClassifications.Remove(docClassification);

            if (result)
            {
                this.ModifyDate = DateTime.Now;
                this.ModifyUserId = userContext.UserId;
            }
            else
            {
                throw new Exception("The docClassification to be removed is not contained in the doc.");
            }

        }

        public void DeleteDocClassification(int docClassificationId, UserContext userContext)
        {
            DocClassification docClassification = this.DocClassifications.FirstOrDefault(e => e.DocClassificationId == docClassificationId);

            if (docClassification != null)
            {
                this.DeleteDocClassification(docClassification, userContext);
            }
            else
            {
                throw new Exception(string.Format("No docClassification with ID = {0} found.", docClassificationId));
            }
        }

        #endregion

        #region DocUnits

        public DocUnit CreateDocUnit(int unitId, int docUnitRoleId, UserContext userContext)
        {
            DateTime currentDate = DateTime.Now;

            this.ModifyDate = currentDate;
            this.ModifyUserId = userContext.UserId;

            DocUnit docUnit = new DocUnit
            {
                DocUnitRoleId = docUnitRoleId,
                UnitId = unitId
            };

            this.DocUnits.Add(docUnit);

            return docUnit;
        }

        public DocUnit CreateDocUnit(Unit unit, DocUnitRole docUnitRole, UserContext userContext)
        {
            return this.CreateDocUnit(unit.UnitId, docUnitRole.DocUnitRoleId, userContext);
        }

        public void DeleteDocUnit(DocUnit docUnit, UserContext userContext)
        {
            bool result = this.DocUnits.Remove(docUnit);

            if (result)
            {
                this.ModifyDate = DateTime.Now;
                this.ModifyUserId = userContext.UserId;
            }
            else
            {
                throw new Exception("The docUnit to be removed is not contained in the doc.");
            }

        }

        public void DeleteDocUnit(int docUnitId, UserContext userContext)
        {
            DocUnit docUnit = this.DocUnits.FirstOrDefault(e => e.DocUnitId == docUnitId);

            if (docUnit != null)
            {
                this.DeleteDocUnit(docUnit, userContext);
            }
            else
            {
                throw new Exception(string.Format("No docUnit with ID = {0} found.", docUnitId));
            }
        }

        #endregion

        #region DocWorkflows

        public DocWorkflow CreateDocWorkflow(
            int docWorkflowActionId,
            DateTime eventDate,
            bool? yesNo,
            int? toUnitId,
            int? principalUnitId,
            string note,
            int unitUserId,
            UserContext userContext)
        {
            DateTime currentDate = DateTime.Now;

            this.ModifyDate = currentDate;
            this.ModifyUserId = userContext.UserId;

            DocWorkflow docWorkflow = new DocWorkflow
            {
                DocWorkflowActionId = docWorkflowActionId,
                EventDate = eventDate,
                Note = note,
                PrincipalUnitId = principalUnitId,
                ToUnitId = toUnitId,
                YesNo = yesNo,
                UnitUserId = unitUserId
            };

            this.DocWorkflows.Add(docWorkflow);

            return docWorkflow;
        }

        public DocWorkflow CreateDocWorkflow(
            DocWorkflowAction docWorkflowAction,
            DateTime eventDate,
            bool? yesNo,
            int? toUnitId,
            int? principalUnitId,
            string note,
            int unitUserId,
            UserContext userContext)
        {
            return CreateDocWorkflow(
                docWorkflowAction.DocWorkflowActionId,
                eventDate,
                yesNo,
                toUnitId,
                principalUnitId,
                note,
                unitUserId,
                userContext
                );
        }

        public void DeleteDocWorkflow(DocWorkflow docWorkflow, UserContext userContext)
        {
            bool result = this.DocWorkflows.Remove(docWorkflow);

            if (result)
            {
                this.ModifyDate = DateTime.Now;
                this.ModifyUserId = userContext.UserId;
            }
            else
            {
                throw new Exception("The docWorkflow to be removed is not contained in the doc.");
            }

        }

        public void DeleteDocWorkflow(int docWorkflowId, UserContext userContext)
        {
            DocWorkflow docWorkflow = this.DocWorkflows.FirstOrDefault(e => e.DocWorkflowId == docWorkflowId);

            if (docWorkflow != null)
            {
                this.DeleteDocWorkflow(docWorkflow, userContext);
            }
            else
            {
                throw new Exception(string.Format("No docWorkflow with ID = {0} found.", docWorkflowId));
            }
        }

        #endregion

        #region DocFiles

        public DocFile CreateDocFile(
            int docFileKindId,
            int docFileTypeId,
            string name,
            string docFileName,
            string docContentStorage,
            Guid docFileContentId,
            bool isPrimary,
            bool isActive,
            UserContext userContext)
        {
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = userContext.UserId;

            DocFile docFile = new DocFile
            {
                DocFileKindId = docFileKindId,
                DocFileTypeId = docFileTypeId,
                Name = name,
                DocFileName = docFileName,
                DocContentStorage = docContentStorage,
                DocFileContentId = docFileContentId,
                IsPrimary = isPrimary,
                IsActive = isActive
            };

            this.DocFiles.Add(docFile);

            return docFile;
        }

        public DocFile CreateDocFile(
            int docFileKindId,
            int docFileTypeId,
            string name,
            string docFileName,
            string docContentStorage,
            Guid docFileContentId,
            UserContext userContext)
        {
            return this.CreateDocFile(
                docFileKindId,
                docFileTypeId,
                name,
                docFileName,
                docContentStorage,
                docFileContentId,
                false,
                true,
                userContext);
        }

        public DocFile UpdateDocFile(
            DocFile docFile,
            int docFileKindId,
            int docFileTypeId,
            string name,
            string docFileName,
            string docContentStorage,
            Guid docFileContentId,
            UserContext userContext)
        {
            throw new NotImplementedException();
        }

        public DocFile UpdateDocFile(
            int docFileId,
            int docFileKindId,
            int docFileTypeId,
            string name,
            string docFileName,
            string docContentStorage,
            Guid docFileContentId,
            UserContext userContext)
        {
            DocFile docFile = this.DocFiles.FirstOrDefault(e => e.DocFileId == docFileId);

            if (docFile != null)
            {
                return this.UpdateDocFile(
                    docFile,
                    docFileKindId,
                    docFileTypeId,
                    name,
                    docFileName,
                    docContentStorage,
                    docFileContentId,
                    userContext);
            }
            else
            {
                throw new Exception(string.Format("No docFile with ID = {0} found.", docFileId));
            }
        }

        public void DeleteDocFile(DocFile docFile, UserContext userContext)
        {
            bool result = this.DocFiles.Remove(docFile);

            if (result)
            {
                this.ModifyDate = DateTime.Now;
                this.ModifyUserId = userContext.UserId;
            }
            else
            {
                throw new Exception("The docFile to be removed is not contained in the doc.");
            }

        }

        public void DeleteDocFile(int docFileId, UserContext userContext)
        {
            DocFile docFile = this.DocFiles.FirstOrDefault(e => e.DocFileId == docFileId);

            if (docFile != null)
            {
                this.DeleteDocFile(docFile, userContext);
            }
            else
            {
                throw new Exception(string.Format("No docFile with ID = {0} found.", docFileId));
            }
        }

        #endregion

        #region DocElectronicServiceStages

        public DocElectronicServiceStage GetCurrentDocElectronicServiceStage()
        {
            return this.DocElectronicServiceStages
                .SingleOrDefault(e => e.IsCurrentStage);
        }

        public DocElectronicServiceStage UpdateCurrentDocElectronicServiceStage(
            int electronicServiceStageId,
            DateTime startingDate,
            DateTime? expectedEndingDate,
            DateTime? endingDate,
            UserContext userContext)
        {
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = userContext.UserId;

            DocElectronicServiceStage current = this.DocElectronicServiceStages
                .SingleOrDefault(e => e.IsCurrentStage);

            if (current != null)
            {
                current.ElectronicServiceStageId = electronicServiceStageId;
                current.StartingDate = startingDate;
                current.ExpectedEndingDate = expectedEndingDate;
                current.EndingDate = endingDate;

                return current;
            }
            else
            {
                throw new Exception("There is no current docElectronicServiceStage in the doc.");
            }
        }

        public DocElectronicServiceStage UpdateCurrentDocElectronicServiceStage(
            DocElectronicServiceStage docElectronicServiceStage,
            UserContext userContext)
        {
            return UpdateCurrentDocElectronicServiceStage(
                docElectronicServiceStage.ElectronicServiceStageId,
                docElectronicServiceStage.StartingDate,
                docElectronicServiceStage.ExpectedEndingDate,
                docElectronicServiceStage.EndingDate,
                userContext);
        }

        public DocElectronicServiceStage EndCurrentDocElectronicServiceStage(UserContext userContext)
        {
            DateTime current = DateTime.Now;

            this.ModifyDate = current;
            this.ModifyUserId = userContext.UserId;

            DocElectronicServiceStage currentStage = this.DocElectronicServiceStages
                .SingleOrDefault(e => e.IsCurrentStage);

            if (currentStage != null)
            {
                currentStage.IsCurrentStage = false;
                currentStage.EndingDate = current;

                return currentStage;
            }
            else
            {
                throw new Exception("There is no current docElectronicServiceStage in the doc.");
            }
        }

        public DocElectronicServiceStage EndCurrentDocElectronicServiceStage(DateTime endDate, UserContext userContext)
        {
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = userContext.UserId;

            DocElectronicServiceStage currentStage = this.DocElectronicServiceStages
                .SingleOrDefault(e => e.IsCurrentStage);

            if (currentStage != null)
            {
                currentStage.IsCurrentStage = false;
                currentStage.EndingDate = endDate;

                return currentStage;
            }
            else
            {
                throw new Exception("There is no current docElectronicServiceStage in the doc.");
            }
        }

        public DocElectronicServiceStage CreateDocElectronicServiceStage(
            int electronicServiceStageId,
            DateTime startingDate,
            DateTime? expectedEndingDate,
            DateTime? endingDate,
            bool isCurrentStage,
            UserContext userContext)
        {
            DateTime current = DateTime.Now;

            this.ModifyDate = current;
            this.ModifyUserId = userContext.UserId;

            DocElectronicServiceStage previous = this.DocElectronicServiceStages
                .SingleOrDefault(e => e.IsCurrentStage);
            if (previous != null)
            {
                previous.IsCurrentStage = false;
                if (!previous.EndingDate.HasValue)
                {
                    previous.EndingDate = current;
                }
            }

            DocElectronicServiceStage docElectronicServiceStage = new DocElectronicServiceStage
            {
                ElectronicServiceStageId = electronicServiceStageId,
                EndingDate = endingDate,
                ExpectedEndingDate = expectedEndingDate,
                IsCurrentStage = isCurrentStage,
                StartingDate = startingDate
            };

            this.DocElectronicServiceStages.Add(docElectronicServiceStage);

            return docElectronicServiceStage;
        }

        public DocElectronicServiceStage CreateDocElectronicServiceStage(
            DocElectronicServiceStage docElectronicServiceStage,
            UserContext userContext)
        {
            return this.CreateDocElectronicServiceStage(
                docElectronicServiceStage.ElectronicServiceStageId,
                docElectronicServiceStage.StartingDate,
                docElectronicServiceStage.ExpectedEndingDate,
                docElectronicServiceStage.EndingDate,
                docElectronicServiceStage.IsCurrentStage,
                userContext);
        }

        public void DeleteDocElectronicServiceStage(
            DocElectronicServiceStage docElectronicServiceStage,
            UserContext userContext)
        {
            bool result = this.DocElectronicServiceStages.Remove(docElectronicServiceStage);

            if (result)
            {
                this.ModifyDate = DateTime.Now;
                this.ModifyUserId = userContext.UserId;
            }
            else
            {
                throw new Exception("The docElectronicServiceStage to be removed is not contained in the doc.");
            }

        }

        public void DeleteDocElectronicServiceStage(int docElectronicServiceStageId, UserContext userContext)
        {
            DocElectronicServiceStage docElectronicServiceStage =
                this.DocElectronicServiceStages.FirstOrDefault(e => e.DocElectronicServiceStageId == docElectronicServiceStageId);

            if (docElectronicServiceStage != null)
            {
                this.DeleteDocElectronicServiceStage(docElectronicServiceStage, userContext);
            }
            else
            {
                throw new Exception(string.Format("No docElectronicServiceStage with ID = {0} found.", docElectronicServiceStageId));
            }
        }

        public DocElectronicServiceStage ReverseDocElectronicServiceStage(
            DocElectronicServiceStage docElectronicServiceStage,
            UserContext userContext)
        {
            if (this.DocElectronicServiceStages.Count < 2)
            {
                throw new Exception(string.Format("Can not reverse doc ID = {0} with less than 2 docElectronicServiceStages.", this.DocId));
            }

            this.DeleteDocElectronicServiceStage(docElectronicServiceStage, userContext);

            DocElectronicServiceStage last = this.DocElectronicServiceStages.OrderByDescending(e => e.DocElectronicServiceStageId).FirstOrDefault();
            last.IsCurrentStage = true;

            return last;
        }

        #endregion

        public void Register(int? docRegisterId, string regUri, string regIndex, int? regNumber, DateTime? regDate, UserContext userContext)
        {
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = userContext.UserId;

            this.DocRegisterId = docRegisterId;
            this.RegUri = regUri;
            this.RegIndex = regIndex;
            this.RegNumber = regNumber;
            this.RegDate = regDate;
            this.IsRegistered = true;
        }

        public void EnsureForProperVersion(byte[] version)
        {
            if (!this.Version.SequenceEqual(version))
            {
                throw new OptimisticConcurrencyException("Doc has been modified.");
            }
        }

        public void EnsureDocRelationsAreLoaded()
        {
            if (!this.DocRelations.Any())
            {
                throw new InvalidOperationException(string.Format("Doc with id {0} has not loaded its docRelations.", this.DocId));
            }
        }
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
