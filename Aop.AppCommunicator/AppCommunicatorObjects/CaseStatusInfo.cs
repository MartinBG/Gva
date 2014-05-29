using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using R_0009_000001;

namespace Aop.AppCommunicator.AppCommunicatorObjects
{
    public class CaseStatusInfo
    {
        public Guid DocumentGuid { get; set; }

        public CaseStatus CaseStatus { get; set; }

        public DocumentURI CaseDocumentUri { get; set; }
    }
}