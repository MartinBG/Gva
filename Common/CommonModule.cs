using Common.Data;
using Common.Models;
using Ninject.Modules;
using Ninject.Extensions.NamedScope;

namespace Common
{
    public class CommonModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().InCallScope();
            Bind<IDbConfiguration>().To<CommonDbConfiguration>();
        }
    }
}
