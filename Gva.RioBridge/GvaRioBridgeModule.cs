using Autofac;
using Common.Rio.PortalBridge;
using Common.Rio.RioObjectExtraction;
using Gva.Rio.PortalBridge;
using Gva.RioBridge.Extractions.AttachedDocDo;

namespace Gva.Rio
{
    public class GvaRioBrdigeModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<RioDocumentParser>().As<IRioDocumentParser>();
            moduleBuilder.RegisterType<R4186AttachedDocDoExtraction>().As<IRioObjectExtraction>();
        }
    }
}
