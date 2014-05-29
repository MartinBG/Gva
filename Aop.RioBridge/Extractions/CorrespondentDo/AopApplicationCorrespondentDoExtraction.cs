using Common.Rio.RioObjectExtractor;
using Aop.Portal.RioObjects;
using Aop.RioBridge.DataObjects;
using Abbcdn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aop;
using Aop.RioBridge.Extractions.AttachedDocDo;

namespace Aop.RioBridge.Extractions.AttachedDocDo
{
    public class AopApplicationCorrespondentDoExtraction : CorrespondentDoExtraction<AopApplication>
    {
        protected override DataObjects.CorrespondentDo GetCorrespondent(AopApplication rioObject)
        {
            return new CorrespondentDo()
            {
                FirstName = rioObject.SenderName,
                LastName = rioObject.SenderLastName,
                Position = rioObject.SenderPosition,
                Phone = rioObject.SenderPhone
            };
        }
    }
}
