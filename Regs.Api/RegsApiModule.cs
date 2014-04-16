using Autofac;
using Common.Data;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Regs.Api
{
    public class RegsApiModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<RegsDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<LotRepository>().As<ILotRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<LotEventDispatcher>().As<ILotEventDispatcher>().InstancePerLifetimeScope();
        }
    }
}
