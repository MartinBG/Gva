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
    public class R4810AttachedDocDoExtraction : R3994CollectionAttachedDocDoExtraction<R_4810.IssuanceMaintenanceCertificateCompetencePerformingTasksApplication>
    {
        protected override R_3994.AttachedDocumentDatasCollection GetR3994Collection1(R_4810.IssuanceMaintenanceCertificateCompetencePerformingTasksApplication rioObject)
        {
            return rioObject.AttachedDocumentDatasCollection;
        }

        protected override R_4696.EAURecipientsAttachedDocumentDatasCollection GetR4696Collection(R_4810.IssuanceMaintenanceCertificateCompetencePerformingTasksApplication rioObject)
        {
            return rioObject.EAURecipientsAttachedDocumentDatasCollection;
        }

        protected override List<R_0009_000139.AttachedDocument> GetAttachedDocuments(R_4810.IssuanceMaintenanceCertificateCompetencePerformingTasksApplication rioObject)
        {
            return rioObject.AttachedDocuments != null ? rioObject.AttachedDocuments.AttachedDocumentCollection : null;
        }
    }
}
