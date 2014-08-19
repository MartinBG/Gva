using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftDocumentDebtFMDO
    {
        [Required(ErrorMessage = "Registration is required.")]
        public PartSelectDO Registration { get; set; }

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
    }
}
