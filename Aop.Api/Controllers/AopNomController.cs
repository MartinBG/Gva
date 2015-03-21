using Common.Api.Models;
using Common.Api.UserContext;
using Common.Data;
using Common.Linq;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using Aop.Api.DataObjects;
using Docs.Api.Models.UnitModels;

namespace Aop.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/noms")]
    public class AopNomController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private UserContext userContext;

        public AopNomController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            try
            {
                this.userContext = this.Request.GetUserContext();
            }
            catch { }
        }

        [Route("units")]
        [HttpGet]
        public IHttpActionResult GetUnits(string name = null, int? unitTypeId = null, int offset = 0, int? limit = null)
        {
            var query = this.unitOfWork.DbContext.Set<Unit>()
                .AsQueryable();

            var predicate =
                    PredicateBuilder.True<Unit>()
                    .AndEquals(e => e.UnitTypeId, unitTypeId)
                    .AndStringContains(e => e.Name, name);

            query = query
                .Where(predicate)
                .OrderBy(e => e.Name)
                .WithOffsetAndLimit(offset, limit);

            var result = query
                .Include(e => e.UnitType)
                .Include(e => e.UnitRelations.Select(k => k.ParentUnit))
                .ToList()
                .Select(e => new UnitDO(e))
                .ToList();

            return Ok(result);
        }

        [Route("units/new")]
        [HttpGet]
        public IHttpActionResult GetNewUnit()
        {
            return Ok(new UnitDO());
        }

        [Route("units/{id}")]
        [HttpGet]
        public IHttpActionResult GetUnit(int id)
        {
            var unit = this.unitOfWork.DbContext.Set<Unit>()
                .Include(e => e.UnitType)
                .Include(e => e.UnitRelations.Select(k => k.ParentUnit))
                .FirstOrDefault(e => e.UnitId == id);

            if (unit == null)
            {
                return NotFound();
            }

            UnitDO returnValue = new UnitDO(unit);

            return Ok(returnValue);
        }

        [Route("units")]
        [HttpPost]
        public IHttpActionResult PostUnit(UnitDO unit)
        {
            try
            {
                using (var transaction = this.unitOfWork.BeginTransaction())
                {
                    Unit newUnit = new Unit();

                    newUnit.Name = unit.Name;
                    newUnit.UnitTypeId = unit.UnitTypeId.Value;
                    newUnit.IsActive = unit.IsActive;
                    newUnit.InheritParentClassification = unit.InheritParentClassification;
                    this.unitOfWork.DbContext.Set<Unit>().Add(newUnit);

                    UnitRelation unitRelation = new UnitRelation();
                    unitRelation.Unit = newUnit;
                    unitRelation.ParentUnitId = unit.ParentId;

                    if (unitRelation.ParentUnitId.HasValue)
                    {
                        Unit parent = this.unitOfWork.DbContext.Set<Unit>()
                            .Include(e => e.UnitRelations)
                            .FirstOrDefault(e => e.UnitId == unitRelation.ParentUnitId.Value);

                        if (parent != null && parent.UnitRelations.Any())
                        {
                            unitRelation.RootUnit = null;
                            unitRelation.RootUnitId = parent.UnitRelations.First().RootUnitId;
                        }
                        else
                        {
                            return Ok(new { err = "Проблем със записа." });
                        }
                    }
                    else
                    {
                        unitRelation.RootUnit = newUnit;
                    }

                    this.unitOfWork.DbContext.Set<UnitRelation>().Add(unitRelation);
                    this.unitOfWork.Save();

                    //this.unitOfWork.Repo<Doc>().ExecSpSetUserDocs(newUnit.UnitId);

                    transaction.Commit();

                    return Ok(new { err = "" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { err = ex.Message });
            }
        }

        [Route("units/{id}")]
        [HttpPost]
        public IHttpActionResult UpdateUnit(int id, UnitDO unit)
        {
            var oldUnit = this.unitOfWork.DbContext.Set<Unit>()
                .Include(e => e.UnitRelations)
                .FirstOrDefault(e => e.UnitId == id);

            if (oldUnit == null)
            {
                return NotFound();
            }

            if (!oldUnit.Version.SequenceEqual(unit.Version))
            {
                return Ok(new { err = "Съществува нова версия на служителя." });
            }

            if (unit.ParentId == unit.UnitId)
            {
                return Ok(new { err = "Не може потребител да е родител на себе си." });
            }

            try
            {
                using (var transaction = this.unitOfWork.BeginTransaction())
                {
                    bool parentChanged = false;
                    UnitRelation unitRelation = oldUnit.UnitRelations.SingleOrDefault();

                    if (unitRelation != null)
                    {
                        parentChanged = unitRelation.ParentUnitId != unit.ParentId;

                        if (parentChanged)
                        {
                            unitRelation.ParentUnit = null;
                            unitRelation.ParentUnitId = unit.ParentId;

                            if (unitRelation.ParentUnitId.HasValue)
                            {
                                Unit parent = this.unitOfWork.DbContext.Set<Unit>()
                                    .Include(e => e.UnitRelations)
                                    .FirstOrDefault(e => e.UnitId == unitRelation.ParentUnitId.Value);

                                if (parent != null && parent.UnitRelations.Any())
                                {
                                    unitRelation.RootUnit = null;
                                    unitRelation.RootUnitId = parent.UnitRelations.First().RootUnitId;
                                }
                                else
                                {
                                    return Ok(new { err = "Проблем със записа." });
                                }
                            }
                            else
                            {
                                unitRelation.RootUnit = null;
                                unitRelation.RootUnitId = oldUnit.UnitId;
                            }
                        }
                    }
                    else
                    {
                        return Ok(new { err = "Проблем със записа." });
                    }

                    oldUnit.Name = unit.Name;
                    oldUnit.UnitTypeId = unit.UnitTypeId.Value;
                    oldUnit.InheritParentClassification = unit.InheritParentClassification;
                    oldUnit.IsActive = unit.IsActive;

                    this.unitOfWork.Save();

                    //if (parentChanged)
                    //{
                    //    this.unitOfWork.Repo<Doc>().ExecSpSetUserDocs(oldUnit.UnitId);
                    //}

                    this.unitOfWork.Save();
                    transaction.Commit();

                    return Ok(new { err = "" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { err = ex.Message });
            }
        }

        [Route("units/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteUnit(int id)
        {
            Unit unit = this.unitOfWork.DbContext.Set<Unit>()
                .Include(e => e.UnitRelations)
                .FirstOrDefault(e => e.UnitId == id);

            if (unit == null)
            {
                return NotFound();
            }

            try
            {
                using (var transaction = this.unitOfWork.BeginTransaction())
                {
                    this.unitOfWork.DbContext.Set<UnitRelation>().Remove(unit.UnitRelations.SingleOrDefault());
                    this.unitOfWork.DbContext.Set<Unit>().Remove(unit);

                    this.unitOfWork.Save();

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                return Ok(new { err = ex.Message });
            }

            return Ok();
        }
    }
}
