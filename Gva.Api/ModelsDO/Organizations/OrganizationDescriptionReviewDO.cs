using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using Gva.Api.ModelsDO.Common;

namespace Gva.Api.ModelsDO.Organizations
{
    public class OrganizationDescriptionReviewDO
    {
        public OrganizationDescriptionReviewDO() {
            this.AuditDetails = new List<OrganizationGroupDO>();
            this.Disparities = new List<DisparityDO>();
        }

        [Required(ErrorMessage = "AuditDetails are required.")]
        public List<OrganizationGroupDO> AuditDetails { get; set; }

        public List<DisparityDO> Disparities { get; set; }
    }
}
