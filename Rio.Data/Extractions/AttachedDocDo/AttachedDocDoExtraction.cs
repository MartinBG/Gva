using Rio.Data.RioObjectExtraction;
using Rio.Objects;
using Rio.Data.DataObjects;
using System.Collections.Generic;
using System.Linq;
using Abbcdn;
using Common.Utils;

namespace Rio.Data.Extractions.AttachedDocDo
{
    public abstract class AttachedDocDoExtraction<TRioObject> : RioObjectExtraction<TRioObject, IList<DataObjects.AttachedDocDo>>
    {
        public AttachedDocDoExtraction()
        {
        }

        protected Abbcdnconfig DeserializeAbbcdn(byte[] xml)
        {
            try
            {
                return XmlSerializerUtils.XmlDeserializeFromBytes<Abbcdnconfig>(xml);
            }
            catch
            {
                return null;
            }
        }

        protected abstract List<KeyValuePair<string, string>> ExtractFileNames(TRioObject rioObject);

        protected abstract List<R_0009_000139.AttachedDocument> ExtractAttachedDocuments(TRioObject rioObject);

        public override IList<DataObjects.AttachedDocDo> Extract(TRioObject rioObject)
        {
            var attachedDocuments = new List<DataObjects.AttachedDocDo>();

            List<KeyValuePair<string, string>> applicationAttachedFileNames = this.ExtractFileNames(rioObject);
            List<R_0009_000139.AttachedDocument> applicationAttachedDocuments = this.ExtractAttachedDocuments(rioObject);

            if (applicationAttachedDocuments != null)
            {
                foreach (var document in applicationAttachedDocuments)
                {
                    DataObjects.AttachedDocDo attachedDoc = new DataObjects.AttachedDocDo();
                    attachedDoc.Description = document.AttachedDocumentDescription;
                    attachedDoc.MimeType = document.FileType;

                    Abbcdnconfig cdnConfig = DeserializeAbbcdn(document.AttachedDocumentFileContent);

                    if (cdnConfig != null)
                    {
                        attachedDoc.AbbcdnInfo = new Rio.Data.DataObjects.AbbcdnInfo
                        {
                            AttachedDocumentFileName = cdnConfig.AttachedDocumentFileName,
                            AttachedDocumentUniqueIdentifier = cdnConfig.AttachedDocumentUniqueIdentifier
                        };
                        attachedDoc.FileName = cdnConfig.AttachedDocumentFileName;
                        attachedDoc.UniqueIdentifier = cdnConfig.AttachedDocumentUniqueIdentifier;
                        attachedDoc.UseAbbcdn = true;
                        attachedDoc.BytesContent = null;
                    }
                    else
                    {
                        attachedDoc.FileName = document.AttachedDocumentFileName;
                        attachedDoc.AbbcdnInfo = null;
                        attachedDoc.UniqueIdentifier = null;
                        attachedDoc.UseAbbcdn = false;
                        attachedDoc.BytesContent = document.AttachedDocumentFileContent;
                    }

                    if (applicationAttachedFileNames != null && applicationAttachedFileNames.Any(e => e.Key == document.AttachedDocumentUniqueIdentifier))
                    {
                        attachedDoc.DocKind = applicationAttachedFileNames.Single(e => e.Key == document.AttachedDocumentUniqueIdentifier).Value;
                    }
                    else
                    {
                        attachedDoc.DocKind = "Прикачен файл";
                    }

                    attachedDocuments.Add(attachedDoc);
                }
            }

            return attachedDocuments;
        }
    }
}
