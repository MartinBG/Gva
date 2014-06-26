using Autofac;
using Common.Jobs;
using Mosv.Rio.IncomingDocProcessor;
using Mosv.Rio.Jobs;
using Rio.Data.Utils.RioValidator;

namespace Mosv.Rio
{
    public class MosvRioModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<RioValidator>().As<IRioValidator>();

            if (bool.Parse(System.Configuration.ConfigurationManager.AppSettings["Mosv.Rio:EnableIncomingDocumentsJob"]))
            {
                moduleBuilder.RegisterType<IncomingDocsJob>().As<IJob>().ExternallyOwned();
                moduleBuilder.RegisterType<IncomingDocProcessor.IncomingDocProcessor>().As<IIncomingDocProcessor>();
            }
        }
    }
}
