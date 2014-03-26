using Common.Api.Models;
using Common.Extensions;
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

        [HttpGet]
        public HttpResponseMessage GetDocFormatTypes(
            string term = null,
            int? id = null,
            string va = null
            )
        {
            var query = this.unitOfWork.DbContext.Set<DocFormatType>().AsQueryable();

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
                    .Where(e => e.DocFormatTypeId == id.Value)
                    .Select(e => new
                    {
                        nomValueId = e.DocFormatTypeId,
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
                    nomValueId = e.DocFormatTypeId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, arrayValue);
        }

        [HttpGet]
        public HttpResponseMessage GetDocCasePartTypes(
            string term = null,
            int? id = null,
            string va = null
            )
        {
            var query = this.unitOfWork.DbContext.Set<DocCasePartType>().AsQueryable();

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
                    .Where(e => e.DocCasePartTypeId == id.Value)
                    .Select(e => new
                    {
                        nomValueId = e.DocCasePartTypeId,
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
                    nomValueId = e.DocCasePartTypeId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, arrayValue);
        }

        [HttpGet]
        public HttpResponseMessage GetDocDirections(
            string term = null,
            int? id = null,
            string va = null
            )
        {
            var query = this.unitOfWork.DbContext.Set<DocDirection>().AsQueryable();

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
                    .Where(e => e.DocDirectionId == id.Value)
                    .Select(e => new
                    {
                        nomValueId = e.DocDirectionId,
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
                    nomValueId = e.DocDirectionId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, arrayValue);
        }

        [HttpGet]
        public HttpResponseMessage GetDocTypeGroups(
            string term = null,
            int? id = null,
            string va = null
            )
        {
            var query = this.unitOfWork.DbContext.Set<DocTypeGroup>().AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                query = query.Where(e => e.Name.ToLower().Contains(term.ToLower()));
            }

            if (!string.IsNullOrEmpty(va))
            {
                query = query.Where(e => e.Name.ToLower().Contains(va.ToLower())); //todo is doctypegroup going to have alias?
            }

            if (id.HasValue)
            {
                var singleValue = query
                    .Where(e => e.DocTypeGroupId == id.Value)
                    .Select(e => new
                    {
                        nomValueId = e.DocTypeGroupId,
                        name = e.Name,
                        alias = e.Name, //todo is doctypegroup going to have alias?
                        isActive = e.IsActive
                    })
                    .FirstOrDefault();

                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, singleValue);
            }

            var arrayValue = query
                .Where(e => e.IsActive)
                .Select(e => new
                {
                    nomValueId = e.DocTypeGroupId,
                    name = e.Name,
                    alias = e.Name, //todo is doctypegroup going to have alias?
                    isActive = e.IsActive
                })
                .ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, arrayValue);
        }

        [HttpGet]
        public HttpResponseMessage GetDocTypes(
            string term = null,
            int? id = null,
            string va = null,
            int? parentValueId = null
            )
        {
            var query = this.unitOfWork.DbContext.Set<DocType>().AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                query = query.Where(e => e.Name.ToLower().Contains(term.ToLower()));
            }

            if (!string.IsNullOrEmpty(va))
            {
                query = query.Where(e => e.Alias.ToLower().Contains(va.ToLower()));
            }

            if (parentValueId.HasValue)
            {
                query = query.Where(e => e.DocTypeGroupId == parentValueId.Value);
            }

            if (id.HasValue)
            {
                var singleValue = query
                    .Where(e => e.DocTypeId == id.Value)
                    .Select(e => new
                    {
                        nomValueId = e.DocTypeId,
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
                    nomValueId = e.DocTypeId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, arrayValue);
        }

        [HttpGet]
        public HttpResponseMessage GetUnits(
            string term = null,
            int? id = null,
            string ids = null,
            string va = null
            )
        {
            var query = this.unitOfWork.DbContext.Set<Unit>().AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                query = query.Where(e => e.Name.ToLower().Contains(term.ToLower()));
            }

            if (!string.IsNullOrEmpty(va))
            {
                query = query.Where(e => e.Name.ToLower().Contains(va.ToLower()));
            }

            if (id.HasValue)
            {
                var singleValue = query
                    .Where(e => e.UnitId == id.Value)
                    .Select(e => new
                    {
                        nomValueId = e.UnitId,
                        name = e.Name,
                        alias = e.Name,
                        isActive = e.IsActive
                    })
                    .FirstOrDefault();

                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, singleValue);
            }

            if (!string.IsNullOrEmpty(ids))
            {
                List<int> unitIds = Helper.GetIdListFromString(ids);
                var selectedValue = query
                    .Where(e => unitIds.Contains(e.UnitId))
                    .Select(e => new
                    {
                        nomValueId = e.UnitId,
                        name = e.Name,
                        alias = e.Name,
                        isActive = e.IsActive
                    })
                    .ToList();

                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, selectedValue);
            }

            var arrayValue = query
                .Where(e => e.IsActive)
                .Select(e => new
                {
                    nomValueId = e.UnitId,
                    name = e.Name,
                    alias = e.Name,
                    isActive = e.IsActive
                })
                .ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, arrayValue);
        }

        [HttpGet]
        public HttpResponseMessage SearchUnits(string name = null)
        {
            var query = this.unitOfWork.DbContext.Set<Unit>().AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.ToLower().Contains(name.ToLower()));
            }

            var arrayValue = query
                .Where(e => e.IsActive)
                .Select(e => new
                {
                    nomValueId = e.UnitId,
                    name = e.Name,
                    alias = e.Name,
                    isActive = e.IsActive
                })
                .ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, arrayValue);
        }

        [HttpGet]
        public HttpResponseMessage GetEmployeeUnits(
            string term = null,
            int? id = null,
            string va = null
            )
        {
            UnitType employee = this.unitOfWork.DbContext.Set<UnitType>().SingleOrDefault(e => e.Alias == "Employee");

            var query = this.unitOfWork.DbContext.Set<Unit>().AsQueryable()
                .Where(e => e.UnitTypeId == employee.UnitTypeId);

            if (!string.IsNullOrEmpty(term))
            {
                query = query.Where(e => e.Name.ToLower().Contains(term.ToLower()));
            }

            if (!string.IsNullOrEmpty(va))
            {
                query = query.Where(e => e.Name.ToLower().Contains(va.ToLower()));
            }

            if (id.HasValue)
            {
                var singleValue = query
                    .Where(e => e.UnitId == id.Value)
                    .Select(e => new
                    {
                        nomValueId = e.UnitId,
                        name = e.Name,
                        alias = e.Name,
                        isActive = e.IsActive
                    })
                    .FirstOrDefault();

                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, singleValue);
            }

            var arrayValue = query
                .Where(e => e.IsActive)
                .Select(e => new
                {
                    nomValueId = e.UnitId,
                    name = e.Name,
                    alias = e.Name,
                    isActive = e.IsActive
                })
                .ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, arrayValue);
        }

        [HttpGet]
        public HttpResponseMessage GetCorrespondents(
            string term = null,
            int? id = null,
            string ids = null,
            string va = null
            )
        {
            var query = this.unitOfWork.DbContext.Set<Correspondent>().AsQueryable();

            if (!string.IsNullOrEmpty(term))
            {
                query = query.Where(e => e.DisplayName.ToLower().Contains(term.ToLower()));
            }

            if (!string.IsNullOrEmpty(va))
            {
                query = query.Where(e => e.Alias.ToLower().Contains(va.ToLower()));
            }

            if (id.HasValue)
            {
                var singleValue = query
                    .Where(e => e.CorrespondentId == id.Value)
                    .Select(e => new
                    {
                        nomValueId = e.CorrespondentId,
                        name = e.DisplayName,
                        alias = e.Alias,
                        isActive = e.IsActive
                    })
                    .FirstOrDefault();

                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, singleValue);
            }

            if (!string.IsNullOrEmpty(ids))
            {
                List<int> corrIds = Helper.GetIdListFromString(ids);
                var selectedValue = query
                    .Where(e => corrIds.Contains(e.CorrespondentId))
                    .Select(e => new
                    {
                        nomValueId = e.CorrespondentId,
                        name = e.DisplayName,
                        alias = e.Alias,
                        isActive = e.IsActive
                    })
                    .ToList();

                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, selectedValue);
            }

            var arrayValue = query
                .Where(e => e.IsActive)
                .Select(e => new
                {
                    nomValueId = e.CorrespondentId,
                    name = e.DisplayName,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, arrayValue);
        }

        [HttpGet]
        public HttpResponseMessage GetDocSourceTypes(
            string term = null,
            int? id = null,
            string va = null
            )
        {
            var query = this.unitOfWork.DbContext.Set<DocSourceType>().AsQueryable();

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
                    .Where(e => e.DocSourceTypeId == id.Value)
                    .Select(e => new
                    {
                        nomValueId = e.DocSourceTypeId,
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
                    nomValueId = e.DocSourceTypeId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, arrayValue);
        }

        [HttpGet]
        public HttpResponseMessage GetAssignmentTypes(
            string term = null,
            int? id = null,
            string va = null
            )
        {
            var query = this.unitOfWork.DbContext.Set<AssignmentType>().AsQueryable();

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
                    .Where(e => e.AssignmentTypeId == id.Value)
                    .Select(e => new
                    {
                        nomValueId = e.AssignmentTypeId,
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
                    nomValueId = e.AssignmentTypeId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, arrayValue);
        }

        [HttpGet]
        public HttpResponseMessage GetDocFileKinds(
            string term = null,
            int? id = null,
            string va = null
            )
        {
            var query = this.unitOfWork.DbContext.Set<DocFileKind>().AsQueryable();

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
                    .Where(e => e.DocFileKindId == id.Value)
                    .Select(e => new
                    {
                        nomValueId = e.DocFileKindId,
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
                    nomValueId = e.DocFileKindId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, arrayValue);
        }

        [HttpGet]
        public HttpResponseMessage GetDocFileTypes(
            string term = null,
            int? id = null,
            string va = null
            )
        {
            var query = this.unitOfWork.DbContext.Set<DocFileType>().AsQueryable();

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
                    .Where(e => e.DocFileTypeId == id.Value)
                    .Select(e => new
                    {
                        nomValueId = e.DocFileTypeId,
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
                    nomValueId = e.DocFileTypeId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive
                })
                .ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, arrayValue);
        }

        [HttpGet]
        public HttpResponseMessage GetYesNo(
            string term = null,
            int? id = null,
            string va = null
            )
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new[] {
                new 
                { 
                    nomValueId = 0,
                    name = "Не",
                    alias = "No",
                    isActive = true
                },
                new 
                { 
                    nomValueId = 1,
                    name = "Да",
                    alias = "Yes",
                    isActive = true
                }
            });
        }
    }
}
