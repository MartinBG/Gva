using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using System;
using System.Collections.Generic;

namespace Gva.Api.ModelsDO.Organizations
{
    public class OrganizationAmendmentDO
    {
        public OrganizationAmendmentDO()
        {
            this.Lims147 = new List<Lims147DO>();
            this.Lims145 = new List<Lims145DO>();
            this.LimsMG = new List<LimsMGDO>();
            this.IncludedDocuments = new List<IncludedDocumentDO>();
        }

        public string DocumentNumber { get; set; }

        public DateTime? DocumentDateIssue { get; set; }

        public int? ChangeNum { get; set; }

        public int Index { get; set; }

        public int? ApprovalPartIndex { get; set; }

        public int CaseTypeId { get; set; }

        public List<Lims147DO> Lims147 { get; set; }

        public List<Lims145DO> Lims145 { get; set; }

        public List<LimsMGDO> LimsMG { get; set; }

        public List<IncludedDocumentDO> IncludedDocuments { get; set; }
    }
}
