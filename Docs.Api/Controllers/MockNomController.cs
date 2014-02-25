using Common.Api.Models;
using Docs.Api.DataObjects;
using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Docs.Api.Controllers
{
    public class MockNomController : ApiController
    {
        private Common.Data.IUnitOfWork unitOfWork;

        public MockNomController(Common.Data.IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public HttpResponseMessage GetCorrespondentGroups(
            string term = null,
            int? id = null,
            string va = null
            )
        {
            var query = this.unitOfWork.DbContext.Set<CorrespondentGroup>().AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                query = query.Where(e => e.Name.ToLower().Contains(term.ToLower()));
            }

            if (!string.IsNullOrEmpty(va))
            {
                query = query.Where(e => e.Alias.ToLower().Contains(va.ToLower()));
            }

            if (id.HasValue)
            {
                var singleValue = query
                    .Where(e => e.CorrespondentGroupId == id.Value)
                    .Select(e => new
                    {
                        nomValueId = e.CorrespondentGroupId,
                        name = e.Name,
                        alias = e.Alias,
                        isActive = e.IsActive
                    })
                    .FirstOrDefault();

                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, singleValue);
            }

            var arrayValue = query
                .Where(e => e.IsActive)
                .Select(e => new
                {
                    nomValueId = e.CorrespondentGroupId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, arrayValue);
        }

        [HttpGet]
        public HttpResponseMessage GetCorrespondentTypes(
            string term = null,
            int? id = null,
            string va = null
            )
        {
            var query = this.unitOfWork.DbContext.Set<CorrespondentType>().AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                query = query.Where(e => e.Name.ToLower().Contains(term.ToLower()));
            }

            if (!string.IsNullOrEmpty(va))
            {
                query = query.Where(e => e.Alias.ToLower().Contains(va.ToLower()));
            }

            if (id.HasValue)
            {
                var singleValue = query
                    .Where(e => e.CorrespondentTypeId == id.Value)
                    .Select(e => new
                    {
                        nomValueId = e.CorrespondentTypeId,
                        name = e.Name,
                        alias = e.Alias,
                        isActive = e.IsActive
                    })
                    .FirstOrDefault();

                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, singleValue);
            }

            var arrayValue = query
                .Where(e => e.IsActive)
                .Select(e => new
                {
                    nomValueId = e.CorrespondentTypeId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, arrayValue);
        }
    }
}
