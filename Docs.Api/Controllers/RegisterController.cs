using System.Linq;
using System.Web.Http;
using Common.Data;
using Docs.Api.Models;

namespace Docs.Api.Controllers
{
    public class RegisterController : ApiController
    {
        private IUnitOfWork unitOfWork;

        public RegisterController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [Route("api/register")]
        public IHttpActionResult GetRegisters()
        {
            var entities = this.unitOfWork.DbContext.Set<RegisterIndex>()
                .Where(e => e.IsActive)
                .Select(e => new
                {
                    id = e.RegisterIndexId,
                    name = e.Name
                })
                .ToList();

            return Ok(entities);
        }
    }
}
