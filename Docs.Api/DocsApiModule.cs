using Common.Data;
using Docs.Api.Models;
using Ninject.Modules;

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
