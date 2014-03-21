using System.Web;
using Common;
using Common.Api;
using Docs.Api;
using Gva.Api;
using Ninject;
using Regs.Api;

namespace Gva.Web.App_Start
{
    public class NinjectConfig
    {
        public static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<HttpContextBase>().ToMethod(ctx => new HttpContextWrapper(HttpContext.Current)).InTransientScope();

            kernel.Load(new DocsApiModule());
            kernel.Load(new GvaApiModule());
            kernel.Load(new RegsApiModule());
            kernel.Load(new CommonApiModule());
            kernel.Load(new CommonModule());
        }
    }
}