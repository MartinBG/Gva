using System.Collections.Generic;
using Gva.Api.Models.Views.Equipment;

namespace Gva.Api.Repositories.EquipmentRepository
{
    public interface IEquipmentRepository
    {
        IEnumerable<GvaViewEquipment> GetEquipments(
            string name = null,
            bool exact = false,
            int offset = 0,
            int? limit = null);

        GvaViewEquipment GetEquipment(int equipmentId);
    }
}
