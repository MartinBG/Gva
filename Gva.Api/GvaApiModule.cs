using Common.Data;
using Ninject.Modules;
using Gva.Api.Models;

namespace Gva.Api
{
    public class GvaApiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbConfiguration>().To<GvaDbConfiguration>();
        }
    }
}
