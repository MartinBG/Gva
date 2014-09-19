using System;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Organizations
{
    public class IncludedDocumentDO
    {
        public NomValue Inspector { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public LimitationDO LinkedLim { get; set; }

        public int? PartIndex { get; set; }

        public string SetPartAlias { get; set; }
    }
}
