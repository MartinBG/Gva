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
    public class R4598AttachedDocDoExtraction : R3994CollectionAttachedDocDoExtraction<R_4598.AuthorizationAirportOperatorApplication>
    {
        protected override R_3994.AttachedDocumentDatasCollection GetR3994Collection1(R_4598.AuthorizationAirportOperatorApplication rioObject)
        {
            return rioObject.AttachedDocumentDatasCollection;
        }

        protected override R_3994.AttachedDocumentDatasCollection GetR3994Collection2(R_4598.AuthorizationAirportOperatorApplication rioObject)
        {
            return rioObject.AdditionalAttachedDocumentDatasCollection;
        }

        protected override List<R_0009_000139.AttachedDocument> GetAttachedDocuments(R_4598.AuthorizationAirportOperatorApplication rioObject)
        {
            return rioObject.AttachedDocuments != null ? rioObject.AttachedDocuments.AttachedDocumentCollection : null;
        }
    }
}
