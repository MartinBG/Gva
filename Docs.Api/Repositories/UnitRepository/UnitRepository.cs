using Common.Api.Models;
using Common.Data;
using Docs.Api.Enums;
using Docs.Api.Infrastructure;
using Docs.Api.Models.DomainModels;
using Docs.Api.Models.UnitModels;
using Docs.Api.Repositories.DocRepository;
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
        private IDocRepository docRepository;

        public UnitRepository(IUnitOfWork unitOfWork, IDocRepository docRepository)
        {
            this.unitOfWork = unitOfWork;
            this.docRepository = docRepository;
            unitsInContext = unitOfWork.DbContext.Set<Unit>();
        }

        public IEnumerable<UnitDomainModel> GetListOfAllActiveUnits()
        {
            var domainEntities = unitsInContext
                .Include(e => e.UnitRelations)
                .Include(e => e.UnitType)
                .Include(e => e.UnitUsers
                    .Select(uu => uu.User))
                .Where(e => e.IsActive == true)
                .OrderBy(e => e.UnitTypeId)
                .Select(e => new UnitDomainModel {
                    UnitId = e.UnitId,
                    Name = e.Name,
                    Type = e.UnitType.Alias,
                    IsActive = e.IsActive,
                    RootUnitId = e.UnitRelations.FirstOrDefault().RootUnitId,
                    ParentUnitId = e.UnitRelations.FirstOrDefault().ParentUnitId,
                    User = e.UnitUsers.Where(r => r.IsActive)
                        .Select(r => new UserForUnitAttachmentDomainModel {
                            UserId = r.User.UserId,
                            UserName = r.User.Username,
                            FullName = r.User.Fullname,
                            IsActive = r.User.IsActive
                        })
                        .FirstOrDefault()
                });

            return domainEntities;
        }

        public IEnumerable<UnitDomainModel> GetListOfAllUnits()
        {
            var domainEntities = unitsInContext
               .Include(e => e.UnitRelations)
               .Include(e => e.UnitType)
               .Include(e => e.UnitUsers
                   .Select(uu => uu.User))
               .OrderBy(e => e.UnitTypeId)
               .Select(e => new UnitDomainModel {
                   UnitId = e.UnitId,
                   Name = e.Name,
                   Type = e.UnitType.Alias,
                   IsActive = e.IsActive,
                   RootUnitId = e.UnitRelations.FirstOrDefault().RootUnitId,
                   ParentUnitId = e.UnitRelations.FirstOrDefault().ParentUnitId,
                   User = e.UnitUsers.Where(r => r.IsActive)
                       .Select(r => new UserForUnitAttachmentDomainModel {
                           UserId = r.User.UserId,
                           UserName = r.User.Username,
                           FullName = r.User.Fullname,
                           IsActive = r.User.IsActive
                       })
                       .FirstOrDefault()
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
                            ClassificationId = c.ClassificationId,
                            ClassificationPermissionId = c.ClassificationPermissionId,
                            ClassificationName = c.Classification.Name,
                            ClassificationPermissionName = c.ClassificationPermission.Name
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
                .Include(e => e.UnitUsers)
                .SingleOrDefault(e => e.UnitId == id);


            if (entity == null)
            {
                throw new Exception(string.Format("Unit with ID = {0} does not exist.", id));
            }

            var activeUnitUser = entity.UnitUsers
                .SingleOrDefault(e => e.IsActive);
            if (activeUnitUser != null)
            {
                activeUnitUser.IsActive = false;
            }

            unitOfWork.Save();
        }

        public void Activate(int id)
        {
            var entity = unitsInContext
                .SingleOrDefault(e => e.UnitId == id);

            if (entity == null)
            {
                throw new Exception(string.Format("Unit with ID = {0} does not exist.", id));
            }

            entity.IsActive = true;

            unitOfWork.Save();
        }

        public void Deactivate(int id)
        {
            var entity = unitsInContext
                .Include(e => e.UnitUsers)
                .SingleOrDefault(e => e.UnitId == id);

            if (entity == null)
            {
                throw new Exception(string.Format("Unit with ID = {0} does not exist.", id));
            }

            entity.IsActive = false;

            var activeUnitUser = entity.UnitUsers
                .SingleOrDefault(e => e.IsActive);
            if (activeUnitUser != null)
            {
                activeUnitUser.IsActive = false;
            }

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
                },
                UnitClassifications = new List<UnitClassification>()
            };

            foreach (var item in model.Classifications)
            {
                unit.UnitClassifications.Add(new UnitClassification {
                    ClassificationId = item.ClassificationId,
                    ClassificationPermissionId = item.ClassificationPermissionId
                });
            }

            using (var transaction = unitOfWork.BeginTransaction())
            {
                unitsInContext.Add(unit);
                unitOfWork.Save();

                docRepository.spSetUnitTokens(unit.UnitId);

                transaction.Commit();
            }

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
            var unit = unitsInContext
                .Include(e => e.UnitClassifications)
                .SingleOrDefault(e =>
                e.UnitId == model.UnitId);
            if (unit == null)
            {
                throw new Exception(string.Format("Unit with ID = {0} does not exist.", model.UnitId));
            }

            //only Name and classifications can be updated from here
            unit.Name = model.Name;

            var unitClassificationContext = unitOfWork.DbContext.Set<UnitClassification>();
            //var classifications = unitClassificationContext.Where(e => e.UnitId == model.UnitId);

            foreach (var item in unit.UnitClassifications)
            {
                //var it = model.Classifications.SingleOrDefault(e =>
                //    e.ClassificationId == item.ClassificationId
                //    && e.ClassificationPermissionId == item.ClassificationPermissionId);
                //if(it!=null)
                //{

                //}
            }            

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

            using (var transaction = unitOfWork.BeginTransaction())
            {
                var unitRelationContext = unitOfWork.DbContext.Set<UnitRelation>();
                unitRelationContext.Remove(entity.UnitRelations.First());

                var unitTokensContext = unitOfWork.DbContext.Set<UnitToken>();
                var unitTokens = unitTokensContext.Where(e => e.UnitId == id)
                    .ToList();
                unitTokensContext.RemoveRange(unitTokensContext);
                unitOfWork.Save();

                unitsInContext.Remove(entity);
                unitOfWork.Save();

                transaction.Commit();
            }
        }

        public void AssignUserToUnit(int unitId, int userId)
        {
            var validator = new DomainValidator();
            validator.AddErrorMessage(DomainErrorCode.Entity_NotFound);
            validator.AddErrorMessage(DomainErrorCode.Entity_NotFound, "Unit", 12);
            validator.Validate();

            var unit = unitsInContext.SingleOrDefault(e =>
                e.UnitId == unitId
                && (Docs.Api.Models.DomainModels.UnitType)e.UnitTypeId == Docs.Api.Models.DomainModels.UnitType.Employee
                && e.IsActive);

            if (unit == null)
            {
                validator.AddErrorMessage(DomainErrorCode.Unit_NotFound_ActivOfTypeEmployee);
            }

            var user = unitOfWork.DbContext.Set<User>().SingleOrDefault(e =>
                    e.UserId == userId
                    && e.IsActive);
            if (user == null)
            {
                validator.AddErrorMessage(DomainErrorCode.Entity_NotFoundOrNotActive);
            }

            validator.Validate();

            var unitUsersInContext = unitOfWork.DbContext.Set<UnitUser>();
            var doesUnitAlreadyExistInRelation = unitUsersInContext.Any(e =>
                e.UnitId == unitId
                && e.IsActive);
            if (doesUnitAlreadyExistInRelation)
            {
                validator.AddErrorMessage(DomainErrorCode.Entity_AlreadyExistInRelation);
            }

            var doesUserAlreadyExistInRelation = unitUsersInContext.Any(e =>
                e.UserId == userId
                && e.IsActive);
            if (doesUserAlreadyExistInRelation)
            {
                validator.AddErrorMessage(DomainErrorCode.Entity_AlreadyExistInRelation);
            }

            validator.Validate();

            var unitUser = new UnitUser {
                Unit = unit,
                User = user
            };

            unitOfWork.DbContext.Set<UnitUser>()
                .Add(unitUser);

            unitOfWork.Save();
        }
    }
}
