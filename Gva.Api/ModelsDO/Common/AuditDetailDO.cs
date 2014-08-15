using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Common
{
    public class AuditDetailDO
    {
        public string SortOrder { get; set; }

        public string Code { get; set; }

        public string Subject { get; set; }

        [Required(ErrorMessage = "AuditResult is required.")]
        public NomValue AuditResult { get; set; }
    }
}
