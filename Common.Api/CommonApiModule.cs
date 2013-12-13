using Common.Api.Models;
using Common.Api.UserContext;
using Common.Data;
using Common.Http;
using Ninject.Extensions.NamedScope;
using Ninject.Modules;

namespace Common.Api
{
    public class CommonApiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbConfiguration>().To<CommonDbConfiguration>();
            Bind<IWebApiConfig>().To<CommonApiWebApiConfig>().InCallScope();
            Bind<IUserContextProvider>().To<UserContextProvider>();
        }
    }
}
