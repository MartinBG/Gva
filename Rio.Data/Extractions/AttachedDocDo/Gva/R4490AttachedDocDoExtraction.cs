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
    public class R4490AttachedDocDoExtraction : R3994CollectionAttachedDocDoExtraction<R_4490.AuthorizationUseRadioAircraftApplication>
    {
        protected override R_3994.AttachedDocumentDatasCollection GetR3994Collection1(R_4490.AuthorizationUseRadioAircraftApplication rioObject)
        {
            return rioObject.AttachedDocumentDatasCollection;
        }

        protected override List<R_0009_000139.AttachedDocument> GetAttachedDocuments(R_4490.AuthorizationUseRadioAircraftApplication rioObject)
        {
            return rioObject.AttachedDocuments != null ? rioObject.AttachedDocuments.AttachedDocumentCollection : null;
        }
    }
}
