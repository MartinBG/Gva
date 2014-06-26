using Abbcdn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mosv;
using Mosv.RioBridge.Extractions.AttachedDocDo;
using Rio.Data.Extractions.AttachedDocDo;
using Rio.Data.Extractions.ApplicationDataDo;
using R_0009_000152;
using R_0009_000153;
using R_0009_000003;
using R_0009_000001;
using R_0009_000016;
using R_0009_000137;
using R_0009_000002;
using R_6006;

namespace Mosv.RioBridge.Extractions.AttachedDocDo.Mosv
{
    public class R6016ApplicationDataDoExtraction : ApplicationDataDoExtraction<R_6016.GrantPublicAccessInformation>
    {
        protected virtual string GetDocFileTypeAlias(R_6016.GrantPublicAccessInformation rioObject)
        {
            return "R-6016";
        }

        protected override string ExtractDocFileTypeAlias(R_6016.GrantPublicAccessInformation rioObject)
        {
            return GetDocFileTypeAlias(rioObject);
        }

        protected override DocumentTypeURI ExtractDocumentTypeURI(R_6016.GrantPublicAccessInformation rioObject)
        {
            return rioObject.HeaderNoIdentification.DocumentTypeURI;
        }

        protected override string ExtractDocumentTypeName(R_6016.GrantPublicAccessInformation rioObject)
        {
            return rioObject.HeaderNoIdentification.DocumentTypeName;
        }

        protected override DocumentURI ExtractDocumentURI(R_6016.GrantPublicAccessInformation rioObject)
        {
            return rioObject.HeaderNoIdentification.DocumentURI;
        }

        protected override DateTime? ExtractApplicationSigningTime(R_6016.GrantPublicAccessInformation rioObject)
        {
            return null;
        }

        protected override string ExtractEmailAddress(R_6016.GrantPublicAccessInformation rioObject)
        {
            return rioObject.HeaderNoIdentification.EmailAddress;
        }

        protected override bool ExtractSendConfirmationEmail(R_6016.GrantPublicAccessInformation rioObject)
        {
            return rioObject.HeaderNoIdentification.SendApplicationWithReceiptAcknowledgedMessage;
        }

        protected override ElectronicServiceApplicant ExtractElectronicServiceApplicant(R_6016.GrantPublicAccessInformation rioObject)
        {
            return null;
        }

        protected override ElectronicServiceApplicantContactData ExtractElectronicServiceApplicantContactData(R_6016.GrantPublicAccessInformation rioObject)
        {
            return null;
        }

        protected override ElectronicServiceProviderBasicData ExtractElectronicServiceProviderBasicData(R_6016.GrantPublicAccessInformation rioObject)
        {
            return rioObject.HeaderNoIdentification.ElectronicServiceProviderBasicData;
        }
    }
}
