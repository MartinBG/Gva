using Autofac;
using Common;
using Common.Api;
using Common.Owin;
using Docs.Api;
using Gva.Api;
using Gva.Rio;
using Owin;
using Regs.Api;
using Rio.Data;

namespace Gva.Web.Host
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
            builder.RegisterModule(new GvaApiModule());
            builder.RegisterModule(new RegsApiModule());
            builder.RegisterModule(new RioDataModule());
            builder.RegisterModule(new GvaRioModule());
            return builder.Build();
        }
    }
}