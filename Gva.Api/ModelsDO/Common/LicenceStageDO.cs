using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Common
{
    public class LicenceStageDO
    {
        public int? ApplicationId { get; set; }

        public int? LotId { get; set; }

        public int? EditionPartIndex { get; set; }

        public string[] StageAliases { get; set; }
    }
}
