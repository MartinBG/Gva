using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.UserContext;
using Common.Data;

namespace Common.Api.Controllers
{
    public class UserController : ApiController
    {
        public UserController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private IUnitOfWork unitOfWork;
        public IHttpActionResult GetUserData()
        {
            int userId = this.Request.GetUserContext().UserId;
            var user = this.unitOfWork.DbContext.Set<User>().SingleOrDefault(u => u.UserId == userId);

            return Ok(new { userFullname = user.Fullname });
        }
    }
}
