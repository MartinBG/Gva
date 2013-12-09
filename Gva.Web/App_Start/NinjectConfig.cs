using Common;
using Ninject;
using Regs.Api;
using System.Web;
using Common.Api;

namespace Gva.Web.App_Start
{
    public class NinjectConfig
    {
        public static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<HttpContextBase>().ToMethod(ctx => new HttpContextWrapper(HttpContext.Current)).InTransientScope();

            kernel.Load(new CommonModule());
            kernel.Load(new CommonApiModule());
            kernel.Load(new RegsApiModule());
        }
    }
}