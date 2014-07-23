using Common.Extensions;
using Common.Linq;
using Aop.Api.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;

namespace Aop.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/nomenclatures")]
    public class AppNomController : ApiController
    {
        private Common.Data.IUnitOfWork unitOfWork;

        public AppNomController(Common.Data.IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [Route("aopEmployer/{id:int}")]
        public IHttpActionResult GetAopEmployer(int id)
        {
            var result = this.unitOfWork.DbContext.Set<AopEmployer>()
                .Where(e => e.AopEmployerId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.AopEmployerId,
                name = string.Format("{0} {1} {2}", result.Name, result.Uic, result.LotNum),
                alias = string.Format("{0} {1}", result.Uic, result.LotNum),
                isActive = true
            });
        }

        [Route("aopEmployer")]
        public IHttpActionResult GetAopEmployers(string term = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<AopEmployer>()
                .AndStringContains(e => e.Name, term);

            var results =
                this.unitOfWork.DbContext.Set<AopEmployer>()
                .Where(predicate)
                .OrderBy(e => e.Name)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    nomValueId = e.AopEmployerId,
                    name = e.Name + " " + e.Uic + " " + e.LotNum,
                    alias = e.Uic + " " + e.LotNum,
                    isActive = true
                })
                .ToList();

            return Ok(results);
        }

        [Route("aopApplicationObject/{id:int}")]
        public IHttpActionResult GetAopApplicationObject(int id)
        {
            var result = this.unitOfWork.DbContext.Set<AopApplicationObject>()
                .Where(e => e.AopApplicationObjectId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.AopApplicationObjectId,
                name = result.Name,
                alias = result.Alias,
                isActive = true
            });
        }

        [Route("aopApplicationObject")]
        public IHttpActionResult GetAopApplicationObjects(string term = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<AopApplicationObject>()
                .AndStringContains(e => e.Name, term);

            var results =
                this.unitOfWork.DbContext.Set<AopApplicationObject>()
                .Where(predicate)
                .OrderBy(e => e.Name)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    nomValueId = e.AopApplicationObjectId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = true
                })
                .ToList();

            return Ok(results);
        }

        [Route("aopApplicationCriteria/{id:int}")]
        public IHttpActionResult GetAopApplicationCriteria(int id)
        {
            var result = this.unitOfWork.DbContext.Set<AopApplicationCriteria>()
                .Where(e => e.AopApplicationCriteriaId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.AopApplicationCriteriaId,
                name = result.Name,
                alias = result.Alias,
                isActive = true
            });
        }

        [Route("aopApplicationCriteria")]
        public IHttpActionResult GetAopApplicationCriterias(string term = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<AopApplicationCriteria>()
                .AndStringContains(e => e.Name, term);

            var results =
                this.unitOfWork.DbContext.Set<AopApplicationCriteria>()
                .Where(predicate)
                .OrderBy(e => e.Name)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    nomValueId = e.AopApplicationCriteriaId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = true
                })
                .ToList();

            return Ok(results);
        }

        [Route("aopProcedureStatus/{id:int}")]
        public IHttpActionResult GetAopProcedureStatus(int id)
        {
            var result = this.unitOfWork.DbContext.Set<AopProcedureStatus>()
                .Where(e => e.AopProcedureStatusId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.AopProcedureStatusId,
                name = result.Name,
                alias = result.Alias,
                isActive = true
            });
        }

        [Route("aopProcedureStatus")]
        public IHttpActionResult GetAopProcedureStatuses(string term = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<AopProcedureStatus>()
                .AndStringContains(e => e.Name, term);

            var results =
                this.unitOfWork.DbContext.Set<AopProcedureStatus>()
                .Where(predicate)
                .OrderBy(e => e.Name)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    nomValueId = e.AopProcedureStatusId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = true
                })
                .ToList();

            return Ok(results);
        }

        [Route("aopApplicationType/{id:int}")]
        public IHttpActionResult GetAopApplicationType(int id)
        {
            var result = this.unitOfWork.DbContext.Set<AopApplicationType>()
                .Where(e => e.AopApplicationTypeId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.AopApplicationTypeId,
                name = result.Name,
                alias = result.Alias,
                isActive = true
            });
        }

        [Route("aopApplicationType")]
        public IHttpActionResult GetAopApplicationTypes(string term = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<AopApplicationType>()
                .AndStringContains(e => e.Name, term);

            var results =
                this.unitOfWork.DbContext.Set<AopApplicationType>()
                .Where(predicate)
                .OrderBy(e => e.Name)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    nomValueId = e.AopApplicationTypeId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = true
                })
                .ToList();

            return Ok(results);
        }

        [Route("aopEmployerType/{id:int}")]
        public IHttpActionResult GetAopEmployerType(int id)
        {
            var result = this.unitOfWork.DbContext.Set<AopEmployerType>()
                .Where(e => e.AopEmployerTypeId == id)
                .SingleOrDefault();

            return Ok(new
            {
                nomValueId = result.AopEmployerTypeId,
                name = result.Name,
                alias = result.Alias,
                isActive = true
            });
        }

        [Route("aopEmployerType")]
        public IHttpActionResult GetAopEmployerTypes(string term = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<AopEmployerType>()
                .AndStringContains(e => e.Name, term);

            var results =
                this.unitOfWork.DbContext.Set<AopEmployerType>()
                .Where(predicate)
                .OrderBy(e => e.Name)
                .WithOffsetAndLimit(offset, limit)
                .Select(e => new
                {
                    nomValueId = e.AopEmployerTypeId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = true
                })
                .ToList();

            return Ok(results);
        }
    }
}
