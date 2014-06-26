using Rio.Data.RioObjectExtractor;
using Rio.Objects;
using Rio.Data.DataObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rio.Data.Extractions.AttachedDocDo.Gva
{
    public class R4900AttachedDocDoExtraction : R3994CollectionAttachedDocDoExtraction<R_4900.AviationSimulatorApplication>
    {
        protected override R_4696.EAURecipientsAttachedDocumentDatasCollection GetR4696Collection(R_4900.AviationSimulatorApplication rioObject)
        {
            return rioObject.EAURecipientsAttachedDocumentDatasCollection;
        }

        protected override List<R_0009_000139.AttachedDocument> GetAttachedDocuments(R_4900.AviationSimulatorApplication rioObject)
        {
            return rioObject.AttachedDocuments != null ? rioObject.AttachedDocuments.AttachedDocumentCollection : null;
        }
    }
}
