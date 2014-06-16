using Autofac;
using Common.Rio.PortalBridge;
using Common.Rio.RioObjectExtraction;
using Aop.Rio.PortalBridge;
using Aop.RioBridge.Extractions.AttachedDocDo;
using Aop.RioBridge.Extractions.CorrespondentDo;

namespace Aop.Rio
{
    public class AopRioBrdigeModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<RioDocumentParser>().As<IRioDocumentParser>();
            //AttachedDocDo Extractions
            moduleBuilder.RegisterType<AopApplicationAttachedDocDoExtraction>().As<IRioObjectExtraction>();
            //CorrespondentDo Extractions
            moduleBuilder.RegisterType<AopApplicationCorrespondentDoExtraction>().As<IRioObjectExtraction>();
        }
    }
}
