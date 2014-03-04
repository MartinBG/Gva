using Common.Data;
using Ninject.Modules;
using Docs.Api.Models;
using Docs.Api.Repositories.CorrespondentRepository;
using Ninject.Extensions.NamedScope;
using Common.Http;
using Common.Api.UserContext;
using Docs.Api.Repositories.DocRepository;

namespace Docs.Api
{
    public class DocsApiModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWebApiConfig>().To<DocsWebApiConfig>();
            Bind<IMvcConfig>().To<DocsMvcConfig>();
            Bind<IDbConfiguration>().To<DocsDbConfiguration>();

            Bind<ICorrespondentRepository>().To<CorrespondentRepository>();
            Bind<IDocRepository>().To<DocRepository>();
        }
    }
}
