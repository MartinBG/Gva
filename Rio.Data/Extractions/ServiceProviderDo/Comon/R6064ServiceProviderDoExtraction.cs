
namespace Rio.Data.Extractions.ServiceProviderDo.Common
{
    public class R6064ServiceProviderDoExtraction : ServiceProviderDoExtraction<R_6064.ContainerTransferFileCompetence>
    {
        protected override DataObjects.ServiceProviderDo ExtractServiceInstructions(R_6064.ContainerTransferFileCompetence rioObject)
        {
            return new DataObjects.ServiceProviderDo()
            {
                //Id = rioObject.ReceiverProvider.ElectronicServiceProviderType,
                Id = string.Empty,
                Name = rioObject.ReceiverProvider.EntityBasicData.Name
            };
        }
    }
}
