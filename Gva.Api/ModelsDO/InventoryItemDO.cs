using System;
using Gva.Api.Models;

namespace Gva.Api.ModelsDO
{
    public class InventoryItemDO
    {
        public InventoryItemDO(GvaViewInventoryItem inventoryItem, GvaLotFile lotFile)
        {
            this.SetPartAlias = inventoryItem.SetPartAlias;
            this.PartIndex = inventoryItem.Part.Index.Value;
            this.Name = inventoryItem.Name;
            this.Type = inventoryItem.Type;
            this.Number = inventoryItem.Number;
            this.Date = inventoryItem.Date;
            this.Publisher = inventoryItem.Publisher;
            this.Valid = inventoryItem.Valid;
            this.FromDate = inventoryItem.FromDate;
            this.ToDate = inventoryItem.ToDate;
            this.CreatedBy = inventoryItem.CreatedBy;
            this.CreationDate = inventoryItem.CreationDate;
            this.EditedBy = inventoryItem.EditedBy;
            this.EditedDate = inventoryItem.EditedDate;
            this.File = lotFile == null ? null : new FileDO(lotFile);
        }

        public string SetPartAlias { get; set; }

        public int PartIndex { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Number { get; set; }

        public DateTime? Date { get; set; }

        public string Publisher { get; set; }

        public bool? Valid { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public string EditedBy { get; set; }

        public DateTime? EditedDate { get; set; }

        public FileDO File { get; set; }
    }
}