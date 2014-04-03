using System.Collections.Generic;
using Gva.Api.Models;

namespace Gva.Api.Repositories.InventoryRepository
{
    public interface IInventoryRepository
    {
        IEnumerable<GvaViewInventoryItem> GetInventoryItemsForLot(int lotId);
    }
}
