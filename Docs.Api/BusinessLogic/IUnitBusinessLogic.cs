using Docs.Api.Models.DomainModels;
using System.Collections.Generic;

namespace Docs.Api.BusinessLogic
{
    public interface IUnitBusinessLogic
    {
        IEnumerable<UnitDomainModel> GetAllUnitsHierarchy(bool includeInactive);        

        UnitDomainModel GetUnitById(int unitId);

        UnitDomainModel CreateUnit(UnitDomainModel model);

        void UpdateUnit(UnitDomainModel model);

        void DeleteUnit(int id);

        void SetUnitActiveStatus(int unitId, bool isActive);
    }
}
