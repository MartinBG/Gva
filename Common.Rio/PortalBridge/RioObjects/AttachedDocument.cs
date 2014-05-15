using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Rio.PortalBridge.RioObjects
{
    public class AttachedDocument
    {
        public byte[] AttachedDocumentFileContent { get; set; }

        public string AttachedDocumentDescription { get; set; }

        public string AttachedDocumentUniqueIdentifier { get; set; }

        public string FileType { get; set; }

        public string AttachedDocumentFileName { get; set; }
    }
}
