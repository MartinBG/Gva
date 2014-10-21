using System;
using System.Collections.Generic;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Organizations
{
    public class IncludedDocumentDO
    {
        public IncludedDocumentDO()
        {
            this.LinkedLim = new List<LimitationDO>();
        }

        public NomValue Inspector { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public List<LimitationDO> LinkedLim { get; set; }

        public int? PartIndex { get; set; }

        public string SetPartAlias { get; set; }
    }
}
