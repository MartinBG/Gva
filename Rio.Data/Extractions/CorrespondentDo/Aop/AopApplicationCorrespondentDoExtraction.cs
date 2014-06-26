using Aop;

namespace Rio.Data.Extractions.CorrespondentDo.Aop
{
    public class AopApplicationCorrespondentDoExtraction : CorrespondentDoExtraction<AopApplication>
    {
        protected override DataObjects.CorrespondentDo GetCorrespondent(AopApplication rioObject)
        {
            return new DataObjects.CorrespondentDo()
            {
                FirstName = rioObject.SenderName,
                LastName = rioObject.SenderLastName,
                Position = rioObject.SenderPosition,
                Phone = rioObject.SenderPhone
            };
        }
    }
}
