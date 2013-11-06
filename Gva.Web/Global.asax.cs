using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Gva.Web.App_Start;
using Ninject;

namespace Gva.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            IKernel kernel = new StandardKernel();
            NinjectConfig.RegisterServices(kernel);

            MvcConfig.Register(kernel);

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(kernel, GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}