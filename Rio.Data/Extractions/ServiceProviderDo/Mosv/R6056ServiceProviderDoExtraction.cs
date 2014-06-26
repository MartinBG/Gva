
namespace Rio.Data.Extractions.ServiceProviderDo.Mosv
{
    public class R6056ServiceProviderDoExtraction : ServiceProviderDoExtraction<R_6056.Signal>
    {
        protected override DataObjects.ServiceProviderDo ExtractServiceInstructions(R_6056.Signal rioObject)
        {
            return new DataObjects.ServiceProviderDo()
            {
                Id = rioObject.ServiceInstructions,
                Name = rioObject.ServiceInstructionsName
            };
        }
    }
}
