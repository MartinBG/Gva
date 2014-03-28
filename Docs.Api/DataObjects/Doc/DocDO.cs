using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.DataObjects
{
    public class DocDO
    {
        public DocDO()
        {
            this.PrivateDocFiles = new List<DocFileDO>();
            this.PublicDocFiles = new List<DocFileDO>();
            this.DocFiles = new List<DocFileDO>();
            this.DocRelations = new List<DocRelationDO>();
            this.DocClassifications = new List<DocClassificationDO>();
            this.DocUnitsFrom = new List<NomDo>();
            this.DocUnitsTo = new List<NomDo>();
            this.DocUnitsImportedBy = new List<NomDo>();
            this.DocUnitsMadeBy = new List<NomDo>();
            this.DocUnitsCCopy = new List<NomDo>();
            this.DocUnitsInCharge = new List<NomDo>();
            this.DocUnitsControlling = new List<NomDo>();
            this.DocUnitsReaders = new List<NomDo>();
            this.DocUnitsEditors = new List<NomDo>();
            this.DocUnitsRegistrators = new List<NomDo>();

            this.DocCorrespondents = new List<NomDo>();
            this.DocWorkflows = new List<DocWorkflowDO>();
            this.DocElectronicServiceStages = new List<DocElectronicServiceStageDO>();
            this.DocUsers = new List<DocUserDO>();

            //this.DocLinks = new List<DocLinkDO>();
        }

        public DocDO(Doc d, UnitUser unitUser = null)
            : this()
        {
            if (d != null)
            {
                this.DocId = d.DocId;
                this.DocDirectionId = d.DocDirectionId;
                this.DocEntryTypeId = d.DocEntryTypeId;
                this.DocSourceTypeId = d.DocSourceTypeId;
                this.DocDestinationTypeId = d.DocDestinationTypeId;
                this.DocSubject = d.DocSubject;
                this.DocBody = d.DocBody;
                this.DocStatusId = d.DocStatusId;
                this.DocTypeId = d.DocTypeId;
                this.DocFormatTypeId = d.DocFormatTypeId;
                this.DocCasePartTypeId = d.DocCasePartTypeId;
                this.DocRegisterId = d.DocRegisterId;
                this.RegUri = d.RegUri;
                this.RegIndex = d.RegIndex;
                this.RegNumber = d.RegNumber;
                this.RegDate = d.RegDate;
                this.ExternalRegNumber = d.ExternalRegNumber;
                this.CorrRegNumber = d.CorrRegNumber;
                this.CorrRegDate = d.CorrRegDate;
                this.AccessCode = d.AccessCode;
                this.AssignmentTypeId = d.AssignmentTypeId;
                this.AssignmentDate = d.AssignmentDate;
                this.AssignmentDeadline = d.AssignmentDeadline;
                this.IsCase = d.IsCase;
                this.IsRegistered = d.IsRegistered;
                this.IsSigned = d.IsSigned;
                this.IsActive = d.IsActive;
                this.Version = d.Version;

                if (d.DocType != null)
                {
                    this.DocTypeAlias = d.DocType.Alias;
                    this.DocTypeName = d.DocType.Name;
                    this.DocTypeIsElectronicService = d.DocType.IsElectronicService;
                }

                if (d.DocDirection != null)
                {
                    this.DocDirectionAlias = d.DocDirection.Alias;
                    this.DocDirectionName = d.DocDirection.Name;
                }

                if (d.DocEntryType != null)
                {
                    this.DocEntryTypeAlias = d.DocEntryType.Alias;
                    this.DocEntryTypeName = d.DocEntryType.Name;
                }

                if (d.DocStatus != null)
                {
                    this.DocStatusAlias = d.DocStatus.Alias;
                    this.DocStatusName = d.DocStatus.Name;
                }

                if (d.DocCasePartType != null)
                {
                    this.DocCasePartTypeAlias = d.DocCasePartType.Alias;
                    this.DocCasePartTypeName = d.DocCasePartType.Name;
                }

                this.UnitUser = new UnitUserDO(unitUser);
                if (d.DocUsers != null && unitUser != null)
                {
                    this.IsRead = d.DocUsers.Any(e => e.UnitId == unitUser.UnitId && e.HasRead && e.IsActive);
                }
            }
        }

        public int? DocId { get; set; }
        public int DocDirectionId { get; set; }
        public int DocEntryTypeId { get; set; }
        public int? DocSourceTypeId { get; set; }
        public int? DocDestinationTypeId { get; set; }
        public string DocSubject { get; set; }
        public string DocBody { get; set; }
        public int? DocStatusId { get; set; }
        public int? DocTypeId { get; set; }
        public int? DocFormatTypeId { get; set; }
        public int? DocRegisterId { get; set; }
        public int? DocCasePartTypeId { get; set; }
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

        public bool IsSelected { get; set; }

        public bool IsActive { get; set; }
        public byte[] Version { get; set; }

        #region Aux fields

        public bool IsDocIncoming { get; set; }
        public bool IsDocInternal { get; set; }
        public bool IsDocOutgoing { get; set; }
        public bool IsDocInternalOutgoing { get; set; }
        public bool IsDocument { get; set; }
        public bool IsResolution { get; set; }
        public bool IsRemark { get; set; }
        public bool IsTask { get; set; }

        //? to be re-done all that is below
        //public bool IsCaseElectronicService { get; set; }

        public string DocEntryTypeAlias { get; set; }
        public string DocEntryTypeName { get; set; }

        public string DocDirectionAlias { get; set; }
        public string DocDirectionName { get; set; }

        public string DocTypeAlias { get; set; }
        public string DocTypeName { get; set; }
        public bool DocTypeIsElectronicService { get; set; }

        public string DocStatusAlias { get; set; }
        public string DocStatusName { get; set; }

        public string DocCasePartTypeAlias { get; set; }
        public string DocCasePartTypeName { get; set; }

        public UnitUserDO UnitUser { get; set; }

        //for presentation
        public List<DocFileDO> PrivateDocFiles { get; set; }
        public List<DocFileDO> PublicDocFiles { get; set; }
        //for edit
        public List<DocFileDO> DocFiles { get; set; }

        public List<DocRelationDO> DocRelations { get; set; }
        public List<DocClassificationDO> DocClassifications { get; set; }

        public List<NomDo> DocUnitsFrom { get; set; }
        public List<NomDo> DocUnitsTo { get; set; }
        public List<NomDo> DocUnitsImportedBy { get; set; }
        public List<NomDo> DocUnitsMadeBy { get; set; }
        public List<NomDo> DocUnitsCCopy { get; set; }
        public List<NomDo> DocUnitsInCharge { get; set; }
        public List<NomDo> DocUnitsControlling { get; set; }
        public List<NomDo> DocUnitsReaders { get; set; }
        public List<NomDo> DocUnitsEditors { get; set; }
        public List<NomDo> DocUnitsRegistrators { get; set; }

        public List<NomDo> DocCorrespondents { get; set; }
        public List<DocWorkflowDO> DocWorkflows { get; set; }
        public List<DocElectronicServiceStageDO> DocElectronicServiceStages { get; set; }
        public List<DocUserDO> DocUsers { get; set; }

        public DocRelationDO ParentDocRelation
        {
            get
            {
                DocRelationDO current = this.DocRelations.FirstOrDefault(e => e.DocId == this.DocId);

                if (current != null && current.ParentDocId.HasValue)
                {
                    return this.DocRelations.FirstOrDefault(e => e.DocId == current.ParentDocId.Value);
                }
                else
                {
                    return null;
                }
            }
        }

        //public List<DocLinkDO> DocLinks { get; set; }

        //public bool HasDocElectronicServiceStages
        //{
        //    get
        //    {
        //        return DocElectronicServiceStages.Any();
        //    }
        //}

        //public bool IsClosedCurrentDocElectronicServiceStages
        //{
        //    get
        //    {
        //        if (DocElectronicServiceStages.Any())
        //        {
        //            var current = DocElectronicServiceStages.FirstOrDefault(e => e.IsCurrentStage);
        //            if (current != null)
        //            {
        //                return current.EndingDate.HasValue;
        //            }
        //        }

        //        return false;
        //    }
        //}

        //public bool HasManyDocElectronicServiceStages
        //{
        //    get
        //    {
        //        return DocElectronicServiceStages.Count > 1;
        //    }
        //}

        //public DocRelationDO ParentDocRelation
        //{
        //    get
        //    {
        //        var result = this.DocRelations.FirstOrDefault(e => e.DocId == this.DocId && e.ParentDocId.HasValue) ?? new DocRelationDO();
        //        return result;
        //    }
        //}

        //public DocRelationDO RootDocRelation { get; set; }

        public bool IsRead { get; set; }

        public string ErrorString { get; set; }

        #endregion

        public void Set()
        {
            if (this.DocEntryTypeAlias == "Document")
            {
                this.IsDocument = true;

                if (this.DocDirectionAlias == "Incomming")
                {
                    this.IsDocIncoming = true;
                }
                else if (this.DocDirectionAlias == "Internal")
                {
                    this.IsDocInternal = true;
                }
                else if (this.DocDirectionAlias == "Outgoing")
                {
                    this.IsDocOutgoing = true;
                }
                else if (this.DocDirectionAlias == "InternalOutgoing")
                {
                    this.IsDocInternalOutgoing = true;
                }
            }
            else if (this.DocEntryTypeAlias == "Resolution")
            {
                this.IsResolution = true;
            }
            else if (this.DocEntryTypeAlias == "Task")
            {
                this.IsTask = true;
            }
            else if (this.DocEntryTypeAlias == "Remark")
            {
                this.IsRemark = true;
            }
        }

        //#region Permissions

        //public bool CanRead { get; set; }
        //public bool CanEdit { get; set; }
        //public bool CanRegister { get; set; }
        //public bool CanManagement { get; set; }
        //public bool CanESign { get; set; }
        //public bool CanFinish { get; set; }
        //public bool CanReverse { get; set; }

        //#endregion

        //#region Visible Commands Flags

        ////команда добавяне на подчинен документ
        //public bool IsVisibleDocChild { get; set; }
        ////команда редакция
        //public bool IsVisibleDocEdit { get; set; }
        ////команда за публичните файлове
        //public bool IsVisibleDocPublicFiles { get; set; }
        ////команда за управление : подпис, одобрение и др.
        //public bool IsVisibleDocWorkflows { get; set; }
        ////команда за управление : искане на подпис, одобрение и др.
        //public bool IsVisibleDocWorkflowRequests { get; set; }
        ////команда регистрация
        //public bool IsVisibleDocRegister { get; set; }
        //public bool IsVisibleUndoDocRegister { get; set; }
        ////команда подписване
        //public bool IsVisibleDocSignatures { get; set; }
        //public bool IsVisibleUndoDocSignatures { get; set; }
        ////команда изпращане
        //public bool IsVisibleDocSend { get; set; }
        ////команда етапи
        //public bool IsVisibleDocElectronicServiceStages { get; set; }
        ////команда преписка
        //public bool IsVisibleChangeDocCasePartType { get; set; }

        ////команда за изготвен
        //public bool IsVisibleDocPreparedCmd { get; set; }
        //public bool IsVisibleReverseDocPreparedCmd { get; set; }

        ////команда обработен
        //public bool IsVisibleDocProcessedCmd { get; set; }
        //public bool IsVisibleReverseDocProcessedCmd { get; set; }

        ////команда приключен
        //public bool IsVisibleDocFinishedCmd { get; set; }
        //public bool IsVisibleReverseDocFinishedCmd { get; set; }

        ////команда анулиране
        //public bool IsVisibleDocCanceledCmd { get; set; }
        //public bool IsVisibleReverseDocCanceledCmd { get; set; }

        //#endregion

        //#region Visble Fields Flags

        ////ui text
        //public string DocSubjectLabel { get; set; }

        ////visible flags
        //public bool IsVisibleDocSubject { get; set; }
        //public bool IsVisibleDocBody { get; set; }

        ///// <summary>
        ///// ExtRegNumber, ExtRegDate, DocSourceTypeId, DocDestinationTypeId, Correspondent&Contacts Roles
        ///// </summary>
        //public bool IsVisibleCorrespondent { get; set; }
        //public bool IsVisibleDocSourceTypeId { get; set; }
        //public bool IsVisibleDocDestinationTypeId { get; set; }

        ///// <summary>
        ///// All DocUnitRoles
        ///// </summary>
        //public bool IsVisibleRoleFrom { get; set; }
        //public bool IsVisibleRoleTo { get; set; }
        //public bool IsVisibleRoleImportedBy { get; set; }
        //public bool IsVisibleRoleMadeBy { get; set; }
        //public bool IsVisibleRoleCCopy { get; set; }
        //public bool IsVisibleRoleInCharge { get; set; }
        //public bool IsVisibleRoleControlling { get; set; }
        //public bool IsVisibleRoleReaders { get; set; }
        //public bool IsVisibleRoleEditors { get; set; }
        //public bool IsVisibleRoleRegistrators { get; set; }

        ///// <summary>
        ///// AssignmentTypeId, AssignmentDate, AssignmentDeadline 
        ///// </summary>
        //public bool IsVisibleAssignment { get; set; }

        //public bool IsVisibleCollapseAddressing { get; set; }
        //public bool IsVisibleCollapseAssignment { get; set; }
        //public bool IsVisibleCollapsePermissions { get; set; }

        //#endregion

        //#region Setup

        //public void SetupCommands()
        //{
        //    switch (this.DocEntryTypeAlias)
        //    {
        //        case "Document":
        //            this.SetupDocumentCommands();
        //            break;
        //        case "Resolution":
        //            this.SetupResolutionCommands();
        //            break;
        //        case "Task":
        //            this.SetupTaskCommands();
        //            break;
        //        case "Remark":
        //            this.SetupRemarkCommands();
        //            break;
        //    };

        //    this.IsVisibleDocEdit = this.IsVisibleDocEdit && this.CanEdit;
        //    this.IsVisibleDocPublicFiles = this.IsVisibleDocPublicFiles && (this.CanEdit || this.CanRegister);
        //    this.IsVisibleDocWorkflows = this.IsVisibleDocWorkflows && this.CanManagement;
        //    this.IsVisibleDocWorkflowRequests = this.IsVisibleDocWorkflowRequests && (this.CanEdit || this.CanManagement);
        //    this.IsVisibleDocRegister = this.IsVisibleDocRegister && this.CanRegister;
        //    this.IsVisibleUndoDocRegister = this.IsVisibleUndoDocRegister && this.CanRegister;
        //    this.IsVisibleDocSignatures = this.IsVisibleDocSignatures && this.CanESign;
        //    this.IsVisibleUndoDocSignatures = this.IsVisibleUndoDocSignatures && this.CanESign;
        //    this.IsVisibleDocSend = this.IsVisibleDocSend && (this.CanEdit || this.CanRegister);
        //    this.IsVisibleDocElectronicServiceStages = this.IsVisibleDocElectronicServiceStages && true; //this.CanEdit;
        //    this.IsVisibleChangeDocCasePartType = this.IsVisibleChangeDocCasePartType && (this.CanEdit || this.CanManagement);

        //    this.IsVisibleDocPreparedCmd = this.IsVisibleDocPreparedCmd && this.CanEdit;
        //    this.IsVisibleReverseDocPreparedCmd = this.IsVisibleReverseDocPreparedCmd && this.CanReverse;

        //    this.IsVisibleDocProcessedCmd = this.IsVisibleDocProcessedCmd && (this.CanEdit || this.CanManagement);
        //    this.IsVisibleReverseDocProcessedCmd = this.IsVisibleReverseDocProcessedCmd && this.CanReverse;

        //    this.IsVisibleDocFinishedCmd = this.IsVisibleDocFinishedCmd && this.CanFinish;
        //    this.IsVisibleReverseDocFinishedCmd = this.IsVisibleReverseDocFinishedCmd && this.CanReverse;

        //    this.IsVisibleDocCanceledCmd = this.IsVisibleDocCanceledCmd && this.CanFinish;
        //    this.IsVisibleReverseDocCanceledCmd = this.IsVisibleReverseDocCanceledCmd && this.CanReverse;
        //}

        //public void SetupDocumentCommands()
        //{
        //    this.IsVisibleDocElectronicServiceStages = IsCaseElectronicService;
        //    this.IsVisibleChangeDocCasePartType = !this.IsCase;

        //    switch (this.DocStatusAlias)
        //    {
        //        case "Draft":
        //            this.IsVisibleDocChild = true;
        //            this.IsVisibleDocPreparedCmd = true;
        //            this.IsVisibleDocPublicFiles = true;
        //            this.IsVisibleDocEdit = true;
        //            if (!this.IsRegistered)
        //            {
        //                this.IsVisibleDocRegister = true;
        //            }
        //            else
        //            {
        //                this.IsVisibleUndoDocRegister = true;
        //            }
        //            break;
        //        case "Prepared":
        //            this.IsVisibleDocChild = true;
        //            this.IsVisibleDocProcessedCmd = true;
        //            this.IsVisibleDocPublicFiles = true;
        //            this.IsVisibleReverseDocPreparedCmd = true;
        //            this.IsVisibleDocWorkflows = true;
        //            this.IsVisibleDocWorkflowRequests = true;
        //            if (!this.IsRegistered)
        //            {
        //                this.IsVisibleDocRegister = true;
        //            }
        //            else
        //            {
        //                this.IsVisibleDocSignatures = true;
        //                if (this.IsSigned)
        //                {
        //                    this.IsVisibleUndoDocSignatures = true;
        //                }
        //                this.IsVisibleUndoDocRegister = true;
        //            }
        //            break;
        //        case "Processed":
        //            this.IsVisibleDocChild = true;
        //            this.IsVisibleDocFinishedCmd = true;
        //            this.IsVisibleDocPublicFiles = true;
        //            this.IsVisibleDocCanceledCmd = true;
        //            this.IsVisibleReverseDocProcessedCmd = true;
        //            if (this.DocDirectionAlias == "Outgoing")
        //            {
        //                this.IsVisibleDocSend = true;
        //            }
        //            break;
        //        case "Finished":
        //            this.IsVisibleReverseDocFinishedCmd = true;
        //            if (this.DocDirectionAlias == "Outgoing")
        //            {
        //                this.IsVisibleDocSend = true;
        //            }
        //            break;
        //        case "Canceled":
        //            this.IsVisibleReverseDocCanceledCmd = true;
        //            break;
        //        default:
        //            break;
        //    };
        //}

        //public void SetupResolutionCommands()
        //{
        //    this.IsVisibleDocElectronicServiceStages = IsCaseElectronicService;
        //    this.IsVisibleChangeDocCasePartType = !this.IsCase;

        //    switch (this.DocStatusAlias)
        //    {
        //        case "Draft":
        //            this.IsVisibleDocChild = true;
        //            this.IsVisibleDocPreparedCmd = true;
        //            this.IsVisibleDocEdit = true;
        //            break;
        //        case "Prepared":
        //            this.IsVisibleDocChild = true;
        //            this.IsVisibleDocProcessedCmd = true;
        //            this.IsVisibleReverseDocPreparedCmd = true;
        //            this.IsVisibleDocWorkflows = true;
        //            this.IsVisibleDocWorkflowRequests = true;
        //            break;
        //        case "Processed":
        //            this.IsVisibleDocChild = true;
        //            this.IsVisibleDocFinishedCmd = true;
        //            this.IsVisibleDocCanceledCmd = true;
        //            this.IsVisibleReverseDocProcessedCmd = true;
        //            break;
        //        case "Finished":
        //            this.IsVisibleReverseDocFinishedCmd = true;
        //            break;
        //        case "Canceled":
        //            this.IsVisibleReverseDocCanceledCmd = true;
        //            break;
        //        default:
        //            break;
        //    };
        //}

        //public void SetupRemarkCommands()
        //{
        //    this.IsVisibleDocElectronicServiceStages = IsCaseElectronicService;
        //    this.IsVisibleChangeDocCasePartType = !this.IsCase;

        //    switch (this.DocStatusAlias)
        //    {
        //        case "Draft":
        //            this.IsVisibleDocChild = true;
        //            this.IsVisibleDocPreparedCmd = true;
        //            this.IsVisibleDocEdit = true;
        //            break;
        //        case "Prepared":
        //            this.IsVisibleDocChild = true;
        //            this.IsVisibleDocProcessedCmd = true;
        //            this.IsVisibleReverseDocPreparedCmd = true;
        //            this.IsVisibleDocWorkflows = true;
        //            this.IsVisibleDocWorkflowRequests = true;
        //            break;
        //        case "Processed":
        //            this.IsVisibleDocChild = true;
        //            this.IsVisibleDocFinishedCmd = true;
        //            this.IsVisibleDocCanceledCmd = true;
        //            this.IsVisibleReverseDocProcessedCmd = true;
        //            break;
        //        case "Finished":
        //            this.IsVisibleReverseDocFinishedCmd = true;
        //            break;
        //        case "Canceled":
        //            this.IsVisibleReverseDocCanceledCmd = true;
        //            break;
        //        default:
        //            break;
        //    };
        //}

        //public void SetupTaskCommands()
        //{
        //    this.IsVisibleDocElectronicServiceStages = IsCaseElectronicService;
        //    this.IsVisibleChangeDocCasePartType = !this.IsCase;

        //    switch (this.DocStatusAlias)
        //    {
        //        case "Draft":
        //            this.IsVisibleDocChild = true;
        //            this.IsVisibleDocPreparedCmd = true;
        //            this.IsVisibleDocEdit = true;
        //            break;
        //        case "Prepared":
        //            this.IsVisibleDocChild = true;
        //            this.IsVisibleDocProcessedCmd = true;
        //            this.IsVisibleReverseDocPreparedCmd = true;
        //            this.IsVisibleDocWorkflows = true;
        //            this.IsVisibleDocWorkflowRequests = true;
        //            break;
        //        case "Processed":
        //            this.IsVisibleDocChild = true;
        //            this.IsVisibleDocFinishedCmd = true;
        //            this.IsVisibleDocCanceledCmd = true;
        //            this.IsVisibleReverseDocProcessedCmd = true;
        //            break;
        //        case "Finished":
        //            this.IsVisibleReverseDocFinishedCmd = true;
        //            break;
        //        case "Canceled":
        //            this.IsVisibleReverseDocCanceledCmd = true;
        //            break;
        //        default:
        //            break;
        //    };
        //}

        //public void SetupFields()
        //{
        //    switch (this.DocEntryTypeAlias)
        //    {
        //        case "Document":
        //            this.DocSubjectLabel = "Относно:";

        //            switch (this.DocDirectionAlias)
        //            {
        //                case "Incomming":
        //                    //адресация
        //                    this.IsVisibleRoleTo = true;
        //                    this.IsVisibleRoleCCopy = true;
        //                    this.IsVisibleRoleImportedBy = true;
        //                    this.IsVisibleCorrespondent = true;
        //                    this.IsVisibleDocSourceTypeId = true;
        //                    break;
        //                case "Internal":
        //                    //адресация
        //                    this.IsVisibleRoleFrom = true;
        //                    this.IsVisibleRoleImportedBy = true;
        //                    this.IsVisibleRoleMadeBy = true;
        //                    this.IsVisibleRoleTo = true;
        //                    this.IsVisibleRoleCCopy = true;
        //                    break;
        //                case "Outgoing":
        //                    //адресация
        //                    this.IsVisibleRoleFrom = true;
        //                    this.IsVisibleRoleMadeBy = true;
        //                    this.IsVisibleRoleImportedBy = true;
        //                    this.IsVisibleCorrespondent = true;
        //                    this.IsVisibleDocDestinationTypeId = true;
        //                    break;
        //                case "InternalOutgoing":
        //                    //адресация
        //                    this.IsVisibleRoleFrom = true;
        //                    this.IsVisibleRoleMadeBy = true;
        //                    this.IsVisibleRoleImportedBy = true;
        //                    this.IsVisibleRoleTo = true;
        //                    this.IsVisibleRoleCCopy = true;
        //                    this.IsVisibleCorrespondent = true;
        //                    this.IsVisibleDocDestinationTypeId = true;
        //                    break;
        //            }

        //            //общо възлагане
        //            this.IsVisibleRoleInCharge = true;
        //            this.IsVisibleRoleControlling = true;
        //            this.IsVisibleAssignment = true;

        //            //общо доп. права
        //            this.IsVisibleRoleReaders = true;
        //            this.IsVisibleRoleEditors = true;
        //            this.IsVisibleRoleRegistrators = true;

        //            //табове
        //            this.IsVisibleCollapseAddressing = true;
        //            if (this.AssignmentTypeId.HasValue || this.AssignmentDeadline.HasValue || this.AssignmentDate.HasValue ||
        //                this.DocUnits.Any(e => e.DocUnitRoleAlias == "incharge") || this.DocUnits.Any(e => e.DocUnitRoleAlias == "controlling"))
        //            {
        //                this.IsVisibleCollapseAssignment = true;
        //            }
        //            if (this.DocUnits.Any(e => e.DocUnitRoleAlias == "readers") || this.DocUnits.Any(e => e.DocUnitRoleAlias == "editors") ||
        //                this.DocUnits.Any(e => e.DocUnitRoleAlias == "registrators"))
        //            {
        //                this.IsVisibleCollapsePermissions = true;
        //            }
        //            break;
        //        case "Resolution":
        //            this.DocSubjectLabel = "Резолюция:";
        //            //адресация
        //            this.IsVisibleRoleFrom = true;
        //            this.IsVisibleRoleImportedBy = true;
        //            this.IsVisibleRoleCCopy = true;
        //            //възлагане
        //            this.IsVisibleRoleInCharge = true;
        //            this.IsVisibleRoleControlling = true;
        //            this.IsVisibleAssignment = true;
        //            //доп. права
        //            this.IsVisibleRoleReaders = true;
        //            this.IsVisibleRoleEditors = true;
        //            this.IsVisibleRoleRegistrators = true;
        //            //табове
        //            this.IsVisibleCollapseAddressing = true;
        //            this.IsVisibleCollapseAssignment = true;
        //            if (this.DocUnits.Any(e => e.DocUnitRoleAlias == "readers") || this.DocUnits.Any(e => e.DocUnitRoleAlias == "editors") ||
        //                this.DocUnits.Any(e => e.DocUnitRoleAlias == "registrators"))
        //            {
        //                this.IsVisibleCollapsePermissions = true;
        //            }
        //            break;
        //        case "Task":
        //            this.DocSubjectLabel = "Задача:";
        //            //адресация
        //            this.IsVisibleRoleFrom = true;
        //            this.IsVisibleRoleImportedBy = true;
        //            this.IsVisibleRoleCCopy = true;
        //            //възлагане
        //            this.IsVisibleRoleInCharge = true;
        //            this.IsVisibleRoleControlling = true;
        //            this.IsVisibleAssignment = true;
        //            //доп. права
        //            this.IsVisibleRoleReaders = true;
        //            this.IsVisibleRoleEditors = true;
        //            this.IsVisibleRoleRegistrators = true;
        //            //табове
        //            this.IsVisibleCollapseAddressing = true;
        //            this.IsVisibleCollapseAssignment = true;
        //            if (this.DocUnits.Any(e => e.DocUnitRoleAlias == "readers") || this.DocUnits.Any(e => e.DocUnitRoleAlias == "editors") ||
        //                this.DocUnits.Any(e => e.DocUnitRoleAlias == "registrators"))
        //            {
        //                this.IsVisibleCollapsePermissions = true;
        //            }
        //            break;
        //        case "Remark":
        //            this.DocSubjectLabel = "Забележка:";
        //            //адресация
        //            this.IsVisibleRoleImportedBy = true;
        //            this.IsVisibleRoleCCopy = true;
        //            //възлагане
        //            //this.IsVisibleRoleInCharge = true;
        //            //this.IsVisibleRoleControlling = true;
        //            //this.IsVisibleAssignment = true;
        //            //доп. права
        //            //this.IsVisibleRoleReaders = true;
        //            //this.IsVisibleRoleEditors = true;
        //            //this.IsVisibleRoleRegistrators = true;
        //            //табове
        //            this.IsVisibleCollapseAddressing = true;
        //            //this.IsVisibleCollapseAssignment = false;
        //            //this.IsVisibleCollapsePermissions = false;
        //            break;
        //    };
        //}

        //public void SetupFlags()
        //{
        //    this.SetupCommands();
        //    this.SetupFields();
        //}

        ////called only for new documents
        //public void SetupAuto(Unit unit, List<DocUnitRole> docUnitRoles, List<DocTypeUnitRole> docTypeUnitRoles, List<DocTypeClassification> docTypeClassifications)
        //{
        //    if (unit == null)
        //    {
        //        return;
        //    }

        //    DocUnitRole docUnitRole = null;
        //    List<DocUnitDO> autoAddedDocUnitRoles = new List<DocUnitDO>();

        //    switch (this.DocEntryTypeAlias)
        //    {
        //        case "Document":
        //            this.DocSubjectLabel = "Относно:";

        //            switch (this.DocDirectionAlias)
        //            {
        //                case "Incomming":
        //                    //адресация
        //                    break;
        //                case "Internal":
        //                    //адресация
        //                    break;
        //                case "Outgoing":
        //                    //адресация
        //                    break;
        //                case "InternalOutgoing":
        //                    //адресация
        //                    break;
        //            }

        //            //общо възлагане

        //            //общо доп. права

        //            //табове
        //            break;
        //        case "Resolution":
        //            this.DocSubjectLabel = "Резолюция:";
        //            //адресация
        //            //възлагане
        //            //доп. права
        //            //табове
        //            break;
        //        case "Task":
        //            break;
        //    }

        //    //общо за всички

        //    //DocTypeUnitRoles
        //    docUnitRole = docUnitRoles.FirstOrDefault(e => e.Alias.ToLower() == "importedby");
        //    autoAddedDocUnitRoles.Add(CreateDocUnitDOInternal(unit, docUnitRole));

        //    if (docTypeUnitRoles.Any())
        //    {
        //        foreach (var item in docTypeUnitRoles)
        //        {
        //            autoAddedDocUnitRoles.Add(CreateDocUnitDOInternal(item.Unit, item.DocUnitRole));
        //        }
        //    }

        //    if (autoAddedDocUnitRoles.Any())
        //    {
        //        foreach (var item in autoAddedDocUnitRoles)
        //        {
        //            if (!this.DocUnits.Any(e => e.DocUnitRoleId == item.DocUnitRoleId && e.UnitId == item.UnitId))
        //            {
        //                this.DocUnits.Add(item);
        //                //this.DocUnits.AddRange(autoAddedDocUnitRoles);
        //            }
        //        }
        //    }

        //    //DocTypeClassifications
        //    DateTime currentDate = DateTime.Now;

        //    if (docTypeClassifications.Any())
        //    {
        //        foreach (var item in docTypeClassifications)
        //        {
        //            DocClassificationDO dcDO = DocClassificationDOInternal(item, currentDate);
        //            this.DocClassifications.Add(dcDO);
        //        }
        //    }
        //}

        //private DocClassificationDO DocClassificationDOInternal(DocTypeClassification docTypeClassification, DateTime currentDate)
        //{
        //    return new DocClassificationDO()
        //    {
        //        ClassificationId = docTypeClassification.ClassificationId,
        //        //ClassificationByUserId = this._systemUserId, //?
        //        ClassificationDate = currentDate,
        //        IsActive = true
        //    };
        //}

        //private DocUnitDO CreateDocUnitDOInternal(Unit unit, DocUnitRole docUnitRole)
        //{
        //    if (unit != null && docUnitRole != null)
        //    {

        //        return new DocUnitDO()
        //        {
        //            DocUnitId = null,
        //            DocId = this.DocId,
        //            UnitId = unit.UnitId,
        //            DocUnitRoleId = docUnitRole.DocUnitRoleId,
        //            DocUnitRoleAlias = docUnitRole.Alias.ToLower(), //to match logic in js
        //            UnitName = unit.Name,
        //            IsNew = true,
        //            IsSelected = true
        //        };
        //    }

        //    return null;
        //}

        //#endregion
    }
}
