using System.Collections.Generic;
using System.Linq;

namespace Rio.Data.Extractions.AttachedDocDo.Gva
{
    public abstract class R3994CollectionAttachedDocDoExtraction<TRioObject> : AttachedDocDoExtraction<TRioObject>
    {
        protected virtual R_3994.AttachedDocumentDatasCollection GetR3994Collection1(TRioObject rioObject)
        {
            return null;
        }

        protected virtual R_3994.AttachedDocumentDatasCollection GetR3994Collection2(TRioObject rioObject)
        {
            return null;
        }

        protected virtual R_4696.EAURecipientsAttachedDocumentDatasCollection GetR4696Collection(TRioObject rioObject)
        {
            return null;
        }

        protected virtual R_4692.UnnumberedAttachedDocumentDatasCollection GetR4692Collection(TRioObject rioObject)
        {
            return null;
        }

        protected virtual List<R_0009_000139.AttachedDocument> GetAttachedDocuments(TRioObject rioObject)
        {
            return null;
        }

        protected override List<KeyValuePair<string, string>> ExtractFileNames(TRioObject rioObject)
        {
            List<KeyValuePair<string, string>> attachedFileNames = new List<KeyValuePair<string, string>>();

            var attachedDocCollection1 = this.GetR3994Collection1(rioObject);
            if (attachedDocCollection1 != null)
            {
                attachedFileNames.AddRange(
                    attachedDocCollection1.AttachedDocumentDataCollection
                    .Where(e => e.AttachedDocumentUniqueIdentifier != null)
                    .Select(e => new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
            }

            var attachedDocCollection2 = this.GetR3994Collection2(rioObject);
            if (attachedDocCollection2 != null)
            {
                attachedFileNames.AddRange(
                    attachedDocCollection2.AttachedDocumentDataCollection
                    .Where(e => e.AttachedDocumentUniqueIdentifier != null)
                    .Select(e => new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
            }

            var eauRecipientsAttachedDocCollection = this.GetR4696Collection(rioObject);
            if (eauRecipientsAttachedDocCollection != null)
            {
                attachedFileNames.AddRange(
                    eauRecipientsAttachedDocCollection.EAURecipientAttachedDocumentDataCollection
                    .Where(e => e.AttachedDocumentUniqueIdentifier != null)
                    .Select(e => new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
            }

            var unnumberedAttachedDocCollection = this.GetR4692Collection(rioObject);
            if (unnumberedAttachedDocCollection != null)
            {
                attachedFileNames.AddRange(
                    unnumberedAttachedDocCollection.UnnumberedAttachedDocumentDataCollection
                    .Where(e => e.AttachedDocumentUniqueIdentifier != null)
                    .Select(e => new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
            }

            return attachedFileNames;
        }

        protected override List<R_0009_000139.AttachedDocument> ExtractAttachedDocuments(TRioObject rioObject)
        {
            return GetAttachedDocuments(rioObject);
        }
    }
}
