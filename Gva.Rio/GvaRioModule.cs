using Autofac;
using Common.Jobs;
using Common.Rio.RioObjectExtraction;
using Gva.Rio.IncomingDocProcessor;
using Gva.Rio.Jobs;

namespace Gva.Rio
{
    public class GvaRioModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            if (bool.Parse(System.Configuration.ConfigurationManager.AppSettings["Gva.Rio:EnableIncomingDocumentsJob"]))
            {
                moduleBuilder.RegisterType<IncomingDocsJob>().As<IJob>().ExternallyOwned();
                moduleBuilder.RegisterType<IncomingDocProcessor.IncomingDocProcessor>().As<IIncomingDocProcessor>();
            }
        }
    }
}
