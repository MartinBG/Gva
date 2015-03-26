using Autofac;
using Mosv.RioBridge.Extractions.AttachedDocDo.Common;
using Mosv.RioBridge.Extractions.AttachedDocDo.Mosv;
using Rio.Data.Extractions.ApplicationDataDo.Aop;
using Rio.Data.Extractions.ApplicationDataDo.Gva;
using Rio.Data.Extractions.AttachedDocDo.Aop;
using Rio.Data.Extractions.AttachedDocDo.Gva;
using Rio.Data.Extractions.CorrespondentDo.Aop;
using Rio.Data.Extractions.CorrespondentDo.Mosv;
using Rio.Data.Extractions.ServiceProviderDo.Common;
using Rio.Data.Extractions.ServiceProviderDo.Mosv;
using Rio.Data.RioObjectExtraction;
using Rio.Data.Utils.RioDocumentParser;

namespace Rio.Data
{
    public class RioDataModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<Rio.Data.RioObjectExtractor.RioObjectExtractor>().As<Rio.Data.RioObjectExtractor.IRioObjectExtractor>();
            moduleBuilder.RegisterType<RioDocumentParser>().As<IRioDocumentParser>();

            //---ApplicationDataDo Extractions-------------------------------------------------------
            //Common
            moduleBuilder.RegisterType<R6064ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            //Gva
            moduleBuilder.RegisterType<R4186ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4240ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4242ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4244ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4284ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4296ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4356ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4378ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4396ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4470ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4490ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4514ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4544ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4566ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4578ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4588ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4590ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4598ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4606ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4614ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4686ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4738ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4764ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4766ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4810ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4824ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4834ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4860ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4862ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4864ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4900ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4926ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R4958ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5000ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5090ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5094ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5096ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5104ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5116ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5132ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5134ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5144ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5160ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5164ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5166ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5168ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5170ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5178ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5196ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5218ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5242ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5244ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5246ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5248ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5250ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            //Mosv
            moduleBuilder.RegisterType<R6016ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R6054ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R6056ApplicationDataDoExtraction>().As<IRioObjectExtraction>();
            //Aop
            moduleBuilder.RegisterType<AopApplicationDataDoExtraction>().As<IRioObjectExtraction>();

            //---AttachedDocDo Extractions-------------------------------------------------------
            //Gva
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
            moduleBuilder.RegisterType<R5094AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5096AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5104AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5116AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5132AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5134AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5144AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5160AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5164AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5166AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5168AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5170AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5178AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5196AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5218AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5242AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5244AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5246AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5248AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R5250AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            //Mosv
            moduleBuilder.RegisterType<R6016AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R6054AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R6056AttachedDocDoExtraction>().As<IRioObjectExtraction>();
            //Aop
            moduleBuilder.RegisterType<AopAttachedDocDoExtraction>().As<IRioObjectExtraction>();

            //---CorrespondentDo Extractions----------------------------------------------------------------
            //Mosv
            moduleBuilder.RegisterType<R6016CorrespondentDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R6054CorrespondentDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R6056CorrespondentDoExtraction>().As<IRioObjectExtraction>();
            //Aop
            moduleBuilder.RegisterType<AopApplicationCorrespondentDoExtraction>().As<IRioObjectExtraction>();

            //---ServiceProviderDo Extractions-------------------------------------------------------
            //Mosv
            moduleBuilder.RegisterType<R6064ServiceProviderDoExtraction>().As<IRioObjectExtraction>();
            //Mosv
            moduleBuilder.RegisterType<R6016ServiceProviderDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R6054ServiceProviderDoExtraction>().As<IRioObjectExtraction>();
            moduleBuilder.RegisterType<R6056ServiceProviderDoExtraction>().As<IRioObjectExtraction>();
        }
    }
}
