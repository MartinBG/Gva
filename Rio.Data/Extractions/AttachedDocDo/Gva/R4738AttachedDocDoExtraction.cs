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
    public class R4738AttachedDocDoExtraction : R3994CollectionAttachedDocDoExtraction<R_4738.AirNavigationServiceProviderApplication>
    {
        protected override R_3994.AttachedDocumentDatasCollection GetR3994Collection1(R_4738.AirNavigationServiceProviderApplication rioObject)
        {
            return rioObject.AttachedDocumentDatasCollection;
        }

        protected override R_4696.EAURecipientsAttachedDocumentDatasCollection GetR4696Collection(R_4738.AirNavigationServiceProviderApplication rioObject)
        {
            return rioObject.EAURecipientsAttachedDocumentDatasCollection;
        }

        protected override R_4692.UnnumberedAttachedDocumentDatasCollection GetR4692Collection(R_4738.AirNavigationServiceProviderApplication rioObject)
        {
            return rioObject.UnnumberedAttachedDocumentDatasCollection;
        }

        protected override List<R_0009_000139.AttachedDocument> GetAttachedDocuments(R_4738.AirNavigationServiceProviderApplication rioObject)
        {
            return rioObject.AttachedDocuments != null ? rioObject.AttachedDocuments.AttachedDocumentCollection : null;
        }
    }
}
