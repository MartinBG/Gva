﻿using System.Collections.Generic;
using System.Linq;
using Rio.Data.Extractions.AttachedDocDo;

namespace Mosv.RioBridge.Extractions.AttachedDocDo.Mosv
{
    public class R6054AttachedDocDoExtraction : AttachedDocDoExtraction<R_6054.Proposal>
    {
        protected override List<KeyValuePair<string, string>> ExtractFileNames(R_6054.Proposal rioObject)
        {
            List<KeyValuePair<string, string>> attachedFileNames = new List<KeyValuePair<string, string>>();

            if (rioObject.AttachedDocumentDescriptionIdentifiersCollection != null)
            {
                attachedFileNames.AddRange(
                    rioObject.AttachedDocumentDescriptionIdentifiersCollection.AttachedDocumentDescriptionIdentifierCollection
                    .Where(e => e.AttachedDocumentUniqueIdentifier != null)
                    .Select(e => new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentDescription)));
            }

            return attachedFileNames;
        }

        protected override List<R_0009_000139.AttachedDocument> ExtractAttachedDocuments(R_6054.Proposal rioObject)
        {
            return rioObject.AttachedDocuments != null ? rioObject.AttachedDocuments.AttachedDocumentCollection : null;
        }
    }
}
