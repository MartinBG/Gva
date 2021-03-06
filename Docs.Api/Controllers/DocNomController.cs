﻿using Common.Api.Models;
using Common.Extensions;
using Common.Linq;
using Docs.Api.Models;
using Docs.Api.Models.ClassificationModels;
using Docs.Api.Models.UnitModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Docs.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/nomenclatures")]
    public class DocNomController : ApiController
    {
        private Common.Data.IUnitOfWork unitOfWork;

        public DocNomController(Common.Data.IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [Route("registerIndex/{id:int}")]
        public IHttpActionResult GetRegisterIndex(int id)
        {
            var result = this.unitOfWork.DbContext.Set<RegisterIndex>()
                .Where(e => e.RegisterIndexId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.RegisterIndexId,
                name = result.Name,
                alias = result.Alias,
                isActive = result.IsActive
            });
        }

        [Route("registerIndex")]
        public IHttpActionResult GetRegisterIndexes(string term = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<RegisterIndex>()
                .AndStringContains(e => e.Name, term)
                .And(e => e.IsActive);

            var results =
                this.unitOfWork.DbContext.Set<RegisterIndex>()
                .Where(predicate)
                .OrderBy(e => e.RegisterIndexId)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    nomValueId = e.RegisterIndexId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return Ok(results);
        }

        [Route("correspondentGroup/{id:int}")]
        public IHttpActionResult GetCorrespondentGroup(int id)
        {
            var result = this.unitOfWork.DbContext.Set<CorrespondentGroup>()
                .Where(e => e.CorrespondentGroupId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.CorrespondentGroupId,
                name = result.Name,
                alias = result.Alias,
                isActive = result.IsActive
            });
        }

        [Route("correspondentGroup")]
        public IHttpActionResult GetCorrespondentGroups(string term = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<CorrespondentGroup>()
                .AndStringContains(e => e.Name, term)
                .And(e => e.IsActive);

            var results =
                this.unitOfWork.DbContext.Set<CorrespondentGroup>()
                .Where(predicate)
                .OrderBy(e => e.Name)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    nomValueId = e.CorrespondentGroupId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return Ok(results);
        }

        [Route("correspondentType/{id:int}")]
        public IHttpActionResult GetCorrespondentType(int id)
        {
            var result = this.unitOfWork.DbContext.Set<CorrespondentType>()
                .Where(e => e.CorrespondentTypeId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.CorrespondentTypeId,
                name = result.Name,
                alias = result.Alias,
                isActive = result.IsActive
            });
        }

        [Route("correspondentType")]
        public IHttpActionResult GetCorrespondentTypes(string term = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<CorrespondentType>()
                .AndStringContains(e => e.Name, term)
                .And(e => e.IsActive);

            var results =
                this.unitOfWork.DbContext.Set<CorrespondentType>()
                .Where(predicate)
                .OrderBy(e => e.Name)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    nomValueId = e.CorrespondentTypeId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return Ok(results);
        }

        [Route("docFormatType/{id:int}")]
        public IHttpActionResult GetDocFormatType(int id)
        {
            var result = this.unitOfWork.DbContext.Set<DocFormatType>()
                .Where(e => e.DocFormatTypeId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.DocFormatTypeId,
                name = result.Name,
                alias = result.Alias,
                isActive = result.IsActive
            });
        }

        [Route("docFormatType")]
        public IHttpActionResult GetDocFormatTypes(string term = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<DocFormatType>()
                .AndStringContains(e => e.Name, term)
                .And(e => e.IsActive);

            var results =
                this.unitOfWork.DbContext.Set<DocFormatType>()
                .Where(predicate)
                .OrderBy(e => e.Name)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    nomValueId = e.DocFormatTypeId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return Ok(results);
        }

        [Route("docCasePartType/{id:int}")]
        public IHttpActionResult GetDocCasePartType(int id)
        {
            var result = this.unitOfWork.DbContext.Set<DocCasePartType>()
                .Where(e => e.DocCasePartTypeId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.DocCasePartTypeId,
                name = result.Name,
                alias = result.Alias,
                isActive = result.IsActive
            });
        }

        [Route("docCasePartType")]
        public IHttpActionResult GetDocCasePartTypes(string term = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<DocCasePartType>()
                .AndStringContains(e => e.Name, term)
                .And(e => e.IsActive);

            var results =
                this.unitOfWork.DbContext.Set<DocCasePartType>()
                .Where(predicate)
                .OrderBy(e => e.DocCasePartTypeId)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    nomValueId = e.DocCasePartTypeId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return Ok(results);
        }

        [Route("docDirection/{id:int}")]
        public IHttpActionResult GetDocDirection(int id)
        {
            var result = this.unitOfWork.DbContext.Set<DocDirection>()
                .Where(e => e.DocDirectionId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.DocDirectionId,
                name = result.Name,
                alias = result.Alias,
                isActive = result.IsActive
            });
        }

        [Route("docDirection")]
        public IHttpActionResult GetDocDirections(string term = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<DocDirection>()
                .AndStringContains(e => e.Name, term)
                .And(e => e.IsActive);

            var results =
                this.unitOfWork.DbContext.Set<DocDirection>()
                .Where(predicate)
                .OrderBy(e => e.Name)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    nomValueId = e.DocDirectionId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return Ok(results);
        }

        [Route("classification/{id:int}")]
        public IHttpActionResult GetClassification(int id)
        {
            var result = this.unitOfWork.DbContext.Set<Classification>()
                .Where(e => e.ClassificationId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.ClassificationId,
                name = result.Name,
                alias = result.Alias,
                isActive = result.IsActive
            });
        }

        [Route("classification")]
        public IHttpActionResult GetClassifications(string term = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<Classification>()
                .AndStringContains(e => e.Name, term)
                .And(e => e.IsActive);

            var results =
                this.unitOfWork.DbContext.Set<Classification>()
                .Where(predicate)
                .OrderBy(e => e.ClassificationId)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    nomValueId = e.ClassificationId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return Ok(results);
        }

        [Route("classificationPermission/{id:int}")]
        public IHttpActionResult GetClassificationPermission(int id)
        {
            var result = this.unitOfWork.DbContext.Set<ClassificationPermission>()
                .Where(e => e.ClassificationPermissionId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.ClassificationPermissionId,
                name = result.Name,
                alias = result.Alias,
                isActive = result.IsActive
            });
        }

        [Route("classificationPermission")]
        public IHttpActionResult GetClassificationPermissions(string term = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<ClassificationPermission>()
                .AndStringContains(e => e.Name, term)
                .And(e => e.IsActive);

            var results =
                this.unitOfWork.DbContext.Set<ClassificationPermission>()
                .Where(predicate)
                .OrderBy(e => e.ClassificationPermissionId)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    nomValueId = e.ClassificationPermissionId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return Ok(results);
        }

        [Route("docTypeGroup/{id:int}")]
        public IHttpActionResult GetDocTypeGroup(int id)
        {
            var result = this.unitOfWork.DbContext.Set<DocTypeGroup>()
                .Where(e => e.DocTypeGroupId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.DocTypeGroupId,
                name = result.Name,
                alias = result.Name, //todo is doctypegroup going to have alias?
                isActive = result.IsActive
            });
        }

        [Route("docTypeGroup")]
        public IHttpActionResult GetDocTypeGroups(string term = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<DocTypeGroup>()
                .AndStringContains(e => e.Name, term)
                .And(e => e.IsActive);

            var results =
                this.unitOfWork.DbContext.Set<DocTypeGroup>()
                .Where(predicate)
                .OrderBy(e => e.DocTypeGroupId)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    nomValueId = e.DocTypeGroupId,
                    name = e.Name,
                    isActive = e.IsActive
                })
                .ToList();

            return Ok(results);
        }

        [Route("docTypeGroup")]
        [HttpPost]
        public IHttpActionResult PostDocTypeGroup(DocTypeGroup model)
        {
            // Electronic Services can't be edited from the UI.
            model.IsElectronicService = false;

            unitOfWork.DbContext.Set<DocTypeGroup>()
                .Add(model);

            unitOfWork.Save();

            return Ok();
        }

        [Route("docTypeGroup/{id:int}")]
        [HttpPost]
        public IHttpActionResult PostDocTypeGroup(int id, DocTypeGroup model)
        {
            var context = unitOfWork.DbContext.Set<DocTypeGroup>();
            var entitiy = context.SingleOrDefault(e=>
                e.DocTypeGroupId == id);

            entitiy.Name = model.Name;
            entitiy.IsActive = model.IsActive;

            unitOfWork.Save();

            return Ok();
        }

        [Route("docType/{id:int}")]
        public IHttpActionResult GetDocType(int id)
        {
            var result = this.unitOfWork.DbContext.Set<DocType>()
                .Where(e => e.DocTypeId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.DocTypeId,
                name = result.Name,
                alias = result.Alias,
                isActive = result.IsActive
            });
        }

        [Route("docType")]
        public IHttpActionResult GetDocTypes(string term = null, int? parentValueId = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<DocType>()
                .AndStringContains(e => e.Name, term)
                .AndEquals(e => e.DocTypeGroupId, parentValueId)
                .And(e => e.IsActive);

            var results =
                this.unitOfWork.DbContext.Set<DocType>()
                .Where(predicate)
                .OrderBy(e => e.Name)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    nomValueId = e.DocTypeId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return Ok(results);
        }

        [Route("docType")]
        [HttpPost]
        public IHttpActionResult PostDocType(DocType model)
        {
            // Electronic Services can't be edited from the UI.
            model.IsElectronicService = false;

            unitOfWork.DbContext.Set<DocType>()
                .Add(model);

            unitOfWork.Save();

            return Ok();
        }

        [Route("docStatus/{id:int}")]
        public IHttpActionResult GetDocStatus(int id)
        {
            var result = this.unitOfWork.DbContext.Set<DocStatus>()
                .Where(e => e.DocStatusId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.DocStatusId,
                name = result.Name,
                alias = result.Alias,
                isActive = result.IsActive
            });
        }

        [Route("docStatus")]
        public IHttpActionResult GetDocStatuses(string term = null, int? parentValueId = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<DocStatus>()
                .AndStringContains(e => e.Name, term)
                .And(e => e.IsActive);

            var results =
                this.unitOfWork.DbContext.Set<DocStatus>()
                .Where(predicate)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    nomValueId = e.DocStatusId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return Ok(results);
        }

        [Route("unit/{id:int}")]
        public IHttpActionResult GetUnit(int id)
        {
            var result = this.unitOfWork.DbContext.Set<Unit>()
                .Where(e => e.UnitId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.UnitId,
                name = result.Name,
                alias = result.Name,
                isActive = result.IsActive
            });
        }

        [Route("unit")]
        public IHttpActionResult GetUnits([FromUri] int[] ids, string term = null, int offset = 0, int? limit = null)
        {
            var query = this.unitOfWork.DbContext.Set<Unit>().AsQueryable();

            if (ids != null && ids.Length > 0)
            {
                query = query.Where(e => ids.Contains(e.UnitId));
            }
            else
            {
                var predicate =
                    PredicateBuilder.True<Unit>()
                    .AndStringContains(e => e.Name, term)
                    .And(e => e.IsActive);

                query = query
                    .Where(predicate)
                    .OrderBy(e => e.Name)
                    .WithOffsetAndLimit(offset, limit);
            }

            var results = query
                .Select(e => new
                {
                    nomValueId = e.UnitId,
                    name = e.Name,
                    alias = e.Name,
                    isActive = e.IsActive
                })
                .ToList();

            return Ok(results);
        }

        [Route("employeeUnit/{id:int}")]
        public IHttpActionResult GetEmployeeUnit(int id)
        {
            var result = this.unitOfWork.DbContext.Set<Unit>()
                .Include(e => e.UnitRelations.Select(d => d.ParentUnit))
                .Where(e => e.UnitType.Alias == "Employee" && e.UnitId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.UnitId,
                name = String.Format("{0}{1}", result.Name, result.UnitRelations.Any(f => f.ParentUnitId.HasValue) ? " (" + result.UnitRelations.First().ParentUnit.Name + ")" : String.Empty),
                alias = result.Name,
                isActive = result.IsActive
            });
        }

        [Route("employeeUnit")]
        public IHttpActionResult GetEmployeeUnits([FromUri] int[] ids, string term = null, int offset = 0, int? limit = null)
        {
            var query = this.unitOfWork.DbContext.Set<Unit>()
                .Include(e => e.UnitRelations.Select(d => d.ParentUnit)).AsQueryable();

            if (ids != null && ids.Length > 0)
            {
                query = query.Where(e => ids.Contains(e.UnitId));
            }
            else
            {
                var predicate =
                    PredicateBuilder.True<Unit>()
                    .And(e => e.UnitType.Alias == "Employee")
                    .AndStringContains(e => e.Name, term)
                    .And(e => e.IsActive);

                query = query
                   .Where(predicate)
                   .OrderBy(e => e.Name)
                   .WithOffsetAndLimit(offset, limit);
            }

            var results = query
                .ToList()
                .Select(e => new
                {
                    nomValueId = e.UnitId,
                    name = String.Format("{0}{1}", e.Name, e.UnitRelations.Any(f => f.ParentUnitId.HasValue) ? " (" + e.UnitRelations.First().ParentUnit.Name + ")" : String.Empty),
                    alias = e.Name,
                    isActive = e.IsActive
                })
                .ToList();

            return Ok(results);
        }

        [Route("correspondent/{id:int}")]
        public IHttpActionResult GetCorrespondent(int id)
        {
            var result = this.unitOfWork.DbContext.Set<Correspondent>()
                .Where(e => e.CorrespondentId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.CorrespondentId,
                name = result.DisplayName,
                alias = result.Alias,
                isActive = result.IsActive
            });
        }

        [Route("correspondent")]
        public IHttpActionResult GetCorrespondents([FromUri] int[] ids, string term = null, int offset = 0, int? limit = null)
        {
            var query = this.unitOfWork.DbContext.Set<Correspondent>().AsQueryable();

            if (ids != null && ids.Length > 0)
            {
                query = query.Where(e => ids.Contains(e.CorrespondentId));
            }
            else
            {
                var predicate =
                    PredicateBuilder.True<Correspondent>()
                    .AndStringContains(e => e.DisplayName, term)
                    .And(e => e.IsActive);

                query = query
                    .Where(predicate)
                    .OrderBy(e => e.DisplayName)
                    .WithOffsetAndLimit(offset, limit);
            }

            var results = query
                .Select(e => new
                {
                    nomValueId = e.CorrespondentId,
                    name = e.DisplayName,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return Ok(results);
        }

        [Route("correspondentEmail/{id:int}")]
        public IHttpActionResult GetCorrespondentEmail(int id)
        {
            var result = this.unitOfWork.DbContext.Set<Correspondent>()
                .Where(e => e.CorrespondentId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.CorrespondentId,
                name = result.DisplayName + " " + result.Email,
                alias = result.Alias,
                isActive = result.IsActive
            });
        }

        [Route("correspondentEmail")]
        public IHttpActionResult GetCorrespondentEmails([FromUri] int[] ids, string term = null, int offset = 0, int? limit = null)
        {
            var query = this.unitOfWork.DbContext.Set<Correspondent>().AsQueryable();

            if (ids != null && ids.Length > 0)
            {
                query = query.Where(e => ids.Contains(e.CorrespondentId));
            }
            else
            {
                var predicate =
                    PredicateBuilder.True<Correspondent>()
                    .AndStringContains(e => (e.DisplayName + e.Email), term)
                    .And(e => e.IsActive);

                query = query
                    .Where(predicate)
                    .OrderBy(e => e.DisplayName)
                    .WithOffsetAndLimit(offset, limit);
            }

            var results = query
                .Select(e => new
                {
                    nomValueId = e.CorrespondentId,
                    name = e.DisplayName + " " + e.Email,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return Ok(results);
        }

        [Route("docSourceType/{id:int}")]
        public IHttpActionResult GetDocSourceTypes(int id)
        {
            var result = this.unitOfWork.DbContext.Set<DocSourceType>()
                .Where(e => e.DocSourceTypeId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.DocSourceTypeId,
                name = result.Name,
                alias = result.Alias,
                isActive = result.IsActive
            });
        }

        [Route("docSourceType")]
        public IHttpActionResult GetDocSourceTypes(string term = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<DocSourceType>()
                .AndStringContains(e => e.Name, term)
                .And(e => e.IsActive);

            var results =
                this.unitOfWork.DbContext.Set<DocSourceType>()
                .Where(predicate)
                .OrderBy(e => e.Name)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    nomValueId = e.DocSourceTypeId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return Ok(results);
        }

        [Route("assignmentType/{id:int}")]
        public IHttpActionResult GetAssignmentType(int id)
        {
            var result = this.unitOfWork.DbContext.Set<AssignmentType>()
                .Where(e => e.AssignmentTypeId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.AssignmentTypeId,
                name = result.Name,
                alias = result.Alias,
                isActive = result.IsActive
            });
        }

        [Route("assignmentType")]
        public IHttpActionResult GetAssignmentTypes(string term = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<AssignmentType>()
                .AndStringContains(e => e.Name, term)
                .And(e => e.IsActive);

            var results =
                this.unitOfWork.DbContext.Set<AssignmentType>()
                .Where(predicate)
                .OrderBy(e => e.Name)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    nomValueId = e.AssignmentTypeId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return Ok(results);
        }

        [Route("docFileKind/{id:int}")]
        public IHttpActionResult GetDocFileKind(int id)
        {
            var result = this.unitOfWork.DbContext.Set<DocFileKind>()
                .Where(e => e.DocFileKindId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.DocFileKindId,
                name = result.Name,
                alias = result.Alias,
                isActive = result.IsActive
            });
        }

        [Route("docFileKind")]
        public IHttpActionResult GetDocFileKinds(string term = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<DocFileKind>()
                .AndStringContains(e => e.Name, term)
                .And(e => e.IsActive);

            var results =
                this.unitOfWork.DbContext.Set<DocFileKind>()
                .Where(predicate)
                .OrderBy(e => e.Name)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    nomValueId = e.DocFileKindId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return Ok(results);
        }

        [Route("docFileType/{id:int}")]
        public IHttpActionResult GetDocFileType(int id)
        {
            var result = this.unitOfWork.DbContext.Set<DocFileType>()
                .Where(e => e.DocFileTypeId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.DocFileTypeId,
                name = result.Name,
                alias = result.Alias,
                isActive = result.IsActive
            });
        }

        [Route("docFileType")]
        public IHttpActionResult GetDocFileTypes(string term = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<DocFileType>()
                .AndStringContains(e => e.Name, term)
                .And(e => e.IsActive);

            var results =
                this.unitOfWork.DbContext.Set<DocFileType>()
                .Where(predicate)
                .OrderBy(e => e.Name)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    nomValueId = e.DocFileTypeId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return Ok(results);
        }

        [Route("electronicServiceStage/{id:int}")]
        public IHttpActionResult GetElectronicServiceStage(int id)
        {
            var result = this.unitOfWork.DbContext.Set<ElectronicServiceStage>()
                .Where(e => e.ElectronicServiceStageId == id)
                .Select(e => new
                {
                    e.ElectronicServiceStageId,
                    e.Name,
                    e.Alias,
                    e.IsActive,
                    ExecutorName = e.ElectronicServiceStageExecutors.FirstOrDefault().Unit.Name
                })
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.ElectronicServiceStageId,
                name = result.Name,
                alias = result.Alias,
                isActive = result.IsActive,
                executorName = result.ExecutorName ?? "Непосочен"
            });
        }

        [Route("electronicServiceStage")]
        public IHttpActionResult GetElectronicServiceStages(string term = null, int? docTypeId = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<ElectronicServiceStage>()
                .AndStringContains(e => e.Name, term)
                .AndEquals(e => e.DocTypeId, docTypeId)
                .And(e => e.IsActive);

            var results =
                this.unitOfWork.DbContext.Set<ElectronicServiceStage>()
                .Where(predicate)
                .OrderBy(e => e.ElectronicServiceStageId)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    e.ElectronicServiceStageId,
                    e.Name,
                    e.Alias,
                    e.IsActive,
                    ExecutorName = e.ElectronicServiceStageExecutors.FirstOrDefault().Unit.Name
                })
                .ToList()
                .Select(e => new
                {
                    nomValueId = e.ElectronicServiceStageId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive,
                    executorName = e.ExecutorName ?? "Непосочен"
                });

            return Ok(results);
        }

        [Route("electronicServiceProvider/{id:int}")]
        public IHttpActionResult GetElectronicServiceProvider(int id)
        {
            var result = this.unitOfWork.DbContext.Set<ElectronicServiceProvider>()
                .Where(e => e.ElectronicServiceProviderId == id)
                .Select(e => new
                {
                    e.ElectronicServiceProviderId,
                    e.Code,
                    e.Name,
                    e.Bulstat,
                    e.Alias,
                    e.IsActive
                })
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.ElectronicServiceProviderId,
                code = result.Code,
                name = result.Name,
                bulstat = result.Bulstat,
                alias = result.Alias,
                isActive = result.IsActive
            });
        }

        [Route("electronicServiceProvider")]
        public IHttpActionResult GetElectronicServiceProviders(string term = null, bool? excludeActiveProvider = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<ElectronicServiceProvider>()
                .AndStringContains(e => e.Name, term);

            var results =
                this.unitOfWork.DbContext.Set<ElectronicServiceProvider>()
                .Where(predicate)
                .OrderBy(e => e.Code)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    nomValueId = e.ElectronicServiceProviderId,
                    code = e.Code,
                    name = e.Name,
                    bulstat = e.Bulstat,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            if (excludeActiveProvider.HasValue && excludeActiveProvider.Value)
            {
                results = results.Where(e => e.isActive == false).ToList();
            }

            return Ok(results);
        }

        [Route("irregularityType/{id:int}")]
        public IHttpActionResult GetIrregularityType(int id)
        {
            var result = this.unitOfWork.DbContext.Set<IrregularityType>()
                .Where(e => e.IrregularityTypeId == id)
                .Select(e => new
                {
                    e.IrregularityTypeId,
                    e.Name,
                    e.Alias,
                    e.Description,
                })
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.IrregularityTypeId,
                name = result.Name,
                alias = result.Alias,
                description = result.Description
            });
        }

        [Route("irregularityType")]
        public IHttpActionResult GetIrregularityTypes(string term = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<IrregularityType>()
                .AndStringContains(e => e.Name, term);

            var results =
                this.unitOfWork.DbContext.Set<IrregularityType>()
                .Where(predicate)
                .OrderBy(e => e.IrregularityTypeId)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    e.IrregularityTypeId,
                    e.Name,
                    e.Alias,
                    e.Description
                })
                .ToList()
                .Select(e => new
                {
                    nomValueId = e.IrregularityTypeId,
                    name = e.Name,
                    alias = e.Alias,
                    description = e.Description
                });

            return Ok(results);
        }

        [Route("unitType/{id:int}")]
        public IHttpActionResult GetUnitType(int id)
        {
            var result = this.unitOfWork.DbContext.Set<UnitType>()
                .Where(e => e.UnitTypeId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.UnitTypeId,
                name = result.Name,
                alias = result.Alias,
                isActive = result.IsActive
            });
        }

        [Route("unitType")]
        public IHttpActionResult GetUnitTypes(string term = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<UnitType>()
                .AndStringContains(e => e.Name, term)
                .And(e => e.IsActive);

            var results =
                this.unitOfWork.DbContext.Set<UnitType>()
                .Where(predicate)
                .OrderBy(e => e.Name)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    nomValueId = e.UnitTypeId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return Ok(results);
        }

        [Route("docsSettlements/{id:int}")]
        public IHttpActionResult GetSettlement(int id)
        {
            var result = this.unitOfWork.DbContext.Set<Settlement>()
                .Where(e => e.SettlementId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.SettlementId,
                name = result.Name,
                alias = result.Alias,
                isActive = result.IsActive
            });
        }

        [Route("docsSettlements")]
        public IHttpActionResult GetSettlements(string term = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<Settlement>()
                .AndStringContains(e => e.Name, term)
                .And(e => e.IsActive);

            var results =
                this.unitOfWork.DbContext.Set<Settlement>()
                .Where(predicate)
                .OrderBy(e => e.Name)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    nomValueId = e.SettlementId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return Ok(results);
        }

        [Route("docsCountries/{id:int}")]
        public IHttpActionResult GetCountry(int id)
        {
            var result = this.unitOfWork.DbContext.Set<Country>()
                .Where(e => e.CountryId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.CountryId,
                name = result.Name,
                alias = result.Alias,
                isActive = result.IsActive
            });
        }

        [Route("docsCountries")]
        public IHttpActionResult GetCountries(string term = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<Country>()
                .AndStringContains(e => e.Name, term)
                .And(e => e.IsActive);

            var results =
                this.unitOfWork.DbContext.Set<Country>()
                .Where(predicate)
                .OrderBy(e => e.Name)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    nomValueId = e.CountryId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return Ok(results);
        }
    }
}
