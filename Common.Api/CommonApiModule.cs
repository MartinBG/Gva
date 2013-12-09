using Common.Http;
using Ninject.Extensions.NamedScope;
using Ninject.Modules;

namespace Common.Api
{
    public class CommonApiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWebApiConfig>().To<CommonApiWebApiConfig>().InCallScope();
        }
    }
}
