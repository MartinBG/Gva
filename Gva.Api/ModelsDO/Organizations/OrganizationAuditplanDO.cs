using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Organizations
{
    public class OrganizationAuditplanDO
    {
        [Required(ErrorMessage = "AuditPartRequirement is required.")]
        public NomValue AuditPartRequirement { get; set; }

        public string PlanMonth { get; set; }

        public string PlanYear { get; set; }
    }
}
