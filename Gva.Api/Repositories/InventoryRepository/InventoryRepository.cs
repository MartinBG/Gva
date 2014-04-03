using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Data;
using Gva.Api.Models;

namespace Gva.Api.Repositories.InventoryRepository
{
    public class InventoryRepository : IInventoryRepository
    {
        private IUnitOfWork unitOfWork;

        public InventoryRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<GvaViewInventoryItem> GetInventoryItemsForLot(int lotId)
        {
            return this.unitOfWork.DbContext.Set<GvaViewInventoryItem>()
                .Include(i => i.Part)
                .Where(i => i.LotId == lotId);
        }
    }
}
