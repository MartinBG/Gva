using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rio.Data.DataObjects
{
    public class AttachedDocDo
    {
        public string DocKind { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string MimeType { get; set; }
        public string UniqueIdentifier { get; set; }
        public byte[] BytesContent { get; set; }
        public bool UseAbbcdn { get; set; }
        public AbbcdnInfo AbbcdnInfo { get; set; }
    }
}
