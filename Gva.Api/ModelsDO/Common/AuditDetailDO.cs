using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Common
{
    public class AuditDetailDO
    {
        public AuditDetailDO()
        {
            this.Disparities = new List<int>();
        }

        public string AuditPart { get; set; }

        public string SortOrder { get; set; }

        public string Code { get; set; }

        public string Subject { get; set; }

        [Required(ErrorMessage = "AuditResult is required.")]
        public NomValue AuditResult { get; set; }

        public List<int> Disparities { get; set; }
    }
}
