
namespace Rio.Data.Extractions.ServiceProviderDo.Mosv
{
    public class R6016ServiceProviderDoExtraction : ServiceProviderDoExtraction<R_6016.GrantPublicAccessInformation>
    {
        protected override DataObjects.ServiceProviderDo ExtractServiceInstructions(R_6016.GrantPublicAccessInformation rioObject)
        {
            return new DataObjects.ServiceProviderDo()
            {
                Id = rioObject.ServiceInstructions,
                Name = rioObject.ServiceInstructionsName
            };
        }
    }
}
