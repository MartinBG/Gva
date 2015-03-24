using Docs.Api.Models.DomainModels;
using System.Collections.Generic;

namespace Docs.Api.Repositories.UnitRepository
{
    public interface IUnitRepository
    {
        IEnumerable<UnitDomainModel> GetListOfAllUnits();

        IEnumerable<UnitDomainModel> GetListOfAllActiveUnits();

        UnitDomainModel GetUnitById(int unitId);

        UnitDomainModel CreateUnit(UnitDomainModel model);

        void UpdateUnit(UnitDomainModel model);

        void DeleteUnit(int id);

        void SetUnitActiveStatus(int unitId, bool isActive);

        void AssignUserToUnit(int unitId, int userId);
    }
}
