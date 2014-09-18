using System;
using Common.Api.Models;

namespace Mosv.Api.ModelsDO.Admission
{
    public class AdmissionDO
    {
        public string IncomingLot { get; set; }

        public string IncomingNumber { get; set; }

        public DateTime? IncomingDate { get; set; }

        public NomValue ApplicantType { get; set; }

        public string Applicant { get; set; }

        public NomValue ProvideType { get; set; }

        public string Clarification { get; set; }

        public NomValue InformationType { get; set; }

        public NomValue Theme { get; set; }

        public string About { get; set; }

        public string Fee { get; set; }

        public NomValue PaymentType { get; set; }

        public bool PaidFee { get; set; }

        public NomValue Status { get; set; }

        public NomValue DecisionType { get; set; }

        public string AcceptanceResolutionNumber { get; set; }

        public DateTime? AcceptanceResolutionDate { get; set; }

        public NomValue DecisionDeadlineType { get; set; }

        public bool IsExtended { get; set; }

        public NomValue ExtensionReason { get; set; }

        public string ExtensionClarification { get; set; }

        public bool IsDoiDenied { get; set; }

        public NomValue DoiDenialReason { get; set; }

        public string DoiDenialClarification { get; set; }

        public bool IsDeniedByApplicant { get; set; }

        public NomValue ApplicantDenialReason { get; set; }

        public string ApplicantDenialClarification { get; set; }

        public bool HasAppeal { get; set; }

        public bool IsAppealed { get; set; }

        public NomValue AppealResult { get; set; }

        public NomValue DenialReason { get; set; }

        public string DenialClarification { get; set; }
    }
}
