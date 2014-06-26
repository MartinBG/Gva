using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using R_0009_000072;
using R_0009_000001;
using R_0009_000085;

namespace Rio.Data.ServiceContracts.AppCommunicator
{
    public class CaseStatusInfo
    {
        public Guid DocumentGuid { get; set; }

        public CaseStatus CaseStatus { get; set; }

        public string CaseDocumentUri { get; set; }
    }
}
