using Autofac;
using Common.Jobs;
using Gva.Rio.IncomingDocProcessor;
using Gva.Rio.Jobs;
using Rio.Data.Utils.RioValidator;

namespace Gva.Rio
{
    public class GvaRioModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<RioValidator>().As<IRioValidator>();

            if (bool.Parse(System.Configuration.ConfigurationManager.AppSettings["Gva.Rio:EnableIncomingDocumentsJob"]))
            {
                moduleBuilder.RegisterType<IncomingDocsJob>().As<IJob>().ExternallyOwned();
                moduleBuilder.RegisterType<IncomingDocProcessor.IncomingDocProcessor>().As<IIncomingDocProcessor>();
            }
        }
    }
}
