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

namespace Mosv.RioBridge.Extractions.AttachedDocDo.Common
{
    public class R6064ApplicationDataDoExtraction : ApplicationDataDoExtraction<R_6064.ContainerTransferFileCompetence>
    {
        protected virtual string GetDocFileTypeAlias(R_6064.ContainerTransferFileCompetence rioObject)
        {
            return "ContainerTransferFileCompetence";
        }

        protected override string ExtractDocFileTypeAlias(R_6064.ContainerTransferFileCompetence rioObject)
        {
            return GetDocFileTypeAlias(rioObject);
        }

        protected override DocumentTypeURI ExtractDocumentTypeURI(R_6064.ContainerTransferFileCompetence rioObject)
        {
            return rioObject.DocumentTypeURI;
        }

        protected override string ExtractDocumentTypeName(R_6064.ContainerTransferFileCompetence rioObject)
        {
            return rioObject.DocumentTypeName;
        }

        protected override DocumentURI ExtractDocumentURI(R_6064.ContainerTransferFileCompetence rioObject)
        {
            return null;
        }

        protected override DateTime? ExtractApplicationSigningTime(R_6064.ContainerTransferFileCompetence rioObject)
        {
            return null;
        }

        protected override string ExtractEmailAddress(R_6064.ContainerTransferFileCompetence rioObject)
        {
            return null;
        }

        protected override bool ExtractSendConfirmationEmail(R_6064.ContainerTransferFileCompetence rioObject)
        {
            return false;
        }

        protected override ElectronicServiceApplicant ExtractElectronicServiceApplicant(R_6064.ContainerTransferFileCompetence rioObject)
        {
            return null; ;
        }

        protected override ElectronicServiceApplicantContactData ExtractElectronicServiceApplicantContactData(R_6064.ContainerTransferFileCompetence rioObject)
        {
            return null;
        }

        protected override ElectronicServiceProviderBasicData ExtractElectronicServiceProviderBasicData(R_6064.ContainerTransferFileCompetence rioObject)
        {
            return rioObject.SenderProvider;
        }
    }
}
