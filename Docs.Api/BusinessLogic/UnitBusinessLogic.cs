using Docs.Api.Models.DomainModels;
using Docs.Api.Repositories.UnitRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Docs.Api.BusinessLogic
{
    public class UnitBusinessLogic : IUnitBusinessLogic
    {
        IUnitRepository unitRepository;

        public UnitBusinessLogic(IUnitRepository unitRepository)
        {
            this.unitRepository = unitRepository;
        }

        public IEnumerable<UnitDomainModel> GetAllUnitsHierarchy(bool includeInactive)
        {
            Dictionary<int, UnitDomainModel> allUnits = null;

            if (includeInactive)
            {
                allUnits = unitRepository.GetListOfAllUnits()
                    .ToDictionary(e => e.UnitId);
            }
            else
            {
                allUnits = unitRepository.GetListOfAllActiveUnits()
                    .ToDictionary(e => e.UnitId);
            }

            foreach (var item in allUnits)
            {
                if (item.Value.ParentUnitId.HasValue)
                {
                    allUnits[item.Value.ParentUnitId.Value].AddChild(item.Value);
                }
            }

            var mainUnits = allUnits.Where(e =>
                e.Value.ParentUnitId == null)
                .Select(e => e.Value);

            return mainUnits;
        }

        public UnitDomainModel GetUnitById(int unitId)
        {
            return unitRepository.GetUnitById(unitId);
        }

        public UnitDomainModel CreateUnit(UnitDomainModel model)
        {
            if (model.ParentUnitId.HasValue)
            {
                var parent = unitRepository.GetUnitById(model.ParentUnitId.Value);
                model.RootUnitId = parent.RootUnitId;
            }

            return unitRepository.CreateUnit(model);
        }

        public void UpdateUnit(UnitDomainModel model)
        {
            unitRepository.UpdateUnit(model);
        }

        public void DeleteUnit(int id)
        {
            unitRepository.DeleteUnit(id);
        }

        public void SetUnitActiveStatus(int unitId, bool isActive)
        {
            if (isActive)
            {
                // Validate if parent unit is active.                
                var currentUnit = unitRepository.GetUnitById(unitId);
                if (currentUnit.ParentUnitId.HasValue)
                {
                    var parentUnit = unitRepository.GetUnitById(currentUnit.ParentUnitId.Value);
                    if (!parentUnit.IsActive)
                    {
                        throw new Exception(string.Format("Unit with ID = {0} can't be activated because its parent is not active.", unitId));
                    }
                }
            }
            else
            {
                // Validate if all child units in hierarchy are not active.
                var currentUnitHierarchy = GetSubHierarchyForUnit(unitId);
                var result = AreAllChildsInactive(currentUnitHierarchy);
                if (!result)
                {
                    throw new Exception(string.Format("Unit with ID = {0} can't be deactivated because its has active child units.", unitId));
                }
            }

            unitRepository.SetUnitActiveStatus(unitId, isActive);
        }

        private UnitDomainModel GetSubHierarchyForUnit(int id)
        {
            var unitsHierarchy = unitRepository.GetListOfAllUnits()
                .ToDictionary(e => e.UnitId);

            foreach (var item in unitsHierarchy)
            {
                if (item.Value.ParentUnitId.HasValue)
                {
                    unitsHierarchy[item.Value.ParentUnitId.Value].AddChild(item.Value);
                }
            }

            return unitsHierarchy[id];
        }

        private bool AreAllChildsInactive(UnitDomainModel unit)
        {
            bool result = true;

            foreach (var item in unit.ChildUnits)
            {
                result = result && !item.IsActive;
                if (item.ChildUnits.Count > 0)
                {
                    result = result && AreAllChildsInactive(item);
                }
            }

            return result;
        }
    }
}
