using Autofac;
using Common;
using Common.Api;
using Common.Owin;
using Docs.Api;
using Owin;
using Rio.Data;

namespace Ems.Web.Host
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = CreateAutofacContainer();

            App.Configure(app, container);
        }

        public static IContainer CreateAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new CommonModule());
            builder.RegisterModule(new CommonApiModule());
            builder.RegisterModule(new DocsApiModule());
            builder.RegisterModule(new RioDataModule());

            return builder.Build();
        }
    }
}
