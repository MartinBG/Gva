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

        public void AddInventoryItem(GvaInventoryItem inventoryItem)
        {
            this.unitOfWork.DbContext.Set<GvaInventoryItem>().Add(inventoryItem);
        }

        public GvaInventoryItem GetInventoryItem(int partId)
        {
            return this.unitOfWork.DbContext.Set<GvaInventoryItem>()
                .SingleOrDefault(i => i.PartId == partId);
        }

        public IEnumerable<GvaInventoryItem> GetInventoryItemsForLot(int lotId)
        {
            return this.unitOfWork.DbContext.Set<GvaInventoryItem>()
                .Include(i => i.Part)
                .Where(i => i.LotId == lotId);
        }

        public void DeleteInventoryItemsForPart(int partId)
        {
            var inventoryItems = this.unitOfWork.DbContext.Set<GvaInventoryItem>()
                .Where(i => i.PartId == partId)
                .ToList();

            foreach (var inventoryItem in inventoryItems)
            {
                this.unitOfWork.DbContext.Set<GvaInventoryItem>().Remove(inventoryItem);
            }
        }

        public void DeleteInventoryItem(GvaInventoryItem inventoryItem)
        {
            this.unitOfWork.DbContext.Set<GvaInventoryItem>().Remove(inventoryItem);
        }
    }
}
