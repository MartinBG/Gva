
namespace Rio.Data.Extractions.CorrespondentDo.Mosv
{
    public class R6054CorrespondentDoExtraction : CorrespondentDoExtraction<R_6054.Proposal>
    {
        protected override DataObjects.CorrespondentDo GetCorrespondent(R_6054.Proposal rioObject)
        {
            return new DataObjects.CorrespondentDo()
            {
                FirstName = rioObject.ProposalNature,
                Email = rioObject.ContactInformation.EmailAddress,
                Phone = rioObject.ContactInformation.PhoneNumbersDesc,
                Fax = rioObject.ContactInformation.FaxNumbersDesc
            };
        }
    }
}
