using Rio.Data.RioObjectExtraction;
using Rio.Objects;
using Rio.Data.DataObjects;
using System.Collections.Generic;
using System.Linq;
using System;
using R_0009_000003;
using R_0009_000001;
using R_0009_000016;
using R_0009_000137;
using R_0009_000002;

namespace Rio.Data.Extractions.ApplicationDataDo
{
    public abstract class ApplicationDataDoExtraction<TRioObject> : RioObjectExtraction<TRioObject, DataObjects.ApplicationDataDo>
    {
        protected abstract string ExtractDocFileTypeAlias(TRioObject rioObject);

        protected abstract DocumentTypeURI ExtractDocumentTypeURI(TRioObject rioObject);

        protected abstract string ExtractDocumentTypeName(TRioObject rioObject);

        protected abstract DocumentURI ExtractDocumentURI(TRioObject rioObject);

        protected abstract DateTime? ExtractApplicationSigningTime(TRioObject rioObject);

        protected abstract string ExtractEmailAddress(TRioObject rioObject);

        protected abstract bool ExtractSendConfirmationEmail(TRioObject rioObject);

        protected abstract ElectronicServiceApplicant ExtractElectronicServiceApplicant(TRioObject rioObject);

        protected abstract ElectronicServiceApplicantContactData ExtractElectronicServiceApplicantContactData(TRioObject rioObject);

        protected abstract ElectronicServiceProviderBasicData ExtractElectronicServiceProviderBasicData(TRioObject rioObject);

        public override DataObjects.ApplicationDataDo Extract(TRioObject rioObject)
        {
            var applicationDataDo = new DataObjects.ApplicationDataDo();

            applicationDataDo.DocFileTypeAlias = ExtractDocFileTypeAlias(rioObject);
            applicationDataDo.DocumentTypeURI = ExtractDocumentTypeURI(rioObject);
            applicationDataDo.DocumentTypeName = ExtractDocumentTypeName(rioObject);
            applicationDataDo.DocumentURI = ExtractDocumentURI(rioObject);
            applicationDataDo.ApplicationSigningTime = ExtractApplicationSigningTime(rioObject);
            applicationDataDo.Email = ExtractEmailAddress(rioObject);
            applicationDataDo.SendConfirmationEmail = ExtractSendConfirmationEmail(rioObject);
            applicationDataDo.ElectronicServiceApplicant = ExtractElectronicServiceApplicant(rioObject);
            applicationDataDo.ElectronicServiceApplicantContactData = ExtractElectronicServiceApplicantContactData(rioObject);
            applicationDataDo.ElectronicServiceProviderBasicData = ExtractElectronicServiceProviderBasicData(rioObject);

            return applicationDataDo;
        }
    }
}
