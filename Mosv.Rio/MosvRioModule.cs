using Autofac;
using Common.Jobs;
using Common.Rio.RioObjectExtraction;
using Mosv.Rio.IncomingDocProcessor;
using Mosv.Rio.Jobs;

namespace Mosv.Rio
{
    public class MosvRioModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            if (bool.Parse(System.Configuration.ConfigurationManager.AppSettings["Mosv.Rio:EnableIncomingDocumentsJob"]))
            {
                moduleBuilder.RegisterType<IncomingDocsJob>().As<IJob>().ExternallyOwned();
                moduleBuilder.RegisterType<IncomingDocProcessor.IncomingDocProcessor>().As<IIncomingDocProcessor>();
            }
        }
    }
}
