using Gva.Web.Mappers;
using Ninject.Modules;

namespace Gva.Web
{
    public class GvaWebModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMapper>().To<JObjectMapper>();
            Bind<IMapper>().To<PartVersionMapper>();
            Bind<IMapper>().To<PersonMapper>();
        }
    }
}