using Common.Data;
using Ninject.Modules;
using Regs.Api;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;
using Regs.Api.LotEvents.LotEventDispatcherFactory;

namespace Regs.Api
{
    public class RegsApiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbConfiguration>().To<RegsDbConfiguration>();

            Bind<ILotRepository>().To<LotRepository>();

            Bind<ILotEventDispatcherFactory>().To<LotEventDispatcherFactory>();
        }
    }
}
