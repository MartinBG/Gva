
namespace Rio.Data.Extractions.CorrespondentDo.Mosv
{
    public class R6056CorrespondentDoExtraction : CorrespondentDoExtraction<R_6056.Signal>
    {
        protected override DataObjects.CorrespondentDo GetCorrespondent(R_6056.Signal rioObject)
        {
            return new DataObjects.CorrespondentDo()
            {
                FirstName = rioObject.SignalReporterName,
                Email = rioObject.ContactInformation.EmailAddress,
                Phone = rioObject.ContactInformation.PhoneNumbersDesc,
                Fax = rioObject.ContactInformation.FaxNumbersDesc
            };
        }
    }
}
