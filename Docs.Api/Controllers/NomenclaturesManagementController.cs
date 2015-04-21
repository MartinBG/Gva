using System.Linq;
using System.Web.Http;
using Common.Data;
using Docs.Api.Models;

namespace Docs.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/nomenclaturesManagement")]
    public class NomenclaturesManagementController : ApiController
    {
        private IUnitOfWork unitOfWork;

        public NomenclaturesManagementController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #region docTypeGroup
        [Route("docTypeGroups")]
        public IHttpActionResult GetDocTypeGroups()
        {
            var results =
                this.unitOfWork.DbContext.Set<DocTypeGroup>()
                .OrderBy(e => e.DocTypeGroupId)
                .Select(e => new
                {
                    id = e.DocTypeGroupId,
                    name = e.Name,
                    isActive = e.IsActive
                })
                .ToList();

            return Ok(results);
        }

        [Route("docTypeGroups")]
        [HttpPost]
        public IHttpActionResult PostDocTypeGroup(DocTypeGroup model)
        {
            // Electronic Services can't be created from the UI.
            model.IsElectronicService = false;

            unitOfWork.DbContext.Set<DocTypeGroup>()
                .Add(model);

            unitOfWork.Save();

            return Ok();
        }

        [Route("docTypeGroups/{id:int}")]
        [HttpPost]
        public IHttpActionResult PostDocTypeGroup(int id, DocTypeGroup model)
        {
            var context = unitOfWork.DbContext.Set<DocTypeGroup>();
            var entitiy = context.Single(e =>
                e.DocTypeGroupId == id);

            entitiy.Name = model.Name;
            entitiy.IsActive = model.IsActive;

            unitOfWork.Save();

            return Ok();
        }
        #endregion

        #region docType
        [Route("docTypes")]
        public IHttpActionResult GetDocTypes()
        {
            var results =
                this.unitOfWork.DbContext.Set<DocType>()
                .OrderBy(e => e.DocTypeId)
                .Select(e => new
                {
                    id = e.DocTypeId,
                    name = e.Name,
                    alias = e.Alias,
                    isActive = e.IsActive,
                    executionDeadline = e.ExecutionDeadline,
                    removeIrregularitiesDeadline = e.RemoveIrregularitiesDeadline,
                    primaryRegisterIndexId = e.PrimaryRegisterIndexId,
                    docTypeGroupId = e.DocTypeGroupId
                })
                .ToList();

            return Ok(results);
        }

        [Route("docTypes")]
        [HttpPost]
        public IHttpActionResult PostDocType(DocType model)
        {
            // Electronic Services can't be created from the UI.
            model.IsElectronicService = false;
            model.Alias = string.Empty;

            unitOfWork.DbContext.Set<DocType>()
                .Add(model);

            unitOfWork.Save();

            return Ok();
        }

        [Route("docTypes/{id:int}")]
        [HttpPost]
        public IHttpActionResult PostDocTyp(int id, DocType model)
        {
            var context = unitOfWork.DbContext.Set<DocType>();
            var entitiy = context.Single(e =>
                e.DocTypeId == id);

            entitiy.Name = model.Name;
            entitiy.IsActive = model.IsActive;
            entitiy.PrimaryRegisterIndexId = model.PrimaryRegisterIndexId;
            entitiy.DocTypeGroupId = model.DocTypeGroupId;
            entitiy.ExecutionDeadline = model.ExecutionDeadline;
            entitiy.RemoveIrregularitiesDeadline = model.RemoveIrregularitiesDeadline;

            unitOfWork.Save();

            return Ok();
        }
        #endregion
    }
}
