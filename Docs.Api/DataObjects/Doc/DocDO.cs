using Common.Api.Models;
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
                    this.DocTypeGroupId = d.DocType.DocTypeGroupId;
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

                if (d.DocHasReads != null && unitUser != null)
                {
                    this.IsRead = d.DocHasReads.Any(e => e.UnitId == unitUser.UnitId && e.HasRead);
                }

                if (d.DocSourceType != null)
                {
                    this.IsElectronic = d.DocSourceType.Alias == "Internet";
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
        public bool IsElectronic { get; set; }

        public bool IsActive { get; set; }
        public byte[] Version { get; set; }

        #region Aux fields

        //tech edit
        public int? PrimaryRegisterIndexId { get; set; }
        public int? SecondaryRegisterIndexId { get; set; }
        public bool UnregisterDoc { get; set; }
        public int? DocTypeGroupId { get; set; }

        public bool IsDocIncoming { get; set; }
        public bool IsDocInternal { get; set; }
        public bool IsDocOutgoing { get; set; }
        public bool IsDocInternalOutgoing { get; set; }
        public bool IsDocument { get; set; }
        public bool IsResolution { get; set; }
        public bool IsRemark { get; set; }
        public bool IsTask { get; set; }

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
        public string JObjectForm { get; set; }
        public Newtonsoft.Json.Linq.JObject JObject { get; set; }
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

        public bool IsRead { get; set; }

        public string ErrorString { get; set; }

        #endregion

        #region Permissions

        public bool CanRead { get; set; }
        public bool CanEdit { get; set; }
        public bool CanRegister { get; set; }
        public bool CanManagement { get; set; }
        public bool CanESign { get; set; }
        public bool CanFinish { get; set; }
        public bool CanReverse { get; set; }

        public bool CanSubstituteManagement { get; set; }
        public bool CanDeleteManagement { get; set; }
        public bool CanEditTechElectronicServiceStage { get; set; }
        public bool CanEditTech { get; set; }
        public bool CanChangeDocCasePart { get; set; }

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
    }
}
