using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using System;
using System.Collections.Generic;

namespace Gva.Api.ModelsDO.Organizations
{
    public class OrganizationApprovalDO
    {
        [Required(ErrorMessage = "ApprovalType is required.")]
        public NomValue ApprovalType { get; set; }

        [Required(ErrorMessage = "DocumentNumber is required.")]
        public string DocumentNumber { get; set; }

        public DateTime? DocumentDateIssue { get; set; }

        public NomValue ApprovalState { get; set; }

        public DateTime? ApprovalStateDate { get; set; }

        public string ApprovalStateNote { get; set; }

        public int? RecommendationReport { get; set; }

        public int CaseTypeId { get; set; }
    }
}
