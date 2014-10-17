using System.Collections.Generic;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Applications
{
    public class ApplicationNewDO
    {
        public int LotId { get; set; }

        public string SetPartPath { get; set; }

        public List<int> Correspondents { get; set; }

        public NomValue ApplicationType { get; set; }

        public int CaseTypeId { get; set; }
    }
}
