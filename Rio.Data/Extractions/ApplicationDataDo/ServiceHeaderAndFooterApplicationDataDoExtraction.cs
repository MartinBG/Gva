using System;
using System.Collections.Generic;
using System.Linq;
using R_0009_000003;
using R_0009_000001;
using R_0009_000016;
using R_0009_000137;
using R_0009_000002;
using R_0009_000152;
using R_0009_000153;

namespace Rio.Data.Extractions.ApplicationDataDo
{
    public abstract class ServiceHeaderAndFooterApplicationDataDoExtraction<TRioObject> : ApplicationDataDoExtraction<TRioObject>
    {
        protected virtual ElectronicAdministrativeServiceHeader GetElectronicAdministrativeServiceHeader(TRioObject rioObject)
        {
            return null;
        }

        protected virtual ElectronicAdministrativeServiceFooter GetElectronicAdministrativeServiceFooter(TRioObject rioObject)
        {
            return null;
        }

        protected virtual string GetDocFileTypeAlias(TRioObject rioObject)
        {
            return rioObject.GetType().Namespace.Replace('_', '-');
        }

        protected override string ExtractDocFileTypeAlias(TRioObject rioObject)
        {
            return GetDocFileTypeAlias(rioObject);
        }

        protected override DocumentTypeURI ExtractDocumentTypeURI(TRioObject rioObject)
        {
            var header = GetElectronicAdministrativeServiceHeader(rioObject);

            return header != null ? header.DocumentTypeURI : null;
        }

        protected override string ExtractDocumentTypeName(TRioObject rioObject)
        {
            var header = GetElectronicAdministrativeServiceHeader(rioObject);

            return header != null ? header.DocumentTypeName : null; 
        }

        protected override DocumentURI ExtractDocumentURI(TRioObject rioObject)
        {
            var header = GetElectronicAdministrativeServiceHeader(rioObject);

            return header != null ? header.DocumentURI : null; 
        }

        protected override DateTime? ExtractApplicationSigningTime(TRioObject rioObject)
        {
            var footer = GetElectronicAdministrativeServiceFooter(rioObject);

            return footer != null ? footer.ApplicationSigningTime : null;  
        }

        protected override string ExtractEmailAddress(TRioObject rioObject)
        {
            var applicant = ExtractElectronicServiceApplicant(rioObject);

            return applicant != null ? applicant.EmailAddress : null; 
        }

        protected override bool ExtractSendConfirmationEmail(TRioObject rioObject)
        {
            var header = GetElectronicAdministrativeServiceHeader(rioObject);

            return header != null ? header.SendApplicationWithReceiptAcknowledgedMessage : false; 
        }

        protected override ElectronicServiceApplicant ExtractElectronicServiceApplicant(TRioObject rioObject)
        {
            var header = GetElectronicAdministrativeServiceHeader(rioObject);

            return header != null ? header.ElectronicServiceApplicant : null; 
        }

        protected override ElectronicServiceApplicantContactData ExtractElectronicServiceApplicantContactData(TRioObject rioObject)
        {
            var header = GetElectronicAdministrativeServiceHeader(rioObject);

            return header != null ? header.ElectronicServiceApplicantContactData : null; 
        }

        protected override ElectronicServiceProviderBasicData ExtractElectronicServiceProviderBasicData(TRioObject rioObject)
        {
            var header = GetElectronicAdministrativeServiceHeader(rioObject);

            return header != null ? header.ElectronicServiceProviderBasicData : null; 
        }
    }
}
