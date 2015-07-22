using System;
using System.Collections.Generic;
using Gva.Api.Models;
using Gva.Api.ModelsDO;

namespace Gva.Api.Repositories.PersonDocumentRepository
{
    public interface IPersonDocumentRepository
    {
        IEnumerable<InventoryItemDO> GetInventoryItems(
            int? lotId = null,
            int? caseTypeId = null);

        IEnumerable<string> GetNotes(string term);
    }
}
