using Common.Data;
using Ninject.Modules;
using Docs.Api.Models;

namespace Docs.Api
{
    public class GvaApiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbConfiguration>().To<DocsDbConfiguration>();
        }
    }
}
