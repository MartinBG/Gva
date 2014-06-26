using Autofac;
using Common.Jobs;
using Aop.Rio.IncomingDocProcessor;
using Aop.Rio.Jobs;
using Rio.Data.Utils.RioValidator;

namespace Aop.Rio
{
    public class AopRioModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<RioValidator>().As<IRioValidator>();

            if (bool.Parse(System.Configuration.ConfigurationManager.AppSettings["Aop.Rio:EnableIncomingDocumentsJob"]))
            {
                moduleBuilder.RegisterType<IncomingDocsJob>().As<IJob>().ExternallyOwned();
                moduleBuilder.RegisterType<IncomingDocProcessor.IncomingDocProcessor>().As<IIncomingDocProcessor>();
            }
        }
    }
}
