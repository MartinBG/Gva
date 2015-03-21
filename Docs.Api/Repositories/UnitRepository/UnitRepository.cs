using Common.Data;
using Docs.Api.Models.DomainModels;
using Docs.Api.Models.UnitModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Docs.Api.Repositories.UnitRepository
{
    class UnitRepository : IUnitRepository
    {
        private IUnitOfWork unitOfWork;
        private DbSet<Unit> unitsInContext;

        public UnitRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            unitsInContext = unitOfWork.DbContext.Set<Unit>();
        }

        public IEnumerable<UnitDomainModel> GetListOfAllActiveUnits()
        {
            var domainEntities = unitsInContext
                .Include(e => e.UnitRelations)
                .Include(e => e.UnitType)
                .Where(e => e.IsActive == true)
                .Select(e => new UnitDomainModel {
                    UnitId = e.UnitId,
                    Name = e.Name,
                    Type = e.UnitType.Alias,
                    IsActive = e.IsActive,
                    RootUnitId = e.UnitRelations.FirstOrDefault().RootUnitId,
                    ParentUnitId = e.UnitRelations.FirstOrDefault().ParentUnitId,
                });

            return domainEntities;
        }

        public IEnumerable<UnitDomainModel> GetListOfAllUnits()
        {
            var domainEntities = unitsInContext
                .Include(e => e.UnitRelations)
                .Include(e => e.UnitType)
                .Select(e => new UnitDomainModel {
                    UnitId = e.UnitId,
                    Name = e.Name,
                    Type = e.UnitType.Alias,
                    IsActive = e.IsActive,
                    RootUnitId = e.UnitRelations.FirstOrDefault().RootUnitId,
                    ParentUnitId = e.UnitRelations.FirstOrDefault().ParentUnitId,
                });

            return domainEntities;
        }

        public UnitDomainModel GetUnitById(int unitId)
        {
            var domainEntity = unitsInContext
                .Include(e => e.UnitRelations)
                .Include(e => e.UnitClassifications
                    .Select(n => n.Classification))
                .Include(e => e.UnitType)
                .Select(e => new UnitDomainModel {
                    UnitId = e.UnitId,
                    Name = e.Name,
                    Type = e.UnitType.Alias,
                    IsActive = e.IsActive,
                    RootUnitId = e.UnitRelations.FirstOrDefault().RootUnitId,
                    ParentUnitId = e.UnitRelations.FirstOrDefault().ParentUnitId,
                    Classifications = e.UnitClassifications
                        .Select(c => new UnitClassificationDomainModel {
                            UnitClassificationId = c.UnitClassificationId,
                            ClassificationName = c.ClassificationPermission.Name,
                            ClassificationPermission = c.ClassificationPermission.Name
                        }).ToList()
                })
                .SingleOrDefault(e => e.UnitId == unitId);

            if (domainEntity == null)
            {
                throw new Exception(string.Format("Unit with ID = {0} does not exist.", unitId));
            }

            return domainEntity;
        }

        public void SetUnitActiveStatus(int id, bool isActive)
        {
            var entity = unitsInContext
                .SingleOrDefault(e => e.UnitId == id);

            if (entity == null)
            {
                throw new Exception(string.Format("Unit with ID = {0} does not exist.", id));
            }

            entity.IsActive = isActive;

            unitOfWork.Save();
        }

        public UnitDomainModel CreateUnit(UnitDomainModel model)
        {
            var unit = new Unit {
                Name = model.Name,
                IsActive = true,
                UnitTypeId = (int)Enum.Parse(typeof(Models.DomainModels.UnitType), model.Type),
                UnitRelations = new List<UnitRelation> {
                    new UnitRelation {
                        ParentUnitId = model.ParentUnitId,
                        RootUnitId = model.ParentUnitId.HasValue ? model.ParentUnitId.Value : model.UnitId
                    }
                }
            };

            unitsInContext.Add(unit);
            unitOfWork.Save();

            return new UnitDomainModel {
                UnitId = unit.UnitId,
                Name = unit.Name,
                Type = ((Models.DomainModels.UnitType)unit.UnitTypeId).ToString(),
                IsActive = unit.IsActive,
                RootUnitId = unit.UnitRelations.FirstOrDefault().RootUnitId,
                ParentUnitId = unit.UnitRelations.FirstOrDefault().ParentUnitId,
            };
        }

        public void UpdateUnit(UnitDomainModel model)
        {
            var unit = unitsInContext.SingleOrDefault(e => e.UnitId == model.UnitId);
            if (unit == null)
            {
                throw new Exception(string.Format("Unit with ID = {0} does not exist.", model.UnitId));
            }

            //only Name and classifications can be updated from here
            unit.Name = model.Name;

            unitOfWork.Save();
        }

        public void DeleteUnit(int id)
        {            
            var entity = unitsInContext
                .Include(e => e.UnitRelations)
                .Include(e => e.UnitClassifications
                    .Select(n => n.Classification))
                .SingleOrDefault(e => e.UnitId == id);

            if (entity == null)
            {
                throw new Exception(string.Format("Unit with ID = {0} does not exist.", id));
            }

            var unitClassificationContext = unitOfWork.DbContext.Set<UnitClassification>();
            var unitClassifications = unitClassificationContext.Where(e => e.UnitId == id)
                .ToList();
            unitClassificationContext.RemoveRange(unitClassifications);

            var unitRelationContext = unitOfWork.DbContext.Set<UnitRelation>();
            unitRelationContext.Remove(entity.UnitRelations.First());

            var unitTokensContext = unitOfWork.DbContext.Set<UnitToken>();
            var unitTokens = unitTokensContext.Where(e => e.UnitId == id)
                .ToList();
            unitTokensContext.RemoveRange(unitTokensContext);


            unitsInContext.Remove(entity);
            unitOfWork.Save();
        }
    }
}
