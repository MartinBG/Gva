using Common.Data;
using Ninject.Modules;
using Regs.Api;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Regs.Api
{
    public class RegsApiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbConfiguration>().To<RegsDbConfiguration>();

            Bind<ILotRepository>().To<LotRepository>();
        }
    }
}
