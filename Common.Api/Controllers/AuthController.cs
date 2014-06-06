using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http;

namespace Common.Api.Controllers
{
    public class AuthController : ApiController
    {
        public void PostSignOut()
        {
            this.Request.GetOwinContext().Authentication.SignOut();
        }
    }
}
