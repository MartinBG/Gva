using R_0009_000001;
using R_0009_000002;
using R_0009_000003;
using R_0009_000016;
using R_0009_000137;
using System;

namespace Rio.Data.DataObjects
{
    public class ApplicationDataDo
    {
        public string DocFileTypeAlias { get; set; }
        public DocumentTypeURI DocumentTypeURI { get; set; }
        public string DocumentTypeName { get; set; }
        public DocumentURI DocumentURI { get; set; }
        public DateTime? ApplicationSigningTime { get; set; }
        public string Email { get; set; }        
        public bool SendConfirmationEmail { get; set; }
        public ElectronicServiceApplicant ElectronicServiceApplicant { get; set; }
        public ElectronicServiceApplicantContactData ElectronicServiceApplicantContactData { get; set; }
        public ElectronicServiceProviderBasicData ElectronicServiceProviderBasicData { get; set; }
    }
}
