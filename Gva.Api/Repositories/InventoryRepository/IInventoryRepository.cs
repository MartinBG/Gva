using System;
using System.Collections.Generic;
using Gva.Api.Models;
using Gva.Api.ModelsDO;

namespace Gva.Api.Repositories.InventoryRepository
{
    public interface IInventoryRepository
    {
        IEnumerable<InventoryItemDO> GetInventoryItems(
            int? lotId = null,
            int? caseTypeId = null,
            string setAlias = null,
            string documentPart = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? typeId = null);

        IEnumerable<string> GetNotes(string term);
    }
}
