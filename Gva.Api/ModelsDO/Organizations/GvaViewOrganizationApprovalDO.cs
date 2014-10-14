using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Common.Api.Models;
using Gva.Api.Models;
using Gva.Api.Models.Views.Organization;
using Gva.Api.ModelsDO.Common;

namespace Gva.Api.ModelsDO.Organizations
{
    public class GvaViewOrganizationApprovalDO
    {
        public GvaViewOrganizationApprovalDO(GvaViewOrganizationApproval approval)
        {
            GvaViewOrganizationAmendment amendment = approval.Amendments.OrderBy(a => a.Index).Last();
            this.LotId = approval.LotId;
            this.PartIndex = approval.PartIndex;
            this.ApprovalDocumentNumber = approval.DocumentNumber;
            this.AmendmentDocumentNumber = amendment.DocumentNumber;
            this.ApprovalType = approval.ApprovalType;
            this.ApprovalState = approval.ApprovalState;
            this.ApplicationName = approval.Amendments.Last().ApplicationName;
            this.ChangeNum = amendment.ChangeNum;
            this.LastAmendmentDateIssue = amendment.DocumentDateIssue;
            this.FirstAmendmentDateIssue = approval.Amendments.OrderBy(a => a.Index).First().DocumentDateIssue;
            this.AmendmentPartIndex = amendment.PartIndex;
        }

        public int LotId { get; set; }

        public int PartIndex { get; set; }

        public int AmendmentPartIndex { get; set; }

        public string ApprovalDocumentNumber { get; set; }
        
         public string AmendmentDocumentNumber { get; set; }

        public DateTime? LastAmendmentDateIssue { get; set; }

        public DateTime? FirstAmendmentDateIssue { get; set; }

        public int? ChangeNum { get; set; }

        public string ApplicationName { get; set; }

        public NomValue ApprovalState { get; set; }

        public NomValue ApprovalType { get; set; }

    }


}
