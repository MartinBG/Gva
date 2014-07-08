using System.Linq;
using System.Web.Http;
using Common.Data;
using Common.Linq;
using Docs.Api.Models;
using Common.Api.Models;

namespace Docs.Api.Controllers
{
    public class UnitController : ApiController
    {
        private IUnitOfWork unitOfWork;

        public UnitController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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
    }
}
