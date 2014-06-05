using Common.Rio.RioObjectExtraction;
using Common.Rio.RioObjectExtractor;
using Components.DocumentSerializer;
using RioObjects;
using Gva.RioBridge.DataObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abbcdn;

namespace Gva.RioBridge.Extractions.AttachedDocDo
{
    public abstract class AttachedDocDoExtraction<TRioObject> : RioObjectExtraction<TRioObject, IList<DataObjects.AttachedDocDo>>
    {
        private IDocumentSerializer documentSerializer;

        public AttachedDocDoExtraction()
        {
            this.documentSerializer = new DocumentSerializerImpl();
        }

        protected Abbcdnconfig DeserializeAbbcdn(byte[] xml)
        {
            try
            {
                return this.documentSerializer.XmlDeserializeFromBytes<Abbcdnconfig>(xml);
            }
            catch
            {
                return null;
            }
        }

        protected abstract List<KeyValuePair<string, string>> ExtractFileNames(TRioObject rioObject);

        public override IList<DataObjects.AttachedDocDo> Extract(TRioObject rioObject)
        {
            List<KeyValuePair<string, string>> attachedFileNames = this.ExtractFileNames(rioObject);

            var attachedDocuments = new List<DataObjects.AttachedDocDo>();
            var serviceHeader = (IHeaderFooterDocumentsRioApplication)rioObject;

            if (serviceHeader.AttachedDocuments != null)
            {
                foreach (var document in serviceHeader.AttachedDocuments)
                {
                    DataObjects.AttachedDocDo attachedDoc = new DataObjects.AttachedDocDo();
                    attachedDoc.FileName = document.AttachedDocumentFileName;
                    attachedDoc.Description = document.AttachedDocumentDescription;
                    attachedDoc.MimeType = document.FileType;

                    Abbcdnconfig cdnConfig = DeserializeAbbcdn(document.AttachedDocumentFileContent);

                    if (cdnConfig != null)
                    {
                        attachedDoc.AbbcdnInfo = new AbbcdnInfo
                        {
                            AttachedDocumentFileName = cdnConfig.AttachedDocumentFileName,
                            AttachedDocumentUniqueIdentifier = cdnConfig.AttachedDocumentUniqueIdentifier
                        };
                        attachedDoc.UniqueIdentifier = cdnConfig.AttachedDocumentUniqueIdentifier;
                        attachedDoc.UseAbbcdn = true;
                        attachedDoc.BytesContent = null;
                    }
                    else
                    {
                        attachedDoc.AbbcdnInfo = null;
                        attachedDoc.UniqueIdentifier = null;
                        attachedDoc.UseAbbcdn = false;
                        attachedDoc.BytesContent = document.AttachedDocumentFileContent;
                    }

                    if (attachedFileNames.Any(e => e.Key == document.AttachedDocumentUniqueIdentifier))
                    {
                        attachedDoc.DocKind = attachedFileNames.Single(e => e.Key == document.AttachedDocumentUniqueIdentifier).Value;
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
