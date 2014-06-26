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
    public class R5090AttachedDocDoExtraction : R3994CollectionAttachedDocDoExtraction<R_5090.EngagedCommercialTransportationPassengersCargoApplication>
    {
        protected override R_3994.AttachedDocumentDatasCollection GetR3994Collection1(R_5090.EngagedCommercialTransportationPassengersCargoApplication rioObject)
        {
            return rioObject.AttachedDocumentDatasCollection;
        }

        protected override R_4696.EAURecipientsAttachedDocumentDatasCollection GetR4696Collection(R_5090.EngagedCommercialTransportationPassengersCargoApplication rioObject)
        {
            return rioObject.EAURecipientsAttachedDocumentDatasCollection;
        }

        protected override R_4692.UnnumberedAttachedDocumentDatasCollection GetR4692Collection(R_5090.EngagedCommercialTransportationPassengersCargoApplication rioObject)
        {
            return rioObject.UnnumberedAttachedDocumentDatasCollection;
        }

        protected override List<R_0009_000139.AttachedDocument> GetAttachedDocuments(R_5090.EngagedCommercialTransportationPassengersCargoApplication rioObject)
        {
            return rioObject.AttachedDocuments != null ? rioObject.AttachedDocuments.AttachedDocumentCollection : null;
        }
    }
}
