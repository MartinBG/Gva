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
            //AttachedDocDo Extractions
            moduleBuilder.RegisterType<R4186AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4240AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4242AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4244AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4284AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4296AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4356AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4378AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4396AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4470AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4490AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4514AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4544AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4566AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4576AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4578AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4588AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4590AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4598AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4606AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4614AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4686AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4738AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4764AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4766AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4810AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4824AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4834AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4860AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4862AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4864AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4900AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4926AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4958AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5000AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5090AttachedDocDoExtraction>().As<IRioObjectExtraction>();
        }
    }
}
