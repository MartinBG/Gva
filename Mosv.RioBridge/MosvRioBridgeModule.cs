using Autofac;
using Common.Rio.PortalBridge;
using Common.Rio.RioObjectExtraction;
using Mosv.Rio.PortalBridge;
using Mosv.RioBridge.Extractions.AttachedDocDo;
using Mosv.RioBridge.Extractions.CorrespondentDo;
using Mosv.RioBridge.Extractions.ServiceProviderDo;

namespace Mosv.Rio
{
    public class MosvRioBrdigeModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<RioDocumentParser>().As<IRioDocumentParser>();
            //AttachedDocDo Extractions
            moduleBuilder.RegisterType<R6016AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R6054AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R6056AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            //CorrspondentDocDo Extractions
            moduleBuilder.RegisterType<R6016CorrespondentDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R6054CorrespondentDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R6056CorrespondentDoExtraction>().As<IRioObjectExtraction>();
            //ServiceProviderDo Extractions
            moduleBuilder.RegisterType<R6016ServiceProviderDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R6054ServiceProviderDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R6056ServiceProviderDoExtraction>().As<IRioObjectExtraction>();
        }
    }
}
