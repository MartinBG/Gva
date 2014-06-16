using Common.Rio.RioObjectExtractor;
using RioObjects;
using Mosv.RioBridge.DataObjects;
using Abbcdn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mosv;
using Mosv.RioBridge.Extractions.AttachedDocDo;

namespace Mosv.RioBridge.Extractions.CorrespondentDo
{
    public class R6054CorrespondentDoExtraction : CorrespondentDoExtraction<R_6054.Proposal>
    {
        protected override DataObjects.CorrespondentDo GetCorrespondent(R_6054.Proposal rioObject)
        {
            return new DataObjects.CorrespondentDo()
            {
                FirstName = rioObject.ProposalNature,
                SecondName = null,
                LastName = null,
                Email = rioObject.ContactInformation.EmailAddress,
                Phone = rioObject.ContactInformation.PhoneNumbersDesc,
                Fax = rioObject.ContactInformation.FaxNumbersDesc
            };
        }
    }
}
