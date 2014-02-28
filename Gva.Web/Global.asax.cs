using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Gva.Web.App_Start;
using Gva.Web.Mappers;
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

            GlobalConfiguration.Configure((c) => WebApiConfig.Register(kernel, c));
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Mapper.Configuration.ConstructServicesUsing(x => kernel.Get(x));
            foreach (IMapper mapper in kernel.GetAll<IMapper>())
            {
                mapper.CreateMap();
            }
        }
    }
}