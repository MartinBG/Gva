using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Rio.Data.ServiceContracts.AppCommunicator
{
    public class DocumentRequest
    {
        public string DocumentData { get; set; }

        public string DocumentFileName { get; set; }

        public Guid DocumentGuid { get; set; }
    }
}
