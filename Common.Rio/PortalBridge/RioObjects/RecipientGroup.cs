using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Rio.PortalBridge.RioObjects
{
    public class RecipientGroup
    {
        public ElectronicStatementAuthorCollection AuthorCollection { get; set; }

        public string AuthorQuality { get; set; }

        public ElectronicServiceRecipientCollection RecipientCollection { get; set; }
    }
}
