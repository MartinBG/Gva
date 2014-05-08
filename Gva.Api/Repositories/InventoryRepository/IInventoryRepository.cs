using System.Collections.Generic;
using Gva.Api.Models;
using Gva.Api.ModelsDO;

namespace Gva.Api.Repositories.InventoryRepository
{
    public interface IInventoryRepository
    {
        IEnumerable<InventoryItemDO> GetInventoryItemsForLot(int lotId, int? caseTypeId);
    }
}
