using Common.Data;
using Ninject.Modules;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Gva.Api.LotEventHandlers;

namespace Gva.Api
{
    public class GvaApiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbConfiguration>().To<GvaDbConfiguration>();
            Bind<ILotEventHandler>().To<PersonLotEventHandler>();
        }
    }
}
