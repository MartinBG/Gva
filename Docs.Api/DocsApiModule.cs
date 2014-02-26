using Common.Data;
using Ninject.Modules;
using Docs.Api.Models;

namespace Docs.Api
{
    public class DocsApiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbConfiguration>().To<DocsDbConfiguration>();
        }
    }
}
