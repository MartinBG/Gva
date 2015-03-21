using System.Linq;
using System.Web.Http;
using Common.Data;
using Common.Linq;
using Docs.Api.Models;
using Docs.Api.Models.UnitModels;
using System;
using Docs.Api.BusinessLogic;
using Docs.Api.Models.ClassificationModels;
using Docs.Api.Models.DomainModels;

namespace Docs.Api.Controllers
{
    public class UnitController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IUnitBusinessLogic unitBusinessLogic;

        public UnitController(IUnitOfWork unitOfWork, IUnitBusinessLogic unitBusinessLogic)
        {
            this.unitOfWork = unitOfWork;
            this.unitBusinessLogic = unitBusinessLogic;
        }

        [Route("api/nomenclatures/units")]
        public IHttpActionResult GetUnits(string name = null)
        {
            var predicate =
                PredicateBuilder.True<Unit>()
                .AndStringContains(c => c.Name, name)
                .And(c => c.IsActive);

            var units = this.unitOfWork.DbContext.Set<Unit>()
                .Where(predicate)
                .Select(e => new
                {
                    nomValueId = e.UnitId,
                    name = e.Name,
                    alias = e.Name,
                    isActive = e.IsActive
                })
                .ToList();

            return Ok(units);
        }

        [Route("api/units")]
        [HttpGet]
        public IHttpActionResult GetAllUnitsHierarchy(bool includeInactive = false)
        {
            return Ok(unitBusinessLogic.GetAllUnitsHierarchy(includeInactive));
        }

        [Route("api/units/classificationPermissions")]
        [HttpGet]
        public IHttpActionResult GetClassificationPermissions(bool includeInactive = false)
        {
            var result = unitOfWork.DbContext.Set<ClassificationPermission>()
                .Select(e => new
                {
                    Id = e.ClassificationPermissionId,
                    Name = e.Name,
                    Alias = e.Alias
                }).ToList();

            return Ok(result);
        }

        [Route("api/units/classifications")]
        [HttpGet]
        public IHttpActionResult GetClassifications(bool includeInactive = false)
        {
            var result = unitOfWork.DbContext.Set<Classification>()
                .Select(e => new
                {
                    Id = e.ClassificationId,
                    Name = e.Name,
                }).ToList();

            return Ok(result);
        }

        [Route("api/units/{unitId:int}")]
        [HttpGet]
        public IHttpActionResult GetUnitById(int unitId)
        {
            return Ok(unitBusinessLogic.GetUnitById(unitId));
        }

        [Route("api/units")]
        [HttpPost]
        public IHttpActionResult CreateUnit(UnitDomainModel model)
        {
            return Ok(unitBusinessLogic.CreateUnit(model));
        }

        [Route("api/units/{unitId:int}")]
        [HttpPost]
        public IHttpActionResult UpdateUnit(int unitId, UnitDomainModel model)
        {
            unitBusinessLogic.UpdateUnit(model);
            return Ok();
        }

        [Route("api/units/{unitId:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteUnit(int unitId)
        {
            // These validation should be in UnitBusiness logic. Currently UnitBL is in another project, that's why it's here.

            var unitRelation = unitOfWork.DbContext.Set<UnitRelation>()
                .Where(e => e.ParentUnitId == unitId)
                .FirstOrDefault();
            ValidateDeleteUnitRelations(unitRelation);

            var docUnits = unitOfWork.DbContext.Set<Docs.Api.Models.DocUnit>()
                .Where(e => e.UnitId == unitId)
                .FirstOrDefault();
            ValidateDeleteUnitRelations(docUnits);

            var docWorkflow = unitOfWork.DbContext.Set<Docs.Api.Models.DocWorkflow>()
                .Where(e => e.ToUnitId == unitId
                    || e.PrincipalUnitId == unitId)
                .FirstOrDefault();
            ValidateDeleteUnitRelations(docWorkflow);

            var unitUsers = unitOfWork.DbContext.Set<UnitUser>()
                .Where(e => e.UnitId == unitId)
                .FirstOrDefault();
            ValidateDeleteUnitRelations(unitUsers);

            var docTypeUnitRole = unitOfWork.DbContext.Set<Docs.Api.Models.DocTypeUnitRole>()
                .Where(e => e.UnitId == unitId)
                .FirstOrDefault();
            ValidateDeleteUnitRelations(docTypeUnitRole);

            var docHasRead = unitOfWork.DbContext.Set<Docs.Api.Models.DocHasRead>()
                .Where(e => e.UnitId == unitId)
                .FirstOrDefault();
            ValidateDeleteUnitRelations(docHasRead);

            var electronicServiceStageExecutor = unitOfWork.DbContext.Set<Docs.Api.Models.ElectronicServiceStageExecutor>()
                .Where(e => e.UnitId == unitId)
                .FirstOrDefault();
            ValidateDeleteUnitRelations(electronicServiceStageExecutor);

            unitBusinessLogic.DeleteUnit(unitId);
            return Ok();
        }

        private void ValidateDeleteUnitRelations(object objectWithRelation)
        {
            if (objectWithRelation != null)
            {
                throw new Exception("Unit can't be deleted, because other object has relation to it.");
            }
        }

        [Route("api/units/{id:int}/status/{isActive:bool}")]
        [HttpPost]
        public IHttpActionResult SetUnitActiveStatus(int id, bool isActive)
        {
            unitBusinessLogic.SetUnitActiveStatus(id, isActive);
            return Ok();
        }
    }
}
