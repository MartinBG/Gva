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
    public class R4900AttachedDocDoExtraction : R3994CollectionAttachedDocDoExtraction<R_4900.AviationSimulatorApplication>
    {
        protected override R_4696.EAURecipientsAttachedDocumentDatasCollection GetR4696Collection(R_4900.AviationSimulatorApplication rioObject)
        {
            return rioObject.EAURecipientsAttachedDocumentDatasCollection;
        }
    }
}
