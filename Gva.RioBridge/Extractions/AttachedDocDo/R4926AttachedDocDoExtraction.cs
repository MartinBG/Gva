using Common.Rio.RioObjectExtractor;
using RioObjects;
using Gva.RioBridge.DataObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gva.RioBridge.Extractions.AttachedDocDo
{
    public class R4926AttachedDocDoExtraction : R3994CollectionAttachedDocDoExtraction<R_4926.PermitFlyApplication>
    {
        protected override R_4696.EAURecipientsAttachedDocumentDatasCollection GetR4696Collection(R_4926.PermitFlyApplication rioObject)
        {
            return rioObject.EAURecipientsAttachedDocumentDatasCollection;
        }
    }
}
