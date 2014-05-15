using Autofac;
using Gva.Rio.Jobs;
using Common.Api.Jobs;
using Common.Rio.RioObjectExtraction;
using Gva.Rio.IncomingDocProcessor;

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
