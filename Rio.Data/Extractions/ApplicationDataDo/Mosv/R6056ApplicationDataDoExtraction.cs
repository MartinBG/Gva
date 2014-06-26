using System.Collections.Generic;
using System.Linq;
using Rio.Data.Extractions.AttachedDocDo;
using R_6006;
using Rio.Data.Extractions.ApplicationDataDo;
using R_0009_000003;
using R_0009_000001;
using System;
using R_0009_000016;
using R_0009_000137;
using R_0009_000002;

namespace Mosv.RioBridge.Extractions.AttachedDocDo.Mosv
{
    public class R6056ApplicationDataDoExtraction : ApplicationDataDoExtraction<R_6056.Signal>
    {
        protected virtual string GetDocFileTypeAlias(R_6056.Signal rioObject)
        {
            return "R-6056";
        }

        protected override string ExtractDocFileTypeAlias(R_6056.Signal rioObject)
        {
            return GetDocFileTypeAlias(rioObject);
        }

        protected override DocumentTypeURI ExtractDocumentTypeURI(R_6056.Signal rioObject)
        {
            return rioObject.DocumentTypeURI;
        }

        protected override string ExtractDocumentTypeName(R_6056.Signal rioObject)
        {
            return rioObject.DocumentTypeName;
        }

        protected override DocumentURI ExtractDocumentURI(R_6056.Signal rioObject)
        {
            return null;
        }

        protected override DateTime? ExtractApplicationSigningTime(R_6056.Signal rioObject)
        {
            return null;
        }

        protected override string ExtractEmailAddress(R_6056.Signal rioObject)
        {
            return null;
        }

        protected override bool ExtractSendConfirmationEmail(R_6056.Signal rioObject)
        {
            return false;
        }

        protected override ElectronicServiceApplicant ExtractElectronicServiceApplicant(R_6056.Signal rioObject)
        {
            return null;
        }

        protected override ElectronicServiceApplicantContactData ExtractElectronicServiceApplicantContactData(R_6056.Signal rioObject)
        {
            return null;
        }

        protected override ElectronicServiceProviderBasicData ExtractElectronicServiceProviderBasicData(R_6056.Signal rioObject)
        {
            return rioObject.ElectronicServiceProviderBasicData;
        }
    }
}
