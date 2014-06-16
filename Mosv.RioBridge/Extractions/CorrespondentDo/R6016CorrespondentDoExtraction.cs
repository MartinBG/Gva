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
    public class R6016CorrespondentDoExtraction : CorrespondentDoExtraction<R_6016.GrantPublicAccessInformation>
    {
        protected override DataObjects.CorrespondentDo GetCorrespondent(R_6016.GrantPublicAccessInformation rioObject)
        {
            return new DataObjects.CorrespondentDo()
            {
                FirstName = rioObject.HeaderNoIdentification.RecipientNoIdentification.PersonNames.First,
                SecondName = rioObject.HeaderNoIdentification.RecipientNoIdentification.PersonNames.Middle,
                LastName = rioObject.HeaderNoIdentification.RecipientNoIdentification.PersonNames.Last,
                Email = rioObject.HeaderNoIdentification.EmailAddress,
                Phone = rioObject.HeaderNoIdentification.ElectronicServiceApplicantPhoneNumber,
                Fax = rioObject.HeaderNoIdentification.ElectronicServiceApplicantFaxNumber
            };
        }
    }
}
