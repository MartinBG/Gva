using Autofac;
using Autofac.Integration.WebApi;
using Common.Data;
using Common.Tests;

namespace Common
{
    public class CommonModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            moduleBuilder.RegisterGeneric(typeof(DisposableTuple<,>)).AsSelf();
            moduleBuilder.RegisterGeneric(typeof(DisposableTuple<,,>)).AsSelf();
            moduleBuilder.RegisterGeneric(typeof(DisposableTuple<,,,>)).AsSelf();
        }
    }
}
