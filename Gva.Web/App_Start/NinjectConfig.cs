using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;

namespace Gva.Web.App_Start
{
    public class NinjectConfig
    {
        public static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<HttpContextBase>().ToMethod(ctx => new HttpContextWrapper(HttpContext.Current)).InTransientScope();
        }
    }
}