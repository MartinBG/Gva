using Common.Api.Models;
using Common.Extensions;
using Aop.Api.DataObjects;
using Aop.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.Entity;
using Common.Api.UserContext;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Configuration;

namespace Aop.Api.Controllers
{
    [RoutePrefix("api/aop/emps")]
    [Authorize]
    public class AppEmpController : ApiController
    {
        private Common.Data.IUnitOfWork unitOfWork;
        private Aop.Api.Repositories.Aop.IAppRepository appRepository;
        private UserContext userContext;

        public AppEmpController(Common.Data.IUnitOfWork unitOfWork,
            Aop.Api.Repositories.Aop.IAppRepository appRepository)
        {
            this.unitOfWork = unitOfWork;
            this.appRepository = appRepository;
        }

        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            try
            {
                this.userContext = this.Request.GetUserContext();
            }
            catch { }
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateAopEmployer(AopEmployerDO data)
        {
            AopEmployer emp = new AopEmployer();

            emp.Name = data.Name;
            emp.LotNum = data.LotNum;
            emp.AopEmployerTypeId = data.AopEmployerTypeId;

            this.unitOfWork.DbContext.Set<AopEmployer>().Add(emp);

            this.unitOfWork.Save();

            return Ok(new
            {
                aopEmployerId = emp.AopEmployerId
            });
        }
    }
}
