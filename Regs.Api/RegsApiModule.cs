using Common.Data;
using Ninject.Modules;
using Regs.Api;
using Regs.Api.Models;
using Regs.Api.Managers.LotManager;
using Regs.Api.Managers.LobManager;
using Regs.Api.LotEvents.LotEventDispatcherFactory;

namespace Regs.Api
{
    public class RegsApiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbConfiguration>().To<RegsDbConfiguration>();

            Bind<ILotManager>().To<LotManager>();
            Bind<ILobManager>().To<LobManager>();

            Bind<ILotEventDispatcherFactory>().To<LotEventDispatcherFactory>();
        }
    }
}
