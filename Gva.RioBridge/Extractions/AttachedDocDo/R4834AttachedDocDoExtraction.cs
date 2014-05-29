using Common.Rio.RioObjectExtractor;
using Gva.Portal.RioObjects;
using Gva.RioBridge.DataObjects;
using R_Abbcdn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gva.RioBridge.Extractions.AttachedDocDo
{
    public class R4834AttachedDocDoExtraction : R3994CollectionAttachedDocDoExtraction<R_4834.VocationalAviationTrainingCenterApplication>
    {
        protected override R_3994.AttachedDocumentDatasCollection GetR3994Collection1(R_4834.VocationalAviationTrainingCenterApplication rioObject)
        {
            return rioObject.AttachedDocumentDatasCollection;
        }

        protected override R_4696.EAURecipientsAttachedDocumentDatasCollection GetR4696Collection(R_4834.VocationalAviationTrainingCenterApplication rioObject)
        {
            return rioObject.EAURecipientsAttachedDocumentDatasCollection;
        }
    }
}
