using Common.DomainValidation;
using Docs.Api.Enums;
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
        IDomainValidator validator;

        public UnitBusinessLogic(IUnitRepository unitRepository, IDomainValidator validator)
        {
            this.unitRepository = unitRepository;
            this.validator = validator;
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
                var currentUnit = unitRepository.GetUnitById(unitId);
                if (currentUnit.ParentUnitId.HasValue)
                {
                    var parentUnit = unitRepository.GetUnitById(currentUnit.ParentUnitId.Value);
                    if (!parentUnit.IsActive)
                    {
                        validator.AddErrorMessage(DomainErrorCode.Entity_CannotBeActivated);
                        validator.Validate();
                    }
                }

                unitRepository.Activate(unitId);
            }
            else
            {                
                var currentUnitHierarchy = GetSubHierarchyForUnit(unitId);

                if (!AreAllChildsInactive(currentUnitHierarchy))
                {
                    validator.AddErrorMessage(DomainErrorCode.Entity_CannotBeDeactivated);
                    validator.Validate();
                }

                unitRepository.Deactivate(unitId);
            }
        }

        private UnitDomainModel GetSubHierarchyForUnit(int id)
        {
            var allUnits = unitRepository.GetListOfAllUnits()
                .ToDictionary(e => e.UnitId);

            foreach (var item in allUnits)
            {
                if (item.Value.ParentUnitId.HasValue)
                {
                    allUnits[item.Value.ParentUnitId.Value].AddChild(item.Value);
                }
            }

            return allUnits[id];
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
