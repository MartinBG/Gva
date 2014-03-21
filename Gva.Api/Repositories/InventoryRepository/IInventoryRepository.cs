using System.Collections.Generic;
using Gva.Api.Models;

namespace Gva.Api.Repositories.InventoryRepository
{
    public interface IInventoryRepository
    {
        void AddInventoryItem(GvaInventoryItem inventoryItem);

        GvaInventoryItem GetInventoryItem(int partId, int? caseTypeId);

        IEnumerable<GvaInventoryItem> GetInventoryItemsForLot(int lotId, int? caseTypeId);

        void DeleteInventoryItemsForPart(int partId);

        void DeleteInventoryItem(GvaInventoryItem inventoryItem);
    }
}
