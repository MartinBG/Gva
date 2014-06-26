using Rio.Data.RioObjectExtraction;

namespace Rio.Data.Extractions.ServiceProviderDo
{
    public abstract class ServiceProviderDoExtraction<TRioObject> : RioObjectExtraction<TRioObject, DataObjects.ServiceProviderDo>
    {
        public override DataObjects.ServiceProviderDo Extract(TRioObject rioObject)
        {
            return this.ExtractServiceInstructions(rioObject);;
        }

        protected virtual DataObjects.ServiceProviderDo ExtractServiceInstructions(TRioObject rioObject)
        {
            return null;
        }
    }
}
