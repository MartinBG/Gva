using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aop.AppCommunicator.AppCommunicatorObjects
{
    public class DocumentRequest
    {
        public string DocumentData { get; set; }

        public string DocumentFileName { get; set; }

        public Guid DocumentGuid { get; set; }
    }
}