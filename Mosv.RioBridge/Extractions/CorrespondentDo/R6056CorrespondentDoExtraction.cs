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
    public class R6056CorrespondentDoExtraction : CorrespondentDoExtraction<R_6056.Signal>
    {
        protected override DataObjects.CorrespondentDo GetCorrespondent(R_6056.Signal rioObject)
        {
            return new DataObjects.CorrespondentDo()
            {
                FirstName = rioObject.SignalReporterName,
                SecondName = null,
                LastName = null,
                Email = rioObject.ContactInformation.EmailAddress,
                Phone = rioObject.ContactInformation.PhoneNumbersDesc,
                Fax = rioObject.ContactInformation.FaxNumbersDesc
            };
        }
    }
}
