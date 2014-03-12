using System;

namespace Gva.Api.ModelsDO
{
    public class InventoryItemDO
    {
        public string DocumentType { get; set; }

        public int PartIndex { get; set; }

        public string Name { get; set; }

        public string BookPageNumber { get; set; }

        public string Type { get; set; }

        public string Number { get; set; }

        public DateTime? Date { get; set; }

        public string Publisher { get; set; }

        public string Valid { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public int? PageCount { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public string EditedBy { get; set; }

        public DateTime? EditedDate { get; set; }

        public FileDO[] Files { get; set; }
    }
}