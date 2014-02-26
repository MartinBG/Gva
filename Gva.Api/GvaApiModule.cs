using Common.Data;
using Gva.Api.LotEventHandlers;
using Gva.Api.Models;
using Gva.Api.Repositories;
using Ninject.Modules;
using Regs.Api.LotEvents;

namespace Gva.Api
{
    public class GvaApiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbConfiguration>().To<GvaDbConfiguration>();
            Bind<ILotEventHandler>().To<PersonLotEventHandler>();
            Bind<IPersonRepository>().To<PersonRepository>();
        }
    }
}
