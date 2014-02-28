using Common.Data;
using Ninject.Extensions.NamedScope;
using Ninject.Modules;

namespace Common
{
    public class CommonModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().InCallScope();
        }
    }
}
