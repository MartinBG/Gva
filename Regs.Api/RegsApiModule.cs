using Common.Data;
using Ninject.Modules;
using Regs.Api;
using Regs.Api.Models;

namespace Regs.Api
{
    public class RegsApiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbConfiguration>().To<RegsDbConfiguration>();
            Bind<LotManager.ILotManager>().To<LotManager.LotManager>();
        }
    }
}
