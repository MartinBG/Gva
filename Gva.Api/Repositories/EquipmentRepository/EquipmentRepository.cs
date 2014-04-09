using System.Collections.Generic;

using System.Linq;
using Common.Data;
using Common.Linq;
using Gva.Api.Models;
using System.Data.Entity;
using Gva.Api.ModelsDO;
using Regs.Api.Models;

namespace Gva.Api.Repositories.EquipmentRepository
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private IUnitOfWork unitOfWork;

        public EquipmentRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<GvaViewEquipment> GetEquipments(string name,
            bool exact,
            int offset = 0,
            int? limit = null)
        {
            var gvaEquipments = this.unitOfWork.DbContext.Set<GvaViewEquipment>();

            var predicate = PredicateBuilder.True<GvaViewEquipment>();

            predicate = predicate
                .AndStringMatches(p => p.Name, name, exact);

            return gvaEquipments
                .Where(predicate)
                .OrderBy(p => p.Name)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
        }
        public GvaViewEquipment GetEquipment(int equipmentId)
        {
            return this.unitOfWork.DbContext.Set<GvaViewEquipment>()
                .SingleOrDefault(p => p.LotId == equipmentId);
        }
    }
}
