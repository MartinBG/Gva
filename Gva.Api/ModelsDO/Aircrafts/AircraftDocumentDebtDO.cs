using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftDocumentDebtDO
    {
        public DateTime? RegDate { get; set; }

        [Required(ErrorMessage = "RegTime is required.")]
        public int? RegTime { get; set; }

        [Required(ErrorMessage = "AircraftDebtType is required.")]
        public NomValue AircraftDebtType { get; set; }

        public string ContractNumber { get; set; }

        public DateTime? ContractDate { get; set; }

        public string CreditorName { get; set; }

        public string CreditorNameAlt { get; set; }

        public DateTime? StartDate { get; set; }

        public NomValue Inspector { get; set; }

        public string StartReason { get; set; }

        public string StartReasonAlt { get; set; }

        public string LtrNumber { get; set; }

        public DateTime? LtrDate { get; set; }

        public DateTime? EndDate { get; set; }

        public NomValue CloseInspector { get; set; }

        public string EndReason { get; set; }

        public string CloseAplicationNumber { get; set; }

        public DateTime? CloseAplicationDate { get; set; }

        public string CloseCaaAplicationNumber { get; set; }

        public DateTime? CloseCaaAplicationDate { get; set; }

        public string Notes { get; set; }

        public string CreditorAddress { get; set; }

        public string CreditorEmail { get; set; }

        public string CreditorContact { get; set; }

        public string CreditorPhone { get; set; }

    }
}
