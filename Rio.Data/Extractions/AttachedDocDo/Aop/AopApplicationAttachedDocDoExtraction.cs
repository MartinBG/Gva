using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aop;

namespace Rio.Data.Extractions.AttachedDocDo.Aop
{
    public class AopAttachedDocDoExtraction : AttachedDocDoExtraction<AopApplication>
    {
        protected override List<KeyValuePair<string, string>> ExtractFileNames(AopApplication rioObject)
        {
            List<KeyValuePair<string, string>> attachedFileNames = new List<KeyValuePair<string, string>>();

            if (rioObject.AopAttachedDocumentDatasCollection != null)
            {
                attachedFileNames.AddRange(
                    rioObject.AopAttachedDocumentDatasCollection.AopAttachedDocumentDataCollection
                    .Where(e => e.AttachedDocumentUniqueIdentifier != null)
                    .Select(e => new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
            }

            return attachedFileNames;
        }

        protected override List<R_0009_000139.AttachedDocument> ExtractAttachedDocuments(AopApplication rioObject)
        {
            return rioObject.AttachedDocuments != null ? rioObject.AttachedDocuments.AttachedDocumentCollection : null;
        }
    }
}
