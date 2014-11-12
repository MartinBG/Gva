using System;

namespace Gva.Api.ModelsDO
{
    public class InventoryItemDO
    {
        public string SetPartAlias { get; set; }

        public int PartIndex { get; set; }

        public int? ParentPartIndex { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Number { get; set; }

        public DateTime? Date { get; set; }

        public string Publisher { get; set; }

        public bool? Valid { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public int? BookPageNumber { get; set; }

        public int? PageCount { get; set; }

        public FileDataDO File { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public string EditedBy { get; set; }

        public DateTime? EditedDate { get; set; }
    }
}