using System.Collections.Generic;
using Gva.Api.Models;

namespace Gva.Api.Repositories.InventoryRepository
{
    public interface IInventoryRepository
    {
        void AddInventoryItem(GvaInventoryItem inventoryItem);

        GvaInventoryItem GetInventoryItem(int partId);

        IEnumerable<GvaInventoryItem> GetInventoryItemsForLot(int lotId);

        void DeleteInventoryItem(GvaInventoryItem inventoryItem);
    }
}
