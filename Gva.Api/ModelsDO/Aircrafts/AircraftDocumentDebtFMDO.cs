using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftDocumentDebtFMDO
    {
        public AircraftDocumentDebtFMDO()
        {
            this.IsActive = true;
        }

        [Required(ErrorMessage = "Registration is required.")]
        public PartSelectDO Registration { get; set; }

        public int CertId { get; set; }

        public DateTime? RegDate { get; set; }

        [Required(ErrorMessage = "RegTime is required.")]
        public int? RegTime { get; set; }

        [Required(ErrorMessage = "AircraftDebtType is required.")]
        public NomValue AircraftDebtType { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime? DocumentDate { get; set; }

        public NomValue AircraftCreditor { get; set; }

        public string CreditorDocument { get; set; }

        public NomValue Inspector { get; set; }

        public bool IsActive { get; set; }

        public AircraftDocumentDebtCloseDO Close { get; set; }
    }
}
