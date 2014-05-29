using Autofac;
using Common.Jobs;
using Common.Rio.RioObjectExtraction;
using Aop.Rio.IncomingDocProcessor;
using Aop.Rio.Jobs;

namespace Aop.Rio
{
    public class AopRioModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            if (bool.Parse(System.Configuration.ConfigurationManager.AppSettings["Aop.Rio:EnableIncomingDocumentsJob"]))
            {
                moduleBuilder.RegisterType<IncomingDocsJob>().As<IJob>().ExternallyOwned();
                moduleBuilder.RegisterType<IncomingDocProcessor.IncomingDocProcessor>().As<IIncomingDocProcessor>();
            }
        }
    }
}
