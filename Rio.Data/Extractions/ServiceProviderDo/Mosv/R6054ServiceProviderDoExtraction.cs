
namespace Rio.Data.Extractions.ServiceProviderDo.Mosv
{
    public class R6054ServiceProviderDoExtraction : ServiceProviderDoExtraction<R_6054.Proposal>
    {
        protected override DataObjects.ServiceProviderDo ExtractServiceInstructions(R_6054.Proposal rioObject)
        {
            return new DataObjects.ServiceProviderDo()
            {
                Id = rioObject.ServiceInstructions,
                Name = rioObject.ServiceInstructionsName
            };
        }
    }
}
