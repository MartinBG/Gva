using System.Collections.Generic;
using System.Linq;
using Rio.Data.Extractions.AttachedDocDo;
using Rio.Data.Extractions.ApplicationDataDo;
using R_0009_000152;
using R_0009_000153;
using R_6006;
using R_0009_000003;
using R_0009_000001;
using System;
using R_0009_000016;
using R_0009_000137;
using R_0009_000002;

namespace Mosv.RioBridge.Extractions.AttachedDocDo.Mosv
{
    public class R6054ApplicationDataDoExtraction : ApplicationDataDoExtraction<R_6054.Proposal>
    {
        protected virtual string GetDocFileTypeAlias(R_6054.Proposal rioObject)
        {
            return "R-6054";
        }

        protected override string ExtractDocFileTypeAlias(R_6054.Proposal rioObject)
        {
            return GetDocFileTypeAlias(rioObject);
        }

        protected override DocumentTypeURI ExtractDocumentTypeURI(R_6054.Proposal rioObject)
        {
            return rioObject.DocumentTypeURI;
        }

        protected override string ExtractDocumentTypeName(R_6054.Proposal rioObject)
        {
            return rioObject.DocumentTypeName;
        }

        protected override DocumentURI ExtractDocumentURI(R_6054.Proposal rioObject)
        {
            return null;
        }

        protected override DateTime? ExtractApplicationSigningTime(R_6054.Proposal rioObject)
        {
            return null;
        }

        protected override string ExtractEmailAddress(R_6054.Proposal rioObject)
        {
            return null;
        }

        protected override bool ExtractSendConfirmationEmail(R_6054.Proposal rioObject)
        {
            return false;
        }

        protected override ElectronicServiceApplicant ExtractElectronicServiceApplicant(R_6054.Proposal rioObject)
        {
            return null;
        }

        protected override ElectronicServiceApplicantContactData ExtractElectronicServiceApplicantContactData(R_6054.Proposal rioObject)
        {
            return null;
        }

        protected override ElectronicServiceProviderBasicData ExtractElectronicServiceProviderBasicData(R_6054.Proposal rioObject)
        {
            return rioObject.ElectronicServiceProviderBasicData;
        }
    }
}
