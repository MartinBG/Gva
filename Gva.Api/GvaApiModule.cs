using Common.Data;
using Gva.Api.LotEventHandlers;
using Gva.Api.Mappers;
using Gva.Api.Models;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.InventoryRepository;
using Gva.Api.Repositories.PersonRepository;
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
            Bind<ILotEventHandler>().To<InventoryLotEventHandler>();

            Bind<IPersonRepository>().To<PersonRepository>();
            Bind<IInventoryRepository>().To<InventoryRepository>();
            Bind<IFileRepository>().To<FileRepository>();

            Bind<IMapper>().To<JObjectMapper>();
            Bind<IMapper>().To<PartVersionMapper>();
            Bind<IMapper>().To<PersonMapper>();
            Bind<IMapper>().To<RatingPartVersionMapper>();
            Bind<IMapper>().To<InventoryItemMapper>();
        }
    }
}
