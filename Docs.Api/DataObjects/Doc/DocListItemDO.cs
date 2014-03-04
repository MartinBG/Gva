using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.DataObjects
{
    public class DocListItemDO
    {
        public DocListItemDO()
        {
            this.DocCorrespondents = new List<DocCorrespondentDO>();
        }

        public DocListItemDO(Doc d, UnitUser unitUser = null)
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
                this.AssignmentTypeId = d.AssignmentTypeId;
                this.AssignmentDate = d.AssignmentDate;
                this.AssignmentDeadline = d.AssignmentDeadline;
                this.IsCase = d.IsCase;
                this.IsRegistered = d.IsRegistered;
                this.IsSigned = d.IsSigned;
                this.IsActive = d.IsActive;
                this.Version = d.Version;

                if (d.DocStatus != null)
                {
                    this.DocStatusAlias = d.DocStatus.Alias;
                    this.DocStatusName = d.DocStatus.Name;
                }

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

                if (d.DocUsers != null && unitUser != null)
                {
                    this.IsRead = d.DocUsers.Any(e => e.UnitId == unitUser.UnitId && e.HasRead && e.IsActive);
                }
            }
        }

        public void SetDocUsers(List<DocUser> docUsers, UnitUser unitUser = null)
        {
            if (docUsers != null && unitUser != null)
            {
                this.IsRead = docUsers.Any(e => e.UnitId == unitUser.UnitId && e.HasRead && e.IsActive);
            }
        }

        public Nullable<int> DocId { get; set; }
        public int DocDirectionId { get; set; }
        public int DocEntryTypeId { get; set; }
        public Nullable<int> DocSourceTypeId { get; set; }
        public Nullable<int> DocDestinationTypeId { get; set; }
        public string DocSubject { get; set; }
        public string DocBody { get; set; }
        public Nullable<int> DocStatusId { get; set; }
        public Nullable<int> DocTypeId { get; set; }
        public Nullable<int> DocFormatTypeId { get; set; }
        public Nullable<int> DocRegisterId { get; set; }
        public Nullable<int> DocCasePartTypeId { get; set; }
        public string RegUri { get; set; }
        public string RegIndex { get; set; }
        public Nullable<int> RegNumber { get; set; }
        public Nullable<System.DateTime> RegDate { get; set; }
        public string ExternalRegNumber { get; set; }
        public string CorrRegNumber { get; set; }
        public Nullable<System.DateTime> CorrRegDate { get; set; }

        public Nullable<int> AssignmentTypeId { get; set; }
        public Nullable<System.DateTime> AssignmentDate { get; set; }
        public Nullable<System.DateTime> AssignmentDeadline { get; set; }
        public bool IsExamined { get; set; }
        public bool IsCase { get; set; }
        public bool IsRegistered { get; set; }
        public bool IsSigned { get; set; }

        public bool IsSelected { get; set; }

        public bool IsActive { get; set; }
        public byte[] Version { get; set; }

        //
        #region Aux fields

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

        public List<DocCorrespondentDO> DocCorrespondents { get; set; }

        public bool IsRead { get; set; }

        #endregion

        #region ForManagement/ForControl

        public DocRelationDO CaseDocRelation { get; set; }

        #endregion
    }
}
