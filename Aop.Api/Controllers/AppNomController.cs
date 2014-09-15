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
                name = string.Format("{0} ({1}) [{2}]", result.Name, !string.IsNullOrEmpty(result.Uic) ? result.Uic : "няма", !string.IsNullOrEmpty(result.LotNum) ? result.LotNum : "няма"),
                alias = string.Format("{0} {1}", result.Uic, result.LotNum),
                isActive = true
            });
        }

        [Route("aopEmployer")]
        public IHttpActionResult GetAopEmployers(string term = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<AopEmployer>()
                .AndStringContains(e => e.Name + " " + e.Uic, term);

            var results =
                this.unitOfWork.DbContext.Set<AopEmployer>()
                .Where(predicate)
                .OrderBy(e => e.Name)
                .WithOffsetAndLimit(offset, limit)
                .ToList()
                .Select(e => new
                {
                    nomValueId = e.AopEmployerId,
                    name = string.Format("{0} ({1}) [{2}]", e.Name, !string.IsNullOrEmpty(e.Uic) ? e.Uic : "няма", !string.IsNullOrEmpty(e.LotNum) ? e.LotNum : "няма"),
                    alias = string.Format("{0} {1}", e.Uic, e.LotNum),
                    isActive = true
                });

            return Ok(results);
        }
    }
}
