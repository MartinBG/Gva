using Autofac;
using Common.Data;
using Mosv.Api.Controllers;
using Mosv.Api.Models;
using Mosv.Api.Projections.AdmissionView;
using Mosv.Api.Projections.SignalView;
using Mosv.Api.Projections.SuggestionView;
using Mosv.Api.Repositories.AdmissionRepository;
using Mosv.Api.Repositories.SignalRepository;
using Mosv.Api.Repositories.SuggestionRepository;
using Regs.Api.LotEvents;

namespace Mosv.Api
{
    public class MosvApiModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<MosvDbConfiguration>().As<IDbConfiguration>().SingleInstance();

            moduleBuilder.RegisterType<AdmissionProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<SignalProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<SuggestionProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();

            moduleBuilder.RegisterType<AdmissionRepository>().As<IAdmissionRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<SignalRepository>().As<ISignalRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<SuggestionRepository>().As<ISuggestionRepository>().InstancePerLifetimeScope();

            //controllers
            moduleBuilder.RegisterType<AdmissionsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<SignalsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<SuggestionsController>().InstancePerLifetimeScope();
        }
    }
}
